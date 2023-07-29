using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCamFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraOffset;
    public float smoothFactor = 0.5f;
    public bool lookAtTarget = false;


    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - target.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        
        Vector3 newPosition = target.transform.position + cameraOffset;
        // transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
        transform.position = Vector3.Slerp(transform.position, new Vector3(37.5f, cameraOffset.y + target.position.y, cameraOffset.z + target.position.z), smoothFactor); 

        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }
}
