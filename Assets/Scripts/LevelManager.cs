using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{

    public float respawnRate;
    public float nextRespawn;
    public Transform[] respawnPoint;
    public GameObject enemies;
    public int rndVal;
    public int enemiesToKill;
    public int enemiesKilled;
    public int ammoCount;


    public void Update()
    {
        if (Time.time > nextRespawn)
        {
            nextRespawn += respawnRate;
            rndVal = Random.Range(0, 2);
            Instantiate(enemies, respawnPoint[rndVal].position, transform.rotation);
        }

        if (enemiesKilled >= enemiesToKill)
        {
            YouWin();
        }

        else if(ammoCount <= 0)
        {
            YouLose();
        }
    }

    public void YouWin()
    {
        Time.timeScale = 0;
    } 

    public void YouLose()
    {
        Time.timeScale = 0;
    }

    public void EnemiesKilledCounter()
    {
        enemiesKilled += 1;
    }
}
