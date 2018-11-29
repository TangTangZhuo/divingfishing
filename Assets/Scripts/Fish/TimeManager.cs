using System.Collections;
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

    bool offlineShowing = false;

    int messageCount;
	void Awake(){
		StartCoroutine (GetNetWorkTime ());
		messageCount = 0;
		long gold = long.Parse( PlayerPrefs.GetString ("gold", "0"))/2;
		UIManager.Instance.goldT.text = UIManager.UnitChange (gold);

	}

	void Start()
	{		
		if (PlayerPrefs.GetInt ("quitGame", 0) == 1) {
			UpdateGold ();
		}
		flyGold = (FlyGold)UnityEngine.Object.FindObjectOfType (typeof(FlyGold));
        if (!offlineShowing && PlayerPrefs.GetInt("TurnGuideFinish", 0) == 1)
        {
            IPAManager.Instance.AutoPopVIP();
        }
    }

    void OnApplicationQuit()
	{
		//Savee the current system time as a string in the player prefs class
		PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
		PlayerPrefs.SetInt ("quitGame", 1);
		PlayerPrefs.SetString ("foreGold", PlayerPrefs.GetString("gold","0"));	
		//PlayerPrefs.SetInt ("fishingpass", 0);

		PlayerPrefs.SetFloat ("turnMuti", 1);

		PlayerPrefs.SetInt ("EnterGame", 1);

        PlayerPrefs.SetInt("TurnTip", 0);

        if (PlayerPrefs.GetInt("TurnGuide", 0) == 1)
        {
            PlayerPrefs.SetInt("TurnGuideFinish", 1);
        }
    }

    void OnApplicationPause(bool isPause){
		if (isPause) {
			PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
			PlayerPrefs.SetInt ("quitGame", 1);
           // PlayerPrefs.SetInt("EnterGame", 1);
            PlayerPrefs.SetInt("TurnTip", 0);
        } else {
			UpdateGold ();
            if (!offlineShowing && PlayerPrefs.GetInt("TurnGuideFinish", 0) == 1)
            {
                IPAManager.Instance.AutoPopVIP();
            }
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
				
				if (PlayerPrefs.GetInt ("fishingpass", 0) == 1) {
					MessageBox.Show ("OFFLINE", "$" + UIManager.UnitChange ((long)(min * long.Parse( PlayerPrefs.GetString ("valueOffline", "40"))*(1+goldMutiple))));
                    offlineShowing = true;
                }
				if (PlayerPrefs.GetInt ("fishingpass", 0) == 0) {
					MessageBox.Show ("OFFLINE", "$" + UIManager.UnitChange (min * long.Parse( PlayerPrefs.GetString ("valueOffline", "40"))));
                    offlineShowing = true;
                }
				ChangeUIWithVip (GameObject.Find ("PopBG(Clone)").transform, min);

				PlayerPrefs.SetInt ("offlineOnClick", 1);
				messageCount++;

				MessageBox.confim = () => {
					TGSDK.ReportAdRejected(TGSDKManager.doubleID);
					long gold = long.Parse( PlayerPrefs.GetString ("gold", "0")) + (long)(min * long.Parse( PlayerPrefs.GetString ("valueOffline", "40"))*(1+goldMutiple));
					OnMessageBoxBtn(gold);
					PlayerPrefs.SetInt ("quitGame", 0);
                    IPAManager.Instance.AutoPopVIP();
                };
                MessageBox.doubleR = () => {
					TGSDK.ShowAdScene(TGSDKManager.doubleID);

					if (TGSDK.CouldShowAd(TGSDKManager.doubleID)) {
						TGSDK.ShowAd(TGSDKManager.doubleID);
					}
						

					doubleTrans.DOScale(1,2).OnComplete(()=>{
						GameObject adPop =Instantiate ((GameObject)Resources.Load("ADPopBG"),GameObject.Find("Canvas").transform); 

						long gold = long.Parse( PlayerPrefs.GetString ("gold", "0")) + (long)(min * long.Parse( PlayerPrefs.GetString ("valueOffline", "40"))*2*(1+goldMutiple));


						Button btn = adPop.transform.Find("sure").GetComponent<Button>();
						adPop.transform.Find("content").GetComponent<Text>().text ="$" + ((long)(((gold-long.Parse( PlayerPrefs.GetString ("gold", "0")))/(2*(1+goldMutiple)))*(1+goldMutiple))).ToString();
						btn.onClick.AddListener(()=>{
							OnMessageBoxBtn(gold);
							PlayerPrefs.SetInt ("quitGame", 0);
							ProgressManager.Instance.GameWin ();
						});

//						int gold = PlayerPrefs.GetInt ("gold", 0) + (int)(min * PlayerPrefs.GetInt ("valueOffline", 40)*2*(1+goldMutiple));
//						OnMessageBoxBtn(gold);
//						PlayerPrefs.SetInt ("quitGame", 0);

					});
                    IPAManager.Instance.AutoPopVIP();
                };
			}
		}
	}

	void OnMessageBoxBtn(long gold){
		PlayerPrefs.SetString ("gold", gold.ToString());
		flyGold.FlyGoldGenerate (flyGold.targetPos);

		UIManager.Instance.goldT.DOText (UIManager.UnitChange (gold), 1f, false, ScrambleMode.Numerals, null).SetDelay (1);
		Upgrading.Instance.CheckGold (gold);
		UpgradingOffline.Instance.CheckGold (gold);
		//PlayerPrefs.SetString ("sysString", System.DateTime.Now.ToBinary ().ToString ());	
		PlayerPrefs.SetInt ("offlineOnClick", 2);
		messageCount=0;
	}

	void VipReward(){
		GameObject popBG = (GameObject)Resources.Load ("PopBG");
		Transform popTrans = popBG.transform;
		GameObject passVip = popBG.transform.Find ("PassVip").gameObject;
		GameObject doubleImage = popBG.transform.Find ("GoldDouble").gameObject;
		GameObject extra = popBG.transform.Find ("extra").gameObject;
		doubleImage.SetActive (false);
		passVip.SetActive (true);
		if (PlayerPrefs.GetInt ("fishingpass", 0) == 1) {
			passVip.SetActive (false);
			extra.SetActive (true);
		}
	}

	private IEnumerator GetNetWorkTime(){
		WWW req = new WWW ("http://www.hko.gov.hk/cgi-bin/gts/time5a.pr?a=2");
		yield return req;

		if (req.error == null) {
			int lastDay = PlayerPrefs.GetInt ("DayOfYear", 0);
			string timeStamp = req.text.Split ('=') [1].Substring (0, 10); 
			DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime (new DateTime (1970, 1, 1));
			long lTime = long.Parse (timeStamp + "0000000");
			TimeSpan toNow = new TimeSpan (lTime);
			DateTime now = dtStart.Add (toNow);
			Debug.Log ("now.DayOfYear - lastDay"+(now.DayOfYear - lastDay));
			if (now.DayOfYear - lastDay> 0) {
				PlayerPrefs.SetInt ("DayOfYear", now.DayOfYear);
				PlayerPrefs.SetInt ("NewDay", 1);
			}

		}
		else {
			yield return new WaitForSeconds(300);

			StartCoroutine (GetNetWorkTime ());
		}

	}

	void ChangeUIWithVip(Transform popBG,int min){
		if (PlayerPrefs.GetInt ("fishingpass", 0) == 1) {
			popBG.Find ("content").localPosition -= new Vector3 (0, 150, 0);
			popBG.Find ("sure").localPosition -= new Vector3 (0, 150, 0);
			popBG.Find ("double").localPosition -= new Vector3 (0, 150, 0);
			GameObject lineGold =Instantiate ((GameObject)Resources.Load("LineGold"),popBG); 
			lineGold.GetComponent<Text> ().text = "$" + UIManager.UnitChange (min * PlayerPrefs.GetInt ("valueOffline", 40));
			//popBG.Find ("content").GetComponent<Text> ().text = "$" + UIManager.UnitChange (goldSum*2);

		}
		if (PlayerPrefs.GetInt ("fishingpass", 0) == 0) {
			return;
		}
	}

}
