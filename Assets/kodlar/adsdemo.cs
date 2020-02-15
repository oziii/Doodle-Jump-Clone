using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class adsdemo : MonoBehaviour
{
    private RewardBasedVideoAd rAd;

    public string id = "";
    public int count = 100;

    public Text coinText;

    public Button rewardButton;

    void Start()
    {
        coinText.text = PlayerPrefs.GetInt("Coin").ToString();
        rewardButton.interactable = false;

        rAd = RewardBasedVideoAd.Instance;

        rAd.OnAdRewarded +=VideoRewarded;

        rAd.OnAdClosed +=VideoClosed;

        this.RequestAds();
    }
    void RequestAds()
    {
        AdRequest request = new AdRequest.Builder().Build();

        this.rAd.LoadAd(request, id);
    }
    void VideoRewarded(object sender, EventArgs e)
    {
        Reward();
    }
    void VideoClosed(object sender, EventArgs e)
    {
        RequestAds();
    }

    public void ShowAds()
    {
        this.rAd.Show();
    }

    private void Reward()
    {
        int coin = PlayerPrefs.GetInt("Coin");
        coin += count;
        PlayerPrefs.SetInt("Coin", coin);
        coinText.text = coin.ToString();
        rewardButton.interactable = false;
    }

    private void Update()
    {
        if (rAd.IsLoaded())
        {
            rewardButton.interactable = true;
        }
        else
        {
            rewardButton.interactable = false;
        }
    }
}
