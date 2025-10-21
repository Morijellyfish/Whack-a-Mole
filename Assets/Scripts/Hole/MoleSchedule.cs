using UnityEngine;

public class MoleSchedule
{
    public Mole[] Moles;
    private int holeCount;

    public MoleSchedule(int moleCount, int holeCount)
    {
        Moles = new Mole[moleCount];
        this.holeCount = holeCount;
        SetMolesNew();
        SetMolesTime();
        SetMolesHole();
    }

    private void SetMolesNew()
    {
        for (int i = 0; i < Moles.Length; i++)
        {
            Moles[i] = new Mole();
        }
    }

    private void SetMolesTime()
    {
        float baseTime = ((float)TimerController.GameDuration - 0.8f) / Moles.Length;
        for (int i = 0; i < Moles.Length; i++)
        {
            float r = Random.Range(-baseTime / 2, baseTime / 2);
            Moles[i].Time = Mathf.Max((baseTime * i) + r, 0);
        }
    }

    private void SetMolesHole()
    {
        for (int i = 0; i < Moles.Length; i++)
        {
            Moles[i].Hole = Random.Range(0, holeCount);
            //一つ前のモグラと同じ穴かつ時間が近い場合、再度穴を決定する
            if (i > 0 &&
                Moles[i].Hole == Moles[i - 1].Hole &&
                Mathf.Abs(Moles[i].Time - Moles[i - 1].Time) < 0.5f)
            {
                Moles[i].Hole = RandomExcept(holeCount, Moles[i - 1].Hole, 10);
            }
        }

        int RandomExcept(int max, int forbidden, int attemptsLeft = 10)
        {
            if (attemptsLeft <= 0) //最悪のケース
            {
                return 0;
            }
            int r = Random.Range(0, max);
            return r == forbidden ? RandomExcept(max, forbidden, attemptsLeft - 1) : r;
        }
    }
}
