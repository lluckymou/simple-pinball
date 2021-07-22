using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : Item
{
    public override void OnEquip()
    {
        Movement.instance.TiltChance = 20;
    }
    
    public override void OnUnequip()
    {
        Movement.instance.TiltChance = 5;
    }

    public override void OnScoring() {}

    public override void OnDeath() {}

    public override void OnCollision() {}
}