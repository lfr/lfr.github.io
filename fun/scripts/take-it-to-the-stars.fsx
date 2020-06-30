
open FSharp.ValidationBlocks
open System.Text.RegularExpressions
open System
open FsToolkit.ErrorHandling.ResultCE
open Humanizer

[<System.Obsolete("This type is not implemented yet.", false)>]
type todo = unit

// defining the user profile type like a newb
type UserProfile' =
    {
        Username: string
        FirstName: string
        LastName: string
        Bio: string
        PhoneNumber: string
        Email: string
        TwitterHandle: string
    }

// don't think too hard about these
type Username' = todo
type FirstName' = todo
type LastName' = todo
type Bio' = todo
type PhoneNumber' = todo
type Email' = todo
type TwitterHandle' = todo

// defining the user profile type like a pro
type UserProfile'' =
    {
        Username: Username'
        FirstName: FirstName'
        LastName: LastName'
        Bio: Bio'
        PhoneNumber: PhoneNumber'
        Email: Email'
        TwitterHandle: TwitterHandle'
    }

// define errors (unknown at the beginning)
type ValidationError =
    | IsMissingOrBlank
    | ContainsControlCharacters
    | IsInvalid of descriptionOfValidContent:string
    | IsNotAValidEmail
    | IsNotAValidTwitterHandle
    | ForbiddenTwitterHandleContent
    | ExceedsMaximumLength of int

// define a friendly interface name to simplify block declaration
type TextBlock = IBlock<string, ValidationError>

type FreeText = private FreeText of string with
    interface TextBlock with
        member _.Validate =
            fun s -> [if String.IsNullOrWhiteSpace s then IsMissingOrBlank]

type Text = private Text of FreeText with
    interface TextBlock with
        member _.Validate =
            fun s -> [if Regex.IsMatch(s, "[\t\n]") then ContainsControlCharacters]

type Username = private Username of Text with
    interface TextBlock with
        member _.Validate =
            fun s -> [
                if not<|Regex.IsMatch(s, "^[a-z]+[a-z0-9_]+[a-z0-9]+$") then
                    IsInvalid "alphanumeric characters"]

type Name = private Name of Text with
    interface TextBlock with
        member _.Validate =
            fun s -> [
                if Regex.IsMatch(s, "\W|[0-9_]") then
                    IsInvalid "letters or dashes"]

type PhoneNumber = private PhoneNumber of Text with
    interface TextBlock with
        member _.Validate =
            fun s -> [
                if Regex.IsMatch(s, "^[2-9]\d{3}-\d{3}-\d{4}$") then
                    IsInvalid "(555) 555-5555"]

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

type UserProfile''' =
    {
        Username: Username
        FirstName: Name
        LastName: Name
        Bio: FreeText
        PhoneNumber: PhoneNumber
        Email: Email
        TwitterHandle: TwitterHandle
    }

let check x =
    Block.validate
    >> Result.mapError
        (fun errors ->
            errors
            |> List.iter
                (function
                | IsInvalid descOfValid ->
                    printfn "%s is invalid, expected %s." x descOfValid
                | ExceedsMaximumLength length ->
                    printfn "%s exceeds maximum length of %i" x length
                | e -> (x, e.ToString().Humanize(LetterCasing.LowerCase)) ||> printfn "%s %s.")
            errors)

let print =
    function Ok x -> printfn "%A" x | _ -> printfn "Validation failed!"
    
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

// create valid profile
let profile =
    {
        Username = Unchecked.blockof "mal"
        FirstName = Unchecked.blockof "Malcom"
        LastName = Unchecked.blockof "Reynolds"
        Bio = Unchecked.blockof "I aim to misbehave."
        PhoneNumber = Unchecked.blockof "555-555-1234"
        Email = Unchecked.blockof "malcom.reynolds@serenity.com"
        TwitterHandle = Unchecked.blockof "@mal"
    }

Ok profile |> print


// access a specific field
Block.value profile.Bio |> Ok |> print


// display serialized content
let serializedProfile =
    let options =
        System.Text.Json.JsonSerializerOptions(WriteIndented = true)
    
    // THIS IS IMPORTANT
    Serialization.ValidationBlockJsonConverterFactory()
    |> options.Converters.Add

    System.Text.Json.JsonSerializer.Serialize(profile, options)

Ok serializedProfile |> print