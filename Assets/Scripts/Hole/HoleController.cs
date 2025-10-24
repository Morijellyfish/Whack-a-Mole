using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    const int moles = 15;
    private MoleSchedule moleSchedule;

    [SerializeField] Holes holes;

    private void Start()
    {
        moleSchedule = new MoleSchedule(moles, 10);

        // 準備時間終了でモグラ出現開始
        GetComponent<TimerController>().IsPreparationFinished
            .Where(b => b)
            .Take(1)
            .Subscribe(_ => ScheduleSpawn())
            .AddTo(this);
    }

    private void ScheduleSpawn()
    {
        for (int i = 0; i < moleSchedule.Moles.Length; i++)
        {
            SpawnMole(moleSchedule.Moles[i]).Forget();
        }

        async UniTaskVoid SpawnMole(MoleData mole)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(mole.AppearanceTime));
            holes.SpawnMole(mole.Hole, mole.Duration);
        }
    }
}