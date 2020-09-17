using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagItemUI : MonoBehaviour
{
    public Image im;
    public GameObject selected;
    public Button btn;
    private int index;

    private void Start()
    {
        btn.onClick.AddListener(OnSelected);
    }

    private void OnSelected()
    {
        selected.Show();
        UIManager.Ins.popup.rename.OnChangeSelected(index);
    }

    void ScrollCellIndex(int idx)
    {
        im.sprite = UIManager.Ins.popup.rename.temp[idx].icon;
        index = idx;
        if (idx == UIManager.Ins.popup.rename.index)
        {
            selected.Show();
        }
        else
        {
            selected.Hide();
        }
    }
}
