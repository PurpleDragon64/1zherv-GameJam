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

    private EdgeCollider2D edgeCollider;
    

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.sortingLayerName = "Foreground";
        lineRenderer.positionCount = length;
        segPos = new Vector3[length];
        segVel = new Vector3[length];

        edgeCollider = GetComponentInChildren<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldSegpos;

        segPos[0] = target.position;
        Vector2[] edges = new Vector2[length];
        worldSegpos = transform.InverseTransformPoint(segPos[0]);
        edges[0] = new Vector2(worldSegpos.x, worldSegpos.y);

        for(int i = 1; i < length; i++)
        {
            Vector3 targetPos = segPos[i - 1] + (segPos[i] - segPos[i - 1]).normalized * targetDist;
            segPos[i] = Vector3.SmoothDamp(segPos[i], targetPos, ref segVel[i],smoothSpeed);
            worldSegpos = transform.InverseTransformPoint(segPos[i]);
            edges[i] = new Vector2(worldSegpos.x, worldSegpos.y);
		}

        lineRenderer.SetPositions(segPos);
        edgeCollider.points = edges;
    }
}
