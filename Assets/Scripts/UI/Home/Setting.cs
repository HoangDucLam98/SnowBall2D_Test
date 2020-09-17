using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject Ring, Sfx, Music;
    public Button closeSetting;
    public Button rateBtn;

    public void Start()
    {
        if (UIManager.Ins.Ring == 0)
            SetRing();
        if (UIManager.Ins.Music == 0)
            SetMusic();
        if (UIManager.Ins.Sfx == 0)
            SetSfx();

        closeSetting.onClick.AddListener(OpenSettingPopup);
        rateBtn.onClick.AddListener(RateApp);
    }

    void OpenSettingPopup()
    {
        AudioManager1.Ins.PlaySound(SoundType.Click);
        UIManager.Ins.popup.OpenSettingPopup();
    }

    public void SetRing()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        Ring.SetActive(!Ring.activeSelf);
        if (!Ring.activeSelf)
        {
            Handheld.Vibrate();
            UIManager.Ins.Ring = 1;
        }
        else
        {
            UIManager.Ins.Ring = 0;
        }
    }

    public void SetSfx()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        Sfx.SetActive(!Sfx.activeSelf);
        if (!Sfx.activeSelf)
        {
            AudioManager1.Ins.TurnOnSound();
            UIManager.Ins.Sfx = 1;
        }
        else
        {
            AudioManager1.Ins.TurnOffSound();
            UIManager.Ins.Sfx = 0;
        }
    }

    public void SetMusic()
    {
        Music.SetActive(!Music.activeSelf);

        if (!Music.activeSelf)
        {
            AudioManager1.Ins.TurnOnMusic();
            UIManager.Ins.Music = 1;
        }
        else
        {
            AudioManager1.Ins.TurnOffMusic();
            UIManager.Ins.Music = 0;
        }

        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);
    }

    public void RateApp()
    {
        ManagerAds.Ins.RateApp();

        //foreach (var item in UIManager.Ins.figureData.figures)
        //{
        //    if (item.costType == CostType.Rate && !item.isBuy)
        //    {
        //        item.isBuy = true;

        //        var masks = UIManager.Ins.PlayerMasks;

        //        UIManager.Ins.popup.claimReward.OnShow(item);
        //    }
        //}
    }
}
