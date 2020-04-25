 using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControls : MonoBehaviour
{
    public float speed;

    private Rigidbody2D _rb;
    private Vector2 moveVelocity;
    public GameObject projectilePrefab;
    public Transform FirePoint;
    private GameObject Player;
    private GameObject FireBallLeft;

    public float FireBallForce = 20f;
    public int maxHealth = 5;
    public int currentHealth;
    public PlayerHealth healthBar;

    static private PlayerControls instance = null;

    // Lets other scripts find the instance of the PlayerControls script
    public static PlayerControls Instance
    {
        get
        {
            return instance;
        }
    }

    // Ensure there is only one instance of this object in the game
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        // checks to see if the fire button has been pressed down to spawn the projectile
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        // Load gameover screen when player health reaches zero
        if (currentHealth == 0)
        {
            GameManager.Instance.GameOver();

        }
    }

    //checks to see if collision has happend with the player 
    void OnCollisionEnter2D(Collision2D collision)
    {
            Destroy(collision.gameObject);
            TakeDamage(1);
    }

    //spawns the projectile at the firepoint object
    void Shoot()
    {
        GameObject FireBallRight = Instantiate(projectilePrefab, FirePoint.position, FirePoint.rotation);
        Rigidbody2D rb = FireBallRight.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePoint.up * FireBallForce, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    //is called when a projectile collides with the player and will take away the nessary health 
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}
