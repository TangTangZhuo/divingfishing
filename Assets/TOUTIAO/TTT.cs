using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using LitJson;

public class TTT{
	[DllImport("__Internal")]
	static extern void _TTInit(string appid,string channel,string appname);

	[DllImport("__Internal")]
	static extern void _UploadPurchase(string content,string type,int payment,bool result);

	[DllImport("__Internal")]
	static extern void _UploadEvent(string name,string value);


	public static void TTInit(string appid,string channel,string appname) {
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			_TTInit(appid,channel,appname);
	}

	public static void UploadPurchase(string content,string type,int payment,bool result) {
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			_UploadPurchase (content,type,payment,result);	
	}
		
	public static void UploadEvent(string name,Dictionary<string,string> dic){
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			string value = JsonMapper.ToJson (dic);
			_UploadEvent (name, value);
		}
	}
		
}
