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
        ItemEnumeration GiveItem;
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

    void Awake()
    {
        #if UNITY_EDITOR
            GiveItem = ItemEnumeration.NoItem;
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

            if(GiveItem != ItemEnumeration.NoItem)
            {
                Inventory.GiveItem(Items.GetItemFromEnumeration(GiveItem));
                GiveItem = ItemEnumeration.NoItem;
            }
        }
    #endif

    // Method to be used in an event system on the inspector
    public void GetItemInfo(string _item)
    {
        if(!ItemEnumeration.TryParse(_item, out ItemEnumeration item)) return;

        Item itemFound = Items.GetItemFromEnumeration(item);

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

    public void PurchaseSound() => GUIAudio.Speaker.PlayOneShot(GUIAudio.PurchaseSound);

    public void PurchaseFailSound() => GUIAudio.Speaker.PlayOneShot(GUIAudio.PurchaseFailSound);

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