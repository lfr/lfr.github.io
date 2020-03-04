---
published: true
layout: post
title: These aren't the types you're looking for
summary: >-
  Imagine you need a property that will contain text values. You declare it as string, right? Wrong!&nbsp;&#x1F631;
image: /assets/2020/not-the-string.png
---

![splash](/assets/2020/not-the-string.png)

When adding a property meant to contain text to an object in your code, you usually just declare it as `string` without giving it a second thought. Here though, I'm asking you to give it a second thought, [as others did before me](https://fsharpforfunandprofit.com/posts/designing-with-types-intro/) and came to the same conclusion: there's a better way.

## If not string, then what?

Let's say instead of `string` you declare all your properties containing text as `object`. This absurd example is useful because in your mind you're probably thinking about all the reasons why `string` is a way more appropriate type than `object`:

1. `object` is too general and accepts things that are not valid for this property
2. `object` does a poor job at documenting what's valid for this property
3. `object` being too general means validation and error handling code is going to be necessary throughout the solution

If you agree with the above, you're already halfway through this journey, and I'm going to prove that to you with a concrete example. Let's say the property we're adding is 'author' containing an email. Email is text, and as we just saw `object` is not a good choice, so let's consider using `string` instead and just to be thorough let's let's replace `object` with `string` in the above 3 reasons and see what they look like:

1. `string` is too general and accepts things that are not valid for this property
2. `string` does a poor job at documenting what's valid for this property
3. `string` being too general means validation and error handling code is going to be necessary through the solution

Wait, what? They're all still true!

1. `string` is too general because there's a myriad of things that are strings and aren't valid emails, in fact most strings aren't emails
2. `string` does little to document the property, a `string` author could just as well be a first name / last name combination
3. `string` is never guaranteed to be an email until you actively validate its content, in fact, it's not even guaranteed to have any characters in it

So it turns out that `string` is only marginally better than `object`, and that's why...

## These aren't the types you're looking for

So what are the types that I'm looking for?... you may or may not ask depending on whether you made it this far in the article. The answer is simple, but its implementation may vary from straightforward to downright unwieldy depending on your choice of programming language and libraries. This article is only meant to open your mind about something we as programmers have been doing for ages without ever realizing that there's a better way. In the next post I'll be exploring this better way so do follow me on twitter to be notified when it's up.

## Comments & Feedback

I still don't have a comment section but you may give feedback by replying to [this article's tweet](https://twitter.com/fishyrock/status/1235169846083694592).
