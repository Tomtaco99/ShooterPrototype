using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    public EnemyHealth health;
    public BasicEnemyMovement movement;
    public BasicEnemyReactions reaction;

    private void Start()
    {
        health.OnTakeDamage += reaction.HandleReaction;
        health.OnDeath += Die;
    }

    private void Die(Vector3 pos)
    {
        movement.Stop();
        reaction.HandleDeath();
    }

    private void Stop()
    {
        throw new NotImplementedException();
    }
    
}
