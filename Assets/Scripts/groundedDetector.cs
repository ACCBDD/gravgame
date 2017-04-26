using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundedDetector : MonoBehaviour {

	public player daddy;
	// Use this for initialization
	void OnTriggerStay(Collider collide) {
		if (collide.gameObject.tag == "Ground") {
			daddy.isGrounded = true;
		}
	}

	void OnTriggerExit() {
			daddy.isGrounded = false;
	}
}
