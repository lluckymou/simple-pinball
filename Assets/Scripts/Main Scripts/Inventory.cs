using UnityEngine;
using System.Collections.Generic;

public static class Inventory
{
    public static Items[] Slots = new Items[3] { Items.NoItem, Items.NoItem, Items.NoItem };
    public static Items Equipped = Items.NoItem;
    public static Items NextItem
    {
        get => Slots[0];
    }

    public static void GiveItem(Items item)
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
        #if UNITY_EDITOR
            if(ItemGUI.instance.FreeShops)
        #endif
                // Can the item be afforded
                if(Player.instance.Tickets < price)
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

        Slots[position] = GenerateItemFromRarity(rarity);
        ItemGUI.instance.PurchaseSound();

        // Updates item UI
        ItemGUI.instance.LoadItems();
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
        foreach(Rigidbody ball in Field.instance.BallsInField)
            ball.GetComponent<Ball>().ActivatePowerup(Equipped);

        // Updates item UI
        ItemGUI.instance.LoadItems();

        // Plays sound from the board
        Field.instance.ActivatePowerup();
    }

    struct ItemIncidence
    {
        public Items Item;
        public int Incidence;
    }

    // Incidence-based random item generator
    static Items GenerateItemFromRarity(Crates rarity)
    {
        List<ItemIncidence> lootTable = new List<ItemIncidence>();

        // Which lootTable to use
        switch(rarity)
        {
            case Crates.Rusty:
                lootTable = new List<ItemIncidence>()
                {
                    new ItemIncidence(){ Item = Items.Fireball, Incidence = 100 },
                    new ItemIncidence(){ Item = Items.WaterDroplet, Incidence = 100 },
                    new ItemIncidence(){ Item = Items.LuckyCharm, Incidence = 100 },
                    new ItemIncidence(){ Item = Items.CurseOfAnubis, Incidence = 10 },
                };
                break;
            case Crates.Brass:
                lootTable = new List<ItemIncidence>()
                {
                    new ItemIncidence(){ Item = Items.Fireball, Incidence = 50 },
                    new ItemIncidence(){ Item = Items.WaterDroplet, Incidence = 50 },
                    new ItemIncidence(){ Item = Items.LuckyCharm, Incidence = 100 },
                    new ItemIncidence(){ Item = Items.CurseOfAnubis, Incidence = 100 },
                };
                break;
            case Crates.Golden:
                lootTable = new List<ItemIncidence>()
                {
                    new ItemIncidence(){ Item = Items.Fireball, Incidence = 10 },
                    new ItemIncidence(){ Item = Items.WaterDroplet, Incidence = 10 },
                    new ItemIncidence(){ Item = Items.LuckyCharm, Incidence = 10 },
                    new ItemIncidence(){ Item = Items.CurseOfAnubis, Incidence = 200 },
                };
                break;
        }

        // Return Item from incidence
        // this is O(2n), where n = number of items inside lootTable
        int totalIncidence = 0;
        for (int i = 0; i < lootTable.Count; i++)
        {
            totalIncidence += lootTable[i].Incidence;
            lootTable[i] = new ItemIncidence(){ Item = lootTable[i].Item, Incidence = totalIncidence };
        }

        // Chooses an item inside given lootTable's incidence range
        int itemChosen = Random.Range(0, totalIncidence);

        // Returns what item does the lootTable references for the generated random index
        for (int i = 0; i < lootTable.Count; i++)
        {
            int lastInterval = 0;

            // If it's not the first entry check for the previous entry
            if(i > 0) lastInterval = lootTable[i-1].Incidence;

            // Checks if the generated number is inside the interval
            if(itemChosen >= lastInterval && itemChosen < lootTable[i].Incidence)
                return lootTable[i].Item;
        }

        return Items.NoItem;
    }
}