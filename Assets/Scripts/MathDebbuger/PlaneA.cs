using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomMath
{
    [Serializable]
    public struct PlaneA
    {
        public Vec3 normal { get; set; }
        public float distance { get; set; }
        public PlaneA flipped
        {
            get
            {
                return new PlaneA(-normal, -normal * distance);
            }
        }
        public PlaneA(Vec3 inNormal, Vec3 inPoint)
        {
            normal = inNormal.normalized;
            distance = -Vec3.Dot(normal, inPoint);
        }
    
        public PlaneA(Vec3 inNormal, float d)
        {
            
        }
    
        public PlaneA(Vec3 a, Vec3 b, Vec3 c)
        {
            Vec3 sideA = b - a;
            Vec3 sideB = c - a;

            normal = Vec3.Cross(sideA, sideB).normalized;
            distance = -Vec3.Dot(normal, a);
        }

        public static PlaneA Translate(PlaneA planea, Vec3 translation)
        {
            Vec3 aux = - (planea.normal * planea.distance + translation);
            return new PlaneA(planea.normal, aux);
        }

        public Vec3 ClosetsPointOnPlane(Vec3 point)
        {
            return (point - normal * GetDistanceToPoint(point));
        }

        public void Flip()
        {
            normal = -normal;
            distance = -distance;
        }

        public float GetDistanceToPoint(Vec3 point)
        {
            return Vec3.Dot(normal, point) + distance;
        }

        public bool GetSide(Vec3 point)
        {
            if ((Vec3.Dot(normal, point) + distance) > 0)
                return true;
            else
                return false;
        }

        public bool SameSide(Vec3 in0, Vec3 in1)
        {
            if (GetSide(in0) == GetSide(in1))
                return true;
            else
                return false;
        }

        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            Vec3 side1 = b - a;
            Vec3 side2 = c - a;

            normal = Vec3.Cross(side1, side2).normalized;
            distance = -Vec3.Dot(normal, a);
        }

        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            normal = inNormal.normalized;
            distance = -Vec3.Dot(inNormal, inPoint);
        }

        public void Translate(Vec3 translation)
        {
            Vec3 aux = (normal * distance + translation);
            distance = Vec3.Dot(normal, aux);
        }

        public override string ToString()
        {
            return "(normal:("+normal.x+", "+normal.y+","+ normal.z+"), distance:"+distance+")";
        }
    }
}

