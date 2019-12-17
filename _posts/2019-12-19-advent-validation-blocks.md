---
published: false
layout: post
title: "\U0001F384&nbsp;Validation Blocks&nbsp;\U0001F384"
summary: >-
  Designing with types is awesome — and verbose. With FSharp.ValidationBlocks,
  now it can rightfully be just awesome.
image: /public/ValidationBlocks.png
---
<div class="message">
  <i>
    This post is part of the English <a href="https://sergeytihon.com/2019/11/05/f-advent-calendar-in-english-2019/" target="_blank">2019 F# Advent Calendar</a>, check out the other posts there, and special thanks to Sergey Tihon for organizing it.
  </i>
</div>

Have you ever found yourself perusing Scott's excellent [Designing With Types](https://fsharpforfunandprofit.com/series/designing-with-types.html) series, and said to yourself "_man, those single case unions are sweet_"?

Have you ever then found yourself digging a little deeper only to realize you'd need a type declaration like the one below, for every... single... type?

```fsharp
// fsharpforfunandprofit.com

/// Type with constraint that value must be non-null
/// and <= 50 chars.
type String50 = private String50 of string

/// Module containing functions related to String50 type
module String50 =

    // NOTE: these functions can access the internals of the
    // type because they are in the same scope (namespace/module)

    /// constructor
    let create str = 
        if String.IsNullOrEmpty(str) then
            None
        elif String.length str > 50 then
            None
        else
            Some (String50 str)

    // function used to extract data since type is private
    let value (String50 str) = str
```
If this sounds familiar, you probably still decided to use a method like the above, because the advantages of coding against types guaranteed-to-be-valid far outweigh the inconvenience of creating these type/module combinations.

But perhaps like me you've also been thinking that there must be a way to streamline this. If so, feel free to ignore the somewhat pointless backstory below and [jump straight to the actual topic of the post](#vbs).

### Enter RealText

Six months ago, I set out to do just that, and I created a library to declare such types in as little lines of code as possible. I narrowed the scope of it to text (string) types because I figured that narrowing the scope would increase my chances of reducing boilerplate code. I created it, I've used it, I thought it was great, I thought one day I'd share it with the world, and I even gave it a name: **RealText**. The excitement was palpable... and short lived.

### Exit RealText

With the infinite wisdom that comes with hindsight, I realized my library was mostly pointless. For one it still required some boilerplate code, which I kind of had to re-learn every time I had to create new types. Another problem was the fact that it only supported text types because it meant I now had two paradigms, one for text, and one for non-text types. So, without a second thought, I stashed away the whole idea in my increasingly crowded metaphorical closet of dead-end projects.

### When at first you don't succeed, go make babies

A few months later, after we had our second baby and during my paternity leave, I decided to re-tackle the problem with a fresh set of neurons. Learning from my mistakes, this time I decided to not limit any eventual solution to text types, and I also decided that the code necessary to define such types would be so small, that there would be no need to re-learn anything whenever it was necessary to define a new type.

<a name="vbs" />
### Enter FSharp.ValidationBlocks

You know you've done something right when you can't possibly imagine writing any less code to declare or implement whatever you have in mind, and that is exactly what I felt about the second implementation of **RealText**, now called **FSharp.ValidationBlocks** for it supports all primitive types, not just strings.

### How it works

A quick word before delving in the details. Unlike Scott's implementation above that relies on <abbr title="I don't mean to cast shade on his implementation, ROP is beyond the scope of his article and I believe Result didn't even exist when he wrote it">Option</abbr> to differentiate between valid and invalid content, Validation blocks are natively [ROP](https://fsharpforfunandprofit.com/rop/)-oriented, and failed validations return `Error`. One great advantage compared to the now-defunct **RealText** is that the type of error is generic, meaning you create your own validation errors as an existing or new <abbr title="Discriminated Union, F#'s sum type">DU</abbr> in your domain, as you would for any other error, with whatever parameters you need to properly display meaningful error messages. Here's an example defining three types:

1. `FreeText`<br>&nbsp;<small>Any <u>non-blank</u> text</small> (possibly multiline)
2. `Text`<br>&nbsp;<small>Any <u>non-blank</u> text with <u>no control characters</u></small>
3. `Tweet`<br>&nbsp;<small>Any <u>non-blank</u> text with <u>no control characters</u> and a <u>maximum length of 280</u></small>

Before we begin, we should also define some appropriate errors in our domain, here I've used a validation specific DU, but you don't have to, there's no constraints on `Error` type whatsoever.

```fsharp
type TextError =
    | IsBlank
    | ContainsControlCharacters
    | ExceedsMaximumLength of int
```

Let's start with `FreeText`, the least restrictive type.

```fsharp
type FreeText = private FreeText of string with
    interface IText with
        member _.Validate =
            fun s ->
                [if System.String.IsNullOrWhitespace s
                	then IsBlank]
```

No doubt this declaration raises a couple of questions, but I think one thing that's immediately obvious is that there's hardly any superfluous code. 

* There's a type declaration with private constructor, as expected

* There's an interface which serves two purposes: it identifies this type as a validation block, and it ensures that you implement the `Validate` function

* There's a validation function declaration (the compiler will _remind_ you to implement it) that is as simple as it can possibly be

Validation is always a function of the primitive type, `string` in this case, that returns a list of errors under specific conditions. It always takes the following form, so writing these functions is a pretty mindless task.

```fsharp
fun x ->
    [
        if x |> test1 then Error1
        if x |> test2 then Error2
        if x |> test3 then Error3
        // ...
    ]
```

