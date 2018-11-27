using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenjinManager : MonoBehaviour {
    BaseTenjin instance;

    void Start () {
        TenjinInit ();
    }

    void OnApplicationPause(bool pauseStatus){
        if (pauseStatus) {
            //do nothing
        }
        else {
            TenjinInit ();
        }
    }

    void TenjinInit(){
        instance = Tenjin.getInstance("SRKZX22EFVNAVDQJ5QZC5WJVWNMWLQ2N");
        instance.Connect();
    }
}
