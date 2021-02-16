---
published: true
layout: post
title: An excellent journey to Functional Wonderland
summary: >-
  There's a whimsical land of infinite code bliss waiting for you, and the journey starts right here
image: /assets/glow.png
---

![splash](/assets/glow.png)

Wandering developer, your next adventure awaits. I know you're ready, because you've been ready the moment you wrote your first line of code.

This journey will bring you fame, of course, and glory, sooo much glory. But above all else, this journey will take you to a magical place you thought was forever lost, the place where the code you just wrote is **the most rewarding thing** you did today. Because it *feels* safe, neat, and robust, the moment you write it.

You will not be the first Object Oriented Programming developer to set sail, neither will you be the last, but for all that attempted this perilous but infinitely rewarding journey, it always started in the exact same place:

## Letting go of objects

Consider the following decidedly informal definition of an object:

- It's made of a state with properties, but it may also have no properties
- It can have methods, but it may also have no methods
- It has a constructor that may or may not initialize state
- It may or may not contain one or more references to other objects that may or may not reference it in return
- It may inherit any number of properties or methods from its parent, its parent's parent, and so on

That last point notwithstanding, this definition actually makes objects a pretty useful concept for programming.

## Wait, so objects are good?

Objects are not inherently evil, but the definition above makes them a **terrible**, **horrible**, **no bueno**, **very bad** basic building block for a programming paradigm. Why? Because whether in tech or elsewhere, basic building blocks are supposed to be the **simplest composable element** you can think of.

Now read that definition again and tell me — in all honesty — does that sound like a definition of a simplest composable element of anything?

This is important because when you compose an architecture, you want to focus on the composition and forget about the constituent parts.

## Speaking of architecture

Take a brick for instance, it's an excellent basic building block because when you conceptualize a wall of bricks, you no longer care about the individual bricks, only the outside boundaries of the wall matter. Let's say a wall is defined as follows:

- It's made of several bricks with mortar joints between them
- It may have windows or doors

Like objects in code, walls are useful too, but if we were to use walls instead of bricks as a basic building block for construction, we'd have to tweak their definition a little:

- It's made of bricks, but it can be just one brick
- It can have mortar joints, but only if it's made of two or more bricks
- It may have windows or doors, but not if it's a single brick

Does this definition look familiar? This is what happens when you pick the wrong basic building block, you end up having to make all of its features optional. Optional features are no good for basic building blocks because they systematically have to be declared, either explicitly or by convention, and their presence or absence has to be considered at all times.

### Objects are truly nothing like bricks 🧱

When you build an Object Oriented Programming (OOP) solution, you can't afford to forget about its constituent objects, you need to keep track of:

- state
- references
- lifecycle

In addition to this, you also have to keep track of all of the above that may be inherited from its parent, its parent's parent, and so on! They are really terrible basic building blocks.

## But I code without doing any of this!

You can afford to code without doing some of this using patterns and practices with fancy names such as Inversion of Control that are meant to mitigate issues inherent to OOP.

Such patterns exist not because they're good coding practices, but because they're good OOP disaster mitigation techniques. As you venture away from OOP, these arcane vestigial practices will no longer clog your brain.

Everything else you need to keep track of that's not mitigated by such patterns and practices you are already doing without thinking about it. In this way OOP is basically like a bad itch that you no longer notice you're scratching. Every. Single. Day. Because when you stop scratching, you get exceptions.

![oop](/assets/2021/oop.png){:class="small"}

Exceptions are so frequent when building OOP solutions that often one simply accepts them as necessary inconveniences towards an eventually stable solution 🤞, which is sad because things just don't have to be this way.

As one lets go of objects, or more accurately, as one starts using objects only when appropriate, exceptions start to become less and less commonplace, and in time you may find yourself writing code for hours from scratch and be rewarded with a brand new solution that just works!

## How can it be? 😱

In a twist that everybody saw coming, what gets us there is something called Functional Programming, but as it turns out it doesn't necessarily involve any maths or other scarier "m" words, and definitely none of that complicated category theory, all it takes is simply using **a better basic building block**, which is exactly what I'll introduce next time!

## The journey continues ⛵

This is the first time I write for non-FP programmers, if you exist and are interested in the next article in the series please let me know by retweeting, liking, or replying to [this article's tweet](https://twitter.com/luwvis/status/1361580444706361346?s=20), and until next time, safe journey!