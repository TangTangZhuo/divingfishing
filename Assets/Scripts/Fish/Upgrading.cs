using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Upgrading : MonoBehaviour {
	private Text valueT;
	private Text priceT;

	private int value;
	private int price;
	private int gold;

	private static Upgrading instance;
	public static Upgrading Instance{
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
		CheckGold ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateData(float delay){
		
		if (PlayerPrefs.GetInt ("Level", 1) == 1) {
			UIManager.Instance.diveDepth = PlayerPrefs.GetInt ("valueDepth", -17);
			value = PlayerPrefs.GetInt ("valueDepth", UIManager.Instance.diveDepth);
			price = PlayerPrefs.GetInt ("priceDepth", 2810);
		}else if (PlayerPrefs.GetInt ("Level", 1) == 2) {
			UIManager.Instance.diveDepth = PlayerPrefs.GetInt ("valueDepth2", -17);
			value = PlayerPrefs.GetInt ("valueDepth2", UIManager.Instance.diveDepth);
			price = PlayerPrefs.GetInt ("priceDepth2", 1240000);
		}else if (PlayerPrefs.GetInt ("Level", 1) == 3) {
			UIManager.Instance.diveDepth = PlayerPrefs.GetInt ("valueDepth3", -17);
			value = PlayerPrefs.GetInt ("valueDepth3", UIManager.Instance.diveDepth);
			price = PlayerPrefs.GetInt ("priceDepth3", 31400000);
		}

		valueT.text = (UIManager.UnitChange(value)) + "M";
		priceT.text = "$" + UIManager.UnitChange(price);
		
		gold = PlayerPrefs.GetInt ("gold", 0);

		UIManager.Instance.goldT.text = UIManager.UnitChange(PlayerPrefs.GetInt ("foreGold", PlayerPrefs.GetInt("gold",0)));	
		UIManager.Instance.goldT.DOText (UIManager.UnitChange (gold), 1f, false, ScrambleMode.Numerals, null).SetDelay(delay);
	}

	public void OnDepthClick(){
		gold = PlayerPrefs.GetInt ("gold", 0);
		if (gold > price) {
			MultiHaptic.HapticMedium ();
			gold -= price;
			UIManager.Instance.diveDepth -= 7;
			price = (int)(price*1.35f);

			if (PlayerPrefs.GetInt ("Level", 1) == 1) {
				PlayerPrefs.SetInt ("valueDepth", UIManager.Instance.diveDepth);
				PlayerPrefs.SetInt ("priceDepth", price);
			} else if (PlayerPrefs.GetInt ("Level", 1) == 2) {
				PlayerPrefs.SetInt ("valueDepth2", UIManager.Instance.diveDepth);
				PlayerPrefs.SetInt ("priceDepth2", price);
			}else if (PlayerPrefs.GetInt ("Level", 1) == 3) {
				PlayerPrefs.SetInt ("valueDepth3", UIManager.Instance.diveDepth);
				PlayerPrefs.SetInt ("priceDepth3", price);
			}

			PlayerPrefs.SetInt ("gold", gold);
			UpdateData (0);
			CheckGold ();
			UpgradingOffline.Instance.CheckGold ();
		}
	}

	public void CheckGold(){
		if (gold >= price) {
			transform.GetComponent<Button> ().interactable = true;
			transform.GetComponent<Image> ().color = new Color (1, 1, 1, 1);

		} else {
			transform.GetComponent<Button> ().interactable = false;
			transform.GetComponent<Image> ().color = new Color (200 / 255f, 200 / 255f, 200 / 255f, 0.5f);
		
		}
	}

}
