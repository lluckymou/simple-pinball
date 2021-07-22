using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyCharm : Item
{
    public override void OnEquip() {}
    
    public override void OnUnequip() {}

    public override void OnScoring() {}

    public override void OnDeath() {}

    public override void OnCollision()
    {
        Player.instance.Multiplier = Random.Range(0.01f, 3.6f);
    }
}