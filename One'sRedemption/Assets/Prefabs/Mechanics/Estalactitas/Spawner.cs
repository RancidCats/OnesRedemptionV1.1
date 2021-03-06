using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public GameObject cubePrefab;
    public float alturaIndicator;
    public float timer;
    public float cooldown;
    public float cantEstalactitas;
    public bool isEnabled;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
    }
    private void Start()
    {
        isEnabled = false;
    }

    void Update()
    {
        if(BossController.instance.SpawnerOff == true)
            if(isEnabled)
                SpawningRoutine();
    }

    void SpawningRoutine()
    {
        
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            SpawnCubes();
            timer = 0;
        }
    }
    void SpawnCubes()
    {
        for (int x = 0; x < cantEstalactitas; x++)
        {
            Vector3 randomSpawnPosition = transform.position + new Vector3(Random.Range(-15, 16), alturaIndicator, Random.Range(-15, 16));
            Instantiate(cubePrefab, randomSpawnPosition, Quaternion.identity);
        }
    }
}
