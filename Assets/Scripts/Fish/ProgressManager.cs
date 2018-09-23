using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour {
	public Slider progressSlider;
	public Slider sliderBad;
	public bool isReady;
	public bool isRunning;
	public bool isOver;
	public bool isOvering;
	public GameObject[] UIs;

	private static ProgressManager instance;
	public static ProgressManager Instance{
		get{return instance;}
	}

	void Awake(){
		instance = this;	
	}

	// Use this for initialization
	void Start () {
		InitScene ();
		isReady = true;
		isRunning = false;
		isOver = false;
		isOvering = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void GameWin(){
		LoadScene();
	}

	public void GameOver(){
		isOver = true;
		isRunning = false;
	}

	void LoadScene(){
		int levelIndex = PlayerPrefs.GetInt ("Level", 1);
		if (levelIndex == 1) {
			SceneManager.LoadScene ("Level1");
		}
		if (levelIndex == 2) {
			SceneManager.LoadScene ("Level2");
		}
		if (levelIndex == 3) {
			SceneManager.LoadScene ("Level3");
		}
	}

	void InitScene(){
		string levelIndex = PlayerPrefs.GetInt ("Level", 1).ToString();
		if (!SceneManager.GetActiveScene ().name.EndsWith (levelIndex)) {
			LoadScene ();
		}

	}

	public void onStartButton(){
		MultiHaptic.HapticMedium ();
		isRunning = true;
		isReady = false;
		SubmarineController.Instance.gravityScale = 2;
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
			SubmarineController.Instance.moveSpeed = 1;
		else
			SubmarineController.Instance.moveSpeed = 10;
		FishGenerate.Instance.GenerateFish ();
		BGmanager.Instance.GenerateWaterF ();
		BGmanager.Instance.GenerateParallx ();
		BGmanager.Instance.GenerateBubble ();
		SubmarineController.Instance.InitProgressSlider ();
		HideUI ();
		SubmarineController.Instance.progressSlider.gameObject.SetActive (true);
		SubmarineController.Instance.SynDepth ();
		PlayerPrefs.SetInt ("quitGame", 0);
	}

	void HideUI(){
		for (int i = 0; i < UIs.Length; i++) {
			UIs [i].SetActive (false);
		}
	}
}
