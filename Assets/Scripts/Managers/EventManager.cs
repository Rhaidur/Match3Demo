using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager
{
    #region GameManagerEvents
    public static Action Pending;
    public static Action Start;
    public static Action Win;
    public static Action Fail;
    #endregion
}
