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

    // The order of the Dictionary is analogous to the item's, as converting from the enum is far more common with achievements
    static Dictionary<AchievementEnumeration, Achievement> achievementIDs
    {
        get => new Dictionary<AchievementEnumeration, Achievement>()
        {
            { AchievementEnumeration.Ninja, Ninja},
            { AchievementEnumeration.GamblingExpert, GamblingExpert},
            { AchievementEnumeration.GamblingNewbie, GamblingNewbie},
            { AchievementEnumeration.GamblingTycoon, GamblingTycoon},
            { AchievementEnumeration.GettingStarted, GettingStarted},
            { AchievementEnumeration.Jackpot, Jackpot},
            { AchievementEnumeration.OneOfAKind, OneOfAKind},
            { AchievementEnumeration.PinballWizard, PinballWizard},
            { AchievementEnumeration.StraightFlush, StraightFlush},
            { AchievementEnumeration.Survivalist, Survivalist},
            { AchievementEnumeration.TicketApprentice, TicketApprentice},
            { AchievementEnumeration.TicketHoarder, TicketHoarder},
            { AchievementEnumeration.TicketManiac, TicketManiac},
            { AchievementEnumeration.TicketMaster, TicketMaster},
        };
    }

    public static AchievementEnumeration[] AllAchievements
    {
        get => achievementIDs.Keys.ToArray();
    } 

    public static AchievementEnumeration GetEnumerationFromAchievement(Achievement achievement) =>
        achievementIDs.FirstOrDefault(a => a.Value == achievement).Key;

    public static Achievement GetAchievementFromEnumeration(AchievementEnumeration achievement)
    {
        if (achievementIDs.TryGetValue(achievement, out Achievement enumAchievement))
            return enumAchievement;
        else return GettingStarted;
    }
    
    void Awake()
    {
        // Loads all achievements from memory
        foreach(AchievementEnumeration achievementEnumeration in AllAchievements)

            // Checks if said achievement is complete and gives it to the user
            GetAchievementFromEnumeration(achievementEnumeration).Completed = (PlayerPrefs.GetInt(achievementEnumeration.ToString(), 0) == 1);

        // Loads all achievement UI
        AchievementGUI.instance.UpdateUI();
    }

    #if UNITY_EDITOR
        public static void RemoveAchievement(Achievement achievement)
        {
            achievement.Completed = false;

            // Clears the achievement in memory
            PlayerPrefs.SetInt(GetEnumerationFromAchievement(achievement).ToString(), 0);

            // Reloads UI
            AchievementGUI.instance.UpdateUI();
        }
    #endif
    public static void GiveAchievement(Achievement achievement)
    {
        // User already has achievement
        if(achievement.Completed) return;

        // Gives the achievement if it can be completed
        achievement.Completed = achievement.CanBeCompleted;

        if(!achievement.Completed) return;

        // Saves in memory the achievement
        PlayerPrefs.SetInt(GetEnumerationFromAchievement(achievement).ToString(), 1);

        // Plays the "achievement get!" animation        
        AchievementGUI.instance.AchievementGet(achievement);

        // Reloads UI
        AchievementGUI.instance.UpdateUI();
    }
}
