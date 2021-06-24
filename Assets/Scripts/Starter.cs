using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyable
{
    public bool SetDamage(int damage);
    public bool GetActiveDestroy();
    public void SetActiveDestroy(bool active);
}

public interface IDamageble
{
    public int Damage();
    public bool GetActiveDamage();
    public void SetActiveDamage(bool active);
}

public interface IBonusable
{
    public int GetActivatedBonuses();
    public void SetHealth(int health);
    public int GetHealth();
}

public class Starter : MonoBehaviour
{
    private Player player;
    private Enemy[] enemies;

    void Start()
    {
        // init other classes    
    }
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
        player.OnEnter -= SetDamage;
        player.OnEnter -= GetBonus;

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
                destroyable.SetDamage(damage);
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

    void Update()
    {
        // update per frame each needed class
    }
}
