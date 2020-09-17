using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeItem : MonoBehaviour
{
    public Image backGround, upDir, status, figure;
    private Sprite previousBG, previousUD, previousST, previousFI;
    public Button ClaimBtn;

    private void Start()
    {
        previousBG = backGround.sprite;
        if (upDir != null)
            previousUD = upDir.sprite;
        if (status != null)
            previousST = status.sprite;
        if (figure != null)
            previousFI = figure.sprite;
    }

    public void ChallengeItemActive(Sprite backGround, Sprite upDir)
    {
        this.status.Hide();
        this.backGround.sprite = backGround;
        if (this.upDir != null)
            this.upDir.sprite = upDir;
    }

    public void OverChallenge(Sprite status)
    {
        if (ClaimBtn != null)
        {
            ClaimBtn.Show();
        }

        this.status.sprite = status;
        this.status.SetNativeSize();
        this.status.Show();
    }

    public void Reset()
    {
        this.backGround.sprite = previousBG;
        if (this.upDir != null)
            this.upDir.sprite = previousUD;
        this.status.sprite = previousST;
        this.status.Show();
        this.status.SetNativeSize();
        if (ClaimBtn != null)
        {
            ClaimBtn.Hide();
        }
    }
}
