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

    [Header("Items")]
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

    [SerializeField]
    CurseOfAnubis _curseOfAnubis;

    public static CurseOfAnubis CurseOfAnubis
    {
        get => instance._curseOfAnubis;
    }

    [SerializeField]
    AngelWings _angelWings;

    public static AngelWings AngelWings
    {
        get => instance._angelWings;
    }

    [SerializeField]
    CameraFlip _cameraFlip;

    public static CameraFlip CameraFlip
    {
        get => instance._cameraFlip;
    }

    [SerializeField]
    ExtraBall _extraBall;

    public static ExtraBall ExtraBall
    {
        get => instance._extraBall;
    }

    [SerializeField]
    HealthBonus _healthBonus;

    public static HealthBonus HealthBonus
    {
        get => instance._healthBonus;
    }

    [SerializeField]
    PingPong _pingPong;

    public static PingPong PingPong
    {
        get => instance._pingPong;
    }

    [SerializeField]
    Rock _rock;

    public static Rock Rock
    {
        get => instance._rock;
    }

    [SerializeField]
    TennisBall _tennisBall;

    public static TennisBall TennisBall
    {
        get => instance._tennisBall;
    }

    [SerializeField]
    TicketPrize _ticketPrize;

    public static TicketPrize TicketPrize
    {
        get => instance._ticketPrize;
    }
}