using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankContoller : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private GameObject sphere;
    public Transform com;
    public float moveSpeed = 2f;
    public float turnSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.ResetCenterOfMass();
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale *= 0.1f;

        rb.centerOfMass = com.transform.localPosition;
        sphere.GetComponent<SphereCollider>().enabled = false;
        sphere.transform.position = rb.worldCenterOfMass;
        sphere.transform.parent = transform;
        sphere.name = "Tank COM Instance";

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {

        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveVector != Vector3.zero)
        {
            rb.MovePosition(transform.position + moveVector * moveSpeed * Time.deltaTime);
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-moveVector, Vector3.up), 0.1f));
        }

    }




}
