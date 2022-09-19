using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float amount) {
        TakeDamage(amount, null);
    }

    public void TakeDamage(float amount, GameObject deathEffect) {
        health -= amount;
        if (health <= 0) {
            if (deathEffect != null)
                Destroy(Instantiate(deathEffect, transform.position, transform.rotation), 3f);
            Destroy(gameObject);
        }
    }

    public override bool Equals(object other) {
        if (other == null) {
            return false;
        }

        Entity entity = other as Entity;
        if (entity == null) {
            return false;
        }

        return Equals(entity);
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }

    public bool Equals(Entity other) {
        return gameObject.Equals(other.gameObject);
    }
}
