using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepGenerate : MonoBehaviour {
	public Transform[] steps;
	public Transform preStep;
	public Transform end;
//	int currentLevel;
	float leftScreen;
//	float rightScreen;
	float startPos;
	bool isFinishGenerate;

	int[] stepsNumber;

	// Use this for initialization
	void Start () {
//		currentLevel = 30;
//		int abc = GetStepNumber (currentLevel) [0];
//		int de = GetStepNumber (currentLevel) [1];
		//stepsNumber = new int[]{abc, abc, abc, de, de};
		startPos = preStep.position.y;
		stepsNumber = new int[]{4, 4, 4, 4, 4,4,4,4,4};
		isFinishGenerate = false;
		leftScreen = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
//		rightScreen = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x;
	}
	
	// Update is called once per frame
	void Update () {
//		if (isFinishGenerate == false) {
//			if (stepsNumber [0] != 0 || stepsNumber [1] != 0 || stepsNumber [2] != 0 || stepsNumber [3] != 0 || stepsNumber [4] != 0) {
//				int index = Random.Range (0, stepsNumber.Length);
//				if (stepsNumber [index] > 0) {
//					GenerateStep (index);
//					stepsNumber [index] -= 1;
//				}
//			} else {
//				isFinishGenerate = true;
//				Transform theEnd = GameObject.Instantiate (end,transform);
//				theEnd.position = new Vector3 (leftScreen, preStep.position.y - 3.4f);
//			}
//		}
		if (isFinishGenerate == false) {
			if (stepsNumber [0] != 0 || stepsNumber [1] != 0 || stepsNumber [2] != 0 || stepsNumber [3] != 0 || stepsNumber [4] != 0
				|| stepsNumber [5] != 0 || stepsNumber [6] != 0 || stepsNumber [7] != 0 || stepsNumber [8] != 0) {
				int index = Random.Range (0, stepsNumber.Length);
				if (stepsNumber [index] > 0) {
					GenerateStep (index);
					stepsNumber [index] -= 1;
				}
			} else {
				isFinishGenerate = true;
				Transform theEnd = GameObject.Instantiate (end,transform);
				ProgressManager.Instance.progressSlider.maxValue = startPos;
				ProgressManager.Instance.progressSlider.minValue = preStep.position.y - 3.4f;
				ProgressManager.Instance.sliderBad.maxValue = startPos;
				ProgressManager.Instance.sliderBad.minValue = preStep.position.y - 3.4f;
				theEnd.position = new Vector3 (leftScreen, preStep.position.y - 3.4f);
			}
		}
	}

	//返回各类障碍数量，A=B=C,D=E
	int[] GetStepNumber(int level){
		int stepSum = level / 5 + 25;
		if (level < 10 && level > 0) {
			return new int[]{ (int)(0.3f * stepSum), (int)(0.05f * stepSum) };
		} else if (level < 20) {
			return new int[]{ (int)(0.27f * stepSum), (int)(0.1f * stepSum) };
		}else if (level < 30) {
			return new int[]{ (int)(0.23f * stepSum), (int)(0.15f * stepSum) };
		}else if (level < 40) {
			return new int[]{ (int)(0.2f * stepSum), (int)(0.2f * stepSum) };
		}else if (level < 50) {
			return new int[]{ (int)(0.17f * stepSum), (int)(0.25f * stepSum) };
		}else  {
			return new int[]{ (int)(0.13f * stepSum), (int)(0.3f * stepSum) };
		}
	} 

	//生成对应类型的台阶
	void GenerateStep(int num){
//		Transform step = GameObject.Instantiate (steps [num],transform);
//		if (num == 3) {
//			if (Random.Range (0, 2) == 1) {
//				step.position = new Vector3 (leftScreen, preStep.position.y - 3.4f);
//			} else {
//				step.position = new Vector3 (rightScreen, preStep.position.y - 3.4f);
//			}
//		} else {
//			step.position = new Vector3 (leftScreen + Random.value * 3.6f, preStep.position.y - 3.4f);
//		}
		Transform step = GameObject.Instantiate (steps [num],transform);

		step.position = new Vector3 (leftScreen, preStep.position.y - 5.2f);

		preStep = step;
	}
}
