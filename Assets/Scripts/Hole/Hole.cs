using UnityEngine;

public class Hole : MonoBehaviour
{
    public void SpawnMole()
    {
        Debug.Log("Mole spawned in hole: " + gameObject.name);
    }
}