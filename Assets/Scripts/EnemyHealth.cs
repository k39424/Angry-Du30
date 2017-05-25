using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
    [Header("Pre-Defined Values:")]
    public float maxHealth;
    public float currentHealth;
    public LevelManager levelManager;
    public string enemyType;
    public AudioSource audioSource;
    public AudioClip hurtFX;
    public AudioClip deathFX;

    public float damage;
    
    private void Awake() {
        currentHealth = maxHealth;
        damage = 1f;

        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        //audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        
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
  
            audioSource.clip = hurtFX;
               audioSource.PlayOneShot(hurtFX);
        }
    }

  

    private void Update() {
        if (currentHealth <= 0) {
            
            Dead();
        }
    }


    public void Dead()
    {
            foreach (Transform t in this.transform)
            {
               // t.GetComponent<Collider2D>().enabled = false;
                t.gameObject.SetActive(false);
            }
            
        Destroy(gameObject,1f);
    }

    private void OnDestroy()
    {
        if(gameObject.tag == "Enemy" && currentHealth <= 0)
            levelManager.UpdateEnemyKilled(enemyType, 1f);
    }
}
