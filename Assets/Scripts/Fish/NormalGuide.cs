using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGuide : MonoBehaviour {

    public GameObject[] guide;


    private static NormalGuide instance;
    public static NormalGuide Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    public void NormalGuideStart () {
        if(PlayerPrefs.GetInt("fish10", 0)==1 && PlayerPrefs.GetInt("Lock2", 0)==0){
            if(PlayerPrefs.GetInt("MapGuide", 0)==0){
                guide[2].SetActive(true);
                PlayerPrefs.SetInt("MapGuide", 1);
            }
        }

        if (PlayerPrefs.GetInt("IlluGuide", 0) == 1)
        {
            if (PlayerPrefs.GetInt("TurnGuide", 0) == 0)
            {
                guide[3].SetActive(true);
                PlayerPrefs.SetInt("TurnGuide", 1);
            }
        }

        if (PlayerPrefs.GetInt("DepthGuide", 0)==1){
            if(PlayerPrefs.GetInt("IlluGuide", 0)==0){
                guide[1].SetActive(true);
                PlayerPrefs.SetInt("IlluGuide", 1);
            }
        }


	}
	
    
    public void OnDepthGuideBtn(){
        guide[0].SetActive(false);
        Upgrading.Instance.OnDepthClick();
    }

    public void OnIlluGuideBtn()
    {
        guide[1].SetActive(false);
        Illustration.Instance.OnIllBtnClick();
    }

    public void OnMapGuideBtn()
    {
        guide[2].SetActive(false);
        Map.Instance.OnMapBtn();
       
    }

    public void OnTurnGuideBtn()
    {
        guide[3].SetActive(false);
        RotateSelf.Instance.OnTurnBtn();
        PlayerPrefs.SetInt("TurnGuideFinish", 1);
    }
}
