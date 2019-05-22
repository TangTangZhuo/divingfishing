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

    public GameObject fingerGuide;

	public FishShip fishShip;

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
		if (fishShip) {
			fishShip.BodyReady ();
		}
        //DepthLine.Instance.gameObject.SetActive(false);
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
		if (levelIndex == 4) {
			SceneManager.LoadScene ("Level4");
		}
	}

	void InitScene(){
		string levelIndex = PlayerPrefs.GetInt ("Level", 1).ToString();
		if (!SceneManager.GetActiveScene ().name.EndsWith (levelIndex)) {
            PlayerPrefs.SetInt("EnterGame", 1);
            LoadScene ();            
		}

	}

	public void onStartButton(){
        if (PlayerPrefs.GetInt("fingerGuide", 0) == 1 || (PlayerPrefs.GetInt("valueDepth", -17) != -17))
        {
            MultiHaptic.HapticMedium();
            isRunning = true;
            isReady = false;
            SubmarineController.Instance.gravityScale = 2;
            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
                SubmarineController.Instance.moveSpeed = SubmarineController.Instance.maxSpeed;
            else
                SubmarineController.Instance.moveSpeed = SubmarineController.Instance.maxSpeed;
            FishGenerate.Instance.GenerateFish();
            BGmanager.Instance.GenerateWaterF();
            BGmanager.Instance.GenerateParallx();
            BGmanager.Instance.GenerateBubble();
            DepthLine.Instance.GenerateLine();
            SubmarineController.Instance.InitProgressSlider();
            HideUI();
            SubmarineController.Instance.progressSlider.gameObject.SetActive(true);
            SubmarineController.Instance.SynDepth();
            PlayerPrefs.SetInt("quitGame", 0);
            ReShape.Instance.ChangeShape();
			AudioManager.Instance.waterAudio.SetActive (false);
			if (fishShip) {
				fishShip.MouthOC ();
				fishShip.BodyStart ();
			}
			if (PlayerPrefs.GetInt ("fishingpass", 0) == 0 && PlayerPrefs.GetInt ("no_ads", 0) == 0) {				
				StartCoroutine (ChangeOffset ());
			}
			SubmarineController.Instance.StartFishingTime ();
			TTADManager.Instance.Start_Game ();
        }else{

            HideUI();
            fingerGuide.SetActive(true);
        }

	}

	IEnumerator ChangeOffset(){
		while (true) {
			Camera.main.GetComponent<CameraController> ().offset = Mathf.Lerp (1.6f, 0.7f, Time.deltaTime);
			if (Camera.main.GetComponent<CameraController> ().offset < 0.8f) {
				yield break;
			}
			yield return null;
		}
	}

	void HideUI(){
		for (int i = 0; i < UIs.Length; i++) {
			UIs [i].SetActive (false);
		}
	}
}
