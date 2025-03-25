using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroyer : MonoBehaviour
{
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Destroy(gameObject, ps.duration);
    }
}
