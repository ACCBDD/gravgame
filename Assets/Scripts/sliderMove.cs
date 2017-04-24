using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliderMove : MonoBehaviour {

	public float moveTime = 0;

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(74.5f, -40 + Mathf.Sin(moveTime) * 10, 0);
		moveTime += Time.deltaTime;
	}
}
