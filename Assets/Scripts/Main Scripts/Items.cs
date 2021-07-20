using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    // Same-scene "singleton" pattern for static access
    private static Items _instance;
    public static Items instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<Items>();
            return _instance;
        }
    }

    [Header("Special Items")]
    [SerializeField]
    NoItem _noItem;

    public static NoItem NoItem
    {
        get => instance._noItem;
    }

    [Header("Common")]
    [SerializeField]
    Fireball _fireball;

    public static Fireball Fireball
    {
        get => instance._fireball;
    }

    [SerializeField]
    WaterDroplet _waterDroplet;

    public static WaterDroplet WaterDroplet
    {
        get => instance._waterDroplet;
    }

    [SerializeField]
    LuckyCharm _luckyCharm;

    public static LuckyCharm LuckyCharm
    {
        get => instance._luckyCharm;
    }

    [Header("Rare")]
    [SerializeField]
    CurseOfAnubis _curseOfAnubis;

    public static CurseOfAnubis CurseOfAnubis
    {
        get => instance._curseOfAnubis;
    }
}