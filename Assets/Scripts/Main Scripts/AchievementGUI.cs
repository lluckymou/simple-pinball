using TMPro;
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
    
    #if UNITY_EDITOR
        [Header("Debug (editor only)")]
        [SerializeField] 
        AchievementEnumeration GiveAchievement;

        [SerializeField] 
        AchievementEnumeration RemoveAchievement;
    #endif

    [Header("Icons")]
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

    [Header("UI Colors")]
    [SerializeField]
    Color CompletedCommon;

    [SerializeField]
    Color CompletedRare;

    [SerializeField]
    Color CompletedLegendary;

    [SerializeField]
    Color Common;

    [SerializeField]
    Color Rare;
    
    [SerializeField]
    Color Legendary;

    [Header("Achievement Get")]
    [SerializeField]
    GameObject AchievementGetPanel;
    
    [SerializeField]
    Image AchievementGetImage;

    [SerializeField]
    TMP_Text AchievementGetText;

    void Awake()
    {
        #if UNITY_EDITOR
            GiveAchievement = AchievementEnumeration.None;
        #endif
    }

    #if UNITY_EDITOR
        void Update()
        {
            if(GiveAchievement != AchievementEnumeration.None)
            {
                Achievements.GiveAchievement(Achievements.GetAchievementFromEnumeration(GiveAchievement));
                GiveAchievement = AchievementEnumeration.None;
            }

            if(RemoveAchievement != AchievementEnumeration.None)
            {
                Achievements.RemoveAchievement(Achievements.GetAchievementFromEnumeration(RemoveAchievement));
                RemoveAchievement = AchievementEnumeration.None;
            }
        }
    #endif

    public void AchievementGet(Achievement achievement)
    {
        AchievementGetImage.sprite = achievement.Sprite;
        AchievementGetText.text = achievement.Name;

        AchievementGetPanel.GetComponent<Animator>().Play("AchievementGet");
    }

    public void GetAchievementInfo(string _achievement)
    {
        if(!AchievementEnumeration.TryParse(_achievement, out AchievementEnumeration achievement)) return;

        Achievement achievementFound = Achievements.GetAchievementFromEnumeration(achievement);

        PlayerGUI.instance.InfoName.text = (achievementFound.Completed? $"<color=#4683BC>{achievementFound.Name}</color>" : achievementFound.Name);
        PlayerGUI.instance.InfoDescription.text = (achievementFound.Completed? $"<b>(Completed)</b> {achievementFound.Description}" : achievementFound.Description);
    }

    public void UpdateUI()
    {
        foreach(AchievementEnumeration achievementEnumeration in Achievements.AllAchievements)
        {
            Achievement achievement = Achievements.GetAchievementFromEnumeration(achievementEnumeration);

            // Visibility and outline
            achievement.Icon.gameObject.SetActive(achievement.CanBeCompleted || achievement.Completed);
            achievement.Icon.GetComponent<Outline>().enabled = achievement.Completed;

            // Color
            switch(achievement.Rarity)
            {
                case AchievementRarity.Common:
                    achievement.Icon.color = (achievement.Completed? CompletedCommon : Common);
                    break;
                case AchievementRarity.Rare:
                    achievement.Icon.color = (achievement.Completed? CompletedCommon : Rare);
                    break;
                case AchievementRarity.Legendary:
                    achievement.Icon.color = (achievement.Completed? CompletedCommon : Legendary);
                    break;
            }
        }
    }
}