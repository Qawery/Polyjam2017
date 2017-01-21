using UnityEngine;
using UnityEngine.Assertions;

public class CameraControll : MonoBehaviour
{
    public float movementSpeed = 1.5f;
    public float minOrtographicSize = 4f;
    public float maxOrtographicSize = 20f;

    private Camera cameraComponent;

    public void Awake()
    {
        cameraComponent = GetComponent<Camera>();
        Assert.IsNotNull(cameraComponent, "Missing cameraComponent");
    }

    public void LateUpdate()
    {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);

        Vector3 newPosition = new Vector3(transform.position.x + movementVector.x, transform.position.y, transform.position.z + movementVector.y);
        if (newPosition.x > GameplayManager.GetInstance().cameraBounds.GetEastBound())
        {
            newPosition.x = GameplayManager.GetInstance().cameraBounds.GetEastBound();
        }
        else if (newPosition.x < GameplayManager.GetInstance().cameraBounds.GetWestBound())
        {
            newPosition.x = GameplayManager.GetInstance().cameraBounds.GetWestBound();
        }
        if (newPosition.z > GameplayManager.GetInstance().cameraBounds.GetNorthBound())
        {
            newPosition.z = GameplayManager.GetInstance().cameraBounds.GetNorthBound();
        }
        else if (newPosition.z < GameplayManager.GetInstance().cameraBounds.GetSouthBound())
        {
            newPosition.z = GameplayManager.GetInstance().cameraBounds.GetSouthBound();
        }
        transform.position = newPosition;
    }

    public Camera GetCamera()
    {
        return cameraComponent;
    }

    public void ChangeOrtographicSize(float ammount)
    {
        cameraComponent.orthographicSize += ammount;
        if(cameraComponent.orthographicSize > maxOrtographicSize)
        {
            cameraComponent.orthographicSize = maxOrtographicSize;
        }
        if(cameraComponent.orthographicSize < minOrtographicSize)
        {
            cameraComponent.orthographicSize = minOrtographicSize;
        }
    }
}
