using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    [SerializeField] private List<ObjectControlPool> controlPool = new List<ObjectControlPool>();

    private List<GameObject> pooledObjects;

    private new void Awake()
    {
        base.Awake();
    }

    private void Start() 
    {
        pooledObjects = new List<GameObject>();
        foreach(ObjectControlPool p in controlPool)
        {
            for(int i = 0; i < p.amount; i++)
            {
                GameObject obj = Instantiate(p.p_object, this.transform.position, Quaternion.identity, this.transform);
                pooledObjects.Add(obj);
                obj.SetActive(false);
            }
        }
    }

    public GameObject GetObjectFromPool(string tag)
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag) return pooledObjects[i];
        }

         foreach(ObjectControlPool p in controlPool)
        {
            if(p.p_object.tag == tag)
            {
                GameObject obj = Instantiate(p.p_object, this.transform.position, Quaternion.identity, this.transform);
                pooledObjects.Add(obj);
                obj.SetActive(false);
                return obj;
            }
        }
        return null;
    }

    public GameObject GetActiveObject(string tag)
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if(pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag) return pooledObjects[i];
        }
        return null;
    }
}

[System.Serializable]
public class ObjectControlPool
{
    public string name;
    public int amount;
    public GameObject p_object;
}