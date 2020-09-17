using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameController : MonoBehaviour
{
    public GameObject target;
    // public TextMesh textMesh;
    public Image flagImg;
    // public static NameController Ins;
    // public BotNameData data;
    // int index;

    public TextMeshProUGUI textMeshPro;
    public Text textName;

    // private void Awake()
    // {
    //     Ins = this;
    //     // GetComponent<TextMesh>().text = GetRandomName();
    //     textMeshPro.text = GetRandomName();
    //     flagImg.GetComponent<Image>().sprite = GetIcon();
    // }

    // private void Start()
    // {
    //     // // GetComponent<TextMesh>().text = GetRandomName();
    //     // textMeshPro.text = GetRandomName();
    //     // flagImg.GetComponent<Image>().sprite = GetIcon();
    // }

    // public string GetName()
    // {
    //     return textMeshPro.text;
    // }

    private void Update()
    {
        if (target != null)
        {
            Followtarget();
        }
        else
        {
            Destroy();
        }
    }

    void Followtarget()
    {
        // transform.position = new Vector3(target.transform.position.x, target.transform.position.y - target.transform.GetComponent<BallController>().faceObject.GetComponent<BoxCollider2D>().bounds.extents.y / 2 - .2f, 0);
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y - target.transform.GetComponent<BallController>().faceObject.localScale.y * 0.5f - 0.1f, 0);
    }

    // public string GetRandomName()
    // {
    //     index = Random.Range(0, data.botNames.Count);
    //     var m = Random.Range(0, data.botNames[index].botName.Count);
    //     return data.botNames[index].botName[m];
    // }

    // public Sprite GetIcon()
    // {
    //     return data.botNames[index].icon;
    // }

    public void OnSpawn(InfoData info)
    {
        textName.text = info.itemName;
        flagImg.sprite = info.flagSprite;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
