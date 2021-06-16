using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    public float destroyTime = 1f;
    public Vector3 offset = new Vector3(0, 2, 0);

    void Start()
    {
        Destroy(gameObject, destroyTime);

        Vector3 randomOffset = new Vector3(Random.Range(0f, 0.5f), Random.Range(0f, 1f), Random.Range(0f, 0.5f));
        offset += randomOffset;

        transform.localPosition += offset;
    }
}
