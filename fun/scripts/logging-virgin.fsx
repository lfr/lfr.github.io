(*
    This code was published alongide the article below:
    http://impure.fun/fun/_posts/2020-12-13-the-40-year-old-logging-virgin.md
    © 2020 Luis Ferrao
 *)

#r "nuget: Serilog"
#r "nuget: Serilog.Sinks.Console"
#r "nuget: SerilogTraceListener"

open System.Runtime.CompilerServices
open System.Diagnostics
open System.Net.Http
open System.Net.Http.Headers
open System.IO
open Serilog
open Serilog.Core
open Serilog.Events
open Serilog.Configuration
open Serilog.Formatting.Display



(****************************************************
 These utility functions can go anywhere in your code
 ****************************************************)

// returns a text version of the given request without
// authentication headers, so it can be tested in Postman or similar
// NOTE: implementing this is outside of the scope of this article
let getRawContent (r:HttpRequestMessage) =
  "<HttpRequestMessage_to_string_TBD>"

// returns a response's headers from it and its http content
// NOTE: this function is incomplete, it doesn't handle null content messages
let getResponseHeaders (r:HttpResponseMessage) =
  seq {r.Headers :> HttpHeaders; r.Content.Headers} |> Seq.concat

module Trace =
    let verbose = box >> System.Diagnostics.Trace.WriteLine



(********************************************************************
 Recommended location in your project: /Logging/NoTraceDebugSink.fs
 NOTE: Only needed if you log trace events, otherwise just use this:
 https://github.com/serilog/serilog-sinks-debug
 ********************************************************************)

/// This debug sink writes to output window without generating trace events
type NoTraceDebugSink () =

  // this line dictates the appearance of output events
  let defaultDebugOutputTemplate =
    "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"

  let formatter =
    MessageTemplateTextFormatter(defaultDebugOutputTemplate, null)

  interface ILogEventSink with
    member _.Emit logEvent =
      use buffer = new StringWriter()
      formatter.Format(logEvent, buffer)
      Debugger.Log (0, null, string buffer)



(*******************************************************************
 Recommended location in your project: /Logging/LoggingExtensions.fs
 *******************************************************************)

[<Extension>]
type LoggingExtensions =

  [<Extension>]
  static member ConfigDestructuring(config:Serilog.LoggerConfiguration) =
    config

      .Destructure.ByTransforming<HttpRequestMessage>(
      	
      	// defining our logged request object
        fun r ->
          {|
            FullUri = r.RequestUri
            Raw = getRawContent r
          |} |> box)
          
      .Destructure.ByTransforming<HttpResponseMessage>(
      
      	// defining our logged response object
        fun r ->
          {|
            Content = r.Content.ReadAsStringAsync().Result // doesn't handle null content!
            Headers = getResponseHeaders r
            TrailingHeaders = r.TrailingHeaders
            StatusCode = int r.StatusCode
            Message = r.ReasonPhrase
          |} |> box)



(**************************************************************
 Recommended location in your project: /Logging/Setup.fs
 'use' bindings will not work if this code is placed in its own
 module, instead use 'let' bindings and manually dispose at
 the very end of your program inside a 'finally' block
 **************************************************************)

// this logger writes to the console
use outputLogger =
  Serilog.LoggerConfiguration()
    .WriteTo.Sink<NoTraceDebugSink>() // ← new
    .MinimumLevel.Verbose()
    .CreateLogger()

// this logger writes information level events to the console
use consoleLogger =        
  Serilog.LoggerConfiguration()
    .WriteTo.Console()
    .Filter.ByIncludingOnly(fun e -> e.Level = LogEventLevel.Information)
    .CreateLogger()

// this logger writes to a rolling file
use logger =
  Serilog.LoggerConfiguration()
    .WriteTo

    // the line below replaces the .Seq line to simulate a Seq instance
    .Console(
        outputTemplate = "SIMULATED SEQ EVENT: {Message:lj}{NewLine}")
    //.Seq("http://your.seq.host", apiKey = "y0ur4p1k3y==")

    .MinimumLevel.Verbose()
    .ConfigDestructuring()
    .WriteTo.Logger(outputLogger)
    .WriteTo.Logger(consoleLogger)
    .CreateLogger()

// assign this logger as the main static logger
Serilog.Log.Logger <- logger

// test the new logger
logger.Information "I did a log!"

// this listener enables logging of trace events
// skip this if you don't have or care about trace events
use serilogListener =
  // instanciate a new listener
  new SerilogTraceListener.SerilogTraceListener(logger,
    Filter = EventTypeFilter SourceLevels.Verbose)

// remove all Trace listeners and add Serilog's
// ⚠ does not sem to work in .fsx files ⚠
Trace.Listeners.Clear ()
Trace.Listeners.Add serilogListener |> ignore
Trace.verbose "Trace routed to main logger"



(********************************************
 Test creating and logging an HTTP request
 ********************************************)

// execute a dummy HTTP request
let response, elapsedMs, requestPath =
  (new HttpClient())
  |> fun client ->
    let sw = Stopwatch.StartNew()
    let uri = System.Uri("http://google.com")
    client.Send(new HttpRequestMessage(HttpMethod.Get, uri)),
    sw.ElapsedMilliseconds,
    uri.Host

// this creates a log event of level "Debug"
// NOTE: while you can call the logger from anywhere
// using the static Serilog.Log as below, often
// you'll want to pass ILogger instances instead
// since these can be further contextualized with
// additional properties
Serilog.Log
  .ForContext("Request", response.RequestMessage, true)
  .ForContext("Response", response, true)
  .Debug(

    // this is the message template
    "{RequestMethod} {RequestPath} returned status \
     {StatusCode} {Status} in {Elapsed} ms",

    // the values referenced in the message template above
    response.RequestMessage.Method.Method,
    requestPath,
    int response.StatusCode,
    response.StatusCode,
    elapsedMs)

// Result if used with Seq or similar
// ----------------------------------
//
// GET google.com returned status 200 OK in 123 ms
//   └►  {
//         Request: {
//           FullUri: "http://google.com"
//           etc...
//         }
//         Response: {
//           StatusCode: 200
//           Content: "<!doctype html> …"
//           etc...
//         }
//         RequestMethod: "GET"
//         RequestPath: "google.com"
//         StatusCode: 200
//         Status: "OK"
//         Elapsed: 123
//       }