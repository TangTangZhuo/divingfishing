using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradingOffline : MonoBehaviour {
	private Text valueT;
	private Text priceT;

	private int value;
	private int price;
	private int gold;

	private static UpgradingOffline instance;
	public static UpgradingOffline Instance{
		get{return instance;}
	}

	void Awake(){
		instance = this;	
	}
	// Use this for initialization
	void Start () {		
		valueT = transform.Find ("value").GetComponent<Text> ();
		priceT = transform.Find ("price").GetComponent<Text> ();

		UpdateData ();
		CheckGold ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateData(){
		
		UIManager.Instance.offlineGold = PlayerPrefs.GetInt ("valueOffline", 40);

		value = PlayerPrefs.GetInt ("valueOffline", UIManager.Instance.offlineGold);
		price = PlayerPrefs.GetInt ("priceOffline", 2810);
		valueT.text = "+$" + UIManager.UnitChange(value) + "/Min";
		priceT.text = "$" + UIManager.UnitChange(price);

		gold = PlayerPrefs.GetInt ("gold", 0);
		UIManager.Instance.goldT.DOText (UIManager.UnitChange (gold), 0.5f, false, ScrambleMode.None, null);
	}

	public void OnOfflineClick(){
		if (gold > price) {
			MultiHaptic.HapticMedium ();
			gold -= price;

			if (UIManager.Instance.offlineGold < 400) {
				UIManager.Instance.offlineGold += 40;
			} else {
				UIManager.Instance.offlineGold = (int)(UIManager.Instance.offlineGold*1.25f);
			}

			price = (int)(price * 1.25f);
			PlayerPrefs.SetInt ("valueOffline", UIManager.Instance.offlineGold);
			PlayerPrefs.SetInt ("priceOffline", price);
			PlayerPrefs.SetInt ("gold", gold);
			UpdateData ();
			CheckGold ();
		}
	}

	public void CheckGold(){
		if (gold >= price) {
			transform.GetComponent<Button> ().interactable = true;
		} else {
			transform.GetComponent<Button> ().interactable = false;
		}
	}

}
