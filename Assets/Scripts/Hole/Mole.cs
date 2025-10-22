using UnityEngine;

public class Mole : MonoBehaviour
{
    public void Spawn()
    {
        // 考慮する必要アリ
        if (gameObject.activeSelf)
        {
            Debug.LogWarning("Mole is already active.");
        }
        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    public void Tap()
    {
        gameObject.SetActive(false);
    }
}
