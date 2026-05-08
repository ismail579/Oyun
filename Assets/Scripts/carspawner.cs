
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] GameObject[] carPrefabs; // Üretilecek olan arabalar.

    // Minimum ve maksimum üretme aralýðý
    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] float maxSpawnTime = 3f;

    [SerializeField] CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCars());
    }

    IEnumerator SpawnCars()
    {
        while (_characterController.isAlive)
        {
            // Rast gele bir süre beklemeliyiz
            float randomTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomTime);

            // Rast gele bir referans noktasý seçelim
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Arabayý üretmek
            Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], spawnPoint.position, spawnPoint.rotation);
        }
    }
}