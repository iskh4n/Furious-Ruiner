using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject playButton;
    public Button soundOnButton;
    public Button soundOffButton;
    public bool soundEnabled = true;
    //private const string MusicPrefKey = "MusicEnabled";
    private bool isMusicEnabled = true;
    public AudioSource musicAudioSource;
    private ScoreManager scoreManager;
    private NewMovement PlayerMovement;
    private AudioListener audioListener;

    void Start()
    {
        // Başlangıçta oyun devam ederken pauseButton aktif, playButton ise deaktif olsun
        pauseButton.SetActive(true);
        playButton.SetActive(false);
        soundOnButton.onClick.AddListener(EnableSound);
        soundOffButton.onClick.AddListener(DisableSound);
       // soundEnabled = PlayerPrefs.GetInt(MusicPrefKey, 1) == 1;
        soundOffButton.gameObject.SetActive(!soundEnabled);
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        PlayerMovement = GameObject.FindObjectOfType<NewMovement>();
        audioListener = Camera.main.GetComponent<AudioListener>();

    }
    private void Update()
    {
        if (scoreManager.score > 500 && scoreManager.score <= 1000)
        {
            PlayerMovement.playerHorizontalSpeed = 20f;
        }
        else if (scoreManager.score > 1000 && scoreManager.score <= 2000)
        {
            PlayerMovement.playerHorizontalSpeed = 25f;
        }
        else if (scoreManager.score > 2000 )
        {
            PlayerMovement.playerHorizontalSpeed = 30f;
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f; // Oyun zamanını durdur
        pauseButton.SetActive(false); // Pause düğmesini deaktif et
        playButton.SetActive(true); // Play düğmesini etkinleştir
    }
    public void PlayGame()
    {
        Time.timeScale = 1f; // Oyun zamanını devam ettir
        playButton.SetActive(false); // Play düğmesini deaktif et
        pauseButton.SetActive(true); // Pause düğmesini etkinleştir
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // Oyun zamanını 1 olarak ayarla, canlı hale getir
        Debug.Log("Restarting game...");
        try
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        catch (System.Exception e)
        {
            Debug.Log("Error loading scene: " + e.Message);
        }
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void EnableSound()
    {
        soundEnabled = true;
        soundOnButton.gameObject.SetActive(false);
        soundOffButton.gameObject.SetActive(true);
        musicAudioSource.Stop();
        if (audioListener != null )
        {
            audioListener.enabled = false;
        }

        // Tüm müzikleri açmak için gerekli kodları burada ekleyebilirsiniz
    }

    public void DisableSound()
    {
        soundEnabled = false;
        soundOnButton.gameObject.SetActive(true);
        soundOffButton.gameObject.SetActive(false);
        // PlayMusic();
        musicAudioSource.Play();

        if (audioListener != null)
        {
            audioListener.enabled = true;
        }

        // Tüm müzikleri kapatmak için gerekli kodları burada ekleyebilirsiniz
    }
    public void PlayMusic()
    {
        if (isMusicEnabled)
        {
            musicAudioSource.Play();
        }
    }
}
