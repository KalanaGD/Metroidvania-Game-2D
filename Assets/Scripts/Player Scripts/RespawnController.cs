using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{

    public static RespawnController instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private Vector3 _respawnPoint;
    private float _waitToRespawn = 2f;

    private GameObject _player;

    void Start()
    {
        _player = PlayerHealthManager.instance.gameObject;

        _respawnPoint = _player.transform.position;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        _player.SetActive(false);
        yield return new WaitForSeconds(_waitToRespawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _player.SetActive(true);

        PlayerHealthManager.instance.FillHealth();


    }
}
