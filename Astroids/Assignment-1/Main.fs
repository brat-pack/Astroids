﻿module Main

open Asteroids

[<EntryPoint>]
let main argv =
    use g = new Game1()
    g.Run()
    0