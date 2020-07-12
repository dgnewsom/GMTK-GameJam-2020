using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetControlsBooster : MonoBehaviour
{
    public CarController player;
    public GameObject icon;

    public AudioSource pickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            player = collision.GetComponent<CarController>();
            player.ResetControls();
            pickup.Play();
            print("Playing");
            icon.SetActive(false);

            StartCoroutine(delayedDestroy(3f));
        }
    }

    IEnumerator delayedDestroy(float f)
    {

        yield return new WaitForSeconds(f);

        Destroy(gameObject);

    }
}
