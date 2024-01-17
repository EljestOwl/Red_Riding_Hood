using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CustomInput input = null;
    Rigidbody2D _rb;
    BoxCollider2D _collider;

    private float _movementInputDirection;

    private void Awake()
    {
        input = new CustomInput();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void CheckInput()
    {
        
    }

}
