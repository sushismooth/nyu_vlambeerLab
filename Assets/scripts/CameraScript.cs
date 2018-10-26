using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public Camera mainCam;
	public float minX = 0;
	public float maxX = 0;
	public float minZ = 0;
	public float maxZ = 0;
	public float FOVBorder;
	
	// Update is called once per frame
	void Update () {
		if (Pathmaker.tileList.Count > 0)
		{
			SetCameraPos();
		}
	}

	void SetCameraPos()
	{
			Vector3 cameraPos = Vector3.zero;
			foreach (Transform tile in Pathmaker.tileList)
			{
				if (tile == null)
				{
					return;
				}
				cameraPos += tile.transform.position;
				if (tile.transform.position.x < minX)
				{
					minX = tile.transform.position.x;
				}
	
				if (tile.transform.position.x > maxX)
				{
					maxX = tile.transform.position.x;
				}
	
				if (tile.transform.position.z < minZ)
				{
					minZ = tile.transform.position.z;
				}
	
				if (tile.transform.position.z > maxZ)
				{
					maxZ = tile.transform.position.z;
				}
			}


		cameraPos = cameraPos / Pathmaker.tileList.Count;
		cameraPos.y = 100;
		mainCam.transform.position = cameraPos;

		Vector3 maxV3 = new Vector3(maxX, 0, maxZ);
		Vector3 minV3 = new Vector3(minX, 0, minZ);
		float aspectRatio = Screen.width / Screen.height;
		Vector3 middlePoint = (maxV3 + minV3) / 2;
		float distanceBetweenPoints = (maxV3 - minV3).magnitude;
		float distanceFromMiddlePoint = (mainCam.transform.position - middlePoint).magnitude;
		mainCam.fieldOfView = 2.0f * Mathf.Rad2Deg * Mathf.Atan((0.5f * distanceBetweenPoints) / (distanceFromMiddlePoint * aspectRatio)) + FOVBorder;

	}
}
