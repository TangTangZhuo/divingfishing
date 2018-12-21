using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTranslate : MonoBehaviour {
	
	public string[] fishName_Ch1;
	public string[] fishName_TW1;
	public string[] fishName_Ch2;
	public string[] fishName_TW2;
	public string[] fishName_Ch3;
	public string[] fishName_TW3;
	public string[] fishName_Ch4;
	public string[] fishName_TW4;

	private static FishTranslate instance;
	public static FishTranslate Instance{
		get{return instance;}
	}
	void Awake(){
		instance = this;	
	}
		
}
