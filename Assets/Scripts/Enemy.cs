using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDestroyable, IDamageble
{
    [SerializeField] private int health = 50;
    [SerializeField] private int damage = 50;
    private bool activeDamage = true;
    private bool activeDestroy = true;
    public event Action<GameObject, Collider2D> OnEnter;
    public event Action<Collider2D> OnExit;

    private void Start()
    {
        // initialize something
    }

    public bool SetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(WaitForDestroy(gameObject));
            return true; // set death
        }   
        else
        {
            return false; // take damage
        }
    }
    public int Damage()
    {
        return this.damage;
    }
    public bool GetActiveDamage()
    {
        return activeDamage;
    }
    public void SetActiveDamage(bool active)
    {
        activeDamage = active;
    }

    public bool GetActiveDestroy()
    {
        return activeDestroy;
    }
    public void SetActiveDestroy(bool active)
    {
        activeDestroy = active;
    }

    private void OnTriggerEnter2D(Collider2D currentCollider)
    {
        OnEnter?.Invoke(gameObject, currentCollider);
    }
    private void OnTriggerExit2D(Collider2D currentCollider)
    {
        OnExit?.Invoke(currentCollider);
    }

    private IEnumerator WaitForDestroy(GameObject destroyObject)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(destroyObject);
    }
}
