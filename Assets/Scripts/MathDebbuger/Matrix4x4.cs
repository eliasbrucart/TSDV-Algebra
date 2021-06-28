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

        public static Matrix4x4 Transpose(Matrix4x4 original)
        {
            Matrix4x4 mat = original;
            mat.m10 = original.m01;
            mat.m01 = original.m10;
            mat.m20 = original.m02;
            mat.m02 = original.m20;
            mat.m21 = original.m12;
            mat.m12 = original.m21;
            mat.m30 = original.m03;
            mat.m03 = original.m30;
            mat.m31 = original.m13;
            mat.m13 = original.m31;
            mat.m32 = original.m23;
            mat.m23 = original.m32;

            return mat;
        }

        public void Transpose()
        {
            m10 = m01;
            m01 = m10;
            m20 = m02;
            m02 = m20;
            m21 = m12;
            m12 = m21;
            m30 = m03;
            m03 = m30;
            m31 = m13;
            m13 = m31;
            m32 = m23;
            m23 = m32;

            /* 
              matriz original =            Matriz transpuesta =               
                m00, m01, m02, m03          m00, m10, m20, m30
                m10, m11, m12, m13          m01, m11, m21, m31
                m20, m21, m22, m23          m02, m12, m22, m32
                m30, m31, m32, m33          m03, m13, m23, m33             
           */
        }

        public Vector4 GetRow(int RowPos)
        {
            Vector4 RowToReturn = Vector4.zero;
            switch (RowPos)
            {
                case 0:
                    RowToReturn = new Vector4(m00, m01, m02, m03);
                    break;
                case 1:
                    RowToReturn = new Vector4(m10, m11, m12, m13);
                    break;
                case 2:
                    RowToReturn = new Vector4(m20, m21, m22, m23);
                    break;
                case 3:
                    RowToReturn = new Vector4(m30, m31, m32, m33);
                    break;
                default:

                    break;
            }

            return RowToReturn;
        }

    }
}


