using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lava : MonoBehaviour {

	public Vector3 resetPosition;
	public Vector3 gravitySet;
	public bool resetGrav;
	public AudioSource sound;

	void OnCollisionStay (Collision collide) {
		if (collide.gameObject.tag == "Player") {
			collide.transform.position = resetPosition;
			if (resetGrav)
				Physics.gravity = gravitySet;
			collide.rigidbody.velocity = Vector3.zero;
			collide.transform.rotation = Quaternion.identity;
			collide.rigidbody.angularVelocity = Vector3.zero;
			sound.Play();
		}
	}
}
