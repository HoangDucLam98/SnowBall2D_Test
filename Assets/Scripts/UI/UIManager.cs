using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Globalization;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager Ins;
    public FigureData figureData;
    public UIHome home;
    public UIGameplay gamePlay;
    public UIPopup popup;

    const string coinsKey = "PlayerCoins";
    private int playerCoins;
    public int PlayerCoins
    {
        get { return playerCoins; }
        set
        {
            playerCoins = value;
            PlayerPrefs.SetInt(coinsKey, playerCoins);
        }
    }

    const string VideoKey = "Videokey";
    private int videoCount;
    public int VideoCount
    {
        get { return videoCount; }
        set
        {
            videoCount = value;
            PlayerPrefs.SetInt(VideoKey, value);
        }
    }

    const string nameKey = "PlayerName";
    private string playerName;
    public string PlayerName
    {
        get { return playerName; }
        set
        {
            playerName = value;
            PlayerPrefs.SetString(nameKey, playerName);
        }
    }

    const string playerCountryKey = "PlayerCountry";
    private string playerCountry;
    public string PlayerCountry
    {
        get { return playerCountry; }
        set
        {
            playerCountry = value;
            PlayerPrefs.SetString(playerCountryKey, playerCountry);
        }
    }

    const string figureIdKey = "FigureSelected";
    private int playerFigure;
    public int PlayerFigure
    {
        get { return playerFigure; }
        set
        {
            playerFigure = value;
            PlayerPrefs.SetInt(figureIdKey, playerFigure);
        }
    }

    const string numberKilledKey = "NumberKilled";
    private int numberKilled;
    public int NumberKilled
    {
        get { return numberKilled; }
        set
        {
            numberKilled = value;
            PlayerPrefs.SetInt(numberKilledKey, numberKilled);
        }
    }

    const string playerDistanceKey = "PlayerDistance";
    private float playerDistance;
    public float PlayerDistance
    {
        get { return playerDistance; }
        set
        {
            playerDistance = value;
            PlayerPrefs.SetFloat(playerDistanceKey, playerDistance);
        }
    }

    const string playerMoveDistanceKey = "PlayerMoveDistance";
    private float playerMoveDistance;
    public float PlayerMoveDistance
    {
        get { return playerMoveDistance; }
        set
        {
            playerMoveDistance = value;
            PlayerPrefs.SetFloat(playerMoveDistanceKey, playerMoveDistance);
        }
    }

    const string numberTopKey = "NumberTop";
    private int numberTop;
    public int NumberTop
    {
        get { return numberTop; }
        set
        {
            numberTop = value;
            PlayerPrefs.SetInt(numberTopKey, numberTop);
        }
    }

    // Challenge
    const string indexRewardKey = "IndexReward";
    private int indexReward;
    public int IndexReward
    {
        get { return indexReward; }
        set
        {
            indexReward = value;
            PlayerPrefs.SetInt(indexRewardKey, indexReward);
        }
    }

    const string indexChallengeKey = "IndexChallenge";
    private int indexChallenge;
    public int IndexChallenge
    {
        get { return indexChallenge; }
        set
        {
            indexChallenge = value;
            PlayerPrefs.SetInt(indexChallengeKey, indexChallenge);
        }
    }

    const string timeWaitLoseKey = "TimeWaitLose";
    private string timeWaitLose;
    public string TimeWaitLose
    {
        get { return timeWaitLose; }
        set
        {
            timeWaitLose = value;
            PlayerPrefs.SetString(timeWaitLoseKey, timeWaitLose);
        }
    }

    const string timeWaitWinKey = "TimeWaitWin";
    private string timeWaitWin;
    public string TimeWaitWin
    {
        get { return timeWaitWin; }
        set
        {
            timeWaitWin = value;
            PlayerPrefs.SetString(timeWaitWinKey, timeWaitWin);
        }
    }

    private const string TimeJoinChallenge = "TimeJoinChallenge";

    private DateTime timeJoin;

    // Setting
    const string ringKey = "Ring";
    private int ring;
    public int Ring
    {
        get { return ring; }
        set
        {
            ring = value;
            PlayerPrefs.SetInt(ringKey, ring);
        }
    }

    const string musicKey = "Music";
    private int music;
    public int Music
    {
        get { return music; }
        set
        {
            music = value;
            PlayerPrefs.SetInt(musicKey, music);
        }
    }

    const string sfxKey = "Sfx";
    private int sfx;
    public int Sfx
    {
        get { return sfx; }
        set
        {
            sfx = value;
            PlayerPrefs.SetInt(sfxKey, sfx);
        }
    }

    const string DAY_ONLINE = "DayOnline";
    private List<int> dayOnLine;
    public List<int> DayOnLine
    {
        get { return dayOnLine; }
        set
        {
            dayOnLine = value;
            EncryptHelper.SaveList(DAY_ONLINE, value);
        }
    }

    const string IS_FIRST_PLAY = "FirstPlay";

    const string IS_FREE_LIFESAVER = "FreeLifeSaver";
    private bool isFreeLifeSaver;
    public bool IsFreeLifeSaver
    {
        get { return isFreeLifeSaver; }
        set
        {
            isFreeLifeSaver = value;
            EncryptHelper.SetInt(IS_FREE_LIFESAVER, isFreeLifeSaver ? 1 : 0);
        }
    }

    const string IS_BUY_PACK_VIP = "BuyPackVip";
    private bool isBuyPackVip;
    public bool IsBuyPackVip
    {
        get { return isBuyPackVip; }
        set
        {
            isBuyPackVip = value;
            EncryptHelper.SetInt(IS_BUY_PACK_VIP, isBuyPackVip ? 1 : 0);
        }
    }

    const string PLAYER_MASK = "PlayerMask";
    private List<PlayerMaskInfor> playerMasks;
    public List<PlayerMaskInfor> PlayerMasks
    {
        get => playerMasks;
        set
        {
            playerMasks = value;
            SavingDataWithJson.SaveListDataWithEncrypt(value, PLAYER_MASK);
        }
    }

    public int previousCoin;

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }

        IsFirstPlayGame();
    }

    private void IsFirstPlayGame()
    {
//        PlayerPrefs.DeleteKey(IS_FIRST_PLAY);
        if (!PlayerPrefs.HasKey(IS_FIRST_PLAY))
        {
            PlayerPrefs.SetInt(IS_FIRST_PLAY, 1);
            PlayerCoins = 0;
            PlayerName = "";
            PlayerCountry = "";
            IsFreeLifeSaver = false;
            IsBuyPackVip = false;

            InfoFigure data = figureData.figures[31];
            PlayerFigure = data.idFigure;

            NumberKilled = 0;
            PlayerDistance = 0;
            PlayerMoveDistance = 0;
            NumberTop = 0;
            VideoCount = 0;

            IndexReward = 0;
            IndexChallenge = 0;

            var now = DateTime.Now;
            TimeWaitLose = now.ToString();
            TimeWaitWin = now.ToString();

            var t = DateTime.Now;
            var l = new List<int> { t.Day, t.Month, t.Year };
            DayOnLine = l;

            // Setting: 1 = turn on
            Ring = 1;
            Music = 1;
            Sfx = 1;

            CheckPlayerFigure(PlayerFigure);
            InitMasks();
        }
        else
        {
            PlayerCoins = PlayerPrefs.GetInt(coinsKey);
            PlayerName = PlayerPrefs.GetString(nameKey);
            PlayerCountry = PlayerPrefs.GetString(playerCountryKey);
            PlayerFigure = PlayerPrefs.GetInt(figureIdKey);

            NumberKilled = PlayerPrefs.GetInt(numberKilledKey);
            PlayerDistance = PlayerPrefs.GetFloat(playerDistanceKey);
            PlayerMoveDistance = PlayerPrefs.GetFloat(playerMoveDistanceKey);
            NumberTop = PlayerPrefs.GetInt(numberTopKey);
            VideoCount = PlayerPrefs.GetInt(VideoKey);

            IndexReward = PlayerPrefs.GetInt(indexRewardKey);
            IndexChallenge = PlayerPrefs.GetInt(indexChallengeKey);
            TimeWaitLose = PlayerPrefs.GetString(timeWaitLoseKey);
            TimeWaitWin = PlayerPrefs.GetString(timeWaitWinKey);
            dayOnLine = EncryptHelper.LoadList<int>(DAY_ONLINE);

            // Setting: 1 = turn on
            Ring = PlayerPrefs.GetInt(ringKey);
            Music = PlayerPrefs.GetInt(musicKey);
            Sfx = PlayerPrefs.GetInt(sfxKey);

            // CheckNewDay();
            CheckFreeLifeSave();
            CheckPlayerFigure(PlayerFigure);
            PlayerMasks = EncryptHelper.LoadList<PlayerMaskInfor>(PLAYER_MASK);
            LoadMasks();
        }
    }

    private void Start()
    {
        // AudioManager.PlaySound(AudioManager.soundTrackName);
        AudioManager1.Ins.PlayMusic(MusicType.HomeMusic);
        if (Music == 0)
            AudioManager1.Ins.TurnOffMusic();
        if (Sfx == 0)
            AudioManager1.Ins.TurnOffSound();
        CheckNewDay();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     foreach (var item in figureData.figures)
        //     {
        //         if (item.costType == CostType.Vip)
        //             item.isBuy = false;
        //     }
        // }
        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     foreach (var item in figureData.figures)
        //     {
        //         if (item.costType == CostType.Coin)
        //             item.isBuy = false;
        //     }
        // }
        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach (var item in figureData.figures)
            {
                if (item.costType == CostType.Video)
                    item.isBuy = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (var item in figureData.figures)
            {
                if (item.costType == CostType.Run || item.costType == CostType.KillEnemy)
                    item.isBuy = false;
            }
        }
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     TimeWaitWin = (DateTime.Now + new TimeSpan(0, 0, 10)).ToString();
        //     popup.SetChallengeAgainGroup();
        // }
        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     TimeWaitLose = (DateTime.Now + new TimeSpan(0, 0, 10)).ToString();
        //     popup.SetChallengeAgainGroup();
        // }
    }

    void InitMasks()
    {
        var masks = new List<PlayerMaskInfor>();

        foreach (var item in figureData.figures)
        {
            masks.Add(new PlayerMaskInfor()
            {
                id = item.idFigure,
                isBuy = item.isBuy
            });
        }

        PlayerMasks = masks;
    }

    void LoadMasks()
    {
        int count = 0;
        foreach (var item in PlayerMasks)
        {
            figureData.figures[count].isBuy = PlayerMasks[count].isBuy;
            count++;
        }
    }

    public void CheckPlayerFigure(int id)
    {
        InfoFigure data = figureData.figures[id];
        if (!data.isBuy)
            data.isBuy = true;

        if (!data.isUsing)
            data.isUsing = true;

    }

    public void CheckFreeLifeSave()
    {
        IsFreeLifeSaver = EncryptHelper.GetInt(IS_FREE_LIFESAVER) == 1;
        if (IsFreeLifeSaver)
        {
            UIManager.Ins.home.OnShowFreeLifeSave();
        }
    }

    public void CheckNewDay()
    {
        IsBuyPackVip = EncryptHelper.GetInt(IS_BUY_PACK_VIP) == 1;
        var t = DateTime.Now;
        if (dayOnLine[0] != t.Day || dayOnLine[1] != t.Month || dayOnLine[2] != t.Year)
        {
            InfoFigure data = figureData.figures.Find(n => n.costType == CostType.Day);
            if (!data.isBuy)
                popup.claimReward.OnShow(data);
            data.isBuy = true;

            if (IsBuyPackVip && !IsFreeLifeSaver)
            {
                IsFreeLifeSaver = true;
                UIManager.Ins.home.OnShowFreeLifeSave();
            }

            DayOnLine = new List<int> { t.Day, t.Month, t.Year };
        }
    }

    public void PlayVibrate()
    {
        if (Ring == 1)
            Handheld.Vibrate();
    }

    public void LoadGamePlay()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadHome()
    {
        SceneManager.LoadScene(0);
    }

    public bool UpdateCoins(int coins)
    {
        if ((coins < 0 && PlayerCoins > Mathf.Abs(coins)) || coins > 0)
        {
            PlayerCoins += coins;
            //home.playerCoins.text = PlayerCoins.ToString();
            return true;
        }
        else
        {
            return false;
        }
    }

    // public void UpdateNamePlayer(string name)
    // {
    //     PlayerName = name;
    //     home.SetTextInputField(PlayerName);
    // }

    public void ChangeFigureData(int figureId, bool check = false)
    {
        if (!check)
        {
            // Select figure
            ChangeUsing();
            PlayerFigure = figureId;
            ChangeUsing();
        }
        else
        {
            // Buy figure
            InfoFigure data = figureData.figures[figureId];
            data.isBuy = !data.isBuy;

            UpdatePlayerMasks(figureId);
        }
    }

    public void UpdatePlayerMasks(int id)
    {
        var masks = PlayerMasks;
        var i = masks[id];
        i.isBuy = true;
        masks[id] = i;
        PlayerMasks = masks;
    }

    void ChangeUsing()
    {
        InfoFigure data = figureData.figures.Find(n => n.idFigure == PlayerFigure);
        data.isUsing = !data.isUsing;
    }

    public void ChangeNumberKilled()
    {
        ++NumberKilled;
        UIManager.Ins.popup.claimReward.OnCheckCanClaimReward(CostType.KillEnemy);
    }

    public void ChangePlayerDistance(float distance)
    {
        PlayerDistance += distance;
    }

    public void ChangePlayerMoveDistance(float distance)
    {
        PlayerMoveDistance += distance;

        popup.claimReward.OnCheckCanClaimReward(CostType.Run);
    }

    public void ChangeNumberTop()
    {
        NumberTop++;
        popup.claimReward.OnCheckCanClaimReward(CostType.Top);
    }

    public InfoFigure FindByID(int id)
    {
        InfoFigure data = figureData.figures.Find(n => n.idFigure == id);
        return data;
    }

}

[System.Serializable]
public struct PlayerMaskInfor
{
    public int id;
    public bool isBuy;
}
