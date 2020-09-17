using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class RankController : MonoBehaviour
{
    public RankItem playerItem;
    public ScrollRect rankTable;

    public RectTransform content;
    public Text playerRankTxt;
    public List<RankItem> rankItems;

    public void OnShow(List<InfoData> data)
    {
        this.Show();
        playerItem.Hide();
        content.offsetMax = new Vector2(0, 0);
        SpawnRankItem(data);
        UIManager.Ins.gamePlay.PlayComeRank();
    }

    public void SpawnRankItem(List<InfoData> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            // GameObject rankItem = Instantiate(rankPre, content);
            // RankItem item = rankItem.GetComponent<RankItem>();
            rankItems[i].SetUp(data[i]);

            //if (data[i].isPLayer)
            //{
            //    UIManager.Ins.UpdateCoins(data[i].coins);
            //    playerRankTxt.text = "#" + data[i].rank;
            //    if (data[i].rank > 5)
            //        OnShowPlayerRank(data[i]);
            //}
        }
        //rankTable.enabled = true;
        InfoData playerInfo = data.Find(m => m.isPLayer);
        playerRankTxt.text = "#" + playerInfo.rank;
        UIManager.Ins.gamePlay.SetTotalCoinClaim(playerInfo.coins);
        if (playerInfo.rank > 5)
            OnShowPlayerRank(playerInfo);

        AudioManager1.Ins.musicAudio.Stop();
        if (playerInfo.rank > 1)
            AudioManager1.Ins.PlaySound(SoundType.ShowRank);
        else
            AudioManager1.Ins.PlaySound(SoundType.ShowRankWin);

        //UIManager.Ins.gamePlay.NextButton();
        //UIManager.Ins.home.PlayComeHome();
    }

    void OnShowPlayerRank(InfoData info)
    {
        playerItem.SetUp(info);
    }

    //public void ClearContent()
    //{
    //    for (int i = 0; i < content.gameObject.transform.childCount; i++)
    //    {
    //        Destroy(content.gameObject.transform.GetChild(i).gameObject);
    //    }
    //}

}
