---
published: false
layout: post
title: The 40 year old logging virgin
summary: >-
  There's only one way to fight a never-ending stream of monkeys
image: /assets/2020/logging-virgin.png

---

![splash](/assets/2020/logging-virgin.png)

I have a confession to make. Despite being a 40 year-old remarkably successful developer, I've never done proper logging. It's not that I've actively avoided it, it's just a combination of circumstances that led to where I am now, mainly because I was working on prototypes before handing them to *real* developers, or working on solutions so large that logging was handled elsewhere or by someone else.

Whatever the reason, I am here today in front of this project that I lone wolf, and it's in dire need of logging. This project basically processes an incoming stream of data and generates thousands of API calls that do what they're supposed to, most of the time. When something happens with those calls outside of 'most of the time', a discussion takes place between me and the fine folks responsible for the target API.

Usually those discussions quickly turn into someone asking me for some useless piece of additional information, seemingly to delay having to deal with it. One of their most effective ways of delaying work on an open ticket is to ask me for a list of all the API requests in the hour leading to the incident. I mean, if they can't properly investigate issues with their own API without the content of the requests, should *they* be logging these?

Asking for additional information just to delay having deal with something has a name, it's called putting a monkey on their back, it's like a super power, one they've got so comfortable using that soon I wound up with an entire barrel on my back. A barrel, in case you didn't know, is the appropriate term for a group of monkeys, something I googled just for this article in order to sound smarter than I am. Please let me know if it worked on twitter, and while you're in there you can also [follow me](http://twitter.com/intent/user?screen_name=LuisLikeIewis), it helps a lot.

There's only one way to fight a never-ending stream of monkeys: a log of all the API activity that they could peruse at their leisure. Transparency is the only effective weapon against monkey buskers. Yes, I googled that one too.