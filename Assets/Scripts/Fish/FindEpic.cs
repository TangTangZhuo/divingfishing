using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FindEpic : MonoBehaviour {
	
	public Text epicName;
	public Image epicImage;
	public Image writeMask;
	public Button backBtn;

    string epicStr = "";

	public void EpicPicture(string name,Sprite image,float time){
		epicName.text = name;
        epicStr = name;
		epicImage.sprite = image;
		writeMask.DOFade (0, time*0.8f).OnComplete (()=>{
			backBtn.interactable = true;
		});
	}

	public void OnBackBtn(){		
        if (epicStr == SubmarineController.Instance.lastEpicFishName)
        {
            SubmarineController.Instance.settlePop.SetActive(true);
        }
        Destroy (gameObject);       
    }


}

