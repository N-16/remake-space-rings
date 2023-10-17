using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourcePool : MonoBehaviour
{
    private List<GameObject> sources = new List<GameObject>();
    [SerializeField] private GameObject sourcePrefab;
    [SerializeField] private int poolSize;


    void Awake()
    {
        if (sourcePrefab == null)
            Debug.LogError("Null Prefab");
        for (int i = 0; i < poolSize; i++)
        {
            GameObject temp = Instantiate(sourcePrefab, this.transform);
            temp.SetActive(false);
            sources.Add(temp);
        }   
    }

    public GameObject SpawnSource(Vector3 pos)
    {
        foreach (GameObject source in sources)
        {
            if (!source.activeSelf)
            {
                source.SetActive(true);
                source.transform.position = pos;
                return source;
            }
        }
        GameObject temp = Instantiate(sourcePrefab, this.transform);
        sources.Add(temp);
        temp.SetActive(true);
        temp.transform.position = pos;
        return temp;
    }

}
