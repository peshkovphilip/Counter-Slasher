using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bonus : Starter
{
    [SerializeField] public float value = 2f;
    [SerializeField] public TypeOfBonus bonusType = 0;
    [SerializeField] public bool isActive = true;
    public enum TypeOfBonus
    {
        ChangeHealth = 0,
        ChangeSpeed = 1,
    }
}
