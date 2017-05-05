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
    public float ammoCount;
    public GameObject winPanel;
    public GameObject losePanel;
    public PlayerControl playerControl;

    public void Start()
    {
        playerControl = GameObject.Find("SlingShot").GetComponent<PlayerControl>();
        ammoCount = playerControl.ammoCount;
    }

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

        else if (ammoCount <= 0 && enemiesKilled < enemiesToKill && Time.time > 0f)
        {
            YouLose();
        }
    }

    public void YouWin()
    {
        Debug.LogWarning("You Win");
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }

    public void YouLose()
    {
        Debug.LogWarning("You Lose");
        Time.timeScale = 0;
        losePanel.SetActive(true);
    }

    public void EnemiesKilledCounter()
    {
        enemiesKilled += 1;
    }
}
