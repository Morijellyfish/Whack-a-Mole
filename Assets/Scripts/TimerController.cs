using UnityEngine;
using Cysharp.Threading.Tasks;
using R3;
using System.Threading;

public class TimerController : MonoBehaviour
{
    private GameTime gameTime;
    private PreparationTime preparationTime;
    private CancellationTokenSource cts;

    private void Start()
    {
        preparationTime = new PreparationTime();
        preparationTime.Reset();

        gameTime = new GameTime();
        gameTime.Reset();

        //éûä‘êÿÇÍÇÃèàóù
        gameTime.IsTimeUp
            .Where(b => b)
            .Take(1)
            .Subscribe(_ => TimeUp());
        //ñàïbÇÃèàóù
        StartGameTimer();
    }

    public void StartGameTimer()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());
        RunTimerAsync(cts.Token).Forget();
    }

    private async UniTaskVoid RunTimerAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await UniTask.Delay(1000, cancellationToken: ct);
            if (preparationTime.IsFinished.CurrentValue)
            {
                gameTime.Tick();
                Debug.Log(gameTime.Time);
            }
            else
            {
                preparationTime.Tick();
                Debug.Log(preparationTime.Time);
            }
        }
    }

    private void TimeUp()
    {
        Debug.Log("Time up!");
        cts.Cancel();
    }

    private void OnDestroy()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = null;
    }
}
