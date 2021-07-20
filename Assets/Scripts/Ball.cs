using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Powerup materials")]
    [SerializeField]
    Material Fireball;
    
    [SerializeField]
    Material WaterDroplet;
    
    [SerializeField]
    Material LuckyCharm;
    
    [SerializeField]
    Material CurseOfAnubis;
    
    void Awake() => GetComponent<TrailRenderer>().enabled = false;

    public void ActivatePowerup(Items powerup)
    {
        GetComponent<TrailRenderer>().enabled = (powerup != Items.NoItem);

        switch(powerup)
        {
            case Items.Fireball:
                GetComponent<TrailRenderer>().material = Fireball;
                break;
            
            case Items.WaterDroplet:
                GetComponent<TrailRenderer>().material = WaterDroplet;
                break;
            
            case Items.LuckyCharm:
                GetComponent<TrailRenderer>().material = LuckyCharm;
                break;
            
            case Items.CurseOfAnubis:
                GetComponent<TrailRenderer>().material = CurseOfAnubis;
                break;
        }
    }

    void OnCollisionEnter(Collision c)
    {
        BoostObject bo = c.gameObject.GetComponent<BoostObject>();
        if (bo != null)
        {
            Vector3 dir = c.contacts[0].point - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * bo.BoostForce);
        }

        GetComponent<AudioSource>().Play();

        ScoringObject so = c.gameObject.GetComponent<ScoringObject>();
        if (so != null) Player.instance.IncrementScore(so.IncrementValue);
    }
}
