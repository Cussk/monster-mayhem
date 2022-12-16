![Screenshot (15)](https://user-images.githubusercontent.com/108804713/208202180-edd53005-b00e-45b0-84eb-bff715d0e9e3.png)
![Screenshot (16)](https://user-images.githubusercontent.com/108804713/208202182-3a7a6718-58c5-42c2-93b5-5a873cdae80b.png)


# monster-mayhem
 Personal Unity Project

Arena Battler, hero vs. the slimes using power ups to take on an array of monsters attempting to defeat you.


This project is the culmination of everything I learned with Unity's Junior Programmer learning pathway. It takes all the gameplay elements introduced throughout the course and applies them in new ways. This project has pushed my understanding of C# much further, giving me a better grasp of the interactions between scripts, reference errors, research and problem solving. Completing the final build process was very instructional and required much tweaking and further understanding of Unity's Editor to get my build up and running correctly.


Title screen with 3 difficulty levels starting the player with fewer lives, volume control, gameplay instructions. Pause screen with restart button to stat over. Game over screen after all lives lost with restart button. Player character controlled with physics and rigidbody. Enemies controlled with physics and rigidbodies. Enemies have varying speeds in which they approach you with boss monsters every 5 rounds with more health and can spawn mini enemies to attack you.


Pick up 3 powerups, damage on collision, arrows fired with "Spacebar", and homing magic missiles fired with "F" key. Enemies will take a life on collision with player if no powerup active. Enemies have health bars which decrease with every successful attack and destroyed when runs out. Player gains score with every successful enemy defeated.


Background music, character animations and sound effects. Enemies spawn in waves, increasing number of enemies with each successful wave. Boss spawns every 5th wave. Heart for extra life spawns with every boss. New powerup up spawns after current powerups coroutine runs out, also spawn on new waves.


All of the logic was scripted in C# taking what I learned with Create With Code, modifying and expanding upon it to implement new features.


This game was built from prototype to production build. Prototyping with simple geometrics to create gameplay elements. Assets added from the Unity Asset store and course assets from Create With Code were used to bring prototypes to final project. Game built with WebGL and hosted on Unity's site.


Playable version of game: https://play.unity.com/mg/other/monster-mayhem
