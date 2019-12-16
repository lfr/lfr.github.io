---
published: true
layout: post
title: "\U0001F384&nbsp;Validation Blocks&nbsp;\U0001F384"
---
<div class="message">
  <i>
    This post is part of the english <a href="https://sergeytihon.com/2019/11/05/f-advent-calendar-in-english-2019/" target="_blank">F# Advent Calendar</a>, check out the other posts there, and special thanks to Sergey Tihon for organizing it.
  </i>
</div>

Have you ever found yourself perusing fsharpforfunandprofit's excelent [Designing With Types](https://fsharpforfunandprofit.com/series/designing-with-types.html) series, and said to yourself: _man, those single case unions are sweet_?

Have you ever then found yourself digging a little deeper only to realize you'd need a type declaration like this, for every... single... type?

```fsharp
// Code by Scott Wlaschin — fsharpforfunandprofit.com

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
If this sounds familiar, perhaps like me you've still decided for using a method similar to the above, because the advantages of coding against types guaranteed-to-be-valid far outweights the inconvenince of creating these type/module combinations.

Perhaps like me you've also been thinking that there must be a way to streamline this. If so, feel free to ignore the somewhat pointless backstory below and [jump straight to the actual topic of the post](#vbs).

### Enter RealText

Six months ago I set to do just that, and I created a library to declare such types in as little lines of code as possible. I narrowed the scope of it to text (string) types because I figured that narrowing the scope would increase my chances of reducing boilerplate code. I created it, I've used it, I thought it was great, I thought one day I'd share it with the world, and I even gave it a name: **RealText**. The excitement was palpable... and also short lived.

### Exit RealText

With the infinite wisdom that comes with hindsight, I realized my library was mostly pointless. While reducing the boilerplate, it still required some, which I kind of had to re-learn every time I had to create new types. The fact that it only supported text types meant I now had two paradigms, one for text, and one for non-text types. It also was a pain in the (‿ꜟ‿) to maintain, something that's a huge no-no for me. So without a second thought, I stashed the whole idea in the increasingly crowded metaphorical closet of deadend projects.

### When at first you don't succeed, go make babies

A few months later, after we had our second baby and during my paternity leave, I decided to re-tackle the problem with a fresh set of neurons. Learning from my mistakes, this time I decided to not limit any eventual solution to text types, and I also decided that the code necessary to define such types would be so small, that there would be no need to re-learn anything whenever it was necessary to define a new type.

<a name="vb" />
### Enter FSharp.ValidationBlocks

You know you've done something right when you can't possibly imagine writing any less code to declare or implement whatever you have in mind, and that is exactly what I felt about the second implementation of `RealText`, now called `FSharp.ValidationBlocks`, a more fitting name considering it supports all primitive types, not just `string`.

### How it works

A quick word before delving in the details. Unlike Scott's implementation above that <abbr title="I don't mean to cast shade on his implementation, ROP is beyond the scope of his article and I'm not even sure the Result type existed when he wrote it!">relies on</abbr> `Option` to differentiate between valid and invalid content, `FSharp.ValidationBlock`'s is natevely [ROP](https://fsharpforfunandprofit.com/rop/)-oriented, and failed validations should and in fact must return `Error`. One great advantage compared to the now-defunct `RealText` is that the type of `Error` returned is generic, meaning you can create your own validation errors in your domain with whatever parameters you need to properly display meaningful error messages. So without further ado, here's an example defining three types:

1. `FreeText`<br>&nbsp;Any non-blank text
2. `Text`<br>&nbsp;Any non-blank text without control characters (single line)
3. `Tweet`<br>&nbsp;Any non-blank text without control characters with maximum 280 characters

Before we begin, we should also define some appropriate errors in our domain, here I've used a validation-specific DU, but you don't have to, there's no constraints on `Error` type whatsoever.

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
            (fun s ->
                [if System.String.IsNullOrWhitespace s
                	then IsBlank])
```

No doubt this declaration raises a couple of questions, but I think one thing that's immediately obvious is that there's hardly any boilerplate code. 

* There's a type declaration with private constructor, nothing of interest here.
* There's an interface which serves two purposes: it identifies this type as a `ValidationBlock`, and it ensures that you implement the `Validate` function.
* There's a validation function declaration which is enforced by the aforementionned interface (so the compiler will _remind_ you to implement it) that is as simple as it can possibly be. It's a function of the primitive type (`string` in this case) that returns a list of `Error` under specific conditions.

Hopefully now you agree with me that declaring types with `FSharp.ValidatinBlocks` is reduced to the absolute minimum it could possibly be. It's not just the type declaration that's concise, creating a block is as simple as calling `Text.ofSring s`, giving you a `Result<'text,'error>`.

### They are actually really **blocks**

These validating types are meant to be built on top of each other, which where the _blocks_ part of the name comes in. To see this in action, let's continue implementing the remaining two types `Text` and `Tweet` from above.

```fsharp
/// Single line (no control chars) of non-blank text
type Text = private Text of FreeText with
    interface IText with
        member _.Validate =
            fun s ->
                [if s |> Regex("\p{C}").IsMatch
                    then ContainsControlCharacters]
```

Even though `Text` is defined as non-blank, we don't explicitely write this validation, instead, we _build on top_ of `FreeText` by declaring that `Text` is (private) `Text`of `FreeText` in the first line. The rest of the type declaration is straightforward, so now we're ready to declare the last type.

```fsharp
/// Maximum 280 characters, non-blank, no control chars
type Tweet = private Tweet of Text with
    interface IText with
        member _.Validate =
            fun s ->
                [if s.Length > 280 then
                	ExceedsMaximumLength 280]
```

The only new think of interest here is the use of parameters in the error case which allows you to present more meaningful errors to the user. Usually I simply generate english errors from the `TextError` case names, if there's no localization requirement the path from validation to presenting the user with meaningful errors can be extremely short.

### Seriously, let's talk Serialization for a second

Your types may happily live within the boundaries of your domain as awesome `ValidationBlocks`, but one day they have to leave your domain. I like to store my own blocks as their underlying primitive type, and most of the time your serialization needs are going to impose that. In other words, your `Tweet` will have to be serialized as a `string`, not as a `Tweet { Text { FreeText "Validate all the things!" } }`. For this reason, the library includes a `System.Text.Json.Serialization.JsonConverter` that does just that. Add it to your serialization options to ensure your blocks serialize to their primitive type and deserialize back to blocks.

### Final words

I've mostly covered the type declaration because for me that was the biggest disadvantage of the traditional way of designing with types. You want to declare types for anything that has specific validation needs so keeping these declaratinos compact is crutial, and when you think about it, almost any content that enters your domain can and probably should be validated.

But beyond type declaration, there's a few other things that I should mention before I end this article, the most important of which is the fact that there's a `Block.validate` function in the library that is **not** meant to be used directly from your code. You should only call this function once per primitive type in your code. In my solution I will have a `Text` module defined somewhere, it's the only place where I open `FSharp.ValidationBlocks`, and this module defines all the functions (usually a handful) that I use throught my domain. Here's an example of two functions that enable creation of `Text` blocks:

```fsharp
```


| Package | NuGet |
|---|:-:|
| FSharp.ValidationBlocks | [![NuGet](https://img.shields.io/nuget/v/FSharp.ValidationBlocks.svg?style=for-the-badge&logo=appveyor&)](https://www.nuget.org/packages/FSharp.ValidationBlocks/) |
