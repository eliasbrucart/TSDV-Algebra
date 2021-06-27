using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomMath
{ 
    public class Matrix4x4 : MonoBehaviour
    {
        public float m00;
        public float m33;
        public float m23;
        public float m13;
        public float m03;
        public float m32;
        public float m22;
        public float m02;
        public float m12;
        public float m21;
        public float m11;
        public float m01;
        public float m30;
        public float m20;
        public float m10;
        public float m31;

        public Matrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        {
            m00 = column0.x;
            m10 = column0.y;
            m20 = column0.z;
            m30 = column0.w;
            m01 = column1.x;
            m11 = column1.y;
            m21 = column1.z;
            m31 = column1.w;
            m02 = column2.x;
            m12 = column2.y;
            m22 = column2.z;
            m32 = column2.w;
            m03 = column3.x;
            m13 = column3.y;
            m23 = column3.z;
            m33 = column3.w;
        }

        public static Matrix4x4 zero
        {
            get
            {
                return new Matrix4x4(new Vector4(0,0,0,0), new Vector4(0,0,0,0), new Vector4(0,0,0,0), new Vector4(0,0,0,0));
            }
        }

        public static  Matrix4x4 identity
        {
            get
            {
                return new Matrix4x4(new Vector4(1,0,0,0), new Vector4(0,1,0,0), new Vector4(0,0,1,0), new Vector4(0,0,0,1));
            }
        }

        public static Matrix4x4 Rotate(MyQuaternion q)
        {
            Matrix4x4 mat = Matrix4x4.identity;
            mat.m02 = 2.0f * (q.x * q.z) + 2.0f * (q.y * q.w);
            mat.m12 = 2.0f * (q.y * q.z) - 2.0f * (q.x * q.w);
            mat.m22 = 1 - 2.0f * (q.x * q.x) - 2.0f * (q.y * q.y);

            mat.m00 = 1 - 2.0f * (q.y * q.y) - 2.0f * (q.z * q.z);
            mat.m10 = 2.0f * (q.x * q.y) + 2.0f * (q.z * q.w);
            mat.m20 = 2.0f * (q.x * q.z) - 2.0f * (q.y * q.w);

            mat.m01 = 2.0f * (q.x * q.y) - 2.0f * (q.z * q.w);
            mat.m11 = 1 - 2.0f * (q.x * q.x) - 2.0f * (q.z * q.z);
            mat.m21 = 2.0f * (q.y * q.z) + 2.0f * (q.z * q.w);
            return mat;
        }

        public static Matrix4x4 Scale(Vector3 vector)
        {
            Matrix4x4 mat = Matrix4x4.identity;
            mat.m00 = vector.x;
            mat.m11 = vector.y;
            mat.m22 = vector.z;
            mat.m33 = 1;
            return mat;
        }
    }
}


