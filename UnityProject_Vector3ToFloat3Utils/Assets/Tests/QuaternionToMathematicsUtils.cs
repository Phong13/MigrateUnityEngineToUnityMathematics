using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Mathematics;
using System.Net.Security;
public static class QuaternionToMathematicsUtils
{
    public static Quaternion identity => UnityEngine.Quaternion.identity;
    public static quaternion identity_math => quaternion.identity;

    public static Vector3 eulerAngles_deg(Quaternion q)
    {
        return q.eulerAngles;
    }
    public static float3 eulerAngles_deg(quaternion q)
    {
        return math.degrees(math.Euler(math.normalize(q), math.RotationOrder.ZXY));
    }

    public static Quaternion normalized(Quaternion q)
    {
        return q.normalized;
    }
    public static quaternion normalized(quaternion q)
    {
        return math.normalize(q);
    }

    public static float GetIndex(Quaternion q, int index)
    {
        return q[index];
    }
    public static float GetIndex(quaternion q, int index)
    {
        return q.value[index];
    }

    public static void SetIndex(ref Quaternion q, int index, float value)
    {
        q[index] = value;
    }
    public static void SetIndex(ref quaternion q, int index, float value)
    {
        float4 tempValue = q.value;
        tempValue[index] = value;
        q.value = tempValue;
    }

    public static float w(Quaternion q) => q.w;
    public static float w(quaternion q) => q.value.w;
    public static float x(Quaternion q) => q.x;
    public static float x(quaternion q) => q.value.x;
    public static float y(Quaternion q) => q.y;
    public static float y(quaternion q) => q.value.y;
    public static float z(Quaternion q) => q.z;
    public static float z(quaternion q) => q.value.z;

    public static Quaternion Quaternion(float x, float y, float z, float w)
    {
        return new Quaternion(x, y, z, w);
    }
    public static quaternion Quaternion_math(float x, float y, float z, float w)
    {
        return new quaternion(x, y, z, w);
    }

    public static void Set(ref Quaternion q, float x, float y, float z, float w)
    {
        q.Set(x, y, z, w);
    }
    public static void Set(ref quaternion q, float x, float y, float z, float w)
    {
        q = new quaternion(x, y, z, w);
    }

    public static void SetFromToRotation(ref Quaternion q, Vector3 fromDirection, Vector3 toDirection)
    {
        q = UnityEngine.Quaternion.FromToRotation(fromDirection, toDirection);
    }
    public static void SetFromToRotation(ref quaternion q, float3 fromDirection, float3 toDirection)
    {
        float3 fromNorm = math.normalize(fromDirection);
        float3 toNorm = math.normalize(toDirection);

        float dot = math.dot(fromNorm, toNorm);
        dot = math.clamp(dot, -1f, 1f);
        float3 axis = math.cross(fromNorm, toNorm);

        if (math.lengthsq(axis) < 0.0001f)
        {
            axis = new float3(1f, 0f, 0f);
        }
        else
        {
            axis = math.normalize(axis);
        }

        q = quaternion.AxisAngle(axis, math.acos(dot));
    }

    public static void SetLookRotation(ref Quaternion q, Vector3 view, Vector3 up)
    {
        q = UnityEngine.Quaternion.LookRotation(view, up);
    }
    public static void SetLookRotation(ref quaternion q, float3 view, float3 up)
    {
        q = quaternion.LookRotation(math.normalize(view), math.normalize(up));
    }

    public static void ToAngleAxis_deg(ref Quaternion q, out float angle, out Vector3 axis)
    {
        q.ToAngleAxis(out angle, out axis);
    }
    public static void ToAngleAxis_deg(ref quaternion q, out float angle, out float3 axis)
    {
        quaternion qCopy = q;
        qCopy = math.normalize(qCopy);
        qCopy.value.w = math.clamp(qCopy.value.w, -1f, 1f);
        angle = 2f * math.acos(qCopy.value.w);
        float s = math.sqrt(1f - qCopy.value.w * qCopy.value.w);
        if (s < 0.0001f)
        {
            axis = new float3(1f, 0f, 0f);
        }
        else
        {
            axis = new float3(qCopy.value.x / s, qCopy.value.y / s, qCopy.value.z / s);
        }
        angle = math.degrees(angle);
        q = qCopy;
    }

