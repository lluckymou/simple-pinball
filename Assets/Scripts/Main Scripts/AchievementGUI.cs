using UnityEngine;
using UnityEngine.UI;

class AchievementGUI : MonoBehaviour
{
    // Same-scene "singleton" pattern 
    private static AchievementGUI _instance;
    public static AchievementGUI instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<AchievementGUI>();
            return _instance;
        }
    }
    
    public Image Ninja;
    
    public Image GamblingExpert;
    
    public Image GamblingNewbie;
    
    public Image GamblingTycoon;
    
    public Image GettingStarted;
    
    public Image Jackpot;
    
    public Image OneOfAKind;
    
    public Image PinballWizard;
    
    public Image StraightFlush;
    
    public Image Survivalist;
    
    public Image TicketApprentice;
    
    public Image TicketHoarder;
    
    public Image TicketManiac;
    
    public Image TicketMaster;

    public void GetAchievementInfo(string _achievement)
    {
        if(!AchievementEnumeration.TryParse(_achievement, out AchievementEnumeration achievement)) return;

        Achievement achievementFound = Achievements.GetAchievementFromEnumeration(achievement);

        PlayerGUI.instance.InfoName.text = achievementFound.Name;
        PlayerGUI.instance.InfoDescription.text = achievementFound.Description;
    }
}