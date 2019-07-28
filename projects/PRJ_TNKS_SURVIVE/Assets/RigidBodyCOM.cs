using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyCOM : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private GameObject sphere;
    public Vector3 com;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.ResetCenterOfMass();
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale *= 0.1f;
        rb.centerOfMass = com;
        sphere.GetComponent<SphereCollider>().enabled = false;
        sphere.transform.position = rb.worldCenterOfMass;
        sphere.transform.parent = transform;
        sphere.name = transform.name+ " COM Instance";

    }

    // Update is called once per frame
    void Update()
    {
        //rb.centerOfMass=com;
        sphere.transform.localPosition = rb.centerOfMass;
        //Debug.Log("TankCOM: " + rb.centerOfMass);
        //Debug.Log("Transform COM: " + com.localPosition.ToString("F4"));
    }
}
