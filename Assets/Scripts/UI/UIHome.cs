using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class UIHome : MonoBehaviour
{
    public GameObject freeLifeSave, freeLifeSaveBtn;
    public Text playerCoins, playerName;
    public SkeletonGraphic skeleton;
    public Button changeNameBtn;

    public Animation animation;
    public Image figureSelected;

    private void Start()
    {
        changeNameBtn.onClick.AddListener(ChangeName);
        playerCoins.text = UIManager.Ins.PlayerCoins.ToString();
        playerName.text = UIManager.Ins.PlayerName;
        //        LoadAsset(UIManager.Ins.figureData.figures.Find(n => n.idFigure == UIManager.Ins.PlayerFigure));
        // GameController.Instance.SetUpData();
        LoadFigure();
        ManagerAds.Ins.ShowBanner();
    }

    public void LoadFigure()
    {
        figureSelected.sprite = UIManager.Ins.figureData.figures[UIManager.Ins.PlayerFigure].figureSprite;
    }

    public void UpdateCoin()
    {
        if (UIManager.Ins.previousCoin < UIManager.Ins.PlayerCoins)
            StartCoroutine(CoinUpdate(UIManager.Ins.previousCoin, UIManager.Ins.PlayerCoins));
        else
        {
            playerCoins.text = UIManager.Ins.PlayerCoins.ToString();
        }
    }

    IEnumerator CoinUpdate(int previousCoin, int currentCoin)
    {
        while (previousCoin < currentCoin)
        {
            previousCoin += 2; //Increment the display score by 1
            playerCoins.text = previousCoin.ToString(); //Write it to the UI
            yield return new WaitForSeconds(0.01f); // I used .2 secs but you can update it as fast as you want
        }
        // AudioManager1.Ins.PlaySound(SoundType.CoinClaim);
    }

    public void OnShow()
    {
        this.Show();
        UpdateCoin();
        if (UIManager.Ins.IsFreeLifeSaver)
            OnShowFreeLifeSave();
        else
            OnHideFreeLifeSave();
    }

    void ChangeName()
    {
        AudioManager1.Ins.PlaySound(SoundType.Click);
        UIManager.Ins.popup.rename.OnShow();
    }

    public void PlayLeaveHome()
    {
        animation.Play("LeaveHomeAnim");
    }

    public void PlayComeHome()
    {
        animation.Play("ComeHomeAnim");
    }

    public void SetUpData()
    {
        GameController.Instance.SetUpData();
    }

    public void StartGame()
    {
        UIManager.Ins.previousCoin = UIManager.Ins.PlayerCoins;
        gameObject.Hide();
        UIGameplay.Ins.OnPlay();

        // AudioManager.PlaySound(AudioManager.soundName);
        AudioManager1.Ins.PlayMusic(MusicType.IngameMusic);
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);
        ManagerAds.Ins.HideBanner();
    }

    public void LoadAsset(InfoFigure info)
    {
        if (info.asset != null)
        {
            skeleton.skeletonDataAsset = info.asset;
            skeleton.startingLoop = true;
            skeleton.Initialize(true);
        }
    }

    public void FreeLifeSave()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        ManagerAds.Ins.ShowRewardedVideo((b) =>
        {
            if (b)
            {
                OnShowFreeLifeSave();

                ++UIManager.Ins.VideoCount;
                UIManager.Ins.popup.claimReward.OnCheckCanClaimReward(CostType.Video);
            }
        });
    }

    public void OnShowFreeLifeSave()
    {
        UIManager.Ins.IsFreeLifeSaver = true;
        freeLifeSave.Show();
        freeLifeSaveBtn.Hide();
    }

    public void OnHideFreeLifeSave()
    {
        UIManager.Ins.IsFreeLifeSaver = false;
        freeLifeSave.Hide();
        freeLifeSaveBtn.Show();
    }
}
