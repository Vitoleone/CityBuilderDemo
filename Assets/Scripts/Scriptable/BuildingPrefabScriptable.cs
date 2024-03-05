using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabHolder", menuName ="Scriptables/PrefabHolder")]
public class BuildingPrefabScriptable : ScriptableObject
{
    public GameObject hospitalPrefab;
    public GameObject fireStationPrefab;
    public GameObject policeStationPrefab;
    public GameObject schoolPrefab;
    public GameObject marketPrefab;
}
