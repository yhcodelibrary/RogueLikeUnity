using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class MusicTypeComparer : IEqualityComparer<MusicInformation.MusicType>
{
    public bool Equals(MusicInformation.MusicType x, MusicInformation.MusicType y)
    {
        return x == y;
    }

    public int GetHashCode(MusicInformation.MusicType obj)
    {
        return (int)obj;
    }
}


public class SoundTypeComparer : IEqualityComparer<SoundInformation.SoundType>
{
    public bool Equals(SoundInformation.SoundType x, SoundInformation.SoundType y)
    {
        return x == y;
    }

    public int GetHashCode(SoundInformation.SoundType obj)
    {
        return (int)obj;
    }
}
public class VoiceTypeComparer : IEqualityComparer<VoiceInformation.VoiceType>
{
    public bool Equals(VoiceInformation.VoiceType x, VoiceInformation.VoiceType y)
    {
        return x == y;
    }

    public int GetHashCode(VoiceInformation.VoiceType obj)
    {
        return (int)obj;
    }
}
public class ItemTypeComparer : IEqualityComparer<ItemType>
{
    public bool Equals(ItemType x, ItemType y)
    {
        return x == y;
    }

    public int GetHashCode(ItemType obj)
    {
        return (int)obj;
    }
}
public class PlayerTypeComparer : IEqualityComparer<PlayerType>
{
    public bool Equals(PlayerType x, PlayerType y)
    {
        return x == y;
    }

    public int GetHashCode(PlayerType obj)
    {
        return (int)obj;
    }
}
public class StateAbnormalComparer : IEqualityComparer<StateAbnormal>
{
    public bool Equals(StateAbnormal x, StateAbnormal y)
    {
        return x == y;
    }

    public int GetHashCode(StateAbnormal obj)
    {
        return (int)obj;
    }
}

public class CharacterDirectionComparer : IEqualityComparer<CharacterDirection>
{
    public bool Equals(CharacterDirection x, CharacterDirection y)
    {
        return x == y;
    }

    public int GetHashCode(CharacterDirection obj)
    {
        return (int)obj;
    }
}

public class AnimationTypeComparer : IEqualityComparer<AnimationType>
{
    public bool Equals(AnimationType x, AnimationType y)
    {
        return x == y;
    }

    public int GetHashCode(AnimationType obj)
    {
        return (int)obj;
    }
}


public class KeyTypeComparer : IEqualityComparer<KeyType>
{
    public bool Equals(KeyType x, KeyType y)
    {
        return x == y;
    }

    public int GetHashCode(KeyType obj)
    {
        return (int)obj;
    }
}

public class MenuItemActionTypeComparer : IEqualityComparer<MenuItemActionType>
{
    public bool Equals(MenuItemActionType x, MenuItemActionType y)
    {
        return x == y;
    }

    public int GetHashCode(MenuItemActionType obj)
    {
        return (int)obj;
    }
}

public class FirstMenuTypeComparer : IEqualityComparer<FirstMenuType>
{
    public bool Equals(FirstMenuType x, FirstMenuType y)
    {
        return x == y;
    }

    public int GetHashCode(FirstMenuType obj)
    {
        return (int)obj;
    }
}


public class BaseItemComparer : IEqualityComparer<BaseItem>
{
    public bool Equals(BaseItem x, BaseItem y)
    {
        if (CommonFunction.IsNull(x) == true)
        {
            return false;
        }
        if (CommonFunction.IsNull(y) == true)
        {
            return false;
        }
        return x.Name == y.Name;
    }

    public int GetHashCode(BaseItem obj)
    {
        return obj.Name.GetHashCode();
    }
}
public class BaseOptionComparer : IEqualityComparer<BaseOption>
{
    public bool Equals(BaseOption x, BaseOption y)
    {
        if (CommonFunction.IsNull(x) == true)
        {
            return false;
        }
        if (CommonFunction.IsNull(y) == true)
        {
            return false;
        }
        return x.Name == y.Name;
    }

    public int GetHashCode(BaseOption obj)
    {
        return obj.Name.GetHashCode();
    }
}
public class ObjectTypeComparer : IEqualityComparer<ObjectType>
{
    public bool Equals(ObjectType x, ObjectType y)
    {
        return x == y;
    }

    public int GetHashCode(ObjectType obj)
    {
        return (int)obj;
    }
}

public class MapPointComparer : IEqualityComparer<MapPoint>
{
    public bool Equals(MapPoint x, MapPoint y)
    {
        return x == y;
    }

    public int GetHashCode(MapPoint obj)
    {
        return obj.GetHashCode();
    }
}

public class GuidComparer : IEqualityComparer<Guid>
{
    public bool Equals(Guid x, Guid y)
    {
        return x == y;
    }

    public int GetHashCode(Guid obj)
    {
        return obj.GetHashCode();
    }
}