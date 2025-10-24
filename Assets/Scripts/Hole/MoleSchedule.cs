using UnityEngine;

public class MoleSchedule
{
    public MoleData[] Moles;
    private int holeCount;

    public MoleSchedule(int moleCount, int holeCount)
    {
        Moles = new MoleData[moleCount];
        this.holeCount = holeCount;
        SetMolesNew();
        SetMolesAppearanceTime();
        SetMolesDuration();
        SetMolesHole();
    }

    private void SetMolesNew()
    {
        for (int i = 0; i < Moles.Length; i++)
        {
            Moles[i] = new MoleData();
        }
    }

    private void SetMolesAppearanceTime()
    {
        float baseTime = ((float)TimerController.GameDuration - 0.8f) / Moles.Length;
        for (int i = 0; i < Moles.Length; i++)
        {
            float r = Random.Range(-baseTime / 2, baseTime / 2);
            Moles[i].AppearanceTime = Mathf.Max((baseTime * i) + r, 0);
        }
    }

    private void SetMolesDuration()
    {
        for (int i = 0; i < Moles.Length; i++)
        {
            Moles[i].Duration = Random.Range(0.8f, 1.5f);
        }
    }

    private void SetMolesHole()
    {
        float margin = 1.5f;
        float[] holesTime = new float[holeCount];
        for (int i = 0; i < holeCount; i++)
        {
            holesTime[i] = -1; // 初期化
        }

        foreach (var mole in Moles)
        {
            mole.Hole = Random.Range(0, holeCount);
            int attempts = 10; // 最悪のケース防止の回数制限
            while (attempts > 0 && (holesTime[mole.Hole] >= mole.AppearanceTime))
            {
                // 他の穴を探す
                int originalHole = mole.Hole;
                mole.Hole = Random.Range(0, holeCount);
                attempts--;
            }
            holesTime[mole.Hole] = mole.AppearanceTime + mole.Duration + margin;
        }
    }

}
