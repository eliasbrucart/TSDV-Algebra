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
        //    this.x = 0.0f;
        //    this.y = 0.0f;
        //    this.z = 0.0f;
        //    this.w = 0.0f;
        //}

        public MyQuaternion(MyQuaternion quat)
        {
            this.x = quat.x;
            this.y = quat.y;
            this.z = quat.z;
            this.w = quat.w;
        }

        //operators

        //methods
        #region Methods
        public static float Angle(MyQuaternion a, MyQuaternion b)
        {
            throw new NotImplementedException();
        }

        public bool Equals(MyQuaternion other)
        {
            return x == other.x && y == other.y && z == other.z && w == other.w;
        }

        #endregion
    }
}

