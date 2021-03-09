---
published: false
layout: post
title: Letting Go of the Past
summary: >-
  Journey to Functional Wonderland: Part 3
image: /assets/2021/past.png
---

![splash](/assets/2021/past.png)

Ever caught yourself spending an unreasonable amount of time thinking about how to name something, or pondering over architectural concerns that you couldn't care less about, out of fear that some choices may come back to haunt you?

As stated in the [intro to this series](https://impure.fun/fun/2021/02/16/journey-to-functional-wonderland/), most devs just care about **writing simple code that works**, but it's surprisingly easy to accidentally stray from that goal, and if your answer to the above is "yes" then this may have happened to you.

You've been creating and using functions your whole developer life, but if functions aren't exclusive to Functional Programming, what makes FP "functional" and, more importantly, how does it solve the problem above?

## Let's start with the 'programming' part

Programming is basically **modeling real world problems**. As the statistician [George Box](https://en.wikipedia.org/wiki/George_E._P._Box) said:

> All models are wrong but some are useful

If programming is modelling, and all models are wrong, one can safely define programming as the search for the simplest most useful model to solve a real world problem. Simplicity and usefulness are the goals, not accuracy.

The real world is seemingly made of objects for the most part, so we can understand how **appealing** Object Oriented Programming must have been when it first introduced the glorious object as the basic building block of a whole new programming paradigm back in the late fifties.

## A critical mind challenges everything

Let's get back to the premise that has made OOP so appealing, that the real world is mostly made of objects.

Can you think of anything that's not an object?

Here's a few:

- Energy
- Time
- Money (except bills & coins which are a tiny part of modern money)
  
Far from insignificant, these things that aren't objects are very real problems programs often try to solve for.

Objects are just a metaphor, an abstraction in your model meant to represent something concrete in the real world problem you're trying to solve for.

But if we can so easily come up with significant things that aren't objects, and efficiency, not accuracy, is what we're seeking anyway, does it make any sense to start from the premise that everything is an object? Don't get me wrong, **some** things are a fine fit for the object metaphor — even in Functional Programming — but everything?

The very real possibility that we've been forcing a sometimes inadequate metaphor on everything that pertains to code could in and off itself explain the contrived architectures you may come up with when designing OOP solutions, and consequently, the issues I mentioned at the very beginning of this article.

While some may rightfully point out to that many great apps were made with OOP, that's missing the point: It's not that the wrong tool for the job doesn't get the job done, it's all of the unnecessary frustration it may cause along the way.

And this problem doesn't scale well, the more complex the job, the greater the frustration.

## We're ready for a definition!

Now that we've established that programming is basically modelling, we can safely guess what Functional Programming is: modelling real world problems and solutions with (mostly) functions.

Remember, a model isn't trying to accurately recreate the real world, it's trying to efficiently solve for a problem. Solving for a problem isn't "stuff", it's "doing stuff", it's providing the appropriate output for a given input. It's a function. It always has been.

Functional Programming uses functions as the main building block of a program, with anything else that isn't a function (i.e. numbers, text, arrays, yes even objects) being rightfully relegated to either an input or an output of the solution, or some form of intermediate artifact immaterial to the outer scope.

## Everything is transformed

Even if you believe my argument that a lot of important things aren't objects, you may find that even less things in the real world are functions, including every single item in my list above.

I love your skepticism, but never forget that [a good model doesn't have to look anything like the real thing](https://twitter.com/vladikk/status/1335947978482339841), and even if it did, here's what [Antoine Lavoisier](https://en.wikipedia.org/wiki/Antoine_Lavoisier) would have to say about it:

> Nothing is lost, nothing is created, everything is **transformed**

The law of conservation of mass is one of the first laws of physics we learn at school as it's also one of the most intuitive and — conveniently — applies to **everything**, describing it as none other than **product of a transformation**!

A transformation is just a medieval name for a function, and that young alchemist, is the very topic of the next article.

## The journey continues ⛵

This is the first time I write for non-FP programmers, if you're out here and are interested in the next article in the series, please let me know by retweeting, liking, or replying to [this twitter thread](https://twitter.com/luwvis/status/1367410901863837700), and until next time, safe journey!