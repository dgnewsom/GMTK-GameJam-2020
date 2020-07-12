using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public PlayerState playerState;
    public Rigidbody2D car;
   
    [SerializeField] float currentSpeed;
    [SerializeField] float accel = 60f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float drift = 0.9f;
    //public UpdateControlsUI controlUI;
    Dictionary<string, KeyCode> controlsMap = new Dictionary<string, KeyCode>();
    private KeyCode[] keys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

    public Dictionary<string, KeyCode> ControlsMap { get => controlsMap; set => controlsMap = value; }

    public bool isRunning = false;

    [Header("AI Controls")]
    public bool AI = false;
    public float randomShiftTime = 5f;
    [SerializeField] float currentRandomShiftTime = -1;

    // Start is called before the first frame update
    void Start()
    {

        isRunning = false;
        ResetControls();

    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            //randomise controls on space
            /*
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RandomiseControls();
            }


            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetControls();
            }
            */


            if (Input.GetKey(ControlsMap["Forward"]))
            {

                car.GetComponent<Rigidbody2D>().AddForce(transform.up * accel * Time.deltaTime * car.mass * 100);
                //Debug.Log("Forward");
               
            }

            if (Input.GetKey(ControlsMap["Backward"]))
            {
                //Debug.Log("Backward");
                car.GetComponent<Rigidbody2D>().AddForce(transform.up * (accel * -1) * Time.deltaTime * car.mass * 100);


            }

            if (Input.GetKey(ControlsMap["Left"]))
            {
                //Debug.Log("Left");
                car.angularVelocity = 1 * turnSpeed;
            }

            if (Input.GetKey(ControlsMap["Right"]))
            {
                //Debug.Log("Right");
                car.angularVelocity = -1 * turnSpeed;
            }
            car.velocity = ForwardVelocity() + SidewaysVelocity() * drift;
            if (car.velocity.magnitude > playerState.getCurrentMaxSpeed())
            {
                car.velocity = car.velocity.normalized * playerState.getCurrentMaxSpeed();
            }
            if (AI)
            {
                AIControl();
               
            }
        }

    }




    Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }

    Vector2 SidewaysVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

    public void RandomiseControls()
    {
        
        //shuffle array
        for (int x = 0; x < keys.Length; x++)
        {
            KeyCode tmp = keys[x];
            int r = Random.Range(x, keys.Length);
            keys[x] = keys[r];
            keys[r] = tmp;
        }

        ControlsMap = new Dictionary<string, KeyCode>();
        ControlsMap.Add("Forward", keys[0]);
        ControlsMap.Add("Backward", keys[1]);
        ControlsMap.Add("Left", keys[2]);
        ControlsMap.Add("Right", keys[3]);
        

    }

    public void ResetControls()
    {
        ControlsMap = new Dictionary<string, KeyCode>();
        ControlsMap.Add("Forward", KeyCode.W);
        ControlsMap.Add("Backward", KeyCode.S);
        ControlsMap.Add("Left", KeyCode.A);
        ControlsMap.Add("Right", KeyCode.D);
       
        }

    void AIControl()
    {
        currentRandomShiftTime -= Time.deltaTime;
        if (currentRandomShiftTime <= 0)
        {
            RandomiseControls();
            currentRandomShiftTime = randomShiftTime;
        }
        if (!Input.anyKeyDown)
        {
            car.GetComponent<Rigidbody2D>().AddForce(transform.up * accel * Time.deltaTime * car.mass * 100);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (AI)
        {
            if (collision.collider.CompareTag("Obstacle"))
            {
                car.angularVelocity = 10 * turnSpeed;
                //transform.Rotate(Vector3.right, 180f);
            }
        }
    }


    public float getAccel()
    {
        return accel;
    }

    public void setAccel(float newAccel)
    {
        accel = newAccel;
    }

}
