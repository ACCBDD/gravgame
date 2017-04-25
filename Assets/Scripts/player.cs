using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class player : MonoBehaviour {

	public GameObject viewCamera;
	public CanvasGroup canvasFlash;
	public Transform cameraTransform;

	private Quaternion startRot;
	private Quaternion rotTo;
	private Rigidbody rb;
	public Vector3 oldGrav;
	private bool isGrounded = false;
	private bool flash;
	private bool moveEnabled = false;
	private float startTime;

	void Start() {
		rb = GetComponent<Rigidbody>();
		oldGrav = Physics.gravity;
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

		if (oldGrav != Physics.gravity) {
			startRot = viewCamera.transform.rotation;
			if (Physics.gravity == new Vector3(0, 9.81f, 0)) {
				rotTo = Quaternion.Euler(0, 0, 180);
			} else if (Physics.gravity == new Vector3(0, -9.81f, 0)) {
				rotTo = Quaternion.Euler(0, 0, 0);
			} else if (Physics.gravity == new Vector3(9.81f, 0, 0)) {
				rotTo = Quaternion.Euler(0, 0, 90);
			} else {
				rotTo = Quaternion.Euler(0, 0, 270);
			}
			startTime = Time.time;
			moveEnabled = true;
			oldGrav = Physics.gravity;
		}

		if (moveEnabled) {
			float dist = (Time.time - startTime) * 30;
			float travel = dist/40 * (Mathf.PI/2);
			viewCamera.transform.rotation = Quaternion.Lerp(startRot, rotTo, Mathf.Sin(travel));
			if(travel >= (Mathf.PI/2)) {
				viewCamera.transform.rotation = Quaternion.Lerp(startRot, rotTo, 1);
				moveEnabled = false;
			}
		}

		if(isGrounded) {
			if(Input.GetKeyDown(KeyCode.W)) {
				oldGrav = Physics.gravity;
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
				oldGrav = Physics.gravity;
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
				oldGrav = Physics.gravity;
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
				oldGrav = Physics.gravity;
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
