using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnPrefabs; // Işınlanacak obje prefabları
    public Transform player; // Oyuncu karakteri
    public Transform[] spawnPoints; // Işınlanma noktaları
    public float spawnDistance = 50.0f; // Işınlanma mesafesi
    public float spawnInterval = 2.0f; // Işınlanma aralığı
    public float despawnDistance = 100.0f; // Silinme mesafesi

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }
    private void Update()
    {
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("SpawnedObject");
       // GameObject[] spawnedCollectibles = GameObject.FindGameObjectsWithTag("SpawnedCollectible");

        foreach (var spawnedObject in spawnedObjects)
        {
            float distance = Vector3.Distance(spawnedObject.transform.position, player.position);

            if (distance > despawnDistance)
            {
                Destroy(spawnedObject);
                Debug.Log(" spawned object Deleted");
            }
        }

    }

private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Rastgele bir obje prefabı seçin
            GameObject selectedPrefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];
            if (selectedPrefab.tag == "Enemy") { 

            // Rastgele bir spawn noktası seçin
            Transform selectedSpawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];

            // Işınlanma pozisyonunu hesaplayın
            Vector3 spawnPosition = selectedSpawnPoint.position;
            spawnPosition.z = player.position.z + spawnDistance;
            spawnPosition.y = 8f;

            // Seçilen objeyi doğru pozisyonda oluşturun
            GameObject spawnedObject = Instantiate(selectedPrefab, spawnPosition, selectedPrefab.transform.rotation);

            // Oluşturulan objeye "SpawnedObject" etiketi ekle
            spawnedObject.tag = "SpawnedObject";

            yield return new WaitForSeconds(spawnInterval);
            }

      




        }
    }





}
