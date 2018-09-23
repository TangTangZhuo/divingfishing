using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform player;
	public float offset = 0;
	public bool isMove;

	private static CameraController instance;
	public static CameraController Instance{
		get{return instance;}
	}

	void Awake(){
		instance = this;	
	}
	// Use this for initialization
	void Start () {
		isMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMove)
			transform.position = new Vector3 (transform.position.x, player.position.y + offset, transform.position.z);
	}
}
