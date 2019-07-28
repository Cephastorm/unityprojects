using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject bullet;
    public Transform muzzle;
    public Transform pivotRef;
    public Transform parentBone;
    private GameObject pivot;
    [Range(0,1)]
    public float moveSpeed = 0.2f;
    public float bulletSpeed = 350f;
    private Rigidbody rb;
    //Cool down in milliseconds
    private float coolDown;
    public float bulletCoolDown = 1f;
    System.Timers.Timer timer;
    private bool isTimerActive;
    

    // Start is called before the first frame update
    void Start()
    {

        coolDown = bulletCoolDown;
        rb = GetComponent<Rigidbody>();
        pivot = new GameObject("Pivot");
        pivot.transform.rotation = Quaternion.LookRotation(-Vector3.forward, Vector3.up);
        pivot.transform.position = pivotRef.position;
        pivot.transform.parent = parentBone;
        pivotRef.transform.parent = (Transform)pivot.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(coolDown);
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        LayerMask mask = LayerMask.GetMask("Floor");

        if (Physics.Raycast(ray, out hit,200f,mask))
        {

            Vector3 pointToMouse = new Vector3(hit.point.x - pivot.transform.position.x,
                0, hit.point.z - pivot.transform.position.z);
            Debug.DrawLine(pivot.transform.position, pivot.transform.forward * 50f, Color.red, 0.1f);
            pivot.transform.rotation = Quaternion.LookRotation(pointToMouse, 
            pivot.transform.TransformDirection(Vector3.up));


        }


        if (coolDown <= 0)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
            {
                var inBullet = Instantiate(bullet, muzzle.position, pivot.transform.rotation).GetComponent<Bullet>();
                inBullet.speed = bulletSpeed+rb.velocity.magnitude;
                inBullet.maxBounces = 1;
                coolDown = bulletCoolDown;
            }
        }

        
        
    }

    private void FixedUpdate()
    {
       
        coolDown -= Time.deltaTime;
       



            //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            //{
            //    Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime;
            //    rb.MovePosition(transform.position + moveVector);
            //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-moveVector, Vector3.up), 0.2f);
            //    Debug.Log("Moving object");

            //}
        
      


    }

}
