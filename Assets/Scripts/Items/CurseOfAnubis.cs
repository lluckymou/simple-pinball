using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseOfAnubis : Item
{
    public override void OnEquip() 
    {
        Player.instance.Lives = 0;
        Player.instance.Score /= 2;
        Player.instance.Multiplier += 2.5f;

        SceneObjects.instance.MainLight.intensity = 0.25f;
    }
    
    public override void OnUnequip()
    {
        SceneObjects.instance.MainLight.intensity = 0.8f;
    }

    public override void OnScoring() {}

    public override void OnDeath() {}

    public override void OnCollision() {}
}