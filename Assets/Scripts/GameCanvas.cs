using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour {

    public PlayerControl playerControl;

    private void Awake()
    {
        playerControl = GameObject.Find("SlingFront").GetComponent<PlayerControl>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            // Debug.Log("exited");
            Destroy(other.gameObject);
            playerControl.Reload();
        }
    }
}