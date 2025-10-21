using UnityEngine;
using Cysharp.Threading.Tasks;
using R3;
using System.Threading;
using System;

public class TimerController : MonoBehaviour
{
    private GameTime gameTime;
    private PreparationTime preparationTime;
    private CancellationTokenSource cts;

    [SerializeField] GameTimerUI gameTimerUI;
    [SerializeField] PreTimerUI preTimerUI;

    private void Start()
    {
        // èâä˙âª
        preparationTime = new PreparationTime();
        preparationTime.Reset();

        gameTime = new GameTime();
        gameTime.Reset();

        // UIçXêVÇÃèàóù
        gameTime.Time.Subscribe(t => gameTimerUI.UpdateTimer(t)).AddTo(this);
        preparationTime.Time.Subscribe(t => preTimerUI.UpdateTimer(t)).AddTo(this);

        // éûä‘êÿÇÍÇÃèàóù
        gameTime.IsTimeUp
            .Where(b => b)
            .Take(1)
            .Subscribe(_ => TimeUp());
        // ñàïbÇÃèàóù
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
