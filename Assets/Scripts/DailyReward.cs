using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//using Together;

public class DailyReward : MonoBehaviour {

	Button passBtn;
	Button freeBtn;
	IPAManager iPAManager;
	FlyGold flyGold;

    //看广告次数
    int freeRward = 0;

    // Use this for initialization
    void Start () {
		iPAManager = transform.parent.GetComponent<IPAManager> ();
		flyGold = (FlyGold)UnityEngine.Object.FindObjectOfType (typeof(FlyGold));
        freeRward = PlayerPrefs.GetInt("FreeRward", 0);
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


    public void GetFreeDailyReward()
	{	
        freeRward = PlayerPrefs.GetInt("FreeRward", 0);
        if (freeRward >= 10)
        {
            PlayerPrefs.SetInt("FreeRward", 0);
            PlayerPrefs.SetInt("NewDay", 0);
            ClamReward();
        }else{
//            if (TGSDK.CouldShowAd(TGSDKManager.DailyID))
//            {
//				AudioListener.pause = true;
//                TGSDK.ShowAd(TGSDKManager.DailyID);
//				bool adReward = false;
//				TGSDK.AdCloseCallback = (string msg) =>
//                {
//					AudioListener.pause = false;
//					if(adReward){
//                   		PlayerPrefs.SetInt("FreeRward", PlayerPrefs.GetInt("FreeRward", 0) + 1);
//                    	UpdateDailyState();
//					}
//                };
//				TGSDK.AdRewardSuccessCallback = (string obj) => {
//					adReward = true;
//				};
//                
//            }
			TTADManager.Instance.Ad_Button_Click("DailyReward");
			if(TTADManager.Instance.couldShow){
				TTADManager.Instance.Ad_Show_Event("DailyReward");
				AudioListener.pause = true;
				TTADManager.Instance.AutoShowReward ();
				TTADManager.Instance.CheckRewardEvent();
				TTADManager.Instance.RewardFinish += () => {
					TTADManager.Instance.Ad_View("DailyReward");
					AudioListener.pause = false;
					PlayerPrefs.SetInt("FreeRward", PlayerPrefs.GetInt("FreeRward", 0) + 1);
					UpdateDailyState();
				};
			}
			else{
				if (Application.systemLanguage == SystemLanguage.English) {
					TipPop.GenerateTip("no ads", 0.5f);
				} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
					TipPop.GenerateTip("广告不可播放", 0.5f);
				}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
					TipPop.GenerateTip("廣告不可播放", 0.5f);
				}
            }
        }
	}

	void ClamReward(){
		iPAManager.OnDailyBackBtn ();
		iPAManager.UpdateDailyState ();
		long gold = long.Parse( PlayerPrefs.GetString ("gold", "0")) + 1000000;
		PlayerPrefs.SetString ("gold", gold.ToString());
		TTADManager.Instance.Get_Coins (UIManager.UnitChange(1000000), "DailyReward");
		flyGold.FlyGoldGenerate (flyGold.targetPos);
		UIManager.Instance.goldT.DOText (UIManager.UnitChange (gold), 1f, false, ScrambleMode.Numerals, null).SetDelay (1);
		Upgrading.Instance.CheckGold (gold);
		UpgradingOffline.Instance.CheckGold (gold);
	}

	public void UpdateDailyState(){
		
		passBtn = transform.Find("BG").Find("Pass").GetComponent<Button>();
		freeBtn = transform.Find("BG").Find("Free").GetComponent<Button>();

		freeRward = PlayerPrefs.GetInt ("FreeRward", 0);

		freeBtn.transform.Find("Text").GetComponent<Text>().text = freeRward+"/10";

		if (freeRward == 10) {			
			if (Application.systemLanguage == SystemLanguage.English) {
				freeBtn.transform.Find ("Text").GetComponent<Text> ().text = "CLAIM";
			} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
				freeBtn.transform.Find ("Text").GetComponent<Text> ().text = "领取";
			}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
				freeBtn.transform.Find ("Text").GetComponent<Text> ().text = "領取";
			}
		}
		//if (freeRward < 5) {
		//	freeBtn.interactable = false;
		//}
		//if (freeRward >= 5) {
		//	freeBtn.interactable = true;
		//}
	}
}
