using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManagement : MonoBehaviour
{
    public Button playButton;
    public Button soundOnButton;
    public Button soundOffButton;
    public Button powerOffButton;
    public Button helpButton;
    public GameObject helpPanel;
    public Button closeHelpButton;
    public AudioSource musicAudioSource;
    private bool isMusicEnabled;
   private const string MusicPrefKey = "MusicEnabled";
    public bool soundEnabled;
    public bool helpEnabled = true;
    public Text hScoreText;
    private int hScore;
    public GameObject player;
    public GameObject genelPanel; 
    private void Start()
    {
       // Animator playerAnimator = player.GetComponent<Animator>();
        hScore = PlayerPrefs.GetInt("HighScore", 0);
        hScoreText.text = hScore.ToString();

        Time.timeScale = 1f; // Oyun zamanını durdur

        playButton.onClick.AddListener(PlayGame);
        soundOnButton.onClick.AddListener(EnableSound);
        soundOffButton.onClick.AddListener(DisableSound);
        powerOffButton.onClick.AddListener(ExitGame);
        helpButton.onClick.AddListener(OpenHelp);
        closeHelpButton.onClick.AddListener(CloseHelp);

        // Ses ayarlarını kontrol et
        /*soundEnabled = PlayerPrefs.GetInt(MusicPrefKey, 1) == 1;

        if (soundEnabled)
        {
            isMusicEnabled = true;
            PlayMusic();
        }
        else
        {
            isMusicEnabled = false;
            musicAudioSource.Stop();
        }

        // Soundoff butonunun görünürlüğünü güncelle
      //  soundOffButton.gameObject.SetActive(soundEnabled);
       // soundOnButton.gameObject.SetActive(!soundEnabled);
        */
    }
    public void EnableSound()
    {
        soundEnabled = false;
        soundOnButton.gameObject.SetActive(false);
        soundOffButton.gameObject.SetActive(true);
        musicAudioSource.Stop();
        //PlayerPrefs.SetInt(MusicPrefKey, 1); // Sound açık olarak kaydedilir
    }

    public void DisableSound()
    {
        soundEnabled = true;
        soundOnButton.gameObject.SetActive(true);
        soundOffButton.gameObject.SetActive(false);
        // PlayMusic();
        musicAudioSource.Play();

        //PlayerPrefs.SetInt(MusicPrefKey, 0); // Sound kapalı olarak kaydedilir


    }
    public void OpenHelp()
    {
        helpEnabled = true;
        helpPanel.gameObject.SetActive(true);
        helpButton.gameObject.SetActive(false);
        closeHelpButton.gameObject.SetActive(true);
    }
    public void CloseHelp()
    {
        helpEnabled = false;
         helpPanel.gameObject.SetActive(false);
        helpButton.gameObject.SetActive(true);
        closeHelpButton.gameObject.SetActive(false);
       
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Oyun kapanıyor.");
    }
    public void PlayGame()
    {
        // SceneManager.LoadScene("GameScene");
        StartCoroutine(PlayGameCoroutine());
        Debug.Log("OYUN BAŞLIYOR");
    }
    private IEnumerator PlayGameCoroutine()
    {

        Animator playerAnimator = player.GetComponent<Animator>();
        genelPanel.SetActive(false);
        //playerAnimator.SetBool("Play", true);
        Debug.Log(playerAnimator.GetBool("Play"));
        playerAnimator.SetBool("Play", true);
        Debug.Log(playerAnimator.GetBool("Play"));
        
        // Animasyonun bitmesini bekler
        // yield return new WaitForSeconds(playerAnimator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(3f);

        // Sahne geçişini yap
        SceneManager.LoadScene("GameScene");
    }
    public void PlayMusic()
    {
        if (isMusicEnabled)
        {
            musicAudioSource.Play();
        }
        else
        {
            musicAudioSource.Stop();
        }
    }
}
