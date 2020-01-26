using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float movementSpeed = 1;
  
    void Update()
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);
        
    }
}
