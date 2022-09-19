using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelector : MonoBehaviour
{
    [Header("Select Parameters")]
    public string selectableUnitTag = "SelectableUnit";
    public LayerMask entityMask;

    [Header("GUI")]
    public Image boxSelectImage;

    [Header("Internal References")]
    public Camera cam;

    private bool buildingSelect;
    private bool boxSelect;
    private Vector3 startPt;
    private Vector3 endPt;

    [HideInInspector]
    public static UnitSelector instance;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        boxSelectImage.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            startPt = Input.mousePosition;
        }

        if (Input.GetMouseButton(0)) {
            if (Vector3.Distance(startPt, Input.mousePosition) > 50) {
                boxSelect = true;
                BoxSelectGUI(startPt, Input.mousePosition);
            } else {
                boxSelect = false;
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            if (!Input.GetKey(KeyCode.LeftShift))
                Deselect();

            if (boxSelect) {
                buildingSelect = true;
                boxSelectImage.enabled = false;
                endPt = Input.mousePosition;

                foreach (SelectableAttackUnit selectable in UnitTable.instance.playerSelectableAttackUnits) {
                    Vector3 selectablePosition = cam.WorldToScreenPoint(selectable.transform.position);
                    if (selectablePosition.x <= Mathf.Max(startPt.x, endPt.x) && selectablePosition.x >= Mathf.Min(startPt.x, endPt.x) 
                            && selectablePosition.y <= Mathf.Max(startPt.y, endPt.y) && selectablePosition.y >= Mathf.Min(startPt.y, endPt.y)) {
                        Select(selectable);
                        buildingSelect = false;
                    }
                }

                if (!buildingSelect)
                    return;

                Deselect();
                foreach (Building selectableBuilding in UnitTable.instance.playerBuildings) {
                    Vector3 selectablePosition = cam.WorldToScreenPoint(selectableBuilding.transform.position);
                    if (selectablePosition.x <= Mathf.Max(startPt.x, endPt.x) && selectablePosition.x >= Mathf.Min(startPt.x, endPt.x)
                            && selectablePosition.y <= Mathf.Max(startPt.y, endPt.y) && selectablePosition.y >= Mathf.Min(startPt.y, endPt.y)) {
                        Select(selectableBuilding);
                        return;
                    }
                }
            } else {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f, entityMask)) {
                    if (hit.collider.gameObject.GetComponent<SelectableAttackUnit>() != null) {
                        Select(hit.collider.gameObject.GetComponent<SelectableAttackUnit>());
                    } else if (hit.collider.gameObject.GetComponent<Building>() != null && hit.collider.gameObject.GetComponent<Building>().isSelectable) {
                        Select(hit.collider.gameObject.GetComponent<Building>());
                    }
                }
            }
        }
    }

    public void BoxSelectGUI(Vector3 startPosition, Vector3 endPosition) {
        if (!boxSelectImage.enabled)
            boxSelectImage.enabled = true;
        boxSelectImage.rectTransform.position = startPosition / 2 + endPosition / 2;
        boxSelectImage.rectTransform.sizeDelta = new Vector2(Mathf.Abs(endPosition.x - startPosition.x), Mathf.Abs(endPosition.y - startPosition.y));
    }

    public void Select(SelectableAttackUnit selectable) {
        UnitTable.instance.selectedAttackUnits.Add(selectable);
        selectable.BecomeSelected();
    }

    public void Select(Building selectableBuilding) {
        Deselect();
        UnitTable.instance.selectedBuilding = selectableBuilding;
        Debug.Log(selectableBuilding.name + " was selected at " + selectableBuilding.transform.position);
    }

    public void Deselect() {
        foreach (SelectableAttackUnit selectable in UnitTable.instance.selectedAttackUnits) {
            selectable.BecomeDeselected();
        }
        UnitTable.instance.selectedBuilding = null;
        UnitTable.instance.selectedAttackUnits.Clear();
    }
}
