using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FishManager : MonoBehaviour {
	//史诗鱼图片
	public Sprite[] epicSprites;
	public string[] epicNames;
	public string[] epicNames_CH;
	public string[] epicNames_TW;

	static FishManager instance;
	public static FishManager Instance{
		get{ return instance;}
	}

	void Awake(){
		instance = this;
	}
}
