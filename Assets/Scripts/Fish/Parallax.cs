using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	public Transform[] backgrounds;
	public float parallaxScale;
	public float parallaxReductionFactor;
	public float smoothing;

	private Transform cam;
	private Vector3 previousCamPos;

	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;
		previousCamPos = cam.position;
	}
	
	// Update is called once per frame
	void Update () {
		float parallax = (previousCamPos.y - cam.position.y) * parallaxScale;

		for (int i = 0; i < backgrounds.Length; i++) {
			float bgTargetPosY = backgrounds [i].position.y + parallax * (i * parallaxReductionFactor + 1);
			Vector3 bgTargetPos = new Vector3 (backgrounds [i].position.x, bgTargetPosY, backgrounds [i].position.z);
			backgrounds [i].position = Vector3.Lerp (backgrounds [i].position, bgTargetPos, smoothing * Time.deltaTime);
		}

		previousCamPos = cam.position;
	}
}
