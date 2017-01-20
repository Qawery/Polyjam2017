using UnityEngine;
using System.Collections.Generic;

public enum UnitOrders
{
    None, Move, Attack, Patrol
}

public class InputManager : MonoBehaviour
{
    private float overlapRadius = 0.1f;
    private List<AgentAI> selectedUnits;
    private UnitOrders unitOrder;

    public void Awake()
    {
        selectedUnits = new List<AgentAI>();
        unitOrder = UnitOrders.None;
    }

    public void Update()
    {
        DetermineUnitOrder();
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

    public void DetermineUnitOrder()
    {
        if (selectedUnits.Count > 0)
        {
            if (Input.GetKeyDown("Z"))
            {
                //move
            }
            else if (Input.GetKeyDown("X"))
            {
                //attack
            }
            else if (Input.GetKeyDown("C"))
            {
                //stop
            }
            else if (Input.GetKeyDown("V"))
            {
                //patrol
            }
        }
        else
        {
            unitOrder = UnitOrders.None;
        }
    }

    private void LeftMouseButtonOnly()
    {
        KeyValuePair<bool, RaycastHit> rayResult = ShootRayFromCamera();
        if (rayResult.Key)
        {
            print("LMB hit");
            if(selectedUnits.Count > 0)
            {
                if(unitOrder == UnitOrders.None)
                {
                    DeselectAll();
                    TryToSelect(rayResult);
                }
                 else
                {
                    ExecuteOrder(rayResult);
                }
            }
            else
            {
                TryToSelect(rayResult);
            }
        }
    }

    private void LeftMouseButtonShift()
    {
        KeyValuePair<bool, RaycastHit> rayResult = ShootRayFromCamera();
        if (rayResult.Key)
        {
            print("LMB_S hit");
            /**/
        }
        else
        {
            print("LMB_S miss");
            /**/
        }
    }

    private void RightMouseButtonOnly()
    {
        KeyValuePair<bool, RaycastHit> rayResult = ShootRayFromCamera();
        if (rayResult.Key)
        {
            print("RMB hit");
            /**/
        }
        else
        {
            print("RMB miss");
            /**/
        }
    }

    private void RightMouseButtonShift()
    {
        KeyValuePair<bool, RaycastHit> rayResult = ShootRayFromCamera();
        if (rayResult.Key)
        {
            print("RMB_S hit");
            /**/
        }
        else
        {
            print("RMB_S miss");
            /**/
        }
    }

    private KeyValuePair<bool, RaycastHit> ShootRayFromCamera()
    {
        Camera currentCamera = GameplayManager.GetInstance().cameraControll.GetCamera();
        RaycastHit hit;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        return new KeyValuePair<bool, RaycastHit>(Physics.Raycast(ray, out hit), hit);
    }

    private void ActivateHighlightOfSelected()
    {
        foreach (AgentDefault agent in selectedUnits)
        {
            agent.ActivateHighlight();
        }
    }

    private void DeselectAll()
    {
        DeactivateHighlightOfSelected();
        selectedUnits.Clear();
    }

    private void DeactivateHighlightOfSelected()
    {
        foreach (AgentDefault agent in selectedUnits)
        {
            agent.DeactivateHighlight();
        }
    }

    private bool TryToSelect(KeyValuePair<bool, RaycastHit> rayResult)
    {
        if (rayResult.Value.collider.GetComponent<AgentAI>() != null && rayResult.Value.collider.GetComponent<AgentAI>().team == Teams.Player)
        {
            selectedUnits.Add(rayResult.Value.collider.GetComponent<AgentAI>());
            rayResult.Value.collider.GetComponent<AgentAI>().ActivateHighlight();
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool ExecuteOrder(KeyValuePair<bool, RaycastHit> rayResult)
    {
        //TODO wykonanie bieżącego rozkazu w kontekscie reyResult
        switch (unitOrder)
        {
            case UnitOrders.Move:
                MoveAllUnits(rayResult.Value.point);
                break;

            case UnitOrders.Attack:
                break;

            case UnitOrders.Patrol:
                break;
        }
        return false;
    }

    private void MoveAllUnits(Vector3 newDestination)
    {
        foreach(AgentAI agent in selectedUnits)
        {
            agent.GoToDestination(newDestination);
        }
    }
}
