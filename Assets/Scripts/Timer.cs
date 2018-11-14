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
	}

	IEnumerator coroutine;
	public void StartCountDownTurn(float time){
		if (coroutine!=null) {
			StopCoroutine (coroutine);
		}
		coroutine = CountDownTurn (time);
		StartCoroutine (coroutine);
	}

	public void StartCountDownForce(float time){
		StartCoroutine (CountDownForce (time));
	}

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

	public string FormatTwoTime(int totalSeconds)
	{
		int minutes = totalSeconds / 60;
		string mm = minutes < 10f ? "0" + minutes : minutes.ToString();
		int seconds = (totalSeconds - (minutes * 60));
		string ss = seconds < 10 ? "0" + seconds : seconds.ToString();
		return string.Format("{0}:{1}", mm, ss);
	}


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
