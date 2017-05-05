using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public Rigidbody2D myRigid;
    public float projectileSpeed;
    public EnemyHealth enemyHealth;
    public float damageHp;

    private void Awake() {  
        myRigid = GetComponent<Rigidbody2D>();
        //rigidBody.AddForce(transform.right * projectileSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy")
        {
            damageHp = 1;
            enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.DamageHealth(damageHp);
        }

        else if (other.gameObject.tag == "Civilian")
        {
            LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            levelManager.YouLose();
        }
    
    }

    
}
