using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public float respawnRate;
    public float nextRespawn;
    public Transform[] respawnPoint;
    public GameObject enemies;
    public int rndVal;


    public void Update()
    {
        if (Time.time > nextRespawn)
        {
            nextRespawn += respawnRate;
            rndVal = Random.Range(0, 2);
            Instantiate(enemies, respawnPoint[rndVal].position, transform.rotation);
        }
    }
    }
