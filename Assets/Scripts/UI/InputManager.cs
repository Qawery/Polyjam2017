using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public enum UnitOrders
{
    None, Move, Attack, Patrol, DragSelection
}

public class InputManager : MonoBehaviour
{
    public List<AgentAI> selectedUnits;
    public UnitOrders unitOrder;
    private Vector3 LMB_DownPosition;
    private Vector3 LMB_UpPosition;
    private Vector3 currentCursorPosition;

    public void Awake()
    {
        selectedUnits = new List<AgentAI>();
        unitOrder = UnitOrders.None;
    }

    public void Update()
    {
        CleanUpCorpses();
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            //przerwij drag selection
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetMouseButtonUp(0))
            {
                //zrealizuj drag selection
                LMB_UpPosition = GameplayManager.GetInstance().cameraControll.GetCamera().ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                //rozpocznij drag slection
                LMB_DownPosition = GameplayManager.GetInstance().cameraControll.GetCamera().ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                currentCursorPosition = GameplayManager.GetInstance().cameraControll.GetCamera().ScreenToWorldPoint(Input.mousePosition);
                //TODO: narysowanie prostokąta
            }
        }
        else
        {
            DetermineUnitOrder();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    LeftMouseButtonShift();
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    RightMouseButtonShift();
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    LeftMouseButtonOnly();
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    RightMouseButtonOnly();
                }
            }
        }
    }

    private void CleanUpCorpses()
    {
        int i = 0;
        while (i < selectedUnits.Count)
        {
            if (selectedUnits[i] == null)
            {
                selectedUnits.RemoveAt(i);
            }
            else
            {
                i++;
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
                //TODO selection
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
        switch (unitOrder)
        {
            case UnitOrders.Move:
                //TODO rozproszenie rozdzialu
                MoveAllUnits(rayResult.Value.point);
                break;

            case UnitOrders.Attack:
                AttackAllUnits(rayResult.Value);
                break;

            case UnitOrders.Patrol:
                PatrolAllUnits(rayResult.Value.point);
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

    private void AttackAllUnits(RaycastHit result)
    {
        if (result.collider.GetComponent<AgentDefault>() != null && result.collider.GetComponent<AgentDefault>().team != Teams.Player)
        {
            foreach (AgentAI agent in selectedUnits)
            {
                agent.AttackTarget(result.collider.GetComponent<AgentDefault>());
            }
        }
        else
        {
            foreach (AgentAI agent in selectedUnits)
            {
                agent.AttackMove(result.point);
            }
        }
    }

    private void PatrolAllUnits(Vector3 newDestination)
    {
        foreach (AgentAI agent in selectedUnits)
        {
            agent.Patrol(newDestination);
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
        CancelDragSelection();
        unitOrder = UnitOrders.Move;
    }

    public void SetToAttackOrder()
    {
        CancelDragSelection();
        unitOrder = UnitOrders.Attack;
    }

    public void SetToStopOrder()
    {
        CancelDragSelection();
        StopUnits();
        unitOrder = UnitOrders.None;
    }

    public void SetToPatrolOrder()
    {
        CancelDragSelection();
        unitOrder = UnitOrders.Patrol;
    }

    private void CancelDragSelection()
    {
        //Cancels drag selection
    }
}
