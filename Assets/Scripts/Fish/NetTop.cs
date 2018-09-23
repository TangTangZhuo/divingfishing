using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NetTop : MonoBehaviour {
	//public Transform fish;
	Text score;
	Transform scoreParent;
	SubmarineController submarine;
	GameObject scoreParticle;

	bool isOver;
	// Use this for initialization
	void Start () {
		isOver = false;
		submarine = SubmarineController.Instance;
		score = submarine.score;
		scoreParent = submarine.scoreParent;
		scoreParticle = (GameObject)Resources.Load("ScoreParticle");
	}
	
	// Update is called once per frame
	void Update () {
		if (!isOver) {
			if (transform.parent.position.y < UIManager.Instance.diveDepth) {
				ProgressManager.Instance.GameOver ();
				isOver = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.tag == "Fish"||collider.tag == "unusual") {	
			if (!ProgressManager.Instance.isOvering && !ProgressManager.Instance.isReady) {
				if (collider.tag == "unusual") {
					MultiHaptic.HapticHeavy ();
					//ScoreGenerate (collider.transform);
					collider.GetComponent<GhostSprites> ().alphaFluctuationOverride = 0.15f;
					//Destroy( collider.GetComponent<GhostSprites> ());
				}
				if (ProgressManager.Instance.isRunning) {
					MultiHaptic.HapticLight ();
					ScoreGenerate (collider.transform,new Vector3(0,200,0));
					collider.transform.position = new Vector3 (transform.position.x + Random.Range (-0.8f, 0.8f), transform.position.y + 0.2f + Random.Range (-0.5f, 0.4f), 0.5f);
					collider.transform.SetParent (transform);
				} else if (ProgressManager.Instance.isOver) {
					MultiHaptic.HapticLight ();
					ScoreGenerate (collider.transform,new Vector3(0,0,0));
					collider.transform.position = new Vector3 (transform.position.x + Random.Range (-0.8f, 0.8f), transform.position.y -0.4f + Random.Range (0.4f, 0.9f), 0.5f);
					collider.transform.SetParent (transform);
				}
			}
		}
	}

	void ScoreGenerate(Transform fish,Vector3 offset){
		Text text = Text.Instantiate (score, fish.position, score.transform.rotation, scoreParent);
		text.text = (submarine.fishDic [fish.name]/2).ToString();
		text.transform.position = Camera.main.WorldToScreenPoint (fish.position)+offset;
		text.color = Color.white;
		GameObject scoreParticleObj = Instantiate (scoreParticle, Camera.main.ScreenToWorldPoint(text.transform.position), scoreParticle.transform.rotation);
		StartCoroutine (ScoreWithParticle (text.transform, scoreParticleObj.transform));
		text.transform.DOScale (1.5f, 0.3f).OnComplete(()=>{text.transform.DOScale (1f, 0.3f).OnComplete(()=>{
			Destroy(text.gameObject);
			Destroy(scoreParticleObj);
		});});
	}
		
	IEnumerator ScoreWithParticle(Transform score,Transform particle){
		while (score != null) {
			particle.position = Camera.main.ScreenToWorldPoint (score.position);
			yield return 0;  
		}					
	}
}
