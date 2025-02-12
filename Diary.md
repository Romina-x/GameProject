# Week 14
### 08/02/2025
- Implemented a score system which shows a final score for the player on the Level Cleared screen.
### 07/02/2025
- Created a pause button and pause menu for the level.
- Created an in game timer element for the HUD.
### 06/02/2025
- Redesigned the current UI elements with new UI assets.
- Updated Game Over, Level Cleared screens and the Player's Health Bar.
### 04/02/2025
- Began refactoring scripts to use a LevelManager which controls the states of the level. (Playing, GameOver etc)

# Week 13
### 31/01/2025
- Created a collider to detect the player entering the goal area.
- Configured the goal scripts to activate the level cleared UI screen on collision.
- Created a goal indicator arrow to direct the player towards the goal after rescuing all the animals.
### 30/01/2025
- Redesign of quest UI based on user feedback.
### 29/01/2025
- Refactored animal script to use observer pattern.
- Created UI elements for questlines and created basic show and hide animations for them.
- Configured the quest animations and UI to show based on whether the animals are rescued or not.

# Week 12 - Start of Term 2
### 24/01/2025
- Improved the enemy combat mechanics based on user feedback by disabling enemies from being able to attack while stunned.
- Bugfix to prevent enemies from attacking during their death animation.
### 23/01/2025
- Created teleport mechanic for when animals get stuck too far away from the player.
- Set up new physics layers for the animals and teleport radius and configured them in the collision matrix.
### 22/01/2025
- Began working on animal follow mechanic (Animal follows the player once rescued from its cage).
- Created Animal script to manage the movement and set it up with basic following.

# Week 11
### 06/12/2024
- Bugfix to player's NavMeshAgent which was causing the agent to not relocate to the player when they leave the NavMeshSurface.
- Bugfix to the heart idle animation which was overriding the positions of all hearts in the scene.
- Finalised level one layout.
### 04/12/2024
- Refactored the UI system to use the observer pattern.
### 03/12/2024
- Added collectible hearts which increase the player's health.
### 02/12/2024
- Redesigned layout and environment for level one.

# Week 10
### 27/11/2024
- Created a second melee enemy which is faster and does less damage.
### 25/11/2024
- Created the HUD (Heads up diplay) for the player which includes player and enemy health bars.
- Created game over screen with restart level button, which is triggered when the player's health is below zero.
 
# Week 9
### 20/11/2024
- Reconfigured NavMeshSurface to fit the new terrain so that enemies can move around the new level area.
- Refactored code style on all files to follow unity guidelines for readability.
### 19/11/2024
- Continued building level one terrain.

# Week 8
### 16/11/2024
- Began constructing level one terrain and features.
### 11/11/2024
- Added cage with trapped animal for rescue mechanic: Enemies guard a cage and when they are defeated the cage disappears and the animal is released.

# Week 7
### 10/11/2024
- Extra bug fixing for player movement:
- Fix to jump animation bug which caused it to look like a double jump.
- Fix to bug that caused animations to get stuck when pressing jump twice.
### 9/11/2024
- Added a movement radius to the enemy: Now the enemy only starts following the player if the player enters it's follow radius. The Enemy returns to spawn if player exits the radius.
### 6/11/2024
- Aded player attack mechanic: Enemy takes damage on collision with the player's axe.
- Situation where player dies is still unhandled.
### 5/11/2024
- Added death animation for player.
- Fixed bug where enemy continues to follow player when dead.
### 4/11/2024
- Implemented enemy attack mechanic: Enemy auto attacks when the player is in range (Within the attack radius).

# Week 6
### 29/10/2024
- Refactored all the states out into their respective classes.
- Fixed gravity and running bugs from refactored code.

# Week 5
### 25/20/2024
- Began refactoring player class into a hierarchical state machine.
- Created all state classes and state factory.
### 23/10/2024
- Linked running animation to enemy movement.
### 21/10/2024
- Imported enemy prefab and configured it's movement to follow the player.
- Researched Nav Meshes and applied this to the enemy so that it uses AI to navigate around obstacles towards the player.

# Week 4
### 16/10/2024
- Fixed the jump movement and added the jump animation to it.
### 15/10/2024
- Researched animation layering for attack movement.
- Added attack animation that layers over walking and running.

# Week 3
### 13/10/2024
- First attempt at jump physics / movement. Not fixed.
- Added basic terrain / skybox.
### 12/10/2024
- Added walk and run movement, and linked them to their corresponding animations.
### 08/10/2024
- Began prototyping player character.
- Tied idle, walking and running animations to the keyboard keys.

# Week 2
### 05/10/2024
- Finished gathering 3D assets.
- Researched player movement in unity.
### 03/10/2024
- Finalised game idea and mechanics and documented them.
- Began gathering 3D assets.