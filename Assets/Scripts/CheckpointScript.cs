using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [Header("Logic")]
    [SerializeField] private bool passed;
    public bool Passed { get => passed; }
    public Transform pointTo;
    public bool willRandom;
    [Header("Effects")]
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private SpriteRenderer[] lights;
    public AudioSource checkpointSound;
    [Header("Power ups")]
    public bool spawnPowerUp = true;
    public GameObject[] powerupList;
    [SerializeField] private Transform[] spawnLocations;


    private void Awake()
    {
        if (spawnPowerUp)
        {
            spawnPowerups();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !passed)
        {
            passed = true;
            setLight();
            particleSystem.Play();
            print("Passed Checkpoint: " + transform.position);
            if (willRandom)
            {
                collision.gameObject.GetComponent<PlayerState>().randomiseControls();
                checkpointSound.Play();
            }
        }

    }



    public void resetCheckpoint()
    {
        passed = false;
        setLight();
    }


    void setLight()
    {
        foreach (SpriteRenderer r in lights)
        {
            if (passed)
            {
                r.material.SetFloat("_ShiftValue", 1.1f);

            }
            else
            {
                r.material.SetFloat("_ShiftValue", -0.1f);

            }
        }
    }

    public Vector3 getPoint()
    {
        return pointTo.position;
    }


    void spawnPowerups()
    {
        GameObject temp;
        GameObject tempPowerUp;

        foreach (Transform t in spawnLocations)
        {
            temp = powerupList[(int)Math.Floor(UnityEngine.Random.Range(0, powerupList.Length - 0.001f))];
            tempPowerUp = Instantiate(temp, t.position, Quaternion.identity, gameObject.transform);
        }
    }
}
