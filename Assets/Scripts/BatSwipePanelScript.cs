using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BatSwipePanelScript : MonoBehaviour, IBeginDragHandler, IDragHandler {

	public static BatSwipePanelScript instance;

	public float minDrag; // the minimum length after which a drag i.e. swipe is considered valid

	private Vector2 startTouchPosition; // the touch's start position
	private Vector2 newTouchPosition; // the current touch's position 

	void Awake(){
		instance = this;
		Input.multiTouchEnabled = false; // switch multitouch off
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) { 
		startTouchPosition = eventData.position; // set startTouchPosition to the touch's start position
	}

	public void OnDrag(PointerEventData eventData) {
		
		newTouchPosition = eventData.position; // set newTouchPosition to current drag position

		// if the bat has not been swinged i.e the player has not tried hitting the ball before 
		// and the drag length is greated than the minimum drag length required then call the
		// BatControllerScript's HitTheBall function with dragAngle passed as the parameter
		if (!BatControllerScript.instance.IsBatSwinged && Vector2.Distance (newTouchPosition, startTouchPosition) >= minDrag) {
			BatControllerScript.instance.IsBatSwinged = true;
			Vector2 dragDirection = newTouchPosition - startTouchPosition; // direction vector of the drag
			float dragAngle = Mathf.Atan2(dragDirection.y, dragDirection.x) * Mathf.Rad2Deg; // angle of the direction vector

			// reset the dragAngle to match that of the world's angle
			dragAngle += 90; 
			dragAngle *= -1;

			// call the BatControllerScript's HitTheBall function with dragAngle passed as the parameter
			BatControllerScript.instance.HitTheBall (dragAngle);
		}
	}
}
