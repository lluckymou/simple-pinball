using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    // Same-scene "singleton" pattern 
    private static Player _instance;
    public static Player instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<Player>();
            return _instance;
        }
    }

    [Header("Player")]
    [SerializeField]
    GameObject BallPrefab;

    [SerializeField]
    int _score;

    [SerializeField]
    int _lives;

    [SerializeField]
    int _tickets;

    [SerializeField]
    float _multiplier;

    [Header("Multiplier Settings")]
    [SerializeField]
    float MultiplierIncrementTime;

    [SerializeField]
    float MultiplierIncrement;

    [Header("Ticket Gathering Settings")]
    [SerializeField]
    int TicketEarningFactor;

    float timeAlive = 0, timeDead = 0;
    float lastTicketIncrement = 0;

    public static bool Tilt;

    public int Score
    {
        get => _score;
        set
        {
            Inventory.Equipped.OnScoring();

            // Updates score variable
            _score = value;

            if(_score == 0) lastTicketIncrement = 0; //reset condition
            else if(_score > lastTicketIncrement+(TicketEarningFactor*_multiplier))
            {
                lastTicketIncrement += TicketEarningFactor * _multiplier;
                Tickets += 1;
            }

            if(_score >= 1000000) Achievements.GiveAchievement(Achievements.PinballWizard);

            // Updates UI text
            PlayerGUI.instance.Score.text = _score.ToString();
        }
    }

    public int Tickets
    {
        get => _tickets;
        set
        {
            // Updates ticket variable
            _tickets = value;

            PlayerPrefs.SetInt("ticketCount", value);

            if(_tickets > 0)
                if(_tickets >= 1000 && Achievements.TicketHoarder.Completed) Achievements.GiveAchievement(Achievements.TicketManiac);
                else if(_tickets >= 100 && Achievements.TicketMaster.Completed) Achievements.GiveAchievement(Achievements.TicketHoarder);
                else if(_tickets >= 10 && Achievements.TicketApprentice.Completed) Achievements.GiveAchievement(Achievements.TicketMaster);
                else Achievements.GiveAchievement(Achievements.TicketApprentice);

            // Updates UI text
            PlayerGUI.instance.Tickets.text = _tickets.ToString();
            PlayerGUI.instance.PlayMenuTickets.text = _tickets.ToString();
        }
    }

    public float Multiplier
    {
        get => _multiplier;
        set
        {
            // Updates multiplier variable
            _multiplier = value;

            if(_multiplier >= 5 && Achievements.Survivalist.Completed) Achievements.GiveAchievement(Achievements.Ninja);
            else if(_multiplier >= 3) Achievements.GiveAchievement(Achievements.Survivalist);

            // Updates UI text
            PlayerGUI.instance.Multiplier.text = $"{_multiplier.ToString("0.0")}x";
        }
    }

    public int Lives
    {
        get => _lives;
        set
        {
            // Updates multiplier variable
            _lives = value;

            // Resets scoring multiplier
            Multiplier = 1;

            // Updates UI text
            PlayerGUI.instance.Lives.text = _lives.ToString();
        }
    }

    void Awake()
    {
        Tickets = PlayerPrefs.GetInt("ticketCount", 0);

        PlayerGUI.instance.Score.text = _score.ToString();
        PlayerGUI.instance.Multiplier.text = $"{_multiplier}x";
        PlayerGUI.instance.Lives.text = (_lives < 0 ? "0" : _lives.ToString());

        Inventory.GetMemory();
    }

    public void IncrementScore(int value) => Score += (int) (value * Multiplier);

    public void GameStart() 
    {
        Achievements.GiveAchievement(Achievements.GettingStarted);
        Tilt = false;
        Lives = 4;
        Score = 0;
    }

    public void SpawnBall() => Instantiate(BallPrefab, Field.instance.Spawnpoint);

    void Update()
    {
        if(Field.instance.HasBall)
        {
            if(!Field.instance.StationaryBalls)
            {
                timeAlive += Time.deltaTime;

                if(timeAlive > MultiplierIncrementTime)
                {
                    timeAlive = 0;
                    Multiplier += MultiplierIncrement;
                }
            }
        }
        else
        {
            timeDead += Time.deltaTime;

            if(timeDead > 0.8f)
            {
                if(Lives < 0) return;

                timeDead = 0;
                Lives -= 1;
                Tilt = false;
                Inventory.Equipped.OnDeath();
                Inventory.Unequip();

                if(_lives < 0) 
                {
                    PlayerGUI.instance.Lives.text = "0";
                    PlayerGUI.instance.StopGame(Score);
                }
                else SpawnBall();
            }
        }       
    }

}