    public static string ToString(Quaternion q)
    {
        return q.ToString();
    }
    public static string ToString(quaternion q)
    {
        return $"({q.value.x:F5}, {q.value.y:F5}, {q.value.z:F5}, {q.value.w:F5})";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Angle_deg(Quaternion a, Quaternion b)
    {
        return UnityEngine.Quaternion.Angle(a, b);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Angle_deg(quaternion a, quaternion b)
    {
        a = math.normalize(a);
        b = math.normalize(b);

        float dot = math.dot(a, b);

        if (dot < 0f)
        {
            dot = -dot;
        }
        dot = math.clamp(dot, -1f, 1f);

        float halfAngle = math.acos(dot);
        return math.degrees(2f * halfAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion AngleAxis_deg(float angle, Vector3 axis)
    {
        return UnityEngine.Quaternion.AngleAxis(angle, axis);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static quaternion AngleAxis_deg(float angle, float3 axis)
    {
        return quaternion.AxisAngle(math.normalize(axis), math.radians(angle));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Dot(Quaternion a, Quaternion b)
    {
        return UnityEngine.Quaternion.Dot(a, b);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Dot(quaternion a, quaternion b)
    {
        a = math.normalize(a);
        b = math.normalize(b);
        return math.dot(a.value, b.value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Euler_deg(float x, float y, float z)
    {
        return UnityEngine.Quaternion.Euler(x, y, z);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static quaternion Euler_deg_math(float x, float y, float z)
    {
        return quaternion.Euler(math.radians(x), math.radians(y), math.radians(z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Euler_deg(Vector3 v)
    {
        return UnityEngine.Quaternion.Euler(v);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static quaternion Euler_deg_math(Vector3 v)
    {
        return quaternion.Euler(math.radians(v.x), math.radians(v.y), math.radians(v.z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
    {
        return UnityEngine.Quaternion.FromToRotation(fromDirection, toDirection);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static quaternion FromToRotation(float3 fromDirection, float3 toDirection)
    {
        float3 axis = math.cross(fromDirection, toDirection);
        float dot = math.clamp(math.dot(math.normalize(fromDirection), math.normalize(toDirection)), -1f, 1f);
        float l = math.length(axis);

        if (l < 1e-7f)
        {
            if (dot > 0f)
            {
                return quaternion.identity;
            }
            else
            {
                float3 perp;
                if (math.abs(fromDirection.x) < math.abs(fromDirection.y) && math.abs(fromDirection.x) < math.abs(fromDirection.z))
                    perp = math.normalize(math.cross(fromDirection, new float3(1, 0, 0)));
                else if (math.abs(fromDirection.y) < math.abs(fromDirection.z))
                    perp = math.normalize(math.cross(fromDirection, new float3(0, 1, 0)));
                else
                    perp = math.normalize(math.cross(fromDirection, new float3(0, 0, 1)));

                return new quaternion(perp.x, perp.y, perp.z, 0f);
            }
        }
        axis = math.normalize(axis);
        float angle_rad = math.acos(dot);
        quaternion qq = quaternion.AxisAngle(axis, angle_rad);
        if (qq.value.w < 0f)
        {
            qq.value = -qq.value;
        }

        return qq;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Inverse(Quaternion rotation)
    {
        return UnityEngine.Quaternion.Inverse(rotation);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static quaternion Inverse(quaternion rotation)
    {
        return math.inverse(rotation);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
    {
        return UnityEngine.Quaternion.Lerp(a, b, t);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static quaternion Lerp(quaternion a, quaternion b, float t)
    {
        t = math.clamp(t, 0f, 1f);
        float4 lerped = math.lerp(a.value, b.value, t);
        return math.normalize(new quaternion(lerped));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)
    {
        return UnityEngine.Quaternion.LerpUnclamped(a, b, t);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static quaternion LerpUnclamped(quaternion a, quaternion b, float t)
    {
        float4 lerped = math.lerp(a.value, b.value, t);
        return math.normalize(new quaternion(lerped));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion LookRotation(Vector3 forward, Vector3 upwards)
    {
        return UnityEngine.Quaternion.LookRotation(forward, upwards);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static quaternion LookRotation(float3 forward, float3 upwards)
    {
        return quaternion.LookRotation(math.normalize(forward), math.normalize(upwards));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion LookRotation(Vector3 forward)
    {
        return UnityEngine.Quaternion.LookRotation(forward);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static quaternion LookRotation(float3 forward)
    {
        return quaternion.LookRotation(math.normalize(forward), new float3(0, 1, 0));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Normalize(ref Quaternion q)
    {
        q = UnityEngine.Quaternion.Normalize(q);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Normalize(ref quaternion q)
    {
        q = math.normalize(q);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
    {
        return UnityEngine.Quaternion.RotateTowards(from, to, maxDegreesDelta);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static quaternion RotateTowards(quaternion from, quaternion to, float maxDegreesDelta)
    {
        float num = math.angle(from, to);
        quaternion result;
        if (num == 0f)
        {
            result = to;
        }
        else
        {
            float t = math.min(1f, math.radians(maxDegreesDelta) / num);
            result = math.slerp(from, to, t);
        }
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
    {
        return UnityEngine.Quaternion.Slerp(a, b, t);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static quaternion Slerp(quaternion a, quaternion b, float t)
    {
        t = math.clamp(t, 0f, 1f);
        return math.slerp(a, b, t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
    {
        return UnityEngine.Quaternion.SlerpUnclamped(a, b, t);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static quaternion SlerpUnclamped(quaternion a, quaternion b, float t)
    {
        return math.slerp(a, b, t);
    }

    public static Quaternion Multiply(Quaternion left, Quaternion right)
    {
        return left * right;
    }
    public static quaternion Multiply(quaternion left, quaternion right)
    {
        return math.mul(left, right);
    }

    public static bool Equals(Quaternion a, Quaternion b)
    {
        return a == b;
    }
    public static bool Equals(quaternion a, quaternion b)
    {
        return a.value.x == b.value.x &&
                a.value.y == b.value.y &&
                a.value.z == b.value.z &&
                a.value.w == b.value.w;
    }
}
