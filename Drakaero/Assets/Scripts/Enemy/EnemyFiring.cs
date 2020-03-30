using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    /* Enemy Firing */
    public GameObject enemyShot;
    public Vector2 timeBtnShots = new Vector2(0.5f, 9.5f);
    public float speed = 1;
    private float nxtShot = -1;
    private bool ok2Fire = false;
    public Transform EnemyFirePoint;
    public float EnemyFireBallForce = 20f;


    void Awake()
    {
        Debug.Assert(enemyShot.GetComponent<EnemyFireBall>() != null);
        Random.seed = (int)Time.realtimeSinceStartup;
    }

    void Start()
    {
        nxtShot = Time.time + Random.Range(timeBtnShots.x, timeBtnShots.y);
    }

    void FixedUpdate()
    {
        if (ok2Fire && nxtShot < Time.time)
        {
            EnemyFire();
        }
    }

    public void EnemyFire()
    {
        nxtShot = Time.time + Random.Range(timeBtnShots.x, timeBtnShots.y);
        GameObject EnemyFireBall = Instantiate(enemyShot, EnemyFirePoint.position, EnemyFirePoint.rotation);
        Rigidbody2D rb = EnemyFireBall.GetComponent<Rigidbody2D>();
        rb.AddForce(EnemyFirePoint.up * EnemyFireBallForce, ForceMode2D.Impulse);
    }
    
    private void OnBecameVisible()
    {
        ok2Fire = true;
    }
    private void OnBecameInvisible()
    {
        ok2Fire = false;
    }
}
