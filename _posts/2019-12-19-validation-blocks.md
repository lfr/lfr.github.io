---
published: false
layout: post
title: 'This Year, To Save You From Tears, I Bring You ValidationBlocks'
---
<div class="message">
  <i>
    This post is part of the <a href="https://sergeytihon.com/2019/11/05/f-advent-calendar-in-english-2019/" target="_blank">F# Advent Calendar</a>, check out the other posts there, and special thanks to Sergey Tihon for organizing it.
  </i>
</div>

Have you ever found yourself perusing fsharpforfunandprofit's excelent [Designing with Types](https://fsharpforfunandprofit.com/series/designing-with-types.html) series, and said to yourself: _man, those single case unions are sweet_? Have you ever then found yourself digging a little deeper only to realize you'd need a type declaration like this, for every... single... type?

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
