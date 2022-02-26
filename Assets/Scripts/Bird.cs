using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spr;

    [SerializeField] private float launchForce = 500;
    [SerializeField] private float maxDragDistance = 5;
    private Vector2 _startPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _rb.position;
        _rb.isKinematic = true;
    }

    private void OnMouseDown()
    {
        _spr.color = Color.red;
    }
    
    private void OnMouseUp()
    {
        Vector2 currentPosition = _rb.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        _rb.isKinematic = false;
        _rb.AddForce(direction * launchForce);
        _spr.color = Color.white;
    }
    
    private void OnMouseDrag()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Above method returns Vector3, below line will create a V2 from it by omitting z
        Vector2 desiredPosition = mousePosition;

        var distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > maxDragDistance)
        {
            var direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * maxDragDistance);
        }
        
        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;
        
        _rb.position = desiredPosition;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rb.position = _startPosition;
        _rb.isKinematic = true;
        _rb.velocity = Vector2.zero;
    }
}
