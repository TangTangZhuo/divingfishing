using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteDance.Union;

public class TTADManager : MonoBehaviour {
	private AdNative adNative;
	private RewardVideoAd rewardAd;

	public delegate void RewardFinishDe();
	public event RewardFinishDe RewardFinish;

	public delegate void RewardErrorDe();
	public event RewardErrorDe RewardError;

	public delegate void RewardShowDe();
	public event RewardShowDe RewardShow;

	[HideInInspector]
	public bool couldShow = false;

	string iosID;

	static TTADManager instance;
	public static TTADManager Instance{
		get { return instance;}
	}

	void Awake(){
		iosID = "917875662";
		instance = this;
		StartCoroutine(CheckLoadRewardAd());
	}

	//预加载广告
	IEnumerator CheckLoadRewardAd(){
		if (Application.platform != RuntimePlatform.IPhonePlayer) {
			yield break;
		}
		while (true) {
			if (!couldShow) {
				LoadRewardAd ();
			} else {
				yield break;
			}
			yield return new WaitForSeconds (1);
		}
	}

	//重置看广告的奖励类型
	public void CheckRewardEvent(){
		if (RewardFinish != null) {
			RewardFinish = null;
		}
	}

	//播放广告
	public void AutoShowReward(){
		LoadRewardAd();
		ShowRewardAd();
	}

	//关闭广告后获得对应奖励
	public void ExRewardFinish(){
		if (RewardFinish != null) {
			RewardFinish ();
			RewardFinish = null;
			DisposeAds();
			LoadRewardAd();
		}
	}

	public void AdShowError(){
		if (RewardError != null) {
			RewardError ();
			RewardError = null;
		}
	}

	public void AdShowSuccess(){
		if (RewardShow != null) {
			RewardShow ();
			RewardShow = null;
		}
	}

	//打点相关
	//广告展示是否成功,播放广告前调用
	public void Ad_Show_Event(string ad_position){
		RewardError = null;
		RewardShow = null;
		RewardError += () => {
			Dictionary<string,string> dic = new Dictionary<string, string> ();
			dic.Add ("ad_position", ad_position);
			dic.Add("is_success","no");
			TTT.UploadEvent ("ad_show", dic);
		};
		RewardShow += () => {
			Dictionary<string,string> dic = new Dictionary<string, string> ();
			dic.Add ("ad_position", ad_position);
			dic.Add("is_success","yes");
			TTT.UploadEvent ("ad_show", dic);
		};
	}
	//广告点击
	public void Ad_Button_Click(string ad_position){
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic.Add ("ad_position", ad_position);
		TTT.UploadEvent ("ad_button_click",dic);
	}
	//观看广告，关闭广告时调用
	public void Ad_View(string ad_position){
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic.Add ("is_completed", "yes");
		dic.Add ("ad_position", ad_position);
		TTT.UploadEvent ("ad_view",dic);
	}
	//获取金币
	public void Get_Coins(string number,string from){
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic.Add ("coin_amount", number);
		dic.Add ("get_from", from);
		TTT.UploadEvent ("get_coins", dic);
	}
	//解锁深度
	public void Map_Depth(string map,int depth){
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic.Add ("map_"+map, depth+"m");
		TTT.UploadEvent ("map_depth", dic);
	}
	//解锁地图
	public void Map_Unlock(string map){
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic.Add ("map", map);
		TTT.UploadEvent ("map_unlock", dic);
	} 
	//离线奖励
	public void Click_Award(string type){
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic.Add ("award_type", type);
		TTT.UploadEvent ("click_award", dic);
	}
	//关卡统计
	public void Quest(string maptype,string gold,string time,string depth){
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic.Add ("map_type", maptype);
		dic.Add ("gold_award", gold);
		dic.Add ("duration", time);
		dic.Add ("depth", depth);
		TTT.UploadEvent ("quest", dic);
	}
	//获取虚拟物品
	public void Get_Virtual_Items(string type,int amount,string name,string from){
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic.Add ("item_type", type);
		dic.Add ("item_amount", amount.ToString());
		dic.Add ("item_name", name);
		dic.Add ("get_form", from);
		TTT.UploadEvent ("get_virtual_items", dic);
	}
	//开始游戏
	public void Start_Game(){
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		TTT.UploadEvent ("start_game", dic);
	}
	//不需要奖励按钮
	public void Cancel_Award(string type,int amount){
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic.Add ("type", type);
		dic.Add ("amount", amount.ToString());
		TTT.UploadEvent ("cancel_award", dic);
	}

