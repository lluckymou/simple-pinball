using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Achievements : MonoBehaviour
{
    // Same-scene "singleton" pattern for static access
    private static Achievements _instance;
    public static Achievements instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<Achievements>();
            return _instance;
        }
    }

    [Header("Achievements")]
    [SerializeField]
    Ninja _Ninja;

    public static Ninja Ninja
    {
        get => instance._Ninja;
    }

    [SerializeField]
    GamblingExpert _gamblingExpert;

    public static GamblingExpert GamblingExpert
    {
        get => instance._gamblingExpert;
    }

    [SerializeField]
    GamblingNewbie _gamblingNewbie;

    public static GamblingNewbie GamblingNewbie
    {
        get => instance._gamblingNewbie;
    }

    [SerializeField]
    GamblingTycoon _gamblingTycoon;

    public static GamblingTycoon GamblingTycoon
    {
        get => instance._gamblingTycoon;
    }

    [SerializeField]
    GettingStarted _gettingStarted;

    public static GettingStarted GettingStarted
    {
        get => instance._gettingStarted;
    }

    [SerializeField]
    Jackpot _jackPot;

    public static Jackpot Jackpot
    {
        get => instance._jackPot;
    }

    [SerializeField]
    OneOfAKind _oneOfAKind;

    public static OneOfAKind OneOfAKind
    {
        get => instance._oneOfAKind;
    }

    [SerializeField]
    PinballWizard _pinballWizard;

    public static PinballWizard PinballWizard
    {
        get => instance._pinballWizard;
    }

    [SerializeField]
    StraightFlush _straightFlush;

    public static StraightFlush StraightFlush
    {
        get => instance._straightFlush;
    }

    [SerializeField]
    Survivalist _survivalist;

    public static Survivalist Survivalist
    {
        get => instance._survivalist;
    }

    [SerializeField]
    TicketApprentice _ticketApprentice;

    public static TicketApprentice TicketApprentice
    {
        get => instance._ticketApprentice;
    }

    [SerializeField]
    TicketHoarder _ticketHoarder;

    public static TicketHoarder TicketHoarder
    {
        get => instance._ticketHoarder;
    }

    [SerializeField]
    TicketManiac _ticketManiac;

    public static TicketManiac TicketManiac
    {
        get => instance._ticketManiac;
    }

    [SerializeField]
    TicketMaster _ticketMaster;

    public static TicketMaster TicketMaster
    {
        get => instance._ticketMaster;
    }

    static Dictionary<Achievement, AchievementEnumeration> achievementIDs
    {
        get => new Dictionary<Achievement, AchievementEnumeration>()
        {
            { Ninja, AchievementEnumeration.Ninja},
            { GamblingExpert, AchievementEnumeration.GamblingExpert},
            { GamblingNewbie, AchievementEnumeration.GamblingNewbie},
            { GamblingTycoon, AchievementEnumeration.GamblingTycoon},
            { GettingStarted, AchievementEnumeration.GettingStarted},
            { Jackpot, AchievementEnumeration.Jackpot},
            { OneOfAKind, AchievementEnumeration.OneOfAKind},
            { PinballWizard, AchievementEnumeration.PinballWizard},
            { StraightFlush, AchievementEnumeration.StraightFlush},
            { Survivalist, AchievementEnumeration.Survivalist},
            { TicketApprentice, AchievementEnumeration.TicketApprentice},
            { TicketHoarder, AchievementEnumeration.TicketHoarder},
            { TicketManiac, AchievementEnumeration.TicketManiac},
            { TicketMaster, AchievementEnumeration.TicketMaster},
        };
    }

    public static AchievementEnumeration GetEnumerationFromAchievement(Achievement achievement)
    {
        if (achievementIDs.TryGetValue(achievement, out AchievementEnumeration enumAchievement))
            return enumAchievement;
        else return AchievementEnumeration.GettingStarted;
    }

    public static Achievement GetAchievementFromEnumeration(AchievementEnumeration item) =>
        achievementIDs.FirstOrDefault(i => i.Value == item).Key;

    
}
