using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EpicTip : MonoBehaviour {
	Text text;

	// Use this for initialization
	void OnEnable () {
		text = transform.GetComponent<Text> ();
		StartCoroutine (ChangeColor ()); 
	}

	IEnumerator ChangeColor(){
		text.DOColor (new Color (155 / 255f, 155/255f, 155/255f), 0.7f).OnComplete (()=>{
			text.DOColor (new Color (255/255f, 1, 1), 1f).OnComplete(()=>{				
				StartCoroutine(ChangeColor());
			});
		});
		yield return null;
	}
}
