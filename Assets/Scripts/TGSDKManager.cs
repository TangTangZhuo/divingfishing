﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Together;

public class TGSDKManager : MonoBehaviour {

	public static string doubleID = "tRT7YpGlTTqgckAIAi2";
	public static string tripleID = "eS8dNITBKtMrwUjjJCp";
	public static string forceID = "m8crylWHjGPrpRq4zeH";
	public static string turnID = "T3BwzKomNvymsuu2EsN";


	void Awake(){
		TGSDK.Initialize ("Hn4Xb1am9p7ZJ5CLu932");
		TGSDK.PreloadAd ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
