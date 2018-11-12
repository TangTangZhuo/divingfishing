using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradingOffline : MonoBehaviour {
	private Text valueT;
	private Text priceT;

	private long value;
	private long price;
	private long gold;

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

		UpdateData (1);
		CheckGold (gold);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateData(float dalay){
		
		UIManager.Instance.offlineGold = long.Parse( PlayerPrefs.GetString ("valueOffline", "40"));

		value = long.Parse( PlayerPrefs.GetString ("valueOffline", UIManager.Instance.offlineGold.ToString()));
		price = long.Parse( PlayerPrefs.GetString ("priceOffline", "2810"));
		valueT.text = "+$" + UIManager.UnitChange(value) + "/Min";
		priceT.text = "$" + UIManager.UnitChange(price);

		gold = long.Parse( PlayerPrefs.GetString ("gold", "0"));
		UIManager.Instance.goldT.text = UIManager.UnitChange(long.Parse( PlayerPrefs.GetString ("foreGold", PlayerPrefs.GetString("gold","0"))));	
		UIManager.Instance.goldT.DOText (UIManager.UnitChange (gold), 1f, false, ScrambleMode.Numerals, null).SetDelay(dalay);
	}

	public void OnOfflineClick(){
		gold = long.Parse( PlayerPrefs.GetString ("gold", "0"));
		if (gold > price) {
			MultiHaptic.HapticMedium ();
			gold -= price;

			if (UIManager.Instance.offlineGold < 400) {
				UIManager.Instance.offlineGold += 40;
			} else {
				UIManager.Instance.offlineGold = (long)(UIManager.Instance.offlineGold*1.25f);
			}

			price = (long)(price * 1.25f);
			PlayerPrefs.SetString ("valueOffline", UIManager.Instance.offlineGold.ToString());
			PlayerPrefs.SetString ("priceOffline", price.ToString());
			PlayerPrefs.SetString ("gold", gold.ToString());
			UpdateData (0);
			CheckGold (gold);
			Upgrading.Instance.CheckGold (gold);
		}
	}

	public void CheckGold(long curGold){
		if (curGold >= price) {
			transform.GetComponent<Button> ().interactable = true;
			transform.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		} else {
			transform.GetComponent<Button> ().interactable = false;
			transform.GetComponent<Image> ().color = new Color (200 / 255f, 200 / 255f, 200 / 255f, 0.5f);
		}
	}

}
