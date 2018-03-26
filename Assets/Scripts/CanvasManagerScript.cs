using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManagerScript : MonoBehaviour {

	public static CanvasManagerScript instance;

	public GameObject batSwipePanel;
	public GameObject ballSpeedSlider;
	public Text ballSpeedText;
	public GameObject batSpeedSlider;
	public Text batSpeedText;
	public GameObject batElevationSlider;
	public Text batElevationText;
	public Text ballTypeButtonText;
	public Text trajectoryButtonText;
	public Text ballBounceAngleText;

	public float minBatElevationValue;
	public float maxBatElevationValue;

	public float minInGameBallSpeed;
	public float maxInGameBallSpeed;
	public float minRealWorldBallSpeed;
	public float maxRealWorldBallSpeed;

	public float minInGameBatSpeed;
	public float maxInGameBatSpeed;
	public float minRealWorldBatSpeed;
	public float maxRealWorldBatSpeed;

	private float ballSpeedSliderValue;
	private float ingameBallSpeed;
	private float realWorldBallSpeed;

	private float batSpeedSliderValue;
	private float ingameBatSpeed;
	private float realWorldBatSpeed;

	private float batElevationValue;

	private int ballType;

	void Awake(){
		instance = this;
	}

	void Start(){
		UpdateDefaultValues ();
	}

	// Update UI to default values
	private void UpdateDefaultValues(){
		ChangeBallSpeed ();
		ChangeBatSpeed ();
		ChangeBatElevation ();
	}

	// Called when the ball speed slider value is changed
	public void ChangeBallSpeed(){
		ballSpeedSliderValue = ballSpeedSlider.GetComponent<Slider>().value;
		ingameBallSpeed = ScaleSpeedToIngame(ballSpeedSliderValue, minInGameBallSpeed, maxInGameBallSpeed, 0, 1);
		realWorldBallSpeed = ScaleSpeedToIngame(ballSpeedSliderValue, minRealWorldBallSpeed, maxRealWorldBallSpeed, 0, 1);
		ballSpeedText.text = realWorldBallSpeed.ToString("#.##") + "kmph";
		// Update ballSpeed to inGameBallSpeed
		BallControllerScript.instance.BallSpeed = ingameBallSpeed;
	}

	// Called when the bat speed slider value is changed
	public void ChangeBatSpeed(){
		batSpeedSliderValue = batSpeedSlider.GetComponent<Slider>().value;
		ingameBatSpeed = ScaleSpeedToIngame(batSpeedSliderValue, minInGameBatSpeed, maxInGameBatSpeed, 0, 1);
		realWorldBatSpeed = ScaleSpeedToIngame(batSpeedSliderValue, minRealWorldBatSpeed, maxRealWorldBatSpeed, 0, 1);
		batSpeedText.text = realWorldBatSpeed.ToString("#.##") + "kmph";
		// Update batSpeed to inGameBatSpeed
		BatControllerScript.instance.BatSpeed = ingameBatSpeed;
	}

	// Called when the bat elevation slider value is changed
	public void ChangeBatElevation(){
		batElevationValue = batElevationSlider.GetComponent<Slider>().value;
		batElevationValue = ScaleSpeedToIngame(batElevationValue, minBatElevationValue, maxBatElevationValue, 0, 1);
		//rb.velocity = direction * ballSpeed;
		batElevationText.text = batElevationValue.ToString("0") + " degrees";
		// Update batElevation to batElevationValue
		BatControllerScript.instance.BatElevation = batElevationValue;
	}

	// Scale the speed value from one range to another 
	// Example: from a value between range 0 to 1 to a value corresponding to that value, between range 20 to 40.
	private float ScaleSpeedToIngame (float speed, float scaleMinTo, float scaleMaxTo, float scaleMinFrom, float scaleMaxFrom){
		return (scaleMaxTo - scaleMinTo) * (speed - scaleMinFrom) / (scaleMaxFrom - scaleMinFrom) + scaleMinTo;
	}

	// reset everything to default except UI;
	public void OnReset() {
		BallControllerScript.instance.ResetBall ();
		BatControllerScript.instance.ResetBat ();
		CameraControllerScript.instance.ResetCamera ();
		StumpsControllerScript.instance.ResetStumps ();
		UpdateDefaultValues ();
		batSwipePanel.SetActive (false);
	}

	// Called when the switch side of the ball button is pressed
	public void OnSwitchSide(){
		BallControllerScript.instance.SwitchSide ();
	}

	// Called when the change type of the ball button is pressed
	public void OnBallTypeButton() {
		ballType++;
		if (ballType > 2) {
			ballType = 0;
		}

		switch (ballType) {
		case 0:
			ballTypeButtonText.text = "Straight";
				break;
		case 1:
			ballTypeButtonText.text = "Leg Spin";
			break;
		case 2:
			ballTypeButtonText.text = "Off Spin";
			break;
		}

		BallControllerScript.instance.BallType = ballType;
	}

	// Called when the enable trajectory button is pressed
	public void OnTrajectoryButton(){
		if (BallControllerScript.instance.IsTrajectoryEnabled) {
			BallControllerScript.instance.IsTrajectoryEnabled = false;
			trajectoryButtonText.text = "Trajectory: Disabled";
		}else{
			BallControllerScript.instance.IsTrajectoryEnabled = true;
			trajectoryButtonText.text = "Trajectory: Enabled";
		}
	}

	// Enable the bat swipe panel
	public void EnableBatSwipePanel(){
		batSwipePanel.SetActive (true);
	}

	// Update the ball's bounce text
	public void UpdateBallsBounceAngleUI (float angle){
		ballBounceAngleText.text = "After Bounce Angle: " + angle.ToString ("##.##");
	}
}	
