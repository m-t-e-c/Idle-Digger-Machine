using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElephantSDK;

public class RollicRemoteControl : MonoBehaviour
{
    [Header("===Advertisement Remotes===")]
    public int isBannerActive = RemoteConfig.GetInstance().GetInt("banner_background_toggle",1);
    public int rateUsLevel = RemoteConfig.GetInstance().GetInt("rateUs_level", 3);
    public int extraEarnMultipier = RemoteConfig.GetInstance().GetInt("extra_earn_multipier", 5);
    public int rewardedAdsStartLevel = RemoteConfig.GetInstance().GetInt("rewarded_ads_start_level", 5);
    public int interstitialAdsStartLevel = RemoteConfig.GetInstance().GetInt("interstitial_start_level", 3);
    public int interstitialAdsInterval = RemoteConfig.GetInstance().GetInt("interstial_interval", 60);
    public int interAfterRewardedTimer = RemoteConfig.GetInstance().GetInt("after_rewarded_inter_timer", 60);
}
