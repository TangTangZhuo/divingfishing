using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingManager : MonoBehaviour {
	bool onSetting = false;
	// Use this for initialization
	void Start () {
		UpdateState ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
		}
		if (audio == 1) {
			PlayerPrefs.SetInt ("Audio", 0);
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
