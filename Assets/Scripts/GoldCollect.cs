using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoldCollect : MonoBehaviour {
	Transform goldImage;
	Transform extraImage;
	// Use this for initialization
	void Start () {
		Invoke ("GetGoldImage", 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GetGoldImage(){
		if (GameObject.FindGameObjectWithTag ("goldImage")) {
			goldImage = GameObject.FindGameObjectWithTag ("goldImage").transform;
			extraImage = GameObject.FindGameObjectWithTag ("extraImage").transform;

		}else{
			Invoke ("DestroyImage", 1f);
		}
	}

	void DestroyImage(){
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "goldCollider") {
			goldImage.DOScale (new Vector3 (2f, 2f, 2f), 0.2f).OnComplete(()=>{
				goldImage.DOScale (1.8f, 0.2f);
				Destroy(gameObject);
			});
		}
		if (col.tag == "extraCollider") {
			extraImage.DOScale (new Vector3 (1.2f, 1.2f, 1.2f), 0.2f).OnComplete(()=>{
				extraImage.DOScale (1f, 0.2f);
				Destroy(gameObject);
			});
		}

	}
}
