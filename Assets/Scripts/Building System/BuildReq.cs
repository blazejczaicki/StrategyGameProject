using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Requirement")]
public class BuildReq : ScriptableObject
{
    public List<Requirement> requirements;

    public bool IsRequirementsFulFilled(InventoryObject inventory)
    {
        List<bool> requirementsFulFilled = new List<bool>(new bool[requirements.Count]);
        int i = 0;
        foreach (var requirement in requirements)
        {
            if (requirement.amount <= inventory.GetItemAmount(requirement.itemType))
            {
                requirementsFulFilled[i] = true;
            }
            i++;
        }
        return requirementsFulFilled.Exists(x => x == false) ? false : true;
    }
}

[System.Serializable]
public class Requirement
{
    public ItemType itemType = ItemType.Wood;
    public int amount = 125;
}