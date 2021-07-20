using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    // Same-scene "singleton" pattern
    private static Field _instance;
    public static Field instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<Field>();
            return _instance;
        }
    }

    public Transform Spawnpoint;

    [Header("Sounds")]
    [SerializeField]
    AudioClip Death;

    [SerializeField]
    AudioClip Powerup;

    public bool HasBall
    {
        get => BallsInField.Count > 0;
    }

    public bool StationaryBalls
    {
        get
        {
            foreach(Rigidbody ball in BallsInField)
                if(ball.velocity.magnitude > 0.1)
                    return false;
            
            return true;
        }
    }

    [HideInInspector]
    public List<Rigidbody> BallsInField = new List<Rigidbody>();

    public void ActivatePowerup() => 
        GetComponent<AudioSource>().PlayOneShot(Powerup);

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Ball")
            BallsInField.Add(c.GetComponent<Rigidbody>());
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Ball")
        {
            BallsInField.Remove(c.GetComponent<Rigidbody>());
            Destroy(c.gameObject);

            // Plays death sound
            GetComponent<AudioSource>().PlayOneShot(Death);
        }
    }
}
