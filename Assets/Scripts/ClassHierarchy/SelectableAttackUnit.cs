using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableAttackUnit : MobileAttackUnit
{
    
    public Color selectColor;

    [Header("Internal references")]
    public Renderer rend;

    private Color originalColor;

    void Start() {
        attackAI.SetEnemyNames(enemyNames);
        originalColor = rend.material.color;
        UnitTable.instance.playerSelectableAttackUnits.Add(this);
    }

    public void BecomeSelected() {
        rend.material.color = selectColor;
    }

    public void BecomeDeselected() {
        rend.material.color = originalColor;
    }

    private void OnDestroy() {
        UnitTable.instance.playerSelectableAttackUnits.Remove(this);
        UnitTable.instance.selectedAttackUnits.Remove(this);
    }
}
