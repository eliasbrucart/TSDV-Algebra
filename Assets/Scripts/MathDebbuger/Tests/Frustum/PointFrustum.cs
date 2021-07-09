using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFrustum : MonoBehaviour
{
    [SerializeField] private GameObject frustumPrefab;
    private LineRenderer line;
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, frustumPrefab.transform.position);
    }
}
