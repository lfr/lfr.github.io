---
published: true
layout: post
title: The Objects You Know and Love&nbsp;💟
summary: >-
  Journey to Functional Wonderland: Part 2
image: /assets/2021/objects.png
---

![splash](/assets/2021/objects.png)

This journey starts from a surprisingly familiar place. When considering Functional Programming as an alternative to Object Oriented Programming, it's tempting to take the shortcut **functions** vs. **objects**, and understandably ask: what's wrong with objects?

<div class="message">
  This article was originally the first in the series, if you've read it before feel free to skip ahead or read the <a href="/fun/2021/02/16/journey-to-functional-wonderland/">re-written first article</a>.
</div>

## Nothing's wrong with objects!

Consider the following (decidedly informal) definition of an object:

- It's made of a state with properties, but it may also have no state, and/or no properties
- It can have methods, but it may also have no methods
- It has a constructor that may or may not initialize state
- It may or may not contain one or more references to other objects that may also reference it in return
- It may inherit any number of properties or methods from its parent, its parent's parent, and so on

That last point notwithstanding, this definition makes objects a pretty useful concept for programming.

## Wait, so objects are good?

The issue with OOP has never been with *objects* so much as with the *orientation* part.

Objects are not inherently evil, but the above definition means objects are truly a terrible, horrible, no bueno, very bad **basic building block** for a programming paradigm. Why? Because basic building blocks are supposed to be the **simplest composable element** you can think of.

Now re-read that definition and ask yourself: does it read like a definition of a simplest composable element of anything?

Why does this matter? Because when you compose an **architecture**, you want to focus on the composition and forget about the constituent parts, and it's object **orientation** — meaning the choice of making objects the basic building block, not objects themselves — that makes this impossible.

## Speaking of architecture

Take bricks 🧱 for instance, they're an excellent basic building block because when you conceptualize a wall you no longer care about the individual bricks, only the outside boundaries of the wall matter. Let's say a wall is defined as follows:

- It's made of several bricks with mortar joints between them
- It may have windows or doors

Like **objects**, walls are useful too, but if we were to use walls instead of bricks as a basic building block for construction, we'd have to change the definition above:

- It can be a single brick, or it can be a composition of (single-brick) walls
- It has mortar joins, but not if it's a single-brick wall
- It may have windows or doors, but only when it's a composition of bricks

What happens when you pick the wrong basic building block is that you end up **having to make all of its features optional**, which is bad because optional features have to be declared, either explicitly or by convention, and their presence or absence has to always be considered.

### Objects are truly nothing like bricks

The object oriented paradigm makes it very hard to only consider the outside shape of any composition because you seldom can afford to forget about its constituent objects, namely, you need to keep track of:

- state
- references
- lifecycle

Not only you have to consider the above for the classes that you use, but also for their parents and their parent's parents, and so on. They are really terrible basic building blocks.

## But I code without doing any of this!

You can afford to code without doing some of this, some of the time, using patterns and practices with fancy names such as Inversion of Control, but no amount of patterns will let you ever reach a place where you don't have to do **any** of this **all** of the time.

Such patterns exist not because they're good coding practices, but because they're good OOP disaster mitigation techniques.

As you venture further away from OOP, these arcane vestigial practices will no longer clog your brain or your code, as you will find that much of what they offer you get for free just for using a better paradigm.

## OOP is truly exceptional

Everything you can't avoid even with good practices can be seen as a mental & code tax, which you are paying, often unknowingly.

In this way OOP is like a bad itch that you no longer notice you're scratching. Every. Single. Day. Because when you stop scratching, you get exceptions.

![oop](/assets/2021/oop.png){:class="small"}

Exceptions are so frequent when building OOP solutions that they're generally accepted as necessary inconveniences towards a solution that's eventually stable enough to focus on actual functionality.

This is sad because there's an entire software development world out there where this silly workflow sounds like a terrible joke.

As you let go of objects as the basic building block of your code, exceptions start to become less and less commonplace, and in time you may find yourself writing code for hours — from scratch — and be rewarded with a brand new solution that just works.

## Your first basic spell ✨

It may come as a surprise but a lot of the wizardry ahead doesn't involve any maths or other scarier "m" words, and definitely none of that impenetrable category theory. All it takes is simply using **a better basic building block** for your code.

This will be the first and the most significant basic spell in your spell book, and it's exactly what I'll introduce next time!

## The journey continues ⛵

This is the first time I write for non-FP programmers, if you exist and are interested in the next article in the series, please let me know by retweeting, liking, or replying to [this article's tweet](https://twitter.com/luwvis/status/1361580444706361346?s=20), and until next time, safe journey!