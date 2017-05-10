using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingBody : MonoBehaviour {
    public PlayerControl playerControl;
    public Rigidbody2D ammoRigid;
    public Transform ammoRespawn;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            ammoRigid = other.gameObject.GetComponent<Rigidbody2D>();
            playerControl.ammoRigid.transform.position = ammoRespawn.position;
        }
    }
}
