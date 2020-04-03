﻿using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PoolConfig", menuName = "PoolConfig")]
public class PoolConfig : ScriptableObject {
    //obj pool
    private Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();

    public void InitPool(string poolName, int size, PoolInflationType type = PoolInflationType.DOUBLE) {
        if (poolDict.ContainsKey(poolName))
                return;
        else {
            GameObject pb = Resources.Load<GameObject>(poolName);
            poolDict[poolName] = new Pool(poolName, pb, size, type);
        }
    }

    // Returns an available object from the pool 
    public GameObject GetObjectFromPool(string poolName) {
        GameObject result = null;

        if (poolDict.ContainsKey(poolName)) {
            Pool pool = poolDict[poolName];
            result = pool.NextAvailableObject(true);
        }

        return result;
    }

    // Return obj to the pool
    public void ReturnObjectToPool(GameObject go) {
        PoolObject po = go.GetComponent<PoolObject>();
        if (po != null) {
            Pool pool = null;
            if (poolDict.TryGetValue(po.poolName, out pool))
                pool.ReturnObjectToPool(po);
        }
    }
}
