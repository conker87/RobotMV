Hi there!

Here's a bit of tips on putting the scene together for Clearwater Cliffs.
This scene uses a lot of blending tricks, so I'm here to explain all that!

Layers (back to front):
1. sky
2. clouds
3. abyss (black tiles)
	- Place solid black tiles under the water tiles to darken the water area.
4. far cliffs
5. far cliffs fade
	- Make a duplicate of the sky layer at 50% opacity, but it should only go as high as the tallest cliff.
	  This produces a fade effect over the cliffs.
6. mid cliffs
7. mid cliffs fade
	- Same as #5, but 25% opacity.
8. near cliffs
9. cliff bottom darkener
	- You can use tile 88 to darken the bottom of the background cliffs as they go into the water.
10. water
	- The water in this scene uses a hard light blend mode, which you can do in your game using a shader.
	  That's kind of beyond the scope of this readme, so I'll leave that as an exercise for the reader.
	  *Note: If you're looking at the Pyxel Edit file, there's a bug where it doesn't save the blend mode.
		 You can change it using the gear button next to the layer.
11. cliffs (foreground)
12. grass
	- Just place the grass tiles on top of whichever cliffs you want. :D
13. water (foreground)
	- This uses tiles 74-78, as opposed to the other water. Set opacity to 50%.

Hope that helps make sense of putting together the scene! I've also included some pre-assembled cliffs and clouds in a png in this zip file.
-Will