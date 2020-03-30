using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject SpawnPosition;
    public int enemyCount = 20;

    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    private float timeDelta = 0;

    float spawnPosX;
    float spawnPosY;

    static private EnemySpawner instance = null;

    // Lets other scripts find the instance of the EnemySpawner script
    public static EnemySpawner Instance
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
        Debug.Assert(enemyPrefab != false, "Enemy prefab unassigned");
        timeDelta = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn enemies
        Spawning();
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        spawnPosX = SpawnPosition.transform.position.x;
        int varPosY = Random.Range(-2,2);
        spawnPosY = SpawnPosition.transform.position.y + varPosY;

        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = new Vector2(spawnPosX, spawnPosY);
    }

    void Spawning()
    {
        // Whilst timeDelta is above zero, we minus the time between the last update calls 
        // then check timeDelta again, if it is below zero we know the correct time has passed 
        // so we spawn an Enenemy, then We reset timeDelta to a random value.
        if (timeDelta > 0)
        {
            timeDelta -= Time.deltaTime;
            if (timeDelta <= 0)
            {
                SpawnEnemy();

                timeDelta = Random.Range(minTime, maxTime);
            }
        }
    }
}
