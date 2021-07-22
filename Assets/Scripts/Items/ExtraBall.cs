using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBall : Item
{
    public override void OnEquip()
    {
        Player.instance.SpawnBall();
    }
    
    public override void OnUnequip() {}

    public override void OnScoring() {}

    public override void OnDeath() {}

    public override void OnCollision() {}
}