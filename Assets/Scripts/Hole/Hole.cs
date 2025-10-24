using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using System.Threading;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] Mole mole;

    private CancellationTokenSource despawnCts;

    private void Start()
    {
        mole.gameObject.OnMouseDownAsObservable()
            .Where(_ => mole.enabled)
            .Subscribe(_ => TapMole())
            .AddTo(this);
    }

    public void SpawnMole(float duration)
    {
        despawnCts?.Cancel();
        despawnCts?.Dispose();
        mole.Spawn();
        despawnCts = new();
        DespawnMole(duration).Forget();
    }

    public async UniTaskVoid DespawnMole(float duration)
    {
        await UniTask.Delay((int)(duration * 1000), cancellationToken: despawnCts.Token);
        mole.Despawn();
    }

    public void TapMole()
    {
        ScoreController.Instance.AddScore();
        mole.Tap();
    }
}