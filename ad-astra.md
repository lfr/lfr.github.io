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

**Take F# to the stars** is a series about these little known yet life-saving blocks that make ambitious F# projects also reliable and safe, which is something your starship needs, trust me.

After embarking on your journey through functional spacetime you will hopefully soon acquire the superpower of creating code that never throws exceptions. This sounds like fantasy coming from a non-FP background, but it's very real and it even has a name: [pure code](https://en.wikipedia.org/wiki/Pure_function).

While pure code is as close to perfection as humans will ever get, there are things you can't possibly control, like human input for instance. The code that deals with humans is by definition impure, and thus needs special attention.

## Saving our code from ourselves

Surprisingly there's only one way to properly ensure that our domain is always valid, regardless of what users type in text boxes or whatever garbage is returned by your perfectly valid API call, and that's to create a domain with self-validating custom types instead of generic types such as strings.

So if we already know the solution, what's the big deal? The problem is that this solution requires *a lot more* code in the design phase, and nobody likes to write *a lot more* code to achieve similar results.

## Validation blocks to the rescue

These series tries to introduce the concept of validation blocks that drastically reduce the amount of code necessary to create models with such self-validating custom types.

You don't need to know how to design with types to follow this series, it's meant for beginners, but you do need to be familiar with F# syntax.

For your convenience all 4 articles can be found below, in chronological order. **If you only read one, just read the last one**. If you only read two, read the first and last one. Whatever you read, I wish you a safe flight!

<a id="anchor"><br></a>

|🔗|<small>Articles</small>|
|:-:|:--|
|[![](/assets/2020/not-the-string.png)](/fun/_posts/2020-03-04-these-arent-the-types.md)|<small>**These aren't the types you're looking for**<br><small>Imagine your object needs a property for text values. You declare it as string, right? Wrong! 😱</small></small>|
|[![](/assets/2020/jack-in-the-box.png)](/fun/2020/04/06/jack-in-the-box-code/)|<small>**Jack-in-the-box code**<br><small>Most of the time your code plays a beautiful song, but turn that crank for long enough...</small></small>|
|[![](/assets/2020/domain-fitness.jpg)](/fun/2020/05/04/domain-fitness/)|<small>**Put your F# domain on a diet**<br><small>Do this one thing for instant domain weight loss!!! 🤩🤫🤭</small></small>|
|[![](/assets/2020/take-it-to-the-stars.png)](/fun/2020/06/30/take-it-to-the-stars/)|<small>**Take F# to the stars**<br><small>Your airtight domain will keep you safe</small></small><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|