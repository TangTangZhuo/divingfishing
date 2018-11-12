using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DailyReward : MonoBehaviour {

	Button passBtn;
	Button freeBtn;
	IPAManager iPAManager;
	FlyGold flyGold;

	// Use this for initialization
	void Start () {
		iPAManager = transform.parent.GetComponent<IPAManager> ();
		flyGold = (FlyGold)UnityEngine.Object.FindObjectOfType (typeof(FlyGold));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetPassDailyReward(){	
		if (PlayerPrefs.GetInt ("fishingpass", 0) == 0) {			
			iPAManager.OnDailyBackBtn ();
			iPAManager.OnVipBtn ();
		}
		if (PlayerPrefs.GetInt ("fishingpass", 0) == 1) {
			PlayerPrefs.SetInt ("FreeRward", 0);
			PlayerPrefs.SetInt ("NewDay", 0);
			ClamReward ();
		}
	}

	public void GetFreeDailyReward(){
		PlayerPrefs.SetInt ("FreeRward", 0);
		PlayerPrefs.SetInt ("NewDay", 0);
		ClamReward ();
	}

	void ClamReward(){
		iPAManager.OnDailyBackBtn ();
		iPAManager.UpdateDailyState ();
		int gold = PlayerPrefs.GetInt ("gold", 0) + 1000000;
		PlayerPrefs.SetInt ("gold", gold);
		flyGold.FlyGoldGenerate (flyGold.targetPos);
		UIManager.Instance.goldT.DOText (UIManager.UnitChange (gold), 1f, false, ScrambleMode.Numerals, null).SetDelay (1);
		Upgrading.Instance.CheckGold (gold);
		UpgradingOffline.Instance.CheckGold (gold);
	}

	public void UpdateDailyState(){
		
		passBtn = transform.Find("BG").Find("Pass").GetComponent<Button>();
		freeBtn = transform.Find("BG").Find("Free").GetComponent<Button>();

		int freeRward = PlayerPrefs.GetInt ("FreeRward", 0);
		freeBtn.transform.Find("Text").GetComponent<Text>().text = freeRward+"/5";

		if (freeRward < 5) {
			freeBtn.interactable = false;
		}
		if (freeRward >= 5) {
			freeBtn.interactable = true;
		}
	}
}
