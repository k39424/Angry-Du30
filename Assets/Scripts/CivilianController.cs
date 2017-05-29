using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianController : MonoBehaviour {
    public Rigidbody2D myRigid;
    public AudioSource audioSource;
    public AudioClip hurtAudio;

    private void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            audioSource.clip = hurtAudio;
            audioSource.Play();
        }

    }
}
