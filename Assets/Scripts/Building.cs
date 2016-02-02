using UnityEngine;
using System.Collections;
using System;

public class Building : MonoBehaviour, IPriceable
{
    public int price
    {
        get
        {
            return 100;
        }
    }
}
