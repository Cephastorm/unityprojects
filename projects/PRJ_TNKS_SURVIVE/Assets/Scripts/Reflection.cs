using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Reflection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void OnDrawGizmosSelected()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out RaycastHit hit, Mathf.Infinity))
        {
            Debug.DrawRay(
                transform.position,
                transform.TransformDirection(Vector3.back) * hit.distance,
                Color.yellow, 2);

            Vector3 cross = Vector3.Cross(Vector3.back, hit.normal);
            float length = hit.distance;
            Vector3 normal = Vector3.back.normalized;
            Vector3 forward = transform.TransformDirection(Vector3.back);


            Debug.DrawRay(
              hit.point,
              cross* hit.distance,
              Color.white, 2);

            if (Physics.Raycast(hit.point, hit.normal, out RaycastHit hit2, Mathf.Infinity))
            {
                Debug.DrawRay(
                hit.point,
                Vector3.Reflect(forward,hit.normal),
                Color.green, 2); ;
            }

        }

    }
}

