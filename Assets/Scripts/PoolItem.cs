using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    public Transform m_transform;
    public GameObject obj;

    void Reset()
    {
        m_transform = transform;
        obj = gameObject;
    }

    [ContextMenu("Add PoolItem")]
    void Add()
    {
        m_transform = transform;
        obj = gameObject;
    }

    public virtual void OnSpawn()
    {

    }

    public virtual void OnDespawn()
    {

    }
}