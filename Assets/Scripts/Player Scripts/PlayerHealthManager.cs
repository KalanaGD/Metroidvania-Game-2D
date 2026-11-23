using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager instance;

    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _currenthealth;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }

    private void Start()
    {
        _currenthealth = _maxHealth;
    }

    public void DamagePlayer(int _damageAmount)
    {
        _currenthealth -= _damageAmount;

        if (_currenthealth <= 0)
        {
            _currenthealth = 0;
            //gameObject.SetActive(false);

            RespawnController.instance.Respawn();

        }
    }

    public void  FillHealth()
    {
        _currenthealth = _maxHealth;

        // uicontroller.instance.upadate health (current, max health)
    }


}
