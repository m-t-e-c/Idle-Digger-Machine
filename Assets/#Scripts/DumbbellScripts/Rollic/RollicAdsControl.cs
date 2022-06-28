using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElephantSDK;
using System;
//using RollicGames.Advertisements;
//using com.adjust.sdk;

public class RollicAdsControl : MonoBehaviour
{
#region Rollic Advertisement SDK Unit Ids
// #if UNITY_IOS
//     private readonly string[] _bannerAdUnits = {"84c23801920e4add99632a1450449c2f" };
//     private readonly string[] _interstitialAdUnits = {"28d8e5d23cf842bba9604d55dbd77cb3" };
//     private readonly string[] _rewardedVideoAdUnits = {"e71f020b25714f58984d6bab38e5d4f1" };
// #elif UNITY_ANDROID || UNITY_EDITOR
//     private readonly string[] _bannerAdUnits = {"0697d941ebb3489aae269a82b4c2ca05" };
//     private readonly string[] _interstitialAdUnits = {"5405a3dab98e4602a669a25e7e3e9f85" };
//     private readonly string[] _rewardedVideoAdUnits = {"15a2da2b668f4ac5899e738fdbb7239a" };
// #endif
#endregion
#region Rollic Advertisement SDK Functions
//     public void ShowInterstitial(string x)
//     {
//         if(!isAdsAvailable) return;
//         if(!isAdLevel) return;
//         if(!canShowInterstitial) return;
//         if(RLAdvertisementManager.Instance.isInterstitialReady())
//         {
//             isInAd = true;
//             RLAdvertisementManager.Instance.showInterstitial();
//             stM.ElephantTelemetry(x + "_interstitial", "time", (int)PlayerPrefs.GetFloat("gameTime"));
//             elapsedSinceLastInter = 0;
//         }
//     }
//     public void ShowBanner()
//     {
//         if(!isAdsAvailable) return;
//         RLAdvertisementManager.Instance.loadBanner();
//         if(isBannerActive == 1) whiteBanner.SetActive(true);
//     }
//     public void ShowRewarded()
//     {
//         if(!isAdsAvailable) return;
//         if (RLAdvertisementManager.Instance.isRewardedVideoAvailable())
//         {
//             isInAd = true;
//             RLAdvertisementManager.Instance.showRewardedVideo();
//             elapsedSinceLastInter = 0;
//             Time.timeScale = 0f;
//         }
//         else
//         {
//             StartCoroutine(AdvertNotAvailable());
//         }
//     }
//     void TimedInterstitial(float t)
//     {
//         if(isAdsAvailable && !isInAd && interInterval)
//         {
//             elapsed += Time.deltaTime;
//             if(elapsed >= t) ShowInterstitial("interval");
//         }
//     }
//     public IEnumerator AdvertNotAvailable()
//     {
//         adNotAvailable.SetActive(true);
//         yield return new WaitForSecondsRealtime(1f);
//         adNotAvailable.SetActive(false);
//     }
#endregion
#region Rollic Advertisement SDK Events Setup
//     // Initialize
//     void InitializeRollicAdEvents()
//     {
//         RLAdvertisementManager.Instance.init(_rewardedVideoAdUnits, _bannerAdUnits, _interstitialAdUnits);
//         RLAdvertisementManager.OnRollicAdsSdkInitializedEvent += OnRollicAdsSdkInitializedEvent;
//         RLAdvertisementManager.Instance.rewardedAdResultCallback = RewardedAdResultCallback;
//         // Banner 
//         RLAdvertisementManager.OnRollicAdsAdLoadedEvent += OnRollicAdsAdLoadedEvent;
//         RLAdvertisementManager.OnRollicAdsAdClickedEvent += OnRollicAdsAdClickedEvent;

//         // Intersitital
//         RLAdvertisementManager.OnRollicAdsInterstitialLoadedEvent += OnRollicAdsInterstitialLoadedEvent;
//         RLAdvertisementManager.OnRollicAdsInterstitialFailedEvent += OnRollicAdsInterstitialFailedEvent;
//         RLAdvertisementManager.OnRollicAdsInterstitialDismissedEvent += OnRollicAdsInterstitialDismissedEvent;
//         RLAdvertisementManager.OnRollicAdsInterstitialExpiredEvent += OnRollicAdsInterstitialExpiredEvent; 
//         RLAdvertisementManager.OnRollicAdsInterstitialShownEvent  += OnRollicAdsInterstitialShownEvent;

//         // Rewarded Ad
//         RLAdvertisementManager.OnRollicAdsRewardedVideoLoadedEvent += OnRollicAdsRewardedVideoLoadedEvent;
//         RLAdvertisementManager.OnRollicAdsRewardedVideoFailedEvent += OnRollicAdsRewardedVideoFailedEvent;
//         RLAdvertisementManager.OnRollicAdsRewardedVideoExpiredEvent += OnRollicAdsRewardedVideoExpiredEvent;
//         RLAdvertisementManager.OnRollicAdsRewardedVideoShownEvent += OnRollicAdsRewardedVideoShownEvent;
//         RLAdvertisementManager.OnRollicAdsRewardedVideoFailedToPlayEvent += OnRollicAdsRewardedVideoFailedEvent;
//         RLAdvertisementManager.OnRollicAdsRewardedVideoReceivedRewardEvent += OnRollicAdsRewardedVideoReceivedRewardEvent;
//         RLAdvertisementManager.OnRollicAdsRewardedVideoClosedEvent += OnRollicAdsRewardedVideoClosedEvent;   
//     }
//     void RewardedAdResultCallback(RLRewardedAdResult result)
//     {
//         switch (result)
//         {
//             case RLRewardedAdResult.Finished:
//                 userCanBeRewarded = true;
//                  Time.timeScale = 1f;
//                 sc.CustomDebug("Advertisement Event : Rewarded Ad Complete, Rewards Granted");
//                 break;
//             case RLRewardedAdResult.Skipped:
//                 userCanBeRewarded = false;
//                 Time.timeScale = 1f;
//                 sc.CustomDebug("Advertisement Event : Rewarded Ad Skipped, Rewards Denied");
//                 break;            
//             case RLRewardedAdResult.Failed:
//                 userCanBeRewarded = false;
//                 Time.timeScale = 1f;
//                 sc.CustomDebug("Advertisement Event : Rewarded Ad Failed, Rewards Denied");
//                 break;            
//                 default:
//                 break;
//         }
//     }
//     void OnRollicAdsSdkInitializedEvent(string adUnitId)
//     {
//         RLAdvertisementManager.Instance.init(_rewardedVideoAdUnits,_bannerAdUnits, _interstitialAdUnits,MoPubBase.LogLevel.Debug);
//         sc.CustomDebug("Advertisement Event : Rollic Ads SDK Initialized");
//     }
//     // Banner
//     void OnRollicAdsAdClickedEvent (string adUnitId) => sc.CustomDebug("Advertisement Event : User Clicked Banner Ad");
//     void OnRollicAdsAdLoadedEvent (string adUnitId, float parsedHeight) => sc.CustomDebug("Advertisement Event : Banner Loaded Successfuly, Height = " + parsedHeight);
//     // Rewarded
//     void OnRollicAdsRewardedVideoLoadedEvent (string adUnitId){ sc.CustomDebug("Advertisement Event : Rewarded Video Loaded"); isAdShown = false;  Time.timeScale = 1f;}
//     void OnRollicAdsRewardedVideoFailedEvent (string adUnitId, string errorMessage){ sc.CustomDebug("Advertisement Event : Rewarded Video Failed, Error Code = " + errorMessage); isInAd = false;  Time.timeScale = 1f;}
//     void OnRollicAdsRewardedVideoExpiredEvent (string adUnitId){sc.CustomDebug("Advertisement Event : Rewarded Video Expired"); isInAd = false; Time.timeScale = 1f; //         elapsedSinceLastRewarded = 0;}
//     void OnRollicAdsRewardedVideoShownEvent (string adUnitId)
//     {
//         sc.CustomDebug("Advertisement Event : Rewarded Video Shown");
//         isInAd = false;
//         Time.timeScale = 1f;
//         elapsedSinceLastRewarded = 0;
//     }
//     void OnRollicAdsRewardedVideoFailedToPlayEvent (string adUnitId, string errorMessage){ sc.CustomDebug("Advertisement Event : Rewarded Video Failed to Play"); isInAd = false; Time.timeScale = 1f; isAdShown = true;}
//     void OnRollicAdsRewardedVideoReceivedRewardEvent (string adUnitId, string label, float amount)
//     {
//         sc.CustomDebug("Advertisement Event : Rewarded Video Watched, Rewards Granted");
//         isInAd = false; 
//         Time.timeScale = 1f; 
//         userCanBeRewarded = true;
//         isAdShown = true;
//         elapsedSinceLastRewarded = 0;
//     }
//     void OnRollicAdsRewardedVideoClosedEvent (string adUnitId)
//     {
//         sc.CustomDebug("Advertisement Event : Rewarded Video Closed");
//         isAdShown = true;
//         isInAd = false; 
//         Time.timeScale = 1f;
//         elapsedSinceLastRewarded = 0;
//     }
//     // Interstitial
//     void OnRollicAdsInterstitialLoadedEvent(string adUnitId) => sc.CustomDebug("Advertisement Event : Interstitial Video Loaded");
//     void OnRollicAdsInterstitialFailedEvent (string adUnitId, string errorCode){ sc.CustomDebug("Advertisement Event : Interstitial Video Failed, Error Code = " + errorCode); isInAd = false; Time.timeScale = 1f; elapsedSinceLastInter = 0;}
//     void OnRollicAdsInterstitialDismissedEvent (string adUnitId){ sc.CustomDebug("Advertisement Event : User Closed Interstitial Video"); isInAd = false; Time.timeScale = 1f; elapsedSinceLastInter = 0; }
//     void OnRollicAdsInterstitialExpiredEvent (string adUnitId){ sc.CustomDebug("Advertisement Event : Interstitial Video Expired"); isInAd = false; Time.timeScale = 1f; elapsedSinceLastInter = 0; }
//     void OnRollicAdsInterstitialShownEvent (string adUnitId){ sc.CustomDebug("Advertisement Event : Interstitial Video Shown"); isInAd = false; Time.timeScale = 1f; elapsedSinceLastInter = 0; }
#endregion
#region Adjust Variables & Functions
//     AdjustEvent adjustEvent;
//     AdjustEvent level10 = new AdjustEvent("8oz4os");
//     AdjustEvent level20 = new AdjustEvent("5fvr0m");
//     AdjustEvent level30 = new AdjustEvent("b0tjm0");
//     AdjustEvent level40 = new AdjustEvent("imymrq");
//     AdjustEvent level50 = new AdjustEvent("rrak7x");
//     public void CheckAdjust()
//     {
//         if(gc.levelNumber >= 10 && gc.levelNumber < 20) adjustEvent = level10;
//         else if(gc.levelNumber >= 20 && gc.levelNumber < 30) adjustEvent = level20;
//         else if(gc.levelNumber >= 30 && gc.levelNumber < 40) adjustEvent = level30;
//         else if(gc.levelNumber >= 40 && gc.levelNumber < 50) adjustEvent = level40;
//         else if(gc.levelNumber >= 50) adjustEvent = level50;
//         else return;
//         Adjust.trackEvent(adjustEvent);
//         Debug.Log("Adjust Fired");
//     }
#endregion 
    GameControl gc;
    RollicRemoteControl rrc;
    SettingsControl sc;

