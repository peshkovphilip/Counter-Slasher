using UnityEngine;

public class EnemyController : IStarter, IDestroyer
{
    private Damage damage;

    public void Starter()
    {
        damage = new Damage();
        Enemy[] enemies = Object.FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.OnEnter += damage.SetDamage;
            enemy.OnExit += damage.SetDamageble;
        }
    }

    public void Destroyer()
    {
        Enemy[] enemies = Object.FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.OnEnter -= damage.SetDamage;
            enemy.OnExit -= damage.SetDamageble;
        }
    }

}
