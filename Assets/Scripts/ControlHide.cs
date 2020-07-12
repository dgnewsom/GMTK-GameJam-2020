using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHide: MonoBehaviour
{
    private GameObject ControlsUI;
    public GameObject icon;
    public AudioSource pickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ControlsUI = GameObject.FindGameObjectWithTag("ControlUI");
            //ControlsUI.SetActive(false);
            StartCoroutine(HideControl());
            pickup.Play();
            icon.SetActive(false);
        }
    }

    IEnumerator HideControl()
    {
        ControlsUI.SetActive(false);
        yield return new WaitForSeconds(5);
        ControlsUI.SetActive(true);
        Destroy(gameObject);

    }
}