    [Header("===Advertisement Checkers===")]
    public bool isAdsAvailable = true;
    public bool isAdLevel = false;
    public bool userCanBeRewarded = false; 
    public bool isInAd = false;
    public bool isAdShown = false;
    public bool canShowRewarded = true;
    public bool canShowInterstitial = true;

    [Header("===Advertisement UI Elements===")]
    public GameObject adNotAvailable;
    public GameObject whiteBanner;

    [Header("===Advertisement Timers===")]
    public float elapsedSinceLastInter;
    public float elapsedSinceLastRewarded;

#region Unity Main Functions
    void Awake()
    {
        gc = FindObjectOfType<GameControl>();
        sc = FindObjectOfType<SettingsControl>();
        rrc = FindObjectOfType<RollicRemoteControl>();
        //InitializeRollicAdEvents();
    }
    void Start() 
    {
        Invoke("ShowBanner", 5);
    }
    void Update() 
    {
        //TimedInterstitial(rrc.interstitialAdsInterval); 
        TimedInterval();
        CheckLevelForAds();
    }
#endregion
#region Interstitial Checkers
    void CheckLevelForAds()
    {
        if(gc.levelNumber >= rrc.interstitialAdsStartLevel && !isAdLevel) isAdLevel = true;
    }
    void TimedInterval()
    {
        if (isAdsAvailable && !isInAd)
        {
            if(elapsedSinceLastRewarded >= rrc.interAfterRewardedTimer) canShowInterstitial = true;
            else
            {
                elapsedSinceLastRewarded += Time.deltaTime;
                canShowInterstitial = false;
            }
        }
    }
#endregion
}
