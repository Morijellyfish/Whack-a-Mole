using UnityEngine;

public class Mole : MonoBehaviour
{
    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    // 出現時間終了時処理
    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    // タップ時処理
    public void Tap()
    {
        gameObject.SetActive(false);
    }
}
