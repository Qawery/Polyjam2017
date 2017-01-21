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
                SetToMoveOrder();
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                SetToAttackOrder();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                SetToStopOrder();
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                SetToPatrolOrder();
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
                    unitOrder = UnitOrders.None;
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
        //TODO sprawdzanie
        if (rayResult.Value.collider.GetComponent<AgentAI>() != null && rayResult.Value.collider.GetComponent<AgentAI>().IsAvailableToSelect())
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
                //TODO rozproszenie rozdzialu
                MoveAllUnits(rayResult.Value.point);
                break;

            case UnitOrders.Attack:
                //TODO atak
                break;

            case UnitOrders.Patrol:
                //TODO patrol
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

    public void SetToMoveOrder()
    {
        unitOrder = UnitOrders.Move;
    }

    public void SetToAttackOrder()
    {
        unitOrder = UnitOrders.Attack;
    }

    public void SetToStopOrder()
    {
        StopUnits();
        unitOrder = UnitOrders.None;
    }

    public void SetToPatrolOrder()
    {
        unitOrder = UnitOrders.Patrol;
    }
}
