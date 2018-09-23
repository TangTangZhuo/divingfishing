using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour {
	bool isFished;
	float speed;
	float[] speeds;
	float leftScreen;
	float rightScreen;

	// Use this for initialization
	void Start () {
		speeds = new float[]{ 1.05f, 1.1f, 1.15f, 1.2f, 1.5f };
		speed = speeds [Random.Range (0, 5)];
		leftScreen = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		rightScreen = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 1)).x;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isFished ) {
			if (transform.eulerAngles.y == 180) {
				if (transform.position.x > rightScreen) {
					transform.eulerAngles = new Vector3 (0, 0, 0);
				}
				transform.Translate (speed * Time.deltaTime, 0, 0, Space.World);
			} else if (transform.eulerAngles.y == 0){
				if (transform.position.x < leftScreen) {
					transform.eulerAngles = new Vector3 (0, 180, 0);
				}
				transform.Translate (-speed * Time.deltaTime, 0, 0, Space.World);

			}
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Net" && !ProgressManager.Instance.isOvering && !ProgressManager.Instance.isReady) {
			isFished = true;
			Destroy (GetComponent<BoxCollider2D> ());
		}
	}
}
