using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;
using Together;

public class SubmarineController : MonoBehaviour {
	public float moveSpeed;
	[HideInInspector]
	public float maxSpeed = 0;
	Rigidbody2D playerRig;
	[HideInInspector]
	public float gravityScale;
	public Transform netParent;
	public Transform boundL;
	public Transform boundR;
	public Text score;
	public Text specialScore;
	public Transform scoreParent;
	public Slider progressSlider;
	public GameObject settleView;
	//史诗鱼拍立得
	public GameObject epicPop;
	//重生倍数
	[HideInInspector]
	public float rebirthMulti = 1;

	[HideInInspector]
	public float force;

	//转盘倍数
	[HideInInspector]
	public float turnMulti = 1;

	//发现新的史诗鱼
    [HideInInspector]
    public List<string> epicFish;
    //结算弹窗
    public GameObject settlePop;

	float time;
	[HideInInspector]
	public bool isSettle;
	int fishIndex;
	long goldSum;
	int settleCount;
	long curAccumulation = 0;
	float settleTime;
	//FlyGold flyGod;
	[HideInInspector]
	int goldMultiple = 1;

	////重生////
	//重生等级
	int rebirthLvl = 1;
	//当前重生经验
	int curRebirthExp = 0;
	//升级所需经验
	int rebirthExp = 0;


	public Dictionary<string,int> fishDic = new Dictionary<string, int>();
	Dictionary<string,int> expDic = new Dictionary<string, int>();

	private static SubmarineController instance;
	public static SubmarineController Instance{
		get{return instance;}
	}

	void Awake(){
		instance = this;

		//PlayerPrefs.DeleteAll ();
		//PlayerPrefs.SetString ("gold", "1999999999999");

	}

	// Use this for initialization
	void Start () {
		settleCount = 0;
		settleTime = 0;
		time = 0;
		isSettle = false;
		fishIndex = 0;
		playerRig = GetComponent<Rigidbody2D> ();

		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
			maxSpeed = 0.3f;
			moveSpeed = maxSpeed;
		} else {
			maxSpeed = 10;
			moveSpeed = maxSpeed;
		}
		playerRig.gravityScale = 0;
		InitFishDic ();
		InitExpDic ();
		curRebirthExp = PlayerPrefs.GetInt ("curRebirthExp", 0);
		goldSum = 0;
		InitProgressSlider ();
		UpdateGoldMutiple ();
//		flyGod = (FlyGold)Object.FindObjectOfType(typeof(FlyGold));

		epicFish = new List<string>();

