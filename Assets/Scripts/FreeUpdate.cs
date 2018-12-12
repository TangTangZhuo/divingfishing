using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Together;

public class FreeUpdate : MonoBehaviour {
    public Sprite freePicture;
    public Image depthImage;
    public Button depthButton;
    public Text price;

    private static FreeUpdate instance;
    public static FreeUpdate Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }

    IEnumerator coroutine;

    public void UpdateFree()
    {
    
        depthButton.onClick.AddListener(Upgrading.Instance.OnDepthClick);

        if(PlayerPrefs.GetInt("Level", 1) == 1 && PlayerPrefs.GetInt("freeDepth1",0)>=5)
        {
            UpdateFreeBtn("freeDepth1");
        }
        if(PlayerPrefs.GetInt("Level", 1) == 2 && PlayerPrefs.GetInt("freeDepth2", 0) >= 10)
        {
            UpdateFreeBtn("freeDepth2");
        }
        if (PlayerPrefs.GetInt("Level", 1) == 3 && PlayerPrefs.GetInt("freeDepth3", 0) >= 15)
        {
            UpdateFreeBtn("freeDepth3");
        }
    }

    void UpdateFreeBtn(string level){
        coroutine = PunchSelf();
        depthButton.interactable = true;
		Sprite curSprite = depthImage.sprite;
        depthImage.sprite = freePicture;
        depthImage.color = new Color(1, 1, 1, 1);
        //price.text = "Free";
		if (Application.systemLanguage == SystemLanguage.English) {
			price.text = "Free";
		} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
			price.text = "免费";
		}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
			price.text = "免費";
		}
        StartCoroutine(coroutine);
        PlayerPrefs.SetInt(level, 0);
        depthButton.onClick.RemoveAllListeners();
        depthButton.onClick.AddListener(() => {			
            if(TGSDK.CouldShowAd(TGSDKManager.FreeId)){
                TGSDK.ShowAd(TGSDKManager.FreeId);
                StopCoroutine(coroutine);
                TGSDK.AdCloseCallback = (string obj) => {
                    Upgrading.Instance.FreeClick();
					depthImage.sprite = curSprite;
					depthButton.onClick.RemoveAllListeners();
					depthButton.onClick.AddListener(Upgrading.Instance.OnDepthClick);
                };
            }
            else{
				if (Application.systemLanguage == SystemLanguage.English) {
					TipPop.GenerateTip("no ads", 0.5f);
				} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified||Application.systemLanguage == SystemLanguage.Chinese) {			
					TipPop.GenerateTip("广告不可播放", 0.5f);
				}else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
					TipPop.GenerateTip("廣告不可播放", 0.5f);
				}
            }
        });
    }

    IEnumerator PunchSelf()
    {
        Transform priceTrans = price.transform;
        while (true)
        {
            if (priceTrans)
            {
                priceTrans.DOPunchScale(Vector3.one * 0.2f, 0.3f, 10, 1);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
