using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{
    public List<int> resourceIncome = new List<int>() { 0 };
    public string ownerName = "";
    public bool isSelectable = false;

    void Start()
    {
        if (ownerName.Equals(UnitTable.instance.playerName)) {
            isSelectable = true;
            UnitTable.instance.playerBuildings.Add(this);
        } else if (UnitTable.instance.aiNames.Contains(ownerName)) {
            UnitTable.instance.aiBuldingTable[ownerName].Add(this);
        }
    }

    public virtual void ChangeOwner(string newOwnerName) {
        if (UnitTable.instance.aiNames.Contains(ownerName))
            UnitTable.instance.aiBuldingTable[ownerName].Remove(this);
        if (UnitTable.instance.playerName.Equals(ownerName))
            UnitTable.instance.playerBuildings.Remove(this);

        if (UnitTable.instance.aiNames.Contains(newOwnerName))
            UnitTable.instance.aiBuldingTable[newOwnerName].Add(this);
        if (UnitTable.instance.playerName.Equals(newOwnerName))
            UnitTable.instance.playerBuildings.Add(this);

        ownerName = newOwnerName;
    }

    private void OnDestroy() {
        if (UnitTable.instance.aiNames.Contains(ownerName))
            UnitTable.instance.aiBuldingTable[ownerName].Remove(this);
        if (UnitTable.instance.playerName.Equals(ownerName))
            UnitTable.instance.playerBuildings.Remove(this);
    }
}
