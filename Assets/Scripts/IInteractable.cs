using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnLeftClickObject(PlayerController controller);
    void OnRightClickObject(PlayerController controller);
    void OnCoursor();
    void OnExitCoursor();
}
