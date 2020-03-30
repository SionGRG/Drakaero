using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBall : MonoBehaviour
{
    public float velX = -300f;
    float velY = 0f;
    Rigidbody2D rbEnemyFire;
    // Player Hit damage
    public GameObject playerHitDamage;
    public string[] ignoreList;
    
    void Start()
    {
        rbEnemyFire = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Enemy fire ball movement
        rbEnemyFire.velocity = new Vector2(velX, velY);

        // If enemy fireball goes off screen, destroy it
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x > Screen.width + 15 || screenPosition.x < -15)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (string tag in ignoreList)
        {
            if (other.tag == tag)
            {
                return;
            }
            if (other.tag == "Player")
            {
                // Player has been hit
                string playerHit = "PLayer has been hit";
                Debug.Log(playerHit);
                Destroy(gameObject);
            }
        }

        float fireLifetime = 0.35f;
        GameObject fire = (GameObject)Instantiate(playerHitDamage, other.gameObject.transform.position, Quaternion.identity);
        Destroy(fire, fireLifetime);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
