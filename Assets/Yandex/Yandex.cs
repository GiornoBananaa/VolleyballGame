using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{

    [DllImport("__Internal")] private static extern void RewardedAd();
    [DllImport("__Internal")] private static extern void StartAd();


    void Start()
    {
        ShowStartAd();
    }

    public void ShowStartAd()
    {
        StartAd();
    }

    public void AdButton()
    {
        RewardedAd();
    }
}
