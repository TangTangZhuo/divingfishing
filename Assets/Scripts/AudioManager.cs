using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public GameObject epicAudio;
	public GameObject fishAudio;
	public GameObject clickAudio;
	public GameObject goldAudio;
	public GameObject waterAudio;


	private static AudioManager instance;
	public static AudioManager Instance{
		get{return instance;}
	}

	void Awake(){
		instance = this;
	}

	
}
