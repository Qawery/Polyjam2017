using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class GUI : MonoBehaviour
{
    public Slider powerSlider;
    public Text victoryText;
    public Text defeatText;
    public Button moveButton;
    public Button attackButton;
    public Button stopButton;
    public Button patrolButton;
    public float selectedOffset = 20f;
    private float originalY;
    private List<Button> allButtons;
    
    public void Awake()
    {
        Assert.IsNotNull(victoryText, "Missing victoryText");
        victoryText.enabled = false;
        Assert.IsNotNull(defeatText, "Missing defeatText");
        defeatText.enabled = false;
        Assert.IsNotNull(powerSlider, "Missing powerSlider");
        allButtons = new List<Button>();
        Assert.IsNotNull(moveButton, "Missing moveButton");
        allButtons.Add(moveButton);
        Assert.IsNotNull(attackButton, "Missing attackButton");
        allButtons.Add(attackButton);
        Assert.IsNotNull(stopButton, "Missing stopButton");
        allButtons.Add(stopButton);
        Assert.IsNotNull(patrolButton, "Missing patrolButton");
        allButtons.Add(patrolButton);
        originalY = patrolButton.transform.position.y;
        HideAll();
    }

    public void Start()
    {
        powerSlider.maxValue = GameplayManager.GetInstance().scenario.maxResources;
        powerSlider.minValue = 0;
    }

    public void Update()
    {
        powerSlider.value = GameplayManager.GetInstance().scenario.resourcesCounter;
        if (GameplayManager.GetInstance().inputManager.selectedUnits.Count > 0)
        {
            ShowAll();
            UnselectAll();
            switch (GameplayManager.GetInstance().inputManager.unitOrder)
            {
                case UnitOrders.None:
                    break;

                case UnitOrders.Move:
                    SelectButton(moveButton);
                    break;

                case UnitOrders.Attack:
                    SelectButton(attackButton);
                    break;

                case UnitOrders.Patrol:
                    SelectButton(patrolButton);
                    break;
            }
        }
        else
        {
            HideAll();
        }
    }

    private void HideAll()
    {
        foreach(Button button in allButtons)
        {
            button.enabled = false;
            button.GetComponent<Image>().enabled = false;
        }
    }

    private void ShowAll()
    {
        foreach (Button button in allButtons)
        {
            button.enabled = true;
            button.GetComponent<Image>().enabled = true;
        }
    }

    private void UnselectAll()
    {
        foreach (Button button in allButtons)
        {
            UnselectButton(button);
        }
    }

    private void SelectButton(Button button)
    {
        button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y + selectedOffset, button.transform.position.z);
    }

    private void UnselectButton(Button button)
    {
        button.transform.position = new Vector3(button.transform.position.x, originalY, button.transform.position.z);
    }

    public void ShowEndingText(bool isVictory)
    {
        if(isVictory)
        {
            victoryText.enabled = true;
        }
        else
        {
            defeatText.enabled = true;
        }
    }
}