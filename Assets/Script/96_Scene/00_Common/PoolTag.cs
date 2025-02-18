using System.Collections.Generic;

public static class PoolTagException
{
    // 주의: 만약에 PoolTag에 Stage가 넘어가도 유지되어야 하는 것들이 추가가 된다면, ObjectPool의 Reset 부분에서 해당 PoolTag를 필터링하는 과정이 필요한다.
    public static List<PoolTag> WhenSceneChangeReset = new List<PoolTag>
    {
    };
}


public enum PoolTag
{
    // MiniMap Icon
    EnemyMiniMapIcon,
    MiniMapIconIppali,
    MiniMapIconIbkkugi,
    MiniMapIconKoppulso,
    MiniMapIconGiljjugi,
    MiniMapIconDulduli,
    MiniMapIconItmomi,
    // Monster Attack Object
    IbkkugiThrow,
    ItmomiThrow,
    Tentacle,
    TentacleVertical,
    TentacleHorizontal,
    GergusThrow,
    IppaliEgg,
    // Game Item
    Soul,
    // Player SKill
    Ghost,
    Mark,
}