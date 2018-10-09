using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoldCollect : MonoBehaviour {
	Transform goldImage;
	// Use this for initialization
	void Start () {
		Invoke ("GetGoldImage", 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GetGoldImage(){
		goldImage = GameObject.FindGameObjectWithTag ("goldImage").transform;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "goldCollider") {
			goldImage.DOScale (new Vector3 (1.5f, 1.5f, 1.5f), 0.2f).OnComplete(()=>{
				goldImage.DOScale (1, 0.2f);
				Destroy(gameObject);
			});
		}
	}
}
