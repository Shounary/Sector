using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTable : MonoBehaviour
{
    [HideInInspector]
    public static UnitTable instance;

    public string playerName;
    public List<StrategyAI> strategyAIs;

    [Header("Testing")]
    public List<string> aiNames = new List<string>();

    public HashSet<SelectableAttackUnit> selectedAttackUnits;
    public List<SelectableAttackUnit> playerSelectableAttackUnits;
    public Building selectedBuilding;
    public List<Building> playerBuildings;
    public List<Factory> playerFactories;

    public Dictionary<string, List<AIControlledUnit>> aiUnitTable;
    public Dictionary<string, List<Building>> aiBuldingTable;
    public Dictionary<string, List<Factory>> aiFactoryTable;


    private void Awake() {
        instance = this;

        foreach (StrategyAI ai in strategyAIs) {
            aiNames.Add(ai.aiName);
        }

        selectedAttackUnits = new HashSet<SelectableAttackUnit>();
        playerSelectableAttackUnits = new List<SelectableAttackUnit>();
        playerBuildings = new List<Building>();
        playerFactories = new List<Factory>();



        aiUnitTable = new Dictionary<string, List<AIControlledUnit>>();
        foreach (string aiName in aiNames) {
            aiUnitTable.Add(aiName, new List<AIControlledUnit>());
        }

        aiBuldingTable = new Dictionary<string, List<Building>>();
        foreach (string aiName in aiNames) {
            aiBuldingTable.Add(aiName, new List<Building>());
        }

        aiFactoryTable = new Dictionary<string, List<Factory>>();
        foreach (string aiName in aiNames) {
            aiFactoryTable.Add(aiName, new List<Factory>());
        }
    }
}
