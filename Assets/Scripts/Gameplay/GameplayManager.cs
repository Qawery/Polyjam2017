using UnityEngine;
using UnityEngine.Assertions;

public class GameplayManager : MonoBehaviour
{
    private static GameplayManager instance;
    public CameraControll cameraControll;
    public CameraBounds cameraBounds;

    private GameplayManager()
    {
    }

    public void Awake()
    {
        instance = this;
        Assert.IsNotNull(cameraControll, "Missing cameraControll.");
        Assert.IsNotNull(cameraBounds, "Missing cameraBounds");
    }

    public static GameplayManager GetInstance()
    {
        Assert.IsNotNull(instance, "Trying to access GameplayManager instance before initialization.");
        return instance;
    }
}
