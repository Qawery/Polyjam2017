using UnityEngine;
using UnityEngine.Assertions;

public class CameraControll : MonoBehaviour
{
    private Camera cameraComponent;
    public float movementSpeed = 0.5f;

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
}
