using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;

    public static ObjectPooler Instance;

    private void Awake() {
        Instance = this;    
    }
    void Start()
    {
        
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        for(int i = 0; i < pools.Count; i++)
        {
            Queue<GameObject> newPool = new Queue<GameObject>();

            for(int j = 0; j < pools[i].poolSize; j++)
            {
                GameObject newObject = Instantiate(pools[i].prefab);
                newObject.SetActive(false);

                IPoolItem poolItem = newObject.GetComponent<IPoolItem>();
                if(poolItem != null)
                    poolItem.OnStart();
                    
                newPool.Enqueue(newObject);

            }

            poolDictionary.Add(pools[i].tag, newPool);
        }

    }

    public GameObject SpawnObject(string tag)
    {
        if(poolDictionary.ContainsKey(tag))
        {
            GameObject returningObject = poolDictionary[tag].Dequeue();
            returningObject.SetActive(true);

            poolDictionary[tag].Enqueue(returningObject);

            return returningObject;
        }
        
        return null;
    }
}
