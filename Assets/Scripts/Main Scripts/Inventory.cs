using UnityEngine;

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

    static Items GenerateItemFromRarity(Crates rarity)
    {
        return Items.Fireball;
    }
}