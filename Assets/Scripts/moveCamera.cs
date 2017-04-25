using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {

	public GameObject viewCamera;
	public Vector3 moveTo;
	public float targetRot;
	private BoxCollider cd;
	private MeshRenderer mr;
	private Vector3 startPos;
	private Quaternion startRot;
	private Quaternion rotTo;
	private float speed = 30;
	private float startTime;
	private float moveLength;
	private bool moveEnabled = false;

	void Start() {
		mr = GetComponent<MeshRenderer>();
		cd = GetComponent<BoxCollider>();
		viewCamera.transform.position = new Vector3(0, 0, -36);
	}

	void OnTriggerExit() {
		startPos = viewCamera.transform.position;
		startRot = viewCamera.transform.rotation;
		moveEnabled = true;
		startTime = Time.time;
		moveLength = Vector3.Distance(startPos, moveTo);
		rotTo = Quaternion.Euler(0, 0, targetRot);
		mr.enabled = true;
		cd.isTrigger = false;
	}
	// Update is called once per frame
	void Update() {
		if (moveEnabled) {
			float dist = (Time.time - startTime) * speed;
			float travel = dist/moveLength * (Mathf.PI/2);
			viewCamera.transform.position = Vector3.Lerp(startPos, moveTo, Mathf.Sin(travel));
			viewCamera.transform.rotation = Quaternion.Lerp(startRot, rotTo, Mathf.Sin(travel));
			if(travel >= (Mathf.PI/2)) {
				viewCamera.transform.position = Vector3.Lerp(startPos, moveTo, 1);
				viewCamera.transform.rotation = Quaternion.Lerp(startRot, rotTo, 1);
				moveEnabled = false;
			}
		}
	}
}