In other words, declaring types with validation blocks is reduced to saying "this is a validation block" (using the interface) and "under these conditions, you get these errors" (implementing the interface), which I believe we can all agree is the absolute minimum amount of code one can expect to write to define this behavior. It's not just the type declaration that's concise, creating a block can be as simple as calling `Text.ofSring s`, which returns a `Result<'text, 'error>`.

### They are actually blocks

![ValidationBlocks logo](https://api.nuget.org/v3-flatcontainer/validationblocks/0.9.0/icon){: style="float: right; margin-left: 1rem;"}
These validating types are meant to be built on top of each other, which explains the _blocks_ part of the name. To see this in action, let's continue implementing the remaining two types `Text` and `Tweet` from above.

```fsharp
/// Single line (no control chars) of non-blank text
type Text = private Text of FreeText with
    interface IText with
        member _.Validate =
            fun s ->
                [if s |> Regex("\p{C}").IsMatch
                    then ContainsControlCharacters]
```

Even though `Text` is defined as non-blank, we don't explicitly write this validation, instead, we _build on top_ of `FreeText` by declaring that `Text` is a `Text`of `FreeText` in the first line, **the string will be automatically validated using the validation of `FreeText` before attempting the validation of `Text`**. The rest of the type declaration is just a validation that yields an error if the string contains control characters, so now we're ready to declare the last type.

```fsharp
/// Maximum 280 characters, non-blank, no control chars
type Tweet = private Tweet of Text with
    interface IText with
        member _.Validate =
            fun s ->
                [if s.Length > 280 then
                    ExceedsMaximumLength 280]
```

Again, we only declare the validation that's specific to `Tweet`, all other validation rules are implied by writing `Tweet of Text`. The only new thing of interest here is the use of parameters in the error case which illustrates the ability to present more meaningful errors to the user for more complex validation types. I follow a strict discipline of having error union cases that together with their parameters (if any) allow me to generate an error message that spell out *exactly* what went wrong, so in this case I can very easily display a message "The given tweet exceeds the maximum length of 280 characters", but I can also re-use the same error to display another message elsewhere that says "The given email exceeds the maximum length of 320 characters".

### Seriously, let's talk Serialization

Your types may happily live within the boundaries of your domain as awesome validation blocks, but one day they probably have to leave your domain. I like to store my own blocks as their underlying primitive type, allowing me to refactor my code without breking anything, and most of the time your serialization needs are going to impose that anyway. In other words, your `Tweet` will have to be serialized as a string, not as a `Tweet { Text { FreeText "covfefe" } }`. For this reason, the library includes a `System.Text.Json` JsonConverter that does just that. Add it to your serialization options to ensure all your blocks serialize to their primitive type and deserialize back to the correct block type.

### Final words

I've mostly covered the type declaration because for me that was the biggest disadvantage of the traditional way of designing with types. You want to declare types for anything that has specific validation needs so keeping these declarations compact is key, and when you think about it, almost any content that enters your domain can and probably should be validated.

But beyond type declaration, there's a few other things that I should mention before I end this article, the most important of which is the fact that there's a `Block.validate` function in the library that is **not** meant to be used everywhere in your code. You should only call this function **once per primitive** type. In my solution I will have a `Text` module defined somewhere, it's the only place where I open `FSharp.ValidationBlocks`, and this module defines the functions that I use throughout my domain. Here's an example of a few functions to create `Text` blocks and one to get a value out of them:

```fsharp
module Text

/// Wraps a string into a block of the expected type
let ofString<'text when 'text :> IText> s =
   (s.Trim()) |> Block.validate<'text, TextError>

/// Wraps a string into Some 'text, if not null/blank, otherwise None
let ofOptionalString<'text when 'text :> IText> s =
    if System.String.IsNullOrWhiteSpace s then None |> Ok
    else ofString (s.Trim()) |> Result.map Some
        
/// Non-ROP version of Text.ofString, may throw an exception
let ofUnchecked<'text when 'text :> IText> s =
   match ofString s with
   | Ok x -> x
   | Error e -> sprintf "Attempt to access error Result: %A." e |> failwith

/// Gets the string contained in a text block
let value (text:IText) = Block.unwrap text :?> string
```

Note that usually you'll be omitting the `'text` type parameter, meaning you'll be mostly calling `Text.ofstring s` instead of `Text.ofString<Tweet> s`. Of course you could use `Block.validate` and `Block.unwrap` directly in your code, but you shouldn't as you'd have to specify the error type every time. By writing my own `Text` module here I not only confine the references to `FSharp.ValidationBlocks` to a single module in my solution, but it also allows me to build in some string-specific generic behavior for all text blocks like trimming and checking for blanks before attempting to create a block. Similarly, you may have noticed the example types above all refer to an `IText` interface, but this interface doesn't exist in `FSharp.ValidationBlocks`, the actual interface in the library is `IBlock<'primitive, 'error>`. Again, you could use it directly, but your type declarations will be much cleaner if you declare the following interface, this single line of code will make all your type declarations much more readable, trust me.

```fsharp
type IText = inherit IBlock<string, TextError>
```

### Disclaimer

Having an actual day job, I barely managed to publish this article and create the NuGet package in time for Santa. The GitHub should be up early January. If you want to wait for that, whenever it's ready I'll post it on twitter so follow me if you'd like a notification. You can already play with it using the NuGet package below, but note that I mainly focused on the API, there's a ton of room for simple performance optimizations, especially with serialization performance, but that just hasn't been a priority for my project.



| Package | NuGet |
|---|:-:|
| FSharp.ValidationBlocks | [![NuGet](https://img.shields.io/nuget/v/FSharp.ValidationBlocks.svg?style=for-the-badge&logo=appveyor&)](https://www.nuget.org/packages/FSharp.ValidationBlocks/) |
