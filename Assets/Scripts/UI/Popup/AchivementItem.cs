using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementItem : MonoBehaviour
{
    public Text achivementName, achivement;

    public void SetUpAchivement(string achivementName, string achivement)
    {
        this.achivementName.text = achivementName;
        this.achivement.text = achivement;
    }
}
