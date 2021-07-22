using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinballWizard : Achievement
{
    public override Image Icon
    {
        get => AchievementGUI.instance.PinballWizard;
    }
}