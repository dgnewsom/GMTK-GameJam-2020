using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerState : MonoBehaviour
{
    public Rigidbody2D rb;
    public CarController carController;
    public CarEffecrScript carEffecrScript;
    [Header("Health")]
    public float MaxHealth = 100;
    [SerializeField] private float currentHealth;
    [SerializeField] private bool DEAD;
    [Header("Speed")]
    public float MaxSpeed = 0;
    [SerializeField] private float currentMaxSpeed = 0;
    [SerializeField] private float maxSpeedMod = 1;
    [SerializeField] private bool slowed = false;
    //[SerializeField] private Dictionary<string, float> speedModDic= new Dictionary<string,float>();
    [SerializeField] private ArrayList speedModDic = new ArrayList();

    [Header("Sounds")]
    public AudioSource revving;
    public AudioSource backgroundRevs;

    [Header("Other")]
    public bool immune = true;

    public bool Slowed { get => slowed; set => slowed = value; }


    // Start is called before the first frame update
    void Start()
    {
        if (rb.Equals(null))
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (carController.Equals(null))
        {
            carController = GetComponent<CarController>();
        }
        currentHealth = MaxHealth;
        currentMaxSpeed = MaxSpeed*maxSpeedMod;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentHealth <= 0)
        {
            DEAD = true;
        }
        updateMaxSpeed();
        playDrivingEffect();

        SetRevSound();

    }

    private void SetRevSound()
    {
        float curSpeed = getSpeed();
        if(curSpeed < 10)
        {
            if (!backgroundRevs.isPlaying)
            {
                backgroundRevs.Play();
                revving.Stop();
            }
        }
        else
        {
            if (!revving.isPlaying)
            {
                backgroundRevs.Stop();
                revving.Play();
            }
        }

        revving.volume = (curSpeed / 100) + 0.2f ;
    }

    public float takeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            DEAD = true;
        }
        return currentHealth;
        
    }

    public bool isDead()
    {
        return DEAD;
    }

    public float getSpeed()
    {
        return rb.velocity.magnitude;
    }

    public float getCurrentMaxSpeed()
    {
        return currentMaxSpeed;
    }


    public float getHealth()
    {
        return currentHealth;
    }

    void updateMaxSpeed()
    {
        maxSpeedMod = 1;
        foreach (SpeedModifier mod in speedModDic)
        {
            maxSpeedMod = maxSpeedMod * mod.ModifierValue;
        }
        currentMaxSpeed = MaxSpeed * maxSpeedMod;
        //print(speedModDic.ToString());

    }

    public void addMaxSpeedMod(SpeedModifier mod)
    {
        if (!speedModDic.Contains(mod))
        {
            speedModDic.Add(mod);
        }
    }

    public void removeMaxSpeedMod(SpeedModifier mod)
    {
        
        if (speedModDic.Contains(mod))
        {
            speedModDic.Remove(mod);
        }
    }

    public void randomiseControls()
    {
        if (!immune)
        {
            carController.RandomiseControls();
        }
        carEffecrScript.playJam();
    }

    void playDrivingEffect()
    {
        //print((getSpeed() >= MaxSpeed) + ",  " + getSpeed());
        if (getSpeed()>= MaxSpeed*1.1f)
        {
            carEffecrScript.setDriving(true);
        }
        else
        {
            carEffecrScript.setDriving(false);

        }
    }  
}
