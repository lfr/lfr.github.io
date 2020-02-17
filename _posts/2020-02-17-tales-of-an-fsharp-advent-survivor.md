---
published: false
---
## Tales of an F# Advent survivor

Last Xmas I introduced `FSharp.ValidationBlocks`, a tiny new F# library with huge aspirations. Looking back at [that post](/2019/12/19/advent-validation-blocks/) (please don't), I feel like I did a very poor job at conveying how exciting this library should be to anyone creating a model in F#.

### The **type**, that unsung hero

I also assumed that designing with types would be a given, but this assumption is based on nothing. I think in the near future I'll probably write a post specifically about the topic using my own lib for the examples. I won't do as good a job as [this guy](https://fsharpforfunandprofit.com/series/designing-with-types.html), but at least I'll add my voice to his message.

### That sweet F# Advent treat...

It's December 2019 and I've got to publish something. My library exists, I've been using it for months and it's awesome. But the public API sucks, I know it's not ready. There's no GitHub project. I don't even have a blog. What do I do? I consider cancelling my Advent entry. NO. The point of the entry was precisely for me to kick myself in the backside and make me share it with the world. It's not ready, but it's brilliant, and because I have no platform whatsoever the 2019 F# Advent Calendar is virtually the only way for the world to hear about it.

### ...with a bitter aftertaste

It's done. I've created a blog and a lengthy post about my lib. There's no GitHub, but I did what I could and at least there's a Nuget package. Except that it doesn't work. When I came back from Xmas break I realized the version I uploaded to Nuget didn't perform validation for blocks built on top of other blocks, only for ground level blocks. A minuscule bug with huge consequences, due to a last minute change to make my public API better that failed to achieve even that. But the worst part is that I definitely wouldn't have time to publish the fullproject "early jan" as promised, and I would have to wait till "mid feb" for an opportunity to make things right.

### Not all was lost

The main appeal of my library was always how simple it makes declaring new types, and at least that part I knew I had nailed. I hope that for anyone that skimmed through my article (let's face it, 99% of you just skimmed through it, and I'm grateful nonetheless) at least got that out of it: that you can design with types with minimal code. It. Can. Be. Done. That was the most important part of my blog post, and I may have gotten everything else wrong, but if I convinced you of that, I did something right, and I really hope you'll take a look at the GitHub project, complete with a public API made of a total of two functions `Block.value` and `Block.validate` that are so beautiful and simple that I get teary eyed just from looking at them. If everyone deserves a second chance, [this is me asking for mine](https://github.com/lfr/FSharp.ValidationBlocks).
