using UnityEngine;

public enum BatchDirection
{
    North, South, West, East
}
public class BatchInfo
{
    public GameObject unitType;
    public int remainingNumber;
    public Vector3 destination;             //By nie blokować spawnu
    public SquadAI squadAI;                 //Na potrzeby przeciwników
    public BatchDirection batchDirection;   //Na potrzeby fal

    public BatchInfo(GameObject unitTypeOrdered, int numberOrdered, Vector3 newDestination)
    {
        unitType = unitTypeOrdered;
        remainingNumber = numberOrdered;
        destination = newDestination;
        squadAI = null;
        batchDirection = BatchDirection.North;
    }
}