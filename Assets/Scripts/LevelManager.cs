using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;


public class LevelManager : MonoBehaviour
{
    // public float enemiesToKill;
    [Header("Camera")]
    public CameraController cameraController;
    [Space]

    [Header("Player")]
    public PlayerControl playerControl;
    public float ammoCount = 1;

    [Space]
    [Header("UI")]
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject pauseBttn;
    public GameObject pausePanel;
    public Animator winAnim;
    public Animator loseAnim;
    public GameObject winImage;
    public GameObject loseImage;
  

    [Space]
    [Header("UI-Objectives")]
    public Text criminalNumText;
    public Text crocNumText;
    public Text crocsToKillText;
    public Text criminalsToKillText;
    public GameObject inCanvas;

    [Space]
    public float criminalNum;
    public float crocNum;
    public float criminalsToKill;
    public float crocsToKill;
    [Space]
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip bgm;


    private void Awake()
    {
        playerControl = GameObject.Find("SlingFront").GetComponent<PlayerControl>();
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }
   
    public void Start()
    {   
        if (winPanel.activeSelf == true) winPanel.SetActive(false);

        if (losePanel.activeSelf == true) losePanel.SetActive(false);

        if (Time.timeScale == 0) Time.timeScale = 1;

      
        GetComponent<AudioSource>();
        cameraController = Camera.main.GetComponent<CameraController>();
        
        pausePanel.SetActive(false);

        winAnim = winImage.GetComponent<Animator>();
        loseAnim = loseImage.GetComponent<Animator>();

        crocsToKillText.text = crocsToKill.ToString();
        criminalsToKillText.text = criminalsToKill.ToString();

        audioSource.PlayOneShot(bgm);
    }

    public void Update()
    {
        
        //Check if the objectives are met
        if (criminalNum >= criminalsToKill && crocNum >= crocsToKill)
        {
            YouWin();
        }

        //Check if player has no ammo but the enemies are still alive
        else if (ammoCount <= 0 && (criminalNum < criminalsToKill || crocNum < crocsToKill && Time.time > 0f))
        {
            YouLose();
        }

    }

    public void YouWin()
    {
        playerControl.enabled = false;
        HideUI();
        winPanel.SetActive(true);
        //Time.timeScale = 0;
        winAnim.SetTrigger("win");
       
    }

    public void YouLose()
    {
        playerControl.enabled = false;
        HideUI();
        losePanel.SetActive(true);
        loseAnim.SetTrigger("lose");
        //Time.timeScale = 0;
    }

    //Update number of killed enemies by getting its EnemyType and incrementing criminalNum
    public void UpdateEnemyKilled(string enemyType, float killedNumber)
    {
        if (enemyType == null)return;

        if (enemyType == "Criminal")
        {
            criminalNum += killedNumber;
            criminalNumText.text = criminalNum.ToString();
        }

        else if (enemyType == "Crocodile")
        {
            crocNum += killedNumber;
            crocNumText.text = crocNum.ToString();
        }
        
    }

    // Pause Button
    public void TogglePauseMenu()
    {
        //if (Time.timeScale == 0) return;

        pauseBttn.SetActive(!pauseBttn.activeSelf);
        pausePanel.SetActive(!pausePanel.activeSelf);
        Time.timeScale = (pauseBttn.activeSelf) ? 1 : 0;
    }

    //Retry Button
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    //Main Menu Button
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Hide UI on Win or Lose
    public void HideUI()
    {
        playerControl.enabled = false;
        cameraController.enabled = false;

        foreach( Transform t in inCanvas.transform)
        {
            t.gameObject.SetActive(false);
        }
    }
    public void UpdateAmmo(float ammo)
    {
        ammoCount = ammo;
    }

    
}
