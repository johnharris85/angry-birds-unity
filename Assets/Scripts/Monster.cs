using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ensures that the 'parent' component is selected in Unity when clicking on it
// Good for components with lots of children
[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private new ParticleSystem particleSystem;

    public bool dead = false;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (ShouldDieFromCollision(col))
        {
            Die();
        }
        
    }

    private bool ShouldDieFromCollision(Collision2D col)
    {
        if (dead)
            return false;
        
        var bird = col.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;
        
        // Contacts is an array of positions where contact occurred
        if (col.contacts[0].normal.y < -0.5)
        {
            return true;
        }

        return false;
    }

    private void Die()
    {
        particleSystem.Play();
        GetComponent<SpriteRenderer>().sprite = deadSprite;
        dead = true;
    }
}
