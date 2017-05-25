using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {
    public string nextLevelName;

    public void NextLvl()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
