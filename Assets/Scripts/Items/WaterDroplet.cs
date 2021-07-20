using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDroplet : Item
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

    public override void OnEquip() {}
    
    public override void OnUnequip() {}

    public override void OnScoring() {}

    public override void OnDeath() {}
}