using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateControlsUI : MonoBehaviour
{
    public CarController playerCar;

    [Header("Buttons")]

    public GameObject Button_Up;
    public GameObject Button_Left;
    public GameObject Button_Down;
    public GameObject Button_Right;

    [Header("Sprits")]

    public GameObject Sprites_Forward;
    public GameObject Sprites_Left;
    public GameObject Sprites_Back;
    public GameObject Sprites_Right;






    private void Update()
    {
        //rotateControlsToCar();
        UpdateControls();
    }

    public void UpdateControls()
    {
        Dictionary<string, KeyCode> controlsMap = playerCar.ControlsMap;
        /*
        up.text = controlsMap["Forward"].ToString();
        left.text = controlsMap["Left"].ToString();
        down.text = controlsMap["Backward"].ToString();
        right.text = controlsMap["Right"].ToString();
        */
        foreach(KeyValuePair<string,KeyCode> control in controlsMap)
        {
            if (control.Value.Equals(KeyCode.W))
            {
                setImageLocation(Button_Up.transform, control.Key);
            }
            else if(control.Value.Equals(KeyCode.S))
            {
                setImageLocation(Button_Down.transform, control.Key);

            }
            else if (control.Value.Equals(KeyCode.A))
            {
                setImageLocation(Button_Left.transform, control.Key);

            }
            else if (control.Value.Equals(KeyCode.D))
            {
                setImageLocation(Button_Right.transform, control.Key);

            }

        }
    }

    void rotateControlsToCar()
    {
        transform.rotation = playerCar.transform.rotation;
        Sprites_Forward.transform.up = Vector2.up;
        Sprites_Left.transform.up = Vector2.up;
        Sprites_Right.transform.up = Vector2.up;
        Sprites_Back.transform.up = Vector2.up;

    }

    void setImageLocation(Transform t, string s)
    {
        if (s.Equals("Forward"))
        {
            Sprites_Forward.transform.position = t.position;
        } else if (s.Equals("Left"))
        {
            Sprites_Left.transform.position = t.position;
        } else if (s.Equals("Right"))
        {
            Sprites_Right.transform.position = t.position;
        }
        else if (s.Equals("Backward"))
        {
            Sprites_Back.transform.position = t.position;
        }
    }
}
