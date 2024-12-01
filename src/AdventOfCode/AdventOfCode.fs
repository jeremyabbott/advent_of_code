namespace AdventOfCode
open System

module Helpers =
    
    let readLines path =
        System.IO.File.ReadAllLines(path)

module Day1 =
    let getLocations () =
        let locations =
            Helpers.readLines "/Users/jeremyabbott/code/advent_of_code/src/AdventOfCode/2024/Day01_Part1.txt"
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
        
        
