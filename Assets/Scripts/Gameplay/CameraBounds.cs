using UnityEngine;
using UnityEngine.Assertions;

public class CameraBounds : MonoBehaviour
{
    public BoxCollider boundsCollider;
    private float northBound;
    private float southBound;
    private float eastBound;
    private float westBound;
    private float previousOrtographicSize;
    public void Start()
    {
        Assert.IsNotNull(boundsCollider, "Missing boundsCollider");
        previousOrtographicSize = GameplayManager.GetInstance().cameraControll.GetCamera().orthographicSize;
        RecalculateBounds();
    }

    private void OrtographicChange()
    {
        if(previousOrtographicSize != GameplayManager.GetInstance().cameraControll.GetCamera().orthographicSize)
        {
            previousOrtographicSize = GameplayManager.GetInstance().cameraControll.GetCamera().orthographicSize;
            RecalculateBounds();
        }
    }

    private void RecalculateBounds()
    {
        northBound = transform.position.z + ((boundsCollider.size.z * transform.localScale.z) / 2) - previousOrtographicSize;
        southBound = transform.position.z - ((boundsCollider.size.z * transform.localScale.z) / 2) + previousOrtographicSize;
        if (northBound < southBound)
        {
            northBound = transform.position.z;
            southBound = transform.position.z;
        }
        eastBound = transform.position.x + ((boundsCollider.size.x * transform.localScale.x) / 2) - previousOrtographicSize;
        westBound = transform.position.x - ((boundsCollider.size.x * transform.localScale.x) / 2) + previousOrtographicSize;
        if (eastBound < westBound)
        {
            eastBound = transform.position.x;
            westBound = transform.position.x;
        }
    }

    public float GetNorthBound()
    {
        OrtographicChange();
        return northBound;
    }

    public float GetSouthBound()
    {
        OrtographicChange();
        return southBound;
    }

    public float GetWestBound()
    {
        OrtographicChange();
        return westBound;
    }

    public float GetEastBound()
    {
        OrtographicChange();
        return eastBound;
    }
}
