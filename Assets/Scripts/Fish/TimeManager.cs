﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using Together;
using UnityEngine.UI;
using DG.Tweening;

public class TimeManager : MonoBehaviour {

	DateTime currentDate;
	DateTime oldDate;
	FlyGold flyGold;

	int messageCount;
	void Awake(){
		messageCount = 0;
		int gold = PlayerPrefs.GetInt ("gold", 0)/2;
		if(gold>=1000000){
			if (gold / 1000 > 1000) {
				UIManager.Instance.goldT.text = (gold / 1000).ToString ().Insert ((gold / 1000000).ToString ().Length, ",") + "K";
			} else {
				UIManager.Instance.goldT.text = gold/1000+"K";
			}
		}

	}

	void Start()
	{		
		if (PlayerPrefs.GetInt ("quitGame", 0) == 1) {
			UpdateGold ();
		}
		flyGold = (FlyGold)UnityEngine.Object.FindObjectOfType (typeof(FlyGold));
	}

	void OnApplicationQuit()
	{
		//Savee the current system time as a string in the player prefs class
		PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
		PlayerPrefs.SetInt ("quitGame", 1);
		PlayerPrefs.SetInt ("foreGold", PlayerPrefs.GetInt("gold",0));	
	}

	void OnApplicationPause(bool isPause){
		if (isPause) {
			PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
			PlayerPrefs.SetInt ("quitGame", 1);
		} else {
			UpdateGold ();
		}
	}

	int OfflineTime(){
		//Store the current time when it starts
		currentDate = System.DateTime.Now;

		//Grab the old time from the player prefs as a long
		long temp = Convert.ToInt64(PlayerPrefs.GetString("sysString",currentDate.ToBinary().ToString()));

		//Convert the old time from binary to a DataTime variable
		DateTime oldDate = DateTime.FromBinary(temp);

		//Use the Subtract method and store the result as a timespan variable
		TimeSpan difference = currentDate.Subtract(oldDate);

		return difference.Minutes;
	}

	public void UpdateGold(){
		if (messageCount == 0) {
			int min = OfflineTime ();
			if (min > 0) {
				float goldMutiple = 1;
				if (PlayerPrefs.GetInt ("fishingpass", 0) == 1) {
					goldMutiple = 0.2f;
				}
				VipReward ();
				GameObject popBG = (GameObject)Resources.Load("PopBG");
				Transform doubleTrans = popBG.transform.Find ("double");
				doubleTrans.GetComponentInChildren<Text>().text = "Bonus×2";
				if (!TGSDK.CouldShowAd (TGSDKManager.doubleID)) {
					doubleTrans.GetComponent<Button> ().interactable = false;
				} else {
					doubleTrans.GetComponent<Button> ().interactable = true;
				}
				if (MessageBox.Messagebox != null)
					return;
				MessageBox.Show ("OFFLINE", "$" + UIManager.UnitChange(min * PlayerPrefs.GetInt ("valueOffline", 40)));
				PlayerPrefs.SetInt ("offlineOnClick", 1);
				messageCount++;

				MessageBox.confim = () => {
					TGSDK.ReportAdRejected(TGSDKManager.doubleID);
					int gold = PlayerPrefs.GetInt ("gold", 0) + (int)(min * PlayerPrefs.GetInt ("valueOffline", 40)*(1+goldMutiple));
					OnMessageBoxBtn(gold);
					PlayerPrefs.SetInt ("quitGame", 0);
				};
				MessageBox.doubleR = () => {
					TGSDK.ShowAdScene(TGSDKManager.doubleID);

					if (TGSDK.CouldShowAd(TGSDKManager.doubleID)) {
						TGSDK.ShowAd(TGSDKManager.doubleID);
					}
						

					doubleTrans.DOScale(1,2).OnComplete(()=>{
						GameObject adPop =Instantiate ((GameObject)Resources.Load("ADPopBG"),GameObject.Find("Canvas").transform); 

						int gold = PlayerPrefs.GetInt ("gold", 0) + (int)(min * PlayerPrefs.GetInt ("valueOffline", 40)*2*(1+goldMutiple));


						Button btn = adPop.transform.Find("sure").GetComponent<Button>();
						adPop.transform.Find("content").GetComponent<Text>().text ="$" + ((gold-PlayerPrefs.GetInt ("gold", 0))/(2*(1+goldMutiple))).ToString();
						btn.onClick.AddListener(()=>{
							OnMessageBoxBtn(gold);
							PlayerPrefs.SetInt ("quitGame", 0);
							ProgressManager.Instance.GameWin ();
						});

//						int gold = PlayerPrefs.GetInt ("gold", 0) + (int)(min * PlayerPrefs.GetInt ("valueOffline", 40)*2*(1+goldMutiple));
//						OnMessageBoxBtn(gold);
//						PlayerPrefs.SetInt ("quitGame", 0);

					});

				};
			}
		}
	}

	void OnMessageBoxBtn(int gold){
		PlayerPrefs.SetInt ("gold", gold);
		flyGold.FlyGoldGenerate ();

		UIManager.Instance.goldT.DOText (UIManager.UnitChange (gold), 1f, false, ScrambleMode.Numerals, null).SetDelay (1);
		Upgrading.Instance.CheckGold ();
		UpgradingOffline.Instance.CheckGold ();
		//PlayerPrefs.SetString ("sysString", System.DateTime.Now.ToBinary ().ToString ());	
		PlayerPrefs.SetInt ("offlineOnClick", 2);
		messageCount=0;
	}

	void VipReward(){
//		GameObject popBG = (GameObject)Resources.Load ("PopBG");
//		Transform popTrans = popBG.transform;
//		GameObject passVip = popBG.transform.Find ("PassVip").gameObject;
//		GameObject doubleImage = popBG.transform.Find ("GoldDouble").gameObject;
//		GameObject extra = popBG.transform.Find ("extra").gameObject;
//		doubleImage.SetActive (false);
//		passVip.SetActive (true);
//		if (PlayerPrefs.GetInt ("fishingpass", 0) == 1) {
//			passVip.SetActive (false);
//			extra.SetActive (true);
//		}
	}
}
