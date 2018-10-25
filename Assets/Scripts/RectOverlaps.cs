using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectOverlaps : MonoBehaviour {
	bool isDestroy = false;
	// Use this for initialization
	void Start () {
		
	}
		
	void OnTriggerEnter2D(Collider2D coll){
		if (!SubmarineController.Instance.isSettle) {
			if (!isDestroy) {
				if (coll.tag == "score") {
					double scale = transform.localScale.x;
					double collScale = coll.transform.localScale.x;

					if (scale > collScale) {
						isDestroy = true;
						transform.tag = "Untagged";
						Destroy (gameObject);

					}
				}
			}
		}
	}

}
