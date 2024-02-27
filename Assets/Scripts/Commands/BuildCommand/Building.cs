using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    //iki obje seçip birini kýrmýzý yapýp terraine bastýðýmda functional buttonslar yok oluyor.
    [SerializeField]public GameObject selectedCircle;
    [SerializeField]public Image placementCircle;
    public bool isOnBuilding = false;
    public bool isBuilded;
    public bool canBuild;
    bool boxHit;
    RaycastHit hitInfo;
    int layerNumber = 6;
    int layerMask;
    Renderer size;
    
    private void Start()
    {
        size = GetComponent<Renderer>();
        layerMask = 1 << layerNumber;
    }
    private void FixedUpdate()
    {
        if (!isBuilded)
        {
            CheckCanBuild();
        }
    }
    public void CheckCanBuild()
    {
        if(selectedCircle.active)
        {
            placementCircle.gameObject.SetActive(true);
            selectedCircle.gameObject.SetActive(false);
        }
        boxHit = Physics.BoxCast(size.bounds.center+ Vector3.up * size.bounds.size.y*4, size.bounds.size*0.90f/2, -transform.up, out hitInfo,transform.localRotation,size.bounds.size.y*4,layerMask);
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
    private void OnDestroy()
    {
        if (SelectManager.instance.selectedUnits.Contains(this))
        {
            SelectManager.instance.DeSelectUnit(this);
            selectedCircle.SetActive(false);
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
    private void OnDisable()
    {
        
        if (SelectManager.instance.selectedUnits.Contains(this))
        {
            SelectManager.instance.DeSelectUnit(this);
            selectedCircle.SetActive(false);
        }
    }
}
