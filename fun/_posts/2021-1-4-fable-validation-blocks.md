---
published: false
layout: post
title: Do not go gentle into that impure night
summary: >-
  Fable with FSharp.ValidationBlocks is now a thing
image: /assets/2021/fable-validation-blocks.png
typora-root-url: C:\Users\lfr\source\repos\lfr.github.io
---

![splash](/assets/2021/fable-validation-blocks.png)

If there's one thing worse than writing javascript, it's having to read the javascript that you write. Thanks to [Fable](https://fable.io), you can write javascript that you don't have to read, indeed, you don't even have to write it.

[FSharp.ValidationBlocks](https://github.com/lfr/FSharp.ValidationBlocks) also gives you a power, it's the power to ensure with minimal code that no impure content will ever enter your domain. Yes, you can do validation without it, but like going to the toilet without toilet paper, the result can be messy.

## I'll take both

It does sound like a match made in heaven, after all a lot of web apps display forms that require all sorts of validation. Alas, before this day you had to choose between the two because the reflection that makes `FSharp.ValidationBlocks` magical also made it incompatible with Fable 💔

You see, once defined with minimal code, the validation blocks can all be used with the same two functions `value` and `validate`, regardless of the primitive type (usually string) or the error type (usually a DU in your domain).

In fact those functions become so universal that you can change your entire domain without having to change your validation code because you'd just be changing `validate` into `validate`, and we can all agree that's a pretty mediocre use of your time.

## No more! 🎊🥳🎉

Now there's no need to choose between the *weapon that magically spawns javascript that someone else can be proud of because you certainly ain't going to read it yourself*, and the *shield that magically protects you against impure content like toilet paper*.

These things can be used together now, and all it took was adding `.Fable` to the namespace `FSharp.ValidationBlocks` to make a new namespace `FSharp.ValidationBlocks.Fable`. Alright, it may have involved a little more work, but that's really my problem, not yours.

## Should I try it?

I don't know, does *doing proper [ROP](https://fsharpforfunandprofit.com/rop/) validation without writing a single time `Result<string, string list>`* sound good to you? Is *having all your guaranteed-to-be-valid types be made of as little as 3 lines of code* music to your ears? Does *not having to think where to define validation rules or remember to call them* appeal to you? I'm going to stop now because I ran out of sonic metaphors, but the point is, yes, you should [try it](https://impure.fun/FSharp.ValidationBlocks/demo.html)!
