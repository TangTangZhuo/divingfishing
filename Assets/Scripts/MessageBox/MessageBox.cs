using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Common
{
	public delegate void Confim();
	public delegate void Cancle();
	public delegate void DoubleR();
	public class MessageBox 
	{
		static GameObject Messagebox;
//		static int Result = -1;
		public static Confim confim;
		public static Cancle cancle;
		public static DoubleR doubleR;
		public static string TitleStr;
		public static string ContentStr;

		public static void Show(string content)
		{          
			ContentStr = content;
			Messagebox = (GameObject)Resources.Load("PopBG");
			Messagebox= GameObject.Instantiate(Messagebox,GameObject.Find("Canvas").transform) as GameObject;
			Messagebox.transform.DOScale (1, 0.3f);
			Messagebox.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
			Messagebox.GetComponent<RectTransform>().offsetMin = Vector2.zero;
			Messagebox.GetComponent<RectTransform>().offsetMax = Vector2.zero;      
			//Time.timeScale = 0;      
		}
		public static void Show(string title,string content)
		{
			TitleStr = title;
			ContentStr = content;
			Messagebox = (GameObject)Resources.Load("PopBG");
			Messagebox = GameObject.Instantiate(Messagebox, GameObject.Find("Canvas").transform) as GameObject;
			Messagebox.transform.DOScale (1, 0.1f);
			Messagebox.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
			Messagebox.GetComponent<RectTransform>().offsetMin = Vector2.zero;
			Messagebox.GetComponent<RectTransform>().offsetMax = Vector2.zero;    
			//Time.timeScale = 0;     
		}

		public static void Show(string title,string content,int box)
		{
			TitleStr = title;
			ContentStr = content;
			if (box == 1) {
				Messagebox = (GameObject)Resources.Load ("PopBG");
			}
			if (box == 2) {
				Messagebox = (GameObject)Resources.Load ("PurchasePop");
			}
			Messagebox = GameObject.Instantiate(Messagebox, GameObject.Find("Canvas").transform) as GameObject;
			Messagebox.transform.DOScale (1, 0.3f);
		//	Messagebox.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
		//	Messagebox.GetComponent<RectTransform>().offsetMin = Vector2.zero;
		//	Messagebox.GetComponent<RectTransform>().offsetMax = Vector2.zero;    
			//Time.timeScale = 0;     
		}

		public static void Sure()
		{
			if (confim!= null)
			{
				MultiHaptic.HapticMedium ();
				confim();
				GameObject.Destroy(Messagebox);
				TitleStr = "标题";
				ContentStr = null;
				//Time.timeScale = 1;
			}
		}     
		public static void Double()
		{
			MultiHaptic.HapticMedium ();
			if (Messagebox.tag == "PopBG") {
				if (doubleR != null) {
					doubleR ();
					GameObject.Destroy (Messagebox);
				}
			}
			if (Messagebox.tag == "PurchasePop") {	
				if (cancle != null) {
					cancle ();

				}
				GameObject.Destroy (Messagebox);
				
			}
//			Result = 2;
//			GameObject.Destroy(Messagebox);
//			TitleStr = "标题";
//			ContentStr = null;
			//Time.timeScale = 1;
		}

	}
}