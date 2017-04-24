using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class player : MonoBehaviour {
	private bool isGrounded = false;
	public CanvasGroup canvasFlash;
	public Transform cameraTransform;
	private bool flash;
	private Rigidbody rb;
	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody>();
	}
	void OnCollisionStay(Collision collide) {
		if (collide.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}

	void OnCollisionExit(Collision collide) {
		if (collide.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}

	Vector3 MousePointInWorld() {
		Ray mousePoint = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane zPlane = new Plane(new Vector3(0, 0, 1), new Vector3(0,0,0));
		float dist;
		zPlane.Raycast(mousePoint, out dist);
		return mousePoint.GetPoint(dist);
	}

	RaycastHit RaycastToMouse() {
		Vector3 direction = MousePointInWorld() - transform.position;
		RaycastHit hitInfo;
		Physics.Raycast(transform.position, direction, out hitInfo, Mathf.Infinity);
		return hitInfo;
	}

	void Update () {
		if (flash) {
			canvasFlash.alpha = canvasFlash.alpha - Time.deltaTime * 3;
						if (canvasFlash.alpha <= 0) {
								canvasFlash.alpha = 0;
								flash = false;
						}
		}

		if(isGrounded) {
			if(Input.GetKeyDown(KeyCode.W)) {
				Vector3 oldGrav = Physics.gravity;
				switch ((int)cameraTransform.eulerAngles.z) {
					case 0:
						Physics.gravity = new Vector3(0, 9.81f, 0);
						break;
					case 90:
						Physics.gravity = new Vector3(-9.81f, 0, 0);
						break;
					case 180:
						Physics.gravity = new Vector3(0, -9.81f, 0);
						break;
					case 270:
						Physics.gravity = new Vector3(9.81f, 0, 0);
						break;
				}
				if (oldGrav != Physics.gravity) {
					flash = true;
					canvasFlash.alpha = 1;
				}	
			}

			if(Input.GetKeyDown(KeyCode.S)) {
				Vector3 oldGrav = Physics.gravity;
				switch ((int)cameraTransform.eulerAngles.z) {
					case 0:
						Physics.gravity = new Vector3(0, -9.81f, 0);
						break;
					case 90:
						Physics.gravity = new Vector3(9.81f, 0, 0);
						break;
					case 180:
						Physics.gravity = new Vector3(0, 9.81f, 0);
						break;
					case 270:
						Physics.gravity = new Vector3(-9.81f, 0, 0);
						break;
				}
				if (oldGrav != Physics.gravity) {
					flash = true;
					canvasFlash.alpha = 1;
				}
			}

			if(Input.GetKeyDown(KeyCode.A)) {
				Vector3 oldGrav = Physics.gravity;
				switch ((int)cameraTransform.eulerAngles.z) {
					case 0:
						Physics.gravity = new Vector3(-9.81f, 0, 0);
						break;
					case 90:
						Physics.gravity = new Vector3(0, -9.81f, 0);
						break;
					case 180:
						Physics.gravity = new Vector3(9.81f, 0, 0);
						break;
					case 270:
						Physics.gravity = new Vector3(0, 9.81f, 0);
						break;
				}
				if (oldGrav != Physics.gravity) {
					flash = true;
					canvasFlash.alpha = 1;
				}
			}

			if(Input.GetKeyDown(KeyCode.D)) {
				Vector3 oldGrav = Physics.gravity;
				switch ((int)cameraTransform.eulerAngles.z) {
					case 90:
						Physics.gravity = new Vector3(0, 9.81f, 0);
						break;
					case 180:
						Physics.gravity = new Vector3(-9.81f, 0, 0);
						break;
					case 270:
						Physics.gravity = new Vector3(0, -9.81f, 0);
						break;
					case 0:
						Physics.gravity = new Vector3(9.81f, 0, 0);
						break;
				}
				if (oldGrav != Physics.gravity) {
					flash = true;
					canvasFlash.alpha = 1;
				}
			}

			if(Input.GetKeyDown("mouse 1")) {
				rb.AddForce((MousePointInWorld() - transform.position).normalized * 500);
			}
		}
	}
}
