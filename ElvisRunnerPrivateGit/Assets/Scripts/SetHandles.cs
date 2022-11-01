using UnityEngine;

public struct SetHandles
{
    /// <summary>
    /// Set new position in Vector3.x
    /// </summary>
    /// <returns></returns>
    static public Vector3 SetVector3x(Vector3 vector3,float value)
    {
        vector3.x = value;
        return vector3;
    }

    /// <summary>
    /// Set new position in Vector3.x
    /// </summary>
    /// <returns></returns>
    static public Vector3 SetVector3y(Vector3 vector3, float value)
    {
        vector3.y = value;
        return vector3;
    }

    /// <summary>
    /// Set new position in Vector3.x
    /// </summary>
    /// <returns></returns>
    static public Vector3 SetVector3z(Vector3 vector3, float value)
    {
        vector3.z = value;
        return vector3;
    }
}