		rebirthMulti = 1+PlayerPrefs.GetInt ("star", 0)*(PlayerPrefs.GetInt ("rebirthLevel", 1)-1)/100f;
		turnMulti = PlayerPrefs.GetFloat ("turnMuti", 1);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (playerRig.gravityScale != gravityScale) {
			playerRig.gravityScale = gravityScale;
		}
		if (ProgressManager.Instance.isRunning) {			
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
					if (Input.GetTouch (0).deltaPosition.x > 0) {
						transform.rotation = Quaternion.Euler (0, 0, 0);
					}
					if (Input.GetTouch (0).deltaPosition.x < 0) {
						transform.rotation = Quaternion.Euler (0, 180, 0);
					}
					transform.Translate (new Vector3 (Input.GetTouch (0).deltaPosition.x * moveSpeed * Time.deltaTime, 0, 0), Space.World);
				}
			} else {

				if (Input.GetKey (KeyCode.A)) {		
					transform.rotation = Quaternion.Euler (0, 180, 0);
					transform.Translate (new Vector3 (-moveSpeed * Time.deltaTime, 0, 0), Space.World);			
				} else if (Input.GetKey (KeyCode.D)) {
					transform.rotation = Quaternion.Euler (0, 0, 0);
					transform.Translate (new Vector3 (moveSpeed * Time.deltaTime, 0, 0), Space.World);
				}
					
			}
			progressSlider.value = transform.position.y;
		}
		if (isSettle) {	
			PlayerPrefs.SetString ("foreGold", PlayerPrefs.GetString("gold","0"));	
			if (PlayerPrefs.GetInt ("golden_net", 0) == 1) {
				HidePopUI (false);
			} else {
				HidePopUI (true);
			}
			time += Time.deltaTime;
			if (time >= settleTime) {
				if (fishIndex < settleCount) {
					Transform fish = netParent.GetChild (fishIndex);
					Settlement (fish, 0.3f);
					string fishName = fish.name.Split (new char[]{ '(' }) [0];
					if (PlayerPrefs.GetInt (fishName, 0)==0) {
						
						//添加新史诗鱼
						if(fishName.StartsWith("unusual")){
							epicFish.Add (fishName);
							PlayerPrefs.SetInt ("star", PlayerPrefs.GetInt ("star", 0) + 1);
						}

						PlayerPrefs.SetInt ("illNew", 1);
					}
					PlayerPrefs.SetInt (fish.name.Split (new char[]{'('}) [0], 1);
					ScoreGenerate (fish,settleTime);

				}
				else if(fishIndex == settleCount){
					Transform fish = netParent.GetChild (fishIndex);
					fish.DOMoveY (fish.position.y + 1.3f, 0.3f, false);
					fish.DOScale (1, 0.3f).OnComplete(()=>{
						fish.GetComponent<SpriteRenderer> ().DOFade (0f, 0.3f);
						if(fish.childCount>0){
							Destroy(fish.GetChild(0).gameObject);
						}
						isSettle = false;

						GameObject popBG = (GameObject)Resources.Load("PopBG");
						Transform doubleTrans = popBG.transform.Find("double");
						doubleTrans.GetComponentInChildren<Text>().text = "Bonus×2";

						if(PlayerPrefs.GetInt("double",0)>=2){
							

							//doubleTrans.DOPunchRotation(new Vector3(100,100,100),1,10,1);
							//MessageBox.Messagebox.transform.Find("double").DOPunchRotation(new Vector3(1,1,1),1,10,1);
							int levelIndex = PlayerPrefs.GetInt ("Level", 1);
							if (levelIndex == 1) {								
								doubleTrans.GetComponentInChildren<Text>().text = "Bonus×3";
							}
							if (levelIndex == 2) {
								doubleTrans.GetComponentInChildren<Text>().text = "Bonus×4";
							}
							if (levelIndex == 3) {
								doubleTrans.GetComponentInChildren<Text>().text = "Bonus×5";
							}
						}
						//UpdateGoldMutiple ();

						string doubleText = doubleTrans.GetComponentInChildren<Text>().text;
						if(doubleText == "Bonus×2"){
							if (!TGSDK.CouldShowAd (TGSDKManager.doubleID)) {
								doubleTrans.GetComponent<Button> ().interactable = false;
							} else {
								doubleTrans.GetComponent<Button> ().interactable = true;
							}
						}else{
							if (!TGSDK.CouldShowAd (TGSDKManager.tripleID)) {
								doubleTrans.GetComponent<Button> ().interactable = false;
							} else {
								doubleTrans.GetComponent<Button> ().interactable = true;
							}
						}

						//弹出结算窗口
						if (PlayerPrefs.GetInt ("golden_net", 0) == 1) {
							MessageBox.Show("You Earend","$"+ UIManager.UnitChange(goldSum*2));
						}
						if (PlayerPrefs.GetInt ("golden_net", 0) == 0) {
							MessageBox.Show("You Earend","$"+ UIManager.UnitChange(goldSum));
						}
						ChangeUIWithGoldNet(GameObject.Find("PopBG(Clone)").transform);

                       
                        StartCoroutine( FindEpicFish (1));

						if(PlayerPrefs.GetInt("double",0)>=2){							
							Transform doubleTrans1 = GameObject.Find("PopBG(Clone)").transform.Find("double");
							if(doubleTrans1!=null){
								doubleTrans1.DOPunchRotation(new Vector3(0,0,5),1,5,1).SetLoops(100);
							}
						}

						MessageBox.confim =()=>{							
							FBLogWithLevel();

                            //如果倒计时结束则播放插屏，（无VIP和Noads)
                            if (PlayerPrefs.GetInt("fishingpass",0)==0 || PlayerPrefs.GetInt("no_ads", 0) == 0)
                            {
                                if (PlayerPrefs.GetInt("ForceReady", 0) == 1)
                                {
                                    if (TGSDK.CouldShowAd(TGSDKManager.forceID))
                                    {
                                        TGSDK.ShowAd(TGSDKManager.forceID);
                                        TGSDK.AdCloseCallback = (string obj) =>
                                        {
                                            PlayerPrefs.SetInt("ForceReady", 0);
                                            Timer.Instance.StartCountDownForce(45);
                                            PlayerPrefs.SetInt("PopNoAds", 1);
                                        };
                                    }
                                    else
                                    {
                                        Debug.Log("can't play forceAD");
                                    }

                                }
                            }


							long gold =long.Parse( PlayerPrefs.GetString ("gold", "0")) + goldSum*goldMultiple;

							curAccumulation = goldSum*goldMultiple;
							PlayerPrefs.SetString ("accumulation", (long.Parse( PlayerPrefs.GetString ("accumulation", "0"))+curAccumulation).ToString());

							PlayerPrefs.SetString ("gold", gold.ToString());
							Upgrading.Instance.CheckGold(gold);
							UpgradingOffline.Instance.CheckGold(gold);

							//重生
							PlayerPrefs.SetInt ("curRebirthExp", curRebirthExp);
							rebirthLvl = PlayerPrefs.GetInt("exp",1);
							rebirthExp = rebirthLvl*2000;
							if(curRebirthExp>rebirthExp){
								PlayerPrefs.SetInt("exp",rebirthLvl+1);
								PlayerPrefs.SetInt ("curRebirthExp", 0);
							}

							ProgressManager.Instance.GameWin ();
							PlayerPrefs.SetInt ("double", PlayerPrefs.GetInt ("double", 0) + 1);
							PlayerPrefs.SetInt ("ClamGold", 1);
						};
						MessageBox.doubleR =()=>{	
							FBLogWithLevel();
							//GameObject popBG = (GameObject)Resources.Load("PopBG");
							//Transform doubleTrans = popBG.transform.Find("double");
																				
							if(doubleText == "Bonus×2"){
								if (TGSDK.CouldShowAd(TGSDKManager.doubleID)) {
									TGSDK.ShowAd(TGSDKManager.doubleID);
								}
							}else{
								if (TGSDK.CouldShowAd(TGSDKManager.tripleID)) {
									TGSDK.ShowAd(TGSDKManager.tripleID);
								}
							}
								


							doubleTrans.DOScale(1,2).OnComplete(()=>{
								GameObject adPop =Instantiate ((GameObject)Resources.Load("ADPopBG"),GameObject.Find("Canvas").transform); 


								string doubleName = doubleTrans.GetComponentInChildren<Text>().text;
								long gold = long.Parse( PlayerPrefs.GetString ("gold", "0"));
								goldSum*=goldMultiple;
								curAccumulation = goldSum;
								Button btn = adPop.transform.Find("sure").GetComponent<Button>();
								adPop.transform.Find("content").GetComponent<Text>().text ="$" + 0.ToString();
								btn.onClick.AddListener(()=>{
									PlayerPrefs.SetInt ("ClamGold", 1);
									PlayerPrefs.SetString ("gold", (gold+goldSum).ToString());
									PlayerPrefs.SetString ("accumulation", (long.Parse( PlayerPrefs.GetString ("accumulation", "0"))+curAccumulation).ToString());
									Upgrading.Instance.CheckGold(gold);
									UpgradingOffline.Instance.CheckGold(gold);
									ProgressManager.Instance.GameWin ();	
								});

								TGSDK.AdCompleteCallback = (string msg) => {
									Debug.Log("AdCompleteCallback");
									PlayerPrefs.SetInt("double",0);
									if (PlayerPrefs.GetInt ("FreeRward", 0) < 10) {
										PlayerPrefs.SetInt ("FreeRward", PlayerPrefs.GetInt ("FreeRward", 0) + 1);
									}
									if(doubleName == "Bonus×3"){
										gold = long.Parse( PlayerPrefs.GetString ("gold", "0")) + goldSum*3;
										curAccumulation = goldSum*3;
									}else if(doubleName == "Bonus×4"){
										gold = long.Parse( PlayerPrefs.GetString ("gold", "0")) + goldSum*4;
										curAccumulation = goldSum*4;
									}else if(doubleName == "Bonus×5"){
										gold = long.Parse( PlayerPrefs.GetString ("gold", "0")) + goldSum*5;
										curAccumulation = goldSum*5;
									}else if(doubleName == "Bonus×2"){
										gold = long.Parse( PlayerPrefs.GetString ("gold", "0")) + goldSum*2;
										curAccumulation = goldSum*2;
									}
									adPop.transform.Find("content").GetComponent<Text>().text ="$" + ((gold-long.Parse( PlayerPrefs.GetString ("gold", "0"))-goldSum)*2).ToString();
								};

							});
								


						//	};
						};

                        settlePop = GameObject.Find("PopBG(Clone)");
                        if (epicFish.Count != 0)
                        {
                            //隐藏结算界面
                            settlePop.SetActive(false);
                        }
                    });						
					string fishName = fish.name.Split (new char[]{ '(' }) [0];
					if (PlayerPrefs.GetInt (fishName, 0)==0) {
						//添加新史诗鱼
						if(fishName.StartsWith("unusual")){
							epicFish.Add (fishName);
							PlayerPrefs.SetInt ("star", PlayerPrefs.GetInt ("star", 0) + 1);
						}

						PlayerPrefs.SetInt ("illNew", 1);
					}
					PlayerPrefs.SetInt (fishName, 1);
					ScoreGenerate (fish,settleTime);



				}	
				MultiHaptic.HapticLight ();
				fishIndex++;
				time = 0;
			}

		}
	}

	void FBLogWithLevel(){
		if (PlayerPrefs.GetInt ("Level", 1) == 1) {
			FaceBookGetLog.LogFinishLevel1Event();
		} else if (PlayerPrefs.GetInt ("Level", 1) == 2) {
			FaceBookGetLog.LogFinishLevel1Event();
		} else if (PlayerPrefs.GetInt ("Level", 1) == 3) {
			FaceBookGetLog.LogFinishLevel1Event();
		}
	}

	void ChangeUIWithGoldNet(Transform popBG){
		if (PlayerPrefs.GetInt ("golden_net", 0) == 1) {
			popBG.Find ("content").localPosition -= new Vector3 (0, 150, 0);
			popBG.Find ("sure").localPosition -= new Vector3 (0, 150, 0);
			popBG.Find ("double").localPosition -= new Vector3 (0, 150, 0);
			GameObject lineGold =Instantiate ((GameObject)Resources.Load("LineGold"),popBG); 
			lineGold.GetComponent<Text> ().text = "$" + UIManager.UnitChange (goldSum);
			popBG.Find ("content").GetComponent<Text> ().text = "$" + UIManager.UnitChange (goldSum*2);

		}
		if (PlayerPrefs.GetInt ("golden_net", 0) == 0) {
			return;
		}
	}

	float GetSettleTime(int count){
		if (count < 10) {
			return 0.1f;
		} else if (count >= 10 && count < 20) {
			return 0.08f;
		} else if (count >= 20 && count < 30) {
			return 0.06f;
		} else if (count >= 30 && count < 40) {
			return 0.05f;
		} else if (count >= 40 && count < 50) {
			return 0.04f;
		} else {
			return 0.02f;
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.tag == "Pier") {
			if (ProgressManager.Instance.isOver) {
				gravityScale = 0;
				playerRig.velocity = Vector3.zero;
				ProgressManager.Instance.isReady = true;
				ProgressManager.Instance.isOver = false;
				settleCount = netParent.childCount - 1;

				if (PlayerPrefs.GetInt ("Level", 1) == 1) {
					if (settleCount > PlayerPrefs.GetInt ("maxFish-1", 0)) {
						PlayerPrefs.SetInt ("maxFish-1", settleCount);
					}
					PlayerPrefs.SetInt ("bestFish-1", PlayerPrefs.GetInt ("bestFish-1", 0) + settleCount);
				}else if (PlayerPrefs.GetInt ("Level", 1) == 2) {
					if (settleCount > PlayerPrefs.GetInt ("maxFish-2", 0)) {
						PlayerPrefs.SetInt ("maxFish-2", settleCount);
					}
					PlayerPrefs.SetInt ("bestFish-2", PlayerPrefs.GetInt ("bestFish-2", 0) + settleCount);
				}else if (PlayerPrefs.GetInt ("Level", 1) == 3) {
					if (settleCount > PlayerPrefs.GetInt ("maxFish-3", 0)) {
						PlayerPrefs.SetInt ("maxFish-3", settleCount);
					}
					PlayerPrefs.SetInt ("bestFish-3", PlayerPrefs.GetInt ("bestFish-3", 0) + settleCount);
				}

				settleTime = GetSettleTime (settleCount);
				isSettle = true;
				progressSlider.gameObject.SetActive (false);
			}
		}

		if (collider.tag == "BoundaryL") {
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
					if (Input.GetTouch (0).deltaPosition.x < 0) {
						moveSpeed = 0;
					} else {
						moveSpeed = maxSpeed;
					}
				} 
			}
			else {
				if (Input.GetKey (KeyCode.A)) {
					moveSpeed = 0;
				} else {
					moveSpeed = maxSpeed;
				}
			}
		}
		if (collider.tag == "BoundaryR") {
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
					if (Input.GetTouch (0).deltaPosition.x > 0) {
						moveSpeed = 0;
					} else {
						moveSpeed = maxSpeed;
					}
				} 
			} else {
				if (Input.GetKey (KeyCode.D)) {
					moveSpeed = 0;
				} else {
					moveSpeed = maxSpeed;
				}
			}
		}
		
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.tag == "BoundaryL"||coll.tag == "BoundaryR") {
			moveSpeed = maxSpeed;
		}
	}

	void OnTriggerStay2D(Collider2D collider){
		if (collider.tag == "BoundaryL") {
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
					if (Input.GetTouch (0).deltaPosition.x < 0) {
						moveSpeed = 0;
					} else {
						moveSpeed = maxSpeed;
					}
				} 
			}
			else {
				if (Input.GetKey (KeyCode.A)) {
					moveSpeed = 0;

				} else {
					//moveSpeed = minSpeed;
					moveSpeed = maxSpeed;
				}
			}
		}
		if (collider.tag == "BoundaryR") {
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
					if (Input.GetTouch (0).deltaPosition.x > 0) {
						moveSpeed = 0;
					} else {
						moveSpeed = maxSpeed;
					}
				} 
			} else {
				if (Input.GetKey (KeyCode.D)) {
					moveSpeed = 0;
				} else {
					//moveSpeed = minSpeed;
					moveSpeed = maxSpeed;
				}
			}
		}
		transform.position = new Vector2 (Mathf.Clamp (transform.position.x, boundL.position.x, boundR.position.x), transform.position.y);
	}

	void Settlement(Transform fish,float time){
		fish.DOMoveY (fish.position.y + 1.3f, time, false);
		fish.DOScale (1, time).OnComplete(()=>{
			fish.GetComponent<SpriteRenderer> ().DOFade (0f, time);
			if(fish.childCount>0){
				Destroy(fish.GetChild(0).gameObject);
			}
		});
	}

	void ScoreGenerate(Transform fish,float time){
		Text text = score;
		if (fish.name.StartsWith ("fish")) {
			text = Text.Instantiate (score, netParent.position, score.transform.rotation, scoreParent);
		} else {
			text = Text.Instantiate (specialScore, netParent.position, score.transform.rotation, scoreParent);
		}
		//text = Text.Instantiate (score, netParent.position, score.transform.rotation, scoreParent);

		int fishgold = (int)(fishDic [fish.name]/2*rebirthMulti*turnMulti);
		text.text ="$"+(fishgold).ToString();
		goldSum += fishgold;

		//增加重生经验
		curRebirthExp += expDic [fish.name];

		text.transform.position = Camera.main.WorldToScreenPoint (transform.position+Vector3.down*2);
		text.DOFade (0.1f, time*12);
		text.transform.DOMoveY (text.transform.position.y+600f, time*12, false);
		text.transform.localScale = Vector3.one*1.5f;
		text.transform.DOScale (1.65f, time*12).OnComplete(()=>{Destroy(text.gameObject);});
	}

    [HideInInspector]
    public string lastEpicFishName = "";
	//在结算分数弹出完毕播放史诗鱼动画
	IEnumerator FindEpicFish(float time){
		bool isDoing = false;

		while (epicFish.Count!=0) {
            if (!isDoing)
            {
                isDoing = true;
                Transform epicTrans = Instantiate(epicPop, GameObject.Find("Canvas").transform).transform;

                FindEpic findEpic = epicTrans.GetComponent<FindEpic>();

                int index = int.Parse(epicFish[0].Split(new char[] { 'l' })[1]) - 1;

                if (lastEpicFishName == "")
                {
                    lastEpicFishName = FishManager.Instance.epicNames[index];
                }

                findEpic.EpicPicture (FishManager.Instance.epicNames [index], FishManager.Instance.epicSprites [index], time);

				epicTrans.DOScale (1, time);
				epicTrans.DORotate (new Vector3 (0, 0, Random.Range(14,21)), time, 0).OnComplete (() => {
					epicFish.Remove(epicFish[0]);
					isDoing = false;
				});
			}
			yield return null;
           
        }
       


        //"unusual"

    }

	void InitFishDic(){
		fishDic.Add ("fish1(Clone)", 510);
		fishDic.Add ("fish2(Clone)", 750);
		fishDic.Add ("fish3(Clone)", 1250);
		fishDic.Add ("fish4(Clone)", 2000);
		fishDic.Add ("fish5(Clone)", 3000);
		fishDic.Add ("fish6(Clone)", 4500);
		fishDic.Add ("fish7(Clone)", 7000);
		fishDic.Add ("fish8(Clone)", 10000*3);
		fishDic.Add ("fish9(Clone)", 16000);
		fishDic.Add ("fish10(Clone)", 25000*3);
		fishDic.Add ("fish11(Clone)", 10000);
		fishDic.Add ("fish12(Clone)", 20000);
		fishDic.Add ("fish13(Clone)", 30000);
		fishDic.Add ("fish14(Clone)", 40000);
		fishDic.Add ("fish15(Clone)", 60000*3);
		fishDic.Add ("fish16(Clone)", 90000);
		fishDic.Add ("fish17(Clone)", 135000);
		fishDic.Add ("fish18(Clone)", 202500);
		fishDic.Add ("fish19(Clone)", 303750);
		fishDic.Add ("fish20(Clone)", 455600*3);
		fishDic.Add ("fish21(Clone)", 203400);
		fishDic.Add ("fish22(Clone)", 305100);
		fishDic.Add ("fish23(Clone)", 457650);
		fishDic.Add ("fish24(Clone)", 651000);
		fishDic.Add ("fish25(Clone)", 932000);
		fishDic.Add ("fish26(Clone)", 1258200);
		fishDic.Add ("fish27(Clone)", 1698500);
		fishDic.Add ("fish28(Clone)", 2293060);
		fishDic.Add ("fish29(Clone)", 3095640);
		fishDic.Add ("fish30(Clone)", 4179110);

		AddUnusual (30);
	}

	void InitExpDic(){
		expDic.Add ("fish1(Clone)", 1);
		expDic.Add ("fish2(Clone)", 2);
		expDic.Add ("fish3(Clone)", 3);
		expDic.Add ("fish4(Clone)", 4);
		expDic.Add ("fish5(Clone)", 5);
		expDic.Add ("fish6(Clone)", 6);
		expDic.Add ("fish7(Clone)", 7);
		expDic.Add ("fish8(Clone)", 8*3);
		expDic.Add ("fish9(Clone)", 9);
		expDic.Add ("fish10(Clone)", 10*3);
		expDic.Add ("fish11(Clone)", 11);
		expDic.Add ("fish12(Clone)", 12);
		expDic.Add ("fish13(Clone)", 13);
		expDic.Add ("fish14(Clone)", 14);
		expDic.Add ("fish15(Clone)", 15*3);
		expDic.Add ("fish16(Clone)", 16);
		expDic.Add ("fish17(Clone)", 17);
		expDic.Add ("fish18(Clone)", 18);
		expDic.Add ("fish19(Clone)", 19);
		expDic.Add ("fish20(Clone)", 20*3);
		expDic.Add ("fish21(Clone)", 21);
		expDic.Add ("fish22(Clone)", 22);
		expDic.Add ("fish23(Clone)", 23);
		expDic.Add ("fish24(Clone)", 24);
		expDic.Add ("fish25(Clone)", 25);
		expDic.Add ("fish26(Clone)", 26);
		expDic.Add ("fish27(Clone)", 27);
		expDic.Add ("fish28(Clone)", 28);
		expDic.Add ("fish29(Clone)", 29);
		expDic.Add ("fish30(Clone)", 30);

		AddUnusualExp (30);
	}


	void AddUnusual(int number){
		for (int i = 1; i <= number; i++) {
			fishDic.Add ("unusual"+i+"(Clone)", fishDic["fish"+i+"(Clone)"]*2);
		}
	}

	void AddUnusualExp(int number){
		for (int i = 1; i <= number; i++) {
			expDic.Add ("unusual"+i+"(Clone)", expDic["fish"+i+"(Clone)"]*2);
		}
	}

	public void InitProgressSlider(){
		progressSlider.minValue = UIManager.Instance.diveDepth;
		progressSlider.maxValue = 0;
	}
		
	public void SynDepth(){
		progressSlider.transform.Find ("depth").GetComponent<Text> ().text = UIManager.Instance.diveDepth+"M";
	}

	public void UpdateGoldMutiple(){
		GameObject popBG = (GameObject)Resources.Load ("PopBG");
		GameObject doubleImage = popBG.transform.Find ("GoldDouble").gameObject;
		//GameObject passVip = popBG.transform.Find ("PassVip").gameObject;
		SkinnedMeshRenderer skin1 = netParent.parent.Find ("FishNet").GetComponent<SkinnedMeshRenderer> ();
		SkinnedMeshRenderer skin2 = netParent.parent.Find ("FishNetReady").GetComponent<SkinnedMeshRenderer> ();
		Material goldNet = IPAManager.Instance.goldNet;
				
		if (PlayerPrefs.GetInt ("golden_net", 0) == 1) {
			goldMultiple = 2;
			skin1.material = goldNet;
			skin2.material = goldNet;
			doubleImage.SetActive (true);
			//skin1.transform.GetChild (0).gameObject.SetActive (true);
			//skin2.transform.GetChild (0).gameObject.SetActive (true);			
		}
		if (PlayerPrefs.GetInt ("golden_net", 0) == 0) {
			goldMultiple = 1;
			doubleImage.SetActive (false);
		}

	}

	void HidePopUI(bool bo){
		GameObject popBG = (GameObject)Resources.Load ("PopBG");
		GameObject doubleImage = popBG.transform.Find ("GoldDouble").gameObject;
		GameObject passVip = popBG.transform.Find ("PassVip").gameObject;
		GameObject extra = popBG.transform.Find ("extra").gameObject;
		doubleImage.SetActive (!bo);
		passVip.SetActive (false);
		extra.SetActive (false);
	}
}
