using UnityEngine;

public class Holes : MonoBehaviour
{
    [SerializeField] private Hole[] holes;

    public void SpawnMole(int holeIndex)
    {
        holes[holeIndex].SpawnMole();
    }
}
