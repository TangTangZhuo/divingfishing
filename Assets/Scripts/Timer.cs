using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public delegate void TurnTimeFinishDe();
	public event TurnTimeFinishDe TurnTimeFinish;

	public delegate void ADTimeFinishDe();
	public event ADTimeFinishDe ADTimeFinish;

	[HideInInspector]
	public Text countDownText;

	//Coroutine coroutineTurn;

	static Timer instance;
	public static Timer Instance{
		get{ return instance;}	
	}
	void Awake(){
		instance = this;
	}

	public void GetCountDownText(Text text){
		countDownText = text;
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);

		//新用户第一次进游戏5分钟免广告，之后超90秒单倍领取需要播放广告
		if (PlayerPrefs.GetInt ("NewUser", 0) == 0) {
			StartCountDownForce (300);
			PlayerPrefs.SetInt ("NewUser", 1);
		} else {
			StartCountDownForce (90);
		}

		ADTimeFinish += () => {
			PlayerPrefs.SetInt("ForceReady",1);
		};

		TurnTimeFinish += () => {
			PlayerPrefs.SetFloat ("turnMuti", 1);
			if(FindObjectOfType<RotateSelf>()){
				FindObjectOfType<RotateSelf>().turnTable.GetComponent<TurnTable>().UpdateBoost();
			}

		};
	}

	//开始转盘倒计时
	IEnumerator coroutine;
	public void StartCountDownTurn(float time){
		if (coroutine!=null) {
			StopCoroutine (coroutine);
		}
		coroutine = CountDownTurn (time);
		StartCoroutine (coroutine);
	}

	//开始插屏倒计时
	IEnumerator coroutineForce;
	public void StartCountDownForce(float time){
		if (coroutineForce != null) {
			StopCoroutine (coroutineForce);
		}
		coroutineForce = CountDownForce (time);
		StartCoroutine (coroutineForce);
	}

	//转盘倒计时及回调
	IEnumerator CountDownTurn(float time){
		float timee = time;
		while (timee > 0) {
			timee -= Time.deltaTime;
			if (countDownText) {
				countDownText.text = FormatTwoTime((int)timee);
			}
			yield return null;
		}

		if (TurnTimeFinish != null) {
			TurnTimeFinish ();
		}
	}

	//分钟转小时：分钟
	public string FormatTwoTime(int totalSeconds)
	{
		int minutes = totalSeconds / 60;
		string mm = minutes < 10f ? "0" + minutes : minutes.ToString();
		int seconds = (totalSeconds - (minutes * 60));
		string ss = seconds < 10 ? "0" + seconds : seconds.ToString();
		return string.Format("{0}:{1}", mm, ss);
	}

	//插屏广告倒计时
	IEnumerator CountDownForce(float time){
		while (time > 0) {
			time -= Time.deltaTime;
			yield return null;
		}
		if (ADTimeFinish != null) {
			ADTimeFinish ();
		}
	}
}
