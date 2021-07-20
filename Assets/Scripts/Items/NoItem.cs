using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoItem : Item
{
    [Header("Descriptive attributes")]
    new string Name;
    new string Description;

    public override void OnEquip() {}
    
    public override void OnUnequip() {}

    public override void OnScoring() {}

    public override void OnDeath() {}

    public override void OnCollision() {}
}