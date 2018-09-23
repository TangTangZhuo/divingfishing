using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaIllustration : MonoBehaviour {

	public Sprite[] fishImage;
	public string[] fishName;
	public Sprite[] unusualImage;
	public Sprite[] unLockImage;

	// Use this for initialization
	void Start () {
		int level = PlayerPrefs.GetInt ("Level", 1);
		if (level == 1) {
			if (transform.name == "Area1") {
				ChangeArea ();
			}
		}if (level == 2) {
			if (transform.name == "Area2") {
				ChangeArea ();
			}
		}if (level == 3) {
			if (transform.name == "Area3") {
				ChangeArea ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeArea(){
		int index = 0;
		if (transform.name == "Area1") {
			index = 0;
		}if (transform.name == "Area2") {
			index = 10;
		}if (transform.name == "Area3") {
			index = 20;
		}
		Illustration.Instance.fishImage = fishImage;
		Illustration.Instance.fishName = fishName;
		Illustration.Instance.unusualImage = unusualImage;
		Illustration.Instance.unLockImage = unLockImage;
		Illustration.Instance.IllUpdate (index);
		transform.GetComponent<Image> ().color = new Color (0.5f, 0.5f, 0.5f);
		MultiHaptic.HapticMedium ();

	}

	public void ChangeColorToWrite(){
		transform.GetComponent<Image> ().color = new Color (1, 1, 1);
	}
}
