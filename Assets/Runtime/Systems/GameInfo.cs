using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInfo {

    public const int
        GroundLayer = 6;

    public const string
        GroundLayerName = "Ground";

    public static readonly LayerMask
        GroundMask = LayerMask.GetMask(GroundLayerName);
}
