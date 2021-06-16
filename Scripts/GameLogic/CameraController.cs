using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;  
    public float pitch = 2f;

    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 11f;   
    private float currentZoom = 10f;

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            currentZoom += Input.GetAxis("Mouse ScrollWheel") *-2;
            currentZoom = isBetween(currentZoom, minZoom, maxZoom);
        }       
    }

    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
    }

    private float isBetween(float currentNumber, float minNumber, float maxNumber)
    { 
        if(currentNumber > maxNumber) return maxNumber;
        if(currentNumber < minNumber) return minNumber;
        return currentNumber;    
    }
}


