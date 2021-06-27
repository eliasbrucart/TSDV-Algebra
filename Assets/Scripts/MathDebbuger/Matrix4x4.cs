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
                return new Matrix4x4(new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0));
            }
        }

        public static Matrix4x4 identity
        {
            get
            {
                return new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
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

        public static Matrix4x4 Scale(Vec3 vector)
        {
            Matrix4x4 mat = Matrix4x4.identity;
            mat.m00 = vector.x;
            mat.m11 = vector.y;
            mat.m22 = vector.z;
            mat.m33 = 1;
            return mat;
        }

        public static Matrix4x4 Translate(Vec3 vector)
        {
            Matrix4x4 mat = Matrix4x4.identity;
            mat.m03 = vector.x;
            mat.m12 = vector.y;
            mat.m23 = vector.z;
            mat.m33 = 1;
            return mat;
        }

        public static Matrix4x4 TRS(Vec3 pos, MyQuaternion q, Vec3 scale)
        {
            Matrix4x4 trs = Matrix4x4.zero;
            trs = Matrix4x4.Translate(pos) * Matrix4x4.Rotate(q) * Matrix4x4.Scale(scale);
            return trs;
        }

        public static Vector4 operator *(Matrix4x4 lhs, Vector4 vector)
        {
            Vector4 vec;
            vec.x = (lhs.m00 * vector.x) + (lhs.m01 * vector.y) + (lhs.m02 * vector.z) + (lhs.m03 * vector.w);
            vec.y = (lhs.m10 * vector.x) + (lhs.m11 * vector.y) + (lhs.m12 * vector.z) + (lhs.m13 * vector.w);
            vec.z = (lhs.m20 * vector.x) + (lhs.m21 * vector.y) + (lhs.m22 * vector.z) + (lhs.m23 * vector.w);
            vec.w = (lhs.m30 * vector.x) + (lhs.m31 * vector.y) + (lhs.m32 * vector.z) + (lhs.m33 * vector.w);
            return vec;
        }

        public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            Matrix4x4 mat = new Matrix4x4(Vector4.zero, Vector4.zero, Vector4.zero, Vector4.zero);
            mat.m00 = (lhs.m00 * rhs.m00) + (lhs.m01 * rhs.m10) + (lhs.m02 * rhs.m20) + (lhs.m03 * rhs.m30);
            mat.m01 = (lhs.m00 * rhs.m01) + (lhs.m01 * rhs.m11) + (lhs.m02 * rhs.m21) + (lhs.m03 * rhs.m31);
            mat.m02 = (lhs.m00 * rhs.m02) + (lhs.m01 * rhs.m12) + (lhs.m02 * rhs.m22) + (lhs.m03 * rhs.m32);
            mat.m02 = (lhs.m00 * rhs.m03) + (lhs.m01 * rhs.m13) + (lhs.m02 * rhs.m23) + (lhs.m03 * rhs.m33);

            mat.m10 = (lhs.m10 * rhs.m00) + (lhs.m11 * rhs.m10) + (lhs.m12 * rhs.m20) + (lhs.m13 * rhs.m30);
            mat.m11 = (lhs.m10 * rhs.m01) + (lhs.m11 * rhs.m11) + (lhs.m12 * rhs.m21) + (lhs.m13 * rhs.m31);
            mat.m12 = (lhs.m10 * rhs.m02) + (lhs.m11 * rhs.m12) + (lhs.m12 * rhs.m22) + (lhs.m13 * rhs.m32);
            mat.m13 = (lhs.m10 * rhs.m03) + (lhs.m11 * rhs.m13) + (lhs.m12 * rhs.m23) + (lhs.m13 * rhs.m33);

            mat.m20 = (lhs.m20 * rhs.m00) + (lhs.m21 * rhs.m10) + (lhs.m22 * rhs.m20) + (lhs.m23 * rhs.m30);
            mat.m21 = (lhs.m20 * rhs.m01) + (lhs.m21 * rhs.m11) + (lhs.m22 * rhs.m21) + (lhs.m23 * rhs.m31);
            mat.m22 = (lhs.m20 * rhs.m02) + (lhs.m21 * rhs.m12) + (lhs.m22 * rhs.m22) + (lhs.m23 * rhs.m32);
            mat.m23 = (lhs.m20 * rhs.m03) + (lhs.m21 * rhs.m13) + (lhs.m22 * rhs.m23) + (lhs.m23 * rhs.m33);

            mat.m30 = (lhs.m30 * rhs.m00) + (lhs.m31 * lhs.m10) + (lhs.m32 * rhs.m20) + (lhs.m33 * rhs.m30);
            mat.m31 = (lhs.m30 * rhs.m01) + (lhs.m31 * lhs.m11) + (lhs.m32 * rhs.m21) + (lhs.m33 * rhs.m31);
            mat.m32 = (lhs.m30 * rhs.m02) + (lhs.m31 * lhs.m12) + (lhs.m32 * rhs.m22) + (lhs.m33 * rhs.m32);
            mat.m33 = (lhs.m30 * rhs.m03) + (lhs.m31 * lhs.m13) + (lhs.m32 * rhs.m23) + (lhs.m33 * rhs.m33);

            return mat;
        }

        public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            return (lhs.m00 == rhs.m00 && lhs.m01 == rhs.m01 && lhs.m02 == rhs.m02 && lhs.m03 == rhs.m03
                && lhs.m10 == rhs.m10 && lhs.m11 == rhs.m11 && lhs.m12 == rhs.m12 && lhs.m13 == rhs.m13
                && lhs.m20 == rhs.m20 && lhs.m21 == rhs.m21 && lhs.m22 == rhs.m22 && lhs.m23 == rhs.m23
                && lhs.m30 == rhs.m30 && lhs.m31 == rhs.m31 && lhs.m32 == rhs.m32 && lhs.m33 == rhs.m33);
        }
        public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            return (lhs.m00 != rhs.m00 || lhs.m01 != rhs.m01 || lhs.m02 != rhs.m02 || lhs.m03 != rhs.m03
                && lhs.m10 != rhs.m10 || lhs.m11 != rhs.m11 || lhs.m12 != rhs.m12 || lhs.m13 != rhs.m13
                && lhs.m20 != rhs.m20 || lhs.m21 != rhs.m21 || lhs.m22 != rhs.m22 || lhs.m23 != rhs.m23
                && lhs.m30 != rhs.m30 || lhs.m31 != rhs.m31 || lhs.m32 != rhs.m32 || lhs.m33 != rhs.m33);
        }

    }
}


