using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameplayManager : MonoBehaviour
{
    private static GameplayManager instance;
    public CameraControll cameraControll;
    public CameraBounds cameraBounds;
    public GUI gui;
    public InputManager inputManager;
    public Scenario scenario;
    public List<AgentAI> allPlayerAgents;
    public List<TowerAI> towerList;
    public bool isControllEnabled;

    private GameplayManager()
    {
    }

    public void Awake()
    {
        instance = this;
        isControllEnabled = true;
        Assert.IsNotNull(cameraControll, "Missing cameraControll.");
        Assert.IsNotNull(cameraBounds, "Missing cameraBounds");
        Assert.IsNotNull(gui, "Missing GUI");
        inputManager = GetComponent<InputManager>();
        Assert.IsNotNull(inputManager, "Missing inputManager");
        scenario = GetComponent<Scenario>();
        Assert.IsNotNull(scenario, "Missing scenario");
        allPlayerAgents = new List<AgentAI>();
        towerList = new List<TowerAI>();
    }

    public static GameplayManager GetInstance()
    {
        Assert.IsNotNull(instance, "Trying to access GameplayManager instance before initialization.");
        return instance;
    }
}
