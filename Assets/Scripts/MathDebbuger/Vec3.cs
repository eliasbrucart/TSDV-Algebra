﻿using UnityEngine;
using System;
namespace CustomMath
{
    public struct Vec3 : IEquatable<Vec3>
    {
        #region Variables
        public float x;
        public float y;
        public float z;

        public float sqrMagnitude 
        { 
            get 
            { 
                return (x*x + y*y + z*z);
            } 
        }
        public Vec3 normalized
        { 
            get 
            { 
                Vec3 v3Aux = new Vec3(x, y, z);
                return (v3Aux / v3Aux.magnitude);
            } 
        }
        public float magnitude { get 
            {
                return (float)Math.Sqrt(sqrMagnitude);
            } 
        }
        #endregion

        #region constants
        public const float epsilon = 1e-05f;
        #endregion

        #region Default Values
        public static Vec3 Zero { get { return new Vec3(0.0f, 0.0f, 0.0f); } }
        public static Vec3 One { get { return new Vec3(1.0f, 1.0f, 1.0f); } }
        public static Vec3 Forward { get { return new Vec3(0.0f, 0.0f, 1.0f); } }
        public static Vec3 Back { get { return new Vec3(0.0f, 0.0f, -1.0f); } }
        public static Vec3 Right { get { return new Vec3(1.0f, 0.0f, 0.0f); } }
        public static Vec3 Left { get { return new Vec3(-1.0f, 0.0f, 0.0f); } }
        public static Vec3 Up { get { return new Vec3(0.0f, 1.0f, 0.0f); } }
        public static Vec3 Down { get { return new Vec3(0.0f, -1.0f, 0.0f); } }
        public static Vec3 PositiveInfinity { get { return new Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); } }
        public static Vec3 NegativeInfinity { get { return new Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); } }
        #endregion                                                                                                                                                                               

        #region Constructors
        public Vec3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3(Vec3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector2 v2)
        {
            this.x = v2.x;
            this.y = v2.y;
            this.z = 0.0f;
        }
        #endregion

        #region Operators
        public static bool operator ==(Vec3 left, Vec3 right)
        {
            float diff_x = left.x - right.x;
            float diff_y = left.y - right.y;
            float diff_z = left.z - right.z;
            float sqrmag = diff_x * diff_x + diff_y * diff_y + diff_z * diff_z;
            return sqrmag < epsilon * epsilon;
        }
        public static bool operator !=(Vec3 left, Vec3 right)
        {
            return !(left == right);
        }

        public static Vec3 operator +(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x + rightV3.x, leftV3.y + rightV3.y, leftV3.z + rightV3.z);
        }

        public static Vec3 operator -(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x - rightV3.x, leftV3.y - leftV3.y, leftV3.z - leftV3.z);
        }

        public static Vec3 operator -(Vec3 v3)
        {
            return new Vec3(-v3.x, -v3.y, -v3.z);
        }

        public static Vec3 operator *(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        public static Vec3 operator *(float scalar, Vec3 v3)
        {
            return new Vec3(scalar * v3.x, scalar * v3.y, scalar * v3.z);
        }
        public static Vec3 operator /(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x / scalar, v3.y / scalar, v3.z / scalar);
        }

        public static implicit operator Vector3(Vec3 v3)
        {
            return new Vector3(v3.x, v3.y, v3.z);
        }

        public static implicit operator Vector2(Vec3 v2)
        {
            return new Vector2(v2.x, v2.y);
        }
        #endregion

        #region Functions
        public override string ToString()
        {
            return "X = " + x.ToString() + "   Y = " + y.ToString() + "   Z = " + z.ToString();
        }
        public static float Angle(Vec3 from, Vec3 to)
        {
            float dot = Dot(from, to);
            float magnitude = Magnitude(from) * Magnitude(to);
            float resultCosine = Mathf.Acos(dot / magnitude);
            return resultCosine  / Mathf.Rad2Deg;
        }
        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength)
        {
            if (Magnitude(vector) > maxLength)
                return vector.normalized * maxLength;
            else
                return vector;
        }
        public static float Magnitude(Vec3 vector)
        {
            return Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));
        }
        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            return new Vec3(((a.y * b.z) - (a.z * b.y)), ((a.z * b.x) - (a.x * b.z)), ((a.x * b.y) - (a.y * b.x)));
        }
        public static float Distance(Vec3 a, Vec3 b)
        {
            return Mathf.Sqrt(Mathf.Pow(b.x - a.x, 2) + Mathf.Pow(b.y - a.y, 2) + Mathf.Pow(b.z - a.z, 2));
        }
        public static float Dot(Vec3 a, Vec3 b)
        {
            return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
        }
        public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
        {
            if(t >= 1)
                return b;
            return new Vec3(a + (b - a) * t);
        }
        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t)
        {
            return new Vec3(a + (b - a) * t);
        }
        public static Vec3 Max(Vec3 a, Vec3 b)
        {
            float newX = a.x;
            float newY = a.y;
            float newZ = a.z;
            if (b.x > newX)
                newX = b.x;
            if (b.y > newY)
                newY = b.y;
            if (b.z > newZ)
                newZ = b.z;
            return new Vec3(newX,newY,newZ);
        }
        public static Vec3 Min(Vec3 a, Vec3 b)
        {
            float newX = a.x;
            float newY = a.y;
            float newZ = a.z;
            if (b.x < newX)
                newX = b.x;
            if (b.y < newY)
                newY = b.y;
            if (b.z < newZ)
                newZ = b.z;
            return new Vec3(newX,newY,newZ);
        }
        public static float SqrMagnitude(Vec3 vector)
        {
            return Mathf.Sqrt(Magnitude(vector));
        }
        public static Vec3 Project(Vec3 vector, Vec3 onNormal) 
        {
            return (Dot(vector, onNormal) / Mathf.Pow(Magnitude(onNormal), 2) * onNormal);
        }
        public static Vec3 Reflect(Vec3 inDirection, Vec3 inNormal) 
        {
            //Vec3 normal = new Vec3(inNormal.normalized);
            inNormal.Normalize();
            return inDirection - 2 * (Dot(inDirection, inNormal)) * inNormal;
        }
        public void Set(float newX, float newY, float newZ)
        {
            x = newX;
            y = newY;
            z = newZ;
        }
        public void Scale(Vec3 scale)
        {
            Set(x*scale.x, y*scale.y, z*scale.z);
        }
        public static Vec3 Scale(Vec3 a, Vec3 b)
        {
            return new Vec3(a.x * b.x, a.y * b.y, a.z * b.z);
        }
        public void Normalize()
        {
            x = x / magnitude;
            y = y / magnitude;
            z = z / magnitude;
        }
        #endregion

        #region Internals
        public override bool Equals(object other)
        {
            if (!(other is Vec3)) return false;
            return Equals((Vec3)other);
        }

        public bool Equals(Vec3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }
        #endregion
    }
}