using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControlledUnit : MobileAttackUnit
{
    [Header("Testing")]
    public GameObject movementGoal;
    public AIControlledUnit reference;

    void Start() {
        reference = this;
        attackAI.SetEnemyNames(enemyNames);
        if (UnitTable.instance.aiNames.Contains(ownerName)) {
            UnitTable.instance.aiUnitTable[ownerName].Add(this);
        }
        InvokeRepeating("FindRandomTarget", 0.7f, 0.5f);
        InvokeRepeating("IsAtFiringPosition", 1f, 0.5f);
    }

    void Update() {
        if (movementGoal != null) {
            moveAgent.SetDestination(movementGoal.transform.position);
        }
    }

    void FindRandomTarget() {
        if (movementGoal == null) {
            List<MobileAttackUnit> potentialTargets = new List<MobileAttackUnit>();
            foreach (SelectableAttackUnit playerUnit in UnitTable.instance.playerSelectableAttackUnits) { // check if player is enemy
                potentialTargets.Add(playerUnit);
            }
            foreach (string aiName in UnitTable.instance.aiNames) {
                if (!aiName.Equals(ownerName)) {
                    foreach (AIControlledUnit aIControlledUnit in UnitTable.instance.aiUnitTable[aiName]) {
                        potentialTargets.Add(aIControlledUnit);
                    }
                }
            }
            if (potentialTargets.Count > 0) {
                movementGoal = potentialTargets[Mathf.FloorToInt(Random.Range(0, potentialTargets.Count))].gameObject;
            }
        }
    }

    void IsAtFiringPosition() {
        if (movementGoal != null && Vector3.Distance(movementGoal.transform.position, attackAI.firePoint.position) <= attackAI.attackWeapon.range) {
            bool pathBlocked = Physics.Raycast(attackAI.firePoint.position, movementGoal.transform.position - attackAI.firePoint.position, attackAI.attackWeapon.range, attackAI.doNotFireThrough);
            if (!pathBlocked) {
                moveAgent.stoppingDistance = attackAI.attackWeapon.range;
            } else {
                moveAgent.stoppingDistance = 1.5f;
            }
        }
    }

    public void SetInitialOwnershipAndTargets(string newOwnerName, List<string> newEnemyNames) {
        ownerName = newOwnerName;
        enemyNames = newEnemyNames;
    }

    private void OnDestroy() {
        if (UnitTable.instance.aiNames.Contains(ownerName))
            UnitTable.instance.aiUnitTable[ownerName].Remove(this);
    }
}
