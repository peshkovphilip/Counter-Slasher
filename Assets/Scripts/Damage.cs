using UnityEngine;
using System;

public class Damage
{
    public event Action<bool> Restart;
    public void SetDamage(GameObject target, Collider2D collisionCollider)
    {
        GameObject collisionObject = collisionCollider.gameObject;
        if ((collisionObject.GetComponent<IDamageble>() != null) && (target.GetComponent<IDestroyable>() != null))
        {
            IDamageble damagable = collisionObject.GetComponent<IDamageble>();
            IDestroyable destroyable = target.GetComponent<IDestroyable>();
            if ((damagable.GetActiveDamage()) && (destroyable.GetActiveDestroy()))
            {
                int damage = damagable.Damage();
                if (destroyable.SetDamage(damage))
                {
                    if (target.GetComponent<Player>() != null)
                        try
                        {
                            Restart.Invoke(true); // NullReferenceException if Enemy detroyed
                        } 
                        catch
                        {
                            Debug.Log("object destroyed");
                        }
                        
                }
                damagable.SetActiveDamage(false);
            }
        }
    }
    public void SetDamageble(Collider2D collisionCollider)
    {
        GameObject collisionObject = collisionCollider.gameObject;
        if (collisionObject.GetComponent<IDamageble>() != null)
        {
            collisionObject.GetComponent<IDamageble>().SetActiveDamage(true);
        }
    }
}