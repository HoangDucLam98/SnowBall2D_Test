using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[CreateAssetMenu(menuName = "Data/FigureSprite")]
public class FigureData : ScriptableObject
{
    public List<InfoFigure> figures;
}

[System.Serializable]
public class InfoFigure
{
    public int id;
    public Sprite figureSprite;
    public int idFigure;
    public bool isUsing = false;
    public bool isBuy = false;

    public SkeletonDataAsset asset;

    public string heroName;
    public CostType costType;
    public int cost;
    public int number;
    public string mission;
}

public enum CostType
{
    Coin, KillEnemy, Video, Tournament, Rate, Free, Day, Vip, Run, Top
}