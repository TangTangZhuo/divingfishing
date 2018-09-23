using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {
	Rigidbody2D playerRig;
	public float force;
	public ParticleSystem destroyParticle;
	public Transform particleParent;
//	float movespeed;
	// Use this for initialization
	void Start () {
		playerRig = transform.GetComponent<Rigidbody2D> ();
//		movespeed = PlayerController.Instance.moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		//RaycastHit2D[] hits = Physics2D.RaycastAll (transform.position, Vector2.down, 0.5f);
		RaycastHit2D[] hits = Physics2D.CircleCastAll (transform.position, 0.17f, Vector2.down/6,0.3f);

		foreach (RaycastHit2D hit in hits) {
			if ((hit.transform.tag == "Step" || hit.transform.tag == "RedStep")&& playerRig.velocity.y < -8) {
				playerRig.velocity = Vector3.zero;
				playerRig.AddForce (Vector2.up * force);
				float screenWidth = (Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0)).x +
				                   Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, 0)).x) / 2;
				ParticleSystem particle = ParticleSystem.Instantiate (destroyParticle, 
					                          new Vector3 (screenWidth, hit.transform.position.y, 0),
					                          destroyParticle.transform.rotation, particleParent);
				Destroy (particle.gameObject, 3);
				DestroyWithTag (hit.transform, "Step");
			}
			else if (hit.transform.tag == "RedStep"&& playerRig.velocity.y >= -8){
				playerRig.velocity = Vector3.zero;
				playerRig.AddForce (Vector2.up * force*3);
			}
		}
		if (playerRig.velocity.y > 4) {
			playerRig.velocity = new Vector3 (playerRig.velocity.x, 4, 0);
		}
	}
		
	void DestroyWithTag(Transform trans,string tag){
		if (trans.tag == tag||trans.tag == "RedStep") {
			Transform obj = trans;
			while (obj.parent.tag == tag) {
				obj = obj.parent;
			}
			Destroy (obj.gameObject);
		}
	}


}
