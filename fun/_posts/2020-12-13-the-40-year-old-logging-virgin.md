---
published: false
layout: post
title: The 40 year old logging virgin
summary: >-
  Turns out logging is not just a glorified print
image: /assets/2020/logging-virgin.png
typora-root-url: C:\Users\lfr\source\repos\lfr.github.io
---

![splash](/assets/2020/logging-virgin.png)

<div class="message">
  <i>
    <p>
      This post is part of the English <a href="https://sergeytihon.com/2020/10/22/f-advent-calendar-in-english-2020/" target="_blank">2020 F# Advent Calendar</a>, check out the other posts there, and special thanks to Sergey Tihon for organizing it.  
    </p>
    <p>
      Don't put yourself through the pain of getting code to work from this article, there's a full working example <a href="/fun/scripts/logging-virgin.fsx" target="_blank">available here</a> for your convenience.
    </p>
  </i>
</div>

Whatever brought you here, you may think of logging as entirely un-sexy. Let me challenge that thought for a second: If UX is how apps talk to end users, logging is how programs talk to devs. This often overlooked line of communication is the key to your long term relationship with your production code.

## I have a confession to make

Despite being a 40 year-old accomplished developer, I've never done proper logging. I mean yes I have used a debugger output window, but that's hardly going beyond first base according to this famous table I just made up:

| Base        | Logging capability                       |
| --- | --- |
| First base  | Console                                  |
| Second base | Console + debugger output                |
| Third base  | Console + debugger output + rolling file |
| Home run    | Hosted centralized queryable logs        |

It's not that I've actively avoided logging, it's just a combination of circumstances that led me to where I am now, mainly because I was working on prototypes before handing them to *real* developers, or working on solutions so large that logging was handled elsewhere or by someone else.

## Virgin because reasons

Whatever the reason, today I have this project that I lone wolf, and it's in dire need of logging. It processes an incoming stream of data and generates thousands of API calls that do what they're supposed to, most of the time. When something happens to those calls outside of 'most of the time', a discussion takes place between me and the fine folks responsible for the target API.

Usually these discussions quickly turn into someone asking me for some useless piece of additional information, seemingly to delay having to deal with it. One of their most effective delaying methods consists of asking me for a list of all the API requests in the hour leading to the incident. I mean, if they can't properly investigate API issues without the content of the requests, shouldn't *they* be logging them? 🤔

As often is the case, who should do something matters less than whose problem it is. In this case they could just ask me for these logs so they were getting them for free and there was no way they were going to spend a dime in this problem. It was up for me to log their own API activity for them so that they could peruse it at their leisure. Sigh.

## Poor man won't help you

I try not to trivialize things that I'm not familiar with, but I have to admit I've often trivialized logging thinking how hard can it possibly be? While not particularly hard, turns out logging is not just a glorified print either.

At the beginning I went for a poor man's logging kind of approach, which is my favorite approach to all problems that I trivialize. I thought to myself that my code already does a decent amount of tracing, I could simply "redirect" it to some ELK stack and be done with it. How naïve of me.

## Logging done right

I understand how ridiculous it is for me to devsplain this after having done it only once, but let's be honest, this is what you're here for, otherwise you'd be reading a book, not a blog. I've boiled the requirements for a log to be useful down to just two:

- Events can easily be grouped by type
- Event properties are informative and easily queryable

The second requirement is more of a requirement is just a matter of picking the appropriate logging framework and the right log event repository.

The first requirement however is the one that made me drop my Trace redirecting idea, because it would've required me to give all events an event type id, which is very unappealing to me. I don't want to maintain an event type list somewhere that needs attention whenever I log something new, I hate this kind of housekeeping.

## Enter Serilog

