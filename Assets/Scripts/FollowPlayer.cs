using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    //public Vector2 camera


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -2f)+calculateOffset();
    }

    Vector3 calculateOffset()
    {
        Vector3 returnOffset = player.rotation * offset;
        return returnOffset;
    }
}
