using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProductionBuilding : Warehouse
{
    [SerializeField] private float productionTime = 0;
    [SerializeField] private float productionSpeed = 0;
    [SerializeField] private float burnFuelTime = 0;
    [SerializeField] private ItemType itemProductType = ItemType.Resources;
    [SerializeField] private ItemObject product;
    [SerializeField] private Slider production;
    [SerializeField] private Slider burning;

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
        production.maxValue = productionTime;
        burning.maxValue = burnFuelTime;
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
        Debug.Log(isWorking);
        if (isWorking)
        {
            if (IsBurnFuel(slotFuel))
            {
                currentProductionTime += productionSpeed * Time.deltaTime;
                UpdateSliders();
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
            inventoryController.UpdateInventoryUI();
        }

    }

    private void UpdateSliders()
    {
        production.value = currentProductionTime;
        burning.value = currentBurnTime;
    }

    private void CreateProduct(InventorySlot slotProduct)
    {
        if (slotProduct.item.type==ItemType.Default)
        {
            inventoryController.inventoryObject.AddToTargetSlot(product, 1, 2);
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
            inventoryController.ResetSlot(slotResource.slotID);
        }
        return true;
    }

    private void GetNewFuelUnit(InventorySlot slotFuel)
    {
        currentBurnTime = burnFuelTime;
        slotFuel.amount -= 1;
        if (slotFuel.amount<=0)
        {
            inventoryController.ResetSlot(slotFuel.slotID);
        }
    }
}
