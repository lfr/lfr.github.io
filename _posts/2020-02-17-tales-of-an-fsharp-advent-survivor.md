---
published: true
layout: post
title: 'Tales Of An F# Advent Survivor'
summary: >-
  &#x1F3B5;&nbsp;Last Xmas, I gave you my blocks, but the very next day, validation did fail&nbsp;&#x1F3B5;
image: /assets/2020/advent-survivor.jpg
---
<p align="center">
    <img src="/assets/2020/advent-survivor.jpg" width="500">
</p>

Last Xmas I introduced **FSharp.ValidationBlocks**, a tiny F# library with huge aspirations. Looking back at [that post](/2019/12/19/advent-validation-blocks/) (please don't), I feel like I did a very poor job at conveying how exciting this library should be to anyone creating a model in F#.

## The *type*, that unsung hero

I also assumed that designing with types would be a given, but that assumption was based on nothing. I'll have to write something about it in the near future, it won't be as good as [this](https://fsharpforfunandprofit.com/series/designing-with-types.html), but at least I'll be adding my voice to his message, and I'll use my own library for the examples.

## That sweet F# Advent treat...

It's December 2019, and I've got to publish something. My library exists, I've been using it for months and it's awesome. But the public API sucks, I know it's not ready. There's no GitHub project. I don't even have a blog. What do I do? I consider cancelling my Advent entry. NO. The point of the entry was precisely for me to kick myself in the behind and make me share it with the world. It's not ready, but it's brilliant, and because I have no platform whatsoever the F# Advent Calendar is virtually the only way for the world to hear about it.

## ...with a bitter aftertaste

It's done. I've created a lengthy post about my library, and an actual blog to host it (you're looking at it). There's no GitHub, but I did what I could and at least there's a Nuget package. Except that it doesn't work! 😱

After the break I realized the version I uploaded to Nuget didn't perform recursive validation (for blocks built on top of other blocks). A minuscule bug with huge consequences, due to a last minute change to make my public API better that failed to achieve even that. But the worst part is that I definitely wouldn't have time to publish the full project "early jan" as promised, and I would have to wait till "mid feb" for an opportunity to make things right.

## Not all was lost

The main appeal of my library has always been how it makes declaring new types simple & quick, and at least that part I knew I had nailed. Let's face it, 99% of you just skimmed through it (and I'm grateful nonetheless), but hopefully some of you at least got this out of it: that you can design with types with minimal code. It &sdot; Can &sdot; Be &sdot; Done. That was the most important part of my post, and I may have gotten everything else wrong, but if I convinced you of that, I did something right.

## Where there's a will, there's a GitHub project

I believe I made a much better case for the existence of **FSharp.ValidationBlocks** in the GitHub README, where you'll also find a complete description of the brand new public API made of a grand total of 2 functions `Block.value` and `Block.validate` that are so beautiful and simple that I get teary eyed whenever I think of them. If everyone deserves a second chance, [this is me asking for mine](https://github.com/lfr/FSharp.ValidationBlocks).

## Comments & Feedback

To give feedback please use my very sophisticated "Comment Engine" by replying to [this tweet](https://twitter.com/fishyrock/status/1229781726970437632).
