using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlotController : MonoBehaviour
{
    [SerializeField] private PreviewBuildingController building;
    [SerializeField] private Text text;

    public void ActivePreview()
    {
        building.gameObject.SetActive(true);
    }
}
