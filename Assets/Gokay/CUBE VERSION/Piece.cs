using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PieceType{
    none=-1,
    ramp=0,
    longboi=1,
    jumpboi=2,
    slideboi=3
}



public class Piece : MonoBehaviour
{
    public PieceType type;
    public int visualIndex;
}
