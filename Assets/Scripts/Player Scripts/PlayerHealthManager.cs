using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager instance;

    private int _maxHealth = 10;
    [SerializeField]private int _currenthealth;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _currenthealth = _maxHealth;
    }

    public void DamagePlayer(int _damageAmount)
    {
        _currenthealth -= _damageAmount;

        if(_currenthealth <= 0)
        {
            _currenthealth = 0;
            gameObject.SetActive(false);

        }
    }


}
