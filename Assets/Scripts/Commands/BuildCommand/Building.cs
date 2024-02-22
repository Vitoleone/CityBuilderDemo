using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField]public GameObject selectedCircle;
    [SerializeField]public Image placementCircle;
    public bool isBuilded;
    public bool canBuild;
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
        
        RaycastHit hitInfo;
        placementCircle.gameObject.SetActive(true);
        selectedCircle.gameObject.SetActive(false);
        bool boxHit = Physics.BoxCast(size.bounds.center+ Vector3.up * size.bounds.size.y*4, size.bounds.size/2, Vector3.down, out hitInfo,transform.rotation,size.bounds.size.y*4,layerMask);
        if (!boxHit)
        {
            canBuild = true;
            placementCircle.color = Color.green;
        }
        else
        {
            canBuild = false;
            placementCircle.color = Color.red;
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
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    //Check if there has been a hit yet
    //    if (boxHit)
    //    {//center + rotation * hit.distance
    //        //Draw a Ray forward from GameObject toward the hit
    //        Gizmos.DrawRay(size.bounds.center + Vector3.up * 2f, -transform.up * hitInfo.distance);
    //        //Draw a cube that extends to where the hit exists
    //        Gizmos.DrawWireCube((size.bounds.center + Vector3.up * size.bounds.size.y) + Vector3.down * hitInfo.distance, size.bounds.size);
    //    }
    //    //If there hasn't been a hit yet, draw the ray at the maximum distance
    //    else
    //    {//center + rotation * maxDistance
    //        //Draw a Ray forward from GameObject toward the maximum distance
    //        Gizmos.DrawRay(size.bounds.center + Vector3.up * 2f, -transform.up * (size.bounds.size.y));
    //        //Draw a cube at the maximum distance
    //        Gizmos.DrawWireCube((size.bounds.center + Vector3.up * size.bounds.size.y) + (Vector3.down * size.bounds.size.y), size.bounds.size);
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
