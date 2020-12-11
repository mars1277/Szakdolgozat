using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Offhand : Item
{
    public Offhand(string r, int l) : base(r, l)
    {
    }

    public override string GetItemType()
    {
        return "Offhand";
    }

    public virtual string GetOffhandType()
    {
        return "";
    }

}
