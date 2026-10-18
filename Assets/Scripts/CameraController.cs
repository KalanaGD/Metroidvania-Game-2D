using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private PlayerController _Player;

    void Start()
    {
        _Player =  FindObjectOfType<PlayerController>();
    }
    
    void Update()
    {
        transform.position = new Vector3(_Player.transform.position.x, _Player.transform.position.y,transform.position.z);
    }
}
