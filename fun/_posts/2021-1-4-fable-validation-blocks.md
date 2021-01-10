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

If there's anything worse than writing javascript, it's having to read the javascript that you write. Thanks to [Fable](https://fable.io), you can write javascript that you don't have to read, indeed, you don't even have to write it.

Similarly, [FSharp.ValidationBlocks](https://github.com/lfr/FSharp.ValidationBlocks) allows you to validate things without writing any validation code, only rules. Yes, you can try to do validation without it, but like going to the toilet without toilet paper, it's not impossible but it can be messy.

## Thanks, I'll take both ✌

You'd be forgiven for trying to combine them like Voltron, after all web apps often display forms that require validation. Unfortunately the reflection that makes `FSharp.ValidationBlocks` magical also made it incompatible with Fable.

## No happy ending? 💔

Wait, in a plot twist that everybody saw coming, you no longer need to choose between <span style="color:magenta">the weapon that magically spawns javascript that someone else can be proud of because you certainly ain't gonna read it yourself</span>, and <span style="color:green">the shield that magically protects you against impure content like toilet paper</span>.

These things can be used together now, and all it took was adding `.Fable` to the namespace `FSharp.ValidationBlocks` making a new namespace `FSharp.ValidationBlocks.Fable`. Ok, it may have involved a little more work than that, but that's really my problem, not yours.

## Should I try it?

I don't know, let's see:

* Does doing <u>proper [ROP](https://fsharpforfunandprofit.com/rop/) validation without writing a single time <span style="font-family: monospace; background: #eee">Result<string, string list></span></u> sound good to you?
* Is <u>having all your guaranteed-to-be-valid types written with as little as 3 lines of code</u> music to your ears?
* Does <u>not having to think where to define validation rules</u> or <u>remember to call them</u> appeal to you?

I'm going to stop now because I ran out of sonic metaphors, but the point is, yes, [you should definitely try it](https://impure.fun/FSharp.ValidationBlocks/demo/)!