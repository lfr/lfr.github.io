---
published: true
layout: post
title: The Objects You Know and Love&nbsp;💟
summary: >-
  Journey to Functional Wonderland: Part 2
image: /assets/2021/objects.webp
---

![splash](/assets/2021/objects.webp)

<div class="message">
  <h4><a href="https://twitter.com/luwvis/status/1372144028565770248">#ApplesForLambda</a> Campaign</h4>
  iOS users, if you've been here before when some text was missing, I hope you give it another chance! 
</div>

This journey begins in a surprisingly familiar place. When comparing Functional Programming to Object Oriented Programming, it's tempting to take the shortcut **functions** vs. **objects**, and understandably ask: what's wrong with objects?

## Nothing's wrong with objects!

Consider the following (decidedly informal) definition of an object:

- It's made of a state with properties, but it may also have no state, and/or no properties
- It can have methods, but it may also have no methods
- It has a constructor that may or may not initialize state
- It may or may not contain one or more references to other objects that may also reference it in return
- It may inherit any number of properties or methods from its parent, its parent's parent, and so on

That last point notwithstanding, this definition makes objects a pretty useful concept for programming.

## Wait, so objects are good?

The problem with Object Oriented Programming has never been with *objects* so much as with the *orientation* part.

Objects are not inherently evil, but the above definition means objects are a truly terrible, horrible, no bueno, very bad **basic building block** for a programming paradigm. Why? Because basic building blocks are supposed to be the **simplest composable element** you can think of.

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

### Objects are nothing like bricks

The object oriented paradigm makes it very hard to only consider the outside shape of any composition because you seldom can afford to forget about its constituent objects, namely, you need to keep track of:

- state
- references
- lifecycle

Not only you have to consider the above for the classes that you use, but also for their parents and their parent's parents, and so on. They are really terrible basic building blocks.

## But I code without doing any of this!

You can afford to code without doing some of this, some of the time, using patterns and practices with fancy names such as Inversion of Control, but no amount of patterns will let you ever reach a place where you don't have to do **any** of this **all** of the time.

Such patterns exist not because they're good coding practices, but because they're good OOP disaster mitigation techniques.

As you venture further away from OOP, these arcane vestigial practices will no longer clog your brain or your code, as you will find that much of what they offer you get for free just for using a better paradigm.

## OOP is really exceptional

Everything you can't avoid even with good practices can be seen as a mental & code tax, which you are paying, often unknowingly.

In this way OOP is like a bad itch that you no longer notice you're scratching, except when you stop, because you get exceptions.

![oop](/assets/2021/oop.png){:class="small"}

Exceptions are so frequent when building OOP solutions that they're generally accepted as necessary inconveniences towards a solution that's eventually stable enough to focus on actual functionality.

This is sad because there's an entire world out there where this silly workflow sounds like a terrible joke.

When you let go of objects as the basic building block of your code, exceptions become more and more exceptional, until one day you'll be rewarded with a brand new solution created from scratch that just works, 

## Your first basic spell ✨

It may come as a surprise but a lot of the wizardry ahead doesn't involve any maths or other scarier "m" words, and definitely none of that impenetrable category theory.

All it takes is using **a simpler basic building block** for your code which we'll to very soon, but first, it's time to properly define Functional Programming, so head on to the [next article](/fun/2021/03/15/fp/) for that!