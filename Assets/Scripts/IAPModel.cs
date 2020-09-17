using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPModel : MonoBehaviour
{
    public void BuyReindeerComplete()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);
        UIManager.Ins.UpdateCoins(2000);
        // IAPManager.Ins.BuyReindeer();
    }
    public void BuySledgeComplete()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);
        UIManager.Ins.UpdateCoins(5000);
        // IAPManager.Ins.BuySledge();
    }
    public void BuySantaComplete()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);
        UIManager.Ins.UpdateCoins(15000);
        // IAPManager.Ins.BuySanta();
    }
    public void BuyChristmasComplete()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);
        UIManager.Ins.UpdateCoins(50000);
        // IAPManager.Ins.BuyChristmas();
    }
    public void BuyNoAdsComplete()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);
        ManagerAds.Ins.RemoveAds();
        // IAPManager.Ins.BuyNoAds();
    }
    public void BuyPackVipComplete()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);
        BuySkinController.Ins.BuyPackVip();
        // IAPManager.Ins.BuyPackVip();
    }
    public void BuyPackAllComplete()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);
        BuySkinController.Ins.BuyPackAll();
        // IAPManager.Ins.BuyPackAll();
    }
}
