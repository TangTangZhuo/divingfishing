using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Upgrading : MonoBehaviour {
	private Text valueT;
	private Text priceT;

	private int value;
	private long price;
	private long gold;

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
		CheckGold (gold);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateData(float delay){
		
		if (PlayerPrefs.GetInt ("Level", 1) == 1) {
			UIManager.Instance.diveDepth = PlayerPrefs.GetInt ("valueDepth", -17);
			value = PlayerPrefs.GetInt ("valueDepth", UIManager.Instance.diveDepth);
			price = long.Parse( PlayerPrefs.GetString ("priceDepth", "2810"));
		}else if (PlayerPrefs.GetInt ("Level", 1) == 2) {
			UIManager.Instance.diveDepth = PlayerPrefs.GetInt ("valueDepth2", -17);
			value = PlayerPrefs.GetInt ("valueDepth2", UIManager.Instance.diveDepth);
			price = long.Parse( PlayerPrefs.GetString ("priceDepth2", "1240000"));
		}else if (PlayerPrefs.GetInt ("Level", 1) == 3) {
			UIManager.Instance.diveDepth = PlayerPrefs.GetInt ("valueDepth3", -17);
			value = PlayerPrefs.GetInt ("valueDepth3", UIManager.Instance.diveDepth);
			price = long.Parse( PlayerPrefs.GetString ("priceDepth3", "31400000"));
		}

		valueT.text = (UIManager.UnitChange(value)) + "M";
		priceT.text = "$" + UIManager.UnitChange(price);
		
		gold = long.Parse( PlayerPrefs.GetString ("gold", "0"));

		UIManager.Instance.goldT.text = UIManager.UnitChange(long.Parse( PlayerPrefs.GetString ("foreGold",PlayerPrefs.GetString("gold","0"))));	
		UIManager.Instance.goldT.DOText (UIManager.UnitChange (gold), 1f, false, ScrambleMode.Numerals, null).SetDelay(delay);
	}

	public void OnDepthClick(){
		gold = long.Parse( PlayerPrefs.GetString ("gold", "0"));
		if (gold > price) {
			MultiHaptic.HapticMedium ();
			gold -= price;
			UIManager.Instance.diveDepth -= 7;
			price = (long)(price*1.35f);

			if (PlayerPrefs.GetInt ("Level", 1) == 1) {
				PlayerPrefs.SetInt ("valueDepth", UIManager.Instance.diveDepth);
				PlayerPrefs.SetString ("priceDepth", price.ToString());
			} else if (PlayerPrefs.GetInt ("Level", 1) == 2) {
				PlayerPrefs.SetInt ("valueDepth2", UIManager.Instance.diveDepth);
				PlayerPrefs.SetString ("priceDepth2", price.ToString());
			}else if (PlayerPrefs.GetInt ("Level", 1) == 3) {
				PlayerPrefs.SetInt ("valueDepth3", UIManager.Instance.diveDepth);
				PlayerPrefs.SetString ("priceDepth3", price.ToString());
			}

			PlayerPrefs.SetString ("gold", gold.ToString());
			UpdateData (0);
			CheckGold (gold);
			UpgradingOffline.Instance.CheckGold (gold);

		}
	}

	public void CheckGold(long curGold){
		if (curGold >= price) {
			transform.GetComponent<Button> ().interactable = true;
			transform.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
            if(PlayerPrefs.GetInt("DepthGuide", 0)==0){
                NormalGuide.Instance.guide[0].SetActive(true);
                PlayerPrefs.SetInt("DepthGuide", 1);
            }
		} else {
			transform.GetComponent<Button> ().interactable = false;
			transform.GetComponent<Image> ().color = new Color (200 / 255f, 200 / 255f, 200 / 255f, 0.5f);
		}
	}

}
