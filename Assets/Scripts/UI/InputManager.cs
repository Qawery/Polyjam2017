using UnityEngine;

public class InputManager : MonoBehaviour
{
    //DEBUG
    public AgentAI agent;

    public void Update()
    {
        MouseControll();
    }

    private void MouseControll()
    {
        //TODO blokada by nie wysyłać rozkazów przez GUI, na razie jet blokada wysokości kursora
        if (Input.GetButtonDown("Fire1") && Input.mousePosition.y > 10)
        {
            Camera currentCamera = GameplayManager.GetInstance().cameraControll.GetCamera();
            RaycastHit hit;
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //TODO mamy punkt na terenie
                agent.SetDestination(hit.point);
            }
        }
    }
}
