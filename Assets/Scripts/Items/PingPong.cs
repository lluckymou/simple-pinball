using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : Item
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
        Movement.instance.FlipperMotorVelocity = 2500;
        Movement.instance.FlipperMotorForce = 250;
    }
    
    public override void OnUnequip()
    {
        Movement.instance.FlipperMotorVelocity = 1500;
        Movement.instance.FlipperMotorForce = 150;
    }

    public override void OnScoring() {}

    public override void OnDeath() {}

    public override void OnCollision() {}
}