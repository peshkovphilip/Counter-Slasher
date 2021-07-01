using UnityEngine;
using System;

public class Extra //: MonoBehaviour
{
    public void GetBonus(GameObject target, Collider2D collisionCollider)
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
                //Destroy(collisionObject); // how to destroy object wuthout use Monobeh ?
            }
        }
    }
}