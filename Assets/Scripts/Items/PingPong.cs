using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : Item
{
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