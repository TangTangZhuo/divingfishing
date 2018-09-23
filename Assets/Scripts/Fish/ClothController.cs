using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothController : MonoBehaviour {

	private static ClothController instance;
	public static ClothController Instance{
		get{return instance;}
	}

	void Awake(){
		instance = this;	
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Ready(){
		
	}

	public void Run(){
	}
}
