using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaIllustration : MonoBehaviour {

	public Sprite[] AreaImage;
	public Sprite[] AreaClickImage;
	public Sprite[] fishImage;
	public string[] fishName;
	//public string[] fishName_Ch;
	//public string[] fishName_TW;
	public Sprite[] unusualImage;
	public Sprite[] unLockImage;

	// Use this for initialization
	void Start () {
		int level = PlayerPrefs.GetInt ("Level", 1);
		if (level == 1) {
			if (transform.name == "Area1") {
				ChangeArea ();
			}
		}if (level == 2) {
			if (transform.name == "Area2") {
				ChangeArea ();
			}
		}if (level == 3) {
			if (transform.name == "Area3") {
				ChangeArea ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeArea(){
		int index = 0;
		if (transform.name == "Area1") {
			index = 0;
			if (Application.systemLanguage == SystemLanguage.English) {
				Illustration.Instance.fishName = fishName;
			} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
				Illustration.Instance.fishName = FishTranslate.Instance.fishName_Ch1;
			}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
				Illustration.Instance.fishName = FishTranslate.Instance.fishName_TW1;
			}
		}if (transform.name == "Area2") {
			index = 10;
			if (Application.systemLanguage == SystemLanguage.English) {
				Illustration.Instance.fishName = fishName;
			} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
				Illustration.Instance.fishName = FishTranslate.Instance.fishName_Ch2;
			}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
				Illustration.Instance.fishName = FishTranslate.Instance.fishName_TW2;
			}
		}if (transform.name == "Area3") {
			index = 20;
			if (Application.systemLanguage == SystemLanguage.English) {
				Illustration.Instance.fishName = fishName;
			} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
				Illustration.Instance.fishName = FishTranslate.Instance.fishName_Ch3;
			}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
				Illustration.Instance.fishName = FishTranslate.Instance.fishName_TW3;
			}
		}
		Illustration.Instance.fishImage = fishImage;
		Illustration.Instance.unusualImage = unusualImage;
		Illustration.Instance.unLockImage = unLockImage;
		Illustration.Instance.IllUpdate (index);

		if (PlayerPrefs.GetInt ("Level", 1) == 1) {
			transform.GetComponent<Image> ().sprite = AreaClickImage [0];
		}
		if (PlayerPrefs.GetInt ("Level", 1) == 2) {
			transform.GetComponent<Image> ().sprite = AreaClickImage [1];
		}
		if (PlayerPrefs.GetInt ("Level", 1) == 3) {
			transform.GetComponent<Image> ().sprite = AreaClickImage [2];
		}
		if (PlayerPrefs.GetInt ("Level", 1) == 4) {
			transform.GetComponent<Image> ().sprite = AreaClickImage [2];
		}
	//	transform.GetComponent<Image> ().color = new Color (255/255f, 142/255f, 13/255f);
		MultiHaptic.HapticMedium ();

	}

	public void ChangeColorToWrite(){
		//transform.GetComponent<Image> ().color = new Color (229/255f, 113/255f, 6/255f);

		if (PlayerPrefs.GetInt ("Level", 1) == 1) {
			transform.GetComponent<Image> ().sprite = AreaImage [0];
		}
		if (PlayerPrefs.GetInt ("Level", 1) == 2) {
			transform.GetComponent<Image> ().sprite = AreaImage [1];
		}
		if (PlayerPrefs.GetInt ("Level", 1) == 3) {
			transform.GetComponent<Image> ().sprite = AreaImage [2];
		}
		if (PlayerPrefs.GetInt ("Level", 1) == 4) {
			transform.GetComponent<Image> ().sprite = AreaImage [2];
		}
	}
}
