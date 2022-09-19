using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileAttackUnit : MobileEntity
{
    public List<string> enemyNames;
    public AttackAI attackAI;

    void Start()
    {
        attackAI.SetEnemyNames(enemyNames);
    }
}
