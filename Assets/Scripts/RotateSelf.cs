using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RotateSelf : MonoBehaviour {

	public GameObject turnTable;
    public GameObject turnTip;
	public Text CountDown;

    private static RotateSelf instance;
    public static RotateSelf Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void OnEnable () {

        if(Timer.Instance && Timer.Instance.timeInt<=60&&(PlayerPrefs.GetInt("TurnTip",0)==1)){
            turnTip.SetActive(true);
            PlayerPrefs.SetInt("TurnTip", 0);
        }

		StartCoroutine (Rotate ());
		Invoke ("GetCountDownText", 0.1f);

	}

	void GetCountDownText(){
		Timer.Instance.GetCountDownText(CountDown);
		turnTable.GetComponent<TurnTable> ().UpdateBoost ();
	}
	
	IEnumerator Rotate(){
		yield return new WaitForSeconds (2);
		while (true) {
			transform.DORotate (transform.eulerAngles + new Vector3 (0,0,1720), 2f, RotateMode.FastBeyond360);
			yield return new WaitForSeconds (4);
		}
	}

	public void OnTurnBtn(){
		turnTable.SetActive (true);
	}

}
