using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProductionBuilding : Warehouse
{
    [SerializeField] private float productionTime = 0;
    [SerializeField] private float productionSpeed = 0;
    [SerializeField] private float burnFuelTime = 0;
    [SerializeField] private ItemType itemProductType = ItemType.Resources;
    [SerializeField] private ItemObject product;
    private SliderController sliderController;

    private float currentProductionTime = 0;
    private float currentBurnTime = 0;

    private InventorySlot slotResource;
    private InventorySlot slotFuel;
    private InventorySlot slotProduct;

    private bool isWorking = false;
    private bool isReadyToProduce = false;

   // public Action 

    protected override void Awake()
    {
        base.Awake();
        sliderController = inventoryBuildingController.GetComponent<SliderController>();
        sliderController.production.maxValue = productionTime;
        sliderController.burning.maxValue = burnFuelTime;
    }

    private void Update()
    {
       Produce();
    }

    public override void OnLeftClickObject(PlayerController controller)
    {
        base.OnLeftClickObject(controller);
        isReadyToProduce = true;
    }

    public void Produce()
    {
        if (isReadyToProduce)
        {
        slotResource = inventoryObject.GetInventorySlot(0);
        slotFuel = inventoryObject.GetInventorySlot(1);
        slotProduct = inventoryObject.GetInventorySlot(2);
        MakeProduct(slotFuel, slotResource, slotProduct);
        }
    }

    private void MakeProduct(InventorySlot slotFuel, InventorySlot slotResource, InventorySlot slotProduct)
    {
        if (isWorking)
        {
            if (IsBurnFuel(slotFuel))
            {
                currentProductionTime += productionSpeed * Time.deltaTime;
                sliderController.UpdateSliders(currentProductionTime, currentBurnTime);
            }
        }
        else
        {
            if(slotFuel.item.type != ItemType.Default && GetNewResourceUnit(slotResource))
            {
                isWorking = true;
            }
        }

        if (productionTime < currentProductionTime)
        {
            isWorking = false;
            currentProductionTime = 0;
            CreateProduct(slotProduct);
            inventoryBuildingController.UpdateInventoryUI();////
        }
    }

    private void CreateProduct(InventorySlot slotProduct)
    {
        if (slotProduct.item.type==ItemType.Default)
        {
            inventoryObject.AddToTargetSlot(product, 1, 2);
        }
        else if (slotProduct.item.type == itemProductType)
        {
            slotProduct.amount += 1;
        }
    }

    private bool IsBurnFuel(InventorySlot slotFuel)
    {
        currentBurnTime -= slotFuel.item.burningSpeed * Time.deltaTime;
        if (currentBurnTime <= 0)
        {
            if (slotFuel.amount > 0)
            {
                GetNewFuelUnit(slotFuel);
            }
            else
            {
                return false;
            }
        }
        return true;        
    }

    private bool GetNewResourceUnit(InventorySlot slotResource)
    {
        if (slotResource.item.type == ItemType.Default)
            return false;
        slotResource.amount -= 1;
        if (slotResource.amount<=0)
        {            
            inventoryBuildingController.ResetSlot(slotResource.slotID);
        }
        return true;
    }

    private void GetNewFuelUnit(InventorySlot slotFuel)
    {
        currentBurnTime = burnFuelTime;
        slotFuel.amount -= 1;
        if (slotFuel.amount<=0)
        {
            inventoryBuildingController.ResetSlot(slotFuel.slotID);
        }
    }
}
