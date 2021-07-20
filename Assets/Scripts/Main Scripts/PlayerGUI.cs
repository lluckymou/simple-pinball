using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerGUI : MonoBehaviour
{
    // Same-scene "singleton" pattern 
    private static PlayerGUI _instance;
    public static PlayerGUI instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<PlayerGUI>();
            return _instance;
        }
    }

    [Header("Debug")]
    [SerializeField]
    TMP_Text FPS;

    [Header("Important Buttons")]
    [SerializeField]
    Button Play;

    [SerializeField]
    Button PlayAgain;

    [Header("Panels")]
    [SerializeField]
    GameObject PlayPanel;

    [SerializeField]
    GameObject GameOverPanel;

    [Header("Panel Labels")]
    [SerializeField]
    TMP_Text GameOverLabel;

    [Header("Player labels")]
    public TMP_Text Score;

    public TMP_Text Multiplier;

    public TMP_Text Tickets;

    public TMP_Text PlayMenuTickets;

    public TMP_Text Lives;

    [Header("Other Labels/Fields")]
    [SerializeField]
    TMP_InputField HighScore;

    float deltaTime = 0, refreshRate = 0;

    void Awake()
    {
        OpenPlayPanel();

        Play.onClick.AddListener(PlayGame);
        PlayAgain.onClick.AddListener(OpenPlayPanel);
    }

    void OpenPlayPanel()
    {
        PlayPanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    void PlayGame()
    {
        Player.instance.GameStart();
        CloseAll();
    }

    public void StopGame(int score)
    {
        GameOverPanel.SetActive(true);
        GameOverLabel.text = $"<b>Game Over!</b>\nScore: {score}";

        Leaderboard.Games.Add(new Game() { Score = score, Time = System.DateTime.Now });
        GenerateGameList();
    }

    void CloseAll()
    {
        PlayPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    void GenerateGameList()
    {
        Leaderboard.Order();

        string text = "";

        for (int i = 0; i < Leaderboard.Games.Count; i++)
            text += $"<i>#{(i+1).ToString("00")}</i> | Score: <b>{Leaderboard.Games[i].Score}</b> | <color=#666>{Leaderboard.Games[i].Time}</color>\n";

        HighScore.text = text;
    }

    void Update()
    {
        refreshRate += Time.deltaTime;
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if(refreshRate > 0.5f)
        {
            FPS.text = $"FPS: {((int) (1f / deltaTime))}";
            refreshRate = 0;
        }
    }
}
