using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSlotController : MonoBehaviour
{
    [SerializeField] private PreviewBuildingController building;

    public void ActivePreview()
    {
        building.gameObject.SetActive(true);
    }
}
