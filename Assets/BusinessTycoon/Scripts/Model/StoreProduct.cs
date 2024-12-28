﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoreProduct
{
    [HideInInspector]
    public int Id = 0;
    [NonSerialized]
    public int BusinessId = 1000;
    [NonSerialized]
    public int CurrencyId = 10;
    [NonSerialized]
    public string EffectType = "Gold";
    [HideInInspector]
    public bool Unlocked = false;

    public string Name = "";
    public string SKU = "";
    public double Cost = 0;
    public double Effect = 0;
}