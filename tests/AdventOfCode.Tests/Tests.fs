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