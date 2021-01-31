using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Line", order = 1)]
public class Line : ScriptableObject
{
    public enum Character {Sirio, Deneb, StrangeCreature};
    public enum SirioAnims {None, Smile, SmileHand, Hand, Nod};
    public enum SirioSounds {None, Ah, Umm, Oh, Hehe};
    public enum DenebAnims {None, InOut, Happy, Sad, Surprised, DemonsGO};
    public enum DenebSounds {None, StepsIn, StepsOut};
    public string textLine;
    public Character character;
    public SirioAnims sirio_anim;
    public SirioSounds sirio_sound;
    public DenebAnims deneb_anim;
    public DenebSounds deben_sound;
}
