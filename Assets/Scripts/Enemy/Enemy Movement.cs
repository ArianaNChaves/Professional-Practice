using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : BaseEnemy
{

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentState = State.Moving;
        StartCoroutine(Moving());
    }
    protected override IEnumerator Moving()
    {
        while (_currentState == State.Moving)
        {
            //mira al platyer
            _targetDirection = _target.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(_targetDirection);
            Quaternion smoothRotation = Quaternion.Slerp(enemyVisual.rotation, newRotation,rotationSpeed * Time.deltaTime);
            enemyVisual.rotation = smoothRotation;
            
            //todo Mejorar el movimiento 
            _rigidbody.MovePosition(transform.position + _targetDirection.normalized * (moveSpeed * Time.deltaTime));
            yield return null;
        }
    }
}
