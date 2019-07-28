using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(LineRenderer))]
public class TurretController : MonoBehaviour
{
    // Start is called before the first frame update

    private HingeJoint hj;
    private Rigidbody rb;
    private LineRenderer lr;
    public bool bDrawLine = true;
    public Transform bulletSpawn;
    public GameObject bullet;
    public float bulletSpeed;
    private float coolDown;
    public float bulletCoolDown = 1f;
    JointSpring jointSpring;
    public Transform tankBody;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        rb.centerOfMass = Vector3.zero;
        hj = GetComponent<HingeJoint>();
        hj.axis = Vector3.up;
        hj.useSpring = true;
        jointSpring = hj.spring;



    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        LayerMask mask = LayerMask.GetMask("Floor");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {

            Vector3 pointToMouse = new Vector3(hit.point.x - transform.position.x,
                0, hit.point.z - transform.position.z);
            float firstDot = Vector3.Dot(Vector3.Cross(-tankBody.transform.forward, pointToMouse.normalized), Vector3.up);
            float secondDot = Vector3.Dot(-tankBody.transform.forward, pointToMouse.normalized);
            //float signedTheta = Mathf.Atan2(firstDot, secondDot) * (180.0f / Mathf.PI);
            float targetAngle = Mathf.Atan2(firstDot, secondDot) * (180.0f / Mathf.PI);

            jointSpring.targetPosition = targetAngle;
            hj.spring = jointSpring;

            //Debug.Log(jointSpring.targetPosition);
            if (bDrawLine)
            {
                Vector3[] pos = { transform.position, hit.point };
                lr.SetPositions(pos);
            }


            if (coolDown <= 0)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
                {
                    var inBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.LookRotation(-transform.forward, Vector3.up)).GetComponent<Bullet>();
                    inBullet.speed = bulletSpeed * (1 + rb.velocity.magnitude);
                    inBullet.maxBounces = 1;
                    coolDown = bulletCoolDown;
                }
            }




        }
    }
    private void FixedUpdate()
    {
        coolDown -= Time.deltaTime;

    }
}
