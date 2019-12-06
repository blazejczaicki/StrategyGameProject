using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider _production;
    [SerializeField] private Slider _burning;

    public Slider production { get => _production; set => _production = value; }
    public Slider burning { get => _burning; set => _burning = value; }

    public void UpdateSliders(float currentProductionTime, float currentBurnTime)
    {
        production.value = currentProductionTime;
        burning.value = currentBurnTime;
    }
}
