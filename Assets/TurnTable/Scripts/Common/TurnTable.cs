using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Together;

public class TurnTable : MonoBehaviour {



	//获取转盘旋转脚本
	public Rotation rotation;
	//获取指针检测脚本
	public Needle needle;
	//获取转转盘所需金币
	public Text goldText;
	//获取转盘元素集合
	public Transform turnTable;
	//获取按钮
	public GameObject turnBtn;
	public GameObject backBtn;
    //获取确认弹窗
    public GameObject turnClam;
    //获取持续弹窗
    public GameObject turnContinue;

	//获取倍数显示
	public Text multiText;
	//获取时间显示
	public Text countDown;


	//本局获得金币数
	int curGold = 0;
	//所持有的金币数
	int holdGold = 0;

	//转盘是否结束旋转
	bool isFinish = false;

	//转盘元素倍数
	float[] multiple = new float[]{1.4f,2f,1.2f,1.1f,1.5f,1.3f};
	//转盘元素金币数
	int[] golds;

	//初始化
	void OnEnable () {		
		
		rotation.RotationFinish +=()=>{ 
			RotateFinish ();
		};
		needle.CheckNeedleCallBack += (Collider2D coll) => {
			CheckNeedle(coll);
		};
			
//		Timer.Instance.TurnTimeFinish += () => {
//			PlayerPrefs.SetFloat ("turnMuti", 1);
//			UpdateBoost();
//		};
//		InitTable (curGold);
	}

	//更新转盘元素
	void InitTable(int gold){
//		golds = new int[multiple.Length];
//		for (int i = 0; i < multiple.Length; i++) {
//			turnTable.GetChild (i).Find ("Text").GetComponent<Text> ().text = ((int)(gold * multiple [i])).ToString();
//			golds [i] = (int)(gold * multiple [i]);
//		}
	}

	//点击广告旋转按钮
	public void OnTurnBtn(){
		bool completeAD = false;
		if (TGSDK.CouldShowAd (TGSDKManager.turnID)) {
			TGSDK.ShowAd (TGSDKManager.turnID);

			turnBtn.SetActive (false);
			backBtn.SetActive (false);

			TGSDK.AdCloseCallback = (string obj) => {
				if(completeAD){
					rotation.RotateThis();

					if (PlayerPrefs.GetInt ("FreeRward", 0) < 10) {
						PlayerPrefs.SetInt ("FreeRward", PlayerPrefs.GetInt ("FreeRward", 0) + 1);
					}

				}
			};
			TGSDK.AdCompleteCallback = (string obj) => {
				completeAD = true;
			};
			TGSDK.AdRewardFailedCallback = (string obj) => {
				OnBackBtn();
			};
			TGSDK.AdShowFailedCallback = (string obj) => {
				OnBackBtn();
			};

		} else {
			TipPop.GenerateTip ("no ads", 0.5f);
		}

//		rotation.RotateThis();
//		turnBtn.SetActive (false);
//		backBtn.SetActive (false);
	}

    //延长转盘奖励时间
    public void OnContinueBtn(){
        bool completeAD = false;
        if (TGSDK.CouldShowAd(TGSDKManager.turnID))
        {
            gameObject.SetActive(true);
            turnContinue.SetActive(false);

            TGSDK.ShowAd(TGSDKManager.turnID);

            turnBtn.SetActive(false);
            backBtn.SetActive(false);

            TGSDK.AdCloseCallback = (string obj) => {
                if (completeAD)
                {
                    rotation.RotateThis();

                    if (PlayerPrefs.GetInt("FreeRward", 0) < 10)
                    {
                        PlayerPrefs.SetInt("FreeRward", PlayerPrefs.GetInt("FreeRward", 0) + 1);
                    }

                }
            };
            TGSDK.AdCompleteCallback = (string obj) => {
                completeAD = true;
            };
            TGSDK.AdRewardFailedCallback = (string obj) => {
                OnBackBtn();
            };
            TGSDK.AdShowFailedCallback = (string obj) => {
                OnBackBtn();
            };

        }
        else
        {
            TipPop.GenerateTip("no ads", 0.5f,Color.gray);
        }
        //gameObject.SetActive(true);
        //turnContinue.SetActive(false);
        //rotation.RotateThis();
    }

    //点击金币旋转按钮
    public void OnGoldBtn(){
//		if (holdGold >= curGold) {
//			PlayerPrefs.SetInt ("gold", holdGold - curGold);
//			rotation.RotateThis ();
//			HideBtn ();
//		} else {
//			TipPop.GenerateTip ("not enough money", 0.5f);
//		}
	}

	//点击返回按钮
	public void OnBackBtn(){
		turnBtn.SetActive (true);
		backBtn.SetActive (true);
		gameObject.SetActive (false);

	//	HideBtn ();
	//	PlayerControllerSky.GameEnd ();
	}

	//转盘结束回调
	void RotateFinish(){
		isFinish = true;
	}

	//转盘结束时获得对应奖励
	void CheckNeedle(Collider2D coll){
		if (isFinish) {
			if (coll.name.StartsWith ("item")) {
				int index = int.Parse(coll.name.Split (new char[]{ 'm' }) [1]);
				PlayerPrefs.SetFloat ("turnMuti", multiple[index]);
				SubmarineController.Instance.turnMulti = multiple [index];
				UpdateBoost ();

				isFinish = false;

                turnClam.SetActive(true);
                turnClam.transform.Find("Extra").Find("ExtraText").GetComponent<Text>().text = multiple[index] * 100 + "%";
                turnClam.transform.Find("Confirm").GetComponent<Button>().onClick.AddListener(() => {
                    turnClam.SetActive(false);
                    OnBackBtn();
                });
				//Invoke ("OnBackBtn", 0.5f);

				Timer.Instance.StartCountDownTurn (600);
                PlayerPrefs.SetInt("TurnTip", 1);
            }
            else{
				rotation.RotateLittle ();
			}

		}
	}

	public void UpdateBoost(){
		float m = PlayerPrefs.GetFloat ("turnMuti", 1);
		if (m != 1) {
			multiText.transform.parent.gameObject.SetActive (true);
			multiText.text = m * 100 + "%";
		} else {
			multiText.transform.parent.gameObject.SetActive (false);
			countDown.text = "Boost?";
		}
	}

}
