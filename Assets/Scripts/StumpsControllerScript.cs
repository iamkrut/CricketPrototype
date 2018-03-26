using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StumpsControllerScript : MonoBehaviour {

	public static StumpsControllerScript instance;

	public GameObject[] stumps; // store all the stumps
	public List<Vector3> defaultStumpPositions; // to store the default positions of all the stumps

	void Awake(){
		instance = this;
		defaultStumpPositions = new List<Vector3> ();
		foreach (GameObject stump in stumps) {
			defaultStumpPositions.Add (stump.transform.position); // add each stump's default position
		}
	}

	public void ResetStumps(){
		int count = 0; // count is the iterator
		foreach (GameObject stump in stumps) {
			stump.GetComponent<Rigidbody> ().velocity = Vector3.zero; // reset the stump's velocity to zero
			stump.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero; // reset the stump's angular velocity to zero
			stump.GetComponent<Rigidbody> ().useGravity = false; // reset stump's to not get affected by gravity
			stump.transform.position = defaultStumpPositions [count]; // reset the stump's position
			stump.transform.rotation = Quaternion.identity; // reset the stump's rotation
			count++; // increment the iterator
		}
	}
}
