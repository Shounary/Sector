using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public static List<int> enemyBuildings;
    public bool startIsDestroyed = false;
    public static bool isDestroyed;
    public int startEnergyResource = 1000;
    public static int currentEnergyResource;
    public int startEnergyResourceIncome = 50; // per 5 seconds
    public static int currentEnergyResourceIncome; // per 5 seconds

    private void Start() {
        currentEnergyResource = startEnergyResource;
        isDestroyed = startIsDestroyed;
        currentEnergyResourceIncome = startEnergyResourceIncome;
    }







}
