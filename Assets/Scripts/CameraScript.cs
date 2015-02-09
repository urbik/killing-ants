using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private Vector3 cameraOffset = new Vector3(6, 16, -2);
	private float firstY = -1000;
	private float eps = .005f;

	void Start () {
		
	}

	void Update () {

	}

	public void updatePosition(Vector3 playerPosition)
	{
		Vector3 newCameraPosition = playerPosition + cameraOffset;
		if (firstY < (-1000 + eps))
		{
			firstY = newCameraPosition.y;
		}
		newCameraPosition.y = firstY;
		transform.position = newCameraPosition;
	}
}