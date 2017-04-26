using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    // Use this for initialization
    public int sceneToLoad;
    public GameObject PanelToShow;
    public GameObject PanelToHide;
	
    public void LoadScene() {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitOnClick() {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadPanelOnClick() {
            PanelToHide.SetActive(false);
            PanelToShow.SetActive(true);  
    }
}
