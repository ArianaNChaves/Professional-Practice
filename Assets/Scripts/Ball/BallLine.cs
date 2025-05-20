using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BallLine : MonoBehaviour
{
    [SerializeField] private Transform target;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawLine();
    }

    private void DrawLine()
    {
        if (!target) return;

        Vector3 pointA = transform.position;
        Vector3 pointB = target.position;

        _lineRenderer.SetPosition(0, pointA);
        _lineRenderer.SetPosition(1, pointB); 
    }

}
