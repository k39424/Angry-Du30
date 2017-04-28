using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour {

    public PlayerControl playerControl;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ammo")
        {
            Destroy(other.gameObject);
            playerControl.Reload();
        }
    }
}
