using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketHoarder : Achievement
{
    public override Image Icon
    {
        get => AchievementGUI.instance.TicketHoarder;
    }
}