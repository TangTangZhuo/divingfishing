using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;
using Together;

public class SubmarineController : MonoBehaviour {
	public float moveSpeed;
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
	public float force;

	float time;
	bool isSettle;
	int fishIndex;
	int goldSum;
	int settleCount;
	float settleTime;
	//FlyGold flyGod;
	[HideInInspector]
	int goldMultiple = 1;

	public Dictionary<string,int> fishDic = new Dictionary<string, int>();

	private static SubmarineController instance;
	public static SubmarineController Instance{
		get{return instance;}
	}

	void Awake(){
		instance = this;
		//PlayerPrefs.SetInt ("gold", 1999999999);
		//PlayerPrefs.DeleteAll ();
	}

	// Use this for initialization
	void Start () {
		settleCount = 0;
		settleTime = 0;
		time = 0;
		isSettle = false;
		fishIndex = 0;
		playerRig = GetComponent<Rigidbody2D> ();
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
			moveSpeed = 0.5f;
		else
			moveSpeed = 10;
		playerRig.gravityScale = 0;
		InitFishDic ();
		goldSum = 0;
		InitProgressSlider ();
//		UpdateGoldMutiple ();
//		flyGod = (FlyGold)Object.FindObjectOfType(typeof(FlyGold));
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
			PlayerPrefs.SetInt ("foreGold", PlayerPrefs.GetInt("gold",0));	
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
					if (PlayerPrefs.GetInt (fish.name.Split (new char[]{ '(' }) [0], 0)==0) {
						PlayerPrefs.SetInt ("illNew", 1);
					}
					PlayerPrefs.SetInt (fish.name.Split (new char[]{'('}) [0], 1);
					ScoreGenerate (fish);

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

						MessageBox.Show("You Earend","$"+ UIManager.UnitChange(goldSum));

						if(PlayerPrefs.GetInt("double",0)>=2){							
							Transform doubleTrans1 = GameObject.Find("PopBG(Clone)").transform.Find("double");
							if(doubleTrans1!=null){
								doubleTrans1.DOPunchRotation(new Vector3(0,0,5),1,5,1).SetLoops(100);
							}
						}

						MessageBox.confim =()=>{					
							int gold = PlayerPrefs.GetInt ("gold", 0) + goldSum*goldMultiple;
							PlayerPrefs.SetInt ("gold", gold);
							Upgrading.Instance.CheckGold();
							UpgradingOffline.Instance.CheckGold();

							ProgressManager.Instance.GameWin ();
							PlayerPrefs.SetInt ("double", PlayerPrefs.GetInt ("double", 0) + 1);
							PlayerPrefs.SetInt ("ClamGold", 1);
						};
						MessageBox.doubleR =()=>{															
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
								int gold = PlayerPrefs.GetInt ("gold", 0);
								goldSum*=goldMultiple;

								Button btn = adPop.transform.Find("sure").GetComponent<Button>();
								adPop.transform.Find("content").GetComponent<Text>().text ="$" + 0.ToString();
								btn.onClick.AddListener(()=>{
									PlayerPrefs.SetInt ("ClamGold", 1);
									PlayerPrefs.SetInt ("gold", gold);
									Upgrading.Instance.CheckGold();
									UpgradingOffline.Instance.CheckGold();
									ProgressManager.Instance.GameWin ();	
								});

								TGSDK.AdCompleteCallback = (string msg) => {
									Debug.Log("AdCompleteCallback");
									PlayerPrefs.SetInt("double",0);
									if(doubleName == "Bonus×3"){
										gold = PlayerPrefs.GetInt ("gold", 0) + goldSum*3;
									}else if(doubleName == "Bonus×4"){
										gold = PlayerPrefs.GetInt ("gold", 0) + goldSum*4;
									}else if(doubleName == "Bonus×5"){
										gold = PlayerPrefs.GetInt ("gold", 0) + goldSum*5;
									}else if(doubleName == "Bonus×2"){
										gold = PlayerPrefs.GetInt ("gold", 0) + goldSum*2;
									}
									adPop.transform.Find("content").GetComponent<Text>().text ="$" + (gold-PlayerPrefs.GetInt ("gold", 0)-goldSum).ToString();
								};

							});
								


