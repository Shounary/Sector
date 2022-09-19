using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponBlueprint
{
    [Header("Parameters")]
    public float range = 20;
    public int damage = 20;
    public float fireRateBPS = 1f;
    public float bulletSpeed = 50;

    [Header("Visuals")]
    public GameObject bulletPrefab;
}
