using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
enum ItemGivePanel
{
    Nothing,
    Fireball,
    WaterDroplet,
    LuckyCharm,
    CurseOfAnubis
}
#endif

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
        ItemGivePanel GiveItem;
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

    #if UNITY_EDITOR
        void Update()
        {
            if(ClearInventory)
            {
                Inventory.Clear();
                ClearInventory = false;
            }

            if(GiveItem != ItemGivePanel.Nothing)
            {
                switch(GiveItem)
                {
                    case ItemGivePanel.Fireball: Inventory.GiveItem(Items.Fireball); break;
                    case ItemGivePanel.WaterDroplet: Inventory.GiveItem(Items.WaterDroplet); break;
                    case ItemGivePanel.LuckyCharm: Inventory.GiveItem(Items.LuckyCharm); break;
                    case ItemGivePanel.CurseOfAnubis: Inventory.GiveItem(Items.CurseOfAnubis); break;
                }

                GiveItem = ItemGivePanel.Nothing;
            }
        }
    #endif

    void Awake()
    {
        #if UNITY_EDITOR
            GiveItem = ItemGivePanel.Nothing;
        #endif

        RustyCrate.onClick.AddListener(() => Inventory.PurchaseItem(3, Crates.Rusty));
        BrassCrate.onClick.AddListener(() => Inventory.PurchaseItem(6, Crates.Brass));
        GoldenCrate.onClick.AddListener(() => Inventory.PurchaseItem(10, Crates.Golden));
    }

    public void LoadItems()
    {
        LoadItem(Inventory.Slots[0], FirstItem);
        LoadItem(Inventory.Slots[1], SecondItem);
        LoadItem(Inventory.Slots[2], ThirdItem);
    }

    public void PurchaseSound() => UISpeaker.PlayOneShot(Purchase);

    public void PurchaseFailSound() => UISpeaker.PlayOneShot(PurchaseFail);

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