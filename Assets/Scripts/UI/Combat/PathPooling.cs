using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PathPooling : MonoBehaviour{ 
    public static PathPooling sharedInstance;
    public static bool deleteAll;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private void Awake()
    {
        sharedInstance = this;
    }
    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject toPool;
        for(int i = 0; i< amountToPool; i++)
        {
            toPool = Instantiate(objectToPool);
            toPool.SetActive(false);
            pooledObjects.Add(toPool);
        }
    }
    public GameObject GetPooledObject(int j)
    {
        if(j > amountToPool)
        {
            return null;
        }
        if(!pooledObjects[j].activeInHierarchy)
            return pooledObjects[j];
        else
        {
            deleteAll = true;
            for(int i = amountToPool - 1; i > 0; i--)
            {
                if (!pooledObjects[i].activeInHierarchy)
                    return pooledObjects[i];
            }
        }
        return null;
    }
    public void DeleteDots(int num)
    {
        if (deleteAll)
        {
            deleteAll = false;
            for (int i = 0; i < amountToPool; i++)
            {
                pooledObjects[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i <= num; i++)
            {
                pooledObjects[i].SetActive(false);
            }
        }
    }

}
