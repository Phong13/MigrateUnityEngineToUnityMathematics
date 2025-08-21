using NUnit.Framework;
using UnityEngine;
using Unity.Mathematics;

public class TestQMUtils
{
    //-----------------------------------------------
    [Test]
    public void identityTest()
    {
        var Q = QuaternionToMathematicsUtils.identity;
        var q = QuaternionToMathematicsUtils.identity_math;
        Assert.AreEqual(Q.x, QuaternionToMathematicsUtils.x(q), 1e-5f);
        Assert.AreEqual(Q.y, QuaternionToMathematicsUtils.y(q), 1e-5f);
        Assert.AreEqual(Q.z, QuaternionToMathematicsUtils.z(q), 1e-5f);
        Assert.AreEqual(Q.w, QuaternionToMathematicsUtils.w(q), 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void eulerAngles_degTest()
    {
        var q1 = Quaternion.Euler(45f, 30f, 90f);
        var q2 = quaternion.Euler(math.radians(45f), math.radians(30f), math.radians(90f));
        var Q = QuaternionToMathematicsUtils.eulerAngles_deg(q1);
        var q = QuaternionToMathematicsUtils.eulerAngles_deg(q2);
        Assert.AreEqual(Q.x, q.x, 1e-5f);
        Assert.AreEqual(Q.y, q.y, 1e-5f);
        Assert.AreEqual(Q.z, q.z, 1e-5f);
    }
    [Test]
    public void eulerAngles_degTest2()
    {
        var Q = QuaternionToMathematicsUtils.eulerAngles_deg(new Quaternion(1f, 0f, 0f, 0f));
        var q = QuaternionToMathematicsUtils.eulerAngles_deg(new quaternion(1f, 0f, 0f, 0f));
        Assert.AreEqual(Q.x, q.x, 1e-5f);
        Assert.AreEqual(Q.y, q.y, 1e-5f);
        Assert.AreEqual(Q.z, q.z, 1e-5f);
    }
    [Test]
    public void eulerAngles_degTest3()
    {
        var Q = QuaternionToMathematicsUtils.eulerAngles_deg(new Quaternion(0f, 1f, 0f, 0f));
        var q = QuaternionToMathematicsUtils.eulerAngles_deg(new quaternion(0f, 1f, 0f, 0f));
        Assert.AreEqual(Q.x, q.x, 1e-5f);
        Assert.AreEqual(Q.y, q.y, 1e-5f);
        Assert.AreEqual(Q.z, q.z, 1e-5f);
    }
    [Test]
    public void eulerAngles_degTest4()
    {
        var q1 = Quaternion.Euler(20f, 35f, -15f);
        var q2 = quaternion.Euler(math.radians(20f), math.radians(35f), math.radians(-15f));
        var Q = QuaternionToMathematicsUtils.eulerAngles_deg(q1);
        var q = QuaternionToMathematicsUtils.eulerAngles_deg(q2);
        Assert.AreEqual(Q.x, q.x, 1e-5f);
        Assert.AreEqual(Q.y, q.y, 1e-5f);
        Assert.AreEqual(Q.z, q.z, 1e-5f);
    }
    [Test]
    public void eulerAngles_degTest5()
    {
        var Q = QuaternionToMathematicsUtils.eulerAngles_deg(new Quaternion(0f, 0f, 0f, 1));
        var q = QuaternionToMathematicsUtils.eulerAngles_deg(new quaternion(0f, 0f, 0f, 1f));
        Assert.AreEqual(Q.x, q.x, 1e-5f);
        Assert.AreEqual(Q.y, q.y, 1e-5f);
        Assert.AreEqual(Q.z, q.z, 1e-5f);
    }
    [Test]
    public void eulerAngles_degTest6()
    {
        var q1 = Quaternion.Euler(0f, 0f, 0f);
        var q2 = quaternion.Euler(math.radians(0f), math.radians(0f), math.radians(0f));
        var Q = QuaternionToMathematicsUtils.eulerAngles_deg(q1);
        var q = QuaternionToMathematicsUtils.eulerAngles_deg(q2);
        Assert.AreEqual(Q.x, q.x, 1e-5f);
        Assert.AreEqual(Q.y, q.y, 1e-5f);
        Assert.AreEqual(Q.z, q.z, 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void normalizedTest()
    {
        var q1 = Quaternion.Euler(20f, 35f, -15f);
        var q2 = quaternion.Euler(math.radians(20f), math.radians(35f), math.radians(-15f));
        var Q = QuaternionToMathematicsUtils.normalized(q1);
        var q = QuaternionToMathematicsUtils.normalized(q2);
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void normalizedTest2()
    {
        var q1 = Quaternion.Euler(12f, 0f, 0f);
        var q2 = quaternion.Euler(math.radians(12f), math.radians(0f), math.radians(0f));
        var Q = QuaternionToMathematicsUtils.normalized(q1);
        var q = QuaternionToMathematicsUtils.normalized(q2);
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void normalizedTest3()
    {
        var q1 = Quaternion.Euler(200f, 3500f, -15f);
        var q2 = quaternion.Euler(math.radians(200f), math.radians(3500f), math.radians(-15f));
        var Q = QuaternionToMathematicsUtils.normalized(q1);
        var q = QuaternionToMathematicsUtils.normalized(q2);
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void normalizedTest4()
    {
        var q1 = Quaternion.Euler(0.8f, 9.777f, 11f);
        var q2 = quaternion.Euler(math.radians(0.8f), math.radians(9.777f), math.radians(11f));
        var Q = QuaternionToMathematicsUtils.normalized(q1);
        var q = QuaternionToMathematicsUtils.normalized(q2);
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void normalizedTest5()
    {
        var q1 = Quaternion.Euler(-2f, -54f, -6.95f);
        var q2 = quaternion.Euler(math.radians(-2f), math.radians(-54f), math.radians(-6.95f));
        var Q = QuaternionToMathematicsUtils.normalized(q1);
        var q = QuaternionToMathematicsUtils.normalized(q2);
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void GetIndexTest()
    {
        var q1 = Quaternion.Euler(20f, 35f, -15f);
        var q2 = quaternion.Euler(math.radians(20f), math.radians(35f), math.radians(-15f));
        for (int i = 0; i < 4; i++)
        {
            Assert.AreEqual(QuaternionToMathematicsUtils.GetIndex(q1, i), QuaternionToMathematicsUtils.GetIndex(q2, i), 1e-5f);
        }
    }
    [Test]
    public void GetIndexTest2()
    {
        var q1 = Quaternion.Euler(16f, 100000f, 0f);
        var q2 = quaternion.Euler(math.radians(16f), math.radians(100000f), math.radians(0f));
        for (int i = 0; i < 4; i++)
        {
            Assert.AreEqual(QuaternionToMathematicsUtils.GetIndex(q1, i), QuaternionToMathematicsUtils.GetIndex(q2, i), 1e-5f);
        }
    }
    [Test]
    public void GetIndexTest3()
    {
        var q1 = Quaternion.identity;
        var q2 = quaternion.identity;
        for (int i = 0; i < 4; i++)
        {
            Assert.AreEqual(QuaternionToMathematicsUtils.GetIndex(q1, i), QuaternionToMathematicsUtils.GetIndex(q2, i), 1e-5f);
        }
    }
    //-----------------------------------------------
    [Test]
    public void SetIndexTest()
    {
        var q1 = Quaternion.Euler(20f, 35f, -15f);
        var q2 = quaternion.Euler(math.radians(20f), math.radians(35f), math.radians(-15f));
        QuaternionToMathematicsUtils.SetIndex(ref q1, 2, 5f);
        QuaternionToMathematicsUtils.SetIndex(ref q2, 2, 5f);
        Assert.AreEqual(q1[2], q2.value[2], 1e-5f);
    }
    [Test] // Should fail invalid index
    public void SetIndexTest2()
    {
        var q1 = Quaternion.Euler(20f, 35f, -15f);
        var q2 = quaternion.Euler(math.radians(20f), math.radians(35f), math.radians(-15f));
        QuaternionToMathematicsUtils.SetIndex(ref q1, 99, 5f);
        QuaternionToMathematicsUtils.SetIndex(ref q2, 99, 5f);
        Assert.AreEqual(q1[2], q2.value[2], 1e-5f);
    }
    [Test]
    public void SetIndexTest3()
    {
        var q1 = Quaternion.Euler(20f, 35f, -15f);
        var q2 = quaternion.Euler(math.radians(20f), math.radians(35f), math.radians(-15f));
        QuaternionToMathematicsUtils.SetIndex(ref q1, 1, 5.667f);
        QuaternionToMathematicsUtils.SetIndex(ref q2, 1, 5.667f);
        Assert.AreEqual(q1[2], q2.value[2], 1e-5f);
    }
    [Test] // Should fail invalid index
    public void SetIndexTest4()
    {
        var q1 = Quaternion.Euler(20f, 35f, -15f);
        var q2 = quaternion.Euler(math.radians(20f), math.radians(35f), math.radians(-15f));
        QuaternionToMathematicsUtils.SetIndex(ref q1, -1, 2f);
        QuaternionToMathematicsUtils.SetIndex(ref q2, -1, 2f);
        Assert.AreEqual(q1[2], q2.value[2], 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void wTest()
    {
        var q1 = Quaternion.Euler(20f, 35f, -15f);
        var q2 = quaternion.Euler(math.radians(20f), math.radians(35f), math.radians(-15f));
        Assert.AreEqual(QuaternionToMathematicsUtils.w(q1), QuaternionToMathematicsUtils.w(q2));
    }
    [Test]
    public void wTest2()
    {
        var q1 = Quaternion.Euler(16.66666f, 88f, 0f);
        var q2 = quaternion.Euler(math.radians(16.66666f), math.radians(88f), math.radians(0f));
        Assert.AreEqual(QuaternionToMathematicsUtils.w(q1), QuaternionToMathematicsUtils.w(q2));
    }
    [Test]
    public void xTest()
    {
        var q1 = Quaternion.Euler(20f, 35f, -15f);
        var q2 = quaternion.Euler(math.radians(20f), math.radians(35f), math.radians(-15f));
        Assert.AreEqual(QuaternionToMathematicsUtils.x(q1), QuaternionToMathematicsUtils.x(q2));
    }
    [Test]
    public void xTest2()
    {
        var q1 = Quaternion.Euler(16.66666f, 88f, 0f);
        var q2 = quaternion.Euler(math.radians(16.66666f), math.radians(88f), math.radians(0f));
        Assert.AreEqual(QuaternionToMathematicsUtils.x(q1), QuaternionToMathematicsUtils.x(q2));
    }
    [Test]
    public void yTest()
    {
        var q1 = Quaternion.Euler(20f, 35f, -15f);
        var q2 = quaternion.Euler(math.radians(20f), math.radians(35f), math.radians(-15f));
        Assert.AreEqual(QuaternionToMathematicsUtils.y(q1), QuaternionToMathematicsUtils.y(q2));
    }
    [Test]
    public void yTest2()
    {
        var q1 = Quaternion.Euler(16.66666f, 88f, 0f);
        var q2 = quaternion.Euler(math.radians(16.66666f), math.radians(88f), math.radians(0f));
        Assert.AreEqual(QuaternionToMathematicsUtils.y(q1), QuaternionToMathematicsUtils.y(q2));
    }
    [Test]
    public void zTest()
    {
        var q1 = Quaternion.Euler(20f, 35f, -15f);
        var q2 = quaternion.Euler(math.radians(20f), math.radians(35f), math.radians(-15f));
        Assert.AreEqual(QuaternionToMathematicsUtils.z(q1), QuaternionToMathematicsUtils.z(q2));
    }
    [Test]
    public void zTest2()
    {
        var q1 = Quaternion.Euler(16.66666f, 88f, 0f);
        var q2 = quaternion.Euler(math.radians(16.66666f), math.radians(88f), math.radians(0f));
        Assert.AreEqual(QuaternionToMathematicsUtils.z(q1), QuaternionToMathematicsUtils.z(q2));
    }
    //-----------------------------------------------
    [Test]
    public void QuaternionTest()
    {
        var Q = QuaternionToMathematicsUtils.Quaternion(0f, 1f, 0f, 0f);
        var q = QuaternionToMathematicsUtils.Quaternion_math(0f, 1f, 0f, 0f);
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void QuaternionTest2()
    {
        var Q = QuaternionToMathematicsUtils.Quaternion(0f, 1200f, -99f, 0.878783f);
        var q = QuaternionToMathematicsUtils.Quaternion_math(0f, 1200f, -99f, 0.878783f);
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void QuaternionTest3()
    {
        var Q = QuaternionToMathematicsUtils.Quaternion(0f, 0f, 0f, 0f);
        var q = QuaternionToMathematicsUtils.Quaternion_math(0f, 0f, 0f, 0f);
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void QuaternionTest4()
    {
        var Q = QuaternionToMathematicsUtils.Quaternion(1f, 0f, 0f, 0f);
        var q = QuaternionToMathematicsUtils.Quaternion_math(1f, 0f, 0f, 0f);
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void SetTest()
    {
        var q1 = Quaternion.Euler(20f, 35f, -15f);
        var q2 = quaternion.Euler(math.radians(20f), math.radians(35f), math.radians(-15f));
        QuaternionToMathematicsUtils.Set(ref q1, 0.1f, -0.2f, 3f, 0f);
        QuaternionToMathematicsUtils.Set(ref q2, 0.1f, -0.2f, 3f, 0f);
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void SetTest2()
    {
        var q1 = QuaternionToMathematicsUtils.Quaternion(1f, 0f, 0f, 0f);
        var q2 = QuaternionToMathematicsUtils.Quaternion_math(1f, 0f, 0f, 0f);
        QuaternionToMathematicsUtils.Set(ref q1, 100f, -69.88888f, 3f, 12f);
        QuaternionToMathematicsUtils.Set(ref q2, 100f, -69.88888f, 3f, 12f);
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void SetTest3()
    {
        var q1 = Quaternion.Euler(90f, -10f, -180f);
        var q2 = quaternion.Euler(math.radians(90f), math.radians(-10f), math.radians(-180f));
        QuaternionToMathematicsUtils.Set(ref q1, -0f, -0f, -0f, -0f);
        QuaternionToMathematicsUtils.Set(ref q2, -0f, -0f, -0f, -0f);
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void SetTest4()
    {
        var q1 = Quaternion.Euler(123456.78910f, 35.000000000001f, -900020001f);
        var q2 = quaternion.Euler(math.radians(123456.78910f), math.radians(35.000000000001f), math.radians(-900020001f));
        QuaternionToMathematicsUtils.Set(ref q1, 12f, -411f, 3.333f, 0f);
        QuaternionToMathematicsUtils.Set(ref q2, 12f, -411f, 3.333f, 0f);
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void SetTest5()
    {
        var q1 = Quaternion.Euler(-45f, 60f, 30f);
        var q2 = quaternion.Euler(math.radians(-45f), math.radians(60f), math.radians(30f));
        QuaternionToMathematicsUtils.Set(ref q1, 1f, 2f, 3f, 4f);
        QuaternionToMathematicsUtils.Set(ref q2, 1f, 2f, 3f, 4f);
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void SetTest6()
    {
        var q1 = Quaternion.Euler(0f, 0f, 0f);
        var q2 = quaternion.Euler(math.radians(0f), math.radians(0f), math.radians(0f));
        QuaternionToMathematicsUtils.Set(ref q1, -5f, 0.5f, 100f, -100f);
        QuaternionToMathematicsUtils.Set(ref q2, -5f, 0.5f, 100f, -100f);
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void SetTest7()
    {
        var q1 = Quaternion.Euler(270f, -90f, 45f);
        var q2 = quaternion.Euler(math.radians(270f), math.radians(-90f), math.radians(45f));
        QuaternionToMathematicsUtils.Set(ref q1, 0f, 0f, 0f, 1f);
        QuaternionToMathematicsUtils.Set(ref q2, 0f, 0f, 0f, 1f);
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void SetTest8()
    {
        var q1 = Quaternion.Euler(33.33f, -44.44f, 55.55f);
        var q2 = quaternion.Euler(math.radians(33.33f), math.radians(-44.44f), math.radians(55.55f));
        QuaternionToMathematicsUtils.Set(ref q1, -12f, 34f, -56f, 78f);
        QuaternionToMathematicsUtils.Set(ref q2, -12f, 34f, -56f, 78f);
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void SetFromToRotationTest()
    {
        var q1 = Quaternion.Euler(123456.78910f, 35.000000000001f, -900020001f);
        var q2 = quaternion.Euler(math.radians(123456.78910f), math.radians(35.000000000001f), math.radians(-900020001f));
        QuaternionToMathematicsUtils.SetFromToRotation(ref q1, Vector3.right, Vector3.up);
        QuaternionToMathematicsUtils.SetFromToRotation(ref q2, new float3(1, 0, 0), new float3(0, 1, 0));
        Assert.AreEqual(q1.x, q2.value.x, 1e-4f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-4f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-4f);
    }
    [Test]
    public void SetFromToRotationTest2()
    {
        var q1 = Quaternion.Euler(0.00001f, 0.01f, -0.023f);
        var q2 = quaternion.Euler(math.radians(0.00001f), math.radians(0.01f), math.radians(-0.023f));
        QuaternionToMathematicsUtils.SetFromToRotation(ref q1, Vector3.right, Vector3.up);
        QuaternionToMathematicsUtils.SetFromToRotation(ref q2, new float3(1, 0, 0), new float3(0, 1, 0));
        Assert.AreEqual(q1.x, q2.value.x, 1e-4f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-4f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-4f);
    }
    [Test]
    public void SetFromToRotationTest3()
    {
        var q1 = Quaternion.Euler(0f, 0f, 0f);
        var q2 = quaternion.Euler(math.radians(0f), math.radians(0f), math.radians(0f));
        QuaternionToMathematicsUtils.SetFromToRotation(ref q1, Vector3.forward, Vector3.back);
        QuaternionToMathematicsUtils.SetFromToRotation(ref q2, new float3(0, 0, 1), new float3(0, 0, -1));
        Assert.AreEqual(q1.x, q2.value.x, 1e-4f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-4f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-4f);
    }
    [Test]
    public void SetFromToRotationTest4()
    {
        var q1 = Quaternion.Euler(-90f, 180f, 90f);
        var q2 = quaternion.Euler(math.radians(-90f), math.radians(180f), math.radians(90f));
        QuaternionToMathematicsUtils.SetFromToRotation(ref q1, Vector3.up, Vector3.down);
        QuaternionToMathematicsUtils.SetFromToRotation(ref q2, new float3(0, 1, 0), new float3(0, -1, 0));
        Assert.AreEqual(q1.x, q2.value.x, 1e-4f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-4f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-4f);
    }
    [Test]
    public void SetFromToRotationTest5()
    {
        var q1 = Quaternion.Euler(45f, 45f, 45f);
        var q2 = quaternion.Euler(math.radians(45f), math.radians(45f), math.radians(45f));
        QuaternionToMathematicsUtils.SetFromToRotation(ref q1, Vector3.right, Vector3.right);
        QuaternionToMathematicsUtils.SetFromToRotation(ref q2, new float3(1, 0, 0), new float3(1, 0, 0));
        Assert.AreEqual(q1.x, q2.value.x, 1e-4f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-4f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-4f);
    }
    [Test]
    public void SetFromToRotationTest6()
    {
        var q1 = Quaternion.Euler(-180f, 0f, 180f);
        var q2 = quaternion.Euler(math.radians(-180f), math.radians(0f), math.radians(180f));
        QuaternionToMathematicsUtils.SetFromToRotation(ref q1, Vector3.back, Vector3.forward);
        QuaternionToMathematicsUtils.SetFromToRotation(ref q2, new float3(0, 0, -1), new float3(0, 0, 1));
        Assert.AreEqual(q1.x, q2.value.x, 1e-4f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-4f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-4f);
    }
    //-----------------------------------------------
    [Test]
    public void SetLookRotationTest()
    {
        var q1 = Quaternion.Euler(-180f, 0f, 180f);
        var q2 = quaternion.Euler(math.radians(-180f), math.radians(0f), math.radians(180f));
        QuaternionToMathematicsUtils.SetLookRotation(ref q1, Vector3.forward, Vector3.up);
        QuaternionToMathematicsUtils.SetLookRotation(ref q2, new float3(0, 0, 1), new float3(0, 1, 0));
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void SetLookRotationTest2()
    {
        var q1 = Quaternion.Euler(0f, 0f, 0f);
        var q2 = quaternion.Euler(math.radians(0f), math.radians(0f), math.radians(0f));
        QuaternionToMathematicsUtils.SetLookRotation(ref q1, Vector3.up, Vector3.forward);
        QuaternionToMathematicsUtils.SetLookRotation(ref q2, new float3(0, 1, 0), new float3(0, 0, 1));
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void SetLookRotationTest3()
    {
        var q1 = Quaternion.Euler(45f, -90f, 30f);
        var q2 = quaternion.Euler(math.radians(45f), math.radians(-90f), math.radians(30f));
        QuaternionToMathematicsUtils.SetLookRotation(ref q1, Vector3.right, Vector3.up);
        QuaternionToMathematicsUtils.SetLookRotation(ref q2, new float3(1, 0, 0), new float3(0, 1, 0));
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void SetLookRotationTest4()
    {
        var q1 = Quaternion.Euler(-60f, 180f, -120f);
        var q2 = quaternion.Euler(math.radians(-60f), math.radians(180f), math.radians(-120f));
        QuaternionToMathematicsUtils.SetLookRotation(ref q1, Vector3.back, Vector3.down);
        QuaternionToMathematicsUtils.SetLookRotation(ref q2, new float3(0, 0, -1), new float3(0, -1, 0));
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void SetLookRotationTest5()
    {
        var q1 = Quaternion.Euler(90f, 0f, 0f);
        var q2 = quaternion.Euler(math.radians(90f), math.radians(0f), math.radians(0f));
        QuaternionToMathematicsUtils.SetLookRotation(ref q1, Vector3.left, Vector3.up);
        QuaternionToMathematicsUtils.SetLookRotation(ref q2, new float3(-1, 0, 0), new float3(0, 1, 0));
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void SetLookRotationTest6()
    {
        var q1 = Quaternion.Euler(0f, 180f, 0f);
        var q2 = quaternion.Euler(math.radians(0f), math.radians(180f), math.radians(0f));
        QuaternionToMathematicsUtils.SetLookRotation(ref q1, Vector3.forward, Vector3.back);
        QuaternionToMathematicsUtils.SetLookRotation(ref q2, new float3(0, 0, 1), new float3(0, 0, -1));
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void ToAngleAxisTest1()
    {
        float angleQ = 16.5f;
        Vector3 axisQ = Vector3.back;
        float angleq = 16.5f;
        float3 axisq = Vector3.back;

        var Qrot = Quaternion.Euler(123456.78910f, 35.000000000001f, -900020001f);
        var qrot = quaternion.Euler(math.radians(123456.78910f), math.radians(35.000000000001f), math.radians(-900020001f));

        QuaternionToMathematicsUtils.ToAngleAxis_deg(ref Qrot, out angleQ, out axisQ);
        QuaternionToMathematicsUtils.ToAngleAxis_deg(ref qrot, out angleq, out axisq);

        Assert.AreEqual(angleQ, angleq, 1e-3f);
        Assert.AreEqual(axisQ.x, axisq.x, 1e-3f);
        Assert.AreEqual(axisQ.y, axisq.y, 1e-3f);
        Assert.AreEqual(axisQ.z, axisq.z, 1e-3f);
    }
    [Test]
    public void ToAngleAxisTest2()
    {
        float angleQ = 0f;
        Vector3 axisQ = Vector3.right;
        float angleq = 0f;
        float3 axisq = new float3(1, 0, 0);

        var Qrot = Quaternion.Euler(0f, 0f, 0f);
        var qrot = quaternion.Euler(math.radians(0f), math.radians(0f), math.radians(0f));

        QuaternionToMathematicsUtils.ToAngleAxis_deg(ref Qrot, out angleQ, out axisQ);
        QuaternionToMathematicsUtils.ToAngleAxis_deg(ref qrot, out angleq, out axisq);

        Assert.AreEqual(angleQ, angleq, 1e-3f);
        Assert.AreEqual(axisQ.x, axisq.x, 1e-3f);
        Assert.AreEqual(axisQ.y, axisq.y, 1e-3f);
        Assert.AreEqual(axisQ.z, axisq.z, 1e-3f);
    }
    [Test]
    public void ToAngleAxisTest3()
    {
        float angleQ = 180f;
        Vector3 axisQ = Vector3.up;
        float angleq = 180f;
        float3 axisq = new float3(0, 1, 0);

        var Qrot = Quaternion.Euler(180f, 0f, 0f);
        var qrot = quaternion.Euler(math.radians(180f), math.radians(0f), math.radians(0f));

        QuaternionToMathematicsUtils.ToAngleAxis_deg(ref Qrot, out angleQ, out axisQ);
        QuaternionToMathematicsUtils.ToAngleAxis_deg(ref qrot, out angleq, out axisq);

        Assert.AreEqual(angleQ, angleq, 1e-3f);
        Assert.AreEqual(axisQ.x, axisq.x, 1e-3f);
        Assert.AreEqual(axisQ.y, axisq.y, 1e-3f);
        Assert.AreEqual(axisQ.z, axisq.z, 1e-3f);
    }
    [Test]
    public void ToAngleAxisTest4()
    {
        float angleQ = 90f;
        Vector3 axisQ = Vector3.forward;
        float angleq = 90f;
        float3 axisq = new float3(0, 0, 1);

        var Qrot = Quaternion.Euler(0f, 90f, 0f);
        var qrot = quaternion.Euler(math.radians(0f), math.radians(90f), math.radians(0f));

        QuaternionToMathematicsUtils.ToAngleAxis_deg(ref Qrot, out angleQ, out axisQ);
        QuaternionToMathematicsUtils.ToAngleAxis_deg(ref qrot, out angleq, out axisq);

        Assert.AreEqual(angleQ, angleq, 1e-3f);
        Assert.AreEqual(axisQ.x, axisq.x, 1e-3f);
        Assert.AreEqual(axisQ.y, axisq.y, 1e-3f);
        Assert.AreEqual(axisQ.z, axisq.z, 1e-3f);
    }
    [Test]
    public void ToAngleAxisTest5()
    {
        float angleQ = 270f;
        Vector3 axisQ = Vector3.left;
        float angleq = 270f;
        float3 axisq = new float3(-1, 0, 0);

        var Qrot = Quaternion.Euler(-90f, 0f, 0f);
        var qrot = quaternion.Euler(math.radians(-90f), math.radians(0f), math.radians(0f));

        QuaternionToMathematicsUtils.ToAngleAxis_deg(ref Qrot, out angleQ, out axisQ);
        QuaternionToMathematicsUtils.ToAngleAxis_deg(ref qrot, out angleq, out axisq);

        Assert.AreEqual(angleQ, angleq, 1e-3f);
        Assert.AreEqual(axisQ.x, axisq.x, 1e-3f);
        Assert.AreEqual(axisQ.y, axisq.y, 1e-3f);
        Assert.AreEqual(axisQ.z, axisq.z, 1e-3f);
    }
    [Test]
    public void ToAngleAxisTest6()
    {
        float angleQ = 45f;
        Vector3 axisQ = new Vector3(1f, 1f, 0f).normalized;
        float angleq = 45f;
        float3 axisq = math.normalize(new float3(1f, 1f, 0f));

        var Qrot = Quaternion.Euler(45f, 45f, 0f);
        var qrot = quaternion.Euler(math.radians(45f), math.radians(45f), math.radians(0f));

        QuaternionToMathematicsUtils.ToAngleAxis_deg(ref Qrot, out angleQ, out axisQ);
        QuaternionToMathematicsUtils.ToAngleAxis_deg(ref qrot, out angleq, out axisq);

        Assert.AreEqual(angleQ, angleq, 1e-3f);
        Assert.AreEqual(axisQ.x, axisq.x, 1e-3f);
        Assert.AreEqual(axisQ.y, axisq.y, 1e-3f);
        Assert.AreEqual(axisQ.z, axisq.z, 1e-3f);
    }
    //-----------------------------------------------
    [Test]
    public void ToStringTest() //Failing because of extra decimal places
    {
        string sQ = QuaternionToMathematicsUtils.ToString(new Quaternion(1f, -2.5f, 0.125f, 0f));
        string sq = QuaternionToMathematicsUtils.ToString(new quaternion(1f, -2.5f, 0.125f, 0f));
        Assert.AreEqual(sQ, sq);
    }
    //-----------------------------------------------
    [Test]
    public void Angle_degTest()
    {
        var Qa = Quaternion.Euler(0f, 0f, 0f);
        var Qb = Quaternion.Euler(0f, 0f, 0f);
        var qa = quaternion.Euler(math.radians(0f), math.radians(0f), math.radians(0f));
        var qb = quaternion.Euler(math.radians(0f), math.radians(0f), math.radians(0f));

        float a1 = QuaternionToMathematicsUtils.Angle_deg(Qa, Qb);
        float a2 = QuaternionToMathematicsUtils.Angle_deg(qa, qb);
        Assert.AreEqual(a1, a2, 1e-4f);
    }
    [Test]
    public void Angle_degTest2()
    {
        var Qa = Quaternion.Euler(10f, -99.99f, 0.7834125f);
        var Qb = Quaternion.Euler(0f, 22f, -9000500f);
        var qa = quaternion.Euler(math.radians(10f), math.radians(-99.99f), math.radians(0.7834125f));
        var qb = quaternion.Euler(math.radians(0f), math.radians(22f), math.radians(-9000500f));

        float a1 = QuaternionToMathematicsUtils.Angle_deg(Qa, Qb);
        float a2 = QuaternionToMathematicsUtils.Angle_deg(qa, qb);
        Assert.AreEqual(a1, a2, 1e-4f);
    }
    [Test]
    public void Angle_degTest3()
    {
        var Qa = Quaternion.Euler(0f, 0f, 0f);
        var Qb = Quaternion.Euler(123456.78910f, 35.000000000001f, -900020001f);
        var qa = quaternion.Euler(math.radians(0f), math.radians(0f), math.radians(0f));
        var qb = quaternion.Euler(math.radians(123456.78910f), math.radians(35.000000000001f), math.radians(-900020001f));

        float a1 = QuaternionToMathematicsUtils.Angle_deg(Qa, Qb);
        float a2 = QuaternionToMathematicsUtils.Angle_deg(qa, qb);
        Assert.AreEqual(a1, a2, 1e-4f);
    }
    [Test]
    public void Angle_degTest4()
    {
        var Qa = Quaternion.Euler(10f, 0f, 0.1f);
        var Qb = Quaternion.Euler(10f, 0f, 0.1f);
        var qa = quaternion.Euler(math.radians(10f), math.radians(0f), math.radians(0.1f));
        var qb = quaternion.Euler(math.radians(10f), math.radians(0f), math.radians(0.1f));

        float a1 = QuaternionToMathematicsUtils.Angle_deg(Qa, Qb);
        float a2 = QuaternionToMathematicsUtils.Angle_deg(qa, qb);
        Assert.AreEqual(a1, a2, 1e-4f);
    }
    //-----------------------------------------------
    [Test]
    public void AngleAxisTest()
    {
        var Q = QuaternionToMathematicsUtils.AngleAxis_deg(45f, new Vector3(1, 0, 0));
        var q = QuaternionToMathematicsUtils.AngleAxis_deg(45f, new float3(1, 0, 0));
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void AngleAxisTest2()
    {
        var Q = QuaternionToMathematicsUtils.AngleAxis_deg(-90f, new Vector3(0, 0, 0));
        var q = QuaternionToMathematicsUtils.AngleAxis_deg(-90f, new float3(0, 0, 0));
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void AngleAxisTest3()
    {
        var Q = QuaternionToMathematicsUtils.AngleAxis_deg(540f, new Vector3(1.777f, 20.6f, -18));
        var q = QuaternionToMathematicsUtils.AngleAxis_deg(540f, new float3(1.777f, 20.6f, -18));
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void AngleAxisTest4()
    {
        var Q = QuaternionToMathematicsUtils.AngleAxis_deg(45f, new Vector3(17f, 12.666f, 0.0f));
        var q = QuaternionToMathematicsUtils.AngleAxis_deg(45f, new float3(17f, 12.666f, 0.0f));
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void DotTest()
    {
        var Qa = Quaternion.Euler(0f, 0f, 0f);
        var Qb = Quaternion.Euler(0f, 0f, 0f);
        var qa = quaternion.Euler(math.radians(0f), math.radians(0f), math.radians(0f));
        var qb = quaternion.Euler(math.radians(0f), math.radians(0f), math.radians(0f));
        float d1 = QuaternionToMathematicsUtils.Dot(Qa, Qb);
        float d2 = QuaternionToMathematicsUtils.Dot(qa, qb);
        Assert.AreEqual(d1, d2, 1e-5f);
    }
    [Test]
    public void DotTest2()
    {
        var Qa = Quaternion.Euler(10f, -99.99f, 0.7834125f);
        var Qb = Quaternion.Euler(0f, 22f, -9000500f);
        var qa = quaternion.Euler(math.radians(10f), math.radians(-99.99f), math.radians(0.7834125f));
        var qb = quaternion.Euler(math.radians(0f), math.radians(22f), math.radians(-9000500f));
        float d1 = QuaternionToMathematicsUtils.Dot(Qa, Qb);
        float d2 = QuaternionToMathematicsUtils.Dot(qa, qb);
        Assert.AreEqual(d1, d2, 1e-5f);
    }
    [Test]
    public void DotTest3()
    {
        var Qa = Quaternion.Euler(0f, 0f, 0f);
        var Qb = Quaternion.Euler(123456.78910f, 35.000000000001f, -900020001f);
        var qa = quaternion.Euler(math.radians(0f), math.radians(0f), math.radians(0f));
        var qb = quaternion.Euler(math.radians(123456.78910f), math.radians(35.000000000001f), math.radians(-900020001f));
        float d1 = QuaternionToMathematicsUtils.Dot(Qa, Qb);
        float d2 = QuaternionToMathematicsUtils.Dot(qa, qb);
        Assert.AreEqual(d1, d2, 1e-5f);
    }
    [Test]
    public void DotTest4()
    {
        var Qa = Quaternion.Euler(10f, 0f, 0.1f);
        var Qb = Quaternion.Euler(10f, 0f, 0.1f);
        var qa = quaternion.Euler(math.radians(10f), math.radians(0f), math.radians(0.1f));
        var qb = quaternion.Euler(math.radians(10f), math.radians(0f), math.radians(0.1f));
        float d1 = QuaternionToMathematicsUtils.Dot(Qa, Qb);
        float d2 = QuaternionToMathematicsUtils.Dot(qa, qb);
        Assert.AreEqual(d1, d2, 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void EulerTest()
    {
        var Q = QuaternionToMathematicsUtils.Euler_deg(10f, -20.5f, 0f);
        var q = QuaternionToMathematicsUtils.Euler_deg_math(10f, -20.5f, 0f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void EulerTest2()
    {
        var Q = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var q = QuaternionToMathematicsUtils.Euler_deg_math(0f, 0f, 0f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void EulerTest3()
    {
        var Q = QuaternionToMathematicsUtils.Euler_deg(10000f, -205000f, 10f);
        var q = QuaternionToMathematicsUtils.Euler_deg_math(10000f, -205000f, 10f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void EulerTest4()
    {
        var Q = QuaternionToMathematicsUtils.Euler_deg(1.9090f, -90808f, 0.00198f);
        var q = QuaternionToMathematicsUtils.Euler_deg_math(1.9090f, -90808f, 0.00198f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    //-----------------------------------------------    
    [Test]
    public void FromToRotationTest()
    {
        var Q = QuaternionToMathematicsUtils.FromToRotation(Vector3.forward, Vector3.up);
        var q = QuaternionToMathematicsUtils.FromToRotation(new float3(0, 0, 1), new float3(0, 1, 0));
        Assert.AreEqual(Q.x, q.value.x, 1e-3f);
        Assert.AreEqual(Q.y, q.value.y, 1e-3f);
        Assert.AreEqual(Q.z, q.value.z, 1e-3f);
        Assert.AreEqual(Q.w, q.value.w, 1e-3f);
    }
    [Test]
    public void FromToRotationTest2()
    {
        var V = new Vector3(17f, 12.666f, 0.0f);
        var f = new float3(17f, 12.666f, 0.0f);
        var Q = QuaternionToMathematicsUtils.FromToRotation(V, Vector3.up);
        var q = QuaternionToMathematicsUtils.FromToRotation(f, new float3(0, 1, 0));
        Assert.AreEqual(Q.x, q.value.x, 1e-3f);
        Assert.AreEqual(Q.y, q.value.y, 1e-3f);
        Assert.AreEqual(Q.z, q.value.z, 1e-3f);
        Assert.AreEqual(Q.w, q.value.w, 1e-3f);
    }
    [Test]
    public void FromToRotationTest3()
    {
        var V = new Vector3(2f, 1f, 0f);
        var f = new float3(2f, 1f, 0f);
        var Q = QuaternionToMathematicsUtils.FromToRotation(V, Vector3.up);
        var q = QuaternionToMathematicsUtils.FromToRotation(f, new float3(0, 1, 0));
        Assert.AreEqual(Q.x, q.value.x, 1e-3f);
        Assert.AreEqual(Q.y, q.value.y, 1e-3f);
        Assert.AreEqual(Q.z, q.value.z, 1e-3f);
        Assert.AreEqual(Q.w, q.value.w, 1e-3f);
    }
    [Test]
    public void FromToRotationTest4()
    {
        var Q = QuaternionToMathematicsUtils.FromToRotation(new Vector3(0.555f, 0, 1.16f), Vector3.up);
        var q = QuaternionToMathematicsUtils.FromToRotation(new float3(0.555f, 0, 1.16f), new float3(0, 1, 0));
        Assert.AreEqual(Q.x, q.value.x, 1e-3f);
        Assert.AreEqual(Q.y, q.value.y, 1e-3f);
        Assert.AreEqual(Q.z, q.value.z, 1e-3f);
        Assert.AreEqual(Q.w, q.value.w, 1e-3f);
    }
    //-----------------------------------------------   
    [Test]
    public void InverseTest()
    {
        var Q = QuaternionToMathematicsUtils.Inverse(QuaternionToMathematicsUtils.Euler_deg(0f, 30f, -10f));
        var q = QuaternionToMathematicsUtils.Inverse(QuaternionToMathematicsUtils.Euler_deg_math(0f, 30f, -10f));
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void InverseTest2()
    {
        var Q = QuaternionToMathematicsUtils.Inverse(QuaternionToMathematicsUtils.Euler_deg(0.787f, 12.666f, -10f));
        var q = QuaternionToMathematicsUtils.Inverse(QuaternionToMathematicsUtils.Euler_deg_math(0.787f, 12.666f, -10f));
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void InverseTest3()
    {
        var Q = QuaternionToMathematicsUtils.Inverse(QuaternionToMathematicsUtils.Euler_deg(1.9090f, -90808f, 0.00198f));
        var q = QuaternionToMathematicsUtils.Inverse(QuaternionToMathematicsUtils.Euler_deg_math(1.9090f, -90808f, 0.00198f));
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void InverseTest4()
    {
        var Q = QuaternionToMathematicsUtils.Inverse(QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f));
        var q = QuaternionToMathematicsUtils.Inverse(QuaternionToMathematicsUtils.Euler_deg_math(0f, 0f, 0f));
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    //-----------------------------------------------  
    [Test]
    public void LerpTest()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 90f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 90f, 0f);

        var Q = QuaternionToMathematicsUtils.Lerp(Qa, Qb, 0.5f);
        var q = QuaternionToMathematicsUtils.Lerp(qa, qb, 0.5f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void LerpTest2()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 0f, 0f);

        var Q = QuaternionToMathematicsUtils.Lerp(Qa, Qb, -1.566f);
        var q = QuaternionToMathematicsUtils.Lerp(qa, qb, -1.566f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void LerpTest3()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(-67.7f, 12f, 0.8f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(-67.7f, 12f, 0.8f);

        var Q = QuaternionToMathematicsUtils.Lerp(Qa, Qb, 521.398f);
        var q = QuaternionToMathematicsUtils.Lerp(qa, qb, 521.398f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void LerpTest4()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 0f, 0f);

        var Q = QuaternionToMathematicsUtils.Lerp(Qa, Qb, 0f);
        var q = QuaternionToMathematicsUtils.Lerp(qa, qb, 0f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void LerpUnclampedTest()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 90f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 90f, 0f);

        var Q = QuaternionToMathematicsUtils.LerpUnclamped(Qa, Qb, 1.5f);
        var q = QuaternionToMathematicsUtils.LerpUnclamped(qa, qb, 1.5f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void LerpUnclampedTest2()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 0f, 0f);

        var Q = QuaternionToMathematicsUtils.LerpUnclamped(Qa, Qb, -1.566f);
        var q = QuaternionToMathematicsUtils.LerpUnclamped(qa, qb, -1.566f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void LerpUnclampedTest3()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(-67.7f, 12f, 0.8f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(-67.7f, 12f, 0.8f);

        var Q = QuaternionToMathematicsUtils.LerpUnclamped(Qa, Qb, 521.398f);
        var q = QuaternionToMathematicsUtils.LerpUnclamped(qa, qb, 521.398f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void LerpUnclampedTest4()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 0f, 0f);

        var Q = QuaternionToMathematicsUtils.LerpUnclamped(Qa, Qb, 0f);
        var q = QuaternionToMathematicsUtils.LerpUnclamped(qa, qb, 0f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    //-----------------------------------------------  
    [Test]
    public void LookRotationTest()
    {
        var Q = QuaternionToMathematicsUtils.LookRotation(new Vector3(0.2f, 1f, 3f), Vector3.up);
        var q = QuaternionToMathematicsUtils.LookRotation(new float3(0.2f, 1f, 3f), new float3(0, 1, 0));
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void LookRotationTest2()
    {
        var Q = QuaternionToMathematicsUtils.LookRotation(Vector3.forward, Vector3.up);
        var q = QuaternionToMathematicsUtils.LookRotation(new float3(0, 0, 1), new float3(0, 1, 0));
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void LookRotationTest3()
    {
        var V = new Vector3(2f, 1f, 0f);
        var f = new float3(2f, 1f, 0f);
        var Q = QuaternionToMathematicsUtils.LookRotation(V, Vector3.up);
        var q = QuaternionToMathematicsUtils.LookRotation(f, new float3(0, 1, 0));
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void LookRotationTest4()
    {
        var V = new Vector3(17f, 12.666f, 0.0f);
        var f = new float3(17f, 12.666f, 0.0f);
        var Q = QuaternionToMathematicsUtils.LookRotation(V, Vector3.up);
        var q = QuaternionToMathematicsUtils.LookRotation(f, new float3(0, 1, 0));
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
    }
    [Test]
    public void LookRotationTest5()
    {
        var V = new Vector3(0f, 0f, 0.0f);
        var f = new float3(0f, 0f, 0.0f);
        var Q = QuaternionToMathematicsUtils.LookRotation(V, Vector3.up);
        var q = QuaternionToMathematicsUtils.LookRotation(f, new float3(0, 1, 0));
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
    }
    //-----------------------------------------------  
    [Test]
    public void NormalizeTest()
    {
        var q1 = Quaternion.Euler(30f, 45f, 60f);
        var q2 = quaternion.Euler(math.radians(30f), math.radians(45f), math.radians(60f));
        QuaternionToMathematicsUtils.Normalize(ref q1);
        QuaternionToMathematicsUtils.Normalize(ref q2);
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void NormalizeTest2()
    {
        var q1 = new Quaternion(0f, 0f, 0f, 0f);
        var q2 = new quaternion(0f, 0f, 0f, 0f);
        QuaternionToMathematicsUtils.Normalize(ref q1);
        QuaternionToMathematicsUtils.Normalize(ref q2);
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void NormalizeTest3()
    {
        var q1 = new Quaternion(1f, 2f, 3f, 4f);
        var q2 = new quaternion(1f, 2f, 3f, 4f);
        QuaternionToMathematicsUtils.Normalize(ref q1);
        QuaternionToMathematicsUtils.Normalize(ref q2);
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void NormalizeTest4()
    {
        var q1 = new Quaternion(-5f, 2f, -3f, 1f);
        var q2 = new quaternion(-5f, 2f, -3f, 1f);
        QuaternionToMathematicsUtils.Normalize(ref q1);
        QuaternionToMathematicsUtils.Normalize(ref q2);
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    [Test]
    public void NormalizeTest5()
    {
        var q1 = Quaternion.Euler(0f, 0f, 180f);
        var q2 = quaternion.Euler(math.radians(0f), math.radians(0f), math.radians(180f));
        QuaternionToMathematicsUtils.Normalize(ref q1);
        QuaternionToMathematicsUtils.Normalize(ref q2);
        Assert.AreEqual(q1.x, q2.value.x, 1e-5f);
        Assert.AreEqual(q1.y, q2.value.y, 1e-5f);
        Assert.AreEqual(q1.z, q2.value.z, 1e-5f);
        Assert.AreEqual(q1.w, q2.value.w, 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void RotateTowardsTest()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 90f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 90f, 0f);
        var Q = QuaternionToMathematicsUtils.RotateTowards(Qa, Qb, 30f);
        var q = QuaternionToMathematicsUtils.RotateTowards(qa, qb, 30f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void RotateTowardsTest2()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 0f, 0f);
        var Q = QuaternionToMathematicsUtils.RotateTowards(Qa, Qb, 0f);
        var q = QuaternionToMathematicsUtils.RotateTowards(qa, qb, 0f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void RotateTowardsTest3()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0.78974f, 90f, -12.87f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0.78974f, 90f, -12.87f);
        var Q = QuaternionToMathematicsUtils.RotateTowards(Qa, Qb, 6000f);
        var q = QuaternionToMathematicsUtils.RotateTowards(qa, qb, 6000f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void RotateTowardsTest4()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(1f, 90f, 15.9f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(1f, 90f, 15.9f);
        var Q = QuaternionToMathematicsUtils.RotateTowards(Qa, Qb, -9.99f);
        var q = QuaternionToMathematicsUtils.RotateTowards(qa, qb, -9.99f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    //-----------------------------------------------
    [Test]
    public void SlerpTest()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 90f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 90f, 0f);
        var Q = QuaternionToMathematicsUtils.Slerp(Qa, Qb, 0.5f);
        var q = QuaternionToMathematicsUtils.Slerp(qa, qb, 0.5f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void SlerpTest2()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 0f, 0f);
        var Q = QuaternionToMathematicsUtils.Slerp(Qa, Qb, -1.566f);
        var q = QuaternionToMathematicsUtils.Slerp(qa, qb, -1.566f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void SlerpTest3()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(-67.7f, 12f, 0.8f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(-67.7f, 12f, 0.8f);
        var Q = QuaternionToMathematicsUtils.Slerp(Qa, Qb, 521.398f);
        var q = QuaternionToMathematicsUtils.Slerp(qa, qb, 521.398f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void SlerpTest4()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 0f, 0f);
        var Q = QuaternionToMathematicsUtils.Slerp(Qa, Qb, 0f);
        var q = QuaternionToMathematicsUtils.Slerp(qa, qb, 0f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void SlerpUnclampedTest()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 90f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 90f, 0f);
        var Q = QuaternionToMathematicsUtils.SlerpUnclamped(Qa, Qb, 1.5f);
        var q = QuaternionToMathematicsUtils.SlerpUnclamped(qa, qb, 1.5f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void SlerpUnclampedTest2()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 0f, 0f);
        var Q = QuaternionToMathematicsUtils.SlerpUnclamped(Qa, Qb, -1.566f);
        var q = QuaternionToMathematicsUtils.SlerpUnclamped(qa, qb, -1.566f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void SlerpUnclampedTest3()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(-67.7f, 12f, 0.8f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(-67.7f, 12f, 0.8f);
        var Q = QuaternionToMathematicsUtils.SlerpUnclamped(Qa, Qb, 521.398f);
        var q = QuaternionToMathematicsUtils.SlerpUnclamped(qa, qb, 521.398f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    [Test]
    public void SlerpUnclampedTest4()
    {
        var Qa = Quaternion.identity;
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var qa = quaternion.identity;
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 0f, 0f);
        var Q = QuaternionToMathematicsUtils.SlerpUnclamped(Qa, Qb, 0f);
        var q = QuaternionToMathematicsUtils.SlerpUnclamped(qa, qb, 0f);
        Assert.AreEqual(Q.x, q.value.x, 1e-4f);
        Assert.AreEqual(Q.y, q.value.y, 1e-4f);
        Assert.AreEqual(Q.z, q.value.z, 1e-4f);
        Assert.AreEqual(Q.w, q.value.w, 1e-4f);
    }
    //-----------------------------------------------
    [Test]
    public void MultiplyTest()
    {
        var Qa = QuaternionToMathematicsUtils.Euler_deg(0f, 30f, 0f);
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 15f, 0f);
        var qa = QuaternionToMathematicsUtils.Euler_deg_math(0f, 30f, 0f);
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 15f, 0f);
        var Q = QuaternionToMathematicsUtils.Multiply(Qa, Qb);
        var q = QuaternionToMathematicsUtils.Multiply(qa, qb);
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void MultiplyTest2()
    {
        var Qa = QuaternionToMathematicsUtils.Euler_deg(0f, 90f, 0f);
        var Qb = QuaternionToMathematicsUtils.Euler_deg(12.98f, 90f, 17.9090f);
        var qa = QuaternionToMathematicsUtils.Euler_deg_math(0f, 90f, 0f);
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(12.98f, 90f, 17.9090f);
        var Q = QuaternionToMathematicsUtils.Multiply(Qa, Qb);
        var q = QuaternionToMathematicsUtils.Multiply(qa, qb);
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void MultiplyTest3()
    {
        var Qa = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var qa = QuaternionToMathematicsUtils.Euler_deg_math(0f, 0f, 0f);
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 0f, 0f);
        var Q = QuaternionToMathematicsUtils.Multiply(Qa, Qb);
        var q = QuaternionToMathematicsUtils.Multiply(qa, qb);
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    [Test]
    public void MultiplyTest4()
    {
        var Qa = QuaternionToMathematicsUtils.Euler_deg(-1.920f, -90.98f, 0f);
        var Qb = QuaternionToMathematicsUtils.Euler_deg(-8.8f, 20f, 0f);
        var qa = QuaternionToMathematicsUtils.Euler_deg_math(-1.920f, -90.98f, 0f);
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(-8.8f, 20f, 0f);
        var Q = QuaternionToMathematicsUtils.Multiply(Qa, Qb);
        var q = QuaternionToMathematicsUtils.Multiply(qa, qb);
        Assert.AreEqual(Q.x, q.value.x, 1e-5f);
        Assert.AreEqual(Q.y, q.value.y, 1e-5f);
        Assert.AreEqual(Q.z, q.value.z, 1e-5f);
        Assert.AreEqual(Q.w, q.value.w, 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void EqualsTest()
    {
        var Qa = QuaternionToMathematicsUtils.Euler_deg(0f, 45f, 0f);
        var Qb = QuaternionToMathematicsUtils.Euler_deg(0f, 45f, 0f);
        var Qc = QuaternionToMathematicsUtils.Euler_deg(0f, 30f, 0f);
        var Qd = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var Qe = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);

        var qa = QuaternionToMathematicsUtils.Euler_deg_math(0f, 45f, 0f);
        var qb = QuaternionToMathematicsUtils.Euler_deg_math(0f, 45f, 0f);
        var qc = QuaternionToMathematicsUtils.Euler_deg_math(0f, 30f, 0f);
        var qd = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);
        var qe = QuaternionToMathematicsUtils.Euler_deg(0f, 0f, 0f);

        bool eqQ1 = QuaternionToMathematicsUtils.Equals(Qa, Qb);
        bool eqq1 = QuaternionToMathematicsUtils.Equals(qa, qb);
        Assert.AreEqual(eqQ1, eqq1);

        bool eqQ2 = QuaternionToMathematicsUtils.Equals(Qa, Qc);
        bool eqq2 = QuaternionToMathematicsUtils.Equals(qa, qc);
        Assert.AreEqual(eqQ2, eqq2);

        bool eqQ3 = QuaternionToMathematicsUtils.Equals(Qd, Qe);
        bool eqq3 = QuaternionToMathematicsUtils.Equals(qd, qe);
        Assert.AreEqual(eqQ3, eqq3);
    }
    //-----------------------------------------------
}


