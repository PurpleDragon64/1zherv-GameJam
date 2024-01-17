using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform target;
    public float targetDist;
    public float smoothSpeed;
    public int length;

    private Vector3[] segVel;
    private Vector3[] segPos;
    

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.sortingLayerName = "Foreground";
        lineRenderer.positionCount = length;
        segPos = new Vector3[length];
        segVel = new Vector3[length];
    }

    // Update is called once per frame
    void Update()
    {
        segPos[0] = target.position;

        for(int i = 1; i < length; i++)
        {
            Vector3 targetPos = segPos[i - 1] + (segPos[i] - segPos[i - 1]).normalized * targetDist;
            segPos[i] = Vector3.SmoothDamp(segPos[i], targetPos, ref segVel[i],smoothSpeed);
		}

        lineRenderer.SetPositions(segPos);
    }
}
