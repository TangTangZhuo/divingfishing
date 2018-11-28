using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FingerGuide : MonoBehaviour {

    public Transform finger;
    public Transform left;
    public Transform right;
    
	// Use this for initialization
	void OnEnable () {
        StartCoroutine(FingerMove(1.3f));
	}
	
    public void OnTouch(){
        gameObject.SetActive(false);
        PlayerPrefs.SetInt("fingerGuide", 1);
        ProgressManager.Instance.onStartButton();
    }
	
    IEnumerator FingerMove(float time){
        float leftx = left.localPosition.x;
        float rightx = right.localPosition.x;

        while(true){
            finger.DOLocalMoveX(leftx, time - 0.3f, false);
            finger.DOLocalRotate(new Vector3(0, 0, 40), time, RotateMode.Fast).OnComplete(()=>{
                finger.DOLocalRotate(new Vector3(0, 0, -40), time, RotateMode.Fast);
                finger.DOLocalMoveX(rightx, time - 0.3f, false);
            });

             yield return new WaitForSeconds(time*2);

        }
    }
}
