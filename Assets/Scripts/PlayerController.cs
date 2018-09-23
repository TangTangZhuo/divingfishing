using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	Rigidbody2D playerRig;
	public float moveSpeed;
	[HideInInspector]
	public bool isStart;
	[HideInInspector]
	public bool isMove;
	public Transform lightning;
	public float lightningSpeed;
//	private float addSpeed;
	float sliderValue=0;
	float badSliderValue=0;

	private static PlayerController instance;
	public static PlayerController Instance{
		get{return instance;}
	}

	void Awake(){
		instance = this;	
	}

	// Use this for initialization
	void Start () {
		playerRig = GetComponent<Rigidbody2D> ();
		isStart = false;
		isMove = true;
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
			moveSpeed = 1;
		} else {
		//	moveSpeed = 1;
		}
//		addSpeed = 0;
	}
	
	// Update is called once per frame
	void Update () {
//		if (playerRig.velocity.y < -6) {
//			addSpeed = moveSpeed * playerRig.velocity.y / -6f - moveSpeed;
//		} else {
//			addSpeed = 0;
//		}
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
			if (isMove) {
				//if (playerRig.velocity.y <= 0) {
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
					//playerRig.AddForce (Vector2.right * Input.GetTouch (0).deltaPosition.x * (moveSpeed + addSpeed));
					if(Input.GetTouch (0).deltaPosition.x>10)
						transform.Translate (new Vector3 (10 * moveSpeed * Time.deltaTime, 0, 0), Space.World);					
					else if(Input.GetTouch (0).deltaPosition.x<-10)
						transform.Translate (new Vector3 (-10 * moveSpeed * Time.deltaTime, 0, 0), Space.World);					
					else{
						transform.Translate (new Vector3 (Input.GetTouch (0).deltaPosition.x * moveSpeed * Time.deltaTime, 0, 0), Space.World);
					}
					if (!isStart) {
						DropGenerate.Instance.isGenerate = true;
						isStart = true;
					}
				} else {
					playerRig.velocity = new Vector2 (playerRig.velocity.x / 1.1f, playerRig.velocity.y);
				}
			//	}
			}
		} else {
			if (isMove) {
				//if (playerRig.velocity.y <= 0) {
					if (Input.GetKey (KeyCode.A)) {
					//	playerRig.AddForce (Vector2.left * (moveSpeed + addSpeed));
					transform.Translate (new Vector3 (-moveSpeed * Time.deltaTime,0, 0),Space.World);

						if (!isStart) {
							DropGenerate.Instance.isGenerate = true;
							isStart = true;
						}
					} else if (Input.GetKey (KeyCode.D)) {
					//	playerRig.AddForce (Vector2.right * (moveSpeed + addSpeed));
					transform.Translate (new Vector3 (moveSpeed * Time.deltaTime, 0, 0),Space.World);
						if (!isStart) {
							DropGenerate.Instance.isGenerate = true;
							isStart = true;
						}
					} else {
						playerRig.velocity = new Vector2 (playerRig.velocity.x / 1.1f, playerRig.velocity.y);
					}
				//}
			}
		}
		if (transform.position.y < sliderValue && isStart) {
			ProgressManager.Instance.progressSlider.value = transform.position.y;
			sliderValue = ProgressManager.Instance.progressSlider.value - 2;
		}
		if (isStart) {
			//lightning.position += Vector3.down * Time.deltaTime * lightningSpeed;
			lightning.position = Vector3.Lerp (lightning.position, lightning.position + Vector3.down, Time.deltaTime * lightningSpeed);
			if (lightning.position.y < badSliderValue) {
				ProgressManager.Instance.sliderBad.value = lightning.position.y;
				badSliderValue = ProgressManager.Instance.sliderBad.value - 2;
			}
		}


	}

//	void OnTriggerEnter2D(Collider2D collider){
//		if (collider.tag == "Pier") {
//			if (ProgressManager.Instance.isOver) {
//				playerRig.gravityScale = 0;
//				playerRig.velocity = Vector3.zero;
//				ProgressManager.Instance.isReady = true;
//			}
//		}
//	}
}
