using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BuySkinController : MonoBehaviour
{
    public static BuySkinController Ins;

    [SerializeField]
    private Transform figureContent;
    [SerializeField]
    private GameObject figureButton;
    [SerializeField]
    private GridLayoutGroup gridLayout;
    private FigureItem previousFigureItem, currentFigureItem, selectedFigureItem;

    public Button[] listButton;
    public Text costButtonTxt, videoTxt;
    private InfoFigure currentInfoData;
    public Button missionInfo;

    private int _currentFigure;

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // listInfo = new List<InfoFigure>();

        // foreach (var item in figureData.figures)
        // {
        //     InfoFigure data = new InfoFigure();
        //     data.heroName = item.heroName;
        //     data.isUsing = item.isUsing;
        //     data.costType = item.costType;
        //     data.cost = item.cost;
        //     data.asset = item.asset;
        //     data.isBuy = item.isBuy;
        //     data.idFigure = item.idFigure;

        //     listInfo.Add(data);
        // }

        // GenerateBuySkin();
    }

    public void GenerateBuySkin()
    {
        ClearListSkin();
        if (missionInfo.gameObject.activeSelf)
        {
            missionInfo.Hide();
        }

        if (UIManager.Ins.figureData.figures.Count < 4)
        {
            gridLayout.constraintCount = UIManager.Ins.figureData.figures.Count;
        }
        else
        {
            gridLayout.constraintCount = 3;
        }

        foreach (var item in UIManager.Ins.figureData.figures)
        {
            GameObject newButton = Instantiate(figureButton, figureContent);
            FigureItem figureItem = newButton.GetComponent<FigureItem>();

            // if (!item.isBuy && item.costType == CostType.KillEnemy && item.number <= UIManager.Ins.NumberKilled)
            // {
            //     item.isBuy = true;
            // }

            // if (!item.isBuy && item.costType == CostType.Top && item.number <= UIManager.Ins.NumberTop)
            // {
            //     item.isBuy = true;
            // }

            // if (!item.isBuy && item.costType == CostType.Run && item.number <= UIManager.Ins.PlayerMoveDistance)
            // {
            //     item.isBuy = true;
            // }

            // if (!item.isBuy && item.costType == CostType.Video)
            // {
            //     if (UIManager.Ins.VideoCount >= item.number)
            //         item.isBuy = true;
            //     else
            //     {
            //         figureItem.videoTxt.text = UIManager.Ins.VideoCount + "/" + item.number;
            //         videoTxt.text = UIManager.Ins.VideoCount + "/" + item.number;
            //     }
            // }

            if (!item.isBuy)
            {
                switch (item.costType)
                {
                    case CostType.KillEnemy:
                        if (item.number <= UIManager.Ins.NumberKilled)
                            item.isBuy = true;
                        break;

                    case CostType.Top:
                        if (item.number <= UIManager.Ins.NumberTop)
                            item.isBuy = true;
                        break;

                    case CostType.Run:
                        if (item.number <= UIManager.Ins.PlayerMoveDistance)
                            item.isBuy = true;
                        break;

                    case CostType.Video:
                        if (UIManager.Ins.VideoCount >= item.number)
                            item.isBuy = true;
                        else
                        {
                            string numberVideoTxt = UIManager.Ins.VideoCount + "/" + item.number;
                            figureItem.videoTxt.text = numberVideoTxt;
                        }
                        break;

                }
            }

            if (item.idFigure == UIManager.Ins.PlayerFigure)
            {
                item.isUsing = true;
                selectedFigureItem = currentFigureItem = figureItem;
                currentFigureItem.SetActiveBox();
            }
            else if (item.isUsing)
            {
                item.isUsing = false;
            }
            figureItem.SetUp(item);

            newButton.GetComponent<Button>().onClick.AddListener(() => OnSelectItem(figureItem, item));
        }
    }

    private void OnSelectItem(FigureItem figureItem, InfoFigure info)
    {
        // AudioManager.PlaySound(AudioManager.selectFigureName);
        AudioManager1.Ins.PlaySound(SoundType.Click);

        // if (missionInfo.gameObject.activeSelf)
        // {
        //     missionInfo.Hide();
        // }

        if (currentFigureItem != previousFigureItem || figureItem != previousFigureItem)
        {
            previousFigureItem = currentFigureItem;
            currentFigureItem = figureItem;

            previousFigureItem.SetActiveBox();
            currentFigureItem.SetActiveBox();

            currentInfoData = info;
        }

        if (!info.isBuy && info.mission != "")
        {
            missionInfo.GetComponentInChildren<Text>().text = info.mission;
            missionInfo.Show();
        }

        if (info.isBuy)
        {
            HideAllButton();
            listButton[0].Show();
        }
        else if (info.costType == CostType.Coin)
        {
            HideAllButton();
            costButtonTxt.text = info.cost.ToString();
            listButton[1].Show();
        }
        else if (info.costType == CostType.Video)
        {
            HideAllButton();
            videoTxt.text = figureItem.videoTxt.text;
            listButton[2].Show();
        }
        else
        {
            HideAllButton();
            // listButton[3].Show();
        }

        if (info.isUsing)
        {
            HideAllButton();
            // listButton[3].Show();
        }
    }

    void HideAllButton()
    {
        foreach (var item in listButton)
        {
            item.Hide();
        }
    }

    public void HideMissionInfo()
    {
        missionInfo.Hide();
    }

    public void ChangeCurrentFigure(int idFigure)
    {
        _currentFigure = idFigure;
    }

    public void SelectFigure()
    {
        // AudioManager.PlaySound(AudioManager.buttonName);
        AudioManager1.Ins.PlaySound(SoundType.Select);

//        // get info of previous figure selected
//        InfoFigure previousInfo = UIManager.Ins.figureData.figures[UIManager.Ins.PlayerFigure];
//        UIManager.Ins.ChangeFigureData(currentInfoData.idFigure);
//
//        // get info of current figure selected
//        InfoFigure currentInfo = UIManager.Ins.figureData.figures[UIManager.Ins.PlayerFigure];
//
//        // setup itemFigure
//        selectedFigureItem.SetUp(previousInfo);
//        currentFigureItem.SetUp(currentInfo);
//
//        selectedFigureItem = currentFigureItem;

//        UIManager.Ins.home.LoadAsset(currentInfo);

        UIManager.Ins.PlayerFigure = _currentFigure;
        UIManager.Ins.home.LoadFigure();
//        HideAllButton();
    }

    public void BuyFigure()
    {
        // AudioManager.PlaySound(AudioManager.buyButtonName);
        AudioManager1.Ins.PlaySound(SoundType.UnlockFigure);

        if (UIManager.Ins.UpdateCoins(-currentInfoData.cost))
        {
            UIManager.Ins.ChangeFigureData(currentInfoData.idFigure, true);
            currentFigureItem.SetUp(currentInfoData);
            HideAllButton();
            listButton[0].Show();
        }
        else
        {
            Debug.Log("Not enought Money");
        }
    }

    public void BuyPackVip()
    {
        ManagerAds.Ins.RemoveAds();

        foreach (var item in UIManager.Ins.figureData.figures)
        {
            if (item.costType == CostType.Vip)
            {
                item.isBuy = true;
                UIManager.Ins.IsBuyPackVip = true;
                UIManager.Ins.UpdatePlayerMasks(item.idFigure);
            }
        }
        GenerateBuySkin();
    }

    public void BuyPackAll()
    {
        foreach (var item in UIManager.Ins.figureData.figures)
        {
            if (item.costType == CostType.Coin)
            {
                item.isBuy = true;
                UIManager.Ins.UpdatePlayerMasks(item.idFigure);
            }
        }
        GenerateBuySkin();
    }

    void ClearListSkin()
    {
        for (int i = 0; i < figureContent.childCount; i++)
        {
            Destroy(figureContent.GetChild(i).gameObject);
        }
    }
}
