using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup : MonoBehaviour
{
    public GameObject vipPopup;
    public GameObject availablePopup;
    public GameObject storePopup;
    public GameObject achivementPopup;
    public GameObject settingPopup;
    public UIRename rename;
    public RectTransform viewport;

    // Challenge
    public GameObject[] challengeList;
    public Sprite backGround, upDir, status, figure, backGrounFinal;
    public GameObject challengeAgainGroup, StartChallengeBtn;
    private int[] idFigures = { 2, 7, 17, 19, 26 };
    int currentReward;
    public Image reward;
    // bool isAllowClaim;
    InfoFigure dataReward;
    public Text timeWaitLose, timeWaitWin;
    public bool isAllowStart;
    public Challenge challengeScr;

    // Achivement
    public GameObject AchivementItemPre, Content;
    // public Transform Content;
    string[] achivementNames = { "Number of wins", "Total kills", "Total distance you moved", "Total distance you launched an enemy" };
    List<string> achivements;

    public ClaimReward claimReward;
    public Animation animation;

    // Start is called before the first frame update
    void Start()
    {
        claimReward.OnStart();
        // UnlockChallenge();
        ChangeReward();
        // if (UIManager.Ins.IndexChallenge > 0)
        // {
        //     for (int i = 0; i < UIManager.Ins.IndexChallenge; i++)
        //     {
        //         ChallengeItem challengeItem = challengeList[i].GetComponent<ChallengeItem>();
        //         challengeItem.ChallengeItemActive(backGround, upDir);
        //         challengeItem.OverChallenge(status);
        //     }
        // }

        // if (!UIManager.Ins.CheckIsCanPlayChallenge())
        // {
        //     SetChallengeAgainGroup();
        // }

        achivements = new List<string>();
    }

    private void Update()
    {
        timeWaitLose.text = challengeScr.timeJoin;
        timeWaitWin.text = challengeScr.timeJoin;
    }

    void ChangeReward()
    {
        dataReward = UIManager.Ins.FindByID(idFigures[currentReward]);
        reward.sprite = dataReward.figureSprite;
        reward.SetNativeSize();
    }

    public void ClaimReward()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        claimReward.OnShow(dataReward);
        dataReward.isBuy = true;
        ++currentReward;
        ChangeReward();
        LockChallenge();
    }

    public void SetChallengeAgainGroup()
    {
        if (!challengeScr.CheckIsCanPlayChallenge())
        {
            // challengeScr.SpawnTime(UIManager.Ins.TimeWaitLose);
            challengeAgainGroup.Show();
            StartChallengeBtn.Hide();
        }
        else
        {
            // challengeScr.SpawnTime(UIManager.Ins.TimeWaitWin);
            challengeAgainGroup.Hide();
            StartChallengeBtn.Show();
        }
    }

    // void SetTime(float time)
    // {
    //     var ts = TimeSpan.FromSeconds(time);
    //     timeWaitLose.text = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
    // }

    public void OpenVipPopup()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        if (vipPopup != null)
        {
            bool isActive = vipPopup.activeSelf;
            vipPopup.SetActive(!isActive);
        }
    }

    public void OpenAvailablePopup()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        if (availablePopup != null)
        {
            bool isActive = availablePopup.activeSelf;
            availablePopup.SetActive(!isActive);
        }

        if (availablePopup.activeSelf)
        {
            SetChallengeAgainGroup();
            UnlockChallenge();
            if (UIManager.Ins.IndexChallenge > 0)
            {
                for (int i = 0; i < UIManager.Ins.IndexChallenge; i++)
                {
                    if (i == 4)
                        backGround = backGrounFinal;
                    ChallengeItem challengeItem = challengeList[i].GetComponent<ChallengeItem>();
                    challengeItem.ChallengeItemActive(backGround, upDir);
                    challengeItem.OverChallenge(status);
                }
            }
        }
    }

    public void ChallengeAgainBtn()
    {
        ManagerAds.Ins.ShowRewardedVideo((b) =>
        {
            if (b)
            {
                var now = DateTime.Now.ToString();
                UIManager.Ins.TimeWaitLose = now;
                UIManager.Ins.TimeWaitWin = now;
            }

            SetChallengeAgainGroup();
            ++UIManager.Ins.VideoCount;
            UIManager.Ins.popup.claimReward.OnCheckCanClaimReward(CostType.Video);
        });
    }

    public void OpenStorePopup()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        if (storePopup != null)
        {
            bool isActive = storePopup.activeSelf;
            storePopup.SetActive(!isActive);
        }

//        if (storePopup.activeSelf == true)
//            BuySkinController.Ins.GenerateBuySkin();
    }

    public void OpenAchivementPopup()
    {
        ClearAchivement();
        UpdateAchivements();
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        for (int i = 0; i < achivementNames.Length; i++)
        {
            AchivementItem item = Instantiate(AchivementItemPre, Content.transform).GetComponent<AchivementItem>();
            item.SetUpAchivement(achivementNames[i], achivements[i]);
        }

        if (achivementPopup != null)
        {
            bool isActive = achivementPopup.activeSelf;
            achivementPopup.SetActive(!isActive);
        }
    }

    public void OpenSettingPopup()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        bool check = settingPopup.activeSelf;

        if (!check)
        {
            settingPopup.SetActive(!check);
            StartCoroutine(ShowSetting());
        }
        else
            StartCoroutine(HideSetting());
    }

    void UpdateAchivements()
    {
        achivements.Add(UIManager.Ins.NumberTop.ToString());
        achivements.Add(UIManager.Ins.NumberKilled.ToString());
        achivements.Add((float)System.Math.Round(UIManager.Ins.PlayerMoveDistance, 2) + " m");
        achivements.Add((float)System.Math.Round(UIManager.Ins.PlayerDistance, 2) + " m");
    }

    void ClearAchivement()
    {
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            Destroy(Content.transform.GetChild(i).gameObject);
        }

        achivements.Clear();
    }

    IEnumerator ShowSetting(float time = 1f)
    {
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime / time;

            var f = Mathf.Lerp(viewport.offsetMin.y, 0, i);
            viewport.offsetMin = new Vector2(viewport.offsetMin.x, f);
            yield return null;
        }
    }

    IEnumerator HideSetting(float time = 1f)
    {
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime / time;

            var f = Mathf.Lerp(viewport.offsetMin.y, 595, i);
            viewport.offsetMin = new Vector2(viewport.offsetMin.x, f);
            yield return null;
        }
        settingPopup.Hide();
    }

    public void ChallengeState()
    {
        UIManager.Ins.gamePlay.ChallengeState();
        OpenAvailablePopup();
    }

    public void UnlockChallenge()
    {
        Sprite backGround = this.backGround;
        if (UIManager.Ins.IndexChallenge == 4)
            backGround = backGrounFinal;

        if (UIManager.Ins.IndexChallenge > 0)
            challengeList[UIManager.Ins.IndexChallenge - 1].GetComponent<ChallengeItem>().OverChallenge(status);

        if (UIManager.Ins.IndexChallenge < 5)
            challengeList[UIManager.Ins.IndexChallenge].GetComponent<ChallengeItem>().ChallengeItemActive(backGround, upDir);
    }

    public void LockChallenge()
    {
        foreach (var item in challengeList)
        {
            item.GetComponent<ChallengeItem>().Reset();
        }

        UIManager.Ins.IndexChallenge = 0;

        UnlockChallenge();
    }

    // Animation
    public void PlayComeReward()
    {
        animation.Play("ComeRewardPopupAnim");
    }
    public void PlayLeaveReward()
    {
        animation.Play("LeaveRewardPopupAnim");
    }

}
