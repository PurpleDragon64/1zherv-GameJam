using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float newX = Mathf.Lerp(transform.position.x, followTarget.position.x, Time.deltaTime * followSpeed);
        float newY = Mathf.Lerp(transform.position.y, followTarget.position.y, Time.deltaTime * followSpeed);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
