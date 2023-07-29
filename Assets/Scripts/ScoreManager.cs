using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text scoreTextt;

    public Text highScoreText;

    public Transform playerTransform;
    public int score;
    public int highScore;
    public GameObject Startpos;
    private float distance;
    private void Start()
    {
       
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
        scoreTextt.text = score.ToString();
        highScoreText.text = highScore.ToString();
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);

        }
    }
    private void Update()
    {
        UpdateScoreText();
        distance = (Startpos.transform.position.z + playerTransform.position.z);
        score = Mathf.RoundToInt(distance);

    }
    public void IncreaseScore(int amount)
    {
        score += amount;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);

        }
        

        //PlayerPrefs.DeleteAll();

    }
}
