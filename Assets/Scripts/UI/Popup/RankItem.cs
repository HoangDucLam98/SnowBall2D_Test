using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankItem : MonoBehaviour
{
    public Image flagImage;
    public Text rankLevelTxt, pName, coinValueTxt;

    public void SetUp(InfoData infoData)
    {
        flagImage.sprite = infoData.flagSprite;
        rankLevelTxt.text = "#" + infoData.rank;
        pName.text = infoData.itemName;
        coinValueTxt.text = infoData.coins.ToString();
        this.Show();
    }

}
