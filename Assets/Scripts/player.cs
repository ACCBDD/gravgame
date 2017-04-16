using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	private bool isGrounded = false;

	// Use this for initialization
	void OnCollisionEnter(Collision collide) {
		if (collide.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}

	// Update is called once per frame
	void Update () {
		if(isGrounded) {
			if(Input.GetKey(KeyCode.W)) {
				Physics.gravity = new Vector3(0, 9.81f, 0);
			}

			if(Input.GetKey(KeyCode.S)) {
				Physics.gravity = new Vector3(0, -9.81f, 0);
			}

			if(Input.GetKey(KeyCode.A)) {
				Physics.gravity = new Vector3(-9.81f, 0, 0);
			}

			if(Input.GetKey(KeyCode.D)) {
				Physics.gravity = new Vector3(9.81f, 0, 0);
			}
		}
	}
}
