module Tests

open System
open Xunit

[<Fact>]
let ``Day1`` () =
    let (list1, list2) = AdventOfCode.Day1.getLocations "2024/Day01_Part1.txt"
    Assert.Equal(1000, list1.Length)
    Assert.Equal(1000, list2.Length)
    // Part 1
    let totalDistance = AdventOfCode.Day1.getTotalDistance list1 list2
    Assert.Equal(1530215, totalDistance)
    // Part 2
    let similarityScore = AdventOfCode.Day1.getSimilarityScore list1 list2
    Assert.Equal(26800609, similarityScore)
    
[<Fact>]
let ``Day2 Part 1`` () =
    let safeReports = AdventOfCode.Day2.analyzeReports "2024/Day02_Part1.txt"
    Assert.Equal(670, safeReports)
    
[<Fact>]
let ``Day2 Part 2`` () =
    let safeReports = AdventOfCode.Day2.analyzeReportsV2 "2024/Day02_Part1.txt"
    Assert.Equal(700, safeReports)

[<Fact>]
let shouldBeSafe () =
    let safe = AdventOfCode.Day2.isSafeV2 [8;6;4;4;1]
    Assert.True(safe)
    
[<Fact>]
let shouldNotBeSafe () =
    let safe = AdventOfCode.Day2.isSafeV2 [9;7;6;2;1]
    Assert.False(safe)

[<Fact>]    
let ``Day3 Part 1`` () =
    let input = AdventOfCode.Helpers.readText "2024/Day03.txt"
    let part1 = AdventOfCode.Day3.solvePart1 input
    Assert.Equal(178538786, part1)
 
[<Fact>]    
let ``Day3 Part 2`` () =
    let input = AdventOfCode.Helpers.readText "2024/Day03.txt"
    let part2 = AdventOfCode.Day3.solvePart2 input
    Assert.Equal(102467299, part2)
    
[<Fact>]
let ``Does Regex Blend`` () =
    let input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"
    let matches = AdventOfCode.Day3.findMulExpressions input
    Assert.Equal(4, matches |> Seq.length)
    
[<Fact>]
let ``Does Multiplier Regex Blend`` () =
    let input = "mul(2,4)"
    let left, right = AdventOfCode.Day3.extractMulValues input
    Assert.Equal(2, left)
    Assert.Equal(4, right)

[<Fact>]
let ``Day 3 Part 2 Scratch`` () =
    let input = "mul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"
    let conditionalMatcher = System.Text.RegularExpressions.Regex("do\(\)|don't\(\)|mul\(\d+,\d+\)")
    let matches =
        conditionalMatcher.Matches(input)
        |> Seq.map(_.Value)
        |> List.ofSeq
    
    let solvePart2 parts =
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
        solver initialState parts
    let _, input = solvePart2 matches
    Assert.Equal(48, AdventOfCode.Day3.solvePart1 input)