# CRICKET PROTOTYPE
### Gameplay
<div align="center">
  <a href="https://www.youtube.com/watch?v=-dPnljPxPdA"><img src="https://img.youtube.com/vi/-dPnljPxPdA/0.jpg" alt="IMAGE ALT TEXT"></a>
</div>

### UI Layout 
![alt text](https://github.com/KrutPatel2257/CricketPrototype/blob/master/CricketPrototype.png)

### How to Play?
1.	Set the values of the ball’s speed, ball’s type, balling side, bat’s speed and bat’s elevation.
2.	Drag across the screen to set the marker to the point where you want the ball to pitch.
3.	Press the “Throw” button to release the ball
4.	Swipe in any direction once the ball is in the hit range of the bat to hit it.
5.	Press “Reset” button to reset the game.

### Features
1.	The ball’s bounce angle will depend on the speed of the ball. Higher the speed, higher the bounce.
2.	The ball’s speed after it gets hit by the bat will depend on the bat’s speed and the ball’s speed. A ball with a higher speed will have more return speed compared to a slower ball at the same bat’s hit speed.
3.	The elevation of the ball will decide if the ball will be lofted or grounded.
4.	If a leg spin or off spin ball is delivered, the spin of the ball would also depend on the speed of the ball. So a fast ball would spin less compared to a slow one.

### How does it work?
#### When the ball is thrown:
In the beginning, the ball’s interaction with gravity is disabled. An impulse force will be added to the ball with the ball’s speed value in the direction of the marker’s position. Once it hits the ground, we will get the bounce direction by negating the y direction value and scale it up or down based on the speed of the ball and another scalar value. Now we will add an impulse force in that direction with ball’s speed value. We will also enable the effect of gravity on the ball by changing the useGravity to true on the rigidbody to make the movement look more realistic. 

#### After the player swipes to hit the ball:
I am using the bats forward direction to calculate the hit direction of the ball. Once the player has swiped across the screen, we will calculate its angle and update the bat’s rotation to match that. Now we will use the forward direction of the bat’s transform to get the hit direction and send the ball in the opposite direction of it by adding an impulse force of value based on the bat’s speed and half of the ball’s speed. 

** Note: the bat would not hit the ball if the swipe is not perfectly timed. The bat movements are not realistic. **
