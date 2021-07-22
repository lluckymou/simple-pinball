using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class Achievement : MonoBehaviour
{
    [Header("Descriptive fields")]
    public string Name;

    public string Description;

    [Header("State")]
    public bool Completed = false;

    [Header("Requirement")]
    public List<AchievementEnumeration> Constraints;

    public abstract Image Icon
    {
        get;
    }
}