using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDead : MonoBehaviour {
	Material material;
	public PlayerController playerController;
//	private BalloonCollider balloonCo;
	public bool isDead;
	private bool isReset;

//	private Vector3 startPosition;
//
//	public Transform chain;

	private static PlayerDead instance;
	public static PlayerDead Instance{
		get{return instance;}
	}

	void Awake(){
		instance = this;	
	}
	// Use this for initialization
	void Start () {
		isDead = true;
		isReset = false;

		material = GetComponent<SpriteRenderer> ().material;
//		balloonCo = GetComponentInParent<BalloonCollider> ();

		Color color = new Color (0, 0, 0);
		material.SetColor ("_icolor", color);

//		startPosition = chain.position;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void GameOver(){
		StartCoroutine (Dissolve ());
		isDead = false;
	}

	//死亡溶解
	IEnumerator Dissolve(){
		float value = 0.251f;
		for (float time = 0; time < 1; time += Time.deltaTime) {
			value = Mathf.Lerp (value, 0.8f, time);
			material.SetFloat ("_value", value);
			if (Mathf.Abs (value - 0.8f) < 0.0001f) {
				Time.timeScale = 0.3f;
				playerController.moveSpeed = 0;
				if (!isReset) {
					isReset = true;
					Invoke ("Reset", 0.3f);
				}
				continue;
			}
			yield return 0;
		}
	}

	void Reset(){
		SceneManager.LoadScene ("Main");
		isDead = true;
		isReset = false;
		Time.timeScale = 1;
	}
}
