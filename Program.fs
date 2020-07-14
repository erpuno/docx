// MIT License

open System
open System.IO
open System.Text
open TemplateEngine.Docx
open FSharp.Data

let split (ss:#seq<obj>) (s:string) =
    s.Split(ss |> Seq.map string |> Seq.toArray, StringSplitOptions.None)

let read filePath =
    let bin = File.ReadAllText(filePath, Encoding.UTF8)
    let csv = CsvFile.Parse(bin, "," , '\u0022', hasHeaders = true)
        in (csv.Headers,csv.Rows)

let file x =
    match split [|" "|] x with
    | [|""; name; ""|] -> name
    | [|""; name|]     -> name
    |                _ -> ""

let table k (headers,rows) : TableContent =
    match headers with
    | None -> new TableContent(k)
    | Some a ->
        let h = Seq.toList a
        let r = List.fold (fun acc (x:CsvRow) -> (x.Columns |> Array.toList) :: acc) [] (rows |> Seq.toList)
        let row = List.fold (fun acc (k,v) -> new FieldContent(k,v) :> IContentItem :: acc) []
        let t = new TableContent(k)
        let _ = do printfn "Headers: %A\nRows: %A" h r
            in List.fold (fun acc x -> t.AddRow((row (List.zip h x)) |> List.toArray)) t r

let field x =
    match x with
    | [|""; "scalar"; k; v; ""|] -> new FieldContent(k,v) :> IContentItem
    | [|""; "vector"; k; v; ""|] -> table k (read v) :> IContentItem
    | [|""; "zimage"; k; v; ""|] -> new ImageContent(k, File.ReadAllBytes(v)) :> IContentItem
    |                          _ -> new EmptyContent()    :> IContentItem

let ret (_:unit) = 0
let tmp (_:TemplateProcessor) = ()
let varList vars = seq { for i in split [|","|] vars do split [|" "|] i } |> List.ofSeq
let memo list = list |> List.map field
let content memo = memo |> List.toArray |> Content
let debug vars inp out = do
    printfn "Vars: %A" vars
    printfn "Template: %A" inp
    printfn "Output: %A" out

let eval args =
    match split [|"-vars"; "-in"; "-out"|] (args |> String.concat " ") with
    | [|_; vars; inp; out|] -> do
       File.Delete(file out)
       File.Copy(file inp, file out)
       let x = memo (varList vars) in do
       debug x inp out
       use z = new TemplateProcessor(file out)
       z.FillContent(content x).SaveChanges() |> tmp
    | _ -> ()

[<EntryPoint>]
let main args = args |> eval |> ret
