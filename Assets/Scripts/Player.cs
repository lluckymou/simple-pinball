using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    // Points is the private field, used for inspector purposes
    [SerializeField]
    int points = 0;

    int Score
    {
        get => points;
        set
        {
            points = value;
        }
    }

    public void IncrementScore(int value) => Score += value;

}
