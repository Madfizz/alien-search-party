using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int amountAliensGreen;
    public int amountAliensGrey;
    public GameObject[] alienPrefabs;

    public Transform[] alienGroupings;

    public Transform[] spawnPoints;
    public Transform[] farms;
    public Transform[] towns;

    // List of pooled Aliens
    private List<GameObject>[] aliensList;

    enum AlienType {Green, Grey};

    void Start()
    {
        CreateAliens();
        InvokeRepeating("SpawnAliens", 1.0f, 5.0f);
    }

    void CreateAliens()
    {
        aliensList = new List<GameObject>[2];
        aliensList[(int)AlienType.Green] = SetupAliens(AlienType.Green, amountAliensGreen, alienPrefabs[(int)AlienType.Green], alienGroupings[(int)AlienType.Green]);
        aliensList[(int)AlienType.Grey] = SetupAliens(AlienType.Grey, amountAliensGrey, alienPrefabs[(int)AlienType.Grey], alienGroupings[(int)AlienType.Grey]);
    }

    // Helper function to setup alien object pooling for different types
    List<GameObject> SetupAliens(AlienType _type, int _amount, GameObject _prefab, Transform _parent)
    {
        List<GameObject> alienList = ObjectPooler.CreateObjectPool(_amount, _prefab);
        ObjectPooler.AssignParentGroup(alienList, _parent);

        return alienList;
    }

    void SpawnAliens()
    {
        int rndType = RandomSpawnChance();
        GameObject alien = ObjectPooler.GetPooledObject(aliensList[rndType]);

        Transform destination;

        // If alien spawned is Green, send to a random farm
        if (rndType == (int)AlienType.Green)
        {
            destination = farms[Random.Range(0, farms.Length)];
        }
        // Else if alien spawned is Grey, send to a random town
        else
        {
            destination = towns[Random.Range(0, towns.Length)];
        }

        // Set spawned alien's destination
        alien.GetComponent<AlienBase>().target = destination;

        alien.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        alien.SetActive(true);
    }

    int RandomSpawnChance()
    {
        int rnd = Random.Range(1, 6);

        if (rnd != 5)
        {
            return 0;
        }

        else 
        {
            return 1;
        }
    }
}
