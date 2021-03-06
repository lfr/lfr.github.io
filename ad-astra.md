---
published: true
layout: page
title: F# ad astra series
summary: >-
  Take F# to the stars: The complete series ✨🚀✨
image: /assets/2020/ad-astra.png
---

<br>
<p>
  <a href="#anchor">
    <img src="/assets/2020/ad-astra.png" alt="splash" />
  </a>
</p>
<br>

After embarking on your journey through functional spacetime you will hopefully soon acquire the superpower of creating code that never throws exceptions. This may sound like fantasy coming from a non-FP background, but it's very real and it even has a name: [pure code](https://en.wikipedia.org/wiki/Pure_function).

While pure code is as close to perfection as humans will ever get, there are things you can't possibly control, such as IO or network issues for instance, or human input. The code that deals with humans is by definition impure, and thus needs special attention, and this attention is the topic of this series.

## Saving our code from ourselves

Users can type all sorts of unexpected content in text boxes, and perfectly valid APIs calls can return invalid content. Surprisingly there's only one consistently reliable way to ensure that our domain is valid.

While known, the solution to this problem requires more code in the design phase, and nobody likes to write more code to achieve similar results.

## Validation blocks to the rescue

These series introduces the concept of validation blocks that dramatically reduce the amount of code necessary to create airtight domains.

As it's meant for beginners, you don't need to know how to design with types in order to follow along, but you do need at least some familiarity with F# syntax.

For your convenience all 4 articles can be found below, in chronological order. **If you only read one, just read the last one**. If you only read two, read the first and last one. Whatever you read, I wish you a safe flight! 🚀

<a id="anchor"><br></a>

|🔗|<small>Articles</small>|
|:-:|:--|
|[![](/assets/2020/not-the-string.png)](/fun/2020/03/04/these-arent-the-types/)|<small>**These aren't the types you're looking for**<br><small>Imagine your object needs a property for text values. You declare it as string, right? Wrong! 😱</small></small>|
|[![](/assets/2020/jack-in-the-box.png)](/fun/2020/04/06/jack-in-the-box-code/)|<small>**Jack-in-the-box code**<br><small>Most of the time your code plays a beautiful song, but turn that crank for long enough...</small></small>|
|[![](/assets/2020/domain-fitness.jpg)](/fun/2020/05/04/domain-fitness/)|<small>**Put your F# domain on a diet**<br><small>Do this one thing for instant domain weight loss!!! 🤩🤫🤭</small></small>|
|[![](/assets/2020/take-it-to-the-stars.png)](/fun/2020/06/30/take-it-to-the-stars/)|<small>**Take F# to the stars**<br><small>Your airtight domain will keep you safe</small></small><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|


