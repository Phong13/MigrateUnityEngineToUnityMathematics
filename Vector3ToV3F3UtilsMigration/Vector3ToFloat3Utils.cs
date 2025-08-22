using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace MigrateToUnityMathematics
{
    public static class Vector3ToFloat3Utils
    {
        public static float magnitude(Vector3 v)
        {
            return v.magnitude;
        }
        public static float magnitude(float3 v)
        {
            return math.length(v);
        }
        public static float sqrMagnitude(Vector3 v)
        {
            return v.sqrMagnitude;
        }
        public static float sqrMagnitude(float3 v)
        {
            return math.lengthsq(v);
        }
        public static Vector3 normalized(Vector3 v)
        {
            System.Diagnostics.Debug.Assert(v != Vector3.zero, "Cannot normalize a zero vector.");
            return v.normalized;
        }
        public static float3 normalized(float3 v)
        {
            System.Diagnostics.Debug.Assert(!(v.x == 0 && v.y == 0 && v.z == 0), "Cannot normalize a zero vector.");
            return math.normalize(v);
        }

        //public static float3 forward => new float3(0, 0, 1);
        public static Vector3 forward => new Vector3(0, 0, 1);
        //public static float3 back => new float3(0, 0, -1);
        public static Vector3 back => new Vector3(0, 0, -1);
        //public static float3 up => new float3(0, 1, 0);
        public static Vector3 up => new Vector3(0, 1, 0);
        //public static float3 down => new float3(0, -1, 0);
        public static Vector3 down => new Vector3(0, -1, 0);
        //public static float3 right => new float3(1, 0, 0);
        public static Vector3 right => new Vector3(1, 0, 0);
        //public static float3 left => new float3(-1, 0, 0);
        public static Vector3 left => new Vector3(-1, 0, 0);
        //public static float3 one => new float3(1, 1, 1);
        public static Vector3 one => new Vector3(1, 1, 1);
        //public static float3 zero => new float3(0, 0, 0);
        public static Vector3 zero => new Vector3(0, 0, 0);

        public static bool Equals(Vector3 v1, Vector3 v2)
        {
            return v1 == v2;
        }
        public static bool Equals(float3 v1, float3 v2)
        {
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
        }
        public static bool NotEquals(Vector3 v1, Vector3 v2)
        {
            return v1 != v2;
        }
        public static bool NotEquals(float3 v1, float3 v2)
        {
            return v1.x != v2.x || v1.y != v2.y || v1.z != v2.z;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle_deg(Vector3 v1, Vector3 v2)
        {
            return Vector3.Angle(v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle_deg(float3 v1, float3 v2)
        {
            float3 v1Normalized = math.normalize(v1);
            float3 v2Normalized = math.normalize(v2);
            float dotProduct = math.dot(v1Normalized, v2Normalized);
            return math.degrees(math.acos(math.clamp(dotProduct, -1f, 1f)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            return Vector3.Cross(v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 Cross(float3 v1, float3 v2)
        {
            return math.cross(v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector3 v1, Vector3 v2)
        {
            return Vector3.Distance(v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(float3 v1, float3 v2)
        {
            return math.distance(v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vector3 v1, Vector3 v2)
        {
            return Vector3.Dot(v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(float3 v1, float3 v2)
        {
            return math.dot(v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(Vector3 v1, Vector3 v2, float t)
        {
            return Vector3.Lerp(v1, v2, t);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 Lerp(float3 v1, float3 v2, float t)
        {
            t = math.clamp(t, 0f, 1f);
            return math.lerp(v1, v2, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Max(Vector3 v1, Vector3 v2)
        {
            return Vector3.Max(v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 Max(float3 v1, float3 v2)
        {
            return math.max(v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Min(Vector3 v1, Vector3 v2)
        {
            return Vector3.Min(v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 Min(float3 v1, float3 v2)
        {
            return math.min(v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 MoveTowards(Vector3 cur, Vector3 targ, float maxDistanceDelta)
        {
            return Vector3.MoveTowards(cur, targ, maxDistanceDelta);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 MoveTowards(float3 cur, float3 targ, float maxDistanceDelta)
        {
            float3 delta = targ - cur;
            float dist = math.length(delta);
            if (dist <= maxDistanceDelta || dist == 0f)
            {
                return targ;
            }
            return cur + (delta / dist) * maxDistanceDelta;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Normalize(ref Vector3 v)
        {
            v.Normalize();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Normalize(ref float3 v)
        {
            v = math.normalize(v);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalize(Vector3 v)
        {
            v.Normalize();
            return v;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 Normalize(float3 v)
        {
            return math.normalize(v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Project(Vector3 v1, Vector3 v2)
        {
            return Vector3.Project(v1, v2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 Project(float3 v1, float3 v2)
        {
            return math.project(v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectOnPlane(Vector3 v, Vector3 planeNormal)
        {
            return Vector3.ProjectOnPlane(v, planeNormal);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ProjectOnPlane(float3 v, float3 planeNormal)
        {
            return v - math.project(v, planeNormal);
        }
    }
}
