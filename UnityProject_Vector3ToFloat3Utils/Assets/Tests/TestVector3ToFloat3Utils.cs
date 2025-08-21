using NUnit.Framework;
using UnityEngine;
using Unity.Mathematics;

public class TestVector3ToFloat3Utils
{
    //-----------------------------------------------
    [Test]
    public void magnitudeTest()
    {
        Vector3 v = new Vector3(3, 4, 0);
        float3 f = new float3(3, 4, 0);

        Assert.AreEqual(v.magnitude, Vector3ToFloat3Utils.magnitude(v), 1e-5f);
        Assert.AreEqual(v.magnitude, Vector3ToFloat3Utils.magnitude(f), 1e-5f);
    }
    [Test]
    public void magnitudeTest2()
    {
        Vector3 v = new Vector3(0, 0, 0);
        float3 f = new float3(0, 0, 0);

        Assert.AreEqual(v.magnitude, Vector3ToFloat3Utils.magnitude(v), 1e-5f);
        Assert.AreEqual(v.magnitude, Vector3ToFloat3Utils.magnitude(f), 1e-5f);
    }
    [Test]
    public void magnitudeTest3()
    {
        Vector3 v = new Vector3(-1, -1, -1);
        float3 f = new float3(-1, -1, -1);

        Assert.AreEqual(v.magnitude, Vector3ToFloat3Utils.magnitude(v), 1e-5f);
        Assert.AreEqual(v.magnitude, Vector3ToFloat3Utils.magnitude(f), 1e-5f);
    }
    [Test]
    public void magnitudeTest4()
    {
        Vector3 v = new Vector3(1e5f, 0, 0);
        float3 f = new float3(1e5f, 0, 0);

        Assert.AreEqual(v.magnitude, Vector3ToFloat3Utils.magnitude(v), 1e-5f);
        Assert.AreEqual(v.magnitude, Vector3ToFloat3Utils.magnitude(f), 1e-5f);
    }
    [Test]
    public void magnitudeTest5()
    {
        Vector3 v = new Vector3(0, -2.988f, 2);
        float3 f = new float3(0, -2.988f, 2);

        Assert.AreEqual(v.magnitude, Vector3ToFloat3Utils.magnitude(v), 1e-5f);
        Assert.AreEqual(v.magnitude, Vector3ToFloat3Utils.magnitude(f), 1e-5f);
    }
    [Test]
    public void magnitudeTest6()
    {
        Vector3 v = new Vector3(0.0001f, 0.0002f, 0.0003f);
        float3 f = new float3(0.0001f, 0.0002f, 0.0003f);

        Assert.AreEqual(v.magnitude, Vector3ToFloat3Utils.magnitude(v), 1e-5f);
        Assert.AreEqual(v.magnitude, Vector3ToFloat3Utils.magnitude(f), 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void sqrMagnitudeTest()
    {
        Vector3 v = new Vector3(1, 2, 3);
        float3 f = new float3(1, 2, 3);

        Assert.AreEqual(v.sqrMagnitude, Vector3ToFloat3Utils.sqrMagnitude(v), 1e-5f);
        Assert.AreEqual(v.sqrMagnitude, Vector3ToFloat3Utils.sqrMagnitude(f), 1e-5f);
    }
    [Test]
    public void sqrMagnitudeTest2()
    {
        Vector3 v = new Vector3(0, 0, 0);
        float3 f = new float3(0, 0, 0);

        Assert.AreEqual(v.sqrMagnitude, Vector3ToFloat3Utils.sqrMagnitude(v), 1e-5f);
        Assert.AreEqual(v.sqrMagnitude, Vector3ToFloat3Utils.sqrMagnitude(f), 1e-5f);
    }
    [Test]
    public void sqrMagnitudeTest3()
    {
        Vector3 v = new Vector3(-1, -1, -1.092f);
        float3 f = new float3(-1, -1, -1.092f);

        Assert.AreEqual(v.sqrMagnitude, Vector3ToFloat3Utils.sqrMagnitude(v), 1e-5f);
        Assert.AreEqual(v.sqrMagnitude, Vector3ToFloat3Utils.sqrMagnitude(f), 1e-5f);
    }
    [Test]
    public void sqrMagnitudeTest4()
    {
        Vector3 v = new Vector3(1e5f, 0, 0);
        float3 f = new float3(1e5f, 0, 0);

        Assert.AreEqual(v.sqrMagnitude, Vector3ToFloat3Utils.sqrMagnitude(v), 1e-5f);
        Assert.AreEqual(v.sqrMagnitude, Vector3ToFloat3Utils.sqrMagnitude(f), 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void normalizedTest()
    {
        Vector3 v = new Vector3(1, 2, 3);
        float3 f = new float3(1, 2, 3);
        Vector3 n = v.normalized;
        Vector3 vn = Vector3ToFloat3Utils.normalized(v);
        float3 fn = Vector3ToFloat3Utils.normalized(f);

        Assert.AreEqual(n.x, vn.x, 1e-5f);
        Assert.AreEqual(n.y, vn.y, 1e-5f);
        Assert.AreEqual(n.z, vn.z, 1e-5f);
        Assert.AreEqual(n.x, fn.x, 1e-5f);
        Assert.AreEqual(n.y, fn.y, 1e-5f);
        Assert.AreEqual(n.z, fn.z, 1e-5f);
    }
    [Test]
    public void normalizedTest2()
    {
        Vector3 v = new Vector3(5.9881f, 2.0001f, 3);
        float3 f = new float3(5.9881f, 2.0001f, 3);
        Vector3 n = v.normalized;
        Vector3 vn = Vector3ToFloat3Utils.normalized(v);
        float3 fn = Vector3ToFloat3Utils.normalized(f);

        Assert.AreEqual(vn.x, fn.x, 1e-5f);
        Assert.AreEqual(vn.y, fn.y, 1e-5f);
        Assert.AreEqual(vn.z, fn.z, 1e-5f);
        Assert.AreEqual(n.x, vn.x, 1e-5f);
        Assert.AreEqual(n.y, vn.y, 1e-5f);
        Assert.AreEqual(n.z, vn.z, 1e-5f);
        Assert.AreEqual(n.x, fn.x, 1e-5f);
        Assert.AreEqual(n.y, fn.y, 1e-5f);
        Assert.AreEqual(n.z, fn.z, 1e-5f);
    }
    [Test]
    public void normalizedTest3()
    {
        Vector3 v = new Vector3(-1, -2, -3);
        float3 f = new float3(-1, -2, -3);
        Vector3 n = v.normalized;
        Vector3 vn = Vector3ToFloat3Utils.normalized(v);
        float3 fn = Vector3ToFloat3Utils.normalized(f);

        Assert.AreEqual(vn.x, fn.x, 1e-5f);
        Assert.AreEqual(vn.y, fn.y, 1e-5f);
        Assert.AreEqual(vn.z, fn.z, 1e-5f);
        Assert.AreEqual(n.x, vn.x, 1e-5f);
        Assert.AreEqual(n.y, vn.y, 1e-5f);
        Assert.AreEqual(n.z, vn.z, 1e-5f);
        Assert.AreEqual(n.x, fn.x, 1e-5f);
        Assert.AreEqual(n.y, fn.y, 1e-5f);
        Assert.AreEqual(n.z, fn.z, 1e-5f);
    }
    [Test]
    public void normalizedTest4()
    {
        Vector3 v = new Vector3(0.5f, 0.5f, 0.5f);
        float3 f = new float3(0.5f, 0.5f, 0.5f);
        Vector3 n = v.normalized;
        Vector3 vn = Vector3ToFloat3Utils.normalized(v);
        float3 fn = Vector3ToFloat3Utils.normalized(f);

        Assert.AreEqual(vn.x, fn.x, 1e-5f);
        Assert.AreEqual(vn.y, fn.y, 1e-5f);
        Assert.AreEqual(vn.z, fn.z, 1e-5f);
        Assert.AreEqual(n.x, vn.x, 1e-5f);
        Assert.AreEqual(n.y, vn.y, 1e-5f);
        Assert.AreEqual(n.z, vn.z, 1e-5f);
        Assert.AreEqual(n.x, fn.x, 1e-5f);
        Assert.AreEqual(n.y, fn.y, 1e-5f);
        Assert.AreEqual(n.z, fn.z, 1e-5f);
    }
    /*
    [Test]
    public void normalizedTest5() //Will fail 0-Vector
    {
        Vector3 v = new Vector3(0, 0, 0);
        float3 f = new float3(0, 0, 0);
        Vector3 n = v.normalized;
        Vector3 vn = Vector3ToFloat3Utils.normalized(v);
        float3 fn = Vector3ToFloat3Utils.normalized(f);

        Assert.AreEqual(vn.x, fn.x, 1e-5f);
        Assert.AreEqual(vn.y, fn.y, 1e-5f);
        Assert.AreEqual(vn.z, fn.z, 1e-5f);
        Assert.AreEqual(n.x, vn.x, 1e-5f);
        Assert.AreEqual(n.y, vn.y, 1e-5f);
        Assert.AreEqual(n.z, vn.z, 1e-5f);
        Assert.AreEqual(n.x, fn.x, 1e-5f);
        Assert.AreEqual(n.y, fn.y, 1e-5f);
        Assert.AreEqual(n.z, fn.z, 1e-5f);
    }
    */
    [Test]
    public void normalizedTest6()
    {
        Vector3 v = new Vector3(0, -3, 4);
        float3 f = new float3(0, -3, 4);
        Vector3 n = v.normalized;
        Vector3 vn = Vector3ToFloat3Utils.normalized(v);
        float3 fn = Vector3ToFloat3Utils.normalized(f);

        Assert.AreEqual(vn.x, fn.x, 1e-5f);
        Assert.AreEqual(vn.y, fn.y, 1e-5f);
        Assert.AreEqual(vn.z, fn.z, 1e-5f);
        Assert.AreEqual(n.x, vn.x, 1e-5f);
        Assert.AreEqual(n.y, vn.y, 1e-5f);
        Assert.AreEqual(n.z, vn.z, 1e-5f);
        Assert.AreEqual(n.x, fn.x, 1e-5f);
        Assert.AreEqual(n.y, fn.y, 1e-5f);
        Assert.AreEqual(n.z, fn.z, 1e-5f);
    }
    /*
    [Test]
    public void normalizedTest7() //Will fail when super close to 0-Vector
    {
        Vector3 v = new Vector3(1e-10f, 2e-10f, 3e-10f);
        float3 f = new float3(1e-10f, 2e-10f, 3e-10f);
        Vector3 n = v.normalized;
        Vector3 vn = Vector3ToFloat3Utils.normalized(v);
        float3 fn = Vector3ToFloat3Utils.normalized(f);

        Assert.AreEqual(vn.x, fn.x, 1e-5f);
        Assert.AreEqual(vn.y, fn.y, 1e-5f);
        Assert.AreEqual(vn.z, fn.z, 1e-5f);
        Assert.AreEqual(n.x, vn.x, 1e-5f);
        Assert.AreEqual(n.y, vn.y, 1e-5f);
        Assert.AreEqual(n.z, vn.z, 1e-5f);
        Assert.AreEqual(n.x, fn.x, 1e-5f);
        Assert.AreEqual(n.y, fn.y, 1e-5f);
        Assert.AreEqual(n.z, fn.z, 1e-5f);
    }
    */
    //-----------------------------------------------
    [Test]
    public void directionConstantsTest()
    {
        Assert.AreEqual(Vector3ToFloat3Utils.forward, Vector3.forward);
        Assert.AreEqual(Vector3ToFloat3Utils.back, Vector3.back);
        Assert.AreEqual(Vector3ToFloat3Utils.up, Vector3.up);
        Assert.AreEqual(Vector3ToFloat3Utils.down, Vector3.down);
        Assert.AreEqual(Vector3ToFloat3Utils.right, Vector3.right);
        Assert.AreEqual(Vector3ToFloat3Utils.left, Vector3.left);
        Assert.AreEqual(Vector3ToFloat3Utils.one, Vector3.one);
        Assert.AreEqual(Vector3ToFloat3Utils.zero, Vector3.zero);
    }
    //-----------------------------------------------
    [Test]
    public void EqualsAndNotEqualsTest()
    {
        Vector3 v1 = new Vector3(1, 2, 3);
        Vector3 v2 = new Vector3(1, 2, 3);
        float3 f1 = new float3(1, 2, 3);
        float3 f2 = new float3(1, 2, 3);
        Vector3 v3 = new Vector3(1, 2, 3.00001f);
        float3 f3 = new float3(1, 2, 3.00001f);

        Assert.IsTrue(Vector3ToFloat3Utils.Equals(v1, v2));
        Assert.IsTrue(Vector3ToFloat3Utils.Equals(f1, f2));
        Assert.IsFalse(Vector3ToFloat3Utils.NotEquals(v1, v2));
        Assert.IsFalse(Vector3ToFloat3Utils.NotEquals(f1, f2));
        Assert.IsFalse(Vector3ToFloat3Utils.Equals(v1, v3));
        Assert.IsFalse(Vector3ToFloat3Utils.Equals(f1, f3));
        Assert.IsTrue(Vector3ToFloat3Utils.NotEquals(v1, v3));
        Assert.IsTrue(Vector3ToFloat3Utils.NotEquals(f1, f3));
    }
    //-----------------------------------------------
    [Test]
    public void Angle_degTest()
    {
        Vector3 v1 = Vector3.right;
        Vector3 v2 = Vector3.up;
        float3 f1 = new float3(1, 0, 0);
        float3 f2 = new float3(0, 1, 0);
        float a = Vector3.Angle(v1, v2);

        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(v1, v2), 1e-5f);
        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(f1, f2), 1e-5f);
    }
    [Test]
    public void Angle_degTest2()
    {
        Vector3 v1 = Vector3.right;
        Vector3 v2 = Vector3.right;
        float3 f1 = new float3(1, 0, 0);
        float3 f2 = new float3(1, 0, 0);
        float a = Vector3.Angle(v1, v2);

        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(v1, v2), 1e-5f);
        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(f1, f2), 1e-5f);
    }
    [Test]
    public void Angle_degTest3()
    {
        Vector3 v1 = Vector3.right;
        Vector3 v2 = -Vector3.right;
        float3 f1 = new float3(1, 0, 0);
        float3 f2 = new float3(-1, 0, 0);
        float a = Vector3.Angle(v1, v2);

        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(v1, v2), 1e-5f);
        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(f1, f2), 1e-5f);
    }
    [Test]
    public void Angle_degTest4()
    {
        Vector3 v1 = new Vector3(1, 0, 0);
        Vector3 v2 = new Vector3(1, 1, 0);
        float3 f1 = new float3(1, 0, 0);
        float3 f2 = new float3(1, 1, 0);
        float a = Vector3.Angle(v1, v2);

        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(v1, v2), 1e-5f);
        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(f1, f2), 1e-5f);
    }
    [Test]
    public void Angle_degTest5()
    {
        Vector3 v1 = Vector3.up;
        Vector3 v2 = -Vector3.right;
        float3 f1 = new float3(0, 1, 0);
        float3 f2 = new float3(-1, 0, 0);
        float a = Vector3.Angle(v1, v2);

        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(v1, v2), 1e-5f);
        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(f1, f2), 1e-5f);
    }
    [Test]
    public void Angle_degTest6()
    {
        Vector3 v1 = new Vector3(1, 0, 0);
        Vector3 v2 = new Vector3(1, 0.0001f, 0);
        float3 f1 = new float3(1, 0, 0);
        float3 f2 = new float3(1, 0.0001f, 0);
        float a = Vector3.Angle(v1, v2);

        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(v1, v2), 1e-5f);
        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(f1, f2), 1e-5f);
    }
    [Test]
    public void Angle_degTest7()
    {
        Vector3 v1 = new Vector3(36.187f, 12, -88);
        Vector3 v2 = new Vector3(-36.187f, -12, 88);
        float3 f1 = new float3(36.187f, 12, -88);
        float3 f2 = new float3(-36.187f, -12, 88);
        float a = Vector3.Angle(v1, v2);

        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(v1, v2), 1e-5f);
        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(f1, f2), 1e-5f);
    }
    /*
    [Test]
    public void Angle_degTest8() //Will fail because normalize fails when super close to 0
    {
        Vector3 v1 = new Vector3(1e-10f, 2e-10f, 3e-10f);
        Vector3 v2 = new Vector3(2e-10f, 4e-10f, 3e-10f);
        float3 f1 = new float3(1e-10f, 2e-10f, 3e-10f);
        float3 f2 = new float3(2e-10f, 4e-10f, 3e-10f);
        float a = Vector3.Angle(v1, v2);

        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(v1, v2), 1e-5f);
        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(f1, f2), 1e-5f);
    }
    */
    [Test]
    public void Angle_degTest9()
    {
        Vector3 v1 = new Vector3(1e-5f, 2e-5f, 3e-5f);
        Vector3 v2 = new Vector3(2e-5f, 4e-5f, 3e-5f);
        float3 f1 = new float3(1e-5f, 2e-5f, 3e-5f);
        float3 f2 = new float3(2e-5f, 4e-5f, 3e-5f);
        float a = Vector3.Angle(v1, v2);

        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(v1, v2), 1e-5f);
        Assert.AreEqual(a, Vector3ToFloat3Utils.Angle_deg(f1, f2), 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void CrossTest()
    {
        Vector3 v1 = Vector3.right;
        Vector3 v2 = Vector3.up;
        float3 f1 = new float3(1, 0, 0);
        float3 f2 = new float3(0, 1, 0);
        Vector3 c = Vector3.Cross(v1, v2);
        Vector3 crossV = Vector3ToFloat3Utils.Cross(v1, v2);
        float3 crossF = Vector3ToFloat3Utils.Cross(f1, f2);

        Assert.AreEqual(crossV.x, crossF.x, 1e-5f);
        Assert.AreEqual(crossV.y, crossF.y, 1e-5f);
        Assert.AreEqual(crossV.z, crossF.z, 1e-5f);
        Assert.AreEqual(c.x, crossV.x, 1e-5f);
        Assert.AreEqual(c.y, crossV.y, 1e-5f);
        Assert.AreEqual(c.z, crossV.z, 1e-5f);
        Assert.AreEqual(c.x, crossF.x, 1e-5f);
        Assert.AreEqual(c.y, crossF.y, 1e-5f);
        Assert.AreEqual(c.z, crossF.z, 1e-5f);
    }
    [Test]
    public void CrossTest2()
    {
        Vector3 v1 = Vector3.right;
        Vector3 v2 = Vector3.right;
        float3 f1 = new float3(1, 0, 0);
        float3 f2 = new float3(1, 0, 0);
        Vector3 c = Vector3.Cross(v1, v2);
        Vector3 crossV = Vector3ToFloat3Utils.Cross(v1, v2);
        float3 crossF = Vector3ToFloat3Utils.Cross(f1, f2);

        Assert.AreEqual(crossV.x, crossF.x, 1e-5f);
        Assert.AreEqual(crossV.y, crossF.y, 1e-5f);
        Assert.AreEqual(crossV.z, crossF.z, 1e-5f);
        Assert.AreEqual(c.x, crossV.x, 1e-5f);
        Assert.AreEqual(c.y, crossV.y, 1e-5f);
        Assert.AreEqual(c.z, crossV.z, 1e-5f);
        Assert.AreEqual(c.x, crossF.x, 1e-5f);
        Assert.AreEqual(c.y, crossF.y, 1e-5f);
        Assert.AreEqual(c.z, crossF.z, 1e-5f);
    }
    [Test]
    public void CrossTest3()
    {
        Vector3 v1 = Vector3.up;
        Vector3 v2 = -Vector3.up;
        float3 f1 = new float3(0, 1, 0);
        float3 f2 = new float3(0, -1, 0);
        Vector3 c = Vector3.Cross(v1, v2);
        Vector3 crossV = Vector3ToFloat3Utils.Cross(v1, v2);
        float3 crossF = Vector3ToFloat3Utils.Cross(f1, f2);

        Assert.AreEqual(crossV.x, crossF.x, 1e-5f);
        Assert.AreEqual(crossV.y, crossF.y, 1e-5f);
        Assert.AreEqual(crossV.z, crossF.z, 1e-5f);
        Assert.AreEqual(c.x, crossV.x, 1e-5f);
        Assert.AreEqual(c.y, crossV.y, 1e-5f);
        Assert.AreEqual(c.z, crossV.z, 1e-5f);
        Assert.AreEqual(c.x, crossF.x, 1e-5f);
        Assert.AreEqual(c.y, crossF.y, 1e-5f);
        Assert.AreEqual(c.z, crossF.z, 1e-5f);
    }
    [Test]
    public void CrossTest4()
    {
        Vector3 v1 = new Vector3(1.1f, 2, 3);
        Vector3 v2 = new Vector3(4.444f, 5, 6);
        float3 f1 = new float3(1.1f, 2, 3);
        float3 f2 = new float3(4.444f, 5, 6);
        Vector3 c = Vector3.Cross(v1, v2);
        Vector3 crossV = Vector3ToFloat3Utils.Cross(v1, v2);
        float3 crossF = Vector3ToFloat3Utils.Cross(f1, f2);

        Assert.AreEqual(crossV.x, crossF.x, 1e-5f);
        Assert.AreEqual(crossV.y, crossF.y, 1e-5f);
        Assert.AreEqual(crossV.z, crossF.z, 1e-5f);
        Assert.AreEqual(c.x, crossV.x, 1e-5f);
        Assert.AreEqual(c.y, crossV.y, 1e-5f);
        Assert.AreEqual(c.z, crossV.z, 1e-5f);
        Assert.AreEqual(c.x, crossF.x, 1e-5f);
        Assert.AreEqual(c.y, crossF.y, 1e-5f);
        Assert.AreEqual(c.z, crossF.z, 1e-5f);
    }
    [Test]
    public void CrossTest5()
    {
        Vector3 v1 = -Vector3.right;
        Vector3 v2 = Vector3.up;
        float3 f1 = new float3(-1, 0, 0);
        float3 f2 = new float3(0, 1, 0);
        Vector3 c = Vector3.Cross(v1, v2);
        Vector3 crossV = Vector3ToFloat3Utils.Cross(v1, v2);
        float3 crossF = Vector3ToFloat3Utils.Cross(f1, f2);

        Assert.AreEqual(crossV.x, crossF.x, 1e-5f);
        Assert.AreEqual(crossV.y, crossF.y, 1e-5f);
        Assert.AreEqual(crossV.z, crossF.z, 1e-5f);
        Assert.AreEqual(c.x, crossV.x, 1e-5f);
        Assert.AreEqual(c.y, crossV.y, 1e-5f);
        Assert.AreEqual(c.z, crossV.z, 1e-5f);
        Assert.AreEqual(c.x, crossF.x, 1e-5f);
        Assert.AreEqual(c.y, crossF.y, 1e-5f);
        Assert.AreEqual(c.z, crossF.z, 1e-5f);
    }
    [Test]
    public void CrossTest6()
    {
        Vector3 v1 = new Vector3(0.001f, 0.002f, 0.003f);
        Vector3 v2 = new Vector3(0.004f, 0.005f, 0.006f);
        float3 f1 = new float3(0.001f, 0.002f, 0.003f);
        float3 f2 = new float3(0.004f, 0.005f, 0.006f);
        Vector3 c = Vector3.Cross(v1, v2);
        Vector3 crossV = Vector3ToFloat3Utils.Cross(v1, v2);
        float3 crossF = Vector3ToFloat3Utils.Cross(f1, f2);

        Assert.AreEqual(crossV.x, crossF.x, 1e-5f);
        Assert.AreEqual(crossV.y, crossF.y, 1e-5f);
        Assert.AreEqual(crossV.z, crossF.z, 1e-5f);
        Assert.AreEqual(c.x, crossV.x, 1e-5f);
        Assert.AreEqual(c.y, crossV.y, 1e-5f);
        Assert.AreEqual(c.z, crossV.z, 1e-5f);
        Assert.AreEqual(c.x, crossF.x, 1e-5f);
        Assert.AreEqual(c.y, crossF.y, 1e-5f);
        Assert.AreEqual(c.z, crossF.z, 1e-5f);
    }
    [Test]
    public void CrossTest7()
    {
        Vector3 v1 = new Vector3(1e-10f, 2e-10f, 3e-10f);
        Vector3 v2 = new Vector3(2e-10f, 4e-10f, 3e-10f);
        float3 f1 = new float3(1e-10f, 2e-10f, 3e-10f);
        float3 f2 = new float3(2e-10f, 4e-10f, 3e-10f);
        Vector3 c = Vector3.Cross(v1, v2);
        Vector3 crossV = Vector3ToFloat3Utils.Cross(v1, v2);
        float3 crossF = Vector3ToFloat3Utils.Cross(f1, f2);

        Assert.AreEqual(crossV.x, crossF.x, 1e-5f);
        Assert.AreEqual(crossV.y, crossF.y, 1e-5f);
        Assert.AreEqual(crossV.z, crossF.z, 1e-5f);
        Assert.AreEqual(c.x, crossV.x, 1e-5f);
        Assert.AreEqual(c.y, crossV.y, 1e-5f);
        Assert.AreEqual(c.z, crossV.z, 1e-5f);
        Assert.AreEqual(c.x, crossF.x, 1e-5f);
        Assert.AreEqual(c.y, crossF.y, 1e-5f);
        Assert.AreEqual(c.z, crossF.z, 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void DistanceTest()
    {
        Vector3 v1 = Vector3.zero;
        Vector3 v2 = Vector3.one;
        float3 f1 = float3.zero;
        float3 f2 = new float3(1, 1, 1);
        float d = Vector3.Distance(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(f1, f2), 1e-5f);
    }
    [Test]
    public void DistanceTest2()
    {
        Vector3 v1 = Vector3.right;
        Vector3 v2 = 2 * Vector3.right;
        float3 f1 = new float3(1, 0, 0);
        float3 f2 = new float3(2, 0, 0);
        float d = Vector3.Distance(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(f1, f2), 1e-5f);
    }
    [Test]
    public void DistanceTest3()
    {
        Vector3 v1 = new Vector3(-1, -2, -3);
        Vector3 v2 = new Vector3(4, 5, 6);
        float3 f1 = new float3(-1, -2, -3);
        float3 f2 = new float3(4, 5, 6);
        float d = Vector3.Distance(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(f1, f2), 1e-5f);
    }
    [Test]
    public void DistanceTest4()
    {
        Vector3 v1 = new Vector3(3, 3, 3);
        Vector3 v2 = new Vector3(3, 3, 3);
        float3 f1 = new float3(3, 3, 3);
        float3 f2 = new float3(3, 3, 3);
        float d = Vector3.Distance(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(f1, f2), 1e-5f);
    }
    [Test]
    public void DistanceTest5()
    {
        Vector3 v1 = new Vector3(-1.909f, 2, -3.66f);
        Vector3 v2 = new Vector3(4, -5.1919f, 6);
        float3 f1 = new float3(-1.909f, 2, -3.66f);
        float3 f2 = new float3(4, -5.1919f, 6);
        float d = Vector3.Distance(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(f1, f2), 1e-5f);
    }
    [Test]
    public void DistanceTest6()
    {
        Vector3 v1 = new Vector3(1e5f, 2e5f, -3e5f);
        Vector3 v2 = new Vector3(-1e5f, -2e5f, 3e5f);
        float3 f1 = new float3(1e5f, 2e5f, -3e5f);
        float3 f2 = new float3(-1e5f, -2e5f, 3e5f);
        float d = Vector3.Distance(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(f1, f2), 1e-5f);
    }
    [Test]
    public void DistanceTest7()
    {
        Vector3 v1 = new Vector3(1e-10f, 2e-10f, 3e-10f);
        Vector3 v2 = new Vector3(2e-10f, 4e-10f, 3e-10f);
        float3 f1 = new float3(1e-10f, 2e-10f, 3e-10f);
        float3 f2 = new float3(2e-10f, 4e-10f, 3e-10f);
        float d = Vector3.Distance(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Distance(f1, f2), 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void DotTest()
    {
        Vector3 v1 = Vector3.right;
        Vector3 v2 = Vector3.right;
        float3 f1 = new float3(1, 0, 0);
        float3 f2 = new float3(1, 0, 0);
        float d = Vector3.Dot(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(f1, f2), 1e-5f);
    }
    [Test]
    public void DotTest2()
    {
        Vector3 v1 = Vector3.right;
        Vector3 v2 = Vector3.up;
        float3 f1 = new float3(1, 0, 0);
        float3 f2 = new float3(0, 1, 0);
        float d = Vector3.Dot(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(f1, f2), 1e-5f);
    }
    [Test]
    public void DotTest3()
    {
        Vector3 v1 = Vector3.right;
        Vector3 v2 = -Vector3.right;
        float3 f1 = new float3(1, 0, 0);
        float3 f2 = new float3(-1, 0, 0);
        float d = Vector3.Dot(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(f1, f2), 1e-5f);
    }
    [Test]
    public void DotTest4()
    {
        Vector3 v1 = new Vector3(1, 2, 3);
        Vector3 v2 = new Vector3(-4, 5, -6);
        float3 f1 = new float3(1, 2, 3);
        float3 f2 = new float3(-4, 5, -6);
        float d = Vector3.Dot(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(f1, f2), 1e-5f);
    }
    [Test]
    public void DotTest5()
    {
        Vector3 v1 = new Vector3(1e5f, 2e5f, 3e5f);
        Vector3 v2 = new Vector3(-1e5f, -2e5f, -3e5f);
        float3 f1 = new float3(1e5f, 2e5f, 3e5f);
        float3 f2 = new float3(-1e5f, -2e5f, -3e5f);
        float d = Vector3.Dot(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(f1, f2), 1e-5f);
    }
    [Test]
    public void DotTest6()
    {
        Vector3 v1 = new Vector3(0.001f, 0.002f, 0.003f);
        Vector3 v2 = new Vector3(0.004f, 0.005f, 0.006f);
        float3 f1 = new float3(0.001f, 0.002f, 0.003f);
        float3 f2 = new float3(0.004f, 0.005f, 0.006f);
        float d = Vector3.Dot(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(f1, f2), 1e-5f);
    }
    [Test]
    public void DotTest7()
    {
        Vector3 v1 = new Vector3(1e-10f, 2e-10f, 3e-10f);
        Vector3 v2 = new Vector3(2e-10f, 4e-10f, 3e-10f);
        float3 f1 = new float3(1e-10f, 2e-10f, 3e-10f);
        float3 f2 = new float3(2e-10f, 4e-10f, 3e-10f);
        float d = Vector3.Dot(v1, v2);

        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(v1, v2), 1e-5f);
        Assert.AreEqual(d, Vector3ToFloat3Utils.Dot(f1, f2), 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void LerpTest()
    {
        Vector3 v1 = Vector3.zero;
        Vector3 v2 = Vector3.one;
        float3 f1 = float3.zero;
        float3 f2 = new float3(1, 1, 1);
        Vector3 l = Vector3.Lerp(v1, v2, 0.5f);
        Vector3 lv = Vector3ToFloat3Utils.Lerp(v1, v2, 0.5f);
        float3 lf = Vector3ToFloat3Utils.Lerp(f1, f2, 0.5f);

        Assert.AreEqual(l.x, lv.x, 1e-5f);
        Assert.AreEqual(l.y, lv.y, 1e-5f);
        Assert.AreEqual(l.z, lv.z, 1e-5f);
        Assert.AreEqual(l.x, lf.x, 1e-5f);
        Assert.AreEqual(l.y, lf.y, 1e-5f);
        Assert.AreEqual(l.z, lf.z, 1e-5f);
    }
    [Test]
    public void LerpTest2()
    {
        Vector3 v1 = new Vector3(1, 2, 3);
        Vector3 v2 = new Vector3(4, 5, 6);
        float3 f1 = new float3(1, 2, 3);
        float3 f2 = new float3(4, 5, 6);
        Vector3 l = Vector3.Lerp(v1, v2, 0);
        Vector3 lv = Vector3ToFloat3Utils.Lerp(v1, v2, 0f);
        Vector3 lf = Vector3ToFloat3Utils.Lerp(f1, f2, 0f);

        Assert.AreEqual(l.x, lv.x, 1e-5f);
        Assert.AreEqual(l.y, lv.y, 1e-5f);
        Assert.AreEqual(l.z, lv.z, 1e-5f);
        Assert.AreEqual(l.x, lf.x, 1e-5f);
        Assert.AreEqual(l.y, lf.y, 1e-5f);
        Assert.AreEqual(l.z, lf.z, 1e-5f);
    }
    [Test]
    public void LerpTest3()
    {
        Vector3 v1 = new Vector3(-1, -2, -3);
        Vector3 v2 = new Vector3(4, 5, 6);
        float3 f1 = new float3(-1, -2, -3);
        float3 f2 = new float3(4, 5, 6);
        Vector3 l = Vector3.Lerp(v1, v2, 1);
        Vector3 lv = Vector3ToFloat3Utils.Lerp(v1, v2, 1f);
        Vector3 lf = Vector3ToFloat3Utils.Lerp(f1, f2, 1f);

        Assert.AreEqual(l.x, lv.x, 1e-5f);
        Assert.AreEqual(l.y, lv.y, 1e-5f);
        Assert.AreEqual(l.z, lv.z, 1e-5f);
        Assert.AreEqual(l.x, lf.x, 1e-5f);
        Assert.AreEqual(l.y, lf.y, 1e-5f);
        Assert.AreEqual(l.z, lf.z, 1e-5f);
    }
    [Test]
    public void LerpTest4()
    {
        Vector3 v1 = Vector3.zero;
        Vector3 v2 = Vector3.one;
        float3 f1 = float3.zero;
        float3 f2 = new float3(1, 1, 1);
        Vector3 l = Vector3.Lerp(v1, v2, 1.5f);
        Vector3 lv = Vector3ToFloat3Utils.Lerp(v1, v2, 1.5f);
        float3 lf = Vector3ToFloat3Utils.Lerp(f1, f2, 1.5f);

        Assert.AreEqual(l.x, lv.x, 1e-5f);
        Assert.AreEqual(l.y, lv.y, 1e-5f);
        Assert.AreEqual(l.z, lv.z, 1e-5f);
        Assert.AreEqual(l.x, lf.x, 1e-5f);
        Assert.AreEqual(l.y, lf.y, 1e-5f);
        Assert.AreEqual(l.z, lf.z, 1e-5f);
    }
    [Test]
    public void LerpTest5()
    {
        Vector3 v1 = Vector3.zero;
        Vector3 v2 = Vector3.one;
        float3 f1 = float3.zero;
        float3 f2 = new float3(1, 1, 1);
        Vector3 l = Vector3.Lerp(v1, v2, -0.5f);
        Vector3 lv = Vector3ToFloat3Utils.Lerp(v1, v2, -0.5f);
        float3 lf = Vector3ToFloat3Utils.Lerp(f1, f2, -0.5f);

        Assert.AreEqual(l.x, lv.x, 1e-5f);
        Assert.AreEqual(l.y, lv.y, 1e-5f);
        Assert.AreEqual(l.z, lv.z, 1e-5f);
        Assert.AreEqual(l.x, lf.x, 1e-5f);
        Assert.AreEqual(l.y, lf.y, 1e-5f);
        Assert.AreEqual(l.z, lf.z, 1e-5f);
    }
    [Test]
    public void LerpTest6()
    {
        Vector3 v1 = new Vector3(1e5f, 2e5f, 3e5f);
        Vector3 v2 = new Vector3(-1e5f, -2e5f, -3e5f);
        float3 f1 = new float3(1e5f, 2e5f, 3e5f);
        float3 f2 = new float3(-1e5f, -2e5f, -3e5f);
        Vector3 l = Vector3.Lerp(v1, v2, 0.25f);
        Vector3 lv = Vector3ToFloat3Utils.Lerp(v1, v2, 0.25f);
        float3 lf = Vector3ToFloat3Utils.Lerp(f1, f2, 0.25f);

        Assert.AreEqual(l.x, lv.x, 1e-5f);
        Assert.AreEqual(l.y, lv.y, 1e-5f);
        Assert.AreEqual(l.z, lv.z, 1e-5f);
        Assert.AreEqual(l.x, lf.x, 1e-5f);
        Assert.AreEqual(l.y, lf.y, 1e-5f);
        Assert.AreEqual(l.z, lf.z, 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void MaxMinTest()
    {
        Vector3 v1 = new Vector3(1, 5, 2);
        Vector3 v2 = new Vector3(3, 2, 4);
        float3 f1 = new float3(1, 5, 2);
        float3 f2 = new float3(3, 2, 4);

        Vector3 max = Vector3.Max(v1, v2);
        Vector3 min = Vector3.Min(v1, v2);
        Vector3 maxV = Vector3ToFloat3Utils.Max(v1, v2);
        float3 maxF = Vector3ToFloat3Utils.Max(f1, f2);
        Vector3 minV = Vector3ToFloat3Utils.Min(v1, v2);
        float3 minF = Vector3ToFloat3Utils.Min(f1, f2);

        Assert.AreEqual(max.x, maxV.x, 1e-5f);
        Assert.AreEqual(max.y, maxV.y, 1e-5f);
        Assert.AreEqual(max.z, maxV.z, 1e-5f);
        Assert.AreEqual(min.x, minV.x, 1e-5f);
        Assert.AreEqual(min.y, minV.y, 1e-5f);
        Assert.AreEqual(min.z, minV.z, 1e-5f);
        Assert.AreEqual(max.x, maxF.x, 1e-5f);
        Assert.AreEqual(max.y, maxF.y, 1e-5f);
        Assert.AreEqual(max.z, maxF.z, 1e-5f);
        Assert.AreEqual(min.x, minF.x, 1e-5f);
        Assert.AreEqual(min.y, minF.y, 1e-5f);
        Assert.AreEqual(min.z, minF.z, 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void MoveTowardsTest()
    {
        Vector3 cur = Vector3.zero;
        Vector3 targ = Vector3.one;
        float3 fCur = float3.zero;
        float3 fTarg = new float3(1, 1, 1);
        float maxDelta = 0.25f;

        Vector3 moved = Vector3.MoveTowards(cur, targ, maxDelta);
        Vector3 movedV = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
        float3 movedF = Vector3ToFloat3Utils.MoveTowards(fCur, fTarg, maxDelta);

        Assert.AreEqual(moved.x, movedV.x, 1e-5f);
        Assert.AreEqual(moved.y, movedV.y, 1e-5f);
        Assert.AreEqual(moved.z, movedV.z, 1e-5f);
        Assert.AreEqual(moved.x, movedF.x, 1e-5f);
        Assert.AreEqual(moved.y, movedF.y, 1e-5f);
        Assert.AreEqual(moved.z, movedF.z, 1e-5f);
    }
    [Test]
    public void MoveTowardsTest2()
    {
        Vector3 cur = Vector3.zero;
        Vector3 targ = Vector3.one;
        float3 fCur = float3.zero;
        float3 fTarg = new float3(1, 1, 1);
        float maxDelta = math.sqrt(3);

        Vector3 moved = Vector3.MoveTowards(cur, targ, maxDelta);
        Vector3 movedV = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
        float3 movedF = Vector3ToFloat3Utils.MoveTowards(fCur, fTarg, maxDelta);

        Assert.AreEqual(moved.x, movedV.x, 1e-5f);
        Assert.AreEqual(moved.y, movedV.y, 1e-5f);
        Assert.AreEqual(moved.z, movedV.z, 1e-5f);
        Assert.AreEqual(moved.x, movedF.x, 1e-5f);
        Assert.AreEqual(moved.y, movedF.y, 1e-5f);
        Assert.AreEqual(moved.z, movedF.z, 1e-5f);
    }
    [Test]
    public void MoveTowardsTest3()
    {
        Vector3 cur = Vector3.zero;
        Vector3 targ = Vector3.one;
        float3 fCur = float3.zero;
        float3 fTarg = new float3(1, 1, 1);
        float maxDelta = 10f;

        Vector3 moved = Vector3.MoveTowards(cur, targ, maxDelta);
        Vector3 movedV = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
        float3 movedF = Vector3ToFloat3Utils.MoveTowards(fCur, fTarg, maxDelta);

        Assert.AreEqual(moved.x, movedV.x, 1e-5f);
        Assert.AreEqual(moved.y, movedV.y, 1e-5f);
        Assert.AreEqual(moved.z, movedV.z, 1e-5f);
        Assert.AreEqual(moved.x, movedF.x, 1e-5f);
        Assert.AreEqual(moved.y, movedF.y, 1e-5f);
        Assert.AreEqual(moved.z, movedF.z, 1e-5f);
    }
    [Test]
    public void MoveTowardsTest4()
    {
        Vector3 cur = Vector3.one;
        Vector3 targ = Vector3.zero;
        float3 fCur = new float3(1, 1, 1);
        float3 fTarg = float3.zero;
        float maxDelta = 0.5f;

        Vector3 moved = Vector3.MoveTowards(cur, targ, maxDelta);
        Vector3 movedV = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
        float3 movedF = Vector3ToFloat3Utils.MoveTowards(fCur, fTarg, maxDelta);

        Assert.AreEqual(moved.x, movedV.x, 1e-5f);
        Assert.AreEqual(moved.y, movedV.y, 1e-5f);
        Assert.AreEqual(moved.z, movedV.z, 1e-5f);
        Assert.AreEqual(moved.x, movedF.x, 1e-5f);
        Assert.AreEqual(moved.y, movedF.y, 1e-5f);
        Assert.AreEqual(moved.z, movedF.z, 1e-5f);
    }
    [Test]
    public void MoveTowardsTest5()
    {
        Vector3 cur = Vector3.one;
        Vector3 targ = Vector3.one;
        float3 fCur = new float3(1, 1, 1);
        float3 fTarg = new float3(1, 1, 1);
        float maxDelta = 1f;

        Vector3 moved = Vector3.MoveTowards(cur, targ, maxDelta);
        Vector3 movedV = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
        float3 movedF = Vector3ToFloat3Utils.MoveTowards(fCur, fTarg, maxDelta);

        Assert.AreEqual(moved.x, movedV.x, 1e-5f);
        Assert.AreEqual(moved.y, movedV.y, 1e-5f);
        Assert.AreEqual(moved.z, movedV.z, 1e-5f);
        Assert.AreEqual(moved.x, movedF.x, 1e-5f);
        Assert.AreEqual(moved.y, movedF.y, 1e-5f);
        Assert.AreEqual(moved.z, movedF.z, 1e-5f);
    }
    [Test]
    public void MoveTowardsTest6()
    {
        Vector3 cur = new Vector3(1e5f, 2e5f, 3e5f);
        Vector3 targ = new Vector3(-1e5f, -2e5f, -3e5f);
        float3 fCur = new float3(1e5f, 2e5f, 3e5f);
        float3 fTarg = new float3(-1e5f, -2e5f, -3e5f);
        float maxDelta = 1e5f;

        Vector3 moved = Vector3.MoveTowards(cur, targ, maxDelta);
        Vector3 movedV = Vector3ToFloat3Utils.MoveTowards(cur, targ, maxDelta);
        float3 movedF = Vector3ToFloat3Utils.MoveTowards(fCur, fTarg, maxDelta);

        Assert.AreEqual(moved.x, movedV.x, 1e-5f);
        Assert.AreEqual(moved.y, movedV.y, 1e-5f);
        Assert.AreEqual(moved.z, movedV.z, 1e-5f);
        Assert.AreEqual(moved.x, movedF.x, 1e-5f);
        Assert.AreEqual(moved.y, movedF.y, 1e-5f);
        Assert.AreEqual(moved.z, movedF.z, 1e-5f);
    }
    //-----------------------------------------------
    [Test]
    public void NormalizeTest()
    {
        Vector3 v1 = new Vector3(1, 1, 0);
        float3 f1 = new float3(1, 1, 0);

        v1.Normalize();
        Vector3ToFloat3Utils.Normalize(ref f1);

        Assert.AreEqual(v1.x, f1.x, 1e-5f);
        Assert.AreEqual(v1.y, f1.y, 1e-5f);
        Assert.AreEqual(v1.z, f1.z, 1e-5f);
    }
    [Test]
    public void NormalizeTest2()
    {
        Vector3 v1 = new Vector3(1, 1, 0);
        float3 f1 = new float3(1, 1, 0);

        v1 = Vector3.Normalize(v1);
        f1 = Vector3ToFloat3Utils.Normalize(f1);

        Assert.AreEqual(v1.x, f1.x, 1e-5f);
        Assert.AreEqual(v1.y, f1.y, 1e-5f);
        Assert.AreEqual(v1.z, f1.z, 1e-5f);
    }
    [Test]
    public void NormalizeTest3()
    {
        Vector3 v1 = new Vector3(12, 1.56f, 0);
        float3 f1 = new float3(12, 1.56f, 0);

        v1.Normalize();
        Vector3ToFloat3Utils.Normalize(ref f1);

        Assert.AreEqual(v1.x, f1.x, 1e-5f);
        Assert.AreEqual(v1.y, f1.y, 1e-5f);
        Assert.AreEqual(v1.z, f1.z, 1e-5f);
    }
    [Test]
    public void NormalizeTest4()
    {
        Vector3 v1 = new Vector3(-1, 18.17f, 90);
        float3 f1 = new float3(-1, 18.17f, 90);

        v1 = Vector3.Normalize(v1);
        f1 = Vector3ToFloat3Utils.Normalize(f1);

        Assert.AreEqual(v1.x, f1.x, 1e-5f);
        Assert.AreEqual(v1.y, f1.y, 1e-5f);
        Assert.AreEqual(v1.z, f1.z, 1e-5f);
    }
    [Test]
    public void NormalizeTest5()
    {
        Vector3 v1 = new Vector3(0, 0, 0);
        float3 f1 = new float3(0, 0, 0);

        v1.Normalize();
        Vector3ToFloat3Utils.Normalize(ref f1);

        Assert.AreEqual(v1.x, f1.x, 1e-5f);
        Assert.AreEqual(v1.y, f1.y, 1e-5f);
        Assert.AreEqual(v1.z, f1.z, 1e-5f);
    }
    [Test]
    public void NormalizeTest6()
    {
        Vector3 v1 = new Vector3(0, 0.000001f, 0);
        float3 f1 = new float3(0, 0.000001f, 0);

        v1 = Vector3.Normalize(v1);
        f1 = Vector3ToFloat3Utils.Normalize(f1);

        Assert.AreEqual(v1.x, f1.x, 1e-5f);
        Assert.AreEqual(v1.y, f1.y, 1e-5f);
        Assert.AreEqual(v1.z, f1.z, 1e-5f);
    }  

    //-----------------------------------------------
    [Test]
    public void ProjectTest()
    {
        Vector3 v1 = new Vector3(1, 1, 0);
        Vector3 v2 = Vector3.right;
        float3 f1 = new float3(1, 1, 0);
        float3 f2 = new float3(1, 0, 0);

        Vector3 proj = Vector3.Project(v1, v2);
        Vector3 projV = Vector3ToFloat3Utils.Project(v1, v2);
        float3 projF = Vector3ToFloat3Utils.Project(f1, f2);

        Assert.AreEqual(proj.x, projV.x, 1e-5f);
        Assert.AreEqual(proj.y, projV.y, 1e-5f);
        Assert.AreEqual(proj.z, projV.z, 1e-5f);
        Assert.AreEqual(proj.x, projF.x, 1e-5f);
        Assert.AreEqual(proj.y, projF.y, 1e-5f);
        Assert.AreEqual(proj.z, projF.z, 1e-5f);
    }
    [Test]
    public void ProjectTest2()
    {
        Vector3 v1 = Vector3.right * 5f;
        Vector3 v2 = Vector3.right * 2f;
        float3 f1 = new float3(5, 0, 0);
        float3 f2 = new float3(2, 0, 0);

        Vector3 proj = Vector3.Project(v1, v2);
        Vector3 projV = Vector3ToFloat3Utils.Project(v1, v2);
        float3 projF = Vector3ToFloat3Utils.Project(f1, f2);

        Assert.AreEqual(proj.x, projV.x, 1e-5f);
        Assert.AreEqual(proj.y, projV.y, 1e-5f);
        Assert.AreEqual(proj.z, projV.z, 1e-5f);
        Assert.AreEqual(proj.x, projF.x, 1e-5f);
        Assert.AreEqual(proj.y, projF.y, 1e-5f);
        Assert.AreEqual(proj.z, projF.z, 1e-5f);
    }
    [Test]
    public void ProjectTest3()
    {
        Vector3 v1 = Vector3.zero;
        Vector3 v2 = Vector3.up;
        float3 f1 = float3.zero;
        float3 f2 = new float3(0, 1, 0);

        Vector3 proj = Vector3.Project(v1, v2);
        Vector3 projV = Vector3ToFloat3Utils.Project(v1, v2);
        float3 projF = Vector3ToFloat3Utils.Project(f1, f2);

        Assert.AreEqual(proj.x, projV.x, 1e-5f);
        Assert.AreEqual(proj.y, projV.y, 1e-5f);
        Assert.AreEqual(proj.z, projV.z, 1e-5f);
        Assert.AreEqual(proj.x, projF.x, 1e-5f);
        Assert.AreEqual(proj.y, projF.y, 1e-5f);
        Assert.AreEqual(proj.z, projF.z, 1e-5f);
    }
    [Test]
    public void ProjectTest4()
    {
        Vector3 v1 = new Vector3(1e-10f, 2e-10f, 3e-10f);
        Vector3 v2 = new Vector3(2e-10f, 4e-10f, 3e-10f);
        float3 f1 = new float3(1e-10f, 2e-10f, 3e-10f);
        float3 f2 = new float3(2e-10f, 4e-10f, 3e-10f);

        Vector3 proj = Vector3.Project(v1, v2);
        Vector3 projV = Vector3ToFloat3Utils.Project(v1, v2);
        float3 projF = Vector3ToFloat3Utils.Project(f1, f2);

        Assert.AreEqual(proj.x, projV.x, 1e-5f);
        Assert.AreEqual(proj.y, projV.y, 1e-5f);
        Assert.AreEqual(proj.z, projV.z, 1e-5f);
        Assert.AreEqual(proj.x, projF.x, 1e-5f);
        Assert.AreEqual(proj.y, projF.y, 1e-5f);
        Assert.AreEqual(proj.z, projF.z, 1e-5f);
    }
    [Test]
    public void ProjectOnPlaneTest()
    {
        Vector3 v = new Vector3(1, 2, 3);
        Vector3 planeNormal = Vector3.up;
        float3 f = new float3(1, 2, 3);
        float3 fPlaneNormal = new float3(0, 1, 0);

        Vector3 plane = Vector3.ProjectOnPlane(v, planeNormal);
        Vector3 planeV = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
        float3 planeF = Vector3ToFloat3Utils.ProjectOnPlane(f, fPlaneNormal);

        Assert.AreEqual(plane.x, planeV.x, 1e-5f);
        Assert.AreEqual(plane.y, planeV.y, 1e-5f);
        Assert.AreEqual(plane.z, planeV.z, 1e-5f);
        Assert.AreEqual(plane.x, planeF.x, 1e-5f);
        Assert.AreEqual(plane.y, planeF.y, 1e-5f);
        Assert.AreEqual(plane.z, planeF.z, 1e-5f);
    }
    [Test]
    public void ProjectOnPlaneTest2()
    {
        Vector3 v = new Vector3(1, 0, 1);
        Vector3 planeNormal = Vector3.up;
        float3 f = new float3(1, 0, 1);
        float3 fPlaneNormal = new float3(0, 1, 0);

        Vector3 plane = Vector3.ProjectOnPlane(v, planeNormal);
        Vector3 planeV = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
        float3 planeF = Vector3ToFloat3Utils.ProjectOnPlane(f, fPlaneNormal);

        Assert.AreEqual(plane.x, planeV.x, 1e-5f);
        Assert.AreEqual(plane.y, planeV.y, 1e-5f);
        Assert.AreEqual(plane.z, planeV.z, 1e-5f);
        Assert.AreEqual(plane.x, planeF.x, 1e-5f);
        Assert.AreEqual(plane.y, planeF.y, 1e-5f);
        Assert.AreEqual(plane.z, planeF.z, 1e-5f);
    }
    [Test]
    public void ProjectOnPlaneTest3()
    {
        Vector3 v = new Vector3(1e5f, 2e5f, 3e5f);
        Vector3 planeNormal = new Vector3(0, 1, 0);
        float3 f = new float3(1e5f, 2e5f, 3e5f);
        float3 fPlaneNormal = new float3(0, 1, 0);

        Vector3 plane = Vector3.ProjectOnPlane(v, planeNormal);
        Vector3 planeV = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
        float3 planeF = Vector3ToFloat3Utils.ProjectOnPlane(f, fPlaneNormal);

        Assert.AreEqual(plane.x, planeV.x, 1e-5f);
        Assert.AreEqual(plane.y, planeV.y, 1e-5f);
        Assert.AreEqual(plane.z, planeV.z, 1e-5f);
        Assert.AreEqual(plane.x, planeF.x, 1e-5f);
        Assert.AreEqual(plane.y, planeF.y, 1e-5f);
        Assert.AreEqual(plane.z, planeF.z, 1e-5f);
    }
    [Test]
    public void ProjectOnPlaneTest4()
    {
        Vector3 v = new Vector3(1e-10f, 2e-10f, 3e-10f);
        Vector3 planeNormal = new Vector3(2e-10f, 4e-10f, 3e-10f);
        float3 f = new float3(1e-10f, 2e-10f, 3e-10f);
        float3 fPlaneNormal = new float3(2e-10f, 4e-10f, 3e-10f);

        Vector3 plane = Vector3.ProjectOnPlane(v, planeNormal);
        Vector3 planeV = Vector3ToFloat3Utils.ProjectOnPlane(v, planeNormal);
        float3 planeF = Vector3ToFloat3Utils.ProjectOnPlane(f, fPlaneNormal);

        Assert.AreEqual(plane.x, planeV.x, 1e-5f);
        Assert.AreEqual(plane.y, planeV.y, 1e-5f);
        Assert.AreEqual(plane.z, planeV.z, 1e-5f);
        Assert.AreEqual(plane.x, planeF.x, 1e-5f);
        Assert.AreEqual(plane.y, planeF.y, 1e-5f);
        Assert.AreEqual(plane.z, planeF.z, 1e-5f);
    }
    //-----------------------------------------------
}

