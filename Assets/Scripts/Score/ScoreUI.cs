using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score:00}";
    }
}
