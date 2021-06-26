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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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

        public static MyQuaternion FromToRotation(Vec3 from)
        {
            throw new NotImplementedException();
        }

        public static MyQuaternion Inverse(MyQuaternion rotation)
        {
            MyQuaternion inverse = new MyQuaternion(-rotation.x, -rotation.y, -rotation.z, rotation.w);
            return inverse;
        }

        public static MyQuaternion Lerp(MyQuaternion a, MyQuaternion b, float t)
        {
            throw new NotImplementedException();
        }

        public static MyQuaternion LerpUnclamped(MyQuaternion a, MyQuaternion b, float t)
        {
            throw new NotImplementedException();
        }

        public static MyQuaternion LookRotation(Vec3 forward)
        {
            throw new NotImplementedException();
        }

        public static MyQuaternion LookRotation(Vec3 forward, Vec3 upwards)
        {
            throw new NotImplementedException();
        }

        public static MyQuaternion Normalize(MyQuaternion q)
        {
            throw new NotImplementedException();
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

        public void Normalized()
        {
            throw new NotImplementedException();
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

