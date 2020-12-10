---
published: true
layout: page
title: F# ad astra series
summary: >-
  Take F# to the stars: The complete series ✨🚀✨
image: /assets/2020/ad-astra.png
---

<p>
  <a href="#anchor">
    <img src="/assets/2020/ad-astra.png" alt="splash" />
  </a>
</p>
Taking F# to the stars is a series about the building blocks necessary to create a solution that reliably and safely achieves its purpose without ever faulting, and trust me, this is something your starship needs.

When venturing in the functional world, you will hopefully very quickly learn how to create pure code that never generates exceptions. I know, this sounds like fantasy coming from traditional object oriented development, but it's very real.

While pure code is as close to perfection as humans will ever get, there are things you can't possibly control, like human input for instance.

## Saving our code from ourselves

We need something to ensure that our domain is always valid, regardless of what users type in text boxes or whatever other impure content is sent its way, and the solution to this problem — in fact the only effective solution — is to create a domain with self-validating custom types instead of generic types such as strings.

So if we already know the solution, what's the big deal? The problem is that this solution requires a lot more code, and nobody likes to write a lot more code to achieve similar results. This is where validation blocks come in.

For your convenience all 4 articles in this introductory series to validation blocks can be found below, in chronological order. If you only read one, just read the last one. If you only read two, read the first and last one. Whatever you read, I wish you a safe flight!

|[![](/assets/2020/not-the-string.png){width=200px}](fun/_posts/2020-03-04-these-arent-the-types.md)|<small>**These aren't the types you're looking for**<br><small>Imagine your object needs a property for text values. You declare it as string, right? Wrong! 😱</small></small><div width="15rem"></div><br>|
|:--|:--|
|<a href=""><img src="" alt="splash"></a>|<small>**These aren't the types you're looking for**<br>Imagine your object needs a property for text values. You declare it as string, right? Wrong! 😱</small><div style="width:15rem"></div><br>|
|<a href="/fun/2020/03/04/these-arent-the-types/"><img src="/assets/2020/not-the-string.png" alt="splash"></a>|<small>**These aren't the types you're looking for**<br>Imagine your object needs a property for text values. You declare it as string, right? Wrong! 😱</small><div style="width:15rem"></div><br>|
|<a href="/fun/2020/03/04/these-arent-the-types/"><img src="/assets/2020/not-the-string.png" alt="splash"></a>|<small>**These aren't the types you're looking for**<br>Imagine your object needs a property for text values. You declare it as string, right? Wrong! 😱</small><div style="width:15rem"></div><br>|

<hr id="anchor" />

<div class="posts">

  <div class="post">
    <h3 class="post-title">
    </h3>
    <a href="/fun/2020/03/04/these-arent-the-types/">
      <img src="/assets/2020/not-the-string.png" alt="splash">
    </a>


    Imagine your object needs a property for text values. You declare it as string, right? Wrong! 😱

  </div>

  <div class="post">
    <h3 class="post-title">
      <a href="/fun/2020/04/06/jack-in-the-box-code/">
        Jack-in-the-box code
      </a>
    </h3>
    <a href="/fun/2020/04/06/jack-in-the-box-code/">
      <img src="/assets/2020/jack-in-the-box.png" alt="splash">
    </a>


    Most of the time your code plays a beautiful song, but turn that crank for long enough...

  </div>

  <div class="post">  
    <h3 class="post-title">
      <a href="/fun/2020/05/04/domain-fitness/">
        Put your F# domain on a diet
      </a>
    </h3>
    <a href="/fun/2020/05/04/domain-fitness/">
      <img src="/assets/2020/domain-fitness.jpg" alt="splash">
    </a>


    Do this one thing for instant domain weight loss!!! 🤩🤫🤭


  </div>

  <div class="post">
    <h3 class="post-title">
      <a href="/fun/2020/06/30/take-it-to-the-stars/">
        Take F# to the stars
      </a>
    </h3>
    <a href="/fun/2020/06/30/take-it-to-the-stars/">
      <img src="/assets/2020/take-it-to-the-stars.png" alt="splash">
    </a>


    Your airtight domain will keep you safe

  </div>

</div>
