using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField]public Image placementCircle;
    [SerializeField]public BuildingType type;
    public bool isOnBuilding = false;
    public bool isBuilded;
    public bool canBuild;
    bool boxHit;
    RaycastHit hitInfo;
    Renderer size;
    private void Start()
    {
        size = GetComponent<Renderer>();
        SelectManager.instance.allBuildings.Add(this);
    }
    private void FixedUpdate()
    {
        if (!isBuilded)
        {
            CheckCanBuild();
        }
    }
    public void OnBuildingSelect()
    {
        placementCircle.gameObject.SetActive(true);
        UIManager.instance.SetFunctionalButtonsActivness(true);
        UIManager.instance.buildings.SetActive(false);
    }
    public void OnBuildingDeselect()
    {
        placementCircle.gameObject.SetActive(false);
        if(SelectManager.instance.selectedUnits.Count <= 0)
        {
            placementCircle.gameObject.SetActive(false);
            UIManager.instance.SetFunctionalButtonsActivness(false);
            UIManager.instance.buildings.SetActive(true);
        }
        
    }
    public void CheckCanBuild()
    {
        if(placementCircle.gameObject.activeInHierarchy)
        {
            placementCircle.gameObject.SetActive(true);
        }
        boxHit = Physics.BoxCast(size.bounds.center+ Vector3.up * size.bounds.size.y*4, size.bounds.size*0.90f/2, -transform.up, out hitInfo,transform.localRotation,size.bounds.size.y*4,LayerMask.GetMask("Ground"));
        if (!boxHit && isOnBuilding == false)
        {
            canBuild = true;
            placementCircle.color = Color.green + new Color(0, 0, 0, -.5f); ;
        }
        else
        {
            canBuild = false;
            placementCircle.color = Color.red + new Color(0,0,0,-.5f);
        }
    }
  
    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.TryGetComponent<Building>(out Building building))
        {
            if(!isOnBuilding)
            {
                isOnBuilding = true;
                building.isOnBuilding = true;
                CheckCanBuild();
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.TryGetComponent<Building>(out Building building))
        {
            isOnBuilding = false;
            building.isOnBuilding = false;
            CheckCanBuild();

        }
    }
    private void OnDestroy()
    {
        if (SelectManager.instance.selectedUnits.Contains(this))
        {
            SelectManager.instance.DeselectBuilding(this);
            placementCircle.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {

        if (SelectManager.instance.selectedUnits.Contains(this))
        {
            SelectManager.instance.DeselectBuilding(this);
            placementCircle.gameObject.SetActive(false);
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.matrix = transform.localToWorldMatrix;

    //    //Check if there has been a hit yet
    //    if (boxHit)
    //    {//center + rotation * hit.distance
    //        //Draw a Ray forward from GameObject toward the hit
    //        Gizmos.DrawRay(size.bounds.center + Vector3.up * 2f, -transform.up * hitInfo.distance);
    //        //Draw a cube that extends to where the hit exists
    //        Gizmos.DrawWireCube((size.bounds.center + Vector3.up * size.bounds.size.y*4) + Vector3.down * hitInfo.distance, size.bounds.size);
    //    }
    //    //If there hasn't been a hit yet, draw the ray at the maximum distance
    //    else
    //    {//center + rotation * maxDistance
    //        //Draw a Ray forward from GameObject toward the maximum distance
    //        Gizmos.DrawRay(size.bounds.center + Vector3.up * 2f, -transform.up * (size.bounds.size.y));
    //        //Draw a cube at the maximum distance
    //        Gizmos.DrawWireCube((size.bounds.center + Vector3.up * size.bounds.size.y*4) + (Vector3.down * size.bounds.size.y), size.bounds.size);
    //    }
    //}

}
