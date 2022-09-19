using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building
{
    public Transform spawnPoint;

    void Start() {
        if (ownerName.Equals(UnitTable.instance.playerName)) {
            isSelectable = true;
            UnitTable.instance.playerBuildings.Add(this);
            UnitTable.instance.playerFactories.Add(this);
        } else if (UnitTable.instance.aiNames.Contains(ownerName)) {
            UnitTable.instance.aiBuldingTable[ownerName].Add(this);
            UnitTable.instance.aiFactoryTable[ownerName].Add(this);
        }
    }

    public override void ChangeOwner(string newOwnerName) {
        if (UnitTable.instance.aiNames.Contains(ownerName))
            UnitTable.instance.aiFactoryTable[ownerName].Remove(this);
        if (UnitTable.instance.playerName.Equals(ownerName))
            UnitTable.instance.playerFactories.Remove(this);
        base.ChangeOwner(newOwnerName);
    }

    private void OnDestroy() {
        if (UnitTable.instance.aiNames.Contains(ownerName)) {
            UnitTable.instance.aiBuldingTable[ownerName].Remove(this);
            UnitTable.instance.aiFactoryTable[ownerName].Remove(this);
        }
        if (UnitTable.instance.playerName.Equals(ownerName)) {
            UnitTable.instance.playerBuildings.Remove(this);
            UnitTable.instance.playerFactories.Remove(this);
        }
    }
}