	private AdNative AdNative
	{
		get
		{
			if (this.adNative == null)
			{
				this.adNative = SDK.CreateAdNative();
			}

			return this.adNative;
		}
	}

	//广告配置
	public void LoadRewardAd()
	{
		if (this.rewardAd != null)
		{
			Debug.LogError("广告已经加载");
			return;
		}

		var adSlot = new AdSlot.Builder()
			#if UNITY_IOS
			.SetCodeId(iosID)
			#else
			.SetCodeId("901121430")
			#endif
			.SetSupportDeepLink(true)
			.SetImageAcceptedSize(1080, 1920)
			.SetRewardName("金币") // 奖励的名称
			.SetRewardAmount(3) // 奖励的数量
			.SetUserID("user123") // 用户id,必传参数
			.SetMediaExtra("media_extra") // 附加参数，可选
			.SetOrientation(AdOrientation.Horizontal) // 必填参数，期望视频的播放方向
			.Build();

		this.AdNative.LoadRewardVideoAd(
			adSlot, new RewardVideoAdListener(this));
	}

	/// <summary>
	/// Show the reward Ad.
	/// </summary>
	public void ShowRewardAd()
	{
		if (this.rewardAd == null)
		{
			Debug.LogError("请先加载广告");
			return;
		}
		this.rewardAd.ShowRewardVideoAd();
	}

	/// <summary>
	/// Dispose the reward Ad.
	/// </summary>
	public void DisposeAds()
	{
		#if UNITY_IOS
		if (this.rewardAd != null)
		{
			this.rewardAd.Dispose();
			this.rewardAd = null;
		}

		#else
		if (this.rewardAd != null)
		{
		this.rewardAd = null;
		}

		#endif
	}

	private sealed class RewardVideoAdListener : IRewardVideoAdListener
	{
		private TTADManager example;

		public RewardVideoAdListener(TTADManager example)
		{
			this.example = example;
		}

		public void OnError(int code, string message)
		{
			Debug.LogError("OnRewardError: " + message);
		}

		public void OnRewardVideoAdLoad(RewardVideoAd ad)
		{
			Debug.Log("OnRewardVideoAdLoad");

			ad.SetRewardAdInteractionListener(
				new RewardAdInteractionListener(this.example));
			ad.SetDownloadListener(
				new AppDownloadListener(this.example));

			this.example.rewardAd = ad;
			TTADManager.instance.couldShow = true;
		}

		public void OnRewardVideoCached()
		{
			Debug.Log("OnRewardVideoCached");
		}
	}

	private sealed class RewardAdInteractionListener : IRewardAdInteractionListener
	{
		private TTADManager example;

		public RewardAdInteractionListener(TTADManager example)
		{
			this.example = example;
		}

		public void OnAdShow()
		{
			Debug.Log("rewardVideoAd show");
			TTADManager.instance.RewardShow ();
		}

		public void OnAdVideoBarClick()
		{
			Debug.Log("rewardVideoAd bar click");
		}

		public void OnAdClose()
		{
			Debug.Log("rewardVideoAd close");
			TTADManager.instance.ExRewardFinish ();
		}

		public void OnVideoComplete()
		{
			Debug.Log("rewardVideoAd complete");
		}

		public void OnVideoError()
		{
			Debug.LogError("rewardVideoAd error");
			TTADManager.instance.AdShowError ();
		}

		public void OnRewardVerify(
			bool rewardVerify, int rewardAmount, string rewardName)
		{
			Debug.Log("verify:" + rewardVerify + " amount:" + rewardAmount +
				" name:" + rewardName);
		}
	}

	private sealed class AppDownloadListener : IAppDownloadListener
	{
		private TTADManager example;

		public AppDownloadListener(TTADManager example)
		{
			this.example = example;
		}

		public void OnIdle()
		{
		}

		public void OnDownloadActive(
			long totalBytes, long currBytes, string fileName, string appName)
		{
			Debug.Log("下载中，点击下载区域暂停");
		}

		public void OnDownloadPaused(
			long totalBytes, long currBytes, string fileName, string appName)
		{
			Debug.Log("下载暂停，点击下载区域继续");
		}

		public void OnDownloadFailed(
			long totalBytes, long currBytes, string fileName, string appName)
		{
			Debug.LogError("下载失败，点击下载区域重新下载");
		}

		public void OnDownloadFinished(
			long totalBytes, string fileName, string appName)
		{
			Debug.Log("下载完成，点击下载区域重新下载");
		}

		public void OnInstalled(string fileName, string appName)
		{
			Debug.Log("安装完成，点击下载区域打开");
		}
	}
}
