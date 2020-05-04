---
published: true
layout: post
title: Put your F# domain on a diet
summary: >-
  Do this one thing for instant domain weight loss!!!! 🤩🤫🤭
image: /assets/2020/domain-fitness.jpg
---

![splash](/assets/2020/domain-fitness.jpg)

<div class="message">
  <i>
    In this post often mention strings, but everything's applicable to any scalar type, not just strings.
  </i>
</div>

In my [previous article](/fun/2020/04/06/jack-in-the-box-code) I wrote some uninspired but effective code that illustrates how custom types can be easily added to your domain to replace strings, requiring some additional type declaration code but resulting in a more robust solution with less validation and error handling code.

## Trust me, it's a thing

Creating such types and defining your domain with them instead of strings is sometimes referred to **designing your domain with types**. *Type* here implies it's a type that you create for that specific thing in your domain, complete with the validation that prevents it from ever existing with invalid content.

## Your last name is not any string

This sort of design means you don't define *last name* as a string, because strings that are empty or contain control characters are never valid names. So you create a type specific for *last name* that contains that rule embedded in it.

Declaring such a type carries some verbosity cost of course, so you'll probably reuse that type for *first name* as well, those usually share the same validation. The point of designing with types is not to create as many types as possible, but rather to make it impossible for your domain entities to be populated with invalid content without relying on validation logic defined outside of your domain. In other words, to **make illegal states unrepresentable**.

## Verbosity cost

There's a cost, but it's not a net cost, it's more like a trade. Think about it this way: for your app to function correctly, if you declare all your domain text fields as strings, you'll be doing that validation somewhere else, likely requiring similar amount of code.

What's more, when debugging illegal states you may be tempted to add extra error handling code to swipe the problem under the rug. No judgement, we've all done it, for some reason errors don't always happen at a convenient time for you to perform a complete differential diagnosis.

![Dr House](/assets/2020/house.gif)

## Time for domain fitness

So this domain bloat is worth it, but that doesn't mean we should stand idle while our once-elegant domains get fatter and fatter, especially when it can be avoided. While designing with types will never *ever* enjoy the brevity of designing with strings, [there's ways](https://github.com/lfr/FSharp.ValidationBlocks) to dramatically reduce the amount of code that it requires. Judge for yourself, here's a type from my previous article:

```fsharp
// Object oriented style Email type with embedded validation
type Email private (s:string) = class end with
    static member Validate = function
    | s when String.IsNullOrWhiteSpace s ->
      Error "input is empty"
    | s when Regex.IsMatch(s, "^[^@]+@\w+.\w+$") ->
      Ok (Email(s))
    | _ -> Error "invalid email"
    member x.Value = s
```

And here's the same type defined with `FSharp.ValidationBlocks`:

```fsharp
// Email type using FSharp.ValidationBlocks
type Email = private Email of Text
  interface TextBlock
    with member _.Validate =
      fun s -> Regex.IsMatch(s, "^[^@]+@\w+.\w+$") => InvalidEmail
```

There's a lot to unpack here, but I the point is clear, the code required to create types with embedded validation can be reduced.

## Let's dissect that Email type 🧐

While I won't deep dive into `FSharp.ValidationBlocks` in this article, I believe it's important to describe everything that's going on with that `Email` because each line is meaningful, so here's the same code with line numbers and a little bit less syntactic sugar:

```fsharp
(* 1 *)   type Email = private Email of Text

(* 2 *)  // interface TextBlock
(* 2' *)    interface IBlock<string, TextError>

(* 3 *)       with member _.Validate =

(* 4 *)      // fun s -> Regex.IsMatch(s, "^[^@]+@\w+.\w+$") => InvalidEmail
(* 4' *)        fun s -> [if Regex.IsMatch(s, "^[^@]+@\w+.\w+$") then InvalidEmail]
```

1. `Email` is a single-case single-field union with a private constructor, so it cannot exist by directly calling the constructor, this ensures that if you have an email anywhere in your code it **has been validated**.
2. This interface identifies the `Email` union type as a ValidationBlock of string having an error type of `TextError`, which is just an enumeration of all possible text validation errors somewhere in your project. In real world examples, I abbreviate this interface to just `TextBlock` because I'll be reusing for every string-type block declaration.
3. In addition to identifying the `Email` union type as a validation block, the interface above also enforces the declaration of the validating function that has the following signature: `'primitive -> 'error list`, or in our concrete example: `string -> TextError list`.
4. The actual validation rule is a trivial function of that enumerates a list of simple predicates and the errors that each yields. There's operators that enable associating predicates with errors with simpler syntax, but they're completely optional.

## Wait, what? How?

I'm aware the above code even with line by line explanations may raise more questions than it answers:

1. How do you create an `Email` block?
2. How do you access its value?
3. What's the `Text` type in `Email of Text`?
4. Where did the empty string check go?
  
All valid questions so let's go through them one by one:

1. **How do you create an Email block?**
  
   An `Email` block is created by calling `Block.validate "john.smith@gmail.com"`resulting in a `Result<Email, TextError>`

2. **How do you access its value?**

   You can access the content of an `Email` block using `Block.value email` or simply using the (experimental) operator `%` as in  `%email`

3. **What's the `Text` type in `Email of Text`?**

   Blocks are built on top of other blocks so that hey only declare the validation that's unique to the block itself, so here `Text` is just another block type to which `Email` delegates all non-email specific validation. Think of it like `Email` inheriting `Text`'s validation, without any of the object oriented inheritance antics.

4. **Where did the empty string check go?**

   There's no need to explicitly check for valid strings because `Text` can never be an empty string
  
## Enough explanations, let me see more code

Patience grasshopper, there's a lot here to digest. Introducing a completely different but ultimately leaner way to designing with types in F# is a domain fitness journey that takes time but will inevitably make your domain both more enjoyable to create and to maintain.

In the next article I'll go through a complete example but in the meantime remember, an elegant domain isn't just nicer to look at, it's also healthier. The less boilerplate code it has, the easier it is to spot issues.

## Feedback & more

If you enjoyed this article or have any comments please consider retweeting or replying to [this article's tweet](https://twitter.com/luislikeIewis/status/1247580130328940544), it's very appreciated.