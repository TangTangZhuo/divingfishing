using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour {
	public Transform vertex1;
	public Transform vertex2;
	public float with;
	LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.startWidth = with;
		lineRenderer.endWidth = with;
		lineRenderer.positionCount = 2;

	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer.SetPosition (0, vertex1.position);
		lineRenderer.SetPosition (1, transform.position);
	}
}
