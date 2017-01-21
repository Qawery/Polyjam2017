using UnityEngine;
using System.Collections.Generic;

public enum UnitOrders
{
    None, Move, Attack, Patrol
}

public class InputManager : MonoBehaviour
{
    public List<AgentAI> selectedUnits;
    public UnitOrders unitOrder;

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
            if (Input.GetKeyDown(KeyCode.Z))
            {
                unitOrder = UnitOrders.Move;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                unitOrder = UnitOrders.Attack;
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                StopUnits();
                unitOrder = UnitOrders.None;
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                unitOrder = UnitOrders.Patrol;
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
                    DeselectAll();
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
            if (unitOrder == UnitOrders.None)
            {
                TryToSelect(rayResult);
            }
            else
            {
                //TODO kolejkowanie rozkazów
            }
        }
    }

    private void RightMouseButtonOnly()
    {
        KeyValuePair<bool, RaycastHit> rayResult = ShootRayFromCamera();
        if (rayResult.Key)
        {
            //TODO Wykonanie rozkazu domyślnego
        }
    }

    private void RightMouseButtonShift()
    {
        KeyValuePair<bool, RaycastHit> rayResult = ShootRayFromCamera();
        if (rayResult.Key)
        {
            //TODO kolejkowanie rozkazu domyślnego
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

    private void StopUnits()
    {
        foreach (AgentAI agent in selectedUnits)
        {
            agent.Idle();
        }
    }
}
