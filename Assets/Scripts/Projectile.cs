using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public Rigidbody2D myRigid;
    public float projectileSpeed;
    public EnemyHealth enemyHealth;
    public float damageHp;
    public GameObject smokePrefab;

    private void Awake() {  
        myRigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
            Instantiate(smokePrefab, new Vector3(transform.position.x, transform.position.y, -5f), Quaternion.Euler(0, 180, 0));

        else
            Debug.LogWarning(other.gameObject.tag.ToString());
       
        if (other.gameObject.tag == "Enemy")
        {
            damageHp = 1;
            enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.DamageHealth(damageHp);
            myRigid.AddForce(new Vector2(-myRigid.velocity.x, myRigid.velocity.y), ForceMode2D.Force);
            //myRigid.velocity = new Vector2(myRigid.velocity.x - other.gameObject.GetComponent<Rigidbody2D>().mass,myRigid.velocity.y);
        }

        else if (other.gameObject.tag == "Civilian")
        {
            LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            levelManager.YouLose();
        }
    }
}
