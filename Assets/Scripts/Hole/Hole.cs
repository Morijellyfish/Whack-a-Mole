using R3;
using R3.Triggers;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] Mole mole;

    private void Start()
    {
        mole.gameObject.OnMouseDownAsObservable()
            .Where(_ => mole.enabled)
            .Subscribe(_ => TapMole())
            .AddTo(this);
    }

    public void SpawnMole()
    {
        mole.Spawn();
    }

    public void TapMole()
    {
        mole.Tap();
    }
}