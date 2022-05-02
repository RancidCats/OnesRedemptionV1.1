using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public float alturaIndicator;
    public float timer;
    public float cooldown;

    void Update()
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
        for (int x = 0; x < 9; x++)
        {
            Vector3 randomSpawnPosition = transform.position + new Vector3(Random.Range(-10, 11), alturaIndicator, Random.Range(-10, 11));
            Instantiate(cubePrefab, randomSpawnPosition, Quaternion.identity);
        }
    }
}
