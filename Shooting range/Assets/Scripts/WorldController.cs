using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int prefabAmount;
    private GameObject[] allPrefabs;

    public Transform[] spawnPositions;
    public Transform enemyGroup;
    public int enemyQuantity = 10;

    public GameObject bonusObject;

    static public WorldController instance;
    private void Awake()
    {
        if (WorldController.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        WorldController.instance = this;
    }
    private void Start()
    {
        allPrefabs = new GameObject[prefabAmount];
        if (prefabAmount > 0)
            for (int i = 0; i < prefabAmount; i++)
            {
                allPrefabs[i] = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], enemyGroup);
                allPrefabs[i].SetActive(false);
            }
        Initialization();
    }
    public void AddEnemy()
    {
        for (int i = 0; i < allPrefabs.Length; i++)
        {
            if (!allPrefabs[i].activeSelf)
            {
                int spawnSideIndex = Random.Range(0, spawnPositions.Length);
                allPrefabs[i].transform.position = spawnPositions[spawnSideIndex].position;
                if (spawnSideIndex > 4)
                    allPrefabs[i].transform.Rotate(0, 180, 0);
                allPrefabs[i].SetActive(true);
                break;
            }
            else continue;
        }
    }
    private void Initialization()
    {
        for(int i = 0; i < enemyQuantity; i++)
        {
            AddEnemy();
        }
    }
    public void AddBonus()
    {
        int spawnSideIndex = Random.Range(0, spawnPositions.Length);
        Vector3 position = spawnPositions[spawnSideIndex].position;
        Quaternion quaternion;
        if (spawnSideIndex > 4)
            quaternion = new Quaternion(0, 180, 0, 0);
        else quaternion = new Quaternion(0, 0, 0, 0);
        Instantiate(bonusObject, position, quaternion, enemyGroup);
    }
}
