using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonus : Item
{
    public override void OnEquip()
    {
        Player.instance.Lives += 1;
    }
    
    public override void OnUnequip() {}

    public override void OnScoring()
    {
        if(Random.Range(0, 50) == 0)
            Player.instance.Lives += 1;
    }

    public override void OnDeath() {}

    public override void OnCollision() {}
}