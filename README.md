This is a C# console application that will help migrate:
    UnityEngine.Vector3  to  Unity.Mathematics.float3
    UnityEngine.Quaternion  to  Unity.Mathematics.quaternion

It uses Roslyn to crawl the code looking for instance methods like:
    myVector3.magnitude
    myVector3.sqrMagnitude
    myVector3.normalized
    myVector3.Normalize()
    == 
    
STEP 1  
    These are replaced with inline static wrapper methods:
         Vector3ToFloat3Utils.length( myVector3 )
         Vector3ToFloat3Utils.sqrlength( myVector3 )
         Vector3ToFloat3Utils.normalize( myVector3 )
         Vector3ToFloat3Utils.Normalize( ref myVector3 )

After these changes the project should still run perfectly using Vector3. 
    
STEP 2

Replace Vector3 with float3. Since "Vector3ToFloat3Utils contains overloaded methods:
       public static float Vector3ToFloat3Utils.length( Vector3 myVector3 )
       public static float Vector3ToFloat3Utils.length( float3 myVector3 )

The project should still fun with very few errors.

STEP 3

Eliminate the static wrapper functions: 
       Vector3ToFloat3Utils.length -> Unity.Mathematics.length()

Use a similar 3 step process with Quaternion
    
    
    
