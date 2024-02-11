using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour,IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] GameObject buildingObject;
    static Stack<GameObject> buildings = new Stack<GameObject>();
    static Stack<GameObject> undoBuildings = new Stack<GameObject>();
    GameObject buildedObject;
    bool canMove = false;

    public void Build()
    {
        buildedObject = Instantiate(buildingObject);
        buildings.Push(buildedObject);
    }
    public void Undo()
    {
        GameObject undoBuilding = buildings.Pop();
        undoBuildings.Push(undoBuilding);
        undoBuilding.SetActive(false);
    }
    public void Redo()
    {
        GameObject undoBuilding = undoBuildings.Pop();
        buildings.Push(undoBuilding);
        undoBuilding.SetActive(true);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        BuildBuilding();
        if(buildedObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                buildedObject.transform.position = new Vector3(hitInfo.point.x, buildedObject.transform.localScale.y/2, hitInfo.point.z);
            }
        }
        

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (buildedObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                buildedObject.transform.position = new Vector3(hitInfo.point.x, buildedObject.transform.localScale.y / 2, hitInfo.point.z);
            }
            
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if (buildedObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo) )
            {
                buildedObject.transform.position = new Vector3(hitInfo.point.x, buildedObject.transform.localScale.y / 2, hitInfo.point.z);
            }
        }
    }

    public void BuildBuilding()
    {
        CommandScheduler.RunBuildingCommand(this);
    }

}
