---
published: true
layout: post
title: Take F# to the stars
summary: >-
  Your airtight domain will keep you safe
image: /assets/2020/take-it-to-the-stars.png
---

![splash](/assets/2020/take-it-to-the-stars.png)

<div class="message">
  <i>
    Since this article took off like the rocket above I'll be posting a commemorative article to celebrate an upcoming milestone. It'll be very different from my previous articles but you can look for it <a href="http://twitter.com/intent/user?screen_name=LuisLikeIewis" target="_blank">in the usual place</a>. Enjoy the read!
  </i>
</div>

Some developers carefully design domains that perfectly reflect current business constraints, while others prefer to hammer away code that just works, letting whatever domain arise from their uninterrupted but hopefully functional stream of consciousness. Regardless of your style, you'll need an airtight domain if you're going to take your F# starship to the stars.

If you've followed this series you may be expecting this article to target the first type of developer above, but read on and you'll soon notice that we get to coding fast without much analysis, only delving into deeper considerations after a decent warm-up, and even then never straying too far off the beaten path.

No whiteboards, no conceptual models, this article is for devs who just want to code.

## The right tool for the job

[FSharp.ValidationBlocks](https://github.com/lfr/FSharp.ValidationBlocks) is a library maintained by yours truly that dramatically reduces the amount of code necessary to create safe domains, the importance of which I've already extensively covered in the previous posts. In this one we'll use validation blocks to create the ubiquitous user profile object.

Additionally, I'll be using two other libraries: [Humanizer](https://github.com/Humanizr/Humanizer) to de-pascalize strings, and [FsToolkit.ErrorHandling](https://github.com/demystifyfp/FsToolkit.ErrorHandling) to handle results with computation expressions which are easier to read. Both libraries are completely optional and only used to simplify the code, so that we can focus on domain-specific concepts.

```fsharp
// not much to see here
open System
open System.Text.RegularExpressions
open FSharp.ValidationBlocks
open FsToolkit.ErrorHandling.ResultCE
open Humanizer
```

Finally, I'll also create a `todo` type, this is very useful to create types that *simply exist* without figuring out what they should be or do. Using the `Obsolete` attribute ensures that all my yet-to-be-defined types are properly tracked as warnings:

```fsharp
[<Obsolete("This type is not implemented yet.")>]
type todo = unit
```

## Let's get started

Imagine you need a user profile entity capable of storing a username along with the user's full name, email, twitter handle, phone number and a multi-line bio.

Many domains need a similar object, which is why I've chosen to use it for this example, but you can apply these ideas to any object that you need to create. This particular one could easily be defined like this:

```fsharp
// this user profile is unsafe
// don't take it to space!
type UserProfile =
    {
        Username: string
        FirstName: string
        LastName: string
        Bio: string
        PhoneNumber: string
        Email: string
        TwitterHandle: string
    }
```

This could do the job, but if you've been following the series, defining all of these fields as `string` is [far from ideal](https://functionalfunsies.com/fun/2020/03/04/these-arent-the-types/). For starters only the `Bio` field accepts line returns — a rule that isn't anywhere in this definition — so we're going to start over with the goal of creating a domain that both states **and** enforces any of such constraints.

## Say no to analysis paralysis

Declaring everything as string is easy but wrong, on the other hand defining a full domain stating all possible constraints seems like a painstakingly sluggish endeavor. My advice is to just start coding your heart out and build your domain structure first and constraints later.

In the user profile example, instead of trying to figure out exactly what each member's definitive type will be, we'll just create a whole bunch of placeholder types that we'll refine later:

```fsharp
// don't think too hard about these
type Username = todo
type FirstName = todo
type LastName = todo
type Bio = todo
type PhoneNumber = todo
type Email = todo
type TwitterHandle = todo
```

That was easy. At this point we don't know whether we'll need so many types but that's fine, this is enough for us to move on and define the structure:

```fsharp
// don't overthink this one
type UserProfile =
    {
        Username: Username
        FirstName: FirstName
        LastName: LastName
        Bio: Bio
        PhoneNumber: PhoneNumber
        Email: Email
        TwitterHandle: TwitterHandle
    }
```

Just like before, we're not aiming for a 100% of the final definition here.

#### Find your inner stone carver

You may have noticed a pattern: contrary to popular belief, designing with types doesn't have to be a slow meticulous effort from beginning to end. It's more like stone carving - we start with the rough outline of the shape of our object and subsequently carve out the details.

![Designing with types is like stone carving](/assets/2020/stone-carving.png)

It's true that the refining part takes more time, but having the skeleton of the final object in place allows us to work on small chunks of well defined scope by the time we get to the more analysis-intensive part.

## Final preparations

Being [ROP](https://fsharpforfunandprofit.com/rop/), validation blocks make use of the `Result` union, which expects an error type. Since we haven't created any validation yet, we don't know any of our errors, but we already know we don't want empty strings anywhere so creating an error for these is usually a good place to start:

```fsharp
// don't strain a neuron over this one either
type ValidationError =
  | IsMissingOrBlank
```

In addition to our error type we'll create an interface abbreviation that's optional but makes block declarations more tidy:

```fsharp
// it's an interface that doesn't start with I 🤯
type TextBlock = IBlock<string, ValidationError>
```

With the basic shape of our user profile, an error type, and this interface, we're now ready to start playing with validation blocks. They're just like Lego, but less painful when you step on one.

## Let the fun begin

Blocks are built on top of blocks, so we we should always start with more general purpose ones. **We don't want any blocks to contain empty strings**, if a field is optional we'll explicitly use F#'s `option` type.

Since this rule is universal, it's a great candidate for our most basic block, the block that accepts anything — as long as it's *something*. We'll call it `FreeText`:

```fsharp
type FreeText = private FreeText of string with
  interface TextBlock with
    member _.Validate =
      fun s -> [
     	if String.IsNullOrWhiteSpace s then
      	  IsMissingOrBlank]
```

#### The 3 easy steps of block definition

Declaring a block is straightforward but let's break it down. The first line gives it a name, makes its constructor private — blocks are **never** meant to be created directly — and finally defines the underlying type:

```fsharp
1. type FreeText = private FreeText of string (* with *)
```

Since this is our first block, its underlying type is the primitive type `string`. All our additional blocks will be built on other blocks as opposed to `string`. This will be our only primordial block so to speak.

At the moment this type is just a regular F# single-case union, so next we specify that it's a validation block of `string` by using the interface defined above:

```fsharp
2. interface TextBlock (* with *)
```

Finally the interface forces us to implement the `Validate` member, which is simply a function of the primitive type `string` that returns a list of errors under certain conditions:

```fsharp
3. member _.Validate =
      fun s -> [
        if String.IsNullOrWhiteSpace s then
          IsMissingOrBlank]
```

As you can see, creating blocks couldn't be easier which is exactly the point of this library. As you'll see later, using these blocks is just as straightforward, but for now let's get back to our code.

#### Back to the code

The `FreeText` block validation matches our `Bio` field perfectly, so we'll just use it directly:

```fsharp
type UserProfile =
  {
    Username: Username
    FirstName: FirstName
    LastName: LastName
    Bio: FreeText // ← updated
    PhoneNumber: PhoneNumber
    Email: Email
    TwitterHandle: TwitterHandle
  }
```

Except for `Bio`, all our other fields do not accept tabs or return characters, so next we create a block for a single line of text. We could call it `SingleLineText` or similar, but this block is by far the most widely used base block, so I personally prefer to just call it `Text`. 

Note that all our blocks reject empty strings, but instead of adding this extra validation to the `Text` block, I'll simply state that it's a block `of FreeText`, which adds `FreeText`' s validation without any additional code:

```fsharp
type Text = private Text of FreeText with
  interface TextBlock with
    member _.Validate =
      fun s -> [
        if Regex.IsMatch(s, "[\t\n]") then
          ContainsControlCharacters]
```

You may have noticed that we introduced a new error type, so the error union needs to be modified accordingly:

```fsharp
// easy peasy
type ValidationError =
  | IsMissingOrBlank
  | ContainsControlCharacters // ← new
```

While extremely useful, the `Text` block's validation by itself doesn't match any of our remaining fields.

Creating specific types for each field as we did during the warm-up phase should always be the last resort, so before doing that we'll check to see if there's any remaining commonality:

```fsharp
type UserProfile =
  {
    ...
    // these two are suspiciously similar
    FirstName: FirstName
    LastName: LastName
    ...
  }
```

First name and last name probably can share the same validation, so we'll just create a name block that rejects weird characters:

```fsharp
type Name = private Name of Text with
  interface TextBlock with
    member _.Validate =
      fun s -> [
        if Regex.IsMatch(s, "\W|[0-9_]") then
          IsInvalid "letters or dashes"]
```

`IsInvalid` is a new type of error, and it takes a description of what would be valid so that a potential error message can help the user fix the issue. We'll add it to our error union:

```fsharp
// thee's company
type ValidationError =
  | IsMissingOrBlank
  | ContainsControlCharacters
  | IsInvalid of descriptionOfValidContent:string
```

And fix our `UserProfile` record:

```fsharp
// identical lines are commented
type UserProfile =
  {
    Username: Username
    FirstName: Name // ← updated
    LastName: Name  // ← updated
    Bio: FreeText   // ← updated earlier
    PhoneNumber: PhoneNumber
    Email: Email
    TwitterHandle: TwitterHandle
  }
```

As you can see we're steadily making progress refining our `UserProfile` record by taking very straightforward steps. In fact, we've already done the part that needs the most analysis, which is finding commonality.

## You get the gist of it

At this point we've covered all the common validation blocks, the remaining blocks all have their unique validation rules so we'll create a block for each. Since none of them accept control characters, they'll all use `Text` as the base block.

```fsharp
type Username = private Username of Text with
  interface TextBlock with
    member _.Validate =
      fun s -> [
        if not<|Regex.IsMatch(s, "^[a-z]+[a-z0-9_]+[a-z0-9]+$") then
          IsInvalid "alphanumeric characters"]

type PhoneNumber = private PhoneNumber of Text with
  interface TextBlock with
    member _.Validate =
      fun s -> [
        if Regex.IsMatch(s, "^[2-9]\d{3}-\d{3}-\d{4}$") then
          IsInvalid "555-555-5555"]

type Email = private Email of Text with
  interface TextBlock with
    member _.Validate =
      fun s -> [
        if not<|Regex.IsMatch(s, "^[\w.]+@\w+\.[a-z0-9]+$") then
          IsNotAValidEmail]

type TwitterHandle = private TwitterHandle of Text with
  interface TextBlock with
    member _.Validate =
      let max = 16
      fun s -> [
        if s.Length > max then ExceedsMaximumLength max
        if not<|Regex.IsMatch(s, "^@\w+$") then
          IsNotAValidTwitterHandle
        if s.Contains("admin") || s.Contains("twitter") then
          ForbiddenTwitterHandleContent
      ]
```

Then add the remaining missing errors:

```fsharp
// seven's a crowd
type ValidationError =
  | IsMissingOrBlank
  | ContainsControlCharacters
  | IsInvalid of descriptionOfValidContent:string
  | IsNotAValidEmail
  | IsNotAValidTwitterHandle
  | ForbiddenTwitterHandleContent
  | ExceedsMaximumLength of int
```

As for the `UserProfile`, turns out that the majority of our initial code can remain unchanged!

```fsharp
type UserProfile =
  {
    Username: Username           // ← unchanged
    FirstName: Name
    LastName: Name
    Bio: FreeText
    PhoneNumber: PhoneNumber     // ← unchanged
    Email: Email                 // ← unchanged
    TwitterHandle: TwitterHandle // ← unchanged
  }
```

## Crash course: using blocks

Since blocks have private constructors, the main way to create new blocks is using the `Block.validate` function, however **this will not compile**:

```fsharp
// assuming there's an existing profile of type UserProfile
let newProfile =
  { profile with Email = Block.validate "usr@acme.com" } // ERROR!!
```

Because `Block.validate` returns a result.  Instead, you can for instance map the result of a successful validation:

```fsharp
// assuming there's an existing profile of type UserProfile
let newProfile =
  Block.validate "usr@acme.com"
  |> Result.map (fun email -> { profile with Email = email })
```

You can also skip validation and just raise exceptions if there's errors, not necessarily best practice but handy in special cases like de-serializing pre-validated content:

```fsharp
// assuming there's an existing profile of type UserProfile
let newProfile =
  { profile with Email = Unchecked.blockof "usr@acme.com" } // OK
```

That's it for creating blocks. To access their value, simply use the `Block.value` function that unsurprisingly returns a `string` for any of our `TextBlock` types:

```fsharp
Block.value profile.Email // returns the email string
```



### I don't have a title for this section but it's the best one

Validation blocks truly shine when they're actively keeping invalid data from your domain, so in order to see it in action, we'll attempt to create one with all sorts of errors.

Since attempting to create blocks with invalid content generates meaningful errors, in a user interface you can imagine displaying a prettified version of the error next to a text box.

In this article however we don't have that luxury, so in order to see the outcome of attempting to create a user profile with both invalid and then valid content we'll create a function called `check`that takes the name of the thing being checked and returns exactly the same result as the `Block.validate` introduced above, but also prints out any errors that may occur during validation. Since `check` is the same as `Block.validate` I'll skip the details for now.

We'll also create a function `print` that dumps a result if `Ok`:

```fsharp
// Result<'a, 'b> → unit
let print =
  function Ok x -> printfn "%A" x | _ -> printfn "Validation failed!"
```

We're finally ready to try creating a block that definitely should fail, which we'll do using a `result` computation expression:

```fsharp
result {

  let! username = check "Username" "_-_-_"
  and! firstName = check "First name" "X Æ"
  and! lastName = check "Last name" "A-12"
  and! bio = check "Bio" String.Empty
  and! phoneNumber = check "Phone number" "123456789"
  and! email = check "Email" "person@company"
  and! twitterHandle = check "Twitter handle" "@12345678901234567"

  return
    {
      Username = username
      FirstName = firstName
      LastName = lastName
      Bio = bio
      PhoneNumber = phoneNumber
      Email = email
      TwitterHandle = twitterHandle
    }
}
|> print
```

Here's the output of the console:

```
Username is invalid, expected alphanumeric characters.
First name is invalid, expected letters or dashes.
Last name is invalid, expected letters or dashes.
Bio is missing or blank.
Email is not a valid email.
Twitter handle exceeds maximum length of 16
Validation failed!
```

How amazing is this with so little code?

Our next attempt uses `Unchecked.blockof` for brevity, it's ok since this example has been handcrafted to succeed validation, but you should avoid it in your code except in very specific circumstances:

```fsharp
let profile =
  {
    Username = Unchecked.blockof "mal"
    FirstName = Unchecked.blockof "Malcolm"
    LastName = Unchecked.blockof "Reynolds"
    Bio = Unchecked.blockof "I aim to misbehave."
    PhoneNumber = Unchecked.blockof "555-555-1234"
    Email = Unchecked.blockof "malcom.reynolds@serenity.com"
    TwitterHandle = Unchecked.blockof "@mal"
  }

Block.value profile.Username |> printfn "%s's profile:"
Ok profile |> print
```

And here's the output:

```
mal's profile:
{
  Username = Username (Text (FreeText "mal"))
  FirstName = Name (Text (FreeText "Malcolm"))
  LastName = Name (Text (FreeText "Reynolds"))
  Bio = FreeText "I aim to misbehave."
  PhoneNumber = PhoneNumber (Text (FreeText "555-555-1234"))
  Email = Email (Text (FreeText "malcom.reynolds@serenity.com"))
  TwitterHandle = TwitterHandle (Text (FreeText "@mal"))
}
```

So this works as expected, but you may be thinking that `Username (Text( FreeText "mal"))` is a bit unwieldy. It's understandable, but think of it as a protective shell that guarantees that its content ("mal" here) is 100% valid. It's the thin layer that keeps your F# spaceship structurally sound.

More importantly, you can use the provided `System.Text.Json` converter to ensure that any serialized `UserProfile` is clean of any validation block nonsense, here's our serialized block:

```json
{
  "Username": "mal",
  "FirstName": "Malcom",
  "LastName": "Reynolds",
  "Bio": "I aim to misbehave.",
  "PhoneNumber": "555-555-1234",
  "Email": "malcom.reynolds@serenity.com",
  "TwitterHandle": "@mal"
}
```

As you can see validation blocks are nowhere to be seen, which is exactly what we want! Using the same converter, this JSON will properly de-serialize back to our `UserProfile` object complete with the appropriate validation block values!

### Tying up loose ends

I've omitted some code above for clarity, you can find it here.

#### Serialization

This is the code that serializes the block created above, note the line that adds the provided converted to the serializer options, it's absolutely necessary otherwise your serialized content will contain all of the validation block details.

```fsharp
// testing serialization
let serializedProfile =
  let options =
    System.Text.Json.JsonSerializerOptions(WriteIndented = true)
    
  // THIS IS IMPORTANT
  Serialization.ValidationBlockJsonConverterFactory()
  |> options.Converters.Add

  // return a serialized version of the profile created above
  System.Text.Json.JsonSerializer.Serialize(profile, options)

Ok serializedProfile |> print
```

#### The check function

I didn't put the `check` function above because it returns exactly the same result as `Block.validate`. It's also a bit ugly and it's unlikely you'll be using anything similar in the real world, where you'd generally display each error next to the relevant text box for instance, or add them to  the payload of an API response.

```fsharp
/// Validates a block while printing any errors that may occur
/// x is the name of the 'thing' being validated, i.e. email

// string → ('a → Result<'b, ValidationError list>)
let check (x:string) =
  // call Block.validate which will validate a string and return a result
  Block.validate
  // map the output of Block.validate if error
  >> Result.mapError
   	// this mapping returns the same errors with a side effect
    (fun errors ->
       errors
       // print individual errors differently according to their type
       |> List.iter
         (function
           | IsInvalid descOfValid ->
             printfn "%s is invalid, expected %s." x descOfValid
           | ExceedsMaximumLength length ->
             printfn "%s exceeds maximum length of %i" x length
           | e ->
             (x, e.ToString().Humanize(LetterCasing.LowerCase))
             ||> printfn "%s %s.")
       // return errors as-is
       errors)
```

## Reach for the stars

Congratulations if you've made it this far. If you did, I'm confident that you'll be considering giving [FSharp.ValidationBlocks](https://github.com/lfr/FSharp.ValidationBlocks) a try, and I'm looking forward to your feedback.

Having an airtight domain brings a refreshing amount of extra confidence in the stability of increasingly complex systems. Whether you're flying solo or as a team, the sky is no longer the limit! 🚀

## Feedback & more

If you enjoyed this article or have any comments please consider [following me](https://twitter.com/intent/user?screen_name=LuisLikeIewis), retweeting or replying to [this article's tweet](https://twitter.com/luislikeIewis), it's very appreciated.
