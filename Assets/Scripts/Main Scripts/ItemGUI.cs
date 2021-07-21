using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGUI : MonoBehaviour
{
    // Same-scene "singleton" pattern 
    private static ItemGUI _instance;
    public static ItemGUI instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<ItemGUI>();
            return _instance;
        }
    }
    
    #if UNITY_EDITOR
        [Header("Debug (editor only)")]
        public bool FreeShops;

        [SerializeField]
        bool ClearInventory;

        [SerializeField] 
        InspectorItem GiveItem;
    #endif

    [Header("Item Shop")]
    [SerializeField]
    Button RustyCrate;

    [SerializeField]
    Button BrassCrate;

    [SerializeField]
    Button GoldenCrate;

    [Header("Powerup Slots")]
    [SerializeField]
    List<Image> FirstItem;

    [SerializeField]
    List<Image> SecondItem;

    [SerializeField]
    List<Image> ThirdItem;

    [Header("Audio")]
    [SerializeField]
    AudioSource UISpeaker;

    [SerializeField]
    AudioClip Purchase;

    [SerializeField]
    AudioClip PurchaseFail;

    void Awake()
    {
        #if UNITY_EDITOR
            GiveItem = InspectorItem.NoItem;
        #endif

        RustyCrate.onClick.AddListener(() => Inventory.PurchaseItem(3, Crates.Rusty));
        BrassCrate.onClick.AddListener(() => Inventory.PurchaseItem(6, Crates.Brass));
        GoldenCrate.onClick.AddListener(() => Inventory.PurchaseItem(10, Crates.Golden));
    }

    #if UNITY_EDITOR
        void Update()
        {
            if(ClearInventory)
            {
                Inventory.Clear();
                ClearInventory = false;
            }

            if(GiveItem != InspectorItem.NoItem)
            {
                Inventory.GiveItem(GetItemFromInspectorEnum(GiveItem));
                GiveItem = InspectorItem.NoItem;
            }
        }
    #endif

    // Method to be used in an event system on the inspector
    public void GetItemInfo(string _item)
    {
        if(!InspectorItem.TryParse(_item, out InspectorItem item)) return;

        Item itemFound = GetItemFromInspectorEnum(item);

        PlayerGUI.instance.InfoName.text = itemFound.Name;
        PlayerGUI.instance.InfoDescription.text = itemFound.Description;
    }

    public void GetSlotInfo(int slot)
    {
        if(slot < 0 || slot >= Inventory.Slots.Length) return;

        PlayerGUI.instance.InfoName.text = Inventory.Slots[slot].Name;
        PlayerGUI.instance.InfoDescription.text = Inventory.Slots[slot].Description;
    }

    public void LoadItems()
    {
        LoadItem(Inventory.Slots[0], FirstItem);
        LoadItem(Inventory.Slots[1], SecondItem);
        LoadItem(Inventory.Slots[2], ThirdItem);
    }

    public void PurchaseSound() => UISpeaker.PlayOneShot(Purchase);

    public void PurchaseFailSound() => UISpeaker.PlayOneShot(PurchaseFail);

    Item GetItemFromInspectorEnum(InspectorItem item)
    {
        switch(item)
        {
            case InspectorItem.Fireball: return Items.Fireball;
            case InspectorItem.WaterDroplet: return Items.WaterDroplet;
            case InspectorItem.LuckyCharm: return Items.LuckyCharm;
            case InspectorItem.CurseOfAnubis: return Items.CurseOfAnubis;
            case InspectorItem.AngelWings: return Items.AngelWings;
            case InspectorItem.CameraFlip: return Items.CameraFlip;
            case InspectorItem.ExtraBall: return Items.ExtraBall;
            case InspectorItem.HealthBonus: return Items.HealthBonus;
            case InspectorItem.PingPong: return Items.PingPong;
            case InspectorItem.Rock: return Items.Rock;
            case InspectorItem.TennisBall: return Items.TennisBall;
            case InspectorItem.TicketPrize: return Items.TicketPrize;
        }

        return Items.NoItem;
    }

    void LoadItem(Item item, List<Image> itemSlot)
    {
        if(item.Icon == null)
            foreach(Image i in itemSlot)
                i.gameObject.SetActive(false);

        else foreach(Image i in itemSlot)
        {
            i.gameObject.SetActive(true);
            i.sprite = item.Icon;
        }
    }
}