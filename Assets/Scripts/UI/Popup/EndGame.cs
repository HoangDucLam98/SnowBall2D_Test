using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Spine.Unity;

public class EndGame : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject spinPanel;
    public RankController rankController;

    public List<RankItem> items;
    int rankIndex = 6;
    float itemHeight = 70f;

    // Spin
    public GameObject spinButton;
    public GameObject spinAgainGroup;
    public GameObject rewardObject;
    public List<GameObject> listReward;
    public Wheel wheel;

    public void UpdateState(List<InfoData> data)
    {
        //var followCoins = data.OrderByDescending(m => m.coins).ToList();
        //var s = followCoins.OrderBy(m => m.isDeath ? 1 : 0).ToList();
        var s = data;

        int count = 0;

        s[0].coins += 100;
        s[1].coins += 50;
        s[2].coins += 20;

        foreach (var item in s)
        {
            s[count].rank = count + 1;
            // if (s[count].isPLayer)
            // {
            //     UIManager.Ins.UpdateCoins(s[count].coins);
            // }
            count++;
        }

        //UIManager.Ins.UpdateCoins(s.Find(m => m.isPLayer == true).coins);

        rankController.OnShow(s);
    }

    public void Clear()
    {
        // rankController.ClearContent();
        rankController.Hide();
        victoryPanel.Hide();
        spinAgainGroup.Hide();
        spinPanel.Hide();
    }

    public void Victory()
    {
        if (victoryPanel != null)
        {
            bool isActive = victoryPanel.activeSelf;
            victoryPanel.SetActive(!isActive);
        }
    }

    public void OpenSpin()
    {
        ManagerAds.Ins.ShowRewardedVideo((b) =>
        {
            if (b)
            {
                if (spinPanel != null)
                {
                    bool isActive = spinPanel.activeSelf;
                    spinPanel.SetActive(!isActive);
                    wheel.OnShow();
                }

                rankController.Hide();

                // UIManager.Ins.gamePlay.PlayComeSpin();
                ++UIManager.Ins.VideoCount;
                UIManager.Ins.popup.claimReward.OnCheckCanClaimReward(CostType.Video);
                wheel.StartSpin();
            }
        });
    }

    public void hideSpinButton()
    {
        spinButton.Hide();
        spinAgainGroup.Hide();
    }

    public void showSpinButton()
    {
        spinButton.Show();
        spinAgainGroup.Hide();
    }

    public void ShowReward(int i)
    {
        rewardObject.Show();
        listReward[i].gameObject.Show();
        spinAgainGroup.Show();
    }

    public void HideAllListReward()
    {
        foreach (var item in listReward)
        {
            item.Hide();
        }
        rewardObject.Hide();
    }

}



[System.Serializable]
public class InfoData
{
    public int rank;
    public string itemName;
    public Sprite flagSprite;
    public Sprite figureSprite;
    public SkeletonDataAsset dataAsset;

    public bool isDeath;

    public bool isPLayer;
    public int id;
    public int idKiller = -1;
    public int numberKilled = 0;

    public int coins = 0;
}