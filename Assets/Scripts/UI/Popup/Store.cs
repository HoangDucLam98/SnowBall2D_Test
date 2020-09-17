using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    const int COIN = 1;
    const int PACK = 0;
    const int SKIN = 2;

    public GameObject[] listBuy; // 0 = coin, 1 = pack, 2 = skin
    public Image[] listTicketActive; // 0 = coin, 1 = pack, 2 = skin
    // Start is called before the first frame update
    void Start()
    {
        ShowSkinShop();
    }

    public void ShowCoinShop()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        HideAllListBuy();
        HideAllListTicketActive();
        listTicketActive[COIN].Show();
        listBuy[COIN].Show();
    }
    public void ShowPackShop()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        HideAllListBuy();
        HideAllListTicketActive();
        listTicketActive[PACK].Show();
        listBuy[PACK].Show();
    }
    public void ShowSkinShop()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        HideAllListBuy();
        HideAllListTicketActive();
        listTicketActive[SKIN].Show();
        listBuy[SKIN].Show();
    }

    public void HideAllListBuy()
    {
        foreach (var item in listBuy)
        {
            item.Hide();
        }
    }

    public void HideAllListTicketActive()
    {
        foreach (var item in listTicketActive)
        {
            item.Hide();
        }
    }
}

