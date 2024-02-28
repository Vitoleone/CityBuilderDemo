using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildCommander
    : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject buildingObject;
    static Stack<GameObject> buildings = new Stack<GameObject>();
    GameObject buildedObject;
    public bool buildFinished = false;
   
    private void OnDestroy()
    {
        EventManager.instance.OnBuildEnded -= BuildEnd;
    }

    public void BuildExecute()
    {
        
        buildings.Push(buildedObject);
    }
    public void Undo()
    {
        GameObject undoBuilding = buildings.Pop();
        undoBuilding.SetActive(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        buildFinished = false;
        StartCoroutine(Building());
    }

    IEnumerator Building()
    {
        Building building = StartBuilding();
        while (!buildFinished)
        {
            
            yield return new WaitForSeconds(0.005f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (!EventSystem.current.IsPointerOverGameObject() && Parent.instance.state != Parent.ParentState.Error)
            {
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground")))
                {
                    buildedObject.transform.position = new Vector3(hitInfo.point.x, 0.05f, hitInfo.point.z);
                    if (Input.GetMouseButtonDown(0) && building.canBuild)
                    {
                        DoBuild(building);
                        break;
                    }
                    else if (Input.GetMouseButtonDown(0) && !building.canBuild)
                    {
                        CantBuild();
                    }
                }
            }
             
        }
    }

    private Building StartBuilding()
    {
        if(buildedObject == null)
        {
            buildedObject = Instantiate(buildingObject);
        }
        
        EventManager.instance.OnBuildEnded += BuildEnd;
        Building building = buildedObject.GetComponent<Building>();
        Parent.instance.state = Parent.ParentState.Building;
        return building;
    }

    private void CantBuild()
    {
        UIManager.instance.SetFunctionalButtonsActivness(false);
        UIManager.instance.buildings.SetActive(false);
        UIManager.instance.ControlPlacementAlertActiveness(true);
        buildFinished = true;
        StopCoroutine(Building());
    }

    private void DoBuild(Building building)
    {
        buildedObject = null;
        buildFinished = true;
        building.placementCircle.gameObject.SetActive(false);
        CommandScheduler.RunBuildingCommand(this);
        building.isBuilded = true;
        SelectManager.instance.DeSelectUnit(building);
        Parent.instance.state = Parent.ParentState.Free;
        EventManager.instance.OnBuildEnded -= BuildEnd;
    }

    void BuildEnd()
    {
        buildFinished = false;
        UIManager.instance.buildings.SetActive(true);
        EventManager.instance.OnBuildEnded -= BuildEnd;
        StartCoroutine(Building());
    }
    
}
