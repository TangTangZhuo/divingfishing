using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {
	public GameObject particle1;
	public GameObject particle2;
	public Rigidbody2D player;

	private float speed1;
	private float speed2;

	// Use this for initialization
	void Start () {
		speed1 = -6;
		speed2 = -8;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.velocity.y < speed1 && particle1.activeSelf == false) {
			particle1.SetActive (true);
		} else if (player.velocity.y < speed2 && particle2.activeSelf == false) {
			particle2.SetActive (true);
		} else if(player.velocity.y >= speed1 && particle1.activeSelf == true) {
			particle1.SetActive (false);
			particle2.SetActive (false);
		}
	}
}
