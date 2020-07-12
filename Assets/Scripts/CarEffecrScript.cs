using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffecrScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private ParticleSystem jamEffect;
    [SerializeField] private ParticleSystem[] driveEffects;
    [SerializeField] private bool driving = false;
    public AudioSource collisionSound;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //print(driving);
    }

    public void playJam()
    {
        jamEffect.Play();
    }

    public void playDriveEffect()
    {
        if (driving)
        {

            foreach (ParticleSystem driveEffect in driveEffects)
            {
                if (!driveEffect.isPlaying)
                {
                    driveEffect.Play();

                }

            }
        }
        else if (!driving)
        {
            foreach (ParticleSystem driveEffect in driveEffects)
            {
                if (driveEffect.isPlaying)
                {

                driveEffect.Stop();
                }

            }
        }
    }

    public void setDriving(bool b)
    {
        driving = b;

        playDriveEffect();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionSound.Play();
    }
}
