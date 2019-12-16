---
published: false
layout: post
title: "\U0001F384 Validation Blocks \U0001F384"
---
<div class="message">
  <i>
    This post is part of the <a href="https://sergeytihon.com/2019/11/05/f-advent-calendar-in-english-2019/" target="_blank">F# Advent Calendar</a>, check out the other posts there, and special thanks to Sergey Tihon for organizing it.
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

Perhaps like me you've also been thinking that there must be a way to streamline this.

Six months ago I set to do just that, and I created a library to declare such types in as little lines of code as possible. I narrowed the scope of it to text (string) types because I figured that narrowing the scope would increase my chances of reducing boilerplate code. I created it, I've used it, I thought it was great, I thought one day I'd share it with the world, and I even gave it a name: **RealText**. The excitement was palpable... and also short lived. With the infinite wisdom that comes with hindsight, I realized my library was mostly pointless. While reducing the boilerplate, it still required some, which I kind of had to re-learn every time I had to create new types. The fact that it only supported text types meant I now had two paradigms, one for text, and one for non-text types. It also was a pain in the (‿ꜟ‿) to maintain, something that's a huge no-no for me. So without a second thought, I stashed the whole idea in the increasingly crowded metaphorical closet of deadend projects.

| Package | NuGet
|---|:-:|
| FSharp.ValidationBlocks | [![NuGet](https://img.shields.io/nuget/v/FSharp.ValidationBlocks.svg?style=for-the-badge&logo=appveyor&)](https://www.nuget.org/packages/FSharp.ValidationBlocks/) |
