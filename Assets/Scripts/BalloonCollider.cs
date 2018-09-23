using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonCollider : MonoBehaviour {
	float screenWith;
	float offSetx;
	public Transform chain;
	public Transform player;

	public PlayerDead playerDead;
	// Use this for initialization
	void Start () {
		screenWith = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0)).x - 
					 Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, 0)).x;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Storm") {
			PlayerDead.Instance.GameOver ();
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		//屏幕左右互通
		offSetx = Mathf.Abs (player.position.x - transform.position.x);
		float offSetBalloon = transform.position.x - collider.transform.position.x;
		if (collider.tag == "BoundaryL") {
			if(offSetBalloon<0)
			chain.position += new Vector3 (screenWith+offSetx, 0);
		}
		if (collider.tag == "BoundaryR") {
			if(offSetBalloon>0)
			chain.position -= new Vector3 (screenWith+offSetx, 0);		
		}
		if (collider.tag == "End") {
			ProgressManager.Instance.GameWin ();
		}

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Step"||coll.gameObject.tag =="drop"||coll.gameObject.tag =="RedStep") {
			if (playerDead.isDead) {
				playerDead.GameOver ();
			}
		}
	}
}
