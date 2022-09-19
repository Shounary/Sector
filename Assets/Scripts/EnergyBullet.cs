using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBullet : MonoBehaviour
{
    public LayerMask layersToIgnore;
    public LayerMask layersToTakeDamage;
    public float bulletDamage = 35f;
    private void OnCollisionEnter(Collision collision) {
        if (layersToIgnore == (layersToIgnore | (1 << collision.gameObject.layer)))
            return;
        Destroy(gameObject);
        if (layersToTakeDamage == (layersToTakeDamage | (1 << collision.gameObject.layer))) {
            collision.collider.gameObject.GetComponent<Entity>().TakeDamage(bulletDamage);
        }
    }
}
