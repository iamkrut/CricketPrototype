using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour {

	public static CameraControllerScript instance;

	public GameObject ball; // the ball game object

	public Vector3 defaultPosition; // default position of camera
	public Vector3 afterBallHitPosition; // the position camera will shift to once the ball is hit
	public Vector3 zoomedViewPosition; // camera's position for a zoomed view
	public float interpolationInterval; // camera's interpolation interval value for lerp
	public float afterHitInterpolationInterval; // camera's interpolation interval value for lerp
	public float xBoundaryValue; // make camera follow the ball on its x axis after it is hit if the ball's x position gets out of the range [-x, x]

	private Vector3 startPosition; // start positon for lerp 
	private Vector3 offsettedPosition; // camera's offseted positon from the ball
	private float interpolationValue; // this value will go from 0 to 1
	private float afterHitInterpolationValue; // this value will go from 0 to 1
	private bool isBallHit; // whether to follow the ball after the bat hits the ball

	public bool IsBallHit {
		set{
			isBallHit = value;
			if (value) {
				startPosition = transform.position; // reset startPositon to the current camera's position
			}
		}
	}

	void Awake() {
		instance = this;
		ResetCamera (); // reset camera
	}

	void Update() {
		if (isBallHit) { // if the player has hit the ball
			// lerp the camera's position from startPosition to an offsetted value from the ball
			afterHitInterpolationValue += afterHitInterpolationInterval * Time.deltaTime;
			offsettedPosition = new Vector3 (ball.transform.position.x < -xBoundaryValue && ball.transform.position.x > xBoundaryValue ? (ball.transform.position.x + afterBallHitPosition.x) : (ball.transform.position.x - afterBallHitPosition.x), afterBallHitPosition.y + ball.transform.position.y, ball.transform.position.z + afterBallHitPosition.z);
			transform.position = Vector3.Lerp (startPosition, offsettedPosition, afterHitInterpolationValue);	
		}else if (ball.transform.position.z >= 8 && interpolationValue < 1) {
			// lerp the camera's position to get a zoomed view
			interpolationValue += interpolationInterval * Time.deltaTime;
			zoomedViewPosition = new Vector3 (0, 9.15f, 8.21f);
			transform.position = Vector3.Lerp (startPosition, zoomedViewPosition, interpolationValue);	
		} 
	}

	public void ResetCamera() {
		isBallHit = false; // reset isBallHit to false
		interpolationValue = 0; // reset interpolationValue to 0
		afterHitInterpolationValue = 0; // reset afterHitInterpolationValue to 0
		transform.position = defaultPosition; // reset camera's position to the default beginning position
		startPosition = defaultPosition; // reset the lerps starting position back to the default position
	}
}
