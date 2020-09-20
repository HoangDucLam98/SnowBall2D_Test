using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    public static event System.Action playerDeath;
    public static UIGameplay Ins;
    private Player player;
    public EndGame end;
    public GameObject playGame;
    public Spawner spawner;
    public Text totalKilled;
    public Text dangerTxt;
    public GameObject intro;
    public GameObject boxPush;
    public GameObject revivalPlayer;

    // Player push Enemy
    public Text namePushedTarget;
    public Text distancePushed;
    public Text BlowingTxt;

    public bool notMoving;
    private bool isPushByPlayer;
    private float distance;
    private float ballSize;
    public Image timeRevivalImage;
    public bool revive;

    // Demo cost revieve
    public Text costRevival;
    public TextMeshProUGUI timeRevival;
    private int costRevive;
    public GameObject reviveEffect;

    // AI Enemy
    public TypeAI typeAI;

    // Challenge
    public AvailableState currentState;

    // Animation
    public Animation animation;

    public RectTransform LifeSaver;

    public int totalCoinClaim { get; private set; }

    bool enoughCoin;
    public Button reviveCoin, reviveVideo;

    // Start is called before the first frame update
    void Start()
    {
        if (Ins == null)
        {
            Ins = this;
        }

        totalCoinClaim = 0;

        // reviveCoin.onClick.AddListener(GoBackCoin);
        // reviveVideo.onClick.AddListener(GoBackVideo);
    }

    private void FixedUpdate()
    {
        if (GameController.Instance.listObject.Count > 1)
        {
            if (GameController.Instance.listObject[1].gameObject.name == "Player(Clone)")
                player = GameController.Instance.listObject[1].gameObject.transform.GetComponent<Player>();
        }

        if (isPushByPlayer)
        {
            distance += Time.fixedDeltaTime * ballSize * 3000;
            changeUIDistance((float)System.Math.Round(distance, 2));
        }
        if (revive)
        {
            HideRevivalButton();
        }
    }

    public void ChallengeState()
    {
        currentState = AvailableState.Challenge;
    }

    public void NormalState()
    {
        currentState = AvailableState.Normal;
    }

    public void OnPlay()
    {
        if (currentState == AvailableState.Normal)
        {
            int index = Random.Range(0, 5);
            // int index = 0;
            switch (index)
            {
                case 0:
                    typeAI = TypeAI.SizeBall;
                    break;
                case 1:
                    typeAI = TypeAI.DirectionToTarget;
                    break;
                case 2:
                    typeAI = TypeAI.FollowTargetInTime;
                    break;
                case 3:
                    typeAI = TypeAI.FollowDirectionInTime;
                    break;
                case 4:
                    typeAI = TypeAI.SmartAI;
                    break;
            }
        }

        if (currentState == AvailableState.Challenge)
        {
            switch (UIManager.Ins.IndexChallenge)
            {
                case 0:
                    typeAI = TypeAI.SizeBall;
                    break;
                case 1:
                    typeAI = TypeAI.DirectionToTarget;
                    break;
                case 2:
                    typeAI = TypeAI.FollowTargetInTime;
                    break;
                case 3:
                    typeAI = TypeAI.FollowDirectionInTime;
                    break;
                case 4:
                    typeAI = TypeAI.SmartAI;
                    break;
            }
        }

        StartCoroutine(showIntro());
        OpenGamePlay();
        spawner.SetNumberEnemySpawn();
        spawner.OnSpawn();
    }

    IEnumerator showIntro()
    {
        intro.Show();
        notMoving = true;
        yield return new WaitForSeconds(2f);
        intro.Hide();
        notMoving = false;
    }

    public IEnumerator CoShowBoxPush(string name, float size)
    {

        if (boxPush.activeSelf)
        {
            boxPush.Hide();
        }

        BlowingTxt.text = "Blowing!";
        BlowingTxt.color = Color.cyan;

        namePushedTarget.text = name;
        boxPush.Show();
        isPushByPlayer = true;
        distance = 0;
        ballSize = size;

        yield return new WaitForSeconds(1f);
        isPushByPlayer = false;

        // Change total distance pushed by Player
        UIManager.Ins.ChangePlayerDistance(distance);

        yield return new WaitForSeconds(1f);
        boxPush.Hide();
    }

    public void ChangeBlowingText()
    {
        BlowingTxt.text = "Defeated!";
        BlowingTxt.color = Color.red;
    }

    public void ShowBoxPush(string name, float size)
    {
        if (size > 0)
            StartCoroutine(CoShowBoxPush(name, size));
    }

    public void OpenGamePlay()
    {
        if (playGame != null)
        {
            bool isActive = playGame.activeSelf;
            playGame.SetActive(!isActive);
        }

        if (boxPush.activeSelf)
        {
            boxPush.Hide();
        }
    }

    public void SetTotalCoinClaim(int coins)
    {
        totalCoinClaim += coins;
    }

    public void NextButton()
    {
        int index = Random.Range(0, 3);
        Player.Done = false;
        if (index != 0)
        {
            ClearData();
            UIManager.Ins.UpdateCoins(totalCoinClaim);
            totalCoinClaim = 0;
            UIManager.Ins.home.OnShow();
            if (currentState == AvailableState.Challenge)
            {
                UIManager.Ins.popup.OpenAvailablePopup();
            }

            AudioManager1.Ins.PlayMusic(MusicType.HomeMusic);
            AudioManager1.Ins.PlaySound(SoundType.Click);
        }
        else
        {
            ManagerAds.Ins.ShowInterstitial(() =>
            {
                ClearData();
                UIManager.Ins.UpdateCoins(totalCoinClaim);
                totalCoinClaim = 0;
                UIManager.Ins.home.OnShow();
                if (currentState == AvailableState.Challenge)
                {
                    UIManager.Ins.popup.OpenAvailablePopup();
                }

                AudioManager1.Ins.PlayMusic(MusicType.HomeMusic);
                AudioManager1.Ins.PlaySound(SoundType.Click);
            });
        }
    }

    public void SetNumberKilled(int numberKilled)
    {
        totalKilled.text = numberKilled.ToString();
    }

    public void SetDangerText(int time)
    {
        dangerTxt.text = "Danger: " + time + " s";
    }

    public void OnPlayerDeath(bool checkVictory = false)
    {
        if (currentState == AvailableState.Challenge)
        {
            if (!checkVictory)
            {
                UIManager.Ins.popup.LockChallenge();
                UIManager.Ins.popup.challengeScr.SetTimeLose();
            }
            if (checkVictory)
                UIManager.Ins.popup.challengeScr.SetTimeWin();
        }
        UIManager.Ins.PlayVibrate();
        OpenGamePlay();
        if (playerDeath != null)
            playerDeath();
        StartCoroutine(CoOnPlayerDeath(checkVictory));
    }

    IEnumerator CoOnPlayerDeath(bool checkVictory)
    {
        var data = GameController.Instance.SortListCharacter();
        if (checkVictory)
        {
            end.Victory();

            // AudioManager.PlaySound(AudioManager.victoryName);
            AudioManager1.Ins.PlaySound(SoundType.Win);
        }
        yield return new WaitForSeconds(2f);
        if (checkVictory)
        {
            end.Victory();
        }
        end.UpdateState(data);
        //NextButton();
        //UIManager.Ins.home.PlayComeHome();
    }

    public void changeUIDistance(float distance)
    {
        distancePushed.text = distance + " m";
    }

    public void RevivalPlayer()
    {
        reviveCoin.Hide();
        reviveVideo.Hide();
        revive = false;
        enoughCoin = false;
        player.Revival();
        StartCoroutine("CoRevivalPlayer");
    }

    void CheckCoin(int costRevive)
    {
        if (Mathf.Abs(costRevive) <= UIManager.Ins.PlayerCoins)
            enoughCoin = true;
    }

    IEnumerator CoRevivalPlayer()
    {
        costRevive = -200 * (int)Mathf.Pow(2, player.numberGoBack);
        CheckCoin(costRevive);
        revivalPlayer.Show();

        if (enoughCoin)
        {
            reviveCoin.Show();
            costRevival.text = costRevive.ToString();
        }
        else
        {
            reviveVideo.Show();
        }

        var t = 5;

        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime / t;
            timeRevivalImage.fillAmount = 1 - i;
            timeRevival.text = (Mathf.Ceil(5 - i * 5)).ToString();
            yield return null;
        }

        if (!revive)
        {
            HideRevivalButton();
            OnPlayerDeath();
        }
    }

    public void HideRevivalButton()
    {
        StopCoroutine("CoRevivalPlayer");
        revivalPlayer.Hide();
        timeRevivalImage.fillAmount = 1;
    }

    public void CloseButton()
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        HideRevivalButton();
        OnPlayerDeath();
    }

    public void GoBack()
    {
        if (!enoughCoin)
        {
            ManagerAds.Ins.ShowRewardedVideo((b) =>
        {
            if (b)
            {
                Player.Done = false;
                player.numberGoBack++;
                revive = true;
                StartCoroutine(PlayEffect());

                AudioManager1.Ins.PlaySound(SoundType.Revive);

                ++UIManager.Ins.VideoCount;
                UIManager.Ins.popup.claimReward.OnCheckCanClaimReward(CostType.Video);
            }
        });
        }
        else
        {
            UIManager.Ins.UpdateCoins(costRevive);
            Player.Done = false;
            player.numberGoBack++;
            revive = true;
            StartCoroutine(PlayEffect());

            AudioManager1.Ins.PlaySound(SoundType.Revive);
        }
    }

    IEnumerator PlayEffect()
    {
        GameObject effectPlayer = (GameObject)Instantiate(reviveEffect, MapControler.Instance.positionRevive, transform.rotation);
        effectPlayer.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        yield return new WaitForSeconds(1f);
        Destroy(effectPlayer.gameObject);
        player.Revival();
    }

    public void LoadNewChallenge()
    {
        if (currentState == AvailableState.Challenge && UIManager.Ins.IndexChallenge < 5)
        {
            UIManager.Ins.IndexChallenge++;
            UIManager.Ins.popup.UnlockChallenge();
        }
    }

    IEnumerator CoSavePlayer()
    {
        Vector2 playerPosition = player.skeletonAnimation.gameObject.transform.position - Vector3.one * 0.5f;
        float t = 0;
        float time = Vector2.Distance(MapControler.Instance.positionRevive, playerPosition) / 9;
        while (t <= 1)
        {
            t += Time.deltaTime / time;
            player.gameObject.transform.position = Vector2.Lerp(playerPosition, MapControler.Instance.positionRevive, t);
            yield return null;
        }
        // PlayLeaveLifeSaver();
        player.faceObject.tag = "Player";
        // player.DustIce.Show();
        player.faceObject.GetComponent<BoxCollider2D>().isTrigger = false;
        player.currentState = StatusState.Chasing;
        UIManager.Ins.IsFreeLifeSaver = false;
        Player.Done = false;
    }

    public void PLaySaver()
    {
        StartCoroutine(ShowLifeSave());
    }

    IEnumerator ShowLifeSave()
    {
        SetIdlePlayer();

        // LifeSaver.transform.localScale = new Vector3(player.faceObject.localScale.x, LifeSaver.transform.localScale.y, 0);
        LifeSaver.localScale = new Vector3(player.faceObject.localScale.x, player.faceObject.localScale.y, 0);
        Vector3 startPos = LifeSaver.localPosition;
        float t = 0;
        float time = 1f;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            LifeSaver.localPosition = Vector2.Lerp(LifeSaver.localPosition, Vector3.zero, t);
            yield return null;
        }

        yield return StartCoroutine(CoSavePlayer());
        float i = 0;
        float timeHide = 1f;
        while (i < 1)
        {
            i += Time.deltaTime / timeHide;
            LifeSaver.localPosition = Vector2.Lerp(LifeSaver.localPosition, startPos, i);
            yield return null;
        }
        ResetLifeSaver();
    }


    // ANIMATION
    public void PlayLeaveSpin()
    {
        animation.Play("LeaveSpinAnim");
    }
    public void PlayLeaveSpinAgain()
    {
        animation.Play("LeaveSpinAgainAnim");
    }
    public void PlayComeSpin()
    {
        animation.Play("ComeSpinAnim");
    }
    public void PlayComeSpinAgain()
    {
        animation.Play("ComeSpinAgainAnim");
    }
    public void PlayComeRank()
    {
        animation.Play("ComeRankAnim");
    }

    public void ResetLifeSaver()
    {
        LifeSaver.localScale = Vector3.one;
    }

    public void SetIdlePlayer()
    {
        player.EndStun();
        player.faceObject.tag = "Untagged";
        // player.DustIce.Hide();
        player.currentState = StatusState.Idle;
        player.faceObject.GetComponent<BoxCollider2D>().isTrigger = true;
        player.clearBall();
    }

    public void StartSpin()
    {
        Wheel.Ins.StartSpin();
    }

    public void SpinAgain()
    {
        Wheel.Ins.SpinAgain();
    }

    void ClearData()
    {
        for (int i = 0; i < spawner.gameObject.transform.childCount; i++)
        {
            Destroy(spawner.gameObject.transform.GetChild(i).gameObject);
        }
        dangerTxt.Hide();
        MapControler.Instance.Clear();
        GameController.Instance.Clear();
        end.HideAllListReward();
        end.Clear();
    }
}

public enum TypeAI
{
    SizeBall, DirectionToTarget, FollowTargetInTime, FollowDirectionInTime, SmartAI
}

public enum AvailableState { Normal, Challenge }
