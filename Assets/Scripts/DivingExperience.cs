using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DivingExperience : MonoBehaviour {
	public Text starImage;
	public Text star1;
	public Text curLvl1;
	public Text diveExp1;
	public Text bonus1;
	public Text star2;
	public Text curLvl2;
	public Text diveExp2;
	public Text bonus2;
	public Image writePop;
	public Transform collectBtn;

	bool resetGame = false;

	public GameObject Pop;

	static DivingExperience instance;
	public static DivingExperience Instance;
	void Awake(){
		instance = this;
	}

	void OnEnable () {
		UpdateUIState ();
	}


	public void OnBackBtn(){
		if (resetGame) {
			ProgressManager.Instance.GameWin ();
		}
		Pop.SetActive (false);
	}

	public void OnOpenBtn(){
		Pop.SetActive (true);
	}

	public void OnCollectBtn(){
		int exp = PlayerPrefs.GetInt ("exp", 0);
		int level = PlayerPrefs.GetInt ("rebirthLevel", 1)-1;
		PlayerPrefs.SetInt ("exp", 0);
		PlayerPrefs.SetInt ("rebirthLevel", level+exp+1);

		UpdateUIState ();

		ResetGame ();
	}

	//清除数据
	void ResetGame(){
		PlayerPrefs.SetInt ("Level", 1);
		PlayerPrefs.SetInt ("Lock2", 0);
		PlayerPrefs.SetInt ("Lock3", 0);
		PlayerPrefs.SetInt ("valueDepth", -17);
		PlayerPrefs.SetInt ("valueDepth2", -17);
		PlayerPrefs.SetInt ("valueDepth3", -17);
		PlayerPrefs.SetInt ("ClamGold", 0);
		PlayerPrefs.SetString ("gold", "0");
		PlayerPrefs.SetString ("valueOffline", "40");
		PlayerPrefs.SetString ("priceDepth", "2810");
		PlayerPrefs.SetString ("priceDepth2", "1240000");
		PlayerPrefs.SetString ("priceDepth3", "31400000");
		PlayerPrefs.SetString ("foreGold", "0");
		PlayerPrefs.SetString ("priceOffline", "2810");

		ResetAnima ();
		resetGame = true;
	}

	//重置动画
	void ResetAnima(){
		collectBtn.DOPunchRotation (Vector3.one*2, 1, 3, 1);
		writePop.transform.DOMoveY (writePop.transform.position.y + Screen.height, 0.3f, false).OnComplete(()=>{
			writePop.transform.DOPunchPosition(Vector3.up*3,0.5f,10,1,false);
			writePop.DOFade(0,0.5f).OnComplete(()=>{
				writePop.gameObject.SetActive(false);
			});
		});

	}

	//更新text显示
	public void UpdateUIState(){
		//星星数量
		int star = PlayerPrefs.GetInt ("star", 0);
		starImage.text = star.ToString();
		star1.text = star.ToString();
		star2.text = star.ToString();

		//当前可加等级
		int exp = PlayerPrefs.GetInt ("exp", 0);
		diveExp1.text = exp.ToString();
		diveExp2.text = "+" +  exp;

		//总等级
		int level = PlayerPrefs.GetInt ("rebirthLevel", 1)-1;
		curLvl1.text = level.ToString ();
		curLvl2.text = level.ToString ();

		//百分比加成
		int bonus = star*level;
		bonus1.text = "+" + bonus + "%";
		bonus2.text = "+" + bonus + "%";

	}
}
