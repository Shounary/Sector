using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntelligence : MonoBehaviour
{
    [HideInInspector]
    public static PlayerIntelligence instance;

    [Header("Building & Resourses")]
    public List<int> startingResources = new List<int>() { 0 };
    public List<int> playerResources;

    public List<UnitBlueprint> playerBlueprints;

    public float buildRate = 0.5f;
    private float buildCountdown = 1f;

    public float resourceIncomeRate = 4f;


    private void Awake() {
        instance = this;
    }


    void Start() {
        playerResources = startingResources;
        InvokeRepeating("AddIncomeToResources", resourceIncomeRate, resourceIncomeRate);
    }

    private void Update() {
        if (buildCountdown > 0) {
            buildCountdown -= Time.deltaTime;
        } else {
            if (Input.GetKeyUp("b") && UnitTable.instance.selectedBuilding != null && UnitTable.instance.selectedBuilding.GetType() == typeof(Factory)) {
                CreateUnitIfPossible((Factory) UnitTable.instance.selectedBuilding, playerBlueprints[0]);
                buildCountdown = buildRate;
            } else {
                buildCountdown = 0;
            }
        }
    }

    public bool CreateUnitIfPossible(Factory factory, UnitBlueprint blueprint) {
        for (int i = 0; i < playerResources.Count; i++) {
            if (playerResources[i] < blueprint.cost[i])
                return false;
        }
        for (int i = 0; i < playerResources.Count; i++) {
            playerResources[i] -= blueprint.cost[i];
        }

        GameObject unit = Instantiate(blueprint.unitPrefab, factory.spawnPoint.position, factory.spawnPoint.rotation);

        return true;
    }

    public List<int> GetTotalIncome() {
        List<int> incomeResources = new List<int>();
        incomeResources.Add(0);
        foreach (Building building in UnitTable.instance.playerBuildings) {
            for (int i = 0; i < building.resourceIncome.Count; i++) {
                incomeResources[i] += building.resourceIncome[i];
            }
        }
        return incomeResources;
    }

    public void AddIncomeToResources() {
        List<int> incomeResources = GetTotalIncome();
        for (int i = 0; i < playerResources.Count; i++) {
            playerResources[i] += incomeResources[i];
        }
    }
}
