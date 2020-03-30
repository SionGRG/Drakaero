using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float velX = 5f;
    float velY = 0f;
    Rigidbody2D rb;
    // EnemyHit Damage
    public GameObject enemyHitDamage;
    public string[] ignoreList;

    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velX, velY);
    }

    // Destroy enemy upon collision
    void OnCollisionEnter2D(Collision2D other)
    {
        foreach (string tag in ignoreList)
        {
            if (other.gameObject.tag == tag)
            {
                return;
            }
            if (other.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
                EnemyController.Instance.TakeDamage(1);
            }
        }

        float fireLifetime = 0.35f;
        GameObject fire = (GameObject)Instantiate(enemyHitDamage, other.gameObject.transform.position, Quaternion.identity);
        Destroy(fire, fireLifetime);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
    // Destroy fireball when it goes off screen
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
