using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingManager : MonoBehaviour {
	bool onSetting = false;

	public AudioSource bg1;
	public AudioSource bg2;
	// Use this for initialization

	private static SettingManager instance;
	public static SettingManager Instance{
		get{return instance;}
	}
	void Awake(){
		instance = this;
	}

	void Start () {
		UpdateState ();
		int audio = PlayerPrefs.GetInt ("Audio", 1);
		if (audio == 0) {	
			if (bg1) {
				bg1.mute = true;
				bg2.mute = true;
			}
		}
		if (audio == 1) {			
			GameObject.Find ("AudioManager").GetComponent<AudioSource> ().mute = false;
			GameObject.Find ("waterAudio").GetComponent<AudioSource> ().mute = false;
		}
		UpdateAudioState ();
	}
	
	// Update is called once per frame


	public void OnTapticClick(){
		int taptic = PlayerPrefs.GetInt ("Taptic", 1);
		if (taptic == 0) {
			PlayerPrefs.SetInt ("Taptic", 1);
		}
		if (taptic == 1) {
			PlayerPrefs.SetInt ("Taptic", 0);
		}
		UpdateState ();

	}

	public void OnAudioClick(){
		int audio = PlayerPrefs.GetInt ("Audio", 1);
		if (audio == 0) {
			PlayerPrefs.SetInt ("Audio", 1);
			GameObject.Find ("AudioManager").GetComponent<AudioSource> ().mute = false;
			GameObject.Find ("waterAudio").GetComponent<AudioSource> ().mute = false;
		}
		if (audio == 1) {
			PlayerPrefs.SetInt ("Audio", 0);
			GameObject.Find ("AudioManager").GetComponent<AudioSource> ().mute = true;
			GameObject.Find ("waterAudio").GetComponent<AudioSource> ().mute = true;
		}
		UpdateAudioState ();

	}

	void UpdateState(){
		int taptic = PlayerPrefs.GetInt ("Taptic", 1);
		if (taptic == 0) {
			transform.GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
		}if (taptic == 1) {
			transform.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		}
	}

	void UpdateAudioState(){
		int audio = PlayerPrefs.GetInt ("Audio", 1);
		if (audio == 0) {
			transform.GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
		}if (audio == 1) {
			transform.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		}
	}
		
	public void OnSettingClick(){
		StartCoroutine (ChangeHeight ());

	}

	IEnumerator ChangeHeight(){
		RectTransform rect = transform.parent.GetComponent<RectTransform> ();
		while(true){
			if (!onSetting) {				
				rect.sizeDelta = Vector2.Lerp(rect.sizeDelta,new Vector2(rect.sizeDelta.x,650),Time.deltaTime*10);
				if (Mathf.Abs( rect.sizeDelta.y - 650)<0.1f) {
					onSetting = true;
					yield break;
				}
				yield return null;
			}
			if (onSetting) {				
				rect.sizeDelta = Vector2.Lerp(rect.sizeDelta,new Vector2(rect.sizeDelta.x,172),Time.deltaTime*10);
				if (Mathf.Abs( rect.sizeDelta.y - 172)<0.1f) {
					onSetting = false;
					yield break;
				}
				yield return null;
			}
		}
	}
}
