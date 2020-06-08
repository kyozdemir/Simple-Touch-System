using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float smoothness;
    [SerializeField]
    float heightTolerance = 20f;
    Vector3 offset;

    float lowY;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        offset = transform.position - target.position;
        lowY = transform.position.y+heightTolerance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothness*Time.deltaTime);
        if (transform.position.y < lowY )
        {
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
        }
    }
}
