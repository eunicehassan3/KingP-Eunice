using TMPro;
using UnityEngine;

public class ScoreBehavior : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    float score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = BallBehavior2.score;
        string message = string.Format("SCORE: {0:0}", score);
        scoreText.text = message;
    }
}
