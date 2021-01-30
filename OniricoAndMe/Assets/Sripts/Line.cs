using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Line", order = 1)]
public class Line : ScriptableObject
{
    public enum Character{Sirio, Deneb};
    public string textLine;
    public Character character;
}
