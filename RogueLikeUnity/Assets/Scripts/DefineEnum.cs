using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum CharacterDirection
{
    Match = 1,
    Top,
    TopLeft,
    Left,
    BottomLeft,
    Bottom,
    BottomRight,
    Right,
    TopRight
}

public enum AreaDirection
{
    Match = 1,
    Top,
    Left,
    Bottom,
    Right
}
public enum Slash
{
    None = 1,
    Slash
}
public enum PlayerType
{
    UnityChan = 1,
    OricharChan
}

public enum KeyType
{
    MoveUp,
    MoveDown,
    MoveRight,
    MoveLeft,
    XMove,
    ChangeDirection,
    Attack,
    DeathBlow,
    Idle,
    Dash,
    MenuOpen,
    MenuOk,
    MenuCancel,
    MenuMultiSelectOk,
    KeyDisplay,
    MessageLog,
    LookOption,
    ItemSort
}

public enum TurnState
{
    None,
    //メニュー系
    FirstMenu = 1,
    ItemMenu,
    SystemMenuInit,
    SystemMenu,
    ItemSubMenu,
    ItemOption,
    ItemInDrive,
    ItemInDriveFromDrive,
    ItemTargetSelect,
    LookDrive,
    MenuExit,
    ItemDelete,
    SaveInit,
    ItemAnalyse,
    //調合系
    AtelierMainInit,
    AtelierStrengthInit,
    AtelierMain,
    AtelierSubAction,
    AtelierMatelialSelect,
    AtelierMatelialOption,
    AtelierCreate,
    AtelierCreateExecute,
    Event,
    //ターン管理系
    Player,
    PlayerItemUse,
    ItemAction,
    MoveAfterCheck,
    EnemyItemEffect,
    EnemyInit,
    Enemy,
    MovingSetup,
    SpecialMoving,
    SpecialMovingTrap,
    Moving,
    Trap,
    TurnFinish,
    //階層移動
    NextFloor,
    PlayerDeathInit,
    PlayerDeath
}

public enum SelectTurnState
{
    ManageStart,
    CharacterSelect,
    ActionSelectStart,
    ActionSelect,
    CharacterAnimation,
    DungeonSelectStart,
    DungeonSelect,
    //メニュー系
    MenuWarehouseOutStart,
    MenuWarehouseOut,
    MenuWarehouseInStart,
    MenuWarehouseIn,
    ItemSubMenu,
    LookDrive,
    ItemOption,
    FinishMenu
}

public enum LoadDirection
{
    Left = 1,
    Top,
    Right,
    Bottom,
    None = 0
}

public enum LoadStatus
{
    None = 1,
    Load,
    Wall,
    Room,
    RoomEntrance,
    RoomExit,
    Water
}

public enum ActionType
{
    None = 1,
    Move,
    Attack,
    DeathBlow,
    Throw,
    Sing,
    Death,
    Blow,
    Special
}

public enum ObjectType
{
    Enemy = 1,
    Player,
    Friend,
    Item,
    Trap,
    Stair,
    Kiln
}

public enum FirstMenuType
{
    Item = 1,
    Foot,
    System,
    GetStair,
    NotStair,
    Atelier,
    AtelierStrength,
    AtelierCancel,
    ItemDelete,
    ItemAnalyse,
    Save,
}

public enum ItemType
{
    All = 1,
    Weapon,
    Shield,
    Ring,
    Food,
    Candy,  　//薬
    Melody,    //巻物
    Ball,//杖
    Bag,  //ツボ
    Other,
    Material
}


public enum WeaponAppearanceType
{
    None = 1,
    Sword,
    Staff,
    Knife,
    Axe,
    Mace,
    Bat,
    HockeyStick,
    Plunger,
}
public enum WeaponEffectType
{
    None = 1,
    Stone,
    MachineGun

}

public enum ShieldAppearanceType
{
    None = 1,
    Podlit,
    Wood,
    Paper,
    Knight,
    Empire,
    Stars
}
public enum RecipeType
{
    Normal,
    Strength
}
public enum MenuItemActionType
{
    Get = 1, //拾う
    Equip, //装備
    RemoveEquip,//外す
    Use,//使う
    Melody,//奏でる
    Put,//置く
    Eat,//食べる
    Throw,//投げる
    Putout,//出す
    Putin,//入れる
    Look,//覗く
    PutinParent,//入れる（ドライブ側）
    LookOption //オプション参照
}

public enum AtelierMenuActionType
{
    Auto,//自動選択
    Manual//手動選択
}

//フェードの状態
public enum FadeState
{
    None = 1,
    FadeIn,
    FadeOut
}

public enum EnemyMove
{
    Self = 1,
    CAs,
    NAs,
    CLAs
}
public enum EnemySearchState
{
    //部屋に入った瞬間から入り口を踏むまで
    ToEntrance = 1,
    //入り口を踏んで部屋を出るとき
    OutRoom,
    //道に入ってから任意の部屋の入り口を踏むまで
    Load,
    //プレイヤーと戦闘中
    FightPlayer
}
public enum AttackState
{
    None = 1,
    Miss,
    Hit,
    Heal,
    Death
}
public enum BehaviorType
{
    None = 1,
    Attack,
    Melody
}
public enum BallType
{
    Fire = 1,
    Gyro,
    Change,
    Pickoff,
    Knuckle,
    Trap,
    Decoy,
    Bean,
    Slow,
    Handmaid,
    Fumble,
    Winning,
    Four,
    Emery
}

