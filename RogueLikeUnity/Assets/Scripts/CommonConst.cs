using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonConst {

    /// <summary>
    /// 0 ～ 65535
    /// キャラクター番号
    /// 10000 ～
    /// アイテム番号
    /// 20000 ～
    /// 罠番号
    /// 30000 ～
    /// オプション番号
    /// 40000 ～
    /// ダンジョン
    /// 50000 ～
    /// 合成番号
    /// 60000 ～
    /// </summary>
    public class ObjNo
    {
        public const long None = 0;
        public const long Player = 11001;
        public const long FlyCroquette = 23004;
        public const long Dollar = 29001;
        public const long Bomb1 = 30011;
        public const long Ember = 30019;
        public const long BallHandmaid = 25010;
        public const long BallGyro = 25002;
        public const long MaterialSandIron = 30003;

        //オプション
        public const long OptionBallEffectDown = 40143;
        public const long OptionBallCathingFire = 40139;
        public const long OptionBallEffectNotStabile = 40145;
        public const long OptionBallWildfire = 40140;
    }

    public class Rotation
    {
        public const float None = 0;
        public const float Top = 0;
        public const float TopLeft = -45;
        public const float Left = -90;
        public const float BottomLeft = -135;
        public const float Bottom = 180;
        public const float BottomRight = 135;
        public const float Right = 90;
        public const float TopRight = 45;
    }
    public class Wait
    {

        /// <summary>
        /// 移動終了後の遊び
        /// </summary>
        public const sbyte EndMove = 2;

        public const sbyte None = 0;
        /// <summary>
        /// 足踏みの遊び
        /// </summary>
        public const float Idle = 0.07f;

        /// <summary>
        /// 方向変化後の遊び
        /// </summary>
        public const float ChangeDirection = 0.3f;

        /// <summary>
        /// アイテムに乗った際の遊び
        /// </summary>
        public const float OnItem = 0.17f;

        /// <summary>
        /// アイテム使用時
        /// </summary>
        public const float ItemUse = 0.3f;

        /// <summary>
        /// メニュー選択時の遊び
        /// </summary>
        public const float MenuSelect = 0.7f;

        /// <summary>
        /// メニュー移動時の遊び
        /// </summary>
        public const float MenuMove = 0.3f;

        /// <summary>
        /// Floor変更時の表示時間(秒)
        /// </summary>
        public const sbyte FloorChangeSeconds = 3;

        /// <summary>
        /// 死亡時
        /// </summary>
        public const float Death = 2;

        /// <summary>
        /// トラップ発動時
        /// </summary>
        public const float TrapStart = 0.33f;

        /// <summary>
        /// 竜巻
        /// </summary>
        public const float Cyclone = 2;

        /// <summary>
        /// 眠り時ターン事
        /// </summary>
        public const float Sleep = 0.1f;

        /// <summary>
        /// RTSの時間
        /// </summary>
        public const float RTS = 1.0f;
    }
    

    public class Dungeon
    {
        /// <summary>
        /// 区画と部屋の余白サイズ
        /// </summary>
        public const ushort OuterMergin = 2;
        /// <summary>
        /// 部屋配置の余白サイズ
        /// </summary>
        public const ushort PosMergin = 1;
        /// <summary>
        /// 最小の部屋サイズ
        /// </summary>
        public const ushort MinRoomSIze = 4;
        /// <summary>
        /// 最大の部屋サイズ
        /// </summary>
        public const ushort MaxRoomSize = 10;
    }

    public class Probability
    {
        /// <summary>
        /// 区画と部屋の余白サイズ
        /// </summary>
        public const float CreateRoom = 0.98f;
    }
    public class Map
    {
        /// <summary>
        /// マップ単位
        /// </summary>
        public const ushort Unit = 5;
        /// <summary>
        /// マージン
        /// </summary>
        public const ushort StartMergin = 0;

        /// <summary>
        /// Z
        /// </summary>
        public const ushort Z = 0;
    }

    public class DisplaySize
    {
        public const int HpHeightMergin = 0;
        public const int HpTitleMergin = 3;
        public const int HpItemMergin = 30;
    }

    public class MovePattern
    {
        /// <summary>
        /// 各自勝手に動く
        /// </summary>
        public const ushort Self = 1;
        
        /// <summary>
        /// CAs
        /// 各自が他の位置を確認しながら独立して動く
        /// </summary>
        public const ushort CAs = 2;

        /// <summary>
        /// NAs
        /// 各自が他と相談しながら全体最適になるように動く
        /// </summary>
        public const ushort NAs = 3;

        /// <summary>
        /// CLAs
        /// ボスの指示で統制的に動く
        /// </summary>
        public const ushort CLAs = 4;
    }

    public class Status
    {
        /// <summary>
        /// 満腹度の減少値（初期値）
        /// </summary>
        public const float SatietyReduce = 0.1f;

        /// <summary>
        /// 満腹度の警告タイミング
        /// </summary>
        public const float SatietyCautionTiming = 20;

        /// <summary>
        /// 満腹度の危険タイミング
        /// </summary>
        public const float SatietyDangerTiming = 0;

        /// <summary>
        /// 満腹度の最大値（初期値）
        /// </summary>
        public const float SatietyMax = 100;
    }
    public class SystemValue
    {
#if UNITY_EDITOR
        public const string UrlResBase = "https://custom-sbjp.firebaseapp.com/RLUT/AssetBundle/{0}";
        public const string CurrentVersion = "0.01";
#else
        public const string UrlResBase = "https://custom-sbjp.firebaseapp.com/RLU/AssetBundle/{0}";
        public const string CurrentVersion = "1a02";
#endif
        /// <summary>
        /// ユニット単位
        /// </summary>
        public const float FieldUnit = 1f;

        public const float AnimationSpeedDefault = 1;
        public const float AnimationSpeedIdle = 10f;
        public const float AnimationSpeedDash = 10f;

        //public const float MoveSpeedDefault = 0.15f;
        //public const float MoveSpeedDefaultEnemy = 0.2f;
        //public const float MoveSpeedItemThrow = 0.4f;
        //public const float MoveSpeedDash = 0.5f;
        //public const float MoveSpeedDamageUp = 1.6f;
        //public const float MoveSpeedGunBullet = 0.5f;
        //public const float MoveSpeedWaterBucket = -0.16f;
        //public const int MoveNumberSpotLight = 9;

        public const float MoveSpeedDefault = 0.075f;
        public const float MoveSpeedDefaultEnemy = 0.10f;
        public const float MoveSpeedItemThrow = 0.2f;
        public const float MoveSpeedDash = 0.25f;

        public const float MoveSpeedDamageUp = 0.8f;
        public const float MoveSpeedGunBullet = 0.25f;
        public const float MoveSpeedWaterBucket = -0.08f;

        public const int MoveNumberSpotLight = 18;


        public const float UiVectorZ = 0;

        /// <summary>
        /// アニメーションの実行距離
        /// </summary>
        public const int AnimationExeDistance = 6;

        /// <summary>
        /// 投げる時の初期飛距離
        /// </summary>
        public const ushort ThrowDistance = 10;

        public static readonly Vector3 VecterDammy = new Vector3(float.MinValue, float.MinValue, float.MinValue);

    }
    public class Color
    {
        public const string Caution = "#E49B0F";
        public const string Danger = "#FF2600";
        public const string ItemBad = "#FF2600";
        public const string Enemy = "#FF2600";
        public const string SelectItem = "#FBEC35";
        public const string Player = "yellow";
        public const string Dungeon = "#BCC8DB";
        public const string DeathCause = "#EF8F9C";
        public const string NotAnalyse = "#BCC8DB";
        public const string SuccessMessage = "#1987E5";

    }
    public class Message
    {
        public static void SetJp()
        {
            SubTitle = "Unity Chan's Rouge-Like";

            StartContinue = "前回の続きから開始";
            StartFirst = "村から開始";

            KeyInfoReflect = "<color=" + Color.SuccessMessage + ">キー情報が反映されました。</color>";
            KeyInfoInitial = "<color=" + Color.SuccessMessage + ">キー情報が初期化されました。</color>";

            KeyConfigSuccess = "キーコンフィグ情報が正常に読み込まれました。";
            SystemSettingSuccess = "システム設定情報が正常に読み込まれました。";

            UnityChanName = "ユニティちゃん";
            StellaChanName = "ステラちゃん";

            SelectCharacter = "キャラクター選択";
            SelectDungeon = "ダンジョン選択";
            Floor = "階層";
            Sat = "満腹";

            NotSet = "未設定";

            Exist = "あり";
            None = "なし";

            DungeonName = "ダンジョン名";
            CharacterName = "キャラクター名";

            Success = "踏破成功";
            Failure = "踏破失敗";

            Name = "名前";
            SearchFloor = "到達階層";
            PlayTime = "プレイ時間";
            Level = "レベル";
            CauseOfFailure = "やられた原因";
            TheMostUsedWeapon = "一番使った武器";
            TheMostUsedShield = "一番使った盾";
            TheMostUsedRing = "一番使った指輪";

            ItemMaximumPossession = "アイテム最大重量";
            Finisher = "必殺技";
            FormulationTalent = "調合適正";

            Good = "良好";
            Bad = "不良";

            BareHands = "素手";

            SelectAuto = "自動選択";
            SelectManual = "手動選択";

            DungeonDepth = "深度";
            Appraisal = "鑑定";
            Visibility = "視界";
            Bringing = "持ち込み";
            TurnHaveTime = "ターン時間";

            NextLv = "次のLv";
            ItemWeight = "所持重量";
            Synthesis = "調合";
            Strength = "強化";
            DiscardItems = "アイテム破棄";
            Close = "閉じる";
            MoveOn = "先に進む";

            SaveAndStop = "セーブして中断";

            Stay = "とどまる";
            ItemMenu = "アイテムメニュー";

            ExaminingFoot = "足元調査";

            KeySetting = "キー設定";
            
            StateAbnormalNormal = "正常";
            StateAbnormalDark = "暗闇";
            StateAbnormalSleep = "睡眠";
            StateAbnormalPoison = "毒";
            StateAbnormalDeadlyPoison = "猛毒";
            StateAbnormalPalalysis = "麻痺";
            StateAbnormalConfusion = "混乱";
            StateAbnormalCharmed = "魅了";
            StateAbnormalDecoy = "デコイ";
            StateAbnormalSlow = "スロー";
            StateAbnormalReticent = "かぜ";
            StateAbnormalStiffShoulder = "肩こり";
            StateAbnormalAcceleration = "加速";
            

            ActionTypeGet = "拾う";
            ActionTypeEat = "食べる";
            ActionTypeEquip = "装備";
            ActionTypePut = "置く";
            ActionTypeLook = "覗く";
            ActionTypeMelody = "奏でる";
            ActionTypePutin = "入れる";
            ActionTypePutinParent = "入れる";
            ActionTypePutout = "出す";
            ActionTypeRemoveEquip = "外す";
            ActionTypeThrow = "投げる";
            ActionTypeUse = "使う";
            ActionTypeLookOption = "オプション";

            FormulationRecipe = "調合レシピ";
            RequiredMaterial = "必要素材";
            MaterialOption = "素材オプション";

            ATK = "攻撃力";
            DEF = "防御力";
            Options = "Option数";
            Satiety = "満腹度";
            State = "状態";

            System = "設定";
            KeyConfig = "キーコンフィグ";
            MoveUp = "移動（上）";
            MoveDown = "移動（下）";
            MoveLeft = "移動（左）";
            MoveRight = "移動（右）";
            XMove = "斜め移動固定";
            ChangeDirection = "方向転換固定";
            DisplayMenu = "メニュー表示";
            MessageLog = "メッセージログ";
            Dash = "ダッシュ";
            Attack = "攻撃";
            Idle = "足踏み";
            DisplayKeySetting = "キー情報表示";
            MenuOk = "メニュー決定";
            MenuCancel = "メニューキャンセル";
            ReferOption = "オプション参照";
            ItemSort = "アイテムソート";
            MultipleItemSelection = "アイテム複数選択";
            DirectionPadUp = "方向キー（上）";
            DirectionPadLeft = "方向キー（左）";

            KeyDescription = @"Keyフィールドにカーソルを合わせて、設定したいキーを押してください。OKを押すと反映されます。.
同一カテゴリ内で同じキーを設定することはできません。";
            KeyDescriptionDPad = @"Keyフィールドにカーソルを合わせて、対応する方向キーを押してください。
操作が逆転する場合はリストボックスから「Reverse」を選択してください。";

            HungryCaution = "<color=" + Color.Caution + ">おなかが減ってきた！</color>";

            HungryDanger = "<color=" + Color.Danger + ">おなかが減って倒れそう！</color>";
            DriveFull = "このDriveは容量がパンパンだ！";
            RemoveEquipItem = "{0}を外した";
            NotDrive = "このアイテムは入れられない！";
            ItemAdhesiveNotRemove = "{0}はくっついて外せない！";
            ItemAdhesive = "{0}は手にくっついた！";
            ItemDeleteNotSelect = "「{0}」キーで破棄するアイテムを選択してください。";
            ItemDeleteNotSelectUseMouse = "「{0}」キーかダブルクリックで破棄するアイテムを選択してください。";
            ItemDelete = "アイテムをまとめて破棄した！";
            ItemAnalyse = "{0}は{1}だった！";
            ItemLuckAnalyseMaterial = "鑑定をするには砂鉄が必要だ！";
            DriveBug = "このDriveはバグっている！";
            PutinAction = "{0}を{1}に入れた";
            ThrowAction = "{0}を投げた！";
            DropItem = "{0}は床に落ちた";
            BreakItem = "{0}は壊れてしまった";
            VanishItem = "{0}は無限の彼方に消え去った";
            WaterInItem = "{0}は水底に沈んだ";
            RecoverPower = "力が湧いてきた！";
            IncreaseHp = "体力が上がった！";
            RecoverHp = "{0}のHPが回復した！";
            RecoverSatiety = "おなかが膨れた！";
            RecoverSatietyMax = "おなかがいっぱいになった！";
            RecoverState = "{0}が治った！";
            BadFood = "この世のものとは思えない腐臭が{0}を襲う！";
            AddDark = "{0}は視界を奪われた！";
            AddState = "{0}は{1}にかかった！";
            AddDecoy = "{0}はデコイに変化した！";
            AddPalalysis = "{0}は体が動かない！";
            AddSlow = "{0}は速度が遅くなった！";
            AddConf = "{0}は混乱した！";
            AddReticent = "{0}は風邪をひいた！";
            AddSleep = "{0}は眠ってしまった！";
            AddShoulder = "{0}は肩がこってしまった！";
            AddAcceleration = "{0}は速度が速くなった！";
            ThrowOffItem = "{0}は{1}を落とした！";
            UseItem = "{0}を使った";
            EatItem = "{0}を食べた";
            BlowCharacter = "{0}は吹っ飛んだ";
            EnemyCamouflage = "アイテムは擬態した{0}だった！";
            EnemyExplosion = "{0}の自爆！";
            EnemyTornado = "{0}は激しく羽ばたいた！";
            EnemyTornado2 = "部屋内の位置が入れ替わった！";
            EnemyRecover1 = "{0}は{1}の傷を舐めた";
            EnemyRecover2 = "{0}のHPが回復した！";
            EnemyAngry = "{0}は激怒した！";
            EnemyRabbitDeathBlow = "{0}ののしかかり！";
            EnemyPlantDeathBlow = "{0}のメテオストーム！";
            EnemyAlarm1 = "{0}は警報を鳴らした！";
            EnemyAlarm2 = "{0}が駆けつけた！";
            EnemyShorten = "{0}の縮地！";
            EnemyThrow = "{0}は{1}を投げた！";
            EnemyAwakening = "{0}の覚醒！";
            EnemySwallowing1 = "{0}の丸呑み！";
            EnemySwallowing2 = "{0}が飲み込まれた！";
            EnemyForgetMap = "{0}の見えざる手！マップを忘れてしまった！";
            ChangePoint = "{0}と場所が入れ替わった";
            EquipItem = "{0}を装備した";
            GetItem = "{0}を拾った";
            OnItem = "{0}に乗った";
            FootNoneObject = "足元には何も無い！";
            FailGetItem = "持ち物がいっぱいだ！";
            MelodySing = "{0}を奏でた！";
            HitThrowAfter = "{0}に当たった！";
            AttackMessage = "{0}に{1}のダメージ！";
            AttackEnemy = "{1}に{2}のダメージ！";
            AttackPlayer = "{1}は{2}のダメージ！";
            AttackMissPlayer = "{0}は攻撃を回避！";
            AttackMissEnemy = "攻撃が外れた！";
            DeathPlayer = "{0}はやられてしまった！";
            DeathEnemy = "{0}は倒れた！{1}の経験値を獲得！";
            DeathCommon = "{0}は倒れた！";
            DeathKiln = "窯が壊れてしまった！";
            LevelUpPlayer = "{0}はレベルアップ！";
            LevelDownPlayer = "{0}はレベルダウン！";
            EmeryCharacter = "{0}はフロアから退場させられた！";
            Division = "{0}は分裂した";
            Sacrifice = "{0}を犠牲に復活した！";
            DamagePower = "{0}は力を奪われた！";
            StealItem = "{0}は{1}を盗んだ！";
            RunAmway = "{0}は逃亡した！";
            ForgetMap = "マップを忘れてしまった！";
            ThrowAway = "{0}は物が拾えなくなった！";
            ShedEquipItem = "{0}を弾き飛ばされた！";
            TrapSetBomb = "{0}は{1}を埋設した";
            DexTrap = "トラップは不発に終わった！";
            TrapCommon = "{0}に踏み込んでしまった！";
            TrapBreak = "{0}が破壊された！";
            TrapSong = "どこからともなく子守歌が聴こえてきた！";
            TrapSong2 = "{0}は眠ってしまった！";
            TrapElectric = "突如、{0}に電流走る・・・！";
            TrapEquipRemove = "{0}は装備を手放してしまった！";
            TrapColor = "極彩色のモニターが輝いた！";
            TrapCyclone = "局地的竜巻が発生した！";
            TrapCyclone2 = "{0}は上階に吹き飛ばされた！";
            TrapBigBang = "地雷を踏んでしまった！";
            TrapCroquette = "鮮明なカレーコロッケの写真が落ちている";
            TrapCroquette2 = "{0}はおなかがすいてしまった！";
            TrapSmoke = "床から神経ガスが噴き出てきた！";
            TrapSummon = "敵が召喚された！";
            TrapRote = "回転床が回りだした！";
            TrapSandStorm = "突如砂嵐が巻き起こった！";
            TrapFly = "ハエのたかり場に突っ込んでしまった！";
            TrapFly2 = "持っていたコロッケにハエがたかった！";
            TrapPllen = "{0}の周辺に花粉が舞い散る！";
            TrapPllen2 = "{0}は力が抜けてしまった！";
            TrapEmber = "床に火が付いた！";
            TrapDamage = "{0}は{1}により{2}のダメージ！";
            TrapRecover = "{0}は{1}により{2}回復！";
            TrapWaterBucket = "空から水の入ったバケツが！";
            DeathBlowNotPower = "必殺技を撃つにはパワーが足りない！";
            DeathBlowKugeltanz = "{0}の必殺クーゲルタンズ！";
            DeathBlowMagicalRecipe = "{0}の不思議なレシピ！";
            DeathBlowMagicalRecipe2 = "次回の調合に必ず成功する！";
            AtelierNotCreate = "それを作るには素材が足りない";
            AtelierMaxItem = "それを作るには持ち物の空きが足りない";
            AtelierMaxMaterial = "必要数以上の素材は選択できません";
            AtelierMaterialNotRes1 = "選択した素材の数が足りません。";
            AtelierMaterialNotRes2 = "「{0}」キーで素材を選択してください。";
            AtelierMaterialNotRes2UseMouse = "「{0}」キーかダブルクリックで素材を選択してください。";
            AtelierFail = "調合に失敗した！";
            AtelierSuccess = "調合に成功した！";
            NotThrow = "{0}は肩こりで物が投げられない！";
            NotSing = "{0}はかぜで声が出ない！";

        }

        public static void SetEn()
        {
            SubTitle = "Unity Chan's Adventure";

            StartContinue = "Start from the previous continuation";
            StartFirst = "Start from the village";

            KeyConfigSuccess = "Key configuration information was successfully read.";
            SystemSettingSuccess = "System setting information was successfully read.";


            KeyInfoReflect = "<color=" + Color.SuccessMessage + ">Key information has been reflected.</color>";
            KeyInfoInitial = "<color=" + Color.SuccessMessage + ">Key information has been initialized.</color>";

            //names
            UnityChanName = "Unity";
            StellaChanName = "Stellas";

            SelectCharacter = "Select Character";
            SelectDungeon = "Select Dungeon";
            Floor = "Floor";
            Sat = "Sat";

            Success = "Success";
            Failure = "Failure";

            Name = "Name";
            SearchFloor = "Reach Floor";
            PlayTime = "Play Time";
            Level = "Level";
            CauseOfFailure = "Cause of failure";
            TheMostUsedWeapon = "The most used weapon";
            TheMostUsedShield = "The most used shield";
            TheMostUsedRing = "The most used ring";

            ItemMaximumPossession = "Maximum item weight";
            Finisher = "Finisher";
            FormulationTalent = "Synthetic Talent";

            Exist = "Exist";
            None = "None";

            NotSet = "Not set";

            DungeonName = "Dungeon Name";
            CharacterName = "Character Name";

            Good = "Good";
            Bad = "Bad";

            BareHands = "Bare Hands";

            SelectAuto = "Auto select";
            SelectManual = "Manual select";

            DungeonDepth = "Depth";
            Appraisal = "Appraisal";
            Visibility = "View";
            Bringing = "Bringing";
            TurnHaveTime = "Turn Time";

            NextLv = "Next Lv";
            ItemWeight = "Item Weight";
            Synthesis = "Synthesis";
            Strength = "Forge";
            DiscardItems = "Discard Items";
            Close = "Close";
            MoveOn = "Move on";

            SaveAndStop = "Save and stop";

            Stay = "Stay";
            ItemMenu = "Item Menu";

            ExaminingFoot = "Examining Foot";

            KeySetting = "Key Settings";

            StateAbnormalNormal = "Normal";
            StateAbnormalDark = "Dark";
            StateAbnormalSleep = "Sleep";
            StateAbnormalPoison = "Poison";
            StateAbnormalDeadlyPoison = "DeadlyPoison";
            StateAbnormalPalalysis = "Palalysis";
            StateAbnormalConfusion = "Confusion";
            StateAbnormalCharmed = "Charmed";
            StateAbnormalDecoy = "Decoy";
            StateAbnormalSlow = "Slow";
            StateAbnormalReticent = "Cold";
            StateAbnormalStiffShoulder = "StiffShoulder";
            StateAbnormalAcceleration = "Acceleration";


            ActionTypeGet = "Get";
            ActionTypeEat = "Eat";
            ActionTypeEquip = "Equip";
            ActionTypePut = "Put";
            ActionTypeLook = "Look";
            ActionTypeMelody = "Sing";
            ActionTypePutin = "Put in";
            ActionTypePutinParent = "Put in";
            ActionTypePutout = "Put out";
            ActionTypeRemoveEquip = "Remove";
            ActionTypeThrow = "Throw";
            ActionTypeUse = "Use";
            ActionTypeLookOption = "Option";

            FormulationRecipe = "Synthesis recipe";
            RequiredMaterial = "Required material";
            MaterialOption = "Material options";

            ATK = "ATK";
            DEF = "DEF";
            Options = "Options";
            Satiety = "Satiety";
            State = "State";

            System = "System Setting";
            KeyConfig = "Key config";
            MoveUp = "Move up";
            MoveDown = "Move down";
            MoveLeft = "Move left";
            MoveRight = "Move right";
            XMove = "Diagonal move";
            ChangeDirection = "Change direction";
            DisplayMenu = "Display menu";
            MessageLog = "Display Log";
            Dash = "Dash";
            Attack = "Attack";
            Idle = "Idle";
            DisplayKeySetting = "Display key setting";
            MenuOk = "Menu Ok";
            MenuCancel = "Menu Cancel";
            ReferOption = "Refer option";
            ItemSort = "Sort Item";
            MultipleItemSelection = "Multiple item selection";
            DirectionPadUp = "D-Pad Up";
            DirectionPadLeft = "D-Pad Left";

            KeyDescription = @"Move the cursor to the ""Key"" field and press the key you want to set.Press OK to apply.
It is not possible to set the same key within the same category.";
            KeyDescriptionDPad = @"Move the cursor to the ""Key"" field and press the corresponding direction key.
If the operation is reversed, select ""Reverse"" from the list box.";

            HungryCaution = "<color=" + Color.Caution + ">Getting hungry!</color>";

            HungryDanger = "<color=" + Color.Danger + "> Faint with hunger!</color>";
            DriveFull = "This drive is full!";
            RemoveEquipItem = "Removed {0}";
            NotDrive = "Can not put in this item!";
            ItemAdhesiveNotRemove = "{0} is glued!";
            ItemAdhesive = "{0} adhered to the hand!";
            ItemDeleteNotSelect = "Please select item to discard with \"{0}\" key.";
            ItemDeleteNotSelectUseMouse = "Please select item to discard with \"{0}\" key or double click.";
            ItemDelete = "Discarded selected items";
            ItemAnalyse = "{0} was {1}!";
            ItemLuckAnalyseMaterial = "Need sand iron to make an appraisal!";
            DriveBug = "This drive is bug!";
            PutinAction = "Put {0} in {1}";
            ThrowAction = "Threw {0}!";
            DropItem = "{0} fell to the floor";
            BreakItem = "{0} have broken";
            VanishItem = "{0} have disappeared to infinity";
            WaterInItem = "{0} sank to the bottom of the water";
            RecoverPower = "Power came up!";
            IncreaseHp = "The maximum value of HP has risen!";
            RecoverHp = "{0}'s  HP has recovered!";
            RecoverSatiety = "Hunger has been relaxed!";
            RecoverSatietyMax = "Became full!";
            RecoverState = "{0} has healed！";
            BadFood = "A stench that can not be thought of as a thing of this world attacks {0}！";
            AddDark = "{0} was deprived of vision!";
            AddState = "{0} took on {1}！";
            AddDecoy = "{0} changed to a decoy！";
            AddPalalysis = "{0} is numb and can not move!";
            AddSlow = "{0} slowed down in speed!";
            AddConf = "{0} was confused!";
            AddReticent = "{0} caught a cold!";
            AddSleep = "{0} fell asleep!";
            AddShoulder = "{0} got stiff shoulder！";
            AddAcceleration = "{0} got faster！";
            ThrowOffItem = "{0} dropped {1}！";
            UseItem = "Used {0}";
            EatItem = "Ate {0}";
            BlowCharacter = "{0} blew away";
            EnemyCamouflage = "Item was a mimetic {0}!";
            EnemyExplosion = "{0} suicide bombed！";
            EnemyTornado = "{0} fluttered violently！";
            EnemyTornado2 = "The position in the room has changed.！";
            EnemyRecover1 = "{0} licked {1}'s injuries";
            EnemyRecover2 = "{0}'s  HP has recovered!";
            EnemyAngry = "{0} was furious!";
            EnemyRabbitDeathBlow = "{0} did a body press!";
            EnemyPlantDeathBlow = "{0} cast a meteorostom!";
            EnemyAlarm1 = "{0} sounded an alarm!";
            EnemyAlarm2 = "{0} came running！";
            EnemyShorten = "{0} did shorten distances!";
            EnemyThrow = "{0} threw {1}!";
            EnemyAwakening = "Awakening of {0}!";
            EnemySwallowing1 = "{0} swallowed！";
            EnemySwallowing2 = "{0} was swallowed！";
            EnemyForgetMap = "Invisible hand of {0}! Forgot the map!";
            ChangePoint = "The place has been exchanged with {0}";
            EquipItem = "Equipped {0}";
            GetItem = "Got {0}";
            OnItem = "Got on {0}";
            FootNoneObject = "Nothing is at foot!";
            FailGetItem = "Belongings are full!";
            MelodySing = "Played {0}!";
            HitThrowAfter = "Hit {0}!";
            AttackMessage = "{0} received {1} damage!";
            AttackEnemy = "{1} received {2} damage!";
            AttackPlayer = "{1} received {2} damage!";
            AttackMissPlayer = "{0} avoided the attack!";
            AttackMissEnemy = "Attack missed！";
            DeathPlayer = "{0} fainted！";
            DeathEnemy = "{0} fainted！gained {1} Exp!";
            DeathCommon = "{0} fainted！";
            DeathKiln = "The kiln has broken!";
            LevelUpPlayer = "{0} has risen the level!";
            LevelDownPlayer = "{0} has risen the fallen!";
            EmeryCharacter = "{0} was banished from the floor!";
            Division = "{0} split!";
            Sacrifice = "Revived the sacrifice of {0}!";
            DamagePower = "{0} was deprived of power！";
            StealItem = "{0} stole {1}!";
            RunAmway = "{0} fled!";
            ForgetMap = "Forgot the map!";
            ThrowAway = "{0} can not pick up items!";
            ShedEquipItem = "send {0} flying!";
            TrapSetBomb = "{0} fill {1}";
            DexTrap = "The trap ended in failure!";
            TrapCommon = "Stepped into {0}!";
            TrapBreak = "{0} was destroyed!";
            TrapSong = "Hear a lullaby from nowhere!";
            TrapSong2 = "{0} fell asleep!";
            TrapElectric = "Suddenly, current flowed through {0}!";
            TrapEquipRemove = "{0} has let go of the equipment!";
            TrapColor = "A polar color monitor shined!";
            TrapCyclone = "Local tornadoes occurred!";
            TrapCyclone2 = "{0}XX was blown off the upper floor!";
            TrapBigBang = "Stepped on a landmine!";
            TrapCroquette = "Sharp curry croquette picture is falling";
            TrapCroquette2 = "{0} got hungry!";
            TrapSmoke = "Nerve gas blew out from the floor!";
            TrapSummon = "Enemy's were summoned!";
            TrapRote = "Rotating floor revolved!";
            TrapSandStorm = "Suddenly a sandstorm wrapped up!";
            TrapFly = "Plunge into garbage!";
            TrapFly2 = "Flies came to the croquette that I had!";
            TrapPllen = "Pollen dances around {0}!";
            TrapPllen2 = "{0} has gone out of power!";
            TrapEmber = "The floor was on fire!";
            TrapDamage = "{0} has {2} damage by {1}!";
            TrapRecover = "{0} has {2} recovered by {1}!";
            TrapWaterBucket = "A bucket with water from the sky!";
            DeathBlowNotPower = "Power is not enough to shoot a Finisher!";
            DeathBlowKugeltanz = "{0}'s Finisher Kugeltanz!";
            DeathBlowMagicalRecipe = "{0}'s Mysterious Recipes!";
            DeathBlowMagicalRecipe2 = "Make sure you succeed in the next synthesis!";
            AtelierNotCreate = "Materials are insufficient to make it";
            AtelierMaxItem = "The belongings are full to make it";
            AtelierMaxMaterial = "Can not select more than the required number of materials";
            AtelierMaterialNotRes1 = "The number of selected materials is insufficient";
            AtelierMaterialNotRes2 = "Please select the material with \"{0}\" key";
            AtelierMaterialNotRes2UseMouse = "Please select the material with \"{0}\" key or double click";
            AtelierFail = "Failed synthesis！";
            AtelierSuccess = "Succeeded synthesis！";
            NotThrow = "{0} can not throw things with stiff shoulders!";
            NotSing = "{0} has no voice by a cold！";

        }


        public const int DisplayCount = 4;
        //public const double DisplayTime = 3500;
        public const float DisplayTime = 3.5f;

        public static string NotSet;

        public static string KeyInfoReflect;
        public static string KeyInfoInitial;
        

        public static string SubTitle;

        public static string StartContinue;
        public static string StartFirst;

        public static string KeyConfigSuccess;
        public static string SystemSettingSuccess;

        public static string UnityChanName;
        public static string StellaChanName;

        public static string SelectCharacter;
        public static string SelectDungeon;
        public static string Floor;
        public static string Sat;

        public static string Exist;
        public static string None;

        public static string Good;
        public static string Bad;

        public static string BareHands;

        public static string DungeonName;
        public static string CharacterName;

        public static string Success;
        public static string Failure;
        public static string Name;
        public static string SearchFloor;
        public static string PlayTime;
        public static string Level;
        public static string CauseOfFailure;
        public static string TheMostUsedWeapon;
        public static string TheMostUsedShield;
        public static string TheMostUsedRing;


        public static string ItemMaximumPossession;
        public static string Finisher;
        public static string FormulationTalent;

        public static string DungeonDepth;
        public static string Appraisal;
        public static string Visibility;
        public static string Bringing;
        public static string TurnHaveTime;

        public static string NextLv;
        public static string ItemWeight;

        public static string Synthesis;
        public static string Strength;

        public static string DiscardItems;
        public static string Close;
        public static string MoveOn;

        public static string SaveAndStop;

        public static string Stay;

        public static string ItemMenu;

        public static string ExaminingFoot;
        //ステータス

        public static string StateAbnormalNormal = "正常";
        public static string StateAbnormalDark = "暗闇";
        public static string StateAbnormalSleep = "睡眠";
        public static string StateAbnormalPoison = "毒";
        public static string StateAbnormalDeadlyPoison = "猛毒";
        public static string StateAbnormalPalalysis = "麻痺";
        public static string StateAbnormalConfusion = "混乱";
        public static string StateAbnormalCharmed = "魅了";
        public static string StateAbnormalDecoy = "デコイ";
        public static string StateAbnormalSlow = "スロー";
        public static string StateAbnormalReticent = "かぜ";
        public static string StateAbnormalStiffShoulder = "肩こり";
        public static string StateAbnormalAcceleration = "加速";

        public static string ActionTypeGet = "拾う";
        public static string ActionTypeEat = "食べる";
        public static string ActionTypeEquip = "装備";
        public static string ActionTypePut = "置く";
        public static string ActionTypeLook = "覗く";
        public static string ActionTypeMelody = "奏でる";
        public static string ActionTypePutin = "入れる";
        public static string ActionTypePutinParent = "入れる";
        public static string ActionTypePutout = "出す";
        public static string ActionTypeRemoveEquip = "外す";
        public static string ActionTypeThrow = "投げる";
        public static string ActionTypeUse = "使う";
        public static string ActionTypeLookOption = "オプション";

        public static string KeySetting;

        public static string SelectAuto;
        public static string SelectManual;

        public static string FormulationRecipe;
        public static string RequiredMaterial;
        public static string MaterialOption;
        public static string ATK;
        public static string DEF;
        public static string Options;
        public static string Satiety;
        public static string State;

        public static string System;
        public static string KeyConfig;
        public static string MoveUp;
        public static string MoveDown;
        public static string MoveLeft;
        public static string MoveRight;
        public static string XMove;
        public static string ChangeDirection;
        public static string DisplayMenu;
        public static string MessageLog;
        public static string Dash;
        public static string Attack;
        public static string Idle;
        public static string DisplayKeySetting;
        public static string MenuOk;
        public static string MenuCancel;
        public static string ReferOption;
        public static string ItemSort;
        public static string MultipleItemSelection;
        public static string DirectionPadUp;
        public static string DirectionPadLeft;

        public static string KeyDescription = "";
        public static string KeyDescriptionDPad = "";

        #region 警告
        public static string HungryCaution = "<color=" + Color.Caution + ">おなかが減ってきた！</color>";
#endregion 警告
        public static string HungryDanger = "<color=" + Color.Danger + ">おなかが減って倒れそう！</color>";
        public static string DriveFull = "このDriveは容量がパンパンだ！";
        public static string RemoveEquipItem = "{0}を外した";
        public static string NotDrive = "このアイテムは入れられない！";
        public static string ItemAdhesiveNotRemove = "{0}はくっついて外せない！";
        public static string ItemAdhesive = "{0}は手にくっついた！";
        public static string ItemDeleteNotSelect = "「{0}」キーで破棄するアイテムを選択してください。";
        public static string ItemDeleteNotSelectUseMouse = "「{0}」キーかダブルクリックで破棄するアイテムを選択してください。";
        public static string ItemDelete = "アイテムをまとめて破棄した！";
        public static string ItemAnalyse = "{0}は{1}だった！";
        public static string ItemLuckAnalyseMaterial = "鑑定をするには砂鉄が必要だ！";
        public static string DriveBug = "このDriveはバグっている！";
        public static string PutinAction = "{0}を{1}に入れた";
        public static string ThrowAction = "{0}を投げた！";
        public static string DropItem = "{0}は床に落ちた";
        public static string BreakItem = "{0}は壊れてしまった";
        public static string VanishItem = "{0}は無限の彼方に消え去った";
        public static string WaterInItem = "{0}は水底に沈んだ";
        public static string RecoverPower = "力が湧いてきた！";
        public static string IncreaseHp = "体力が上がった！";
        public static string RecoverHp = "{0}のHPが回復した！";
        public static string RecoverSatiety = "おなかが膨れた！";
        public static string RecoverSatietyMax = "おなかがいっぱいになった！";
        public static string RecoverState = "{0}が治った！";
        public static string BadFood = "この世のものとは思えない腐臭が{0}を襲う！";
        public static string AddDark = "{0}は視界を奪われた！";
        public static string AddState = "{0}は{1}にかかった！";
        public static string AddDecoy = "{0}はデコイに変化した！";
        public static string AddPalalysis = "{0}は体が動かない！";
        public static string AddSlow = "{0}は速度が遅くなった！";
        public static string AddConf = "{0}は混乱した！"; 
        public static string AddReticent = "{0}は風邪をひいた！"; 
        public static string AddSleep = "{0}は眠ってしまった！";
        public static string AddShoulder = "{0}は肩がこってしまった！";
        public static string AddAcceleration = "{0}は速度が速くなった！";
        public static string ThrowOffItem = "{0}は{1}を落とした！";
        public static string UseItem = "{0}を使った";
        public static string EatItem = "{0}を食べた";
        public static string BlowCharacter = "{0}は吹っ飛んだ";
        public static string EnemyCamouflage = "アイテムは擬態した{0}だった！";
        public static string EnemyExplosion = "{0}の自爆！";
        public static string EnemyTornado = "{0}は激しく羽ばたいた！";
        public static string EnemyTornado2 = "部屋内の位置が入れ替わった！";
        public static string EnemyRecover1 = "{0}は{1}の傷を舐めた";
        public static string EnemyRecover2 = "{0}のHPが回復した！";
        public static string EnemyAngry = "{0}は激怒した！";
        public static string EnemyRabbitDeathBlow = "{0}ののしかかり！";
        public static string EnemyPlantDeathBlow = "{0}のメテオストーム！";
        public static string EnemyAlarm1 = "{0}は警報を鳴らした！";
        public static string EnemyAlarm2 = "{0}が駆けつけた！";
        public static string EnemyShorten = "{0}の縮地！";
        public static string EnemyThrow = "{0}は{1}を投げた！";
        public static string EnemyAwakening = "{0}の覚醒！";
        public static string EnemySwallowing1 = "{0}の丸呑み！";
        public static string EnemySwallowing2 = "{0}が飲み込まれた！";
        public static string EnemyForgetMap = "{0}の見えざる手！マップを忘れてしまった！";
        public static string ChangePoint = "{0}と場所が入れ替わった";
        public static string EquipItem = "{0}を装備した";
        public static string GetItem = "{0}を拾った";
        public static string OnItem = "{0}に乗った";
        public static string FootNoneObject = "足元には何も無い！";
        public static string FailGetItem = "持ち物がいっぱいだ！";
        public static string MelodySing = "{0}を奏でた！";
        public static string HitThrowAfter = "{0}に当たった！";
        public static string AttackMessage = "{0}に{1}のダメージ！";
        public static string AttackEnemy = "{1}に{2}のダメージ！";
        public static string AttackPlayer = "{1}は{2}のダメージ！";
        public static string AttackMissPlayer = "{0}は攻撃を回避！";
        public static string AttackMissEnemy = "攻撃が外れた！";
        public static string DeathPlayer = "{0}はやられてしまった！";
        public static string DeathEnemy = "{0}は倒れた！{1}の経験値を獲得！";
        public static string DeathCommon = "{0}は倒れた！";
        public static string DeathKiln = "窯が壊れてしまった！";
        public static string LevelUpPlayer = "{0}はレベルアップ！";
        public static string LevelDownPlayer = "{0}はレベルダウン！";
        public static string EmeryCharacter = "{0}はフロアから退場させられた！";
        public static string Division = "{0}は分裂した";
        public static string Sacrifice = "{0}を犠牲に復活した！";
        public static string DamagePower = "{0}は力を奪われた！";
        public static string StealItem = "{0}は{1}を盗んだ！";
        public static string RunAmway = "{0}は逃亡した！";
        public static string ForgetMap = "マップを忘れてしまった！";
        public static string ThrowAway = "{0}は物が拾えなくなった！";
        public static string ShedEquipItem = "{0}を弾き飛ばされた！";
        public static string TrapSetBomb = "{0}は{1}を埋設した";
        public static string DexTrap = "トラップは不発に終わった！";
        public static string TrapCommon = "{0}に踏み込んでしまった！";
        public static string TrapBreak = "{0}が破壊された！";
        public static string TrapSong = "どこからともなく子守歌が聴こえてきた！";
        public static string TrapSong2 = "{0}は眠ってしまった！";
        public static string TrapElectric = "突如、{0}に電流走る・・・！";
        public static string TrapEquipRemove = "{0}は装備を手放してしまった！";
        public static string TrapColor = "極彩色のモニターが輝いた！";
        public static string TrapCyclone = "局地的竜巻が発生した！";
        public static string TrapCyclone2 = "{0}は上階に吹き飛ばされた！";
        public static string TrapBigBang = "地雷を踏んでしまった！";
        public static string TrapCroquette = "鮮明なカレーコロッケの写真が落ちている";
        public static string TrapCroquette2 = "{0}はおなかがすいてしまった！";
        public static string TrapSmoke = "床から神経ガスが噴き出てきた！";
        public static string TrapSummon = "敵が召喚された！";
        public static string TrapRote = "回転床が回りだした！";
        public static string TrapSandStorm = "突如砂嵐が巻き起こった！";
        public static string TrapFly = "ハエのたかり場に突っ込んでしまった！";
        public static string TrapFly2 = "持っていたコロッケにハエがたかった！";
        public static string TrapPllen = "{0}の周辺に花粉が舞い散る！";
        public static string TrapPllen2 = "{0}は力が抜けてしまった！";
        public static string TrapEmber = "床に火が付いた！";
        public static string TrapDamage = "{0}は{1}により{2}のダメージ！";
        public static string TrapRecover = "{0}は{1}により{2}回復！";
        public static string TrapWaterBucket = "空から水の入ったバケツが！";
        public static string DeathBlowNotPower = "必殺技を撃つにはパワーが足りない！";
        public static string DeathBlowKugeltanz = "{0}の必殺クーゲルタンズ！";
        public static string DeathBlowMagicalRecipe = "{0}の不思議なレシピ！";
        public static string DeathBlowMagicalRecipe2 = "次回の調合に必ず成功する！";
        public static string AtelierNotCreate = "それを作るには素材が足りない";
        public static string AtelierMaxItem = "それを作るには持ち物の空きが足りない";
        public static string AtelierMaxMaterial = "必要数以上の素材は選択できません";
        public static string AtelierMaterialNotRes1 = "選択した素材の数が足りません。";
        public static string AtelierMaterialNotRes2 = "「{0}」キーで素材を選択してください。";
        public static string AtelierMaterialNotRes2UseMouse = "「{0}」キーかダブルクリックで素材を選択してください。";
        public static string AtelierFail = "調合に失敗した！";
        public static string AtelierSuccess = "調合に成功した！";
        public static string NotThrow = "{0}は肩こりで物が投げられない！";
        public static string NotSing = "{0}はかぜで声が出ない！";

    }
    public class DeathMessage
    {
        public static void SetJp()
        {
            Attack = "{0}の攻撃";
            Hunger = "飢餓";
            Item = "{0}によるダメージ";
            Explosion = "{0}の自爆";
            BodySlam = "{0}ののしかかり";
            MeteorStorm = "{0}のメテオストーム";
            Kugeltanz = "{0}のクーゲルタンズ";
            Trap = "{0}によるダメージ";
            Throw = "{0}の投擲ダメージ";
            Blow = "吹き飛ばしによるダメージ";
        }

        public static void SetEn()
        {
            Attack = "Attack of {0}";
            Hunger = "Hunger";
            Item = "Damage by {0}";
            Explosion = "{0} suicide bombing";
            BodySlam = "{0}'s body press";
            MeteorStorm = "{0}'s Meteorostom";
            Kugeltanz = "{0}'s Kugeltanz";
            Trap = "Damage by {0}";
            Throw = "{0}'s Throw Damage";
            Blow = "Damage to blow-off";
        }

        public static string Attack = "{0}の攻撃";
        public static string Hunger = "飢餓";
        public static string Item = "{0}によるダメージ";
        public static string Explosion = "{0}の自爆";
        public static string BodySlam = "{0}ののしかかり";
        public static string MeteorStorm = "{0}のメテオストーム";
        public static string Kugeltanz = "{0}のクーゲルタンズ";
        public static string Trap = "{0}によるダメージ";
        public static string Throw = "{0}の投擲ダメージ";
        public static string Blow = "吹き飛ばしによるダメージ";
    }

    public class Format
    {
        public const string PerNumber = "{0:0} / {1:0}";
        public const string DefaultName = "{0}{1}{2}";
        public const string DefaultNameWithColor = "<color={3}>{0}{1}{2}</color>";
        public const string UnknownNameWithColor = "<color={1}>{0}</color>";
        public const string CountPerItemName = "{0} ({1:0}/{2:0})";
        public const string CountPerItemNameWithColor = "<color={3}>{0} ({1:0}/{2:0})</color>";
        public const string CountItemName = "{0} ({1:0})";
        public const string CountItemNameWithColor = "<color={2}>{0} ({1:0})</color>";
        public const string StrengthItemName = "{0} (+{1:0})";
        public const string StrengthItemNameWithColor = "<color={2}>{0} (+{1:0})</color>";
        public const string PriceItemName = "{0} ({1:0})";
        public const string PluginName = "{0} ({1:0}/{2:0})";
    }

    public class Key
    {

        public const string StartKey = "54458CA494114A4DA4FB28A1E581E58A5CE84C0EFFFC4AAFB6CC11309D1F8754";
        public const string StartExeKey = "9951601E56214E6FBE397A01630E04C0B01686FB0700497BA77FF08B7B589877";
    }

    public class CryptKey
    {
        public const string StartOTP = "E0DB47CC267342F39152F7E1E05B9CF7E927198CE9404023A7E12B312A51A3AA";
        public const string StartCrypt = "DF86A52CA0B241FC96878198780EA84AEACE04883B8443C784288569A8A3C1B7";
        public const string BGMFiles = "BE777141BF6A4E22AE979D431015CA93";
        public const string OTP = "7363A7302CD9442887707C385FDDF935";
        public const string SCOTP = "262030E4189844B1B994893B6CE92C4C";
        public const string DHOTP = "05DED8A8B1C74F1B97EE1093DF3FBFD6";
        public const string SDOTP = "3452B4D98651444FBBEC66ADE7444580";
        public const string VDOTP = "376EAAC7B2D94458BDBEF7F9095FAFCB";
        public const string ABOTP = "99A1145E278E482F95EB54C9BD6D3CE0";
        public const string KeyControlKey = "1FF26BAB1A364B439E1E9F684C370A93";
        public const string SystemValueKey = "2E91CDC76E534C2BB14CC9003011EA68";
        public const string SaveItemKey = "E96DABA9915F401CA3FB551E59B10EA8";
        public const string SaveItemWarehouseKey = "1B26EDD23C214EE58E57FA2CFD497A82";
        public const string SavePlayingKey = "21D93580AD844023B2D6DC8FCA58394B";
        public const string ErrorLog = "4B0F7E6BC31A48B4B2A593E827BF71B7";
        public const string GetNews = "22778C01539C44EB8178F00EFE319217";
        public const string DecNews = "C408DF1A25BC435E821253012D54226B";
    }
}
