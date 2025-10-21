using TMPro;
using UnityEngine;

public class PreTimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text mainText;
    public void UpdateTimer(int time)
    {
        if (time > 0)
        {
            mainText.text = $"Ready?\r\n{time:0}";
        }
        else
        {
            mainText.text = "Go!";
            Destroy(mainText.gameObject, 0.5f);
        }
    }
}
