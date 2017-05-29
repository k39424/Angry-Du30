using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public GameObject enemyObj;
    public Rigidbody2D enemyRigid;
    public float speed;
    public bool isWalking;
    public bool isIdle;
    public Rigidbody2D myRigid;

    private void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (isIdle)
            return;

        if (isWalking)
            transform.Translate( Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ammo")
        {
            myRigid.velocity = Vector3.zero;
        }
    }
}
