using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEffect : MonoBehaviour
{
    public Vector3 Offset = new Vector3(0.1f, -0.1f);
    public Material material;
    Vector3 sun = new Vector3(-100, 100);

    GameObject _shadow;
    // Start is called before the first frame update
    void Start()
    {
        _shadow = new GameObject("Shadow");
        _shadow.transform.parent = transform;

        _shadow.transform.localPosition = Offset;
        _shadow.transform.localRotation = Quaternion.identity;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        SpriteRenderer sr = _shadow.AddComponent<SpriteRenderer>();
        sr.sprite = renderer.sprite;
        sr.material = material;
        sr.color = Color.black;

        sr.sortingLayerName = renderer.sortingLayerName;
        sr.sortingOrder = renderer.sortingOrder - 1;

        _2dxFX_Blur blur = _shadow.AddComponent<_2dxFX_Blur>();
        blur.Blur = 10;
        blur._Alpha = 0.2f;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 offset = transform.position - sun;
        //Debug.Log(offset.normalized);
        
        offset.Normalize();
        offset.x -= 0.5f;
        offset.y += 0.5f;
        _shadow.transform.position = transform.position + offset;
    }
}
