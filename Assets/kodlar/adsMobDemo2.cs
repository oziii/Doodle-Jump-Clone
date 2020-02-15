using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class adsMobDemo2 : MonoBehaviour
{

    private InterstitialAd inter;

    public string idAndroid="";

    public string idIOS="";

    void Start()
    {
        this.Request();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Request()
    {
#if UNITY_ANDROID
        string id = idAndroid;
#elif UNITY_IPHONE
        string id =idIOS;
#else
    string id="unexpected_platform";
#endif

        this.inter = new InterstitialAd(id);

        this.inter.OnAdClosed +=InterstitialClosed;

        AdRequest request = new AdRequest.Builder().Build();

        this.inter.LoadAd(request);
    }

    private void InterstitialClosed(object sender,EventArgs e)
    {
        this.Request();
    }

    public void Show()
    {
        this.inter.Show();
    }
}
