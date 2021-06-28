using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    private Player player;
    private Enemy[] enemies;
    public event Action<bool> Restart;

    private void OnEnable()
    {
        player = FindObjectOfType<Player>();
        player.OnEnter += SetDamage;
        player.OnEnter += GetBonus;

        enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.OnEnter += SetDamage;
            enemy.OnExit += SetDamageble;
        }
    }

    private void OnDisable()
    {
        player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnEnter -= SetDamage;
            player.OnEnter -= GetBonus;
        }

        enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.OnEnter -= SetDamage;
            enemy.OnExit -= SetDamageble;
        }
    }

    private void SetDamage(GameObject target, Collider2D collisionCollider)
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
                        Restart.Invoke(true);
                }
                damagable.SetActiveDamage(false);
            }
        }
    }
    private void SetDamageble(Collider2D collisionCollider)
    {
        GameObject collisionObject = collisionCollider.gameObject;
        if (collisionObject.GetComponent<IDamageble>() != null)
        {
            collisionObject.GetComponent<IDamageble>().SetActiveDamage(true);
        }
    }
    private void GetBonus(GameObject target, Collider2D collisionCollider)
    {
        GameObject collisionObject = collisionCollider.gameObject;
        if ((collisionObject.GetComponent<Bonus>() != null) && (target.GetComponent<IBonusable>() != null))
        {
            IBonusable bonusable = target.GetComponent<IBonusable>();
            Bonus bonus = collisionObject.GetComponent<Bonus>();
            if (bonus.isActive)
            {
                bonus.isActive = false;
                if (bonus.bonusType == Bonus.TypeOfBonus.ChangeHealth)
                {
                    bonusable.SetHealth((int)(bonusable.GetHealth() * bonus.value));
                }
                Destroy(collisionObject);
            }
        }
    }
}
