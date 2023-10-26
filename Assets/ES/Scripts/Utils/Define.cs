using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        None,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum Layer
    {
        None = 6,
        Enemy = 7,
        Block = 8,

    }

    public enum CameraMode
    {
        None,
        QuarterView,
    }
}
