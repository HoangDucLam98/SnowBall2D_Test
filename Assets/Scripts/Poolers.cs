using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Poolers : MonoBehaviour
{
    public static Poolers Ins;
    public Dictionary<GameObject, List<PoolItem>> pool;

    private void Awake()
    {
        Ins = this;
        pool = new Dictionary<GameObject, List<PoolItem>>();
    }

    public PoolItem GetObject(GameObject obj)
    {
        if (pool.ContainsKey(obj))
        {
            for (int i = 0; i < pool[obj].Count; i++)
            {
                if (!pool[obj][i].gameObject.activeInHierarchy)
                {
                    pool[obj][i].transform.position = Vector3.zero;
                    pool[obj][i].transform.rotation = Quaternion.Euler(0, 0, 0);
                    pool[obj][i].gameObject.SetActive(true);
                    pool[obj][i].transform.parent = transform;
                    return pool[obj][i];
                }
            }

            var o = Instantiate(obj, Vector3.zero, Quaternion.Euler(0, 0, 0));
            var b = o.GetComponent<PoolItem>();
            pool[obj].Add(b);
            return b;
        }
        else
        {
            pool.Add(obj, new List<PoolItem>());
            var o = Instantiate(obj, Vector3.zero, Quaternion.Euler(0, 0, 0));
            var script = o.GetComponent<PoolItem>();
            pool[obj].Add(script);
            return script;
        }
    }

    public PoolItem GetObject(GameObject obj, Vector3 position, Quaternion rotation)
    {
        if (pool.ContainsKey(obj))
        {
            for (int i = 0; i < pool[obj].Count; i++)
            {
                if (!pool[obj][i].gameObject.activeInHierarchy)
                {
                    pool[obj][i].transform.position = position;
                    pool[obj][i].transform.rotation = rotation;
                    pool[obj][i].gameObject.SetActive(true);
                    return pool[obj][i];
                }
            }

            var o = Instantiate(obj, position, rotation);
            var b = o.GetComponent<PoolItem>();
            pool[obj].Add(b);
            return b;
        }
        else
        {
            pool.Add(obj, new List<PoolItem>());
            var o = Instantiate(obj, position, rotation);
            var script = o.GetComponent<PoolItem>();
            pool[obj].Add(script);
            return script;
        }

    }

    public PoolItem GetObject(GameObject obj, Transform parent)
    {
        if (pool.ContainsKey(obj))
        {
            for (int i = 0; i < pool[obj].Count; i++)
            {
                if (!pool[obj][i].gameObject.activeInHierarchy)
                {
                    pool[obj][i].transform.SetParent(parent);
                    pool[obj][i].gameObject.SetActive(true);
                    return pool[obj][i];
                }
            }

            var o = Instantiate(obj, parent);
            var b = o.GetComponent<PoolItem>();
            pool[obj].Add(b);
            return b;
        }
        else
        {
            pool.Add(obj, new List<PoolItem>());
            var o = Instantiate(obj, parent);
            var script = o.GetComponent<PoolItem>();
            pool[obj].Add(script);
            return script;
        }
    }

    public void ClearObject(GameObject obj)
    {
        if (pool.ContainsKey(obj))
        {
            for (int i = 0; i < pool[obj].Count; i++)
            {
                pool[obj][i].Hide();
            }
        }
    }

    public void ClearAll()
    {
        foreach (var key in pool.Keys)
        {
            for (int i = 0; i < pool[key].Count; i++)
            {
                pool[key][i].Hide();
            }
        }
    }
}

public static class GameObjectExtension
{
    public static void Show(this MonoBehaviour m)
    {
        m.gameObject.SetActive(true);
    }

    public static void Hide(this MonoBehaviour m)
    {
        m.gameObject.SetActive(false);
    }

    public static void Show(this GameObject obj)
    {
        obj.SetActive(true);
    }

    public static void Hide(this GameObject obj)
    {
        obj.SetActive(false);
    }

    public static string GetFloatWithOneBehind(this float obj)
    {
        return obj.ToString("F1");
    }

    public static T Cast<T>(this MonoBehaviour m) where T : class
    {
        return m as T;
    }
}