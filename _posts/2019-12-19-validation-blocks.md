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
To the surprise of no one, my answer to both of these questions is yes, yet, I still decided on using the method above because the comfort of writing code against 100% validated values outweights the hassle of defining type/module combinations such as the one above.

| Package | NuGet
|---|:-:|
| ValidationBlocks | [![NuGet](https://img.shields.io/nuget/v/ValidationBlocks.svg?style=flat)](https://www.nuget.org/packages/ValidationBlocks/) |
