﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGenerate : MonoBehaviour {
	public Transform startPosition;
	public Transform[] fish;
	public Transform[] unusual;

	RuntimeAnimatorController[] wind1;
	RuntimeAnimatorController[] wind2;
	RuntimeAnimatorController[] wind3;
	RuntimeAnimatorController[] wind4;
	RuntimeAnimatorController[] wind5;

	int typeDistance;
	int fishIndex;
	int[] euler;

	private static FishGenerate instance;
	public static FishGenerate Instance{
		get{return instance;}
	}

	void Awake(){
		instance = this;	
	}

	// Use this for initialization
	void Start () {
		fishIndex = 0;
		typeDistance = 10;
		euler = new int[]{ 180, 0 };
		wind1 = FishWink.Instance.wind1;
		wind2 = FishWink.Instance.wind2;
		wind3 = FishWink.Instance.wind3;
		wind4 = FishWink.Instance.wind4;
		wind5 = FishWink.Instance.wind5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenerateFish(){
		float screenMid = startPosition.position.x;
		int counter = 0;
		int fishNumber = 0; 
		bool isBig = false;
		for (int i = (int)startPosition.position.y-2; i > UIManager.Instance.diveDepth; i+=0) {
			float baseGeneratePosy = i - typeDistance / 2 ;
			string fishName = fish [fishIndex].GetComponent<SpriteRenderer> ().sprite.name;
			if (fishName == "fish7" || fishName == "fish10" || fishName == "fish26" || fishName == "fish20") {
				fishNumber = 3;
				isBig = true;
			} else {
				isBig = false;
				fishNumber = 10;
			}
			for (int num = 0; num < fishNumber; num++) {
				
				Transform fishNormal = Transform.Instantiate (fish[fishIndex], new Vector3 (screenMid + Random.Range (-2, 3), baseGeneratePosy + Random.Range (-5, 6)), 
					Quaternion.Euler (0, euler [Random.Range (0, 2)], 0), transform);
				if (!isBig) {
					//设置图层遮挡鱼的眼睛
					if (Random.Range (0, 10) == 5) {
						fishNormal.GetComponent<SpriteRenderer> ().sortingOrder = 2;
						fishNormal.GetChild (0).GetComponent<SpriteRenderer> ().sortingOrder = 3;
					}
					if (Random.Range (0, 20) == 5) {
						fishNormal.GetComponent<SpriteRenderer> ().sortingOrder = 4;
						fishNormal.GetChild (0).GetComponent<SpriteRenderer> ().sortingOrder = 5;
					}
					if (Random.Range (0, 40) == 5) {
						fishNormal.GetComponent<SpriteRenderer> ().sortingOrder = 6;
						fishNormal.GetChild (0).GetComponent<SpriteRenderer> ().sortingOrder = 7;
					}
				}
				ChangeFishEye (fishNormal);
				if (Random.Range (0, 60) == 10) {
					Transform fishUnusual = Transform.Instantiate (unusual[fishIndex], new Vector3 (screenMid + Random.Range (-2, 3), baseGeneratePosy + Random.Range (-5, 6)), 
						Quaternion.Euler (0, euler [Random.Range (0, 2)], 0), transform);
					fishUnusual.GetComponent<GhostSprites> ().TrailSize = 70;
					fishUnusual.GetComponent<GhostSprites> ().spacing = 1;
					//ChangeFishEye (fishUnusual);
				}
				if (Random.Range (0, 800) == 10) {
					if (fishIndex  > (fish.Length - 5)) {
						fishIndex = fish.Length - 5;
					}
					Transform fishHighLevel = Transform.Instantiate (fish[fishIndex+4], new Vector3 (screenMid + Random.Range (-2, 3), baseGeneratePosy + Random.Range (-5, 6)), 
						Quaternion.Euler (0, euler [Random.Range (0, 2)], 0), transform);
					ChangeFishEye (fishHighLevel);
				}

			}
			i -= typeDistance;
			counter++;
			if (counter == 2) {
				if (fishIndex < fish.Length-1) {
					fishIndex++;
				}
				counter = 0;
			}

		}
	}

	void ChangeFishEye(Transform fish){
		Animator animator = fish.GetComponentInChildren<Animator> ();
		if(animator){
			if (animator.runtimeAnimatorController.name == "eye1") {
				animator.runtimeAnimatorController	= wind1 [Random.Range (0, wind1.Length)];
			}
			if (animator.runtimeAnimatorController.name == "eye2") {
				animator.runtimeAnimatorController	= wind2 [Random.Range (0, wind2.Length)];
			}
			if (animator.runtimeAnimatorController.name == "eye3") {
				animator.runtimeAnimatorController	= wind3 [Random.Range (0, wind3.Length)];
			}
			if (animator.runtimeAnimatorController.name == "eye4") {
				animator.runtimeAnimatorController	= wind4 [Random.Range (0, wind4.Length)];
			}
			if (animator.runtimeAnimatorController.name == "eye5") {
				animator.runtimeAnimatorController	= wind5 [Random.Range (0, wind5.Length)];
			}

		}
	}
}
