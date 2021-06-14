using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyable
{
    public bool SetDamage(int damage);
}

public interface IDamageble
{
    public int Damage();
}

public class Starter : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
