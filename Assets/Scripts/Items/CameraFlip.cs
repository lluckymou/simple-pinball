using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : Item
{
    public override void OnEquip()
    {
        SceneObjects.instance.MainCamera.transform.rotation = Quaternion.Euler(45, 0, 180);
        Player.instance.Multiplier += 1.5f;
    }
    
    public override void OnUnequip()
    {
        SceneObjects.instance.MainCamera.transform.rotation = Quaternion.Euler(45, 0, 0);
    }

    public override void OnScoring() {}

    public override void OnDeath() {}

    public override void OnCollision() {}
}