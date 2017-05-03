using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    public int enemiesToKill;
    public int enemiesKilled;

    public PlayerControl playerControl;
    public float ammoCount;

    //for win and lose
    public GameObject winPanel;
    public GameObject losePanel;

    public void Start()
    {
        if(enemiesToKill <= 0)
        {
            enemiesToKill = 5;

            ammoCount = playerControl.ammoCount;
        }
    }

    public void Update()
    {
        if (Time.time > nextRespawn)
        {
            nextRespawn += respawnRate;
            rndVal = Random.Range(0, 1);
            Instantiate(enemies, respawnPoint[rndVal].position, transform.rotation);
        }

        if (enemiesKilled >= enemiesToKill)
        {
            YouWin();
<<<<<<< HEAD

        }

        else if (ammoCount <= 0 && enemiesKilled < enemiesToKill)
        {
            YouLose();
=======
        }

        else if(ammoCount <= 0)
        {
            YouLose();

>>>>>>> a385d17b65755417d08772b87054df3f9b999d76
        }
    }

    public void YouWin()
    {
<<<<<<< HEAD
        winPanel.SetActive(true);
        Time.timeScale = 0;
=======
        Time.timeScale = 0;

>>>>>>> a385d17b65755417d08772b87054df3f9b999d76
    }

    public void YouLose()
    {
<<<<<<< HEAD
        losePanel.SetActive(true);
        Time.timeScale = 0;
=======

>>>>>>> a385d17b65755417d08772b87054df3f9b999d76
    }

    public void EnemiesKilledCounter()
    {
        enemiesKilled += 1;
    }
}
