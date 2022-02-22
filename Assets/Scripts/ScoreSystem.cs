using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;

    public const string HighScoreKey = "HighScore";

    private float score;


    
    

    void Update()
    {

        score += Time.deltaTime * scoreMultiplier;

        //FloorToInt changes it from what it was to an int
        scoreText.text = Mathf.FloorToInt(score).ToString();


    }

    // Unity will call this whenever The GameObj attached to Script Gets Destroyed (i.e. GameOver)
    private void OnDestroy()
    {
        // PlayerPrefs is a simple save state class
        // 1st part is the 'key' which is the string name it will go by, 2nd part is default value if it can't find something
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        // Check if the new score is higher than the old high score; If it is save it as new HighScore
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
        }
    }
}
