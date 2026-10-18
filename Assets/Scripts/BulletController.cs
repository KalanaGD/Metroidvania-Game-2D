using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody2D _bulletRb;
    [SerializeField] private float _bulletSpeed;
    public GameObject _impactFx;
    public Vector2 _moveDirection;


    void Update()
    {
        _bulletRb.velocity =  _moveDirection * _bulletSpeed;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(_impactFx, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
    
}
