using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
    public float maxHealth;
    public float currentHealth;

    private void Start() {
        currentHealth = maxHealth;
     }
    public void DamageHealth(float damage) {
        currentHealth -= damage;
    }

    private void Update() {
        if (currentHealth <= 0) {
            Dead();
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
