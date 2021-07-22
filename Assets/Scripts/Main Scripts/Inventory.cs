using UnityEngine;
using System.Collections.Generic;

public static class Inventory
{
    public static Item[] Slots = new Item[3] { Items.NoItem, Items.NoItem, Items.NoItem };
    public static Item Equipped = Items.NoItem;
    public static Item NextItem
    {
        get => Slots[0];
    }

    public static void GiveItem(Item item)
    {
        // Checks where to add the item (from the first to the last slot)
        int position = -1;
        for (int i = 0; i < Slots.Length; i++)
            if(Slots[i] == Items.NoItem)
            {
                position = i;
                break;
            }
        
        if(position == -1)
        {
            ItemGUI.instance.PurchaseFailSound();
            return;
        }

        Slots[position] = item;

        // Updates item UI
        ItemGUI.instance.LoadItems();
    }

    public static void PurchaseItem(int price, Crates rarity)
    {
        
        // Can the item be afforded
        #if UNITY_EDITOR
            if(Player.instance.Tickets < price && !ItemGUI.instance.FreeShops)
        #else
            if(Player.instance.Tickets < price)
        #endif
        {
            ItemGUI.instance.PurchaseFailSound();
            return;
        }

        // Checks where to add the item (from the first to the last slot)
        int position = -1;
        for (int i = 0; i < Slots.Length; i++)
            if(Slots[i] == Items.NoItem)
            {
                position = i;
                break;
            }

        // No suitable position available
        if(position == -1)
        {
            ItemGUI.instance.PurchaseFailSound();
            return;
        }

        // Subtracts the price from the ticket count
        #if UNITY_EDITOR
            if(!ItemGUI.instance.FreeShops)
        #endif
                Player.instance.Tickets -= price;

        Slots[position] = GenerateItemFromRarity(rarity);
        ItemGUI.instance.PurchaseSound();

        // Updates item UI
        ItemGUI.instance.LoadItems();
    }

    public static void Unequip()
    {
        Equipped.OnUnequip();
        Equipped = Items.NoItem;
    }

    public static void UseItem()
    {
        // If there are no balls in the field to apply the item to
        if(Field.instance.BallsInField.Count <= 0) return;

        // If inventory is empty or an item is already being utilised
        if(NextItem == Items.NoItem || Equipped != Items.NoItem) return;
        Equipped = NextItem;

        // Uses powerup
        Slots[0] = Slots[1];
        Slots[1] = Slots[2];
        Slots[2] = Items.NoItem;

        // Activates powerup
        // Method called for all items on activation
        foreach(Rigidbody ball in Field.instance.BallsInField)
        {
            if(Equipped.HasTrail)
            {
                ball.GetComponent<TrailRenderer>().enabled = true;
                ball.GetComponent<TrailRenderer>().material = Equipped.TrailMaterial;
            }

            if(Equipped.ChangeBallMaterial)
                ball.GetComponent<MeshRenderer>().material = Equipped.PoweredUpMaterial;

            if(Equipped.HasCustomPhysicMaterial)
                ball.GetComponent<SphereCollider>().material = Equipped.CustomPhysicMaterial;
        }

        Equipped.OnEquip();

        // Updates item UI
        ItemGUI.instance.LoadItems();

        // Plays sound from the board
        Field.instance.PowerupSound();
    }

    public static void Clear()
    {
        // Unequips current item and resets the item queue
        Unequip();
        Slots = new Item[3] { Items.NoItem, Items.NoItem, Items.NoItem };

        // Updates item UI
        ItemGUI.instance.LoadItems();
    }

    struct ItemIncidence
    {
        public Item item;
        public int incidence;
    }

    // Incidence-based random item generator
    static Item GenerateItemFromRarity(Crates rarity)
    {
        List<ItemIncidence> lootTable = new List<ItemIncidence>();

        // Which lootTable to use
        switch(rarity)
        {
            case Crates.Rusty:
                lootTable = new List<ItemIncidence>()
                {
                    new ItemIncidence(){ item = Items.Fireball, incidence = 4 },
                    new ItemIncidence(){ item = Items.WaterDroplet, incidence = 4 },
                    new ItemIncidence(){ item = Items.CameraFlip, incidence = 3 },
                    new ItemIncidence(){ item = Items.HealthBonus, incidence = 1 },
                    new ItemIncidence(){ item = Items.PingPong, incidence = 3 },
                    new ItemIncidence(){ item = Items.Rock, incidence = 4 },
                    new ItemIncidence(){ item = Items.TennisBall, incidence = 1 },
                };
                break;
            case Crates.Brass:
                lootTable = new List<ItemIncidence>()
                {
                    new ItemIncidence(){ item = Items.Fireball, incidence = 2 },
                    new ItemIncidence(){ item = Items.WaterDroplet, incidence = 1 },
                    new ItemIncidence(){ item = Items.LuckyCharm, incidence = 4 },
                    new ItemIncidence(){ item = Items.CurseOfAnubis, incidence = 1 },
                    new ItemIncidence(){ item = Items.AngelWings, incidence = 2 },
                    new ItemIncidence(){ item = Items.CameraFlip, incidence = 1 },
                    new ItemIncidence(){ item = Items.ExtraBall, incidence = 3 },
                    new ItemIncidence(){ item = Items.HealthBonus, incidence = 1 },
                    new ItemIncidence(){ item = Items.TennisBall, incidence = 2 },
                    new ItemIncidence(){ item = Items.TicketPrize, incidence = 3 }
                };
                break;
            case Crates.Golden:
                lootTable = new List<ItemIncidence>()
                {
                    new ItemIncidence(){ item = Items.CurseOfAnubis, incidence = 1 },
                    new ItemIncidence(){ item = Items.AngelWings, incidence = 2 },
                    new ItemIncidence(){ item = Items.ExtraBall, incidence = 2 },
                    new ItemIncidence(){ item = Items.HealthBonus, incidence = 1 },
                    new ItemIncidence(){ item = Items.TicketPrize, incidence = 1 }
                };
                break;
        }

        // Return Item from incidence
        // this is O(2n), where n = number of items inside lootTable
        int totalIncidence = 0;
        for (int i = 0; i < lootTable.Count; i++)
        {
            totalIncidence += lootTable[i].incidence;
            lootTable[i] = new ItemIncidence(){ item = lootTable[i].item, incidence = totalIncidence };
        }

        // Chooses an item inside given lootTable's incidence range
        int itemChosen = Random.Range(0, totalIncidence);

        // Returns what item does the lootTable references for the generated random index
        for (int i = 0; i < lootTable.Count; i++)
        {
            int lastInterval = 0;

            // If it's not the first entry check for the previous entry
            if(i > 0) lastInterval = lootTable[i-1].incidence;

            // Checks if the generated number is inside the interval
            if(itemChosen >= lastInterval && itemChosen < lootTable[i].incidence)
                return lootTable[i].item;
        }

        return Items.NoItem;
    }
}