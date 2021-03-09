---
published: false
layout: post
title: Letting Go of the Past
summary: >-
  Journey to Functional Wonderland: Part 3
image: /assets/glow.png
---

Ever caught yourself spending an unreasonable amount of time thinking about how to name something, or pondering over architectural concerns that you couldn't care less about, out of fear some choices may come back to haunt you?

As stated in the [introduction to the series](https://impure.fun/fun/2021/02/16/journey-to-functional-wonderland/), most devs just care about writing simple code that works, but it's surprisingly easy to accidentally stray from that goal, and if your answer to the above is "yes" then this may have already happened to you.

You've been creating and using functions your whole developer life, but if functions aren't exclusive to Functional Programming, what makes FP "functional" and, more importantly, how does it solve the problem above?

## Let's start with the 'programming' part

You can boil down programming to **modeling real world problems**. As the statistician [George Box](https://en.wikipedia.org/wiki/George_E._P._Box) said:

> All models are wrong but some are useful

If programming is modelling, and all models are wrong, one can safely define programming as the search for the simplest most useful model to solve a real world problem. Simplicity and usefulness are the goals, not accuracy.

The real world is seemingly made of objects for the most part, so we can understand how **appealing** Object Oriented Programming must have been when it first introduced the glorious object as the basic building block of a whole new programming paradigm back in the late fifties.

## A critical mind challenges everything

Let's get back to the premise that makes OOP so appealing, the fact that the real world is mostly made of objects.

Can you think of anything that's not an object?

Here's a handful of things that aren't objects:

- Energy
- Time
- Money (except bills & coins which are a tiny part of modern money)
- Social media attention & engagement
- Data
  
Do you notice a pattern? Far from insignificant, things that aren't objects are usually the very real world problems that programming often tries to solve for.

Objects are just a metaphor, an abstraction in your model meant to represent something concrete in the real world problem you're trying to solve for.

But if some of the most significant things we solve for aren't objects at all, and efficiency, not accuracy, is what we're seeking anyway, we may have been using the wrong metaphor all long.

While some may rightfully point out to the fact that many great apps were made with OOP, that's missing the greater point that using the wrong tool for the job is bad not because it doesn't get the job done, but rather because of all the frustration it may cause along the way. And the more complex the job, the greater the frustration.

## So what's Functional Programming

Now that we've established that programming is basically modelling, we can safely guess what Functional Programming is: modelling real world problems and solutions with (mostly) functions, as opposed to objects.

Remember, a model isn't trying to accurately recreate the real world, it's trying to efficiently solve for a problem. Solving for a problem isn't "stuff", it's "doing stuff", it's providing the appropriate output for the given input. It's a function, it always has been.

Functional Programming uses functions as the main building block of a program, with anything else that isn't a function (i.e. numbers, text, arrays, yes even objects) being rightfully relegated to either an input or an output of the solution, or some form of intermediary supporting artifact immaterial to the outer scope.

## Everything is transformed

Even if you believe my argument that a lot of important things aren't objects, you may be skeptical that even less things in the real world are functions, including every single item in my list above.

I love your skepticism, but never forget that [a good model doesn't have to look anything like the real thing](https://twitter.com/vladikk/status/1335947978482339841), and even if it did, here's what [Antoine Lavoisier](https://en.wikipedia.org/wiki/Antoine_Lavoisier) would have to say about it:

> Nothing is lost, nothing is created, everything is **transformed**

The law of conservation of mass is one of the first laws of physics we learn at school as it's also one of the most intuitive and applies to **everything**, which, lo and behold, it describes as the product of a transformation.

A transformation is just a medieval name for a function, and that young alchemist, is the very topic of the next article.

## The journey continues ⛵

This is the first time I write for non-FP programmers, if you're out here and are interested in the next article in the series, please let me know by retweeting, liking, or replying to [this twitter thread](https://twitter.com/luwvis/status/1367410901863837700), and until next time, safe journey!