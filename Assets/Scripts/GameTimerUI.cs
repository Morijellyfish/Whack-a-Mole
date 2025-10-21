using TMPro;
using UnityEngine;

public class GameTimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    public void UpdateTimer(int time)
    {
        timerText.text = $"Time: {time:00}";
    }
}
