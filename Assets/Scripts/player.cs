using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class player : MonoBehaviour {
	private bool isGrounded = false;
	public Sprite[] arrowSprites = new Sprite[4];
	public Image uiArrow;
	public CanvasGroup canvasFlash;
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
			if(Input.GetKeyDown(KeyCode.W) && uiArrow.sprite != arrowSprites[0]) {
				Physics.gravity = new Vector3(0, 9.81f, 0);
				uiArrow.sprite = arrowSprites[0];
				flash = true;
				canvasFlash.alpha = 1;
			}

			if(Input.GetKeyDown(KeyCode.S) && uiArrow.sprite != arrowSprites[2]) {
				Physics.gravity = new Vector3(0, -9.81f, 0);
				uiArrow.sprite = arrowSprites[2];
				flash = true;
				canvasFlash.alpha = 1;
			}

			if(Input.GetKeyDown(KeyCode.A) && uiArrow.sprite != arrowSprites[3]) {
				Physics.gravity = new Vector3(-9.81f, 0, 0);
				uiArrow.sprite = arrowSprites[3];
				flash = true;
				canvasFlash.alpha = 1;
			}

			if(Input.GetKeyDown(KeyCode.D) && uiArrow.sprite != arrowSprites[1]) {
				Physics.gravity = new Vector3(9.81f, 0, 0);
				uiArrow.sprite = arrowSprites[1];
				flash = true;
				canvasFlash.alpha = 1;
			}

			if(Input.GetKeyDown("mouse 1")) {
				rb.AddForce((MousePointInWorld() - transform.position).normalized * 500);
			}
		}
	}
}
