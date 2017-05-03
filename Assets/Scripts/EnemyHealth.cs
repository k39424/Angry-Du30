using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
    public float maxHealth;
    public float currentHealth;
    public LevelManager levelManager;

    private void Start() {
        currentHealth = maxHealth;
     }
    public void DamageHealth(float damage) {
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

    public void Dead()
    {
        levelManager.EnemiesKilledCounter();
        Destroy(gameObject);
    }
}
