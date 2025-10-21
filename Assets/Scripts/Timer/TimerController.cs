using UnityEngine;
using Cysharp.Threading.Tasks;
using R3;
using System.Threading;

public class TimerController : MonoBehaviour
{
    public const int GameDuration = 10;

    private GameTime gameTime;
    private PreparationTime preparationTime;
    private CancellationTokenSource cts;

    [SerializeField] GameTimerUI gameTimerUI;
    [SerializeField] PreTimerUI preTimerUI;

    private void Start()
    {
        // 初期化
        preparationTime = new PreparationTime();
        preparationTime.Reset();

        gameTime = new GameTime(GameDuration);
        gameTime.Reset();

        // UI更新の処理
        gameTime.Time.Subscribe(t => gameTimerUI.UpdateTimer(t)).AddTo(this);
        preparationTime.Time.Subscribe(t => preTimerUI.UpdateTimer(t)).AddTo(this);

        // 時間切れの処理
        gameTime.IsTimeUp
            .Where(b => b)
            .Take(1)
            .Subscribe(_ => TimeUp());
        // 毎秒の処理
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
