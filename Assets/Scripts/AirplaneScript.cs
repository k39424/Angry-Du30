using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneScript : MonoBehaviour {
    public Rigidbody2D myRigid;
   //public AudioSource audioSource;
    public GameObject explosionFX;
    public float speed;

    private void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed *Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            myRigid.velocity = Vector2.zero;

            Destroy(this.gameObject);
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Instantiate(explosionFX, myRigid.transform.position, Quaternion.identity);
        }
    }


}
