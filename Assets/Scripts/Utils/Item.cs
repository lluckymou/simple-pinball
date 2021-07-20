using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string Name;

    public string Description;

    public Sprite Icon;

    public bool HasTrail
    {
        get => (TrailMaterial != null);
    }

    public Material TrailMaterial;

    public bool ChangeBallMaterial
    {
        get => (PoweredUpMaterial != null);
    }

    public Material PoweredUpMaterial;

    public abstract void OnEquip();
    
    public abstract void OnUnequip();

    public abstract void OnScoring();

    public abstract void OnDeath();
}