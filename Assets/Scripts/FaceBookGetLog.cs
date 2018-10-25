using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FaceBookGetLog {

	public static void LogFirstLevel2Event () {		
		FB.LogAppEvent(	
			"FirstLevel2",
			default(float?),
			null
		);
		Debug.Log ("LogFirstLevel2Event");
	}

	public static void LogFirstLevel3Event () {
		FB.LogAppEvent(
			"FirstLevel3",
			default(float?),
			null
		);
		Debug.Log ("LogFirstLevel3Event");
	}

	public static void LogFinishLevel1Event () {
		FB.LogAppEvent(
			"CompleteLevel1",
			default(float?),
			null
		);
		Debug.Log ("LogFinishLevel1Event");
	}

	public static void LogFinishLevel2Event () {
		FB.LogAppEvent(
			"CompleteLevel2",
			default(float?),
			null
		);
		Debug.Log ("LogFinishLevel2Event");
	}

	public static void LogFinishLevel3Event () {
		FB.LogAppEvent(
			"CompleteLevel3",
			default(float?),
			null
		);
		Debug.Log ("LogFinishLevel3Event");
	}
}
