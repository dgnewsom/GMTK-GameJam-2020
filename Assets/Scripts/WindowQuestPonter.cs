using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowQuestPonter : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private RectTransform pointerRectransform;
    public Transform PlayerCar;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = PlayerCar.position;
        fromPosition.z = 0f;
        Vector3 dir = toPosition - fromPosition;
        pointerRectransform.localEulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, dir));
        //pointerRectransform.forward = dir;
        
        //print(pointerRectransform.localEulerAngles);
    }

    public void setTarget(Vector3 t) 
    {
        targetPosition = new Vector3(t.x, t.y);
    }
}
