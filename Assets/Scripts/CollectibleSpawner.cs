using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject[] spawnPrefabs; // Işınlanacak obje prefabları
    public Transform player; // Oyuncu karakteri
    public Transform[] spawnPoints; // Işınlanma noktaları
    public float spawnDistance = 50.0f; // Işınlanma mesafesi
    public float spawnInterval = 2.0f; // Işınlanma aralığı
    public float despawnDistance = 100.0f; // Silinme mesafesi
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObjects());

    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] spawnedCollectibles = GameObject.FindGameObjectsWithTag("SpawnedCollectible");
        // GameObject[] spawnedCollectibles = GameObject.FindGameObjectsWithTag("SpawnedCollectible");

        foreach (var spawnedCollectible in spawnedCollectibles)
     {
         float distance = Vector3.Distance(spawnedCollectible.transform.position, player.position);

         if (distance > despawnDistance)
         {
             Destroy(spawnedCollectible);
             Debug.Log(" spawned collectible Deleted");
         }
     }
    }



    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Rastgele bir obje prefabı seçin
            GameObject selectedPrefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];
            

                // Rastgele bir spawn noktası seçin
                Transform selectedSpawnPoint = spawnPoints[Random.Range(0, 2)];

                // Işınlanma pozisyonunu hesaplayın
                Vector3 spawnPosition = selectedSpawnPoint.position;
                spawnPosition.z = player.position.z + spawnDistance;
                spawnPosition.y = 10f;

                // Seçilen objeyi doğru pozisyonda oluşturun
                GameObject spawnedObject = Instantiate(selectedPrefab, spawnPosition, selectedPrefab.transform.rotation);

                // Oluşturulan objeye "SpawnedObject" etiketi ekle
                spawnedObject.tag = "SpawnedCollectible";

                yield return new WaitForSeconds(spawnInterval);
            

        }
    }
 }
