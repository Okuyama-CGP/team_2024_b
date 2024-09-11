using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPitem : BaseItem
{
    [SerializeField] float expAmount = 1;

    protected override void OnPickedUp()
    {
        playerCore.AddEXP(expAmount);
    }
}
