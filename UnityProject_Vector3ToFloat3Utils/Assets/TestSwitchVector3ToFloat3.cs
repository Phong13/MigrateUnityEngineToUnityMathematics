using UnityEngine;

public class TestSwitchVector3ToFloat3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 a = new Vector3(1, 2, 3);
        Debug.Log("length: " + Vector3Utils.length(a));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
