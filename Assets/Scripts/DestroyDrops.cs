using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDrops : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D collider){
		if (collider.tag == "drop") {
			if (collider.transform.parent != null &&collider.transform.parent.tag == "drop")
				Destroy (collider.transform.parent.gameObject);
			else {
				Destroy (collider.gameObject);
			}
		}
	}
}
