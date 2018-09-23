using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NetController : MonoBehaviour {
	public Transform player;
	public float xspeed;
	public float yspeed;
	public float roSpeed;

	Rigidbody2D playerRig;
	bool isOver;
	float distance;
	float rotate;
	bool isNetDoMove;
	// Use this for initialization
	void Start () {
		rotate = 0;
		isOver = false;
		isNetDoMove = false;
		playerRig = player.GetComponent<Rigidbody2D> ();
		distance = Vector3.Distance (transform.position, player.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (ProgressManager.Instance.isRunning) {
			lookat (player);
			if (Mathf.Abs( transform.position.x - player.position.x) > 0.1) {
				transform.DOMoveX (player.position.x, 0.3f, false);
			}
			transform.position = new Vector3 (transform.position.x, player.position.y + distance, 0);
		} else if(ProgressManager.Instance.isOver) {
			SubmarineController.Instance.moveSpeed = 0;
			if (!isOver) {
				transform.DOKill(false);
				isOver = true;
			}
			if (isOver) {
				if (rotate <= 180) {
					playerRig.velocity = Vector3.zero;
					SubmarineController.Instance.gravityScale = 0;
					transform.RotateAround (player.position, -player.right, roSpeed);
					rotate += roSpeed;
					ProgressManager.Instance.isOvering = true;
				} else  {
					transform.localEulerAngles = new Vector3 (165, 0,  -180);
					SubmarineController.Instance.gravityScale = player.position.y / 5;
					transform.position = new Vector3 (player.position.x, player.position.y - distance,transform.position.z );
					ProgressManager.Instance.isOvering = false;
					Transform net = transform.Find ("net");
					if (!isNetDoMove) {
						net.DOLocalMoveZ (-0.5f, 0.5f, false);
						net.DOLocalMoveY (net.localPosition.y - 0.3f, 0.5f, false);
//						net.DOPunchPosition (Vector3.down/5, 5, 7, 0, false);
						isNetDoMove = true;
					}
//					if (net.position.y > -10) {
//						net.DOKill (false);
//					}
					//net.DOPunchPosition (transform.position + Vector3.down, 10, 10, 1, false);
					//net.localPosition = new Vector3 (net.localPosition.x + 1f, net.localPosition.y, net.localPosition.z);
				}
			}
		}else if (ProgressManager.Instance.isReady) {

		}
	}
		

	void lookat(Transform trans){
		// 世界坐标系转到本地坐标系
		Vector3 relative = transform.InverseTransformPoint(trans.position);
		// 归一化（并不是必须的）
		relative.Normalize();

		// 反正切求弧并转成角度
		float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
		angle = Mathf.Abs(angle);

		// 左右2侧的垂直向量
		Vector3 leftVertical = new Vector3(-1, 0, 0);
		Vector3 rightVertical = new Vector3(1, 0, 0);
		// 跟目标向量做点积
		float leftDot = Vector3.Dot(relative, leftVertical);
		float rightDot = Vector3.Dot(relative, rightVertical);

		// >= 0 说明夹角 <= 90
		if (leftDot >= 0)
		{
			transform.Rotate(0, 0, angle);
		}
		else if (rightDot >= 0)
		{
			transform.Rotate(0, 0, -angle);
		}
		else
		{
			Debug.LogError("dot有问题");
		}


	}
}
