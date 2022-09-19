using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureCircle : MonoBehaviour
{
    public float timeToCapture = 1f;
    public Renderer rend;
    public LayerMask layersThatCanCapture;

    [Header("Testing")]
    public string ownerName = "";

    private Collider capturer = null;
    private float captureCounter;

    private void Start() {
        captureCounter = timeToCapture;
    }

    private void OnCollisionEnter(Collision collision) {
        if (capturer == null)
            capturer = collision.collider;
    }

    private void OnCollisionStay(Collision collision) {
        if (capturer != collision.collider)
            return;

        if (layersThatCanCapture == (layersThatCanCapture | (1 << collision.gameObject.layer))) {
            captureCounter -= Time.deltaTime;
            if (captureCounter < 0) {
                capturer = null;
                ChangeOwner(collision.collider.gameObject.GetComponent<MobileEntity>().getOwnership());
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        captureCounter = timeToCapture;
        capturer = null;
    }

    public void ChangeOwner(string newOwnerName) {
        ownerName = newOwnerName;
        foreach (StrategyAI ai in UnitTable.instance.strategyAIs) {
            if (ai.name == ownerName) {
                rend.material.color = ai.teamColor;
            }
        }
        if (ownerName == PlayerIntelligence.instance.name) {
            rend.material.color = PlayerIntelligence.instance.teamColor;
        }
    }
}
