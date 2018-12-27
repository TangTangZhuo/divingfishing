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

	//background
	Image illustration;//0 97 171
	Image maskTop;//0 97 171
	Image topBg;//123 195 246
	Image xup;//
	Color illustrationColor;
	Color maskTopColor;
	Color topBgColor;
	Color illustrationColor_C = new Color(0/255,97/255f,171/255f);
	Color maskTopColor_C = new Color(0/255,97/255f,171/255f);
	Color topBgColor_C = new Color(123/255f,195/255f,246/255f);
	ChristmasSrc christmasSrc;

	void Start () {	
		GetSelfBG ();
		christmasSrc = ChristmasSrc.Instance;
		int level = PlayerPrefs.GetInt ("Level", 1);
		if (level == 4) {
			OnChangeBtn ();
		}
	}

	void GetSelfBG(){
		Transform parent = transform.parent;
		illustration = parent.GetComponent<Image> ();
		maskTop = parent.Find ("maskTop").GetComponent<Image> ();
		topBg = parent.Find ("Top").Find ("bg").GetComponent<Image> ();
		xup = parent.Find ("XUP").GetComponent<Image> ();
		illustrationColor = illustration.color;
		maskTopColor = maskTop.color;
		topBgColor = topBg.color;
	}

	public void OnChangeBtn(){
		areaBG.SetActive (!areaBG.activeSelf);
		extraBG.SetActive (!extraBG.activeSelf);
		ChangeBackground ();
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

	void ChangeBackground(){
		if (extraBG.activeSelf) {
			illustration.color = illustrationColor_C;
			maskTop.color = maskTopColor_C;
			topBg.color = topBgColor_C;
			xup.sprite = christmasSrc.xup_C;
			Illustration.Instance.fishItem = christmasSrc.fishItem_C;
		} else {
			illustration.color = illustrationColor;
			maskTop.color = maskTopColor;
			topBg.color = topBgColor;
			xup.sprite = christmasSrc.xup;
			Illustration.Instance.fishItem = christmasSrc.fishItem;
		}
	}
}
