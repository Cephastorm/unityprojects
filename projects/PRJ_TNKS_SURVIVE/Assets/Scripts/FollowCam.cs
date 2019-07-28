using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float speed = 0.3f;
    [Range(0,1)]
    public float targetAttractionExtent = 0.24f;
    
    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, 0));

        // Smoothly move the camera towards that target position
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, speed);


    }
}
