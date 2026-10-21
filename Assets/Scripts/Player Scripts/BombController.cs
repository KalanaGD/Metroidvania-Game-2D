using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private float _explosionTime;
    [SerializeField] private GameObject _explosion;
    public float _blastRange;
    public LayerMask _distructLayer;
    

    void Update()
    {
        _explosionTime -= Time.deltaTime;
        if (_explosionTime <= 0f && _explosion != null)
        {
            if (_explosion != null)
            {
                Instantiate(_explosion, transform.position, transform.rotation);
            }
            Destroy(gameObject);

            Collider2D[] _objectsToRemove = Physics2D.OverlapCircleAll(transform.position, _blastRange, _distructLayer);

            if (_objectsToRemove.Length > 0)
            {
                foreach (Collider2D colliders in _objectsToRemove)
                {
                    Destroy(colliders.gameObject);
                }
            }
        }

       
    }
}
