using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endCamera : MonoBehaviour {

	public GameObject viewCamera;
	public Vector3 moveTo;
	public float targetRot;
	public player daddy;
	public Text highscoreCongrats, highscoreName, highscoreListName, highscoreListScores;
	public InputField nameInput;
	private int scoreIndex;
	private Vector3 startPos;
	private Quaternion startRot;
	private Quaternion rotTo;
	private float speed = 30;
	private float startTime;
	private float moveLength;
	private float gameEndTime;
	private bool moveEnabled = false;
	private bool scoreAdded = false;

	void Start() {
		viewCamera.transform.position = new Vector3(0, 0, -36);
		updateHighscores();
	}

	void updateHighscores() {
		highscoreListScores.text = formatNumAsTime(PlayerPrefs.GetFloat("Highscore0Score")) + "\n" +
		formatNumAsTime(PlayerPrefs.GetFloat("Highscore1Score")) + "\n" +
		formatNumAsTime(PlayerPrefs.GetFloat("Highscore2Score")) + "\n" +
		formatNumAsTime(PlayerPrefs.GetFloat("Highscore3Score")) + "\n" +
		formatNumAsTime(PlayerPrefs.GetFloat("Highscore4Score")) + "\n" +
		formatNumAsTime(PlayerPrefs.GetFloat("Highscore5Score")) + "\n" +
		formatNumAsTime(PlayerPrefs.GetFloat("Highscore6Score")) + "\n" +
		formatNumAsTime(PlayerPrefs.GetFloat("Highscore7Score")) + "\n" +
		formatNumAsTime(PlayerPrefs.GetFloat("Highscore8Score")) + "\n" +
		formatNumAsTime(PlayerPrefs.GetFloat("Highscore9Score"));

		highscoreListName.text = PlayerPrefs.GetString("Highscore0Name") + "\n" +
		PlayerPrefs.GetString("Highscore1Name") + "\n" +
		PlayerPrefs.GetString("Highscore2Name") + "\n" +
		PlayerPrefs.GetString("Highscore3Name") + "\n" +
		PlayerPrefs.GetString("Highscore4Name") + "\n" +
		PlayerPrefs.GetString("Highscore5Name") + "\n" +
		PlayerPrefs.GetString("Highscore6Name") + "\n" +
		PlayerPrefs.GetString("Highscore7Name") + "\n" +
		PlayerPrefs.GetString("Highscore8Name") + "\n" +
		PlayerPrefs.GetString("Highscore9Name");
	}

	string formatNumAsTime(float num) {
		string centiSecond = Mathf.Round((num * 100) % 100).ToString();
		string seconds = Mathf.Round(num % 60).ToString();
		string minutes = Mathf.Floor(num/60).ToString();
		if (int.Parse(seconds) < 10) { seconds = "0" + seconds.ToString(); }
		if (int.Parse(centiSecond) < 10) { centiSecond = "0" + centiSecond; }
		if (int.Parse(minutes) < 10) { minutes = "0" + minutes; }
		return(minutes + ":" + seconds + "." + centiSecond);
	}

	IEnumerator startRotating() {
		yield return new WaitForSeconds (3.0f);
		moveEnabled = true;
		startTime = Time.time;
	}

	void checkHighscore(float finishTime) {
		bool addScore = false;
		scoreIndex = 0;
		for (int i = 0; i < 10; i++) {
			float curHighscore = PlayerPrefs.GetFloat("Highscore"+ i.ToString() + "Score");
			if (finishTime < curHighscore) {
				scoreIndex = i;
				addScore = true;
				break;
			}
		}
		if(addScore) {
			highscoreCongrats.enabled = true;
			highscoreName.enabled = true;
		}
	}

	public void addHighscore() {
		if (!scoreAdded) {
			for (int i = 9; i >= scoreIndex; i--) {
				float curScore = PlayerPrefs.GetFloat("Highscore"+ i.ToString() + "Score");
				string curName = PlayerPrefs.GetString("Highscore"+ i.ToString() + "Name");
				Debug.Log("setting position " + (i+1) +" to the value " + curScore +" from position" + i);
				PlayerPrefs.SetFloat("Highscore"+ (i+1).ToString() + "Score", curScore);
				PlayerPrefs.SetString("Highscore"+ (i+1).ToString() + "Name", curName);
			}
			PlayerPrefs.SetFloat("Highscore"+ (scoreIndex).ToString() + "Score", gameEndTime);
			PlayerPrefs.SetString("Highscore"+ (scoreIndex).ToString() + "Name", highscoreName.text);
			highscoreCongrats.enabled = false;
			highscoreName.enabled = false;
			nameInput.readOnly = true;
			scoreAdded = true;
			updateHighscores();
		}
	}

	void OnTriggerExit(Collider collide) {
		if (collide.gameObject.tag == "Player") {
			startPos = viewCamera.transform.position;
			startRot = viewCamera.transform.rotation;
			daddy.canvasFlash.alpha = 1;
			daddy.flash = true;
			daddy.fadeSpeed = 0.3f;
			daddy.flashImage.color = new Color(0, 0, 1, 1f);
			daddy.gameObject.transform.position = new Vector3(-100, -1000, 0);
			moveLength = Vector3.Distance(startPos, moveTo);
			rotTo = Quaternion.Euler(0, 0, targetRot);
			gameEndTime = Time.timeSinceLevelLoad;
			StartCoroutine(startRotating());
		}
	}
	// Update is called once per frame
	void Update() {
		if (moveEnabled) {

		}
		if (moveEnabled) {
			float dist = (Time.time - startTime) * speed;
			checkHighscore(gameEndTime);
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
