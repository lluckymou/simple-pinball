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

    [Header("Powerup Sprites")]
    [SerializeField]
    Sprite Fireball;
    
    [SerializeField]
    Sprite WaterDroplet;
    
    [SerializeField]
    Sprite LuckyCharm;
    
    [SerializeField]
    Sprite CurseOfAnubis;

    void Awake()
    {
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

    void LoadItem(Items item, List<Image> itemSlot)
    {
        if(item == Items.NoItem)
            foreach(Image i in itemSlot)
                i.gameObject.SetActive(false);

        else foreach(Image i in itemSlot)
        {
            i.gameObject.SetActive(true);
            i.sprite = ItemSprite(item);
        }
    }

    Sprite ItemSprite(Items item)
    {
        switch(item)
        {
            case Items.Fireball:
                return Fireball;
            
            case Items.WaterDroplet:
                return WaterDroplet;
            
            case Items.LuckyCharm:
                return LuckyCharm;
            
            case Items.CurseOfAnubis:
                return CurseOfAnubis;
        
            default: //case Items.NoItem:
                return null;
        }
    }
}