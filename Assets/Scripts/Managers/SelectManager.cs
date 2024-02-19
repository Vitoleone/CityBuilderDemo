using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectManager : Singleton<SelectManager>
{
    public List<GameObject> selectedUnits;
    public List<GameObject> allUnits;
    public delegate void OnSelectUnit(bool activeness);
    public OnSelectUnit onSelectUnit;
    public float xOffset = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())//Casts ray if mouse is not on a UI element
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.collider.gameObject.GetComponent<Terrain>())
                    {
                        DeselectAllUnits();
                        CommandScheduler.ResetStacks();
                        UIManager.instance.SetFunctionalButtonsActivness(false);

                    }
                }
            }
        }
    }
    public void SelectUnit(GameObject unit)
    {
        if (unit != null)
        {
            selectedUnits.Add(unit);
        }
        if(!UIManager.instance.functionalPanel.activeSelf)
        {
            onSelectUnit?.Invoke(true);
        }
        CommandScheduler.ResetStacks();
    }

    public void DeSelectUnit(GameObject unit)
    {
        if(unit != null)
        {
            selectedUnits.Remove(unit);
        }
        if(selectedUnits.Count <= 0)
        {
            onSelectUnit?.Invoke(false);
        }
        CommandScheduler.ResetStacks();
        ClearChilds();
    }
    public void DeselectAllUnits()
    {
        foreach(GameObject unit in selectedUnits)
        {
            unit.GetComponent<Building>().selectedCircle.SetActive(false); 
        }
        SelectManager.instance.ClearChilds();
        selectedUnits.Clear();
        UIManager.instance.checkButtonsActiveness?.Invoke();
        xOffset = -1;
    }

    public Vector3 GetMidpoint()
    {
        Vector3 midpoint = Vector2.zero;

        foreach(GameObject unit in selectedUnits)
        {
            if(midpoint != Vector3.zero)
            {
                midpoint = Vector3.Lerp(unit.transform.position, midpoint, 0.5f);
            }
            else
            {
                midpoint = unit.transform.position;
            }
        }
        return midpoint;
    }
    public void ChildSelected(GameObject parent)
    {
        foreach(GameObject child in selectedUnits)
        {
            child.transform.SetParent(parent.transform);
        }
    }
    public void ClearChilds()
    {
        foreach(GameObject child in selectedUnits)
        {
            child.transform.SetParent(null);
        }
    }


}
