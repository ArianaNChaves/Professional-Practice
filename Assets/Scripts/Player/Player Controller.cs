using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        PlayerMovement _playerMovement = GetComponent<PlayerMovement>();
    }
}
