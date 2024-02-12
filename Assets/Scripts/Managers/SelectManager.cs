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
        
    }
    public void DeselectAllUnits()
    {
        foreach(GameObject unit in selectedUnits)
        {
            unit.GetComponent<Building>().selectedCircle.SetActive(false); 
        }
        selectedUnits.Clear();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!EventSystem.current.IsPointerOverGameObject())//Casts ray if mouse is not on a UI element
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
                        UIManager.instance.checkButtonsActiveness?.Invoke();
                    }
                }
            }
        }
    }
}
