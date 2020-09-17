using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class FigureItem : MonoBehaviour
{
    [SerializeField]
    private GameObject[] costTypePre; // 0 = coin, 1 = using, 2 = video, 3 = Mission
    public Text nameTxt, costTxt, videoTxt;
    public GameObject boxImage;
    public SkeletonGraphic skeleton;

    public void SetUp(InfoFigure info)
    {
        nameTxt.text = info.heroName;
        if (info.asset != null)
        {
            skeleton.skeletonDataAsset = info.asset;
            skeleton.startingLoop = true;
            skeleton.Initialize(true);
        }

        if (info.isUsing)
        {
            HideAllCostType();
            costTypePre[1].Show();
        }
        else if (info.isBuy)
        {
            HideAllCostType();
        }
        else if (info.costType == CostType.Coin)
        {
            HideAllCostType();
            costTypePre[0].Show();
            costTxt.text = info.cost.ToString();
        }
        else if (info.costType == CostType.Video)
        {
            HideAllCostType();
            costTypePre[2].Show();
        }
        else if (info.costType == CostType.Vip)
        {
            HideAllCostType();
            costTypePre[3].GetComponent<Text>().text = "Vip";
            costTypePre[3].Show();
        }
        else
        {
            HideAllCostType();
            costTypePre[3].Show();
        }
    }

    public void HideAllCostType()
    {
        foreach (var item in costTypePre)
        {
            item.Hide();
        }
    }

    public void SetActiveBox()
    {
        bool check = boxImage.activeSelf;
        boxImage.SetActive(!check);
    }
}
