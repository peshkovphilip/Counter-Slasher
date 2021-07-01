using UnityEngine;

public class PlayerController : IStarter, IDestroyer
{
    private Player player;
    private Damage damage;
    private Extra extra;

    public void Starter()
    {
        player = Object.FindObjectOfType<Player>();
        extra = new Extra(); 
        //extra = Object.FindObjectOfType<Extra>();
        damage = new Damage();
        player.OnEnter += damage.SetDamage;
        player.OnEnter += extra.GetBonus;
    }

    public void Destroyer()
    {
        player = Object.FindObjectOfType<Player>();
        if (player)
        {
            player.OnEnter -= damage.SetDamage;
            player.OnEnter -= extra.GetBonus;
        }
    }
    
}
