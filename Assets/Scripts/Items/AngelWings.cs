using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelWings : Item
{
    public override void OnEquip()
    {
        foreach(Rigidbody ball in Field.instance.BallsInField)
        {
            // Resets speed
            ball.angularVelocity = Vector3.zero;
            ball.velocity = Vector3.zero;

            // Teleports balls to spawnpoint
            ball.transform.position = Field.instance.Spawnpoint.position;
        }
    }
    
    public override void OnUnequip() {}

    public override void OnScoring() {}

    public override void OnDeath() {}

    public override void OnCollision() {}
}