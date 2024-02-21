using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Build : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject buildingObject;
    static Stack<GameObject> buildings = new Stack<GameObject>();
    static Stack<GameObject> undoBuildings = new Stack<GameObject>();
    GameObject buildedObject;
    bool buildFinished = false;
    public void BuildExecute()
    {
        
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
        buildFinished = false;
        StartCoroutine(Building());
    }

    IEnumerator Building()
    {
        buildedObject = Instantiate(buildingObject);
        Building building = buildedObject.GetComponent<Building>();
        while (!buildFinished)
        {
            yield return new WaitForSeconds(0.01f);
            building.CheckCanBuild();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                buildedObject.transform.position = new Vector3(hitInfo.point.x, transform.localScale.y / 2, hitInfo.point.z);
                if (Input.GetMouseButtonDown(0) && building.canBuild)
                {
                    buildFinished = true;
                    
                    building.placementCircle.gameObject.SetActive(false);
                    CommandScheduler.RunBuildingCommand(this);
                    building.isBuilded = true;
                    SelectManager.instance.DeSelectUnit(building);
                    break;
                }
            }
            
        }
    }
    
}
