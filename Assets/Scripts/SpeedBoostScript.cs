using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostScript : MonoBehaviour
{
    public PlayerState player;
    public GameObject icon;
    public AudioSource pickup;
    public SpeedModifier speedModifier;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerState>();
            //print(player);
            speedModifier.setPlayerState(player);
            print("Boosted!");
            StartCoroutine(increaseSpeed());
            pickup.Play();
            icon.SetActive(false);
        }
    }

    IEnumerator increaseSpeed()
    {
        /*
        */
        float currentAccel = player.carController.getAccel();
        player.carController.setAccel(currentAccel * 2f);
        print("reset to " + currentAccel);

        speedModifier.addSpeed();
        yield return new WaitForSeconds(3);
        speedModifier.removeSpeed();
        player.carController.setAccel(currentAccel);
        Destroy(gameObject);

    }
}
