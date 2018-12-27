using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChristmasSrc : MonoBehaviour {
	public Sprite xup;
	public Transform fishItem;

	public Sprite xup_C;
	public Transform fishItem_C;

	private static ChristmasSrc instance;
	public static ChristmasSrc Instance{
		get{return instance;}
	}
	void Awake(){
		instance = this;	
	}
}
