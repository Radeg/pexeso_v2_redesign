using UnityEngine;
using System.Collections;

public class AdController : MonoBehaviour {

    public GoogleMobileAdsDemoScript adMob;

    public void ShowAd()
    {
        DontDestroyOnLoad(this.gameObject);
        adMob.ShowInterstitial();
    }
}
