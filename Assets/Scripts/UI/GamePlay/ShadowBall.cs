using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBall : MonoBehaviour
{
    public GameObject target;
    Vector3 sun = new Vector3(-100, 100);

    // Update is called once per frame
    void LateUpdate()
    {
        //float size = target.transform.localScale.x;
        //transform.localScale = new Vector3(size, size, 1);

        Vector3 offset = target.transform.position - sun;
        //Debug.Log(offset.normalized);

        offset.Normalize();
        offset.x -= 0.5f;
        offset.y += 0.5f;
        transform.position = target.transform.position + offset;
    }
}
