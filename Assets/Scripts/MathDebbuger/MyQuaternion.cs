using System;
using UnityEngine;

namespace CustomMath
{
    public struct MyQuaternion : IEquatable<MyQuaternion>
    {
        public const float kEpsilon = 1E-06F;
        public float x;
        public float y;
        public float z;
        public float w;

        public static MyQuaternion identity { get { return new MyQuaternion(0, 0, 0, 1); } }
        //public Vec3 eulerAngles { get; set; }
        //public MyQuaternion normalized { get; }

        //constructors
        public MyQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        //public MyQuaternion()
        //{
        //    this.x = 0;
        //    this.y = 0;
        //    this.z = 0;
        //    this.w = 0;
        //}

        public MyQuaternion(MyQuaternion quat)
        {
            this.x = quat.x;
            this.y = quat.y;
            this.z = quat.z;
            this.w = quat.w;
        }

        public float this[int index] { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        //operators
        public static implicit operator Quaternion(MyQuaternion q)
        {
            return new Quaternion(q.x, q.y, q.z, q.w);
        }

        public static implicit operator MyQuaternion(Quaternion q)
        {
            return new MyQuaternion(q.x, q.y, q.z, q.w);
        }

        //methods
        #region Methods
        public static float Angle(MyQuaternion a, MyQuaternion b)
        {
            MyQuaternion inverse = MyQuaternion.Inverse(a);
            MyQuaternion result = b * inverse;
            float angle = Mathf.Acos(result.w) * 2.0f * Mathf.Rad2Deg;
            return angle;
        }

        public static MyQuaternion AngleAxis(float angle, Vec3 axis)
        {
            angle *= Mathf.Deg2Rad;
            axis.Normalize();
            MyQuaternion result = new MyQuaternion
            {
                x = axis.x * Mathf.Sin(angle * 0.5f),
                y = axis.y * Mathf.Sin(angle * 0.5f),
                z = axis.y * Mathf.Sin(angle * 0.5f),
                w = Mathf.Cos(angle * 0.5f)
            };
            result.Normalize();

            return result;
        }

        public static MyQuaternion AxisAngle(Vec3 axis, float angle)
        {
            throw new NotImplementedException();
        }

        public static float Dot(MyQuaternion a, MyQuaternion b)
        {
            return (a.x * b.x) + (a.y * b.y) + (a.z * b.z) + (a.w * b.w);
        }

        public static MyQuaternion Euler(float x, float y, float z)
        {
            float rad = Mathf.Deg2Rad;
            x *= rad;
            y *= rad;
            z *= rad;
            MyQuaternion q = new MyQuaternion();
            q.x = Mathf.Sin(x * 0.5f);
            q.y = Mathf.Sin(y * 0.5f);
            q.z = Mathf.Sin(z * 0.5f);
            q.w = Mathf.Cos(x * 0.5f) * Mathf.Cos(y * 0.5f) * Mathf.Cos(z * 0.5f) - Mathf.Sin(x * 0.5f) * Mathf.Sin(y * 0.5f) * Mathf.Sin(z * 0.5f);
            q.Normalize();
            return q;
        }

        public static MyQuaternion EulerAngles(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public static MyQuaternion EulerAngles(Vec3 euler)
        {
            throw new NotImplementedException();
        }

        public static MyQuaternion EulerRotation(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public static MyQuaternion EulerRotation(Vec3 euler)
        {
            throw new NotImplementedException();
        }

        public static MyQuaternion FromToRotation(Vec3 from, Vec3 to)
        {
            Vec3 cross = Vec3.Cross(from, to);
            MyQuaternion result = MyQuaternion.identity;
            result.x = cross.x;
            result.y = cross.y;
            result.z = cross.z;
            result.w = Mathf.Sqrt(from.magnitude * to.magnitude) * Mathf.Sqrt(from.magnitude * to.magnitude) + Vec3.Dot(from, to);
            result.Normalize();
            return result;
        }

        public static MyQuaternion Inverse(MyQuaternion rotation)
        {
            MyQuaternion inverse = new MyQuaternion(-rotation.x, -rotation.y, -rotation.z, rotation.w);
            return inverse;
        }

        public static MyQuaternion Lerp(MyQuaternion a, MyQuaternion b, float t)
        {
            MyQuaternion q = new MyQuaternion();
            if(t < 1)
            {
                q.x = ((b.x - a.x) * t + a.x);
                q.y = ((b.y - a.y) * t + a.y);
                q.z = ((b.z - a.z) * t + a.z);
                q.w = ((b.w - a.w) * t + a.w);
            }
            else
            {
                t = 1.0f;
            }
            q.Normalize();
            return q;
        }

        public static MyQuaternion LerpUnclamped(MyQuaternion a, MyQuaternion b, float t)
        {
            MyQuaternion q = new MyQuaternion();
            q.x = ((b.x - a.x) * t + a.x);
            q.y = ((b.y - a.y) * t + a.y);
            q.z = ((b.z - a.z) * t + a.z);
            q.w = ((b.w - a.w) * t + a.w);
            q.Normalize();
            return q;
        }

        public static MyQuaternion LookRotation(Vec3 forward)
        {
            throw new NotImplementedException();
        }

        public static MyQuaternion LookRotation(Vec3 forward, Vec3 upwards)
        {
            MyQuaternion result;
            if (forward == Vec3.Zero)
            {
                result = MyQuaternion.identity;
                return result;
            }
            if (upwards != forward)
            {
                upwards.Normalize();
                Vec3 a = forward + upwards * -Vec3.Dot(forward, upwards);
                MyQuaternion q = MyQuaternion.FromToRotation(Vec3.Forward, a);
                return MyQuaternion.FromToRotation(a, forward) * q;
            }
            else
            {
                return MyQuaternion.FromToRotation(Vec3.Forward, forward);
            }
        }

        public static MyQuaternion Normalize(MyQuaternion q)
        {
            float magnitude = Mathf.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
            q.x /= magnitude;
            q.y /= magnitude;
            q.z /= magnitude;
            q.w /= magnitude;
            return q;
        }

        public static MyQuaternion RotateTowards(MyQuaternion from, MyQuaternion to, float maxDegreesDelta)
        {
            throw new NotImplementedException();
        }

        public static MyQuaternion Slerp(MyQuaternion a, MyQuaternion b, float t)
        {
            throw new NotImplementedException();
        }

        public static MyQuaternion SlerpUnclamped(MyQuaternion a, MyQuaternion b, float t)
        {
            throw new NotImplementedException();
        }

        public static Vec3 ToEulersAngles(MyQuaternion rotation)
        {
            throw new NotImplementedException();
        }

        public bool Equals(MyQuaternion other)
        {
            return x == other.x && y == other.y && z == other.z && w == other.w;
        }

        public override bool Equals(object other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public void Normalize()
        {
            float magnitude = Mathf.Sqrt(x * x + y * y + z * z + w * w);
            x /= magnitude;
            y /= magnitude;
            z /= magnitude;
            w /= magnitude;
        }

        public void Set(float newX, float newY, float newZ, float newW)
        {
            throw new NotImplementedException();
        }

        public void SetAxisAngles(Vec3 axis, float angle)
        {
            throw new NotImplementedException();
        }

        public void SetEulerAngles(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void SetEulerRotation(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void SetEulerRotation(Vec3 euler)
        {
            throw new NotImplementedException();
        }

        public void SetFromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            throw new NotImplementedException();
        }

        public void SetLookRotation(Vec3 view, Vec3 up)
        {
            throw new NotImplementedException();
        }

        public void SetLookRotation(Vec3 view)
        {
            throw new NotImplementedException();
        }

        public void ToAngleAxis(out float angle, out Vec3 axis)
        {
            throw new NotImplementedException();
        }

        public void ToAngleAxis(out Vec3 axis, out float angle)
        {
            throw new NotImplementedException();
        }

        public Vec3 ToEuler()
        {
            throw new NotImplementedException();
        }

        public Vec3 ToEulerAngles()
        {
            throw new NotImplementedException();
        }

        public string ToString(string format)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region operators

        public static Vec3 operator *(MyQuaternion rotation, Vec3 point)
        {
            throw new NotImplementedException();
        }

        public static MyQuaternion operator *(MyQuaternion lhs, MyQuaternion rhs)
        {
            float x = (lhs.w * rhs.x) + (lhs.x * rhs.w) + (lhs.y * rhs.z) - (lhs.z * rhs.y);
            float y = (lhs.w * rhs.y) + (lhs.x * rhs.z) + (lhs.y * rhs.w) - (lhs.z * rhs.x);
            float z = (lhs.w * rhs.z) + (lhs.x * rhs.y) + (lhs.y * rhs.x) - (lhs.z * rhs.w);
            float w = (lhs.w * rhs.w) + (lhs.x * rhs.x) + (lhs.y * rhs.y) - (lhs.z * rhs.z);
            return new MyQuaternion(x,y,z,w);
        }

        public static bool operator ==(MyQuaternion lhs, MyQuaternion rhs)
        {
            return (lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w);
        }

        public static bool operator !=(MyQuaternion lhs, MyQuaternion rhs)
        {
            return (lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z || lhs.w != rhs.w);
        }

        #endregion
    }
}

