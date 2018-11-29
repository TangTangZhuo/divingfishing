using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthCrack : MonoBehaviour
{
    public InputField input;


    // Update is called once per frame
    void Update()
    {

    }

    public void OnBackBtn()
    {
        gameObject.SetActive(false);
    }

    public void OnValueChange()
    {
        if (input.text != "" && !input.text.Contains("-"))
        {
            int depth = int.Parse(input.text);
            if (depth >= 17 && depth <= 300)
            {
                Upgrading.Instance.ChangeDepth(depth);
            }
        }
    }


}
