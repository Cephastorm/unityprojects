using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveVector.magnitude == 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.right);
            //Debug.Log("Zero movement");
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(
               moveVector, Vector3.up);
        }
    }
}
