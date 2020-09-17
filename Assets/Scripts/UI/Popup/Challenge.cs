using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{
    public string timeJoin;
    private PlayerType type;
    Coroutine spawnTime;

    private void Awake()
    {
        DateTime timeWaitWin = ConvertStringToDateTime(UIManager.Ins.TimeWaitWin);
        DateTime timeWaitLose = ConvertStringToDateTime(UIManager.Ins.TimeWaitLose);
        if (timeWaitWin > DateTime.Now && timeWaitWin > timeWaitLose)
            this.type = PlayerType.Win;

        else if (timeWaitWin < DateTime.Now && timeWaitWin > timeWaitLose)
        {
            UIManager.Ins.IndexChallenge = 0;

            var timeSpan = new TimeSpan(12, 0, 0);
            var newTime = timeWaitWin + timeSpan;
            UIManager.Ins.TimeWaitLose = newTime.ToString();

            if (newTime > DateTime.Now)
                this.type = PlayerType.Lose;
        }
        else
            // UIManager.Ins.popup.LockChallenge();
            UIManager.Ins.IndexChallenge = 0;
    }

    public void SetTimeLose()
    {
        this.type = PlayerType.Lose;
        var timeSpan = new TimeSpan(12, 0, 0);
        var newTime = DateTime.Now + timeSpan;

        UIManager.Ins.TimeWaitLose = newTime.ToString();
    }

    public void SetTimeWin()
    {
        this.type = PlayerType.Win;
        var timeSpan = new TimeSpan(0, 30, 0);
        var newTime = DateTime.Now + timeSpan;

        UIManager.Ins.TimeWaitWin = newTime.ToString();
    }

    public bool CheckIsCanPlayChallenge()
    {
        var now = DateTime.Now;
        if (this.type == PlayerType.Lose)
        {
            UIManager.Ins.popup.timeWaitWin.Hide();
            if (ConvertStringToDateTime(UIManager.Ins.TimeWaitLose) - now <= TimeSpan.Zero)
            {
                return true;
            }
            else
            {
                SpawnTime(UIManager.Ins.TimeWaitLose);
            }

        }

        if (this.type == PlayerType.Win)
        {
            if (ConvertStringToDateTime(UIManager.Ins.TimeWaitWin) - now >= TimeSpan.Zero)
            {
                SpawnTime(UIManager.Ins.TimeWaitWin);
                UIManager.Ins.popup.timeWaitWin.Show();
                return true;
            }
        }

        // switch (this.type)
        // {
        //     case PlayerType.Lose:
        //         return ConvertStringToDateTime(UIManager.Ins.TimeWaitLose) - now <= TimeSpan.Zero;

        //     case PlayerType.Win:
        //         return ConvertStringToDateTime(UIManager.Ins.TimeWaitWin) - now >= TimeSpan.Zero;
        // }

        return false;
    }

    public void SpawnTime(string time)
    {
        if (spawnTime != null)
            StopCoroutine(spawnTime);
        spawnTime = StartCoroutine(Spawntime(ConvertStringToDateTime(time)));
    }

    IEnumerator Spawntime(DateTime newTime)
    {
        var wt = new WaitForSeconds(1);
        while (newTime >= DateTime.Now)
        {
            var span = newTime - DateTime.Now;
            timeJoin = string.Format("{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds);
            //timeJoin = span.Hours + " : " + span.Minutes + " : " + span.Seconds;
            yield return wt;
        }

        if (this.type == PlayerType.Win)
        {
            SetTimeLose();
            UIManager.Ins.popup.LockChallenge();
        }

        UIManager.Ins.popup.SetChallengeAgainGroup();

    }

    public static DateTime ConvertStringToDateTime(string s)
    {
        s = s.Replace('-', '/');
        s = s.Replace('_', ':');
        return Convert.ToDateTime(s);
        //Debug.Log(s);
        // return Convert.ToDateTime(s, new DateTimeFormatInfo { FullDateTimePattern = "mm:ss" });
        //return DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
    }
}

public enum PlayerType
{
    Lose,
    Win
}
