using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FishManager : MonoBehaviour {
	//史诗鱼图片
	public Sprite[] epicSprites;
	public string[] epicNames;

	static FishManager instance;
	public static FishManager Instance{
		get{ return instance;}
	}

	void Awake(){
		instance = this;
	}
}
