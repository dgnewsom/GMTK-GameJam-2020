using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModifier : MonoBehaviour
{
    [SerializeField] PlayerState playerState;

    [SerializeField] private string modifierName;
    public string ModifierName { get => modifierName; set => modifierName = value; }
    
    [SerializeField] private float modifierValue;
    public float ModifierValue { get => modifierValue; set => modifierValue = value; }
    public PlayerState PlayerState { get => playerState; set => playerState = value; }

    public bool pickUp = false;
    private void Awake()
    {
        /*
        if (PlayerState.Equals(null))
        {
            PlayerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
        }
        */
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!pickUp)
        {
        PlayerState.addMaxSpeedMod(gameObject.GetComponent<SpeedModifier>());

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!pickUp)
        {
        PlayerState.removeMaxSpeedMod(gameObject.GetComponent<SpeedModifier>());

        }

    }

    public void addSpeed()
    {
        PlayerState.addMaxSpeedMod(gameObject.GetComponent<SpeedModifier>());

    }

    public void removeSpeed()
    {
        PlayerState.removeMaxSpeedMod(gameObject.GetComponent<SpeedModifier>());

    }

    public void setPlayerState(PlayerState p)
    {
        playerState = p;
    }
}
