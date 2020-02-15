using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class RewardedAdmobScriptV2 : MonoBehaviour
{
    private RewardedAd rewardAd;

    public string idAndroid = "ca-app-pub-3940256099942544/5224354917";

    public string idIOS = "ca-app-pub-3940256099942544/1712485313";

    public int count = 100;

    public Text coinText;

    public Button rewardButton;

    void Start()
    {
        coinText.text = PlayerPrefs.GetInt("Coin").ToString();
        rewardButton.interactable = false;

        this.CreateAndLoadRewardedAd();
    }


    void Update()
    {
        if (rewardAd.IsLoaded())
        {
            rewardButton.interactable = true;
        }
        else
        {
            rewardButton.interactable = false;
        }
    }
    public RewardedAd CreateAndLoadRewardedAd()
    {
#if UNITY_ANDROID
        string id = idAndroid;
#elif UNITY_IPHONE
        string id = idIOS;
#else
        string id="unexpected_platform";
#endif

        this.rewardAd = new RewardedAd(id);

        this.rewardAd.OnAdFailedToLoad += VideoFailedToLoad;

        this.rewardAd.OnAdFailedToShow += VideoFailedToShow;

        this.rewardAd.OnUserEarnedReward += VideoRewarded;

        this.rewardAd.OnAdClosed += VideoClosed;

        AdRequest request = new AdRequest.Builder().Build();

        this.rewardAd.LoadAd(request);

        return rewardAd;
    }
    void VideoFailedToLoad(object sender, EventArgs e)
    {
        CreateAndLoadRewardedAd();
    }

    void VideoFailedToShow(object sender, EventArgs e)
    {
        Show();
    }

    void VideoRewarded(object sender, EventArgs e)
    {
        Reward();
    }
    void VideoClosed(object sender, EventArgs e)
    {
        CreateAndLoadRewardedAd();
    }
    //Ads show function
    public void Show()
    {
        if (this.rewardAd.IsLoaded())
        {
            this.rewardAd.Show();
        }
        else
        {
            MonoBehaviour.print("Rewarded ad is not ready yet");
        }

    }
    //Reward ads function
    private void Reward()
    {
        int coin = PlayerPrefs.GetInt("Coin");
        coin += count;
        PlayerPrefs.SetInt("Coin", coin);
        coinText.text = coin.ToString();
        rewardButton.interactable = false;
    }
}