						//	};
						};							
							
					});						

					if (PlayerPrefs.GetInt (fish.name.Split (new char[]{ '(' }) [0], 0)==0) {
						PlayerPrefs.SetInt ("illNew", 1);
					}
					PlayerPrefs.SetInt (fish.name.Split (new char[]{'('}) [0], 1);
					ScoreGenerate (fish);
					PlayerPrefs.SetInt ("accumulation", PlayerPrefs.GetInt ("accumulation", 0)+(int)(goldSum*0.6f));

				}	
				MultiHaptic.HapticLight ();
				fishIndex++;
				time = 0;
			}

		}
	}

	void ADReward(){
		

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
						moveSpeed = 0.5f;
					}
				} 
			}
			else {
				if (Input.GetKey (KeyCode.A)) {
					moveSpeed = 0;
				} else {
					moveSpeed = 10;
				}
			}
		}
		if (collider.tag == "BoundaryR") {
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
					if (Input.GetTouch (0).deltaPosition.x > 0) {
						moveSpeed = 0;
					} else {
						moveSpeed = 0.5f;
					}
				} 
			} else {
				if (Input.GetKey (KeyCode.D)) {
					moveSpeed = 0;
				} else {
					moveSpeed = 10;
				}
			}
		}
		
	}

	void OnTriggerStay2D(Collider2D collider){
		if (collider.tag == "BoundaryL") {
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
					if (Input.GetTouch (0).deltaPosition.x < 0) {
						moveSpeed = 0;
					} else {
						moveSpeed = 0.5f;
					}
				} 
			}
			else {
				if (Input.GetKey (KeyCode.A)) {
					moveSpeed = 0;
				} else {
					moveSpeed = 10;
				}
			}
		}
		if (collider.tag == "BoundaryR") {
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
					if (Input.GetTouch (0).deltaPosition.x > 0) {
						moveSpeed = 0;
					} else {
						moveSpeed = 0.5f;
					}
				} 
			} else {
				if (Input.GetKey (KeyCode.D)) {
					moveSpeed = 0;
				} else {
					moveSpeed = 10;
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

	void ScoreGenerate(Transform fish){
		Text text = score;
		if (fish.name.StartsWith ("fish")) {
			text = Text.Instantiate (score, netParent.position, score.transform.rotation, scoreParent);
		} else {
			text = Text.Instantiate (specialScore, netParent.position, score.transform.rotation, scoreParent);
		}
		//text = Text.Instantiate (score, netParent.position, score.transform.rotation, scoreParent);
		text.text ="$"+(fishDic [fish.name]/2).ToString();
		goldSum += (fishDic [fish.name]/2);
		text.transform.position = Camera.main.WorldToScreenPoint (fish.position);
		text.DOFade (1f, 0.3f);
		text.transform.DOMoveY (text.transform.position.y+250f, 0.3f, false);
		text.transform.DOScale (1.5f, 0.3f).OnComplete(()=>{Destroy(text.gameObject);});
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

	void AddUnusual(int number){
		for (int i = 1; i <= number; i++) {
			fishDic.Add ("unusual"+i+"(Clone)", fishDic["fish"+i+"(Clone)"]*2);
		}
	}

	public void InitProgressSlider(){
		progressSlider.minValue = UIManager.Instance.diveDepth;
		progressSlider.maxValue = 0;
	}
		
	public void SynDepth(){
		progressSlider.transform.Find ("depth").GetComponent<Text> ().text = UIManager.Instance.diveDepth+"M";
	}

//	public void UpdateGoldMutiple(){
//		GameObject popBG = (GameObject)Resources.Load ("PopBG");
//		GameObject doubleImage = popBG.transform.Find ("GoldDouble").gameObject;
//		GameObject passVip = popBG.transform.Find ("PassVip").gameObject;
//		SkinnedMeshRenderer skin1 = netParent.parent.Find ("FishNet").GetComponent<SkinnedMeshRenderer> ();
//		SkinnedMeshRenderer skin2 = netParent.parent.Find ("FishNetReady").GetComponent<SkinnedMeshRenderer> ();
////		Material goldNet = IPAManager.Instance.goldNet;
//				
//		if (PlayerPrefs.GetInt ("golden_net", 0) == 1) {
//			goldMultiple = 2;
//			skin1.material = goldNet;
//			skin2.material = goldNet;
//				//skin1.transform.GetChild (0).gameObject.SetActive (true);
//				//skin2.transform.GetChild (0).gameObject.SetActive (true);			
//		}
//		if (PlayerPrefs.GetInt ("golden_net", 0) == 0) {
//			goldMultiple = 1;
//		}
//
//	}

	void HidePopUI(bool bo){
//		GameObject popBG = (GameObject)Resources.Load ("PopBG");
//		GameObject doubleImage = popBG.transform.Find ("GoldDouble").gameObject;
//		GameObject passVip = popBG.transform.Find ("PassVip").gameObject;
//		GameObject extra = popBG.transform.Find ("extra").gameObject;
//		doubleImage.SetActive (!bo);
//		passVip.SetActive (false);
//		extra.SetActive (false);
	}
}
