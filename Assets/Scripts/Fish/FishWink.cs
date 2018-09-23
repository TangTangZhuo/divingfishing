using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishWink : MonoBehaviour {
	
	public RuntimeAnimatorController[] wind1;
	public RuntimeAnimatorController[] wind2;
	public RuntimeAnimatorController[] wind3;
	public RuntimeAnimatorController[] wind4;
	public RuntimeAnimatorController[] wind5;

	public static FishWink Instance{
		get{return instance;}
	}
	private static FishWink instance;

	void Awake(){
		instance = this;
	}
}
