using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Build : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject buildingObject;
    [SerializeField] Parent parent;
    static Stack<GameObject> buildings = new Stack<GameObject>();
    static Stack<GameObject> undoBuildings = new Stack<GameObject>();
    GameObject buildedObject;
    public bool buildFinished = false;
    int layerNumber = 6;
    int layerMask;
    private void Start()
    {
        layerMask = 1 << layerNumber;
        
    }
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
        if(buildedObject == null)
        {
            buildedObject = Instantiate(buildingObject);
        }
        EventManager.instance.OnBuildEnded += BuildEnd;
        Building building = buildedObject.GetComponent<Building>();
        parent.state = Parent.ParentState.Building;
        while (!buildFinished)
        {
            yield return new WaitForSeconds(0.01f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo,Mathf.Infinity,layerMask))
            {
                buildedObject.transform.position = new Vector3(hitInfo.point.x, transform.localScale.y / 2, hitInfo.point.z);
                if (Input.GetMouseButtonDown(0) && building.canBuild)
                {
                    buildFinished = true;
                    
                    building.placementCircle.gameObject.SetActive(false);
                    CommandScheduler.RunBuildingCommand(this);
                    building.isBuilded = true;
                    SelectManager.instance.DeSelectUnit(building);
                    parent.state = Parent.ParentState.Free;
                    EventManager.instance.OnBuildEnded -= BuildEnd;
                    buildedObject = null;
                    break;
                }
                else if(Input.GetMouseButtonDown(0) && !building.canBuild)
                {
                    UIManager.instance.SetFunctionalButtonsActivness(false);
                    UIManager.instance.ControlPlacementAlertActiveness(true);
                    buildFinished = true;
                }
                
            }
            
        }
    }
    void BuildEnd()
    {
        buildFinished = false;
        EventManager.instance.OnBuildEnded -= BuildEnd;
        StartCoroutine(Building());
    }
    
}
