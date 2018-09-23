using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmanager : MonoBehaviour {
	Color[] bgColors;
	int index;

	public GameObject waterF;
	public GameObject bubble;
	public Transform[] parallax1;
	public Transform[] parallax2;
	public Transform[] parallax3;
	public Transform[] parallax4;
	private Transform[][] parallax;

	public Transform[] parallaxBg;

	float leftScreen;
	float rightScreen;

	Transform bg;

	private static BGmanager instance;
	public static BGmanager Instance{
		get{return instance;}
	}

	void Awake(){
		instance = this;	
	}

	// Use this for initialization
	void Start () {
		parallax = new Transform[][]{ parallax1, parallax2, parallax3, parallax4 };
		leftScreen = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		rightScreen = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 1)).x;

		index = 0;
		bg = GameObject.Find ("SprayBackround").transform;
		bgColors = new Color[] {new Color (152 / 255f, 242 / 255f, 255 / 255f), new Color (22 / 255f, 194 / 255f, 219 / 255f),
			new Color (0 / 255f, 95 / 255f, 209 / 255f), new Color (27 / 255f, 50 / 255f, 148 / 255f), new Color (35 / 255f, 51 / 255f, 142 / 255f),
			new Color (63 / 255f, 32 / 255f, 123 / 255f), new Color (71 / 255f, 16 / 255f, 117 / 255f), new Color (85 / 255f, 0 / 255f, 93 / 255f),
			new Color (99 / 255f, 1 / 255f, 45 / 255f), new Color (111 / 255f, 2 / 255f, 2 / 255f)
		};
		Camera.main.backgroundColor = bgColors [index];
	}


	// Update is called once per frame
	void Update () {
		
		//背景颜色渐变
		index = (int)(SubmarineController.Instance.transform.position.y / -17);
		if (index > bgColors.Length - 1) {
			index = bgColors.Length - 1;
		}
		Camera.main.backgroundColor = Color.Lerp (Camera.main.backgroundColor, bgColors [index], 0.5f*Time.deltaTime);


	}

	public void GenerateWaterF(){
		Transform startPos = FishGenerate.Instance.startPosition;
		int Count = UIManager.Instance.diveDepth / -20;
		float screenMid = startPos.position.x;
		float baseGeneratePosy = -21 ;
		for (int i = 0; i < Count; i++) {			
			float offset = Random.Range (-5, 6);
			GameObject.Instantiate (waterF, new Vector3 (screenMid, baseGeneratePosy + offset ,0),waterF.transform.rotation,bg);
			baseGeneratePosy -= 20;
		}
	}

	public void GenerateBubble(){
		Transform startPos = FishGenerate.Instance.startPosition;
		int Count = UIManager.Instance.diveDepth / -10;
		float screenMid = startPos.position.x;
		float baseGeneratePosy = -20 ;
		for (int i = 0; i < Count; i++) {			
			float offset = Random.Range (-5, 6);
			GameObject.Instantiate (bubble, new Vector3 (screenMid+Random.Range(-3f,3f), baseGeneratePosy + offset ,0),bubble.transform.rotation,bg);
			baseGeneratePosy -= 8;
		}
	}

	public void GenerateParallx(){
		int parallaxIndexTemp=0;
		int stoneIndexTemp=0;
		int parallaxIndex =0;
		int stoneIndex = 0;

		int Count = UIManager.Instance.diveDepth / -2;
		float baseGeneratePosy = -20 ;

		for (int i = 0; i < Count; i++) {			
			float offset = Random.Range (-2, 2);
			while (true) {
				parallaxIndexTemp = Random.Range (0, parallax.Length);
				if (parallaxIndex != parallaxIndexTemp) {
					parallaxIndex = parallaxIndexTemp;
					break;
				}
			}
			Transform[] getParallax = parallax [parallaxIndex];
			while (true) {
				stoneIndexTemp = Random.Range (0, getParallax.Length);
				if (stoneIndex != stoneIndexTemp) {
					stoneIndex = stoneIndexTemp;
					break;
				}
			}

			Transform trans = getParallax [stoneIndex];
			if (trans.name.StartsWith ("l_")) {
				GameObject.Instantiate (trans, new Vector3 (leftScreen - 0.03f, baseGeneratePosy + offset, parallaxIndex - 5), trans.transform.rotation, parallaxBg [parallaxIndex]);
			} else {
				GameObject.Instantiate (trans, new Vector3 (rightScreen + 0.03f, baseGeneratePosy + offset, parallaxIndex - 5), trans.transform.rotation, parallaxBg [parallaxIndex]);
			}
		
			
			baseGeneratePosy -= 4;

			if (PlayerPrefs.GetInt ("Level", 1) != 1) {
				baseGeneratePosy -= 4;
			}

		}
	}
}
