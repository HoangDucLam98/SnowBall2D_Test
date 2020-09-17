using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    public static Wheel Ins;
    private int randomValue;
    private float timeInterval;
    private bool coroutineAllowed;
    private int finalAngle;
    private bool spinAgained;

    private int startCoins;

    private void Start()
    {
        if (Ins == null)
            Ins = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(Spin());
            coroutineAllowed = false;
        }
    }

    public void OnShow()
    {
        startCoins = UIManager.Ins.gamePlay.totalCoinClaim;
    }

    // IEnumerator Spin()
    // {
    //     coroutineAllowed = false;
    //     randomValue = Random.Range(20, 30);
    //     timeInterval = 0.1f;

    //     for (int i = 0; i < randomValue; i++)
    //     {
    //         transform.Rotate(0, 0, 22.5f);
    //         if (i > Mathf.RoundToInt(randomValue * 0.5f))
    //             timeInterval = 0.2f;
    //         if (i > Mathf.RoundToInt(randomValue * 0.85f))
    //             timeInterval = 0.4f;
    //         yield return new WaitForSeconds(timeInterval);
    //     }

    //     if (Mathf.RoundToInt(transform.eulerAngles.z) % 60 != 0)
    //         transform.Rotate(0, 0, 30f);

    //     finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);
    //     Debug.Log(finalAngle);

    //     switch (finalAngle)
    //     {
    //         case 0:
    //             Debug.Log(0);
    //             break;
    //         case 60:
    //             Debug.Log(60);
    //             break;
    //         case 120:
    //             Debug.Log(120);
    //             break;
    //         case 180:
    //             Debug.Log(180);
    //             break;
    //         case 240:
    //             Debug.Log(240);
    //             break;
    //         case 300:
    //             Debug.Log(300);
    //             break;
    //     }
    //     coroutineAllowed = true;
    // }

    IEnumerator Spin(float time = 4f)
    {
        float randomAngle = Random.Range(0, 360);
        float angle = randomAngle + 360 * Random.Range(15, 50);
        float number = Mathf.Floor(randomAngle / 60);
        float i = 0;
        transform.eulerAngles = Vector3.zero;
        while (i < 1)
        {
            i += Time.deltaTime / time;
            var f = Mathf.SmoothStep(transform.eulerAngles.z, angle, i);
            transform.eulerAngles = new Vector3(0, 0, f);
            yield return null;
        }

        switch (number)
        {
            case 0:
                UIManager.Ins.gamePlay.end.ShowReward(3);
                SetTotalCoins(10);
                break;
            case 1:
                UIManager.Ins.gamePlay.end.ShowReward(1);
                //startCoins *= 3;
                SetTotalCoins(3);
                break;
            case 2:
                UIManager.Ins.gamePlay.end.ShowReward(0);
                //startCoins *= 2;
                SetTotalCoins(2);
                break;
            case 3:
                UIManager.Ins.gamePlay.end.ShowReward(2);
                //startCoins *= 5;
                SetTotalCoins(5);
                break;
            case 4:
                UIManager.Ins.gamePlay.end.ShowReward(1);
                //startCoins *= 3;
                SetTotalCoins(3);
                break;
            case 5:
                UIManager.Ins.gamePlay.end.ShowReward(0);
                //startCoins *= 2;
                SetTotalCoins(2);
                break;
        }

        if( !spinAgained )
            UIManager.Ins.gamePlay.PlayComeSpinAgain();
        else
        {
            spinAgained = false;
            yield return new WaitForSeconds(3f);
            UIManager.Ins.gamePlay.NextButton();
            UIManager.Ins.home.PlayComeHome();
        }
    }

    void SetTotalCoins(int index)
    {
        int newTotalCoins = startCoins * index;
        UIManager.Ins.gamePlay.SetTotalCoinClaim(newTotalCoins);
    }

    public void StartSpin()
    {
        // AudioManager.PlaySound(AudioManager.spinStartName);
        AudioManager1.Ins.PlaySound(SoundType.Spin);

        UIManager.Ins.gamePlay.end.HideAllListReward();
        coroutineAllowed = true;
        // UIManager.Ins.gamePlay.end.hideSpinButton();
    }

    public void SpinAgain()
    {
        // AudioManager.PlaySound(AudioManager.spinStartName);
        AudioManager1.Ins.PlaySound(SoundType.Spin);

        ManagerAds.Ins.ShowRewardedVideo((b) =>
        {
            if (b)
            {
                spinAgained = true;
                // UIManager.Ins.gamePlay.PlayLeaveSpinAgain();
                UIManager.Ins.gamePlay.end.HideAllListReward();
                coroutineAllowed = true;
                // UIManager.Ins.gamePlay.end.hideSpinButton();
                ++UIManager.Ins.VideoCount;
                UIManager.Ins.popup.claimReward.OnCheckCanClaimReward(CostType.Video);
            }
        });
    }
}
