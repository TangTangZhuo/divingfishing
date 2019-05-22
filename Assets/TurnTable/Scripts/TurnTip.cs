using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTip : MonoBehaviour {

    public void OnBackBtn(){
        gameObject.SetActive(false);
		TTADManager.Instance.Cancel_Award ("TurnTable_Continue", 1);
    }


}
