using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] GameObject[] carPrefabs;

    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] float maxSpawnTime = 3f;

    // GÜNCELLEME: PlayerController olarak değiştirildi
    [SerializeField] PlayerController _playerController; 

    void Start()
    {
        // Eğer editörde sürüklemeyi unuttuysak veya yapamadıysak, 
        // bu kod sahnedeki PlayerController scriptini kendi kendine arayıp bulur.
        if (_playerController == null)
        {
            _playerController = FindObjectOfType<PlayerController>();
        }

        StartCoroutine(SpawnCars());
    }

    IEnumerator SpawnCars()
    {
        while (_playerController.isAlive)
        {
            float randomTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomTime);

            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], spawnPoint.position, spawnPoint.rotation);
        }
    }
}