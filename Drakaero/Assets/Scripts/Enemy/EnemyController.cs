using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /* Enemy Movement */
    public float velX = -3f;
    float velY = 0f;
    Rigidbody2D rbEnemy;

    //Enemy Health
    public int maxHealth = 2;
    public int currentEnemyHealth;
    public EnemyHealth healthBar;
    public string[] ignoreList;

    // Enemy Death
    private bool EnemyOnScreen;

    static private EnemyController instance = null;

    // Let other scripts find the instance of ths=is script
    public static EnemyController Instance
    {
        get
        {
            return instance;
        }
    }
    // Ensure there is only one instance of this object in the game
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rbEnemy = GetComponent<Rigidbody2D>();
        // Set Enemy Health
        currentEnemyHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        // Enemy Starts of screen
        EnemyOnScreen = true;
    }

    // Update is called once per frame
    void Update()
    {
        rbEnemy.velocity = new Vector2(velX, velY);
        // Destroy the enemy when health reaches zero
        if (currentEnemyHealth == 0)
        {
            EnemyDeath();
        }
        DeathLine();
    }

    void DeathLine()
    {
        // If enemy goes off screen, destroy it
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x > Screen.width + 15 || screenPosition.x < -15)
        {
            EnemyDeath();
        }
    }

    // Destroy enemy upon leaving screen area
    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (string tag in ignoreList)
        {
            if (other.gameObject.tag == tag)
            {
                return;
            }
            if (other.gameObject.tag == "EnemyDeathLine")
            {
                Destroy(gameObject);
                Debug.Log("The enemy has gone off the screen area");
                EnemyDeath();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentEnemyHealth -= damage;
        healthBar.SetHealth(currentEnemyHealth);
    }

    public void EnemyDeath()
    {
        Destroy(gameObject);
    }

    private void EnemyBecameInvisible()
    {
        Debug.Log("The enemy has gone off the screen");
        EnemyDeath();
    }
}
