﻿module Asteroids

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open State
open System
open Helpers

type Game1() as this =
    inherit Game()

    do this.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>
    let mutable gameState = GameState.Zero()

    override this.Initialize() =
        graphics.PreferredBackBufferWidth <- 1920
        graphics.PreferredBackBufferHeight <- 1080

        graphics.IsFullScreen <- false
        graphics.ApplyChanges()
        this.IsMouseVisible <- false

        do base.Initialize()

    override this.LoadContent() = 
        System.IO.Directory.SetCurrentDirectory("Content")
        do spriteBatch <- new SpriteBatch(this.GraphicsDevice)
        let textures = 
            Map.empty.
                Add("Ship", spriteLoader ("Ship.png") graphics.GraphicsDevice) 
        gameState <- { gameState with textures = textures}
                
    override this.Update (gameTime) =
        gameState <- gameState.Update(gameTime)

    override this.Draw(gameTime) =
        do this.GraphicsDevice.Clear Color.CornflowerBlue
        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend)
        gameState.Draw(spriteBatch)
        spriteBatch.End()
    
