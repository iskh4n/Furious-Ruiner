using UnityEngine;

public class PumpkinCode : MonoBehaviour
{
    private Rigidbody[] pumpkinParts;
    public GameObject explosionEffect;
    private HealthManagerScript healthManager;
    private ScoreManager scoreManager;
    private bool hasDamaged = false;
    private void Start()
    {
        pumpkinParts = GetComponentsInChildren<Rigidbody>();
        SetKinematic(true);
        healthManager = GameObject.FindObjectOfType<HealthManagerScript>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();

    }
    private void SetKinematic(bool isKinematic)
    {
        foreach (Rigidbody part in pumpkinParts)
        {
            part.isKinematic = isKinematic;
                
         }
    }
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Bullet") && !hasDamaged)
        {
            // Bullet objesiyle çarpışma gerçekleştiğinde
            // patlama efektini tetikle
            TriggerExplosion();
            SetKinematic(false);
            hasDamaged = true;
            scoreManager.IncreaseScore(20); Debug.Log("Eklendi pumpkin");
        }
        else if (other.gameObject.CompareTag("Player") && !hasDamaged)
        {
            // Player objesiyle çarpışma gerçekleştiğinde
            // patlama efektini tetikle
            TriggerExplosion();
            SetKinematic(false);
            hasDamaged = true;
            healthManager.TakeDamage(50);
            Debug.Log("-50");
            scoreManager.IncreaseScore(20);
        }
    }
    private void TriggerExplosion()
    {
        // Patlama efektini instantiate et ve pozisyonunu ayarla
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Balkabağını yok et
        Destroy(gameObject, 5f);
        GetComponent<Collider>().isTrigger = false;

    }


}
