using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class player : MonoBehaviour {
	public bool isGrounded = false;
	public bool flash;
	public float fadeSpeed = 3;
	public CanvasGroup canvasFlash;
	public Image flashImage;
	public Transform cameraTransform;
	public Text textBox;


	private string centiSecond, minutes, seconds;
	private AudioSource sound;
	// Use this for initialization
	void Awake () {
		Screen.SetResolution(640, 640, false);
		for (int i = 0; i < 10; i++) {
			if(!PlayerPrefs.HasKey("Highscore"+ i.ToString() + "Score")) {
				PlayerPrefs.SetFloat("Highscore"+ i.ToString() + "Score", 9999);
				PlayerPrefs.SetString("Highscore"+ i.ToString() + "Name", "<blank>");
				Debug.Log(i.ToString() + " " + PlayerPrefs.GetFloat("Highscore"+ i.ToString() + "Score").ToString() + " " + PlayerPrefs.GetString("Highscore"+ i.ToString() + "Name"));
			}

		}
	}

	void Start() {
		sound = GetComponent<AudioSource>();
	}

	void OnCollisionStay(Collision collide) {
		if (collide.gameObject.tag == "Respawn") {
			flash = true;
			canvasFlash.alpha = 1;
			flashImage.color = new Color(1, 0, 0, 0.8f);
		}
	}

	void OnCollisionExit(Collision collide) {
		if (collide.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}

	void Update () {

		if (flashImage.color != new Color(0, 0, 1, 1f)) {
			centiSecond = Mathf.Round((Time.timeSinceLevelLoad * 100) % 100).ToString();
			seconds = Mathf.Round(Time.timeSinceLevelLoad % 60).ToString();
			minutes = Mathf.Floor(Time.timeSinceLevelLoad/60).ToString();
			if (int.Parse(seconds) < 10) { seconds = "0" + seconds.ToString(); }
			if (int.Parse(centiSecond) < 10) { centiSecond = "0" + centiSecond; }
			if (int.Parse(minutes) < 10) { minutes = "0" + minutes; }
			textBox.text = minutes + ":" + seconds + "." + centiSecond;
		} else {
			if (canvasFlash.alpha == 0)
				Destroy(gameObject);
		}

		if (flash) {
			canvasFlash.alpha = canvasFlash.alpha - Time.deltaTime * fadeSpeed;
			if (canvasFlash.alpha <= 0) {
				canvasFlash.alpha = 0;
				flash = false;
			}
		}

		if(Input.GetKeyDown(KeyCode.R)) {
			Physics.gravity = new Vector3(0, -9.81f, 0);
			SceneManager.LoadScene(1);
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
					flashImage.color = new Color(1, 1, 1, 0.39f);
					canvasFlash.alpha = 1;
					sound.Play();
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
					flashImage.color = new Color(1, 1, 1, 0.39f);
					canvasFlash.alpha = 1;
					sound.Play();
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
					flashImage.color = new Color(1, 1, 1, 0.39f);
					sound.Play();
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
					flashImage.color = new Color(1, 1, 1, 0.39f);
					sound.Play();
				}
			}
		}
	}
}
