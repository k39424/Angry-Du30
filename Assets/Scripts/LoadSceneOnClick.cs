using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

   //For loading scenes
    public int sceneToLoad;

    //For Panel showing and hiding
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
