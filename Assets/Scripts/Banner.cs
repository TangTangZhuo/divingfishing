using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Together;

public class Banner : MonoBehaviour {
	#if UNITY_IOS
	public const string bannerID="CkCtraYsEduuE1PURsI";
	#endif

	void Start () {
		if (PlayerPrefs.GetInt ("fishingpass", 0) == 0 && PlayerPrefs.GetInt ("no_ads", 0) == 0) {
			TGSDK.SetBannerConfig (bannerID, "TGBannerNormal", 0, Screen.height-Screen.height / 12, Screen.width, Screen.height / 12, 30);
			if (TGSDK.CouldShowAd (bannerID)) {
				TGSDK.ShowAd (bannerID);
				Debug.Log ("TGBANNERFinish");
			}
			GameObject[] Gos = GameObject.FindGameObjectsWithTag ("bannerUI");
			for (int i = 0; i < Gos.Length; i++) {
				Gos [i].transform.position += new Vector3 (0, 50, 0);
			}
			TGSDK.BannerFailedCallback = (string m1, string m2, string m3) => {
				TGSDK.CloseBanner (bannerID);
				Invoke ("ShowBanner", 4);
			};
				
		}
	}

	void ShowBanner(){
		if (TGSDK.CouldShowAd (bannerID)) {
			TGSDK.ShowAd (bannerID);
			Debug.Log ("TGBANNERFinish");
		}
	}		


		
}
