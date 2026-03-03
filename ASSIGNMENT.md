Create a turn based territory expansion game, that can save and load the game state from a .save.txt file. It must allow two players to play the game on different board sizes. The game goes as follow:

    First move of each player can be on any empty spot
    Next moves can only be on an empty, adjacent to already taken by the player making a move, spot (corners allowed)
    Game continues until a player has no legal moves (The other player wins), or the board is full, which leads to a draw.
    The game is turn based, so player 1 makes a move, then player 2 and repeat

A save.txt file consists of the first line with 2 numbers separated by a space representing the height and the width of board (in that order), and the second line that contains 0s, 1s and 2s representing the pixel values.

Example File:

.txt
4 3
0 0 2 0 0 1 2 0 1 1 2 0

Grading

The bonus points will be awarded in accordance to the following requirements:

- For one bonus point, you must make an application that compiles, implements basic requirements and show it to one of the TA's and answer their questions about your code. Each member should know how all the code in the project works, even if they have not written it personally.

- For two bonus points, you must accomplish what is required for one bonus point and implement at least two of the following features:

    Allow the game to be played by more than 2 players (with setup for how many players should play).

    Add tabs for having multiple games open at the same time

    Create an bot to allow single-player experience, while still supporting a 2 player game.

    Add additional UI for player setup with colors and name, current score and a leaderboard of past results on the side, those should be saved and loaded as well.

    Allow the players to create, save and load maps, which will contain walls, which cannot be taken by a player
