using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject pre;

    private void Start()
    {
        StartCoroutine(SpawnByTime());
    }
    void Spawn()
    {
        TestObj obj = (TestObj)Poolers.Ins.GetObject(pre);
        obj.OnSpawn();
    }

    IEnumerator SpawnByTime()
    {
        while (true)
        {
            float rand = Random.Range(0.4f, 0.7f);
            yield return new WaitForSeconds(rand);
            Spawn();
        }
    }
}
