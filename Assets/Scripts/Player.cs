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
    float _multiplier;

    [Header("Multiplier Settings")]
    [SerializeField]
    float IncrementTime;

    [SerializeField]
    float MultiplierIncrement;

    [Header("UI Fields")]
    [SerializeField]
    TMP_Text ScoreText;

    [SerializeField]
    TMP_Text MultiplierText;

    [SerializeField]
    TMP_Text LivesText;

    float timeAlive = 0;
    float timeDead = 0;

    int Score
    {
        get => _score;
        set
        {
            // Updates score variable
            _score = value;

            // Updates UI text
            ScoreText.text = _score.ToString();
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
            MultiplierText.text = $"{_multiplier}x";
        }
    }

    int Lives
    {
        get => _lives;
        set
        {
            // Updates multiplier variable
            _lives = value;

            // Resets scoring multiplier
            Multiplier = 1;

            // Updates UI text
            LivesText.text = _lives.ToString();
        }
    }

    void Awake()
    {
        ScoreText.text = _score.ToString();
        MultiplierText.text = $"{_multiplier}x";
        LivesText.text = _lives.ToString();
    }

    public void IncrementScore(int value) => Score += (int) (value * Multiplier);

    void GameOver() {}

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
                timeDead = 0;
                Lives -= 1;

                if(Lives < 0) GameOver();
                else Instantiate(BallPrefab, Field.instance.Spawnpoint);
            }
        }       
    }

}
