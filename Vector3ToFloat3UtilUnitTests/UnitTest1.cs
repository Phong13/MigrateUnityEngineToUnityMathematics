using NUnit.Framework;
using UnityEngine;
using Unity.Mathematics;
using MigrateToUnityMathematics;

namespace Vector3ToFloat3UtilUnitTests
{
    public class Tests
    {
        [Test]
        public void TestMagnitudeVector1()
        {
            Vector3 v = new Vector3(3f, 4f, 0f);
            float expected = v.magnitude;
            float actual = Vector3ToFloat3Utils.magnitude(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMagnitudeVector2()
        {
            Vector3 v = new Vector3(0f, 0f, 0f);
            float expected = v.magnitude;
            float actual = Vector3ToFloat3Utils.magnitude(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMagnitudeVector3()
        {
            Vector3 v = new Vector3(-3f, 4f, 0f);
            float expected = v.magnitude;
            float actual = Vector3ToFloat3Utils.magnitude(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMagnitudeVector4()
        {
            Vector3 v = new Vector3(100f, -72.5f, -13.17f);
            float expected = v.magnitude;
            float actual = Vector3ToFloat3Utils.magnitude(v);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMagnitudeFloat1()
        {
            float3 v = new float3(3f, 4f, 0f);
            float expected = math.length(v);
            float actual = Vector3ToFloat3Utils.magnitude(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMagnitudeFloat2()
        {
            float3 v = new float3(0f, 0f, 0f);
            float expected = math.length(v);
            float actual = Vector3ToFloat3Utils.magnitude(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMagnitudeFloat3()
        {
            float3 v = new float3(-3f, 4f, 0f);
            float expected = math.length(v);
            float actual = Vector3ToFloat3Utils.magnitude(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMagnitudeFloat4()
        {
            float3 v = new float3(100f, -72.5f, -13.17f);
            float expected = math.length(v);
            float actual = Vector3ToFloat3Utils.magnitude(v);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSqrMagnitudeVector1()
        {
            Vector3 v = new Vector3(3f, 4f, 0f);
            float expected = v.sqrMagnitude;
            float actual = Vector3ToFloat3Utils.sqrMagnitude(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestSqrMagnitudeVector2()
        {
            Vector3 v = new Vector3(0f, 0f, 0f);
            float expected = v.sqrMagnitude;
            float actual = Vector3ToFloat3Utils.sqrMagnitude(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestSqrMagnitudeVector3()
        {
            Vector3 v = new Vector3(-3f, 4f, 0f);
            float expected = v.sqrMagnitude;
            float actual = Vector3ToFloat3Utils.sqrMagnitude(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestSqrMagnitudeVector4()
        {
            Vector3 v = new Vector3(100f, -72.5f, -13.17f);
            float expected = v.sqrMagnitude;
            float actual = Vector3ToFloat3Utils.sqrMagnitude(v);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSqrMagnitudeFloat1()
        {
            float3 v = new float3(3f, 4f, 0f);
            float expected = math.lengthsq(v);
            float actual = Vector3ToFloat3Utils.sqrMagnitude(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestSqrMagnitudeFloat2()
        {
            float3 v = new float3(0f, 0f, 0f);
            float expected = math.lengthsq(v);
            float actual = Vector3ToFloat3Utils.sqrMagnitude(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestSqrMagnitudeFloat3()
        {
            float3 v = new float3(-3f, 4f, 0f);
            float expected = math.lengthsq(v);
            float actual = Vector3ToFloat3Utils.sqrMagnitude(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestSqrMagnitudeFloat4()
        {
            float3 v = new float3(100f, -72.5f, -13.17f);
            float expected = math.lengthsq(v);
            float actual = Vector3ToFloat3Utils.sqrMagnitude(v);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestNormalizedVector1()
        {
            Vector3 v = new Vector3(3f, 4f, 0f);
            Vector3 expected = v.normalized;
            Vector3 actual = Vector3ToFloat3Utils.normalized(v);

            Assert.AreEqual(expected, actual);
        }
        /*
        [Test]
        public void TestNormalizedVector2()
        {
            Vector3 v = new Vector3(0f, 0f, 0f);
            Vector3 expected = v.normalized;
            Vector3 actual = Vector3ToFloat3Utils.normalized(v);
            Console.WriteLine("Expected: " + expected);
            Console.WriteLine("Actual: " + actual);

            Assert.AreEqual(expected, actual);
        }
        */
        [Test]
        public void TestNormalizedVector3()
        {
            Vector3 v = new Vector3(-3f, 4f, 0f);
            Vector3 expected = v.normalized;
            Vector3 actual = Vector3ToFloat3Utils.normalized(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestNormalizedVector4()
        {
            Vector3 v = new Vector3(100f, -72.5f, -13.17f);
            Vector3 expected = v.normalized;
            Vector3 actual = Vector3ToFloat3Utils.normalized(v);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestNormalizedFloat1()
        {
            float3 v = new float3(3f, 4f, 0f);
            float3 expected = math.normalize(v);
            float3 actual = Vector3ToFloat3Utils.normalized(v);

            Assert.AreEqual(expected, actual);
        }
        /*
        [Test]
        public void TestNormalizedFloat2()
        {
            float3 v = new float3(0f, 0f, 0f);
            float3 expected = math.normalize(v);
            float3 actual = Vector3ToFloat3Utils.normalized(v);
            Console.WriteLine("Expected: " + expected);
            Console.WriteLine("Actual: " + actual);

            Assert.AreEqual(expected, actual);
        }
        */
        [Test]
        public void TestNormalizedFloat3()
        {
            float3 v = new float3(-3f, 4f, 0f);
            float3 expected = math.normalize(v);
            float3 actual = Vector3ToFloat3Utils.normalized(v);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestNormalizedFloat4()
        {
            float3 v = new float3(100f, -72.5f, -13.17f);
            float3 expected = math.normalize(v);
            float3 actual = Vector3ToFloat3Utils.normalized(v);

            Assert.AreEqual(expected, actual);
        }

        /*
        [Test]
        public void TestForwardVector()
        {
            Vector3 expected = new Vector3(0, 0, 1);
            Vector3 actual = Vector3ToFloat3Utils.forward_v3;

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestForwardFloat()
        {
            float3 expected = new float3(0, 0, 1);
            float3 actual = Vector3ToFloat3Utils.forward;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestBackVector()
        {
            Vector3 expected = new Vector3(0, 0, -1);
            Vector3 actual = Vector3ToFloat3Utils.back_v3;

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestBackFloat()
        {
            float3 expected = new float3(0, 0, -1);
            float3 actual = Vector3ToFloat3Utils.back;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestUpVector()
        {
            Vector3 expected = new Vector3(0, 1, 0);
            Vector3 actual = Vector3ToFloat3Utils.up_v3;

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestUpFloat()
        {
            float3 expected = new float3(0, 1, 0);
            float3 actual = Vector3ToFloat3Utils.up;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestDownVector()
        {
            Vector3 expected = new Vector3(0, -1, 0);
            Vector3 actual = Vector3ToFloat3Utils.down_v3;

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDownFloat()
        {
            float3 expected = new float3(0, -1, 0);
            float3 actual = Vector3ToFloat3Utils.down;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestRightVector()
        {
            Vector3 expected = new Vector3(1, 0, 0);
            Vector3 actual = Vector3ToFloat3Utils.right_v3;

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestRightFloat()
        {
            float3 expected = new float3(1, 0, 0);
            float3 actual = Vector3ToFloat3Utils.right;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestLeftVector()
        {
            Vector3 expected = new Vector3(-1, 0, 0);
            Vector3 actual = Vector3ToFloat3Utils.left_v3;

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestLeftFloat()
        {
            float3 expected = new float3(-1, 0, 0);
            float3 actual = Vector3ToFloat3Utils.left;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestOneVector()
        {
            Vector3 expected = new Vector3(1, 1, 1);
            Vector3 actual = Vector3ToFloat3Utils.one_v3;

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestOneFloat()
        {
            float3 expected = new float3(1, 1, 1);
            float3 actual = Vector3ToFloat3Utils.one;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestZeroVector()
        {
            Vector3 expected = new Vector3(0, 0, 0);
            Vector3 actual = Vector3ToFloat3Utils.zero_v3;

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestZeroFloat()
        {
            float3 expected = new float3(0, 0, 0);
            float3 actual = Vector3ToFloat3Utils.zero;

            Assert.AreEqual(expected, actual);
        }
        */

        [Test]
        public void TestEqualsVector1()
        {
            Vector3 v1 = new Vector3(1f, 2f, 3f);
            Vector3 v2 = new Vector3(1f, 2f, 3f);
            bool expected = (v1 == v2);
            bool actual = Vector3ToFloat3Utils.equals(v1, v2);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestEqualsVector2()
        {
            Vector3 v1 = new Vector3(0f, 0f, 0f);
            Vector3 v2 = new Vector3(0f, 0f, 0f);
            bool expected = (v1 == v2);
            bool actual = Vector3ToFloat3Utils.equals(v1, v2);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestEqualsVector3()
        {
            Vector3 v1 = new Vector3(-5f, 2.5f, 10f);
            Vector3 v2 = new Vector3(-5f, 2.5f, 10f);
            bool expected = (v1 == v2);
            bool actual = Vector3ToFloat3Utils.equals(v1, v2);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestEqualsVector4()
        {
            Vector3 v1 = new Vector3(1f, 2f, 3f);
            Vector3 v2 = new Vector3(3f, 2f, 1f);
            bool expected = (v1 == v2);
            bool actual = Vector3ToFloat3Utils.equals(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestEqualsFloat1()
        {
            float3 v1 = new float3(1f, 2f, 3f);
            float3 v2 = new float3(1f, 2f, 3f);
            bool expected = (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
            bool actual = Vector3ToFloat3Utils.equals(v1, v2);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestEqualsFloat2()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(0f, 0f, 0f);
            bool expected = (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
            bool actual = Vector3ToFloat3Utils.equals(v1, v2);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestEqualsFloat3()
        {
            float3 v1 = new float3(-5f, 2.5f, 10f);
            float3 v2 = new float3(-5f, 2.5f, 10f);
            bool expected = (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
            bool actual = Vector3ToFloat3Utils.equals(v1, v2);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestEqualsFloat4()
        {
            float3 v1 = new float3(1f, 2f, 3f);
            float3 v2 = new float3(3f, 2f, 1f);
            bool expected = (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
            bool actual = Vector3ToFloat3Utils.equals(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestNotEqualsVector1()
        {
            Vector3 v1 = new Vector3(1f, 2f, 3f);
            Vector3 v2 = new Vector3(3f, 2f, 1f);
            bool expected = (v1 != v2);
            bool actual = Vector3ToFloat3Utils.notEquals(v1, v2);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestNotEqualsVector2()
        {
            Vector3 v1 = new Vector3(0f, 0f, 0f);
            Vector3 v2 = new Vector3(0f, 0f, 0f);
            bool expected = (v1 != v2);
            bool actual = Vector3ToFloat3Utils.notEquals(v1, v2);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestNotEqualsVector3()
        {
            Vector3 v1 = new Vector3(-5f, 2.5f, 10f);
            Vector3 v2 = new Vector3(-5f, 3f, 10f);
            bool expected = (v1 != v2);
            bool actual = Vector3ToFloat3Utils.notEquals(v1, v2);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestNotEqualsVector4()
        {
            Vector3 v1 = new Vector3(1f, 2f, 3f);
            Vector3 v2 = new Vector3(1f, 2f, 3f);
            bool expected = (v1 != v2);
            bool actual = Vector3ToFloat3Utils.notEquals(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestNotEqualsFloat1()
        {
            float3 v1 = new float3(1f, 2f, 3f);
            float3 v2 = new float3(3f, 2f, 1f);
            bool expected = (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z);
            bool actual = Vector3ToFloat3Utils.notEquals(v1, v2);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestNotEqualsFloat2()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(0f, 0f, 0f);
            bool expected = (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z);
            bool actual = Vector3ToFloat3Utils.notEquals(v1, v2);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestNotEqualsFloat3()
        {
            float3 v1 = new float3(-5f, 2.5f, 10f);
            float3 v2 = new float3(-5f, 3f, 10f);
            bool expected = (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z);
            bool actual = Vector3ToFloat3Utils.notEquals(v1, v2);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestNotEqualsFloat4()
        {
            float3 v1 = new float3(1f, 2f, 3f);
            float3 v2 = new float3(1f, 2f, 3f);
            bool expected = (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z);
            bool actual = Vector3ToFloat3Utils.notEquals(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAngle_degVector1()
        {
            Vector3 v1 = new Vector3(1f, 0f, 0f);
            Vector3 v2 = new Vector3(0f, 1f, 0f);
            float expected = Vector3.Angle(v1, v2);
            float actual = Vector3ToFloat3Utils.Angle_deg(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAngle_degVector2()
        {
            Vector3 v1 = new Vector3(1f, 0f, 0f);
            Vector3 v2 = new Vector3(1f, 0f, 0f);
            float expected = Vector3.Angle(v1, v2);
            float actual = Vector3ToFloat3Utils.Angle_deg(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAngle_degVector3()
        {
            Vector3 v1 = new Vector3(1f, 0f, 0f);
            Vector3 v2 = new Vector3(-1f, 0f, 0f);
            float expected = Vector3.Angle(v1, v2);
            float actual = Vector3ToFloat3Utils.Angle_deg(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAngle_degVector4()
        {
            Vector3 v1 = new Vector3(0f, 0f, 0f);
            Vector3 v2 = new Vector3(1f, 0f, 0f);
            float expected = Vector3.Angle(v1, v2);
            float actual = Vector3ToFloat3Utils.Angle_deg(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAngle_degVector5()
        {
            Vector3 v1 = new Vector3(1.3f, -2.4f, 0.7f);
            Vector3 v2 = new Vector3(-0.5f, 4.1f, -3.3f);
            float expected = Vector3.Angle(v1, v2);
            float actual = Vector3ToFloat3Utils.Angle_deg(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAngle_degVector6()
        {
            Vector3 v1 = new Vector3(-7.77f, 0f, 5.22f);
            Vector3 v2 = new Vector3(3.14f, -1.59f, 2.65f);
            float expected = Vector3.Angle(v1, v2);
            float actual = Vector3ToFloat3Utils.Angle_deg(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAngle_degFloat1()
        {
            float3 v1 = new float3(1f, 0f, 0f);
            float3 v2 = new float3(0f, 1f, 0f);
            float expected = Vector3.Angle(new Vector3(v1.x, v1.y, v1.z), new Vector3(v2.x, v2.y, v2.z));
            float actual = Vector3ToFloat3Utils.Angle_deg(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAngle_degFloat2()
        {
            float3 v1 = new float3(1f, 0f, 0f);
            float3 v2 = new float3(1f, 0f, 0f);
            float expected = Vector3.Angle(new Vector3(v1.x, v1.y, v1.z), new Vector3(v2.x, v2.y, v2.z));
            float actual = Vector3ToFloat3Utils.Angle_deg(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAngle_degFloat3()
        {
            float3 v1 = new float3(1f, 0f, 0f);
            float3 v2 = new float3(-1f, 0f, 0f);
            float expected = Vector3.Angle(new Vector3(v1.x, v1.y, v1.z), new Vector3(v2.x, v2.y, v2.z));
            float actual = Vector3ToFloat3Utils.Angle_deg(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAngle_degFloat4()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(1f, 0f, 0f);
            float expected = Vector3.Angle(new Vector3(v1.x, v1.y, v1.z), new Vector3(v2.x, v2.y, v2.z));
            float actual = Vector3ToFloat3Utils.Angle_deg(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAngle_degFloat5()
        {
            float3 v1 = new float3(1.3f, -2.4f, 0.7f);
            float3 v2 = new float3(-0.5f, 4.1f, -3.3f);
            float expected = Vector3.Angle(new Vector3(v1.x, v1.y, v1.z), new Vector3(v2.x, v2.y, v2.z));
            float actual = Vector3ToFloat3Utils.Angle_deg(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAngle_degFloat6()
        {
            float3 v1 = new float3(-7.77f, 0f, 5.22f);
            float3 v2 = new float3(3.14f, -1.59f, 2.65f);
            float expected = Vector3.Angle(new Vector3(v1.x, v1.y, v1.z), new Vector3(v2.x, v2.y, v2.z));
            float actual = Vector3ToFloat3Utils.Angle_deg(v1, v2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCrossVector1()
        {
            Vector3 v1 = new Vector3(1f, 0f, 0f);
            Vector3 v2 = new Vector3(0f, 1f, 0f);
            Vector3 expected = Vector3.Cross(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestCrossVector2()
        {
            Vector3 v1 = new Vector3(1f, 2f, 3f);
            Vector3 v2 = new Vector3(4f, 5f, 6f);
            Vector3 expected = Vector3.Cross(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestCrossVector3()
        {
            Vector3 v1 = new Vector3(-7.7f, 0f, 5.2f);
            Vector3 v2 = new Vector3(3.1f, -1.5f, 2.6f);
            Vector3 expected = Vector3.Cross(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestCrossVector4()
        {
            Vector3 v1 = new Vector3(0f, 0f, 0f);
            Vector3 v2 = new Vector3(1f, 1f, 1f);
            Vector3 expected = Vector3.Cross(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestCrossVector5()
        {
            Vector3 v1 = new Vector3(2.5f, -3.5f, 4.5f);
            Vector3 v2 = new Vector3(-1.5f, 3.5f, -4.5f);
            Vector3 expected = Vector3.Cross(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestCrossVector6()
        {
            Vector3 v1 = new Vector3(100f, 200f, 300f);
            Vector3 v2 = new Vector3(-100f, 0f, 50f);
            Vector3 expected = Vector3.Cross(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCrossVector7()
        {
            Vector3 v1 = new Vector3(0f, 0f, 0f);
            Vector3 v2 = new Vector3(0f, 0f, 0f);
            Vector3 expected = Vector3.Cross(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCrossFloat1()
        {
            float3 v1 = new float3(1f, 0f, 0f);
            float3 v2 = new float3(0f, 1f, 0f);
            float3 expected = math.cross(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestCrossFloat2()
        {
            float3 v1 = new float3(1f, 2f, 3f);
            float3 v2 = new float3(4f, 5f, 6f);
            float3 expected = math.cross(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestCrossFloat3()
        {
            float3 v1 = new float3(-7.7f, 0f, 5.2f);
            float3 v2 = new float3(3.1f, -1.5f, 2.6f);
            float3 expected = math.cross(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestCrossFloat4()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(1f, 1f, 1f);
            float3 expected = math.cross(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestCrossFloat5()
        {
            float3 v1 = new float3(2.5f, -3.5f, 4.5f);
            float3 v2 = new float3(-1.5f, 3.5f, -4.5f);
            float3 expected = math.cross(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestCrossFloat6()
        {
            float3 v1 = new float3(100f, 200f, 300f);
            float3 v2 = new float3(-100f, 0f, 50f);
            float3 expected = math.cross(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        public void TestCrossFloat7()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(0f, 0f, 0f);
            float3 expected = math.cross(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Cross(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestDistanceVector1()
        {
            Vector3 v1 = new Vector3(0f, 0f, 0f);
            Vector3 v2 = new Vector3(3f, 4f, 0f);
            float expected = Vector3.Distance(v1, v2);
            float actual = Vector3ToFloat3Utils.Distance(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDistanceVector2()
        {
            Vector3 v1 = new Vector3(-1.5f, 2.3f, 7.1f);
            Vector3 v2 = new Vector3(2.5f, -1.3f, 3.1f);
            float expected = Vector3.Distance(v1, v2);
            float actual = Vector3ToFloat3Utils.Distance(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDistanceVector3()
        {
            Vector3 v1 = new Vector3(100f, 200f, 300f);
            Vector3 v2 = new Vector3(-100f, 0f, 50f);
            float expected = Vector3.Distance(v1, v2);
            float actual = Vector3ToFloat3Utils.Distance(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDistanceVector4()
        {
            Vector3 v1 = new Vector3(1f, 1f, 1f);
            Vector3 v2 = new Vector3(1f, 1f, 1f);
            float expected = Vector3.Distance(v1, v2);
            float actual = Vector3ToFloat3Utils.Distance(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDistanceVector5()
        {
            Vector3 v1 = new Vector3(5.5f, -6.6f, 7.7f);
            Vector3 v2 = new Vector3(-5.5f, 6.6f, -7.7f);
            float expected = Vector3.Distance(v1, v2);
            float actual = Vector3ToFloat3Utils.Distance(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDistanceVector6()
        {
            Vector3 v1 = new Vector3(0f, 0f, 0f);
            Vector3 v2 = new Vector3(0f, 0f, 0f);
            float expected = Vector3.Distance(v1, v2);
            float actual = Vector3ToFloat3Utils.Distance(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestDistanceFloat1()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(3f, 4f, 0f);
            float expected = math.distance(v1, v2);
            float actual = Vector3ToFloat3Utils.Distance(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDistanceFloat2()
        {
            float3 v1 = new float3(-1.5f, 2.3f, 7.1f);
            float3 v2 = new float3(2.5f, -1.3f, 3.1f);
            float expected = math.distance(v1, v2);
            float actual = Vector3ToFloat3Utils.Distance(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDistanceFloat3()
        {
            float3 v1 = new float3(100f, 200f, 300f);
            float3 v2 = new float3(-100f, 0f, 50f);
            float expected = math.distance(v1, v2);
            float actual = Vector3ToFloat3Utils.Distance(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDistanceFloat4()
        {
            float3 v1 = new float3(1f, 1f, 1f);
            float3 v2 = new float3(1f, 1f, 1f);
            float expected = math.distance(v1, v2);
            float actual = Vector3ToFloat3Utils.Distance(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDistanceFloat5()
        {
            float3 v1 = new float3(5.5f, -6.6f, 7.7f);
            float3 v2 = new float3(-5.5f, 6.6f, -7.7f);
            float expected = math.distance(v1, v2);
            float actual = Vector3ToFloat3Utils.Distance(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDistanceFloat6()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(0f, 0f, 0f);
            float expected = math.distance(v1, v2);
            float actual = Vector3ToFloat3Utils.Distance(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestDotVector1()
        {
            Vector3 v1 = new Vector3(1f, 0f, 0f);
            Vector3 v2 = new Vector3(0f, 1f, 0f);
            float expected = Vector3.Dot(v1, v2);
            float actual = Vector3ToFloat3Utils.Dot(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDotVector2()
        {
            Vector3 v1 = new Vector3(1f, 2f, 3f);
            Vector3 v2 = new Vector3(4f, 5f, 6f);
            float expected = Vector3.Dot(v1, v2);
            float actual = Vector3ToFloat3Utils.Dot(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDotVector3()
        {
            Vector3 v1 = new Vector3(-7.7f, 0f, 5.2f);
            Vector3 v2 = new Vector3(3.1f, -1.5f, 2.6f);
            float expected = Vector3.Dot(v1, v2);
            float actual = Vector3ToFloat3Utils.Dot(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDotVector4()
        {
            Vector3 v1 = new Vector3(0f, 0f, 0f);
            Vector3 v2 = new Vector3(1f, 1f, 1f);
            float expected = Vector3.Dot(v1, v2);
            float actual = Vector3ToFloat3Utils.Dot(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDotVector5()
        {
            Vector3 v1 = new Vector3(2.5f, -3.5f, 4.5f);
            Vector3 v2 = new Vector3(-1.5f, 3.5f, -4.5f);
            float expected = Vector3.Dot(v1, v2);
            float actual = Vector3ToFloat3Utils.Dot(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDotVector6()
        {
            Vector3 v1 = new Vector3(100f, 200f, 300f);
            Vector3 v2 = new Vector3(-100f, 0f, 50f);
            float expected = Vector3.Dot(v1, v2);
            float actual = Vector3ToFloat3Utils.Dot(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestDotFloat1()
        {
            float3 v1 = new float3(1f, 0f, 0f);
            float3 v2 = new float3(0f, 1f, 0f);
            float expected = math.dot(v1, v2);
            float actual = Vector3ToFloat3Utils.Dot(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDotFloat2()
        {
            float3 v1 = new float3(1f, 2f, 3f);
            float3 v2 = new float3(4f, 5f, 6f);
            float expected = math.dot(v1, v2);
            float actual = Vector3ToFloat3Utils.Dot(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDotFloat3()
        {
            float3 v1 = new float3(-7.7f, 0f, 5.2f);
            float3 v2 = new float3(3.1f, -1.5f, 2.6f);
            float expected = math.dot(v1, v2);
            float actual = Vector3ToFloat3Utils.Dot(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDotFloat4()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(1f, 1f, 1f);
            float expected = math.dot(v1, v2);
            float actual = Vector3ToFloat3Utils.Dot(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDotFloat5()
        {
            float3 v1 = new float3(2.5f, -3.5f, 4.5f);
            float3 v2 = new float3(-1.5f, 3.5f, -4.5f);
            float expected = math.dot(v1, v2);
            float actual = Vector3ToFloat3Utils.Dot(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDotFloat6()
        {
            float3 v1 = new float3(100f, 200f, 300f);
            float3 v2 = new float3(-100f, 0f, 50f);
            float expected = math.dot(v1, v2);
            float actual = Vector3ToFloat3Utils.Dot(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestLerpVector1()
        {
            Vector3 v1 = new Vector3(0f, 0f, 0f);
            Vector3 v2 = new Vector3(10f, 10f, 10f);
            float t = 0f;
            Vector3 expected = Vector3.Lerp(v1, v2, t);
            Vector3 actual = Vector3ToFloat3Utils.Lerp(v1, v2, t);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestLerpVector2()
        {
            Vector3 v1 = new Vector3(0f, 0f, 0f);
            Vector3 v2 = new Vector3(10f, 10f, 10f);
            float t = 0.5f;
            Vector3 expected = Vector3.Lerp(v1, v2, t);
            Vector3 actual = Vector3ToFloat3Utils.Lerp(v1, v2, t);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestLerpVector3()
        {
            Vector3 v1 = new Vector3(1f, 2f, 3f);
            Vector3 v2 = new Vector3(4f, 5f, 6f);
            float t = 0.75f;
            Vector3 expected = Vector3.Lerp(v1, v2, t);
            Vector3 actual = Vector3ToFloat3Utils.Lerp(v1, v2, t);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestLerpVector4()
        {
            Vector3 v1 = new Vector3(-10f, 20f, -30f);
            Vector3 v2 = new Vector3(30f, -20f, 10f);
            float t = 0.33f;
            Vector3 expected = Vector3.Lerp(v1, v2, t);
            Vector3 actual = Vector3ToFloat3Utils.Lerp(v1, v2, t);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestLerpVector5()
        {
            Vector3 v1 = new Vector3(100f, -50f, 0f);
            Vector3 v2 = new Vector3(-100f, 50f, 0f);
            float t = 1f;
            Vector3 expected = Vector3.Lerp(v1, v2, t);
            Vector3 actual = Vector3ToFloat3Utils.Lerp(v1, v2, t);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestLerpVector6()
        {
            Vector3 v1 = new Vector3(0.5f, 0.25f, 0.75f);
            Vector3 v2 = new Vector3(0.25f, 0.5f, 0.125f);
            float t = 0.6f;
            Vector3 expected = Vector3.Lerp(v1, v2, t);
            Vector3 actual = Vector3ToFloat3Utils.Lerp(v1, v2, t);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestLerpFloat1()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(10f, 10f, 10f);
            float t = 0f;
            float3 expected = math.lerp(v1, v2, t);
            float3 actual = Vector3ToFloat3Utils.Lerp(v1, v2, t);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestLerpFloat2()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(10f, 10f, 10f);
            float t = 0.5f;
            float3 expected = math.lerp(v1, v2, t);
            float3 actual = Vector3ToFloat3Utils.Lerp(v1, v2, t);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestLerpFloat3()
        {
            float3 v1 = new float3(1f, 2f, 3f);
            float3 v2 = new float3(4f, 5f, 6f);
            float t = 0.75f;
            float3 expected = math.lerp(v1, v2, t);
            float3 actual = Vector3ToFloat3Utils.Lerp(v1, v2, t);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestLerpFloat4()
        {
            float3 v1 = new float3(-10f, 20f, -30f);
            float3 v2 = new float3(30f, -20f, 10f);
            float t = 0.33f;
            float3 expected = math.lerp(v1, v2, t);
            float3 actual = Vector3ToFloat3Utils.Lerp(v1, v2, t);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestLerpFloat5()
        {
            float3 v1 = new float3(100f, -50f, 0f);
            float3 v2 = new float3(-100f, 50f, 0f);
            float t = 1f;
            float3 expected = math.lerp(v1, v2, t);
            float3 actual = Vector3ToFloat3Utils.Lerp(v1, v2, t);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestLerpFloat6()
        {
            float3 v1 = new float3(0.5f, 0.25f, 0.75f);
            float3 v2 = new float3(0.25f, 0.5f, 0.125f);
            float t = 0.6f;
            float3 expected = math.lerp(v1, v2, t);
            float3 actual = Vector3ToFloat3Utils.Lerp(v1, v2, t);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestMaxVector1()
        {
            Vector3 v1 = new Vector3(1f, 5f, 3f);
            Vector3 v2 = new Vector3(2f, 4f, 6f);
            Vector3 expected = Vector3.Max(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Max(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMaxVector2()
        {
            Vector3 v1 = new Vector3(-1f, -5f, -3f);
            Vector3 v2 = new Vector3(-2f, -4f, -6f);
            Vector3 expected = Vector3.Max(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Max(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMaxVector3()
        {
            Vector3 v1 = new Vector3(0f, 0f, 0f);
            Vector3 v2 = new Vector3(0f, 0f, 0f);
            Vector3 expected = Vector3.Max(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Max(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMaxVector4()
        {
            Vector3 v1 = new Vector3(100f, 200f, 300f);
            Vector3 v2 = new Vector3(50f, 400f, 250f);
            Vector3 expected = Vector3.Max(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Max(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMaxVector5()
        {
            Vector3 v1 = new Vector3(-100f, 0f, 50f);
            Vector3 v2 = new Vector3(-50f, -10f, 60f);
            Vector3 expected = Vector3.Max(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Max(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMaxVector6()
        {
            Vector3 v1 = new Vector3(1.1f, 2.2f, 3.3f);
            Vector3 v2 = new Vector3(3.3f, 2.2f, 1.1f);
            Vector3 expected = Vector3.Max(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Max(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMaxFloat1()
        {
            float3 v1 = new float3(1f, 5f, 3f);
            float3 v2 = new float3(2f, 4f, 6f);
            float3 expected = math.max(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Max(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestMaxFloat2()
        {
            float3 v1 = new float3(-1f, -5f, -3f);
            float3 v2 = new float3(-2f, -4f, -6f);
            float3 expected = math.max(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Max(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestMaxFloat3()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(0f, 0f, 0f);
            float3 expected = math.max(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Max(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestMaxFloat4()
        {
            float3 v1 = new float3(100f, 200f, 300f);
            float3 v2 = new float3(50f, 400f, 250f);
            float3 expected = math.max(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Max(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestMaxFloat5()
        {
            float3 v1 = new float3(-100f, 0f, 50f);
            float3 v2 = new float3(-50f, -10f, 60f);
            float3 expected = math.max(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Max(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestMaxFloat6()
        {
            float3 v1 = new float3(1.1f, 2.2f, 3.3f);
            float3 v2 = new float3(3.3f, 2.2f, 1.1f);
            float3 expected = math.max(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Max(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestMinVector1()
        {
            Vector3 v1 = new Vector3(1f, 5f, 3f);
            Vector3 v2 = new Vector3(2f, 4f, 6f);
            Vector3 expected = Vector3.Min(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Min(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMinVector2()
        {
            Vector3 v1 = new Vector3(-1f, -5f, -3f);
            Vector3 v2 = new Vector3(-2f, -4f, -6f);
            Vector3 expected = Vector3.Min(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Min(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMinVector3()
        {
            Vector3 v1 = new Vector3(0f, 0f, 0f);
            Vector3 v2 = new Vector3(0f, 0f, 0f);
            Vector3 expected = Vector3.Min(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Min(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMinVector4()
        {
            Vector3 v1 = new Vector3(100f, 200f, 300f);
            Vector3 v2 = new Vector3(50f, 400f, 250f);
            Vector3 expected = Vector3.Min(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Min(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMinVector5()
        {
            Vector3 v1 = new Vector3(-100f, 0f, 50f);
            Vector3 v2 = new Vector3(-50f, -10f, 60f);
            Vector3 expected = Vector3.Min(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Min(v1, v2);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestMinVector6()
        {
            Vector3 v1 = new Vector3(1.1f, 2.2f, 3.3f);
            Vector3 v2 = new Vector3(3.3f, 2.2f, 1.1f);
            Vector3 expected = Vector3.Min(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Min(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMinFloat1()
        {
            float3 v1 = new float3(1f, 5f, 3f);
            float3 v2 = new float3(2f, 4f, 6f);
            float3 expected = math.min(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Min(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestMinFloat2()
        {
            float3 v1 = new float3(-1f, -5f, -3f);
            float3 v2 = new float3(-2f, -4f, -6f);
            float3 expected = math.min(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Min(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestMinFloat3()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(0f, 0f, 0f);
            float3 expected = math.min(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Min(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestMinFloat4()
        {
            float3 v1 = new float3(100f, 200f, 300f);
            float3 v2 = new float3(50f, 400f, 250f);
            float3 expected = math.min(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Min(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestMinFloat5()
        {
            float3 v1 = new float3(-100f, 0f, 50f);
            float3 v2 = new float3(-50f, -10f, 60f);
            float3 expected = math.min(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Min(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        [Test]
        public void TestMinFloat6()
        {
            float3 v1 = new float3(1.1f, 2.2f, 3.3f);
            float3 v2 = new float3(3.3f, 2.2f, 1.1f);
            float3 expected = math.min(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Min(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestMoveTowardsVector1()
        {
            Vector3 cur = new Vector3(0f, 0f, 0f);
            Vector3 targ = new Vector3(10f, 0f, 0f);
            float maxDelta = 5f;
            Vector3 expected = Vector3.MoveTowards(cur, targ, maxDelta);
            Vector3 actual = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMoveTowardsVector2()
        {
            Vector3 cur = new Vector3(0f, 0f, 0f);
            Vector3 targ = new Vector3(3f, 4f, 0f);
            float maxDelta = 10f;
            Vector3 expected = Vector3.MoveTowards(cur, targ, maxDelta);
            Vector3 actual = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMoveTowardsVector3()
        {
            Vector3 cur = new Vector3(5f, 5f, 5f);
            Vector3 targ = new Vector3(5f, 5f, 5f);
            float maxDelta = 1f;
            Vector3 expected = Vector3.MoveTowards(cur, targ, maxDelta);
            Vector3 actual = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMoveTowardsVector4()
        {
            Vector3 cur = new Vector3(-10f, 0f, 0f);
            Vector3 targ = new Vector3(10f, 0f, 0f);
            float maxDelta = 15f;
            Vector3 expected = Vector3.MoveTowards(cur, targ, maxDelta);
            Vector3 actual = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMoveTowardsVector5()
        {
            Vector3 cur = new Vector3(1.5f, 2.5f, 3.5f);
            Vector3 targ = new Vector3(4.5f, 5.5f, 6.5f);
            float maxDelta = 0f;
            Vector3 expected = Vector3.MoveTowards(cur, targ, maxDelta);
            Vector3 actual = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMoveTowardsVector6()
        {
            Vector3 cur = new Vector3(0f, 0f, 0f);
            Vector3 targ = new Vector3(0f, 0f, 0f);
            float maxDelta = 10f;
            Vector3 expected = Vector3.MoveTowards(cur, targ, maxDelta);
            Vector3 actual = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMoveTowardsFloat1()
        {
            float3 cur = new float3(0f, 0f, 0f);
            float3 targ = new float3(10f, 0f, 0f);
            float maxDelta = 5f;
            float3 expected = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            float3 actual = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestMoveTowardsFloat2()
        {
            float3 cur = new float3(0f, 0f, 0f);
            float3 targ = new float3(3f, 4f, 0f);
            float maxDelta = 10f;
            float3 expected = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            float3 actual = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestMoveTowardsFloat3()
        {
            float3 cur = new float3(5f, 5f, 5f);
            float3 targ = new float3(5f, 5f, 5f);
            float maxDelta = 1f;
            float3 expected = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            float3 actual = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestMoveTowardsFloat4()
        {
            float3 cur = new float3(-10f, 0f, 0f);
            float3 targ = new float3(10f, 0f, 0f);
            float maxDelta = 15f;
            float3 expected = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            float3 actual = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestMoveTowardsFloat5()
        {
            float3 cur = new float3(1.5f, 2.5f, 3.5f);
            float3 targ = new float3(4.5f, 5.5f, 6.5f);
            float maxDelta = 0f;
            float3 expected = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            float3 actual = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestMoveTowardsFloat6()
        {
            float3 cur = new float3(0f, 0f, 0f);
            float3 targ = new float3(0f, 0f, 0f);
            float maxDelta = 10f;
            float3 expected = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            float3 actual = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestNormalizeVector1()
        {
            Vector3 v = new Vector3(3f, 4f, 0f);
            Vector3 expected = v.normalized;
            Vector3ToFloat3Utils.Normalize(ref v);
            Assert.AreEqual(expected.x, v.x);
            Assert.AreEqual(expected.y, v.y);
            Assert.AreEqual(expected.z, v.z);
        }

        [Test]
        public void TestNormalizeVector2()
        {
            Vector3 v = new Vector3(0f, 0f, 0f);
            Vector3 expected = v.normalized;
            Vector3ToFloat3Utils.Normalize(ref v);
            Assert.AreEqual(expected.x, v.x);
            Assert.AreEqual(expected.y, v.y);
            Assert.AreEqual(expected.z, v.z);
        }

        [Test]
        public void TestNormalizeVector3()
        {
            Vector3 v = new Vector3(-5f, 0f, 0f);
            Vector3 expected = v.normalized;
            Vector3ToFloat3Utils.Normalize(ref v);
            Assert.AreEqual(expected.x, v.x);
            Assert.AreEqual(expected.y, v.y);
            Assert.AreEqual(expected.z, v.z);
        }

        [Test]
        public void TestNormalizeVector4()
        {
            Vector3 v = new Vector3(1.2f, 3.4f, 5.6f);
            Vector3 expected = v.normalized;
            Vector3ToFloat3Utils.Normalize(ref v);
            Assert.AreEqual(expected.x, v.x);
            Assert.AreEqual(expected.y, v.y);
            Assert.AreEqual(expected.z, v.z);
        }

        [Test]
        public void TestNormalizeVector5()
        {
            Vector3 v = new Vector3(100f, 0f, 0f);
            Vector3 expected = v.normalized;
            Vector3ToFloat3Utils.Normalize(ref v);
            Assert.AreEqual(expected.x, v.x);
            Assert.AreEqual(expected.y, v.y);
            Assert.AreEqual(expected.z, v.z);
        }

        [Test]
        public void TestNormalizeVector6()
        {
            Vector3 v = new Vector3(0f, -10f, 0f);
            Vector3 expected = v.normalized;
            Vector3ToFloat3Utils.Normalize(ref v);
            Assert.AreEqual(expected.x, v.x);
            Assert.AreEqual(expected.y, v.y);
            Assert.AreEqual(expected.z, v.z);
        }

        [Test]
        public void TestNormalizeFloat1()
        {
            float3 v = new float3(3f, 4f, 0f);
            float3 expected = math.normalize(v);
            Vector3ToFloat3Utils.Normalize(ref v);
            Assert.AreEqual(expected.x, v.x);
            Assert.AreEqual(expected.y, v.y);
            Assert.AreEqual(expected.z, v.z);
        }

        [Test]
        public void TestNormalizeFloat2()
        {
            float3 v = new float3(0f, 0f, 0f);
            float3 expected = math.normalize(v);
            Vector3ToFloat3Utils.Normalize(ref v);
            Assert.AreEqual(expected.x, v.x);
            Assert.AreEqual(expected.y, v.y);
            Assert.AreEqual(expected.z, v.z);
        }

        [Test]
        public void TestNormalizeFloat3()
        {
            float3 v = new float3(-5f, 0f, 0f);
            float3 expected = math.normalize(v);
            Vector3ToFloat3Utils.Normalize(ref v);
            Assert.AreEqual(expected.x, v.x);
            Assert.AreEqual(expected.y, v.y);
            Assert.AreEqual(expected.z, v.z);
        }

        [Test]
        public void TestNormalizeFloat4()
        {
            float3 v = new float3(1.2f, 3.4f, 5.6f);
            float3 expected = math.normalize(v);
            Vector3ToFloat3Utils.Normalize(ref v);
            Assert.AreEqual(expected.x, v.x);
            Assert.AreEqual(expected.y, v.y);
            Assert.AreEqual(expected.z, v.z);
        }

        [Test]
        public void TestNormalizeFloat5()
        {
            float3 v = new float3(100f, 0f, 0f);
            float3 expected = math.normalize(v);
            Vector3ToFloat3Utils.Normalize(ref v);
            Assert.AreEqual(expected.x, v.x);
            Assert.AreEqual(expected.y, v.y);
            Assert.AreEqual(expected.z, v.z);
        }

        [Test]
        public void TestNormalizeFloat6()
        {
            float3 v = new float3(0f, -10f, 0f);
            float3 expected = math.normalize(v);
            Vector3ToFloat3Utils.Normalize(ref v);
            Assert.AreEqual(expected.x, v.x);
            Assert.AreEqual(expected.y, v.y);
            Assert.AreEqual(expected.z, v.z);
        }

        [Test]
        public void TestProjectVector1()
        {
            Vector3 v1 = new Vector3(3f, 4f, 0f);
            Vector3 v2 = new Vector3(1f, 0f, 0f);
            Vector3 expected = Vector3.Project(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectVector2()
        {
            Vector3 v1 = new Vector3(0f, 0f, 0f);
            Vector3 v2 = new Vector3(1f, 1f, 0f);
            Vector3 expected = Vector3.Project(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectVector3()
        {
            Vector3 v1 = new Vector3(-1f, 5f, 0f);
            Vector3 v2 = new Vector3(0f, 1f, 0f);
            Vector3 expected = Vector3.Project(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectVector4()
        {
            Vector3 v1 = new Vector3(10f, -10f, 5f);
            Vector3 v2 = new Vector3(1f, 0f, 0f);
            Vector3 expected = Vector3.Project(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectVector5()
        {
            Vector3 v1 = new Vector3(3f, 4f, 5f);
            Vector3 v2 = new Vector3(0f, 0f, 1f);
            Vector3 expected = Vector3.Project(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectVector6()
        {
            Vector3 v1 = new Vector3(1f, 2f, 3f);
            Vector3 v2 = new Vector3(0f, 1f, 0f);
            Vector3 expected = Vector3.Project(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectVector7()
        {
            Vector3 v1 = new Vector3(3.2f, 4.6f, 0.1f);
            Vector3 v2 = new Vector3(3.2f, 4.6f, 0.1f);
            Vector3 expected = Vector3.Project(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectVector8()
        {
            Vector3 v1 = new Vector3(3.2f, 4.6f, 0.1f);
            Vector3 v2 = new Vector3(-3.2f, 4.6f, 0.1f);
            Vector3 expected = Vector3.Project(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectVector9()
        {
            Vector3 v1 = new Vector3(-172f, 5f, 1200f);
            Vector3 v2 = new Vector3(172f, -5f, 1200f);
            Vector3 expected = Vector3.Project(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectVector10()
        {
            Vector3 v1 = new Vector3(7.7f, -10f, 5f);
            Vector3 v2 = new Vector3(1f, -10f, 5f);
            Vector3 expected = Vector3.Project(v1, v2);
            Vector3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectFloat1()
        {
            float3 v1 = new float3(3f, 4f, 0f);
            float3 v2 = new float3(1f, 0f, 0f);
            float3 expected = math.project(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectFloat2()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(1f, 1f, 0f);
            float3 expected = math.project(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectFloat3()
        {
            float3 v1 = new float3(-1f, 5f, 0f);
            float3 v2 = new float3(0f, 1f, 0f);
            float3 expected = math.project(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectFloat4()
        {
            float3 v1 = new float3(10f, -10f, 5f);
            float3 v2 = new float3(1f, 0f, 0f);
            float3 expected = math.project(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectFloat5()
        {
            float3 v1 = new float3(3f, 4f, 5f);
            float3 v2 = new float3(0f, 0f, 1f);
            float3 expected = math.project(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectFloat6()
        {
            float3 v1 = new float3(1f, 2f, 3f);
            float3 v2 = new float3(0f, 1f, 0f);
            float3 expected = math.project(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectFloat7()
        {
            float3 v1 = new float3(3f, 4f, 0f);
            float3 v2 = new float3(-3f, -4f, 0f);
            float3 expected = math.project(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectFloat8()
        {
            float3 v1 = new float3(0f, 0f, 0f);
            float3 v2 = new float3(0f, 0f, 0f);
            float3 expected = math.project(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectFloat9()
        {
            float3 v1 = new float3(-12f, 5.333f, 89.2f);
            float3 v2 = new float3(12f, -5.333f, 89.2f);
            float3 expected = math.project(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectFloat10()
        {
            float3 v1 = new float3(1f, 0f, 1f);
            float3 v2 = new float3(-1f, 0f, -1f);
            float3 expected = math.project(v1, v2);
            float3 actual = Vector3ToFloat3Utils.Project(v1, v2);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectOnPlaneVector1()
        {
            Vector3 v = new Vector3(3f, 4f, 0f);
            Vector3 planeNormal = new Vector3(0f, 1f, 0f);
            Vector3 expected = Vector3.ProjectOnPlane(v, planeNormal);
            Vector3 actual = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectOnPlaneVector2()
        {
            Vector3 v = new Vector3(0f, 0f, 0f);
            Vector3 planeNormal = new Vector3(1f, 0f, 0f);
            Vector3 expected = Vector3.ProjectOnPlane(v, planeNormal);
            Vector3 actual = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectOnPlaneVector3()
        {
            Vector3 v = new Vector3(1f, 2f, 3f);
            Vector3 planeNormal = new Vector3(0f, 0f, 1f);
            Vector3 expected = Vector3.ProjectOnPlane(v, planeNormal);
            Vector3 actual = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectOnPlaneVector4()
        {
            Vector3 v = new Vector3(-1f, 4f, -3f);
            Vector3 planeNormal = new Vector3(0f, 1f, 0f);
            Vector3 expected = Vector3.ProjectOnPlane(v, planeNormal);
            Vector3 actual = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectOnPlaneVector5()
        {
            Vector3 v = new Vector3(10f, 10f, 10f);
            Vector3 planeNormal = new Vector3(1f, 1f, 1f).normalized;
            Vector3 expected = Vector3.ProjectOnPlane(v, planeNormal);
            Vector3 actual = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectOnPlaneVector6()
        {
            Vector3 v = new Vector3(0f, 1f, 0f);
            Vector3 planeNormal = new Vector3(0f, 1f, 0f);
            Vector3 expected = Vector3.ProjectOnPlane(v, planeNormal);
            Vector3 actual = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestProjectOnPlaneFloat1()
        {
            float3 v = new float3(3f, 4f, 0f);
            float3 planeNormal = new float3(0f, 1f, 0f);
            float3 expected = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            float3 actual = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectOnPlaneFloat2()
        {
            float3 v = new float3(0f, 0f, 0f);
            float3 planeNormal = new float3(1f, 0f, 0f);
            float3 expected = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            float3 actual = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectOnPlaneFloat3()
        {
            float3 v = new float3(1f, 2f, 3f);
            float3 planeNormal = new float3(0f, 0f, 1f);
            float3 expected = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            float3 actual = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectOnPlaneFloat4()
        {
            float3 v = new float3(-1f, 4f, -3f);
            float3 planeNormal = new float3(0f, 1f, 0f);
            float3 expected = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            float3 actual = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectOnPlaneFloat5()
        {
            float3 v = new float3(10f, 10f, 10f);
            float3 planeNormal = math.normalize(new float3(1f, 1f, 1f));
            float3 expected = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            float3 actual = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        [Test]
        public void TestProjectOnPlaneFloat6()
        {
            float3 v = new float3(0f, 1f, 0f);
            float3 planeNormal = new float3(0f, 1f, 0f);
            float3 expected = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            float3 actual = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }

        //add test for non normalized normals
    }
}