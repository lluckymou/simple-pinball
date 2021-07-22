using UnityEngine;

public abstract class Item : MonoBehaviour
{

    [Header("Descriptive attributes")]
    public string Name;

    public string Description;

    [Header("UI Sprite")]
    public Sprite Icon;

    public bool HasTrail
    {
        get => (TrailMaterial != null);
    }

    [Header("Trail Settings")]
    public Material TrailMaterial;

    public bool HasCustomPhysicMaterial
    {
        get => (CustomPhysicMaterial != null);
    }

    [Header("Physic Material Settings")]
    public PhysicMaterial CustomPhysicMaterial;

    public bool ChangeBallMaterial
    {
        get => (PoweredUpMaterial != null);
    }

    [Header("Ball Material Settings")]
    public Material PoweredUpMaterial;

    public abstract void OnEquip();
    
    public abstract void OnUnequip();

    public abstract void OnScoring();

    public abstract void OnDeath();

    public abstract void OnCollision();
}