using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Common;

public class UIMessage : MonoBehaviour {
	public Text Title;
	public Text Content;
	public Button Sure;
	public Button Double;
	public Button BGMask;
	void Start()
	{
		Sure.onClick.AddListener(MessageBox.Sure);
		Double.onClick.AddListener(MessageBox.Double);
		Title.text =MessageBox.TitleStr;
		Content.text = MessageBox.ContentStr;
		if (BGMask!=null) {
			BGMask.onClick.AddListener (MessageBox.Double);
		}
	}
}