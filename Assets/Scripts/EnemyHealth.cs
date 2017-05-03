using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
    public float maxHealth;
    public float currentHealth;
    public LevelManager levelManager;

    public float damage;
    
    

    public LevelManager levelManager;

    private void Start() {
        currentHealth = maxHealth;
        damage = 1f;

        levelManager = GetComponent<LevelManager>();
     }

    public void DamageHealth(float damage)
    {
        currentHealth -= damage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            DamageHealth(1f);
        }
    }

    private void Update() {
        if (currentHealth <= 0) {
            Dead();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            Debug.Log("Hit!");
            //DamageHealth(damage);
        }
    }

    public void Dead()
    {
<<<<<<< HEAD
        Debug.Log("Dead");
        //Destroy(gameObject);
        //levelManager.EnemiesKilledCounter();
=======
        levelManager.EnemiesKilledCounter();
        Destroy(gameObject);
>>>>>>> a385d17b65755417d08772b87054df3f9b999d76
    }
}
