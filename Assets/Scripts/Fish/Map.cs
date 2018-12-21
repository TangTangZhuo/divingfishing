using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class Map : MonoBehaviour {
	public Text noLeveltext;
	public Transform[] levelPos;
	public Transform lv4;
	public Transform lvChristmasPos;

	public Transform path;
	public Transform point;
	private Transform[] pathPoint;

	public Transform shipMark;

    private static Map instance;
    public static Map Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
	void Start () {	
//		PlayerPrefs.SetInt ("LockChristmas",0);
//		PlayerPrefs.SetInt ("christmasGuid", 0);	
		shipMark.position = levelPos [PlayerPrefs.GetInt ("Level", 1) - 1].position;
		pathPoint = new Transform[path.childCount];
		for (int i = 0; i < path.childCount; i++) {
			pathPoint [i] = path.GetChild (i);
		}
		if (PlayerPrefs.GetInt ("Lock2", 0) == 1) {
			transform.Find("Level2").Find("lock").gameObject.SetActive(false);
		}
		if (PlayerPrefs.GetInt ("Lock3", 0) == 1) {
			transform.Find("Level3").Find("lock").gameObject.SetActive(false);
		}
		if (PlayerPrefs.GetInt ("LockChristmas", 0) == 1) {
			transform.Find("LevelChristmas").Find("lock").gameObject.SetActive(false);
		}
		if(PlayerPrefs.GetInt ("Level", 1)==4){
			shipMark.position = lvChristmasPos.position;
		}
	}

	void OnEnable(){
		if (PlayerPrefs.GetInt ("ReBirthGuide", 0) == 0) {
			transform.Find ("ReBirthGuide").gameObject.SetActive (true);
		}
	}

	public void OnBackBtn(){
		gameObject.SetActive (false);

	}

	public void OnMapBtn(){
		gameObject.SetActive (true);
		point.DOPunchPosition (new Vector3 (0, 10, 0), 1, 5, 1, false).SetLoops(100);
		MultiHaptic.HapticMedium ();
	}

	public void OnLevel1Btn(){
		int level = PlayerPrefs.GetInt ("Level", 1);
		if (level != 1) {
			MultiHaptic.HapticMedium ();
			int v3Count = int.Parse((levelPos [level - 1].name.ToString ()));
			Vector3[] pathV3 = new Vector3[v3Count];
			for (int i = 0; i < v3Count; i++) {
				pathV3 [i] = pathPoint [v3Count - i -1].position;
			}
			Destroy (point.gameObject);
			if (level != 4) {
				shipMark.DOPath (pathV3, 1, PathType.Linear, PathMode.TopDown2D, 10, null).OnComplete (() => {
					PlayerPrefs.SetInt ("Level", 1);
					PlayerPrefs.SetInt ("EnterGame", 1);
					SceneManager.LoadScene ("Level1");
				});
			} else {
				PlayerPrefs.SetInt ("Level", 1);
				PlayerPrefs.SetInt ("EnterGame", 1);
				SceneManager.LoadScene ("Level1");
			}
		}
	}

	public void OnLevel2Btn(){
		if (PlayerPrefs.GetInt ("Lock2", 0) == 1) {
			int level = PlayerPrefs.GetInt ("Level", 1);
			if (level != 2) {
				MultiHaptic.HapticMedium ();
				int index = int.Parse ((levelPos [level - 1].name.ToString ()));
				int curIndex = int.Parse ((levelPos [1].name.ToString ()));
				int v3Count;
				Vector3[] pathV3;
				if (curIndex > index) {
					v3Count = curIndex - index + 1;
					pathV3 = new Vector3[v3Count];
					for (int i = 0; i < curIndex; i++) {
						pathV3 [i] = pathPoint [i].position;
					}
				} else {
					v3Count = index - curIndex + 1;
					pathV3 = new Vector3[v3Count];
					for (int i = 0; i < v3Count; i++) {
						pathV3 [i] = pathPoint [index - i - 1].position;
					}
				}
				Destroy (point.gameObject);
				if (level != 4) {
					shipMark.DOPath (pathV3, 1, PathType.Linear, PathMode.TopDown2D, 10, null).OnComplete (() => {
						PlayerPrefs.SetInt ("Level", 2);
						PlayerPrefs.SetInt ("EnterGame", 1);
						SceneManager.LoadScene ("Level2");
					});
				} else {
					PlayerPrefs.SetInt ("Level", 2);
					PlayerPrefs.SetInt ("EnterGame", 1);
					SceneManager.LoadScene ("Level2");
				}
			}
		} else {
			long cost = 2000000;
			//MessageBox.Show ("", "It costs "+"$2,000K" +"to unlock" ,2);
			if (Application.systemLanguage == SystemLanguage.English) {
				MessageBox.Show ("", "It costs "+"$2,000K " +"to unlock" ,2);
			} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
				MessageBox.Show ("", "花费 "+"$2,000K " +"进行解锁" ,2);
			}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
				MessageBox.Show ("", "花費 "+"$2,000K " +"進行解鎖" ,2);
			}
			MessageBox.confim =()=>{
				long gold = long.Parse( PlayerPrefs.GetString ("gold", "0"));
				if(gold>cost){
					gold -=cost;
					PlayerPrefs.SetString ("gold", gold.ToString());
					UIManager.Instance.goldT.DOText (UIManager.UnitChange (gold), 0.5f, false, ScrambleMode.Numerals, null);
					Upgrading.Instance.CheckGold (gold);
					UpgradingOffline.Instance.CheckGold (gold);
					PlayerPrefs.SetInt ("Lock2",1);
					transform.Find("Level2").Find("lock").gameObject.SetActive(false);
					FaceBookGetLog.LogFirstLevel2Event();
				}else{
					if (Application.systemLanguage == SystemLanguage.English) {
						GenerateText (lv4, "Not enough money！");
					} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
						GenerateText (lv4, "没有足够的钱！");
					}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
						GenerateText (lv4, "沒有足夠的錢！");
					}
				}					
			};
		}
	}		

	public void OnLevel3Btn(){
		if (PlayerPrefs.GetInt ("Lock3", 0) == 1) {
			int level = PlayerPrefs.GetInt ("Level", 1);
			if (level != 3) {
				MultiHaptic.HapticMedium ();
				int index = int.Parse ((levelPos [level - 1].name.ToString ()));
				int curIndex = int.Parse ((levelPos [2].name.ToString ()));
				int v3Count;
				Vector3[] pathV3;
				if (curIndex > index) {
					v3Count = curIndex - index + 1;
					pathV3 = new Vector3[v3Count];
					for (int i = 0; i < v3Count; i++) {
						pathV3 [i] = pathPoint [index + i - 1].position;
					}
				} else {
					v3Count = index - curIndex + 1;
					pathV3 = new Vector3[v3Count];
					for (int i = 0; i < v3Count; i++) {
						pathV3 [i] = pathPoint [index - i].position;
					}
				}
				Destroy (point.gameObject);
				if (level != 4) {
					shipMark.DOPath (pathV3, 1, PathType.Linear, PathMode.TopDown2D, 10, null).OnComplete (() => {
						PlayerPrefs.SetInt ("Level", 3);
						PlayerPrefs.SetInt ("EnterGame", 1);
						SceneManager.LoadScene ("Level3");
					});
				} else {
					PlayerPrefs.SetInt ("Level", 3);
					PlayerPrefs.SetInt ("EnterGame", 1);
					SceneManager.LoadScene ("Level3");
				}
			}
		} else {
			long cost = 300000000;
			//MessageBox.Show ("", "It costs "+"$300,000K " +"to unlock" ,2);
			if (Application.systemLanguage == SystemLanguage.English) {
				MessageBox.Show ("", "It costs "+"$300,000K " +"to unlock" ,2);
			} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
				MessageBox.Show ("", "花费 "+"$300,000K " +"进行解锁" ,2);
			}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
				MessageBox.Show ("", "花費 "+"$300,000K " +"進行解鎖" ,2);
			}
			MessageBox.confim =()=>{
				long gold = long.Parse( PlayerPrefs.GetString ("gold", "0"));
				if(gold>cost){
					gold -=cost;
					PlayerPrefs.SetString ("gold", gold.ToString());
					UIManager.Instance.goldT.DOText (UIManager.UnitChange (gold), 0.5f, false, ScrambleMode.Numerals, null);
					Upgrading.Instance.CheckGold (gold);
					UpgradingOffline.Instance.CheckGold (gold);
					PlayerPrefs.SetInt ("Lock3",1);
					transform.Find("Level3").Find("lock").gameObject.SetActive(false);
					FaceBookGetLog.LogFirstLevel3Event();
				}else{
					if (Application.systemLanguage == SystemLanguage.English) {
						GenerateText (lv4, "Not enough money！");
					} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
						GenerateText (lv4, "没有足够的钱！");
					}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
						GenerateText (lv4, "沒有足夠的錢！");
					}
				}					
			};
		}
	}

	public void OnLevelChristmasBtn(){
		if (PlayerPrefs.GetInt ("LockChristmas", 0) == 1) {
			int level = PlayerPrefs.GetInt ("Level", 1);
			if (level != 4) {
				MultiHaptic.HapticMedium ();
				shipMark.DOMove (lvChristmasPos.position,1,false).OnComplete (() => {
					PlayerPrefs.SetInt ("Level", 4);
					PlayerPrefs.SetInt ("EnterGame", 1);
					SceneManager.LoadScene ("Level4");
				});
			}
		} else {
			if (Application.systemLanguage == SystemLanguage.English) {
				MessageBox.Show ("", "77 meters in depth to unlock" ,2);
			} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
				MessageBox.Show ("", "深度达到77米可进行解锁" ,2);
			}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
				MessageBox.Show ("", "深度達到77米可進行解鎖" ,2);
			}
			MessageBox.confim =()=>{				
				int depth = PlayerPrefs.GetInt ("valueDepth", UIManager.Instance.diveDepth);
				if(depth<=-77){										
					PlayerPrefs.SetInt ("LockChristmas",1);
					transform.Find("LevelChristmas").Find("lock").gameObject.SetActive(false);
					PlayerPrefs.SetInt ("christmasGuid", 1);
					FaceBookGetLog.LogFirstLevelChristmasEvent();
					if (Application.systemLanguage == SystemLanguage.English) {
						TipPop.GenerateTip("Successfully unlocked",0.5f);
					} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
						TipPop.GenerateTip("解锁成功",0.5f);
					}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
						TipPop.GenerateTip("解鎖成功",0.5f);
					}
				}else{
					if (Application.systemLanguage == SystemLanguage.English) {
						GenerateText (lv4, "Not enough depth！");
					} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
						GenerateText (lv4, "深度没有到达77米！");
					}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
						GenerateText (lv4, "深度沒有到達77米！");
					}
				}					
			};
		}
	}

	public void OnLevel4Btn(){
		MultiHaptic.HapticMedium ();
		if (Application.systemLanguage == SystemLanguage.English) {
			GenerateText (lv4, "To Be Continue...");
		} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
			GenerateText (lv4, "暂未开放...");
		}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
			GenerateText (lv4, "暫未開放...");
		}

	}

	void GenerateText(Transform trans,string content){
		Text text = Text.Instantiate (noLeveltext,trans.position,noLeveltext.transform.rotation,trans);
		text.text = content;
		text.DOFade (1f, 0.6f);
		text.transform.DOMoveY (text.transform.position.y+100f, 0.6f, false);
		text.transform.DOScale (1.2f, 0.6f).OnComplete(()=>{Destroy(text.gameObject);});
	} 
		
		
}
