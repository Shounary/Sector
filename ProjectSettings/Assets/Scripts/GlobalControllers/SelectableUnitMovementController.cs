using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectableUnitMovementController : MonoBehaviour
{
    [Header("Internal Reference")]
    public Camera cam;

    void Update()
    {
        if (Input.GetMouseButtonUp(1) && UnitTable.instance.selectedAttackUnits.Count != 0) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                foreach (SelectableAttackUnit selectable in UnitTable.instance.selectedAttackUnits) {
                    selectable.moveAgent.SetDestination(hit.point);
                    if (UnitTable.instance.selectedAttackUnits.Count > 1) {
                        selectable.moveAgent.stoppingDistance = 1f;
                    } else if (UnitTable.instance.selectedAttackUnits.Count > 5) {
                        selectable.moveAgent.stoppingDistance = 2f;
                    }
                }
            }
        }
    }
}
