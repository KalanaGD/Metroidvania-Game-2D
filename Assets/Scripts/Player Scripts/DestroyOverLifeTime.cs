using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverLifeTime : MonoBehaviour
{
    public float _lifeTime;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    
}
