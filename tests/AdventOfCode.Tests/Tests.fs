module Tests

open System
open Xunit

[<Fact>]
let ``Day1`` () =
    let (list1, list2) = AdventOfCode.Day1.getLocations()
    Assert.Equal(1000, list1.Length)
    Assert.Equal(1000, list2.Length)
    // Part 1
    let totalDistance = AdventOfCode.Day1.getTotalDistance list1 list2
    Assert.Equal(1530215, totalDistance)
    // Part 2
    let similarityScore = AdventOfCode.Day1.getSimilarityScore list1 list2
    Assert.Equal(26800609, similarityScore)