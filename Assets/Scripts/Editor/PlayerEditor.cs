using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
public class PlayerEditor : EditorWindow
{
    public int Size = 50;
    public List<CountryBotName> CountryBotNames = new List<CountryBotName>();
    public CountryBotName curPlayer;
    public int curPlayerIndex = 0;
    public int PlayerIndex = 0;
    public Vector2 ScrollPosition;

    [MenuItem("GameData/CountryBotName")]

    public static void Init()
    {
        PlayerEditor brobotEditor = GetWindow<PlayerEditor>();
        brobotEditor.Show();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Refresh"))
        {
            curPlayer = new CountryBotName();
            Refresh();

            if (CountryBotNames.Count != 0)
            {
                curPlayer = CountryBotNames[0];
            }
        }

        // if (GUILayout.Button("Add Brobot"))
        // {
        //     BrobotData bd = GameHelper.GetAllAssetAtPath<BrobotData>(string.Empty, "Assets/Game/Resources/Brobots")[0];
        //     bd.brobotsInfo.Add(new BrobotInfo());
        //     AssetDatabase.Refresh();
        //     Refresh();
        //     Brobots[Brobots.Count - 1].name = BrobotName.BR1;
        //     EditorUtility.SetDirty(bd);
        //     AssetDatabase.SaveAssets();
        // }

        GUILayout.EndHorizontal();

        if (CountryBotNames.Count <= 0)
            return;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Back"))
        {
            if (PlayerIndex > 0)
            {
                PlayerIndex--;
            }

            curPlayerIndex = PlayerIndex;
            curPlayer = CountryBotNames[curPlayerIndex];
        }

        PlayerIndex = EditorGUILayout.Popup(curPlayerIndex, CountryBotNames.Select(s => s.countryName.ToString()).ToArray());

        if (GUILayout.Button("Next"))
        {
            if (PlayerIndex < CountryBotNames.Count - 1)
                PlayerIndex++;
            curPlayerIndex = PlayerIndex;
            curPlayer = CountryBotNames[curPlayerIndex];
        }

        GUILayout.EndHorizontal();

        // if (BrobotIndex != curBrobotIndex)
        // {
        //     curBrobotIndex = BrobotIndex;
        //     curBrobot = Brobots[curBrobotIndex];
        // }


        // if (curBrobot != null)
        // {
        //     DisplayData();
        // }

        // GUILayout.BeginHorizontal();

        //        if (GUILayout.Button("Reset"))
        //        {
        //            foreach (var item in Brobots)
        //            {
        //                item.ResetData();
        //            }
        //            Refresh();
        //        }

        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(GameHelper.GetAllAssetAtPath<BotNameData>(string.Empty, "Assets/Game/Resources/BotNameData")[0]);
            AssetDatabase.SaveAssets();
        }

        GUILayout.EndHorizontal();
    }

    private void DisplayData()
    {
        // ScrollPosition = GUILayout.BeginScrollView(ScrollPosition);
        // GUILayout.Label("Base:");
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Armor: ", GUILayout.Width(70));
        // curBrobot.baseArmor = EditorGUILayout.IntField(curBrobot.baseArmor, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Power: ", GUILayout.Width(70));
        // curBrobot.basePower = EditorGUILayout.IntField(curBrobot.basePower, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Spec.Pow: ", GUILayout.Width(70));
        // curBrobot.baseSpecPower = EditorGUILayout.IntField(curBrobot.baseSpecPower, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.Label("Up To Rank B:");
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Armor: ", GUILayout.Width(70));
        // curBrobot.armorUpRankB= EditorGUILayout.IntField(curBrobot.armorUpRankB, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Power: ", GUILayout.Width(70));
        // curBrobot.powerUpRankB = EditorGUILayout.IntField(curBrobot.powerUpRankB, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Spec.Pow: ", GUILayout.Width(70));
        // curBrobot.specPowUpRankB = EditorGUILayout.IntField(curBrobot.specPowUpRankB, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.Label("Up To Rank A:");
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Armor: ", GUILayout.Width(70));
        // curBrobot.armorUpRankA = EditorGUILayout.IntField(curBrobot.armorUpRankA, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Power: ", GUILayout.Width(70));
        // curBrobot.powerUpRankA = EditorGUILayout.IntField(curBrobot.powerUpRankA, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Spec.Pow: ", GUILayout.Width(70));
        // curBrobot.specPowUpRankA = EditorGUILayout.IntField(curBrobot.specPowUpRankA, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.Label("Up To Rank S:");
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Armor: ", GUILayout.Width(70));
        // curBrobot.armorUpRankS = EditorGUILayout.IntField(curBrobot.armorUpRankS, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Power: ", GUILayout.Width(70));
        // curBrobot.powerUpRankS = EditorGUILayout.IntField(curBrobot.powerUpRankS, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Spec.Pow: ", GUILayout.Width(70));
        // curBrobot.specPowUpRankS = EditorGUILayout.IntField(curBrobot.specPowUpRankS, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Skill C: ", GUILayout.Width(40));
        // curBrobot.skillC = (SkillName)EditorGUILayout.EnumPopup(curBrobot.skillC, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Skill B: ", GUILayout.Width(40));
        // curBrobot.skillA = (SkillName)EditorGUILayout.EnumPopup(curBrobot.skillA, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Skill S: ", GUILayout.Width(40));
        // curBrobot.skillS = (SkillName)EditorGUILayout.EnumPopup(curBrobot.skillS, GUILayout.Width(70));
        // GUILayout.EndHorizontal();
        // //if (CurPlane.canDamage = GUILayout.Toggle(CurPlane.canDamage, "Can Damage: "))
        // //{
        // //    CurPlane.bulletType = (BulletType)EditorGUILayout.EnumPopup("Bullet Type: ", CurPlane.bulletType);
        // //    CurPlane.electricType = (ElectricType)EditorGUILayout.EnumPopup("Electric Type: ", CurPlane.electricType);
        // //    CurPlane.flameType = (FlameType)EditorGUILayout.EnumPopup("Flame Type: ", CurPlane.flameType);
        // //    CurPlane.lazerType = (LazerType)EditorGUILayout.EnumPopup("Lazer Type: ", CurPlane.lazerType);
        // //}

        // GUILayout.EndScrollView();
    }

    private void Refresh()
    {
        //  Lay danh sach cac Enemys
        BotNameData bd = GameHelper.GetAllAssetAtPath<BotNameData>(string.Empty, "Assets/Game/Resources/BotNameData")[0];
        CountryBotNames = bd.botNames;
    }
}
