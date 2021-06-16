using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWallRock : MonoBehaviour
{
    public GameObject dustParticle;
    Vector3 pos;
    [SerializeField] Vector3 offset;
    [SerializeField] float speed;


    private void OnEnable()
    {
        pos = transform.position;
        transform.position = pos + offset;
        
        Invoke(nameof(PlayDustParticles), 0.5f);           
    }

    void Update()
    {
        if(transform.position != pos)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);           
        }
    }

    private void OnDisable()
    {
        Instantiate(dustParticle, transform.position, transform.rotation);
    }

    private void PlayDustParticles()
    {
        Instantiate(dustParticle, transform.position, transform.rotation);
    }
}
