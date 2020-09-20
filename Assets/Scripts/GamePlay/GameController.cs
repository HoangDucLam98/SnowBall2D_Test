using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public List<GameObject> listObject = new List<GameObject>();
    public List<BallController> listBallController = new List<BallController>();
    //public List<GameObject> listObjectDestroyed = new List<GameObject>();
    public Sprite[] sprites;
    public List<InfoData> characters;
    private List<InfoData> characterDeaths;
    public BotNameData data;
    public FigureData figureData;

    public int idFlag;
    //public int idPlayer;
    //Player player;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //Application.targetFrameRate = 60;
        idFlag = data.botNames.FindIndex(s => s.countryCode == UIManager.Ins.PlayerCountry);
        characterDeaths = new List<InfoData>();
    }

    public void Clear()
    {
        listObject.Clear();
        listBallController.Clear();
        //listObjectDestroyed.Clear();
    }

    public void SetUpData()
    {
        characters.Clear();
        characterDeaths.Clear();
        for (int i = 0; i < 12; i++)
        {
            var t = Random.Range(0, data.botNames.Count);
            var m = Random.Range(0, data.botNames[t].botName.Count);

            InfoData info = new InfoData();
            info.id = i;
            info.itemName = data.botNames[t].botName[m];
            info.flagSprite = data.botNames[t].icon;
            int index = Random.Range(30, figureData.figures.Count);
            info.figureSprite = figureData.figures[index].figureSprite;
            info.isDeath = false;
            if (i == 0)
            {
                info.isPLayer = true;
                info.itemName = (UIManager.Ins.PlayerName != "") ? UIManager.Ins.PlayerName : "Player098";
                // int index = data.botNames.FindIndex(s => s.countryCode == UIManager.Ins.PlayerCountry);
                info.flagSprite = data.botNames[idFlag].icon;
                //kiểu ông lưu lại cái idplayer này thì mỗi lần vào game lấy cái idplayer này ra trỏ vào list nhân vật
                info.figureSprite = figureData.figures[UIManager.Ins.PlayerFigure].figureSprite;
                info.dataAsset = figureData.figures[UIManager.Ins.PlayerFigure].asset;
            }
            else
            {
                info.isPLayer = false;
                info.dataAsset = figureData.figures[index].asset;
            }

            characters.Add(info);
        }
    }

    public void DestroyGameObjectInList(GameObject gameObject)
    {
        if (listObject.Contains(gameObject))
        {
            listObject.Remove(gameObject);
            //listObjectDestroyed.Add(gameObject);
        }
    }

    public void AddToCharacterDeaths(int id)
    {
        characterDeaths.Add(characters[id]);
    }

    public List<InfoData> SortListCharacter()
    {
        List<InfoData> newList = new List<InfoData>(characters);
        //foreach (var item in characterDeaths)
        //{
        //    Debug.Log(item.id);
        //    newList.Remove(item);
        //}
        newList.RemoveAt(0);
        var sortList = newList.OrderByDescending(m => m.coins).ToList();

        //for (int i = characterDeaths.Count-1; i >= 0; i--)
        //{
        //    sortList.Add(characterDeaths[i]);
        //}
        sortList.Insert(12 - characterDeaths.Count-1, characters[0]);

        return sortList;
    }

    public int GetIndexOfObject(GameObject gameObject)
    {
        return listObject.IndexOf(gameObject);
    }

    // public void SetObjToListDes(bool check, GameObject gameObject)
    // {
    //     if (check)
    //     {
    //         listObjectDestroyed.Add(gameObject);
    //     }
    //     else
    //     {
    //         listObjectDestroyed.Remove(gameObject);
    //     }
    // }

    // public List<GameObject> ListToFind()
    // {
    //     List<GameObject> newList = listObject;
    //     for (int i = 0; i < listObjectDestroyed.Count; i++)
    //     {
    //         newList.Remove(listObjectDestroyed[i]);
    //     }
    //     return newList;
    // }

    public BallController Killer(int index)
    {
        foreach (BallController item in listBallController)
        {
            if (item.infor.id == index)
            {
                return item;
            }
        }
        return null;
    }

    public void UpdateKiller(int index)
    {
        characters[index].numberKilled++;
        characters[index].coins += 20;

        if (characters[index].isPLayer)
        {
            AudioManager1.Ins.CheckSound(characters[index].numberKilled);
            UIManager.Ins.ChangeNumberKilled();
        }
    }

    public List<InfoData> SortRankCharacters()
    {
        var followCoins = characters.OrderByDescending(m => m.coins).ToList();
        var s = followCoins.OrderBy(m => m.isDeath ? 1 : 0).ToList();

        return s;
    }
}
