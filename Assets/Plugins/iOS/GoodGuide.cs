using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class GoodGuide {
	[DllImport("__Internal")]
	static extern void _goodGuide();

	public static void GoodGameGuide() {
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			_goodGuide ();		
	}

}