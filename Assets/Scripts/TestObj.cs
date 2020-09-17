using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObj : PoolItem
{
    public override void OnSpawn()
    {
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        float rand = Random.Range(1f, 4f);
        yield return new WaitForSeconds(rand);
        this.Hide();
    }
}
