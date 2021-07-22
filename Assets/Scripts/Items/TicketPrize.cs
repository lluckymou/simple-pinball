using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketPrize : Item
{
    public override void OnEquip()
    {
        Player.instance.Tickets += Random.Range(3, 6);
    }
    
    public override void OnUnequip() {}

    public override void OnScoring()
    {
        if(Random.Range(0, 10) == 0)
            Player.instance.Tickets += 1;
    }

    public override void OnDeath() {}

    public override void OnCollision() {}
}