using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TestSmoothing : MonoBehaviour
{
    // public Transform t;
    // public float size, time, angle;

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         StartCoroutine(IScale(size, time));
    //     }

    //     if (Input.GetKeyDown(KeyCode.S))
    //     {
    //         StartCoroutine(IRotate(angle, time));
    //     }
    // }

    // IEnumerator IScale(float size, float time)
    // {
    //     float i = 0;
    //     t.localScale = Vector2.one * 0.2f;
    //     while (i < 1)
    //     {
    //         i += Time.deltaTime / time;

    //         var f = Vector2.Lerp(t.localScale, Vector2.one * size, i);
    //         t.localScale = f;
    //         yield return null;
    //     }
    // }

    // IEnumerator IRotate(float angle, float time)
    // {
    //     float i = 0;
    //     t.eulerAngles = Vector3.zero;
    //     while (i < 1)
    //     {
    //         i += Time.deltaTime / time;

    //         var f = Mathf.Lerp(t.eulerAngles.z, angle, i);
    //         t.eulerAngles = new Vector3(0, 0, f);
    //         yield return null;
    //     }
    // }

    public SkeletonAnimation skele;

    [SpineAnimation]
    public string a1 = "idle";
    [SpineAnimation]
    public string a2 = "stun";

    public SkeletonDataAsset pika, monster;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            skele.state.ClearTracks();
            skele.skeleton.SetSlotsToSetupPose();
            skele.state.SetAnimation(0, a1, true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            skele.state.ClearTracks();
            skele.state.SetAnimation(0, a2, true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            skele.skeletonDataAsset = monster;
            skele.Initialize(true);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            skele.skeletonDataAsset = pika;
            skele.Initialize(true);
        }
    }

}
