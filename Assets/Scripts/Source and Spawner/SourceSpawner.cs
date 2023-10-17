using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceSpawner : MonoBehaviour
{
    [SerializeField] private float seperation = 100f;//seperation betwn two sources
    [SerializeField] private float startinPoint = 0f;
    [SerializeField] private int maxPatternOffset = 35;//max diff in xy direction in a pattern
    //pattern:group of some sources(length) having same xy displacement at same direction 
    [SerializeField] private int minPatternOffset = 25;
    [SerializeField] private int maxPatternLength = 10;
    [SerializeField] private int minPatternLenght = 4;
    [SerializeField] private int noOfSpawned = 12;//spawned
    private Transform ship;//ship transform
    [SerializeField] private SourcePool sourcePool;

    private Pattern currentPattern;//current pattern(structure see at the end)
    private Vector3 lastSourcePos;
    private int patternSpawnIndex = 0;
    private float threshold = 0f;//do stuff after crossin threshold

    //[SerializeField] private Traffic[] traffic;
    [SerializeField] public float sourceRadius;

    private int patternIndex = 0;//Large Noise after Every 3 pattern
    [SerializeField] private GameObject sourceParent;
    private bool spawnTraffic = false;
    private int restIndex = 2;
    private Queue<GameObject> sourceQueue = new Queue<GameObject>();


    void Start()
    {   
        ship = GameObject.FindWithTag("Player").transform;
        currentPattern = PatternPlan(true, noOfSpawned);
        for(int i = 0;i < currentPattern.length; i++){
            PatternSpawnin(false);
        }
    }
    void Update()
    {
        if (patternSpawnIndex == currentPattern.length)//cr8 nxt pattern after all spawned of the pattern
        {
            patternSpawnIndex = 0;
            if (patternIndex == restIndex){
                currentPattern = PatternPlan(true);
                patternIndex = 0;
            }
            else{
                patternIndex += 1;
                currentPattern = PatternPlan();
            }
        }
        if (ship.position.z > sourceQueue.Peek().transform.position.z)//do stuff.......
        {
            PatternSpawnin();
            //threshold += seperation;//increase threshold
        }
 
    }

    void Spawn(Vector3 pos, bool dequeue = true)//beacause it looks cleaner
    {
        //var source = Instantiate(prefab, new Vector3(x, y, z), Quaternion.Euler(90f, 0f, 0f));
        //source.transform.parent = sourceParent.transform;
        if (dequeue)
            StartCoroutine(DisableRoutine(0.5f, sourceQueue.Dequeue()));
        sourceQueue.Enqueue(sourcePool.SpawnSource(pos));
        //Testing queue implementation
        /*float lastZ = sourceQueue.Peek().transform.position.z;
        foreach(GameObject src in sourceQueue){
            if (lastZ > src.transform.position.z){
                Debug.LogError("Queue Failed :(");
            }
            lastZ = src.transform.position.z;
        }*/
    }

    //spawning acc to current pattern
    void PatternSpawnin(bool dequeue = true){
        Vector3 spawnPos = new Vector3(lastSourcePos.x + currentPattern.xOffset, lastSourcePos.y + currentPattern.yOffset, lastSourcePos.z + seperation);
        Spawn(spawnPos, dequeue);
        lastSourcePos = spawnPos;
        patternSpawnIndex += 1;
        /*if (spawnTraffic)
        SpawnTraffic(spawnX, spawnY, spawnZ);*/
    }

    Pattern PatternPlan(bool restPattern = false, int length = 0)//creatin pattern
    {
        Pattern p = new Pattern();
        if (restPattern)
        {
            p.xOffset = 0f;
            p.yOffset = 0f;
            p.length = length !=0 ? length : Random.Range(minPatternLenght, maxPatternLength);
            ChangeRestIndex();
            Debug.Log("rest");
            return p;
        }
        int signX = Random.Range(1, 3) == 1 ? -1 : 1;//left or right????
        int signY = Random.Range(1, 3) == 1 ? -1 : 1;//up or down???????
        int noise = Random.Range(minPatternOffset, maxPatternOffset);
        Debug.Log(noise);
        p.xOffset = signX * Mathf.Sqrt(Random.Range(0, noise * noise));
        p.yOffset = signY * Mathf.Sqrt(noise * noise - p.xOffset * p.xOffset);
        p.length = length !=0 ? length : Random.Range(minPatternLenght, maxPatternLength);//randomly
        return p;
    }
    /*void SpawnTraffic(float centreX, float centreY, float centreZ)
    {
        foreach (Traffic vehicle in traffic)
        {
            if (Random.Range(0, vehicle.spawnProb) == 0)
            {
                float r = Random.Range(0, sourceRadius - 2);
                float x = Random.Range(-r, r);
                int signY = Random.Range(1, 3) == 2 ? 1 : -1;
                float y = signY * Mathf.Sqrt(r * r - x * x);
                vehicle.Spawn(centreX + x, centreY + y, centreZ, new Vector3(90f, 0f, 0f));
                break;
            }
        }
        
    }*/

    void ChangeRestIndex()
    {
        if (Random.Range(1, 26) == 2)
        {
            restIndex = Random.Range(13, 17);
        }
        else
            restIndex = Random.Range(2, 5);
    
    }
    IEnumerator DisableRoutine(float time, GameObject objToDisable){
        float startTime = Time.time;
        while(Time.time - startTime < time){
            yield return null;
        }
        objToDisable.SetActive(false);
    } 
    public Queue<GameObject> GetQueue(){
        return sourceQueue;
    }
    
}

public struct Pattern
{
    public int length;//how many sources
    public float xOffset;//xOffset
    public float yOffset;//yOffset
}
