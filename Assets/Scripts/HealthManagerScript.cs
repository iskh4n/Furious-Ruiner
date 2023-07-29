using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManagerScript : MonoBehaviour
{
    public Image healthBar;
    public float healthScore = 100f;
    public GameObject gameOverPanel;
    public GameObject playpanel;
    public GameObject Shield;
    private bool isProtected = false;
    private float protectionDuration = 10f; // Kalkan koruma süresi (örneğin 2 saniye)

    void Start()
    {
        Time.timeScale = 1f; // Oyun zamanını durdur
        //healthScore = 100f;

}

// Update is called once per frame
void Update()
    {
        if (healthScore <= 0)
        {
            gameOverPanel.SetActive(true);
            playpanel.SetActive(false);
            Time.timeScale = 0f; // Oyun zamanını durdur
        }
        else if (healthScore >= 100)
        {
            healthScore = 100;
        }
    }


    public void TakeDamage(float damage)
    {
        if (isProtected)
        {
            // Kalkan koruması aktifken hasar almayı engelle
            return;
        }
        else
        {
            healthScore -= damage;
            healthBar.fillAmount = healthScore / 100f;
        }
       
    }

    public void Heal(float healingPoint)
    {
        if (healthScore < 100) { 
        healthScore += healingPoint;
        healthScore = Mathf.Clamp(healthScore, 0, 100);
        healthBar.fillAmount = healthScore / 100f;
        }
      
    }

    public void ActivateShield()
    {
        isProtected = true;

        // Kalkan korumasını belirli bir süre sonra devre dışı bırak
        StartCoroutine(DisableShieldAfterDuration());
        Debug.Log("koruma başladı");
        Shield.SetActive(true);

    }

    private IEnumerator DisableShieldAfterDuration()
    {
        yield return new WaitForSeconds(protectionDuration);
        isProtected = false;
        Debug.Log("koruma bitti");
        Shield.SetActive(false);

    }

}
