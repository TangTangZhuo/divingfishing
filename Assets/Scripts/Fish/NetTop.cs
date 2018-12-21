using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NetTop : MonoBehaviour {
	//public Transform fish;
	Text score;
	Text specialScore;
	Transform scoreParent;
	SubmarineController submarine;
	GameObject scoreParticle;

	bool isOver;
	// Use this for initialization
	void Start () {
		isOver = false;
		submarine = SubmarineController.Instance;
		score = submarine.score;
		specialScore = submarine.specialScore;
		scoreParent = submarine.scoreParent;
		scoreParticle = (GameObject)Resources.Load("ScoreParticle");
	}
	
	// Update is called once per frame
	void Update () {
		if (!isOver) {
           // Debug.Log("transform.parent.position.y:" + transform.parent.position.y);
           // Debug.Log("UIManager.Instance.diveDepth" + UIManager.Instance.diveDepth);
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
		if (collider.name == "fish101(Clone)") {
			TipPop.GenerateTip ("Merry Christmas！", 1f, Color.yellow);
		}
	}

	void ScoreGenerate(Transform fish,Vector3 offset){
		Text text = score;
		if (fish.name.StartsWith ("fish")) {
			text = Text.Instantiate (score, fish.position, score.transform.rotation, scoreParent);
			text.color = Color.white;
			if (PlayerPrefs.GetInt ("Audio", 1) == 1) {
				GameObject go = Instantiate (AudioManager.Instance.fishAudio);
				Destroy (go, 1f);
			}
		} else {
			text = Text.Instantiate (specialScore, fish.position, score.transform.rotation, scoreParent);
			if (PlayerPrefs.GetInt ("Audio", 1) == 1) {
				GameObject go = Instantiate (AudioManager.Instance.epicAudio);
				Destroy (go, 1f);
			}
		}
		text.text = "$"+((int)((submarine.fishDic [fish.name]/2)*submarine.rebirthMulti*submarine.turnMulti)).ToString();
		text.transform.position = Camera.main.WorldToScreenPoint (fish.position)+offset;
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
