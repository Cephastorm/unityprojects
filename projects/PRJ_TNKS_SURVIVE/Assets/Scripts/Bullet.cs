using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [Range(0, 1000)]
    public float speed = 100f;
    public Vector3 nextForwardVector;
    [Range(1, 4)]
    public int maxBounces = 2;
    private List<Vector3> directions = new List<Vector3>();
    Vector3 currentForward;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentForward = transform.TransformDirection(Vector3.forward);
        // Debug.Log("Start called");
        Vector3 previousForward = transform.TransformDirection(Vector3.forward);
        Vector3 previousHitPoint = transform.position;
        for (int i = 0; i < maxBounces; i++)
        {
            LayerMask mask = LayerMask.GetMask("Ricochet");
            if (Physics.Raycast(previousHitPoint, previousForward, out RaycastHit hit, Mathf.Infinity, mask))
            {
                Debug.DrawRay(
                    previousHitPoint,
                    previousForward * hit.distance,
                    Color.yellow, 2000);
                //Debug.Log("We hit something, Chief");
                previousForward = Vector3.Reflect(previousForward, hit.normal);
                directions.Add(previousForward);
                previousHitPoint = hit.point;
                directions.Add(previousHitPoint);
            }
        }



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += currentForward * Time.deltaTime * speed * 0.01f;
        //transform.Rotate(currentForward, 12);


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ricochet"))
        {

            if (index < directions.Count)
            {
                //Debug.Log(other.gameObject.name);
                currentForward = directions[index];
                transform.rotation = Quaternion.LookRotation(currentForward, Vector3.up);
                Vector3 currentVelocity = Vector3.zero;
                transform.position = Vector3.SmoothDamp(transform.position, directions[index + 1], ref currentVelocity, 20);
                index += 2;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Destructable"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }
}
