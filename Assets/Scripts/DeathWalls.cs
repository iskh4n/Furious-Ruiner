using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathWalls : MonoBehaviour
{

    private HealthManagerScript healthManager;

    // Start is called before the first frame update
    void Start()
    {
       healthManager = GameObject.FindObjectOfType<HealthManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f; // Oyun zamanını durdur
                                 // Ek olarak isterseniz diğer işlemleri yapabilirsiniz
                                 // Örneğin oyunu durdurduğunuzu belirten bir mesaj gösterebilirsiniz.
            Debug.Log("Game Over! DUVARA ÇARPTIN.");

            healthManager.healthScore = 0;



        }
    }
}
