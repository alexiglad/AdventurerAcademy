using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastData
{
    RaycastHit hit;
    bool hitBool;
    public RaycastData(bool rayBool, RaycastHit rayHit)
    {
        hit = rayHit;
        hitBool = rayBool;
    }

    public RaycastHit Hit { get => hit; }
    public bool HitBool { get => hitBool; }
}