public enum TrapType
{
    Poison = 1, //毒の沼
    DeadlyPoison, //猛毒の沼
    Mud,        //ぬかるみ
    Rotation,   //回転床
    Song,       //子守歌
    Palalysis,  //痺れ霧
    SandStorm,  //砂嵐
    WaterBucket,  //水バケツ
    ColorMonitor,//極彩色モニター
    Cyclone,     //竜巻
    Summon,     //召喚陣
    Bomb,       //地雷
    TheFly,     //ハエのたかり場
    Photo,      //カレーコロッケの写真
    Electric,    //電流
    Pollen,     // 花粉
    Ember,      //残り火
}


public enum CandyType
{
    Common,
    Energy,
    Handmaid,
    Fly,
    Bomb,
    Garlic,
}

public enum FoodType
{
    Common,
    Curry,
    Handmaid,
    Fly
}
public enum MelodyType
{
    Electric,
    Sleep,
    Confusion,
    Light,
    Anarchy,
    Horn,
    Forget,
    ThrowAway,

}
public enum BagType
{
    Save,
    Recycle,
    Change,
    Food,
    Normal
}
public enum OtherType
{
    Dollar,
    Garbage
}
public enum RingType
{
    None = 1,
    Thief,
    TwoRing,
    AbnormalPrevent,
    Ryu,
    OneEyes,
    Listen,
    Occult,
    Health,
    EvilLuck,
    Tunnel,
    Unlucky
}
public enum MaterialType
{
    None = 1,
    Basic,
    Strength,
    StrengthBase
}
public enum OptionBaseType
{
    Weapon,
    Shield,
    Material,
    MaterialStrength,
    Food,
    Candy,
    Ball
}
public enum OptionType
{
    HP,
    Defence,
    Attack,
    Range,
    AttackAbnormal,
    DefenceAbnormal,
    DefenceRecover,
    AttackRecover,
    AttackBlow,
    WalkRecover,
    Adversity,
    SatSmall,
    AttackRace,
    DefenceRace,
    SatBig,
    Narcolepsy,
    Glass,
    ExpPlus,
    ExpMinus,
    Oakenshield,
    Liquid,
    Reflection,
    Breakthrough,
    Fatal,
    IronWall,
    DexTrap,
    Sacrifice,
    Adhesive,

    //以下素材オプション
    AddAbnormal,
    ReverceAbnormal,
    ReverceDamage,
    CatchingFire,
    Wildfire,
    EffectUp,
    EffectDown,
    EffectStabile,
    EffectNotStabile,

    RecoverPower,
}

public enum EnemyType
{
    Rabbit_1,
    Plant_1,
    Bat_1,
    Slime_1,
    Ghost_1,
    Butterfly_1,
    TeddyBear_1,
    SPIDER_1,
    Whale_1,
    Whale_King,
    PA_Drone_1,
    PA_Warrior_1,
    Rabbit_2,
    Ghost_2,
    Slime_2,
    Slime_3,
    Bat_2,
    Rabbit_3,
    TeddyBear_2,
    Rabbit_King,
    Plant_King,
    Alarmer_1,
    DarkStella_1,
    DarkUnity_1,
    Drangwin_1,
}

public enum DeathCouseType
{
    None,
    Attack,
    Hunger,
    Item,
    Explosion,
    BodySlam,
    MeteorStorm,
    Status,
    Trap,
    Throw,
    Kugeltanz,
    Blow,
}

public enum NewsType
{
    None = 1,
    DungeonStart = 2,
    DungeonNext = 3,
    DisruptSuccess = 4,
    DisruptFail = 5,
}

public enum AnimationType
{
    None,
    IsMove,
    IsAttack,
    IsDeathBlow,
    IsThrow,
    IsSing,
    IsDeath
}

public enum OperationMode
{
    KeyOnly,
    UseMouse,
}


#region 一項目で複数の値を持つもの
public enum StateAbnormal
{
    Normal = 0,
    Dark = 1,   //暗闇
    Poison = 2,     //毒
    DeadlyPoison = 4,     //猛毒
    Sleep = 8,      //睡眠
    Palalysis = 16,  //麻痺
    Confusion = 32,  //混乱
    Charmed = 64,    //魅了
    Decoy = 128,    //身代わり
    Slow = 256,      //スロー
    Reticent = 512,    //かぜ
    StiffShoulder = 1024,    //肩こり
    Acceleration = 2048    //加速
}
public enum Race
{
    None = 0,
    Animal = 1,
    Plant = 2,
    Mineral = 4,
    TwoPairs = 8,
    Insect = 16,
    Air = 32,
    MultiPairs = 64
}

#endregion 一項目で複数の値を持つもの