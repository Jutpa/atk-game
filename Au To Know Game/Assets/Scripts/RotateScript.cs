using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float rotX;
    public float rotXval;
    public float rotY;
    public float rotYval;
    public float rotZ;
    public float rotZval;

    private void FixedUpdate()
    {
        transform.Rotate(rotX, rotY, rotZ);
        rotXval += rotX;
        rotYval += rotY;
        rotZval += rotZ;
    }
}
