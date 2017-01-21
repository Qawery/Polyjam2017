﻿using UnityEngine;
using UnityEngine.Assertions;

public class GameplayManager : MonoBehaviour
{
    private static GameplayManager instance;
    public CameraControll cameraControll;
    public CameraBounds cameraBounds;
    public GUI gui;
    public InputManager inputManager;
    public Scenario scenario;

    private GameplayManager()
    {
    }

    public void Awake()
    {
        instance = this;
        Assert.IsNotNull(cameraControll, "Missing cameraControll.");
        Assert.IsNotNull(cameraBounds, "Missing cameraBounds");
        Assert.IsNotNull(gui, "Missing GUI");
        inputManager = GetComponent<InputManager>();
        Assert.IsNotNull(inputManager, "Missing inputManager");
        scenario = GetComponent<Scenario>();
        Assert.IsNotNull(scenario, "Missing scenario");
    }

    public static GameplayManager GetInstance()
    {
        Assert.IsNotNull(instance, "Trying to access GameplayManager instance before initialization.");
        return instance;
    }
}
