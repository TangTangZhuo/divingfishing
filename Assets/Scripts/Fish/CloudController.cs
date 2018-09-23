using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {
	public Transform[] clouds;
	public float speed;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < clouds.Length; i++) {
			clouds [i].position += new Vector3 (Random.Range (-1f, 1f), 0, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < clouds.Length; i++) {
			clouds [i].Translate (new Vector3 (speed*(i+1) * Time.deltaTime, 0, 0));
			if (clouds [i].position.x < -9) {
				clouds [i].position = new Vector3 (1, clouds [i].position.y, clouds [i].position.z);
			}
		}
	}
		
}
