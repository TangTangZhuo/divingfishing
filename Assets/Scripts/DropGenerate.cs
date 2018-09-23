using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGenerate : MonoBehaviour {
	public Transform[] drops;
	public Transform balloon;
	private int[] dropNum;
	public bool isGenerate;

	float time = 0;
	float leftScreen;
	float rightScreen;
	float midScreen;
//	int index ;

	private static DropGenerate instance;
	public static DropGenerate Instance{
		get{return instance;}
	}

	void Awake(){
		instance = this;	
	}
	// Use this for initialization
	void Start () {
//		index = 0;
		dropNum = new int[]{0,2,5,7,9,10};
		isGenerate = false;
		leftScreen = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		rightScreen = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x;
		midScreen = (leftScreen + rightScreen) / 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (isGenerate) {
			if (time < 3) {
				time += Time.deltaTime;
			} else {
				//SendMessage ("GenerateByTime" + dropNum [Random.Range(0,dropNum.Length)]);
				SendMessage ("GenerateByTime" + dropNum [1]);
//				index++;
//				if (index == dropNum.Length)
//					index = 0;
				time = 0;
			}
		}
	}

	//方块(碰撞炸裂)
	void GenerateByTime0(){
		for (int i=0; i < 10; i++) {
			Transform drop = GameObject.Instantiate (drops [0],transform);
			//drop.position = balloon.position + new Vector3 (0, 2, 0);
			drop.position = GetBalloonPos();
		}
	}

	//脚丫
//	void GenerateByTime1(){
//		for (int i=0; i < 10; i++) {
//			Transform drop = GameObject.Instantiate (drops [0]);
//			drop.position = balloon.position + new Vector3 (0, 2, 0);
//		}
//	}

	//垃圾桶(大范围)
	void GenerateByTime2(){
		for (int i=0; i < 10; i++) {
			Transform drop = GameObject.Instantiate (drops [2],transform);
			//drop.position = balloon.position + new Vector3 (0, 2, 0);
			drop.position = GetBalloonPos();
		}
	}

	//凹四边形
//	void GenerateByTime3(){
//		Transform drop = GameObject.Instantiate (drops [3],transform);
//		drop.position = balloon.position + new Vector3 (0, 2, 0);
//		for (int i=1; i < 5; i++) {
//			Transform dropl = GameObject.Instantiate (drops [3],transform);
//			dropl.position = balloon.position + new Vector3 (0, 2, 0) + new Vector3 (- i * 0.4f, i * 0.2f, 0);
//		}
//		for (int i=1; i < 5; i++) {
//			Transform dropr = GameObject.Instantiate (drops [3],transform);
//			dropr.position = balloon.position + new Vector3 (0, 2, 0) + new Vector3 (i * 0.4f, i * 0.2f, 0);
//		}
//	}

	//十字架
//	void GenerateByTime4(){
//		Transform drop = GameObject.Instantiate (drops [4],transform);
//		drop.position = balloon.position + new Vector3 (0, 2, 0);
//		for (int i=1; i < 4; i++) {
//			Transform dropl = GameObject.Instantiate (drops [4],transform);
//			dropl.position = balloon.position + new Vector3 (0, 2, 0) + new Vector3 (- i * 0.45f, i * 0.3f, 0);
//		}
//		for (int i=1; i < 4; i++) {
//			Transform dropr = GameObject.Instantiate (drops [4],transform);
//			dropr.position = balloon.position + new Vector3 (0, 2, 0) + new Vector3 (i * 0.45f, i * 0.3f, 0);
//		}
//		for (int i=1; i < 4; i++) {
//			Transform dropu = GameObject.Instantiate (drops [4],transform);
//			dropu.position = balloon.position + new Vector3 (0, 2, 0) + new Vector3 (0, i * 0.75f, 0);
//		}
//	}

	//钥匙
	void GenerateByTime5(){
		Transform drop = GameObject.Instantiate (drops [5],transform);
		//drop.position = balloon.position + new Vector3 (0, 2, 0);
		drop.position = GetBalloonPos();
		Transform dropl = GameObject.Instantiate (drops [5],transform);
		//dropl.position = balloon.position + new Vector3 (0, 2, 0)+ new Vector3 (-0.75f,-0.45f, 0);
		dropl.position = GetBalloonPos() + new Vector3 (-0.75f, -0.45f, 0);
		Transform dropr = GameObject.Instantiate (drops [5],transform);
		//dropr.position = balloon.position + new Vector3 (0, 2, 0)+ new Vector3 (0.75f,-0.45f, 0);
		dropr.position = GetBalloonPos() + new Vector3 (0.75f, -0.45f, 0);
	}

//	//枫叶
//	void GenerateByTime6(){
//		Transform drop = GameObject.Instantiate (drops [6],transform);
//		drop.position = balloon.position + new Vector3 (0, 2, 0);
//		for (int i = 1; i < 5; i++) {
//			Transform dropl = GameObject.Instantiate (drops [6],transform);
//			dropl.position = balloon.position + new Vector3 (0, 2, 0) + new Vector3 (-i/2 * 0.45f, i*1.5f * 0.3f, 0);
//		}
//	}

	//五角星(数量多)
	void GenerateByTime7(){
		Transform drop = GameObject.Instantiate (drops [7],transform);
		//drop.position = balloon.position + new Vector3 (0, 2, 0);
		drop.position = GetBalloonPos();
		for (int i = 1; i < 10; i++) {
			Transform dropl = GameObject.Instantiate (drops [7],transform);
			//dropl.position = balloon.position + new Vector3 (0, 2, 0) + new Vector3 (-i * 0.3f, 0.5f, 0);
			dropl.position = GetBalloonPos() + new Vector3 (-i * 0.3f, 0.5f, 0);
		}
		for (int i = 1; i < 10; i++) {
			Transform dropl = GameObject.Instantiate (drops [7],transform);
			//dropl.position = balloon.position + new Vector3 (0, 2, 0) + new Vector3 (-i * 0.3f,-0.5f, 0);
			dropl.position = GetBalloonPos() + new Vector3 (-i * 0.3f,-0.5f, 0);
		}
		for (int i = 1; i < 10; i++) {
			Transform dropr = GameObject.Instantiate (drops [7],transform);
			//dropr.position = balloon.position + new Vector3 (0, 2, 0) + new Vector3 (i * 0.3f, 0.5f, 0);
			dropr.position = GetBalloonPos() + new Vector3 (i * 0.3f, 0.5f, 0);
		}
		for (int i = 1; i < 10; i++) {
			Transform dropr = GameObject.Instantiate (drops [7],transform);
			//dropr.position = balloon.position + new Vector3 (0, 2, 0) + new Vector3 (i * 0.3f,-0.5f, 0);
			dropr.position = GetBalloonPos() + new Vector3 (i * 0.3f,-0.5f, 0);
		}
	}

	//鞋
//	void GenerateByTime8(){
//		Transform drops1 = GameObject.Instantiate (drops [8],transform);
//		drops1.position = balloon.position + new Vector3 (-1, 2, 0);
//		drops1.eulerAngles = drops1.eulerAngles + new Vector3 (0, 180, 0);
//		Transform drops2 = GameObject.Instantiate (drops [8],transform);
//		drops2.position = balloon.position + new Vector3 (1, 2, 0);
//
//		Transform drops3 = GameObject.Instantiate (drops [1],transform);
//		drops3.position = drops1.position + new Vector3 (-0.7f, 0.4f, 0);
//		drops3.eulerAngles = drops3.eulerAngles + new Vector3 (0, 180, 0);
//		Transform drops4 = GameObject.Instantiate (drops [1],transform);
//		drops4.position = drops2.position + new Vector3 (0.7f, 0.4f, 0);
//	}

	//三角形
	void GenerateByTime9(){
		for (int i=0; i < 10; i++) {
			Transform drop = GameObject.Instantiate (drops [9],transform);
			//drop.position = balloon.position + new Vector3 (0, 2, 0);
			drop.position = GetBalloonPos();
			drop.GetComponent<Rigidbody2D> ().AddTorque (40);
		}
	}

	//圆
	void GenerateByTime10(){
		for (int i=0; i < 15; i++) {
			Transform drop = GameObject.Instantiate (drops [10],transform);
			//drop.position = balloon.position + new Vector3 (0, 2, 0);
			drop.position = GetBalloonPos();

		}
	}

	Vector3 GetBalloonPos(){
		return(new Vector3 (midScreen, balloon.position.y + 1.5f, 0));
	}
}
