﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Input Data")]
public class InputData : ScriptableObject
{
    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveRight;
    public KeyCode moveLeft;

    public KeyCode interactionLeft;
    public KeyCode interactionRight;

    public KeyCode escape;
    public KeyCode inventory;
    public KeyCode buildScreen;
}
