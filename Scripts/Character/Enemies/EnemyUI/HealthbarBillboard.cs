using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarBillboard : MonoBehaviour
{
    public Transform cam;

    private void Awake()
    {
        if (cam == null)
        {
            Debug.Log("Camera on HealthbarBillboard not assigned!");
            cam = Camera.main.transform;
        }
        
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}