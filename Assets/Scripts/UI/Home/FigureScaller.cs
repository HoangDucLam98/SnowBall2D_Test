using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureScaller : MonoBehaviour
{
    void Start()
    {
        float size = (float)Screen.width / Screen.height;
        transform.localScale = new Vector3(size, size, 0);
    }
}
