using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DraggableLimb : MonoBehaviour
{
    public enum Limb
    {
        RightHand,
        LeftHand,
        Body,
        RightFoot,
        LeftFoot
    }

    public Limb limb;
}
