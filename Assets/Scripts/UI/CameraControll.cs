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
        if (newPosition.x > GameplayManager.getInstance().cameraBounds.getEastBound())
        {
            newPosition.x = GameplayManager.getInstance().cameraBounds.getEastBound();
        }
        else if (newPosition.x < GameplayManager.getInstance().cameraBounds.getWestBound())
        {
            newPosition.x = GameplayManager.getInstance().cameraBounds.getWestBound();
        }
        if (newPosition.z > GameplayManager.getInstance().cameraBounds.getNorthBound())
        {
            newPosition.z = GameplayManager.getInstance().cameraBounds.getNorthBound();
        }
        else if (newPosition.z < GameplayManager.getInstance().cameraBounds.getSouthBound())
        {
            newPosition.z = GameplayManager.getInstance().cameraBounds.getSouthBound();
        }
        transform.position = newPosition;
    }

    public Camera getCamera()
    {
        return cameraComponent;
    }
}
