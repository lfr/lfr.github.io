---
published: false
layout: post
title: WTF is FP? A Definition for the Mildly Interested
summary: >-
  Journey to Functional Wonderland: Part 3
image: /assets/2021/what.png
---

![splash](/assets/2021/what.png)

Ever caught yourself spending an unreasonable amount of time thinking about how to name something, or pondering over architectural concerns that **you couldn't care less about**, out of fear that some choices may come back to **haunt you**?

As stated in the [intro to this series](https://impure.fun/fun/2021/02/16/journey-to-functional-wonderland/), most devs just care about **writing simple code that works**, but it's surprisingly easy to accidentally stray from that goal which is what may have happened if you answered "yes" to the above.

You've been creating and using functions your whole developer life, but if functions aren't exclusive to Functional Programming, what makes it "functional" and — more importantly — how can things you're already using possibly solve the problems you may still be facing?

All great questions, but I'd like to add one more: When you look at this picture, do you see a fridge?

<blockquote class="twitter-tweet" data-theme="dark"><p lang="en" dir="ltr">What do you see in the picture? A piece of cardboard? Some junk? No! — It’s a model!<br><br>Thread on models and bounded contexts 1/9<a href="https://twitter.com/hashtag/DDDesign?src=hash&amp;ref_src=twsrc%5Etfw">#DDDesign</a> <a href="https://twitter.com/hashtag/BoundedContext?src=hash&amp;ref_src=twsrc%5Etfw">#BoundedContext</a> <a href="https://t.co/URvUGh6Ho7">pic.twitter.com/URvUGh6Ho7</a></p>&mdash; Vladik Khononov (@vladikk) <a href="https://twitter.com/vladikk/status/1335947978482339841?ref_src=twsrc%5Etfw">December 7, 2020</a></blockquote> <script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>

## Bear with me

Programming is mostly **modeling real world problems**. As the statistician [George Box](https://en.wikipedia.org/wiki/George_E._P._Box) once said:

> All models are wrong but some are useful

If programming is modelling, and all models are wrong, one could define programming as devising **the simplest most useful model to solve a problem**. Simplicity and usefulness are the goals, not accuracy.

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

But if we can so easily come up with clearly important problems that aren't objects, and if efficiency, not accuracy, is what we're looking for anyway, does it make any sense to start from the premise that everything is an object? Don't get me wrong, *some* things are a good fit for the object metaphor — even in Functional Programming — but *everything*?

## Let's stop naming all the things

It's fair to say that **OOP** focuses on what **is** while **FP** focuses on what **happens**, and this distinction dramatically influences the problems I mention in the intro.

What something *is* depends on the context, this for instance is why MVC/MVP/MVVM were created, because someone realized code was much simpler if you just used different objects specialized for the context in which they're being used.

But what something *is* also depends over time, in a lasting evolving business, wasting too much energy defining stuff that's bound to change is a poor investment of your time.

Interestingly, while what something *is* varies, what *happens* doesn't. The "+" in `2 + 2` is something that performs an arithmetic operation that you're all familiar with, and the context is irrelevant.

Had you written your functional application for King Arthur in CeltScript, you'd probably have used it to add cows, today you'd use the same code to add bitcoins, no refactoring necessary because you focused on what happens instead of naming, classifying and categorizing all the things.

While some may rightfully point out the many great apps that were made with OOP, that's missing the point: It's not that the wrong tool for the job doesn't get the job done, it's all of the unnecessary frustration it may cause along the way.

And this problem doesn't scale well, the more linearly complex the job is, the exponentially greater the frustration becomes.

## We're ready for a definition!

Now that we've established that programming is basically modelling, we can safely guess what Functional Programming is: Modelling real world problems and solutions with functions.

You still use all the familiar things like strings, ints, floats, array and yes, some like myself even use objects, but the occurrence of these is only dictated by the function signatures. They're either the input or the output of a function, or some intermediate artifact immaterial to the outer scope, but they're definitely never part of some grand hierarchical architecture that some poor soul had to contrive into existence.

Remember, a model isn't trying to accurately recreate the real world, it's trying to efficiently solve for a problem. Solving for a problem isn't "stuff", it's "doing stuff", it's providing the appropriate output for a given input. It's a function. It always has been.

## Everything is transformed

Even if you believe my argument that a lot of important things aren't objects, you may be thinking that even fewer things in the real world are functions, including every single item in my own list above.

I'm loving your skepticism, but remember that we're no longer trying to describe what *is* but rather what *happens*, which [Antoine Lavoisier](https://en.wikipedia.org/wiki/Antoine_Lavoisier) summarizes as:

> Nothing is lost, nothing is created, everything is transformed

The law of conservation of mass is one of the first laws of physics we learn at school as it's also one of the most intuitive and — conveniently — describes "everything" as none other than the product of a transformation!

A transformation is just a medieval name for a **function**, and that young software alchemist, is the very topic of the next article.

## The journey continues ⛵

This is the first time I write for non-FP programmers, if you're out here and are interested in the next article in the series, please let me know by retweeting, liking, or replying to [this twitter thread](https://twitter.com/luwvis/status/1367410901863837700), and until next time, safe journey!