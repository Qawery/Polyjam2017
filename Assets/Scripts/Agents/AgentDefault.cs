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
    protected Health health;

    public virtual void Awake()
    {
        health = GetComponent<Health>();
        Assert.IsNotNull(health, "Missing health");
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

    public Health GetHealth()
    {
        return health;
    }

    public virtual bool IsAvailableToSelect()
    {
        return false;
    }
}
