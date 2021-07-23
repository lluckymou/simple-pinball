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

    public AchievementRarity Rarity;

    public abstract Image Icon { get; }

    public bool CanBeCompleted
    {
        get
        {
            if(Constraints.Count < 1) return true;

            foreach(AchievementEnumeration constraint in Constraints)
                if(!Achievements.GetAchievementFromEnumeration(constraint).Completed)
                    return false;
            
            return true;
        }
    }

    public bool IsVisible
    {
        get => CanBeCompleted;
    }
}