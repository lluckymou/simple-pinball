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

    public Items[] Inventory = new Items[3];
    public Items Equipped = Items.NoItem;
    public Items NextItem
    {
        get => Inventory[0];
    }

    int Score
    {
        get => _score;
        set
        {
            // Updates score variable
            _score = value;

            if(_score == 0) lastTicketIncrement = 0; //reset condition
            else if(_score > lastTicketIncrement+TicketEarningFactor)
            {
                lastTicketIncrement += TicketEarningFactor;
                Tickets += 1;
            }

            // Updates UI text
            GUIController.instance.Score.text = _score.ToString();
        }
    }

    int Tickets
    {
        get => _tickets;
        set
        {
            // Updates score variable
            _tickets = value;

            // Updates UI text
            GUIController.instance.Tickets.text = _tickets.ToString();
            GUIController.instance.PlayMenuTickets.text = _tickets.ToString();
        }
    }

    float Multiplier
    {
        get => _multiplier;
        set
        {
            // Updates multiplier variable
            _multiplier = value;

            // Updates UI text
            GUIController.instance.Multiplier.text = $"{_multiplier}x";
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
            GUIController.instance.Lives.text = _lives.ToString();
        }
    }

    void Awake()
    {
        GUIController.instance.Score.text = _score.ToString();
        GUIController.instance.Multiplier.text = $"{_multiplier}x";
        GUIController.instance.Lives.text = (_lives < 0 ? "0" : _lives.ToString());
        GUIController.instance.Tickets.text = _tickets.ToString();
    }

    public void IncrementScore(int value) => Score += (int) (value * Multiplier);

    public void GameStart() 
    {
        Lives = 4;
        Score = 0;
    }

    public void UseItem()
    {
        // If inventory is empty or an item is already being utilised
        if(NextItem == Items.NoItem || Equipped != Items.NoItem) return;

        // foreach(GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        //     ball.GetComponent<Ball>().
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

                if(_lives < 0) 
                {
                    GUIController.instance.Lives.text = "0";
                    GUIController.instance.StopGame(Score);
                }
                else
                    Instantiate(BallPrefab, Field.instance.Spawnpoint);
            }
        }       
    }

}
