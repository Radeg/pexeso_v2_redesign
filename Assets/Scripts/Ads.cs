﻿using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour {

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }
}
