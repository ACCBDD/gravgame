using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliderMove2 : MonoBehaviour {

	public float moveTime = 0;
	public float initialX;
	public float initialY;
	public float moveDist;

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(initialX + Mathf.Sin(moveTime) * moveDist, initialY, 0);
		moveTime += Time.deltaTime;
	}
}
