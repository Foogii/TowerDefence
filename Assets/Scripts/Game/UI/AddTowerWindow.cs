using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AddTowerWindow : MonoBehaviour
{

    public GameObject towerSlotToAddTowerTo;

    public void AddTower(string towerTypeAsString)
    {
        TowerType type = (TowerType)Enum.Parse(typeof(TowerType), towerTypeAsString, true);
        Debug.Log("Tower Spawned???");
        if (TowerManager.Instance.GetTowerPrice(type) <= GameManager.Instance.gold)
        {
            Debug.Log("Tower Should have Spawned");
            GameManager.Instance.gold -= TowerManager.Instance.GetTowerPrice(type); 
            TowerManager.Instance.CreateNewTower(towerSlotToAddTowerTo, type);
            gameObject.SetActive(false);
        }
    }
}
