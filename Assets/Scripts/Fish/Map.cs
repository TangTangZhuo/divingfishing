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

	public Transform path;
	public Transform point;
	private Transform[] pathPoint;

	public Transform shipMark;
	// Use this for initialization
	void Start () {		
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
	}

	public void OnBackBtn(){
		gameObject.SetActive (false);

	}

	public void OnMapBtn(){
		gameObject.SetActive (true);
		point.DOPunchPosition (new Vector3 (0, 10, 0), 1, 5, 1, false).SetLoops(10);
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
			shipMark.DOPath (pathV3, 1, PathType.Linear, PathMode.TopDown2D, 10, null).OnComplete(()=>{
				PlayerPrefs.SetInt ("Level", 1);
				SceneManager.LoadScene ("Level1");
			});
		}
	}

	public void OnLevel2Btn(){
		print (1);
		if (PlayerPrefs.GetInt ("Lock2", 0) == 1) {
			print (2);
			int level = PlayerPrefs.GetInt ("Level", 1);
			if (level != 2) {
				print (3);
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
				shipMark.DOPath (pathV3, 1, PathType.Linear, PathMode.TopDown2D, 10, null).OnComplete (() => {
					PlayerPrefs.SetInt ("Level", 2);
					SceneManager.LoadScene ("Level2");
				});
			}
		} else {
			int cost = 2000000;
			MessageBox.Show ("", "It costs "+"$2,000K" +"to unlock" ,2);
			MessageBox.confim =()=>{
				int gold = PlayerPrefs.GetInt ("gold", 0);
				if(gold>cost){
					gold -=cost;
					PlayerPrefs.SetInt ("gold", gold);
					UIManager.Instance.goldT.DOText (UIManager.UnitChange (gold), 0.5f, false, ScrambleMode.None, null);
					Upgrading.Instance.CheckGold ();
					UpgradingOffline.Instance.CheckGold ();
					PlayerPrefs.SetInt ("Lock2",1);
					transform.Find("Level2").Find("lock").gameObject.SetActive(false);
				}else{
					GenerateText (lv4, "Not enough money！");
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
				shipMark.DOPath (pathV3, 1, PathType.Linear, PathMode.TopDown2D, 10, null).OnComplete (() => {
					PlayerPrefs.SetInt ("Level", 3);
					SceneManager.LoadScene ("Level3");
				});
			}
		} else {
			int cost = 300000000;
			MessageBox.Show ("", "It costs "+"$300,000K" +"to unlock" ,2);
			MessageBox.confim =()=>{
				int gold = PlayerPrefs.GetInt ("gold", 0);
				if(gold>cost){
					gold -=cost;
					PlayerPrefs.SetInt ("gold", gold);
					UIManager.Instance.goldT.DOText (UIManager.UnitChange (gold), 0.5f, false, ScrambleMode.None, null);
					Upgrading.Instance.CheckGold ();
					UpgradingOffline.Instance.CheckGold ();
					PlayerPrefs.SetInt ("Lock3",1);
					transform.Find("Level3").Find("lock").gameObject.SetActive(false);
				}else{
					GenerateText (lv4, "Not enough money！");
				}					
			};
		}
	}

	public void OnLevel4Btn(){
		MultiHaptic.HapticMedium ();
		GenerateText (lv4, "当前关卡还未开放！");
	}

	void GenerateText(Transform trans,string content){
		Text text = Text.Instantiate (noLeveltext,trans.position,noLeveltext.transform.rotation,trans);
		text.text = content;
		text.DOFade (1f, 0.6f);
		text.transform.DOMoveY (text.transform.position.y+100f, 0.6f, false);
		text.transform.DOScale (1.2f, 0.6f).OnComplete(()=>{Destroy(text.gameObject);});
	} 
		
		
}
