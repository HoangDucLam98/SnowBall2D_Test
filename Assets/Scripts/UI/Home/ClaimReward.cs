using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using System.Linq;

public class ClaimReward : MonoBehaviour
{
    public SkeletonGraphic asset;
    public Text nameFigure;
    private List<InfoFigure> killRewards, topRewards, runRewards, videoRewards;

    private int killIndex, topIndex, runIndex, videoIndex;
    public void OnStart()
    {
        this.Hide();

        killRewards = new List<InfoFigure>();
        topRewards = new List<InfoFigure>();
        runRewards = new List<InfoFigure>();
        videoRewards = new List<InfoFigure>();

        killIndex = 0;
        topIndex = 0;
        runIndex = 0;
        videoIndex = 0;

        foreach (var item in UIManager.Ins.figureData.figures)
        {
            if (!item.isBuy)
            {
                if (item.costType == CostType.KillEnemy)
                    killRewards.Add(item);

                if (item.costType == CostType.Run)
                    runRewards.Add(item);

                if (item.costType == CostType.Top)
                    topRewards.Add(item);

                if (item.costType == CostType.Video)
                    videoRewards.Add(item);
            }
        }

        killRewards = killRewards.OrderBy(m => m.number).ToList();
        topRewards = topRewards.OrderBy(m => m.number).ToList();
        runRewards = runRewards.OrderBy(m => m.number).ToList();
        videoRewards = videoRewards.OrderBy(m => m.number).ToList();
    }

    public void OnCheckCanClaimReward(CostType type)
    {
        // switch (type)
        // {
        //     case CostType.KillEnemy:
        //         if (killRewards.Count > killIndex && killRewards[killIndex].number <= UIManager.Ins.NumberKilled)
        //         {
        //             UIManager.Ins.figureData.figures.Find(m => m.idFigure == killRewards[killIndex].idFigure).isBuy = true;
        //             OnShow(killRewards[killIndex]);
        //             killIndex++;
        //         }
        //         break;

        //     case CostType.Top:
        //         if (topRewards.Count > topIndex && topRewards[topIndex].number <= UIManager.Ins.NumberTop)
        //         {
        //             UIManager.Ins.figureData.figures.Find(m => m.idFigure == topRewards[topIndex].idFigure).isBuy = true;
        //             OnShow(topRewards[topIndex]);
        //             topIndex++;
        //         }
        //         break;

        //     case CostType.Run:
        //         if (runRewards.Count > runIndex && runRewards[runIndex].number <= UIManager.Ins.PlayerMoveDistance)
        //         {
        //             UIManager.Ins.figureData.figures.Find(m => m.idFigure == runRewards[runIndex].idFigure).isBuy = true;
        //             OnShow(runRewards[runIndex]);
        //             runIndex++;
        //         }
        //         break;

        //     case CostType.Video:
        //         if (videoRewards.Count > videoIndex && videoRewards[videoIndex].number <= UIManager.Ins.VideoCount)
        //         {
        //             UIManager.Ins.figureData.figures.Find(m => m.idFigure == videoRewards[videoIndex].idFigure).isBuy = true;
        //             OnShow(videoRewards[videoIndex]);
        //             videoIndex++;
        //         }
        //         break;
        // }
    }

    public void OnShow(InfoFigure info)
    {
        this.Show();
        AudioManager1.Ins.PlaySound(SoundType.UnlockFigure);
        UIManager.Ins.popup.PlayComeReward();
        UIManager.Ins.UpdatePlayerMasks(info.idFigure);
        asset.skeletonDataAsset = info.asset;
        asset.startingLoop = true;
        asset.Initialize(true);

        nameFigure.text = info.heroName;
        StartCoroutine(ShowRewardClaimed());
    }

    IEnumerator ShowRewardClaimed()
    {
        yield return new WaitForSeconds(3f);
        UIManager.Ins.popup.PlayLeaveReward();
    }
}
