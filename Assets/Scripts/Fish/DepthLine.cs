using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthLine : MonoBehaviour {

    public GameObject Line;

    static DepthLine instance;
    public static DepthLine Instance{
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
    }

    public void GenerateLine()
    {
        int depth = UIManager.Instance.diveDepth;
        int lineCount = depth / -15;
        for (int i = 1; i < lineCount+1;i++){
            GameObject go = Instantiate(Line, transform);
            int length = i * -15;
            go.transform.localPosition += new Vector3(0, length, 0);
            go.transform.GetComponentInChildren<TextMesh>().text = length + "M";
        }
    }
}
