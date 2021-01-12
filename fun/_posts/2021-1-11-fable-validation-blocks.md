---
published: true
layout: post
title: Do not go gentle into that impure night
summary: >-
  Fable with FSharp.ValidationBlocks is now a thing 💙
image: /assets/2021/fable-validation-blocks.png
---

![splash](/assets/2021/fable-validation-blocks.png)

<div class="message">
  <p>
    <i>
      Don't miss the live demo link at the bottom of the article, it's juicy stuff
    </i>👇
  </p>
</div>

If there's anything worse than writing javascript, it's having to read the javascript that you write. Thanks to [Fable](https://fable.io), you can write javascript that you don't have to read, indeed you don't even have to write it.

Similarly, [FSharp.ValidationBlocks](https://github.com/lfr/FSharp.ValidationBlocks) allows you to validate things without writing any validation code, only rules. Yes, you can try to do validation without it, but like going to the toilet without toilet paper, it's possible but it can be messy.

## Thanks, I'll take both ✌

You'd be forgiven for trying to combine them like Voltron, indeed web apps often display forms that require validation. Unfortunately the reflection that makes `FSharp.ValidationBlocks` magical also made it incompatible with Fable.

## No happy ending? 💔

But hold on for a second - in a plot twist that everybody saw coming - these things can be used together now, and all it took was adding `.Fable` to the namespace `FSharp.ValidationBlocks` making a new namespace `FSharp.ValidationBlocks.Fable`. How did I not think of this before!

## Should I try it?

I don't know, let's see:

* Does doing <u>proper ROP validation without writing a single time <span style="font-family: monospace; background: #eee">Result<string, string list></span></u> sound good to you?
* Is <u>having all your guaranteed-to-be-valid types defined with as little as 3 lines of code</u> music to your ears?
* Does <u>not having to think where validation rules go</u> or <u>remember to call them</u> appeal to you?

I'm going to stop now because I ran out of sound metaphors, but the point is, yes, you should definitely try it, and thanks to Fable now [there's a live demo 🎁](https://impure.fun/FSharp.ValidationBlocks/demo/)! So meta…

## Disclaimer

Don't get triggered by my throwing shade at javascript, it's just for comedic purposes, I have a lot of respect for that language that got many things right from the beginning.