If you go to [Serilog's site](https://serilog.net/) they advertise their "powerful" structured logging as a major differentiator. Their structured logging mechanism relies on a **message template** DSL which is basically a more elaborate version of .NET's `String.Format`:

```fsharp
// this creates a log event of level "Debug"
logger.Debug(

  // this is the message template
  "{RequestMethod} {RequestPath} returned status \
   {StatusCode} {Status} in {Elapsed} ms",

  // the values referenced in the message template above
  response.RequestMessage.Method.Method,
  requestPath,
  int response.StatusCode,
  response.StatusCode,
  elapsedMs)
```

This code creates the following log entry:

```json
PATCH products returned status 200 OK in 2534 ms
  └►  {
        RequestMethod: "PATCH"
        RequestPath: "products"
        StatusCode: 200
        Status: "OK"
        Elapsed: 2534
      }
```

This is fanciful enough as it is, but so far nothing has been done about my first requirement of useful logs, the ability to group them by type.

## Implicit event types

Message templates are very useful to define a log entry that is both meaningful and readable, but these templates can also be used as an event type, because all events that have the same message template are also events of the same type.

Obviously, you don't necessarily want to be storing `"{RequestMethod} {RequestPath} returned status {StatusCode} {Status} in {Elapsed} ms"` but hashed to `0xBC116E09` and it looks a lot more like a natural identifier. Serilog's `RenderedCompactJsonFormatter` automatically adds this hash to the rendered json, and so does Seq which I'll get to later.

With both my requirements fulfilled, it's time to do the deed.

## Warning: may contain traces of F# 🥜

In order to create a run-of-the-mill third base logger, according to Serilog's doc you may start with something like this:

```fsharp
// this logger writes to a rolling file
// NOTE: be careful with 'use' bindings, they may dispose of
// the logger earlier than you anticipate
use logger =
  Serilog.LoggerConfiguration()
    .WriteTo
    .File("log.txt", rollingInterval = RollingInterval.Day)
    // override default level to see 'debug' and 'verbose' events
    .MinimumLevel.Verbose()
    .CreateLogger()

// assign this logger as the main static logger
Serilog.Log.Logger <- logger

// test the new logger
logger.Information "I did a log!"
```

While this works, my journey still had lots of surprises in store as I would soon find out.  My first issue with this approach is that I still had loads of tracing in my code that I didn't want to miss, and I also didn't want to convert to Serilog calls. In fact, some trace messages were being generated by referenced projects which I didn't want or couldn't touch. Thankfully, Serilog has a sink for that:

```fsharp
use serilogListener =
  // instanciate a new listener
  new SerilogTraceListener.SerilogTraceListener("Trace",
    Filter = EventTypeFilter SourceLevels.Verbose)

// remove all Trace listeners and add Serilog's
// ⚠ does not sem to work in .fsx files ⚠
Trace.Listeners.Clear ()
Trace.Listeners.Add serilogListener |> ignore
Trace.verbose "Trace routed to main logger"
```

The eagle eyed reader may have realized that `Trace.verbose` doesn't exist, indeed, it's a home made function, here's its definition: `box >> Trace.WriteLine`. Anyway so far so good, I can both create powerful log entries with my logger and it also collects all legacy Trace messages. This is going great!

## Or is it?

One issue with the logger we just created is that it collects all logs in a text file, but we have to open it to see things happening in real time. This is a step back from the debugger output window. If only we could get my logs to appear in the output window at the same time. Wait, there's a sink for that too! Enter `Serilog.Sinks.Debug`!

```fsharp
// this logger writes all events to output
use outputLogger =
  Serilog.LoggerConfiguration()
    .WriteTo.Debug()
    .MinimumLevel.Verbose()
    .CreateLogger()
```

To have all events from the main logger redirected to the one above, we add this line to its declaration:

```fsharp
// add this line to chain loggers
use logger =
  Serilog.LoggerConfiguration()
    // ...
    .WriteTo.Logger(outputLogger) // ← new
    .CreateLogger()
```

### Stack overflow, not the website, the Ꞙ***🤬 exception

Run it and you'll be hit with a stack overflow exception with zero stack trace. Your code just goes 💥&nbsp;boom&nbsp;💥 soon after launch without any information. If you've created a lot of code before running it, trust me this may cost you a night of sleep. Thankfully this 40 year-old ex logging virgin is here to tell you what happened.

## That escalated quickly

The problem is that Serilog's output logger writes using `System.Diagnostics.Debugger.Write` which also writes to the trace, and since we were capturing the trace to log it, there's an infinite logging loop of doom. The solution is to create our own debug sink, which is trivial:

```fsharp
/// This debug sink writes to output window without generating trace events
type NoTraceDebugSink () =

  // only needs to be changed if you want to tweak the appearance of output events
  let defaultDebugOutputTemplate =
    "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"

  let formatter =
    MessageTemplateTextFormatter(defaultDebugOutputTemplate, null)

  interface ILogEventSink with
    member _.Emit logEvent =
      use buffer = new StringWriter()
      formatter.Format(logEvent, buffer)
      Debugger.Log (0, null, string buffer) // ← this does the trick
```

The code above is super boilerplaty, only the last line matters as it writes to the debugger output window using `Debugger.Log` which does not create a Trace event. This is exactly what we want so let's use our new sink:

```fsharp
// replace stock sink with the one above
use outputLogger =
  Serilog.LoggerConfiguration()
    .WriteTo.Sink<NoTraceDebugSink>() // ← new
    .MinimumLevel.Verbose()
    .CreateLogger()
```

Farewell stack overflow, you shall not be missed.

## Not quite done yet

When my existing process runs in interactive mode, it displays a whole bunch of stuff in the terminal. I quickly realized that a lot of what I displayed in the terminal was almost a duplicate of some of my events.

The next logical step was to stop writing these status reports to the terminal altogether, and instead let some events appear in the console as the execution happens. Thankfully (say it with me) there's a sink for that! It's called [Serilog.Sinks.Console](https://github.com/serilog/serilog-sinks-console) so let's instanciate it and daisy chain it:

```fsharp
// this logger writes information level events to the console
use consoleLogger =        
  Serilog.LoggerConfiguration()
    .WriteTo.Console()
    .Filter.ByIncludingOnly(fun e -> e.Level = LogEventLevel.Information)
    .CreateLogger()

// chain it to the main logger
use logger =
  Serilog.LoggerConfiguration()
    // ...
    .WriteTo.Logger(consoleLogger) // ← new
    .CreateLogger()
```
With the changes above now I can send notifications about the main milestones of my process to both the console as well as any configured sink with the same line:

```fsharp
Serilog.Log.Information "Something worthy of being displayed in the terminal"
```

That's it, not only did we get rid of pesky `printf` lines, we can also follow execution highlights of the process both in the terminal as well as on any of our sinks configured above.

## Time for the home run

It's been a long journey, but believe it or not between third base and the home plate there's only a single line of code. We need a hosted centralized log repository, so we need to move away from the rolling file. I'm going to use [Serilog.Sinks.Seq](https://github.com/serilog/serilog-sinks-seq) here but since this choice only impacts one line of code, feel free to do your research and try different solutions.

```fsharp
// replace the rolling file with Seq
use logger =
  Serilog.LoggerConfiguration()
    .WriteTo
    
    //.File("log.txt", rollingInterval = RollingInterval.Day) ← old
    .Seq("http://your.seq.host", apiKey = "y0ur4p1k3y==")  // ← new

    // same as before:
    .WriteTo.Logger(outputLogger)
    .WriteTo.Logger(consoleLogger)
    .CreateLogger()
```
That's it! Our logs now appear in Seq almost in real time!

!!SEQ_GIF_MISSING!!

You can click on any event to see the details. Event properties that aren't scalars (strings, numbers) can be further drilled down into!

![seq](/assets/2020/seq.png)

If the eagle eyed reader is still with us, he may have noticed the `Request` and `Response` properties that aren't nowhere in the message template. You can add properties to events without polluting the message template by using `ForContext` and naturally you can expand them to see their properties, this is important so event lines remain concise.

```fsharp
logger
  .ForContext("Request", response.RequestMessage, true)
  .ForContext("Response", response, true)
  .Debug(...) // same as above
```

The last boolean parameter indicates that the object should be *destructured* instead of displayed with `.ToString()`.

## This journey isn't quite over yet

In fact, `HttpRequestMessage` and `HttpResponseMessage` are not good candidates to be used as-is. They have too many properties, none of which contain the request's raw content that is the main thing someone needs to replicated it. But the worst part is that the response also contains the request in one of its properties. The best thing to do here is to take the last step in our journey, and customize how certain objects are destructured which we'll do with an extension method:

```fsharp
[<Extension>]
type LoggingExtensions =

  [<Extension>]
  static member ConfigDestructuring(config:Serilog.LoggerConfiguration) =
    config
      // this does not work in FSI
      .Destructure.ByTransforming<HttpRequestMessage>(
      	
      	// actual useful code, part 1
        fun r ->
          {|
            FullUri = r.RequestUri
            Raw = getRawContent r
          |} |> box)
          
      .Destructure.ByTransforming<HttpResponseMessage>(
      
      	// actual useful code, part 2
        fun r ->
          {|
            Raw = r.Content.ReadAsStringAsync().Result // doesn't handle null content!
            Headers = getResponseHeaders r
            TrailingHeaders = r.TrailingHeaders
            StatusCode = int r.StatusCode
            Message = r.ReasonPhrase
          |} |> box)
```

As you can see customizing how http messages are destructured simply consists of providing a function that returns whatever we want, which we define as an anonymous record with just the properties we need. Our new logic can be added to the main logger with the following line:

```fsharp
// add our own custom destructuring logic
use logger =
  Serilog.LoggerConfiguration()
    .WriteTo
    .Seq("http://your.seq.host", apiKey = "y0ur4p1k3y==")        
    .ConfigDestructuring() // ← new
    // ...
    .CreateLogger()
```
With this change, HTTP messages are way more readable when added as properties of any event, and they also don't contain any authentication headers. Using this simple method you can override how any object is logged which allows you to log complex objects in ways that are far more readable or useful!

## You have reached your destination

This may seem like a lot of code just for logging, but most people won't need to handle trace events so an entire chunk of it can be ignored, and this is setup code that's very easy to follow, to maintain, and that you write once and almost never touch again.



## A word about monkeys 🐒

When someone asks you for additional information just to delay dealing with something it's called putting a monkey on your back, and if it goes unchecked you may wind up with an entire barrel of monkeys on your back.

A barrel, in case you're wondering, is the appropriate term for a group of monkeys, something I googled just for this article in order to sound smarter. Please let me know on Twitter if it worked, and while you're there you can also [follow me](http://twitter.com/intent/user?screen_name=LuisLikeIewis), and/or retweet this article's [tweet](http://twitter.com/LuisLikeIewis), both of which help a lot!