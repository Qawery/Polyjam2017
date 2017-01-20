using UnityEngine;
using System.Collections.Generic;

public enum ActiveOrder
{
    None, Move, Attack, Patrol
}

public class InputManager : MonoBehaviour
{
    private float overlapRadius = 0.1f;
    private ActiveOrder order;
    private List<AgentDefault> selectedAgents;

    public void Awake()
    {
        selectedAgents = new List<AgentDefault>();
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetMouseButtonDown(0))
            {
                LeftMouseButtonShift();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RightMouseButtonShift();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                LeftMouseButtonOnly();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RightMouseButtonOnly();
            }
        }
    }

    private void LeftMouseButtonOnly()
    {
        KeyValuePair<bool, RaycastHit> rayResult = ShootRayFromCamera();
        if (rayResult.Key)
        {
            print("NOPE LMB hit");
        }
        else
        {
            print("NOPE LMB miss");
        }
    }

    private void LeftMouseButtonShift()
    {
        KeyValuePair<bool, RaycastHit> rayResult = ShootRayFromCamera();
        if (rayResult.Key)
        {
            print("NOPE LMB_S hit");
        }
        else
        {
            print("NOPE LMB_S miss");
        }
    }

    private void RightMouseButtonOnly()
    {
        KeyValuePair<bool, RaycastHit> rayResult = ShootRayFromCamera();
        if (rayResult.Key)
        {
            print("NOPE RMB hit");
        }
        else
        {
            print("NOPE RMB miss");
        }
    }

    private void RightMouseButtonShift()
    {
        KeyValuePair<bool, RaycastHit> rayResult = ShootRayFromCamera();
        if (rayResult.Key)
        {
            print("NOPE RMB_S hit");
        }
        else
        {
            print("NOPE RMB_S miss");
        }
    }

    private KeyValuePair<bool, RaycastHit> ShootRayFromCamera()
    {
        Camera currentCamera = GameplayManager.GetInstance().cameraControll.GetCamera();
        RaycastHit hit;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        return new KeyValuePair<bool, RaycastHit>(Physics.Raycast(ray, out hit), hit);
    }
}
