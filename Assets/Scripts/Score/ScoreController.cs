using R3;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    ScoreData scoreData;
    [SerializeField] ScoreUI scoreUI;

    static public ScoreController Instance { get; private set; }

    private void Awake()
    {
        Instance = this; // Scene内で複数作成されない前提

        scoreData = new();
    }

    private void Start()
    {
        scoreData.Score.Subscribe(s => scoreUI.UpdateScore(s)).AddTo(this);
    }

    public void AddScore()
    {
        scoreData.AddScore();
    }
}
