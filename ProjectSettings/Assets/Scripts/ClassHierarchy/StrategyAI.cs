using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyAI : MonoBehaviour
{
    [Header("AI Settings")]
    public string aiName;
    public List<string> enemyNames;
    public List<int> startingResources = new List<int>() { 0 };
    public List<UnitBlueprint> unitBlueprints;


    public float buildRate = 0.5f;
    private float buildCountdown = 1f;

    public float resourceIncomeRate = 4f;


    public List<int> resources;

    void Start()
    {
        resources = startingResources;
        InvokeRepeating("AddIncomeToResources", resourceIncomeRate, resourceIncomeRate);
    }

    private void Update() {
        if (buildCountdown > 0) {
            buildCountdown -= Time.deltaTime;
        } else {
            CreateUnitIfPossible(UnitTable.instance.aiFactoryTable[aiName][0], unitBlueprints[0]); //improve
            buildCountdown = buildRate;
        }
    }

    public bool CreateUnitIfPossible(Factory factory, UnitBlueprint blueprint) {
        for (int i = 0; i < resources.Count; i++) {
            if (resources[i] < blueprint.cost[i])
                return false;
        }
        for (int i = 0; i < resources.Count; i++) {
            resources[i] -= blueprint.cost[i];
        }

        GameObject unit = Instantiate(blueprint.unitPrefab, factory.spawnPoint.position, factory.spawnPoint.rotation);
        unit.GetComponent<AIControlledUnit>().SetInitialOwnershipAndTargets(aiName, enemyNames);

        return true;
    }

    public List<int> GetTotalIncome() {
        List<int> incomeResources = new List<int>();
        incomeResources.Add(0);
        foreach (Building building in UnitTable.instance.aiBuldingTable[aiName]) {
            for (int i = 0; i < building.resourceIncome.Count; i++) {
                incomeResources[i] += building.resourceIncome[i];
            }
        }
        return incomeResources;
    }

    public void AddIncomeToResources() {
        List<int> incomeResources = GetTotalIncome();
        for (int i = 0; i < resources.Count; i++) {
            resources[i] += incomeResources[i];
        }
    }
}
