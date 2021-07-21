using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    // Same-scene "singleton" pattern 
    private static Plunger _instance;
    public static Plunger instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<Plunger>();
            return _instance;
        }
    }

    [SerializeField]
    Animator Spring;

    [Header("Sound Effects")]
    [SerializeField]
    AudioClip stress;

    [SerializeField]
    AudioClip fail;

    [SerializeField]
    AudioClip launch;

    [HideInInspector]
    public List<Rigidbody> ObjectsInSpring = new List<Rigidbody>();

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Ball")
            ObjectsInSpring.Add(c.GetComponent<Rigidbody>());
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Ball")
            ObjectsInSpring.Remove(c.GetComponent<Rigidbody>());
    }

    public void Retract()
    {
        PlaySound("STRESS");
        Spring.Play("Stress");
    }

    public void Fail() => PlaySound("FAIL");

    public void Release()
    {
        PlaySound("LAUNCH");
        Spring.Play("Release");
    }

    void PlaySound(string soundKey)
    {
        GetComponent<AudioSource>().Stop();
        
        switch(soundKey)
        {
            case "LAUNCH":
                GetComponent<AudioSource>().PlayOneShot(launch);
                break;
            case "FAIL":
                GetComponent<AudioSource>().PlayOneShot(fail);
                break;
            case "STRESS":
                GetComponent<AudioSource>().PlayOneShot(stress);
                break;
        }
    } 

}
