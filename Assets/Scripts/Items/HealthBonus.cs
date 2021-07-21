using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonus : Item
{
    [Header("Descriptive attributes")]
    new string Name;
    new string Description;

    [Header("UI Sprite")]
    new Sprite Icon;

    [Header("Trail Settings")]
    new Material TrailMaterial;

    [Header("Ball Material Settings")]
    new Material PoweredUpMaterial;
    
    [Header("Physic Material Settings")]
    new Material CustomPhysicMaterial;

    public override void OnEquip()
    {
        Player.instance.Lives += 1;
    }
    
    public override void OnUnequip() {}

    public override void OnScoring() {}

    public override void OnDeath() {}

    public override void OnCollision() {}
}