using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : Item
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