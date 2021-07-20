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
    float IncrementTime;

    [SerializeField]
    float MultiplierIncrement;

    [Header("Ticket Gathering Settings")]
    [SerializeField]
    int TicketEarningFactor;

    float timeAlive = 0, timeDead = 0;
    int lastTicketIncrement = 0;

    public int Score
    {
        get => _score;
        set
        {
            Inventory.Equipped.OnScoring();

            // Updates score variable
            _score = value;

            if(_score == 0) lastTicketIncrement = 0; //reset condition
            else if(_score > lastTicketIncrement+TicketEarningFactor)
            {
                lastTicketIncrement += TicketEarningFactor;
                Tickets += 1;
            }

            // Updates UI text
            PlayerGUI.instance.Score.text = _score.ToString();
        }
    }

    public int Tickets
    {
        get => _tickets;
        set
        {
            // Updates score variable
            _tickets = value;

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
        PlayerGUI.instance.Score.text = _score.ToString();
        PlayerGUI.instance.Multiplier.text = $"{_multiplier}x";
        PlayerGUI.instance.Lives.text = (_lives < 0 ? "0" : _lives.ToString());
        PlayerGUI.instance.Tickets.text = _tickets.ToString();

        ItemGUI.instance.LoadItems();
    }

    public void IncrementScore(int value) => Score += (int) (value * Multiplier);

    public void GameStart() 
    {
        Lives = 4;
        Score = 0;
    }

    void Update()
    {
        if(Field.instance.HasBall)
        {
            if(!Field.instance.StationaryBalls)
            {
                timeAlive += Time.deltaTime;

                if(timeAlive > IncrementTime)
                {
                    timeAlive = 0;
                    Multiplier += MultiplierIncrement;
                }
            }
        }
        else
        {
            timeDead += Time.deltaTime;

            if(timeDead > 1)
            {
                if(Lives < 0) return;

                timeDead = 0;
                Lives -= 1;
                Inventory.Equipped.OnDeath();
                Inventory.Unequip();

                if(_lives < 0) 
                {
                    PlayerGUI.instance.Lives.text = "0";
                    PlayerGUI.instance.StopGame(Score);
                }
                else
                    Instantiate(BallPrefab, Field.instance.Spawnpoint);
            }
        }       
    }

}
