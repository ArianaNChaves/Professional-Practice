using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisions : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    private void OnCollisionEnter(Collision other)
    {
        
        other.gameObject.SetActive(false);
    }
}
