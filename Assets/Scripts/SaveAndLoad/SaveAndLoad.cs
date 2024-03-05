using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class SaveAndLoad : MonoBehaviour
{
    BuildingData data;
    [SerializeField]BuildingData loadedData;
    [SerializeField] BuildingPrefabScriptable buildingPrefabs;
    string savePath;
    string saveValue;
    bool isloaded = false;
    [Serializable]
    public class BuildingData
    {  
        public List<Vector3> positions = new List<Vector3>();
        public List<BuildingType> types = new List<BuildingType>();
    }
    private void Start()
    {
        savePath = Application.persistentDataPath + "/Save.json";
        data = new BuildingData();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            GetAllLocationsAndTypes();
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
            InstantiateLoadedBuildings(loadedData.types, loadedData.positions);
        }
    }
    private void InstantiateLoadedBuildings(List<BuildingType> buildingTypes,List<Vector3> buildingPotisitons)
    {
        for(int i = 0; i < buildingTypes.Count; i++)
        {
            switch (buildingTypes[i])
            {
                case BuildingType.Hospital:
                    Instantiate(buildingPrefabs.hospitalPrefab, buildingPotisitons[i], Quaternion.identity);
                    break;
                case BuildingType.Market:
                    Instantiate(buildingPrefabs.marketPrefab, buildingPotisitons[i], Quaternion.identity);
                    break;
                case BuildingType.FireStation:
                    Instantiate(buildingPrefabs.fireStationPrefab, buildingPotisitons[i], Quaternion.identity);
                    break;
                case BuildingType.PoliceStation:
                    Instantiate(buildingPrefabs.policeStationPrefab, buildingPotisitons[i], Quaternion.identity);
                    break;
                case BuildingType.School:
                    Instantiate(buildingPrefabs.schoolPrefab, buildingPotisitons[i], Quaternion.identity);
                    break;
            }
        }
    }
    void GetAllLocationsAndTypes()
    {
        int i = 0;
        foreach (var building in SelectManager.instance.allBuildings)
        {
            data.positions.Add(building.transform.position);
            data.types.Add(building.type);
            i++;
        }
    }
    void SaveData()
    {
        if (data.positions.Count <= 0 || data.types.Count <= 0)
            return;
        saveValue = JsonUtility.ToJson(data,true);
        File.WriteAllText(savePath, saveValue);
    }
    void LoadData()
    {
        if (!File.Exists(savePath)) //if there is no file in save path return.
            return;
        string jsonData = File.ReadAllText(savePath);
        loadedData = JsonUtility.FromJson<BuildingData>(jsonData);
        isloaded = true;
    }

}
