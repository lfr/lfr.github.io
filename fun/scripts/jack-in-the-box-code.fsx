open System.Text.RegularExpressions
open System
open Microsoft.FSharp.Core

// non empty string
type NonEmptyString = private NonEmptyString of string
  with static member New = function
  | s when System.String.IsNullOrEmpty s -> None
  | s -> Some (NonEmptyString s)

let emailPattern =
    "^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" |> Regex

let consumeValidEmail email =
    if emailPattern.IsMatch email then
        printfn "Valid email: %A" email
    else failwith "Invalid email!"

    
// no validation
// type Email = obj
consumeValidEmail 1              // compiles but breaks
consumeValidEmail "don@syme\n.g" // combiles but breaks
consumeValidEmail "abc@def.g"    // compiles and works

// basic type validation
// type Email = string
consumeValidEmail 1              // does not compile
consumeValidEmail "don@syme\n.g" // compiles but breaks
consumeValidEmail "abc@def.g"    // compiles and works

// safely create an email using basic type validation
let email : string -> Result<Email, string> = function
| s when System.String.IsNullOrWhiteSpace s -> Error "input is empty"
| s when Regex.IsMatch(s, "[\w-+_.@]") -> Error "input contains control characters"
| s when Regex.IsMatch(s, "^[^@]+@\w+.\w+$") -> Ok s
| _ -> Error "invalid email"

// safely consume an email using basic type validation
match email "don@syme\n.fs" with
| Ok email -> consumeValidEmail email
| error -> printf "%A" error

// OUTPUT> Error consuming email: input contains control characters


// embedding validation in the type itself (object oriented style)
type Email private (s:string) = class end with
    static member Validate = function
    | s when String.IsNullOrWhiteSpace s -> Error "input is empty"
    | s when Regex.IsMatch(s, "[^\w-+_.@]") -> Error "input contains control characters"
    | s when Regex.IsMatch(s, "^[^@]+@\w+.\w+$") -> Ok (Email(s))
    | _ -> Error "invalid email"
    member x.Value = s

// safely create and consume an email using embedded validation (oo style)
let result = Email.Validate "do\n@syme.fs"
match result with
| Ok x -> consumeValidEmail x.Value
| e -> printfn "%A" e

// OUTPUT> Error "input contains control characters"