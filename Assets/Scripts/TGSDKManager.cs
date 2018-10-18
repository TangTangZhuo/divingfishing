using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Together;

public class TGSDKManager : MonoBehaviour {
	#if UNITY_IOS
	public static string doubleID = "tRT7YpGlTTqgckAIAi2";
	public static string tripleID = "eS8dNITBKtMrwUjjJCp";

	void Awake(){
		TGSDK.Initialize ("Hn4Xb1am9p7ZJ5CLu932");
		TGSDK.PreloadAd ();
	}
	#elif UNITY_ANDROID
	public static string doubleID = "Kq2mxxpY8LAajjuKbOH";
	public static string tripleID = "JG1vdkGDbmImhHgNLAp";

	void Awake(){
		TGSDK.Initialize ("30Y8MHbl96lXc35uo2Wf");
		TGSDK.PreloadAd ();
	}
	#endif


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
