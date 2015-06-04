open System

// *************************************************************
// Function declarations
// *************************************************************

// get a list of the given size containing random integers.
let getRandomArray size =
    let rnd = Random()
    Array.init size (fun _ -> rnd.Next ())

// helper function that prints each list element to a new line.
let printNums list = 
    printfn "%c" '{'
    list |> Seq.iter (printfn ("%14i,"))
    printfn "%c" '}'

let printStrings list = 
    printfn "%c" '{'
    list |> Seq.iter (printfn ("%14s,"))
    printfn "%c" '}'

// The tree is built of Node arrays.
type Node (next: Node[]) =
    member val Count = 0 with get, set
    member val Next = next with get, set

  
let getNodeSet = [| Node(null) ; Node(null) |]

//let binary number = System.Convert.To(number, 2) . PadLeft(32, '0')
 

// *************************************************************
// put a value into the given Node array.
// *************************************************************
let rec put (current : Node[]) position value = 

   // 1. get the current bit (0 or 1)
    let bit = (value >>> position) &&& 1
    //printf "%i" bit
    //printfn "Position: %2i value: %10i bit: %i" position value bit

    // 2. increment the appropriate count.
    let node:Node = current.[bit]
    node.Count <- node.Count + 1

    // 3. set Next if null.
    node.Next <- 
        (* match node.Next with null -> getNodeSet | _ -> node.Next *)

        (*
        match node.Next with
        |null -> [|Node(null) ; Node(null)|]
        | _ -> node.Next

        *)
        if node.Next <> null 
        then node.Next
        else [| Node(null) ; Node(null) |]


    // 4. If position is not zero, recurse with next position.
    if position <> 0 
    then put (node.Next) (position - 1) value
    //else printfn ""

// *************************************************************
// Get a value at the position starting with the given current Node.
// *************************************************************
let rec get (current : Node[]) sortedPosition (bits:int[]) bitPosition = 

    //printf  "current.[0].Count: %i " (current.[0].Count)
    //printfn "current.[1].Count: %i " (current.[1].Count)

    let zeroNode:Node = current.[0]
    let goZero = sortedPosition <= zeroNode.Count

    // calculate the current bit value.
    let bit = 
        if goZero
        then 0
        else 1

    //printf "%A" bit
    // set bitValue.
    //printf "before: %i" bits.[bitPosition]
    bits.[bitPosition] <- bit
    //printfn " after:  %i" bits.[bitPosition]


    // set sorted position if it needs to decrease.
    let newSortPosition =  
        if goZero
        then sortedPosition 
        else sortedPosition - zeroNode.Count
   

    //base case: finished!
    if(bitPosition = 0) 
    then System.Convert.ToInt32( value = (String.Concat (Array.rev bits)), fromBase = 2 )
    else get (current.[bit].Next) newSortPosition bits (bitPosition - 1)
    //printfn "get %i" newSortPosition


 

// *************************************************************
// The Sorting Algorithm
// *************************************************************
let sequentialRadixTreeSort values = 
    Console.WriteLine "SequentialRadixTreeSort()"
    printfn "\nUnsorted:"
    printNums values
    printfn ""

    // get the root.
    let root = getNodeSet

    // get some local helper values.
    let bitCount = 8 * sizeof<int>
    let initialPosition = bitCount - 1

    // put values in the tree.
    // printfn "put:"
    values |> Seq.iter (put root initialPosition)

    // printfn "get:"
    // retrieve them out of the tree in sorted order.
    // values |> Seq.iter (get root initialPosition)
    let result = Array.init (Array.length values) (fun p -> get root (p + 1) (Array.zeroCreate bitCount) initialPosition)

    printfn "\nSorted:"
    //printStrings result
    printNums result
   
    result

// *************************************************************
// run RadixTreeSort on a random list of numbers.
// *************************************************************
[<EntryPoint>]
let main args =
    printfn "-------------------------------------------------------------------------------"
    printfn "F#"
    printfn "-------------------------------------------------------------------------------"
    printfn ""
    let values = getRandomArray 50

    // start stopwatch and run test.
    let stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let sortedValues = sequentialRadixTreeSort values
    stopWatch.Stop()

    printfn "Elapsed time: %f seconds" (float (stopWatch.Elapsed.Milliseconds) / 1000.0);

    Console.ReadLine() |> ignore
    0