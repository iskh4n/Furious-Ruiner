using UnityEngine;
using System.Collections;

public class EnemyFollow : MonoBehaviour
{
    private Transform playerTransform; // Oyuncu karakterinin transformu
    //public Transform target;
    public float moveSpeed = 5f;
    public float bulletForce = 10f;
    public GameObject bulletPrefab;
    private Rigidbody rb;
    private Animator animator;
    public float deathTime;
    private HealthManagerScript healthManager;
    private ScoreManager scoreManager;
    //private bool hasDamaged = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncu karakterinin transformunu bul
        healthManager = GameObject.FindObjectOfType<HealthManagerScript>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();

    }

     void OnCollisionEnter(Collision collision)
     {
         if (collision.gameObject.CompareTag("Bullet"))
         {
             rb.isKinematic = false;
             Vector3 forceDirection = new Vector3(0.5f, 1f, 1f); // Savrulma yönü
             forceDirection = forceDirection.normalized * bulletForce * collision.gameObject.GetComponent<Rigidbody>().mass;
             Vector3 pointOfImpact = collision.contacts[0].point; // Mermi çarpışma noktası
             forceDirection = Vector3.Normalize(pointOfImpact - transform.position) * bulletForce; // Kuvvet yönü düşmanın merkezine doğru
             rb.AddForce(forceDirection, ForceMode.Impulse);
             animator.speed = 0; // Animasyonu durdur
             scoreManager.IncreaseScore(20);
             Debug.Log("Eklendi");
             //         Die(); // Ölüm durumunu tetikle
             Destroy(this.gameObject, deathTime);
         }

         else if (collision.gameObject.CompareTag("Player"))
         {
             if (rb.isKinematic == true)
             {
                 rb.isKinematic = false;

                 Vector3 forceDirection = new Vector3(0.5f, 1f, 1f); // Savrulma yönü
                 forceDirection = forceDirection.normalized * bulletForce * collision.gameObject.GetComponent<Rigidbody>().mass;

                 Vector3 pointOfImpact = collision.contacts[0].point; // Mermi çarpışma noktası
                 forceDirection = Vector3.Normalize(pointOfImpact - transform.position) * bulletForce; // Kuvvet yönü düşmanın merkezine doğru

                 rb.AddForce(forceDirection, ForceMode.Impulse);

                 animator.speed = 0; // Animasyonu durdur

                 //         Die(); // Ölüm durumunu tetikle
                 Destroy(this.gameObject, deathTime);
                 healthManager.TakeDamage(10);
                 Debug.Log("-10     " + gameObject.name);
             }

         }


     }

  /*  private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasDamaged)
        {
            hasDamaged = true;
            healthManager.TakeDamage(10);
            Debug.Log("-10     " + gameObject.name);
            animator.speed = 0; // Animasyonu durdur
            gameObject.GetComponent<Collider>().isTrigger = false;
           // gameObject.GetComponent<Collider>().enabled = false;

            Die();

        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            rb.isKinematic = false;

            Vector3 forceDirection = new Vector3(0.5f, 1f, 1f); // Savrulma yönü
            forceDirection = forceDirection.normalized * bulletForce * other.gameObject.GetComponent<Rigidbody>().mass;

            //Vector3 pointOfImpact = other.gameObject.contacts[0].point; // Mermi çarpışma noktası
            forceDirection = Vector3.Normalize(forceDirection - transform.position) * bulletForce; // Kuvvet yönü düşmanın merkezine doğru

            rb.AddForce(forceDirection, ForceMode.Impulse);

            // rb.AddForce(collision.gameObject.transform.forward * bulletForce * collision.gameObject.GetComponent<Rigidbody>().mass, ForceMode.Impulse);
            animator.speed = 0; // Animasyonu durdur
            scoreManager.IncreaseScore(20);
            Debug.Log("Eklendi");
            gameObject.GetComponent<Collider>().isTrigger = false;

        }

    }
    */
    void Die()
    {
       // animator.SetBool("Run Forward", false);
        //animator.SetBool("Die",true);
        // Die animasyonunu oynat

        // Düşmanın hareketlerini durdur
        GetComponent<EnemyFollow>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, deathTime);
    }


    void Update()
    {
       // Vector3 targetPosition = target.transform.position;
        Vector3 enemyPosition = transform.position;
        Vector3 directionToTarget = (playerTransform.position - enemyPosition).normalized;



        if (rb.isKinematic)
        {
            transform.position += directionToTarget * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(directionToTarget);
        }
    }
}