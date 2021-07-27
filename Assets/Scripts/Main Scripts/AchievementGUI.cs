using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
    Animator AchievementGetPanel;
    
    [SerializeField]
    Image AchievementGetImage;

    [SerializeField]
    TMP_Text AchievementGetText;

    // Animation queue
    bool animationIsPlaying
    {
        get => AchievementGetPanel.GetCurrentAnimatorStateInfo(0).IsName("Base.AchievementGet");
    }

    Queue<Achievement> achievementQueue = new Queue<Achievement>();

    #if UNITY_EDITOR
        void Awake() => GiveAchievement = AchievementEnumeration.None;
    #endif

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

    // Variable needed as animation only starts the following frame
    bool _animationWaiting = false;
    IEnumerator PlayAnimation()
    {
        // Plays animation and sound
        AchievementGetPanel.Play("AchievementGet");
        GUIAudio.Speaker.PlayOneShot(GUIAudio.AchievementGetSound);

        // Waits for the animation to complete
        do yield return null;
        while(animationIsPlaying);

        // Tries to play next animation
        _animationWaiting = false;
        PlayQueue();
    }

    void PlayQueue()
    {
        if(achievementQueue.Count <= 0 || animationIsPlaying || _animationWaiting) return;

        Achievement _achievement = achievementQueue.Dequeue();
        AchievementGetImage.sprite = _achievement.Sprite;
        AchievementGetText.text = _achievement.Name;

        _animationWaiting = true;
        StartCoroutine(PlayAnimation());
    }

    public void AchievementGet(Achievement achievement)
    {
        achievementQueue.Enqueue(achievement);
        PlayQueue();
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