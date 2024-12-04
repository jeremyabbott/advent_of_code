namespace AdventOfCode
open System

module Helpers =
    
    let readLines path =
        System.IO.File.ReadAllLines(path)
        
    let readText path = 
        System.IO.File.ReadAllText(path)

module Day1 =
    let getLocations input =
        let locations =
            Helpers.readLines input
            |> Array.map (fun l ->
                let locationIds = l.Split(' ') |> Array.filter (fun s -> String.IsNullOrEmpty(s) |> not)
                let first = locationIds.[0].Trim() |> int
                let second = locationIds.[1].Trim() |> int
                (first, second))
            |> Array.unzip
            |> (fun (a, b) -> (a |> Array.sort, b |> Array.sort))
        locations
        
    let getTotalDistance list1 list2 =
        Array.zip list1 list2
        |> Array.map (fun (a:int, b:int) -> Math.Abs(a - b))
        |> Array.sum
        
    let getSimilarityScore (list1: int array) (list2: int array) =
        list1
        |> Array.map(fun l ->
            list2
            |> Array.where (fun l2 -> l2 = l)
            |> Array.length
            |> (fun c -> l * c))
        |> Array.sum
        
module Day2 =
    let isInRange i = i >= 1 && i <= 3 || i <= -1 && i >= -3
    let isNegative i = i < 0
    let isPositive i = i > 0
    let isSafe (report: int list) =
        let deltas =
            report
            |> List.pairwise
            |> List.map(fun (a,b) -> a-b)
        let allInRange = deltas |> List.forall isInRange
        let allIncreasing = deltas |> List.forall isPositive
        let allDecreasing = deltas |> List.forall isNegative
        allInRange && (allIncreasing || allDecreasing)
        
    let analyzeReports input =
        let reports =
            Helpers.readLines input
            |> Array.map(fun l -> l.Split(' ') |> Array.map int |> List.ofArray)
        
        reports |> Array.filter isSafe |> Array.length
        
    let isSafeV2 (report: int list) =
        let safe = isSafe report
        let safeWithBuffer =
            report
            |> List.mapi (fun i _ ->
                let remaining = List.removeAt i report
                isSafe remaining)
            |> List.exists id
        safeWithBuffer || safe
        
    let analyzeReportsV2 input =
        let reports =
            Helpers.readLines input
            |> Array.map(fun l -> l.Split(' ') |> Array.map int |> List.ofArray)
        reports |> Array.filter isSafeV2 |> Array.length
        
module Day3 =
    open System.Text.RegularExpressions
    
    let mulExpressionRegex = Regex("mul\(\d+,\d+\)")
    let mulNumberRegex = Regex("\d+")
    let conditionalMatcher = Regex("do\(\)|don't\(\)|mul\(\d+,\d+\)")
    
    let findMulExpressions input =
        let matches = mulExpressionRegex.Matches(input)
        matches |> Seq.map(_.Value)
        
    let extractMulValues input =
        let matches = mulNumberRegex.Matches(input)
        matches
        |> Seq.map(_.Value)
        |> Seq.map(int)
        |> Array.ofSeq
        |> (fun items -> (items.[0], items.[1]))
        
    let solvePart1 input =
        findMulExpressions input
        |> Seq.map extractMulValues
        |> Seq.map(fun (a, b) -> a * b)
        |> Seq.sum
        
    let solvePart2 input =
        let matches =
            conditionalMatcher.Matches(input)
            |> Seq.map(_.Value)
            |> List.ofSeq
        let initialState = (true, "")
        let rec solver state parts =
            let isSafe, buffer = state
            match parts with
            | [] -> state
            | head :: tail ->
                match head with
                | "do()" -> solver (true, buffer) tail
                | "don't()" -> solver (false, buffer) tail
                | _ ->
                    let newBuffer = if isSafe then buffer + head else buffer
                    let newState = (isSafe, newBuffer)
                    solver newState tail
        let _, result = solver initialState matches
        solvePart1 result
        