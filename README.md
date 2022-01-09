# Swamp
* A platformer 2D game written in C# and Unity Engine.
* An university game project.
* Genres: Adventure, Puzzle, Platformer.
* Currently in early **BETA**.
* Artworks in the game are not possessed by the game creator.
* Soure code: [https://github.com/AdrianNguyen-UIT/Swamp](https://github.com/AdrianNguyen-UIT/Swamp)
* You can play the game at: [https://adriannguyen-uit.itch.io/swamp](https://adriannguyen-uit.itch.io/swamp)
* There are **no SFX** in the current state of the game yet.

## Storyline
* A cute little creature stucks in a mysterious, magical swamp.
* Using the mystical power of the runes he picked up in the swap, the truth of the swamp is slowly revealed.

## Controls (Keyboard only)
* Move left: **A**
* Move right: **D**
* Climb up: **W**
* Climb down: **S**
* Jump: **Space**
* Sprint: **L-Shift**
* Interact: **F**
* Active *first* ability: **Left Mouse Button**
* Active *second* ability: **Right Mouse Button**
* Pause: **ESC**
* Enable/Disable *Cheat Mode*: **C**

## Stamina System
* Movement and abilities need stamina to performed or actived (excluded *walking*).

## Ability System
* Player can use abilities based on the type of rune he picks up.
* There are 4 abilities at the moment.

Ability      | Stamina Cost     | Type (Passive/Active)      | Description              |
|------------|------------------|----------------------------|--------------------------|
|Dash        | 20               | Active                     | Dash in forward direction|
|High Jump   | 25               | Active                     | Jump 1.5x normal jump height, can be active anytime |
|Pump Up     | 0                | Passive                    |  Increase walk, sprint, climb, push/pull speed and jump force. No extra stamina cost|
|Rewind      | 30               | Active                     | Rewind to any of the previous point of time in the last 5 seconds |

## Cheat mode
* To be removed in the final version of the game.
* Allow player to **fly**.
* Player cannot jump.
