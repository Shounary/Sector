using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAI : MonoBehaviour
{
    [Header("Gun")]
    public WeaponBlueprint attackWeapon;

    [Header("AI")]
    public LayerMask doNotFireThrough;

    [Header("Internal References")]
    public Transform rotationPoint;
    public Transform firePoint;

    [Header("Testing")]
    public GameObject currentTarget;
    public List<string> enemyNames;

    private float countdown = .5f;

    void Start() {
        InvokeRepeating("FindTarget", 0.5f, 0.5f);
        countdown = 1f;
    }

    void Update() {
        if (countdown > 0)
            countdown -= Time.deltaTime;

        if (countdown <= 0) {
            countdown = 0;
            if (currentTarget != null && Vector3.Distance(currentTarget.transform.position, firePoint.position) <= attackWeapon.range) {
                rotationPoint.LookAt(currentTarget.transform);
                bool pathBlocked = Physics.Raycast(firePoint.position, currentTarget.transform.position - firePoint.position, attackWeapon.range, doNotFireThrough);
                if (!pathBlocked) {
                    GameObject bulletGO = Instantiate(attackWeapon.bulletPrefab, firePoint.position, firePoint.rotation); // shoot
                    bulletGO.GetComponent<Rigidbody>().AddForce(firePoint.forward * attackWeapon.bulletSpeed, ForceMode.VelocityChange);
                    bulletGO.GetComponent<EnergyBullet>().bulletDamage = attackWeapon.damage;
                    Destroy(bulletGO, attackWeapon.range / attackWeapon.bulletSpeed + 0.05f);
                    countdown = 1 / attackWeapon.fireRateBPS;
                }
            }
        }
    }

    void FindTarget() {
        if (enemyNames == null) {
            return;
        }

        float minDist = Mathf.Infinity;


        List<MobileAttackUnit> potentialTargets = new List<MobileAttackUnit>();
        foreach (string enemyName in enemyNames) {
            if (enemyName.Equals(UnitTable.instance.playerName)) {
                foreach (SelectableAttackUnit playerUnit in UnitTable.instance.playerSelectableAttackUnits) {
                    potentialTargets.Add(playerUnit);
                }
            } else {
                foreach (AIControlledUnit aIControlledUnit in UnitTable.instance.aiUnitTable[enemyName]) {
                    potentialTargets.Add(aIControlledUnit);
                }
            }
        }
        GameObject targetCandidate = null;
        foreach (MobileAttackUnit potentialTarget in potentialTargets) {
            if (Vector3.Distance(transform.position, potentialTarget.transform.position) <= minDist) {
                targetCandidate = potentialTarget.gameObject;
                minDist = Vector3.Distance(transform.position, potentialTarget.transform.position);
            }
        }
        currentTarget = targetCandidate;
    }

    public void SetEnemyNames(List<string> names) {
        currentTarget = null;
        enemyNames = names;
    }
}
