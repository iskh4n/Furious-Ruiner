using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float carSpeed = 10f; // Araba hızı
    private HealthManagerScript healthManager;
    private ScoreManager scoreManager;
    private bool hasDamaged = false;

    private void Start()
    {
        healthManager = GameObject.FindObjectOfType<HealthManagerScript>();
        GetComponent<Collider>().enabled = true;
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();


    }
    private void Update()
    {
        // Arabayı ileri doğru hareket ettir
        transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);

        if (scoreManager.score > 500 && scoreManager.score<=1000)
        {
            carSpeed = 30f;
          //  Debug.Log(scoreManager.score+" "+carSpeed);
        }
        else if (scoreManager.score > 1000)
        {
            carSpeed = 50f;
           // Debug.Log(scoreManager.score + " " + carSpeed);
        }
    }



    private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player") && !hasDamaged)
         {
             // Arabaya çarpıldığında karaktere hasar verme

             healthManager.TakeDamage(30);
             Debug.Log("-30");
            hasDamaged = true;
            GetComponent<Collider>().enabled = false;



         }
     }
    
    


}
