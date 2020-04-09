---
published: true
layout: post
title: Stop writing jack-in-the-box code
summary: >-
  Most of the time your code works. Some of the time it blows up in your face. It's time to stick to the first scenario.
image: /assets/2020/jack-in-the-box.png
---

![splash](/assets/2020/jack-in-the-box.png)

<div class="message">
  <i>
    This post was published much later than expected due to the COVID-19 pandemic. While there's nothing here to bring you comfort, I hope you'll find some of it mildly entertaining.
  </i>
</div>

In my [previous article](/fun/_posts/2020-03-04-these-arent-the-types.md) I explained why it's time to move away from using `string` as the type of things in your code just because they're meant to contain text. That article was more of an appetizer, most of what matters is repeated here so feel free to continue reading even if you haven't read the previous one.

## Jack-in-the-box, the toy

Jack-in-the-box is a 14th-century children's toy that looks like a box with a crank that can be turned to play music. If one keeps turning the crank eventually something pops out to startle them. The good ones pop out randomly, as opposed to at the end of the song.

## Jack-in-the-box, the code

I like to think of loosely typed variables — such as an email field typed as `string` — as little jacks-in-the-box in your code. Most of the time the box plays a beautiful song, but turn that crank for long enough and you hit an edge case, and a nasty exception springs out of the box because that email string turned out to be an empty string. After all that's why you take your laptop when going on vacation.

## Your vacation deserves better

Just like the better quality versions of jack-in-the-box, invalid content in your domain usually breaks at unexpected places. This is not a problem with your architecture. Most (if not all) your code assumes that an email field is not an empty string, is not multiline, does not contain spaces, etc, because assuming the opposite means validating everywhere which is just not realistic.

Of course you can just validate the known entry points such as user input, but start adding shady APIs and morally flexible document DBs into the mix, and soon enough you'll have more "validation" code than "code" code in your solution.

## Goodbye Jack, you won't be missed

Saying goodbye to Jack is easy, and it even has a name, it's called **making illegal states unpresentable**. It's a mouthful, but as far as mouthfuls go, it's one of the most important ones I've come across. It means that if a thing is not supposed to be a different thing, design your domain so it can't possibly be that different thing.

Think about it, you type integer fields as `int` and text fields as `string` precisely so that integer doesn't turn out to be a string when you need to use it. But why settle for this rudimentary safety. If an email is always different than a country code field, why give them the same type. They are different types in real life, it's time for them to be different types in your code.

Don't just do it in the one place where you expect errors, do it everywhere. A string is never a string. Look closely and that endpoint that expects a string probably actually expects a string no longer than 32,768 characters with no tabs, so why use `string` when there's a readily available `32KnoTabString` type for you to use, or at least there will be after you create it.

## Ok, it requires *some* code

So the idea is to create types. **Lots** of types. Creating lots of types might not sound appealing depending on your programming language of choice, but this is absolutely a language limitation, not a technical one. This is how little code is necessary to declare a new type that represents non empty strings in [F#](http://fsharp.org), ready to replace all your strings that were probably never meant to be empty anyway. Good riddance.

```fsharp
// Non-empty string, single-case union style
type ActualText = private ActualText of string with
    static member New = function
    | s when String.IsNullOrEmpty s -> None
    | s -> Some (ActualText s)
    member x.Value = let (ActualText s) = x in s

// DISCLAIMER:
// Meant to illustrate the point above, it's not particularly good code
```

While this is more verbose than not declaring anything and using strings everywhere, think of all the exceptions these 5 lines of code prevent, and all the content-checking and validation code they render useless. It's usually cheaper (both in lines of code and in potential errors) to fix the problem at the source, and the source is your domain — where you define what a thing is.

## Tell it like it isn't

So it all boils down to making domains more explicit about what things **are** (an email *is* a string) and **aren't** (an email is not a multiline string), and this is done using types with all the necessary validation embedded into them. The concept is simple, and so is the code. Let's take a look at one way (of many possible ways) to define an `Email` type using object oriented style:

```fsharp
// embedding validation in the type itself (object oriented style)
type Email private (s:string) = class end with
    static member Validate = function
    | s when String.IsNullOrWhiteSpace s ->
      Error "input is empty"
    | s when Regex.IsMatch(s, "[^\w-+_.@]") ->
      Error "input contains invalid characters"
    | s when Regex.IsMatch(s, "^[^@]+@\w+.\w+$") ->
      Ok (Email(s))
    | _ -> Error "invalid email"
    member x.Value = s
```

Note that here a conscious choice was made to have more validation cases than necessary in order to have more meaningful errors. If brevity is your concern you can always have a single validation case with a universal "invalid" error message. It's still safer than using strings, albeit not particularly user friendly.

## Using your shiny new types

These types require some care. Remember, we got rid of jack-in-the-box, but we still have a box, and we still don't know what's in it until we open it. Call it an *appropriately labeled container*. Or call it `Result`. You may still find `Result` less convenient to use than `string` bindings, but consider the following two things:

1. `Result` will never blow up in your face (this is a good thing)
2. Functional languages have things that start with 'M...' but shall not be named that allow you to write almost exactly the same code that you would when using strings, but with none of the blowing up in your face inconvenience (also a good thing)
   
I'm not going to go deeper into the topic of `Result`, it's a huge topic and beyond the scope of this article. For now we'll just use a plain old match expression (POME) to unpack that box. Turns out this particular one would've blown up in our face. Bloody linebreakers...

```fsharp
// safely create and consume an email using embedded validation
let result = Email.Validate "do\n@syme.fs"

match result with
| Ok x -> consumeValidEmail x.Value
| e -> printfn "%A" e

// OUTPUT> Error "input contains control characters"
```

## There will be blocks

There's not a lot of code in this article, and it's not particularly good code either. The next one will have more and better code, but hopefully it's enough to illustrate the concept of designing with types. If you enjoyed it please consider retweeting [this article's tweet](https://twitter.com/lastIuis/status/1247101237935423488) to support the blog!