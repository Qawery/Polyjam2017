using UnityEngine;

public class BatchInfo
{
    public GameObject unitType;
    public int remainingNumber;
    public Vector3 destination;
    public SquadAI squadAI;

    public BatchInfo(GameObject unitTypeOrdered, int numberOrdered, Vector3 newDestination)
    {
        unitType = unitTypeOrdered;
        remainingNumber = numberOrdered;
        destination = newDestination;
        squadAI = null;
    }
}