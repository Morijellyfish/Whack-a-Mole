using UnityEngine;

public class HoleController : MonoBehaviour
{
    const int moles = 15;
    private MoleSchedule moleSchedule;

    private void Start()
    {
        moleSchedule = new MoleSchedule(moles, 10);
        for (int i = 0; i < moleSchedule.Moles.Length; i++)
        {
            Debug.Log($"Mole {i}: Hole {moleSchedule.Moles[i].Hole}, Time {moleSchedule.Moles[i].Time}");
        }
    }
}