This project is pretty rough, but did save days of mind-numbing, error-prone, tedious find/replace. There was still some find/replace but it was reduced to a few hundred rather than thousands.

This is a C# console application that will help migrate:
    - UnityEngine.Vector3  to  Unity.Mathematics.float3
    - UnityEngine.Quaternion  to  Unity.Mathematics.quaternion

It uses Roslyn to crawl the code looking for instance methods like:
    - myVector3.magnitude
    - myVector3.sqrMagnitude
    - myVector3.normalized
    - myVector3.Normalize()
    - == 
    - myQuat * myQuat
    - myQuat * myVector3
    
STEP 1  

These are replaced with inline static wrapper methods:
        - Vector3ToFloat3Utils.magnitude( myVector3 )
        - Vector3ToFloat3Utils.sqrMagnitude( myVector3 )
        - Vector3ToFloat3Utils.normalized( myVector3 )
        - Vector3ToFloat3Utils.Normalize( ref myVector3 )

After these changes there will be some errors usually for edge cases where there are mixed arguments float3, Vector3. Fix these manually. 

STEP 2

Eliminate the static wrapper functions using manual find/replace: 
       - Vector3ToFloat3Utils.length -> Unity.Mathematics.length()
       - etc...

Use a similar 3 step process with Quaternion


-----USAGE-----

STEP 1

Create a text file containing a list of allowed namespaces and a list of excluded filenames in this format (not creating/using a config file allows all files to be changed by the rewriter):

ALLOWED_NAMESPACES:
UnityVector3Refactor

EXCLUDED_FILENAMES:
Vector3ToFloat3Utils.cs
NameOfExcludedFile.cs

STEP 2

Add Vector3ToFloat3Utils.cs to your project (or the Quaternion wrapper equivalent)

STEP 3

Navigate to where your project is saved and run the rewriter with either:
    -Vector3ToV3F3UtilsMigration "path_to_unity_project_or_solution_file"
    or
    -Vector3ToV3F3UtilsMigration "path_to_unity_project_or_solution_file" "path_to_config_file"

Example WITHOUT config file:
    dotnet run --project Vector3ToV3F3UtilsMigration.csproj -- "C:\Users\rowan\Workspace\Unity\Vector3ToFloat3UtilsTesting\Assembly-CSharp.csproj" 
Example WITH config file: 
    dotnet run --project Vector3ToV3F3UtilsMigration.csproj -- "C:\Users\rowan\Workspace\Unity\Vector3ToFloat3UtilsTesting\Assembly-CSharp.csproj" "C:\Users\rowan\Workspace\Unity\MigrateUnityEngineToUnityMathematics\config.txt"
    
