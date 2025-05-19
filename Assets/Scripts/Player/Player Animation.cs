using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        animator.SetBool(IsMoving, _rigidbody.velocity.magnitude > 0f);
    }
}
