using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    private string APP_ID = "ca-app-pub-3727399978743679~4275164739";

    private static InterstitialAd interstitialAd;
    private static RewardBasedVideoAd rewardBasedVideo;

    void Start()
    {
        MobileAds.Initialize(APP_ID);

        RequestInterstitial();
        RequestRewardedAd();
    }

    void RequestInterstitial()
    {
        string interstitial_ID = "ca-app-pub-3727399978743679/1649001394";
        interstitialAd = new InterstitialAd(interstitial_ID);

        AdRequest adRequest = new AdRequest.Builder().Build();

        interstitialAd.LoadAd(adRequest);

    }

    void RequestRewardedAd()
    {
        string rewardbasedvideoAd_ID = "ca-app-pub-3727399978743679/1827775422";
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        AdRequest adRequest = new AdRequest.Builder().Build();

        rewardBasedVideo.LoadAd(adRequest, rewardbasedvideoAd_ID);

    }


    public static void Display_InterstitialAD()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
    }

    public static void Display_RewardBasedVideoAD()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            MenuController.reward();
        }
    }

    public void ForButtonInterstitialAD() => Display_InterstitialAD();
    public void ForButtonRewardVideoAD() => Display_RewardBasedVideoAD();

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Display_InterstitialAD();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitial();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    void HandleInterstitialADEvents(bool subscribe)
    {
        if (subscribe)
        {
            interstitialAd.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is shown.
            interstitialAd.OnAdOpening += HandleOnAdOpened;
            // Called when the ad is closed.
            interstitialAd.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            interstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        }
        else
        {
            interstitialAd.OnAdLoaded -= HandleOnAdLoaded;
            // Called when an ad request failed to load.
            interstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
            // Called when an ad is shown.
            interstitialAd.OnAdOpening -= HandleOnAdOpened;
            // Called when the ad is closed.
            interstitialAd.OnAdClosed -= HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            interstitialAd.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
        }

        void OnEnable()
        {
            HandleInterstitialADEvents(true);
        }

        void OnDisable()
        {
            HandleInterstitialADEvents(false);
        }
    }
    void Update()
    {

    }
}
