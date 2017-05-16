using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

   // public float respawnRate;
   // public float nextRespawn;
    //public Transform[] respawnPoint;
    public GameObject enemies;
    public int rndVal;
    public float enemiesToKill;
    public float enemiesKilled;
    public float ammoCount = 1;
    public GameObject winPanel;
    public GameObject losePanel;
    public PlayerControl playerControl;
    public GameObject pauseMenu;
    public GameObject pausePanel;

    public Text enemiesKilledText;
    public Text enemiesToKillText;

    public float secondsToWait =.1f;


    public void Start()
    {
        playerControl = GameObject.Find("SlingFront").GetComponent<PlayerControl>();
        ammoCount = playerControl.ammoCount;
        pausePanel.SetActive(false);

        UpdateEnemiesText();
    }

    public void Update()
    {
        //if (Time.time > nextRespawn)
        //{
        //    nextRespawn += respawnRate;
        //    rndVal = Random.Range(0, 2);
        //    Instantiate(enemies, respawnPoint[rndVal].position, transform.rotation);
        //}

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
        playerControl.enabled = false;
        StartCoroutine(IEYouWin());
    }

    public void YouLose()
    {
        playerControl.enabled = false;
        StartCoroutine(IEYouLose());
    }

    public void EnemiesKilledCounter()
    {
        enemiesKilled += 1;
        UpdateEnemiesText();
        
    }

    public void UpdateEnemiesText()
    {
        enemiesKilledText.text = enemiesKilled.ToString();
        enemiesToKillText.text = enemiesToKill.ToString();
    }
    
    private IEnumerator IEYouWin()
    {
        yield return new WaitForSeconds(secondsToWait);
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }

    public IEnumerator IEYouLose()
    {
        yield return new WaitForSeconds(secondsToWait);
        Time.timeScale = 0;
       
        losePanel.SetActive(true);
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        pausePanel.SetActive(!pausePanel.activeSelf);
        Time.timeScale = (pauseMenu.activeSelf) ? 1 : 0;


    }
}
