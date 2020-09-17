using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIRename : MonoBehaviour
{
    public LoopVerticalScrollRect scrollView;

    public InputField nameInput, countryInput;

    private string countryName;
    private string pName;
    public int index;
    public Button okBtn;

    public List<CountryBotName> temp;

    private void Start()
    {
        okBtn.onClick.AddListener(OnCommit);
        countryInput.onEndEdit.AddListener(FindWithString);
        nameInput.characterLimit = 20;
    }

    public void OnShow()
    {
        this.Show();
        temp = GameController.Instance.data.botNames;
        scrollView.totalCount = temp.Count;

        nameInput.text = UIManager.Ins.PlayerName;
        countryInput.text = UIManager.Ins.PlayerCountry;
        countryName = UIManager.Ins.PlayerCountry;
        index = temp.FindIndex(s => s.countryCode == UIManager.Ins.PlayerCountry);
        scrollView.ScrollToCell(index, 10000);
        scrollView.RefillCells();
        // selectedIm.SetParent(scrollView);
    }

    public void ShowBotName(List<CountryBotName> botNames, int index)
    {
        temp = botNames;

        scrollView.totalCount = temp.Count;
        scrollView.RefillCells();
        scrollView.ScrollToCell(index, 600);
    }

    public void OnCommit()
    {
        AudioManager1.Ins.PlaySound(SoundType.Click);
        var s = nameInput.text;
        if (s.Equals(""))
        {
            s = "Player098";
        }

        if (s.Length > 20)
        {
            s = s.Substring(0, 19);
        }
        // var t = GameManager.ins.PlayerInformation;

        UIManager.Ins.PlayerName = s;
        UIManager.Ins.home.playerName.text = s;

        UIManager.Ins.PlayerCountry = countryName;
        GameController.Instance.idFlag = this.index;
        // Debug.Log(UIManager.Ins.PlayerCountry);
        // UIManager.Ins.PlayerCountry = Application.systemLanguage.ToCountryCode();

        this.Hide();
    }

    public void OnChangeSelected(int index)
    {
        this.index = index;
        scrollView.RefreshCells();
        countryInput.text = temp[index].countryCode;
        countryName = temp[index].countryCode;
    }

    public void FindWithString(string m)
    {
        temp = GameController.Instance.data.botNames.Where(s => Contains(s.countryName, m) || Contains(s.countryCode, m)).ToList();
        ShowBotName(temp, 0);
    }

    bool Contains(string s1, string s2)
    {
        s1 = s1.ToLower();
        s2 = s2.ToLower();
        var arr = s2.ToArray();
        foreach (var a in arr)
        {
            if (s1.Contains(a))
            {
                s1.Replace(a.ToString(), "");
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}
