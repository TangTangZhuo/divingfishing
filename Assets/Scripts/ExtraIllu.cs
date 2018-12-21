using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraIllu : MonoBehaviour {
	public GameObject areaBG;
	public GameObject extraBG;
	public Button area1Btn;

	public Sprite[] fishImage;
	public string[] fishName;
	public Sprite[] unusualImage;
	public Sprite[] unLockImage;

	void Start () {
		int level = PlayerPrefs.GetInt ("Level", 1);
		if (level == 4) {
			OnChangeBtn ();
		}
	}

	public void OnChangeBtn(){
		areaBG.SetActive (!areaBG.activeSelf);
		extraBG.SetActive (!extraBG.activeSelf);
		if (extraBG.activeSelf) {
			ChangeArea ();
		} else {
			area1Btn.onClick.Invoke ();
		}
	}

	public void ChangeArea(){
		if (Application.systemLanguage == SystemLanguage.English) {
			Illustration.Instance.fishName = fishName;
		} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified || Application.systemLanguage == SystemLanguage.Chinese) {			
			Illustration.Instance.fishName = FishTranslate.Instance.fishName_Ch4;
		} else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
			Illustration.Instance.fishName = FishTranslate.Instance.fishName_TW4;
		}

		Illustration.Instance.fishImage = fishImage;
		Illustration.Instance.unusualImage = unusualImage;
		Illustration.Instance.unLockImage = unLockImage;
		Illustration.Instance.ChristmasIllUpdate (30);

		MultiHaptic.HapticMedium ();

	}

}
