using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDestroyable
{
    [SerializeField] private int health = 100;
    private bool isDestroyable = true;
    
    public bool SetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(SetDestroy(gameObject));
            return true; // set death
        }   
        else
        {
            return false; // take damage
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision)
        {
            if ((collision.gameObject.GetComponent<IDamageble>() != null) && (isDestroyable))
            {
                int damage = collision.gameObject.GetComponent<IDamageble>().Damage();
                if (collision.gameObject.GetComponent<IDestroyable>() != null)
                {
                    collision.gameObject.GetComponent<IDestroyable>().SetDamage(health);
                }
                SetDamage(damage);
                isDestroyable = false;
            }
        }
    }

    private IEnumerator SetDestroy(GameObject destroyObject)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(destroyObject);
    }
}
