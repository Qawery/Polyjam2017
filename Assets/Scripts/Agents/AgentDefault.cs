using UnityEngine;
using UnityEngine.Assertions;

public enum Teams
{
    Player, Enemy, Neutral
}

public class AgentDefault : MonoBehaviour
{
    public Teams team;
    public GameObject highlight;

    public virtual void Awake()
    {
        Assert.IsNotNull(highlight, "Missing highlight");
        DeactivateHighlight();
    }

    public void ActivateHighlight()
    {
        highlight.SetActive(true);
    }

    public void DeactivateHighlight()
    {
        highlight.SetActive(false);
    }
}
