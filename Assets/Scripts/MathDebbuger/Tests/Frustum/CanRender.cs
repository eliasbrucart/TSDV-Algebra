using CustomMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanRender : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Vec3 pos;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        pos = new Vec3(transform.position);
    }

    public Vec3 GetPos()
    {
        return pos;
    }

    public void Render(bool state)
    {
        mesh.enabled = state;
    }
}
