using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Spine.Unity;

[CustomEditor(typeof(FigureData))]
public class FigureInspector : Editor
{
    public Sprite[] sprites;
    private FigureData data;
    private string ID = "1Gxb_16XAGoxixfQt4G_AoF73prH8yXPGFqgrx-_kohA";
    public string sheetName = "Snow plow";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        data = (FigureData)target;

        GUILayout.Label("ID GG Shet");
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        ID = EditorGUILayout.TextField("ID: ", ID);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        sheetName = EditorGUILayout.TextField("SheetName: ", sheetName);

        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Retrieving"))
        {
            RetrievingData();
        }

        GUILayout.EndHorizontal();
    }

    public void RetrievingData()
    {
        // var path = "Assets/Skin";
        // Object[] data = Resources.LoadAll(path, typeof(Sprite));
        // Debug.Log(data.Length);
        // foreach (var item in data)
        // {
        //     Debug.Log(item);
        // }
        var sp = GameHelper.GetAllAssetAtPath<Sprite>("", "Assets/Skin");
        ReadExcelOnline.Connect();
        ReadExcelOnline.GetTable(ID, sheetName, d =>
        {
            // foreach (var key in d.Keys)
            // {
            //     var rows = d[key];
            //     foreach (var item in rows.Keys)
            //     {
            //         Debug.Log(rows[item] + "  " + key + "  " + item);
            //     }
            // }

            if (data.figures.Count > 0)
            {
                int c = 0;

                foreach (var key in d.Keys)
                {
                    if (c < data.figures.Count)
                    {

                        if (key < 1)
                            continue;

                        var rows = d[key];
                        var temp = new InfoFigure();
                        temp.heroName = rows[1];
                        temp.figureSprite = sp.Find(n => n.name.ToLower().Contains(temp.heroName.ToLower()));

                        var dataSke = GameHelper.GetAllAssetAtPath<SkeletonDataAsset>(null, "Assets/skin-animation/" + temp.heroName + "-ani");
                        temp.asset = dataSke[5];

                        temp.idFigure = int.Parse(rows[0]) - 1;
                        var s = rows[2];
                        if (s.Contains("enemy"))
                        {
                            temp.costType = CostType.KillEnemy;
                            temp.mission = s;

                            // find and convert
                            string numericString = "";
                            foreach (char ch in s)
                            {
                                // Check for numeric characters (0-9), a negative sign, or leading or trailing spaces.
                                if ((ch >= '0' && ch <= '9') || ch == ' ' || ch == '-')
                                {
                                    numericString = string.Concat(numericString, ch);
                                }
                            }
                            if (int.TryParse(numericString, out int j))
                                temp.number = j;

                        }
                        else if (s.Contains("videos"))
                        {
                            temp.costType = CostType.Video;

                            // find and convert
                            string numericString = "";
                            foreach (char ch in s)
                            {
                                // Check for numeric characters (0-9), a negative sign, or leading or trailing spaces.
                                if ((ch >= '0' && ch <= '9') || ch == ' ' || ch == '-')
                                {
                                    numericString = string.Concat(numericString, ch);
                                }
                            }
                            if (int.TryParse(numericString, out int j))
                                temp.number = j;
                        }
                        else if (s.Contains("Tournament"))
                        {
                            temp.costType = CostType.Tournament;
                            temp.mission = "Phần thưởng vô địch Tournament";
                        }
                        else if (s.Contains("Rate"))
                        {
                            temp.costType = CostType.Rate;
                            temp.mission = s;
                        }
                        else if (s.Contains("Free"))
                        {
                            temp.costType = CostType.Free;
                        }
                        else if (s.Contains("ngày"))
                        {
                            temp.costType = CostType.Day;
                            temp.mission = s;
                        }
                        else if (s.Contains("Top"))
                        {
                            temp.costType = CostType.Top;
                            temp.mission = s;

                            // find and convert
                            string numericString = "";
                            foreach (char ch in s)
                            {
                                // Check for numeric characters (0-9), a negative sign, or leading or trailing spaces.
                                if ((ch > '1' && ch <= '9') || ch == ' ' || ch == '-')
                                {
                                    numericString = string.Concat(numericString, ch);
                                }
                            }
                            if (int.TryParse(numericString, out int j))
                                temp.number = j;
                        }
                        else if (s.Contains("Chạy"))
                        {
                            temp.costType = CostType.Run;
                            temp.mission = s;

                            // find and convert
                            string numericString = "";
                            foreach (char ch in s)
                            {
                                // Check for numeric characters (0-9), a negative sign, or leading or trailing spaces.
                                if ((ch >= '0' && ch <= '9') || ch == ' ' || ch == '-')
                                {
                                    numericString = string.Concat(numericString, ch);
                                }
                            }
                            if (int.TryParse(numericString, out int j))
                                temp.number = j;
                        }
                        else if (s.Contains("VIP"))
                        {
                            temp.costType = CostType.Vip;
                            temp.mission = s;
                        }
                        else
                        {
                            temp.costType = CostType.Coin;
                        }

                        if (temp.costType == CostType.Coin)
                            temp.cost = int.Parse(s);

                        data.figures[c] = temp;
                        c++;
                    }

                }

            }
        });

        EditorUtility.SetDirty(data);
        AssetDatabase.SaveAssets();
    }


}

