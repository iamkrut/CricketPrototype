using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatControllerScript : MonoBehaviour {

	public static BatControllerScript instance;

	public GameObject ball; // the ball gameObject
	public float batSpeed; // the bat's speed
	public float batElevation; // the bat's elevation angle i.e. the bat's x rotation axis 
	public float boundaryPointX; // max x value the bat can cover

	public float batsmanReachLimitMin; // the ball can be hit once it is inside this limit
	public float batsmanReachLimitMax; // the ball cannot be hit once it gets outside this limit
	public Vector3 ballsPositionAtHit; // the balls position when it gets hit by the bat

	private bool isBatSwinged; // has the bat swinged
	private Vector3 defaultPosition; // bat's default beginning position

	public float BatSpeed { set { batSpeed = value; } }
	public bool IsBatSwinged { set { isBatSwinged = value; } get { return isBatSwinged; } }

	public float BatElevation { 
		set { 
			batElevation = value;
			transform.rotation = Quaternion.Euler (batElevation, transform.rotation.y, transform.rotation.z); // update the bats rotation to match the elevation
		} 
	}

	void Awake(){
		instance = this;
	}

	void Start(){
		defaultPosition = transform.position; // set defaultPosition to the bats beginning position
	}

	void Update(){
		// if the bat has not swinged once and the ball is thrown and inside the bats hit range then 
		if (!isBatSwinged && BallControllerScript.instance.IsBallThrown && ball.transform.position.z <= batsmanReachLimitMax) {
			transform.transform.position = new Vector3 (ball.transform.position.x, 
				transform.transform.position.y,
				transform.transform.position.z);
		}

		// Clamp the bats position withing the pitch width
		transform.position = new Vector3 (Mathf.Clamp(transform.position.x, -boundaryPointX, boundaryPointX), transform.position.y, transform.position.z);

		// if the bat has swinged once and the ball is hitted by the bat then update its position to the balls position at the time of hit
		// just to make it look as if the bat hit the ball
		if (IsBatSwinged && BallControllerScript.instance.IsBallHit) {
			transform.position = Vector3.MoveTowards (transform.position, ballsPositionAtHit, Time.deltaTime * 20);
		}
	}	

	public void HitTheBall (float dragAngle) {
		// if the ball is inside the bats hit range then hit the ball
		if (ball.transform.position.z >= batsmanReachLimitMin && ball.transform.position.z <= batsmanReachLimitMax) {
			AudioManagerScript.instance.PlayBatHitAudio (); // play the bat hit sound
			ballsPositionAtHit = ball.transform.position; // set the ballsHitPositon to the balls position at the time of hit
			transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, dragAngle, transform.rotation.eulerAngles.z); // change rotation of the bat on the y axis to the swipe dragAngle

			// Call the HitBall function of the BallControllerScript and pass it the forward direction of 
			//the bat's transform and the bat's speed
			BallControllerScript.instance.HitTheBall ((transform.forward), batSpeed); 
		}
	}

	public void ResetBat(){ // reset the values
		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, 0, 0);
		transform.position = defaultPosition;
		isBatSwinged = false;
	}
}
