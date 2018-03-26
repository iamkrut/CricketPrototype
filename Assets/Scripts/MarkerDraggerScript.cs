using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MarkerDraggerScript : MonoBehaviour, IBeginDragHandler, IDragHandler {

	public GameObject marker; // marker game object
	public float boundaryPointX; // max x value the marker can cover
	public float minBoundaryPointZ; // min z value the marker can cover
	public float maxBoundaryPointZ; // max z value the marker can cover

	public float scaleDownDragBy; // value for scaling down the touch drag value to an appropriate the world value

	private Vector2 startTouchPosition; // start touch position of the drag
	private Vector3 markerStartTouchPosition; // marker's position at the start of the drag
	private Vector2 newTouchPosition; // new touch position i.e. the current touch position
	private int touchId;

	void Awake(){
		Input.multiTouchEnabled = false; // disable multitouch
		#if PLATFORM_ANDROID // if the platform is android change the scaleDownDragBy value
		scaleDownDragBy = 0.05f;
		#endif
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) { 
		startTouchPosition = eventData.position; // set the startTouchPosition to the touch's start position 
		markerStartTouchPosition = marker.transform.position; // set the marker's start position
	}

	public void OnDrag(PointerEventData eventData) {
		if (!BallControllerScript.instance.IsBallThrown) { // if the ball is not thrown the marker can be moved
			newTouchPosition = eventData.position; // update it the current position of the touch

			// set the marker's position based on the drag value of the touch
			marker.transform.position = new Vector3 (markerStartTouchPosition.x + (newTouchPosition.x - startTouchPosition.x) * scaleDownDragBy,
				marker.transform.position.y,
				markerStartTouchPosition.z + (newTouchPosition.y - startTouchPosition.y) * scaleDownDragBy);

			// clamp the marker's position within it's reach bounds
			marker.transform.position = new Vector3 (Mathf.Clamp (marker.transform.position.x, -boundaryPointX, boundaryPointX),
				marker.transform.position.y,
				Mathf.Clamp (marker.transform.position.z, minBoundaryPointZ, maxBoundaryPointZ));
		}
	}
}
