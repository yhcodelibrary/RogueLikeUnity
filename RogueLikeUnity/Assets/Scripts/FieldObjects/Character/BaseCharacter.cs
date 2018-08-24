using Assets.Scripts.Models.Save;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseCharacter : BaseObject
{
    #region ステータス関連値
    /// <summary>
    /// １ターンで動ける回数
    /// </summary>
    public float ActionTurn = 1;

    /// <summary>
    /// 現在のターンで動ける残り回数
    /// </summary>
    public float RestActionTurn = 1;

    /// <summary>
    /// １ターンで動ける回数(初期値)
    /// </summary>
    public float DefaultActionTurn = 1;

    /// <summary>
    /// Lv
    /// </summary>
    public int Level;

    /// <summary>
    /// 種族
    /// </summary>
    public int Race;

    /// <summary>
    /// 現在の経験値
    /// </summary>
    public int CurrentExperience;

    /// <summary>
    /// 次のレベルまでの経験値
    /// </summary>
    public int NextLevelExperience;

    /// <summary>
    /// 倒したときに得る経験値
    /// </summary>
    public int HaveExperience;

    /// <summary>
    /// HP
    /// </summary>
    public float CurrentHp;

    /// <summary>
    /// HPの最大値
    /// </summary>
    public float MaxHpDefault;

    /// <summary>
    /// HPの最大値
    /// </summary>
    public float MaxHp
    {
        get
        {
            return MaxHpDefault + MaxHpCorrection + MaxHpOption;
        }
    }

    public long HaveDollar;

    private float _MaxHpCorrection;
    /// <summary>
    /// HPの最大値の補正値
    /// </summary>
    public float MaxHpCorrection
    {
        get
        {
            return _MaxHpCorrection;
        }
        set
        {
            _MaxHpCorrection = value;
            if (CurrentHp > MaxHp)
            {
                CurrentHp = MaxHp;
            }
        }
    }

    private float _MaxHpOption;
    /// <summary>
    /// HPの最大値の補正値(Option)
    /// </summary>
    public float MaxHpOption
    {
        get
        {
            return _MaxHpOption;
        }
        set
        {
            _MaxHpOption = value;
            if (CurrentHp > MaxHp)
            {
                CurrentHp = MaxHp;
            }
        }
    }

    /// <summary>
    /// ターン回復HP
    /// </summary>
    private float _TrunRecoverHp;
    /// <summary>
    /// ターン回復HP
    /// </summary>
    public float TrunRecoverHp
    {
        get
        {
            float cor = 0;
            foreach (BaseOption op in this.Options)
            {
                cor += op.TurnRecoverHp(_TrunRecoverHp);
            }
            return _TrunRecoverHp + cor;
        }
        set
        {
            _TrunRecoverHp = value;
        }
    }

    /// <summary>
    /// 衝突ダメージ
    /// </summary>
    public int BlowAfterDamage;

    /// <summary>
    /// 基本攻撃力
    /// </summary>
    public float BaseAttack;

    /// <summary>
    /// 基本防御力
    /// </summary>
    public float BaseDefense;

    /// <summary>
    /// 防御力
    /// </summary>
    public virtual float Defense
    {
        get
        {
            return BaseDefense;
        }
    }

    public BaseOption[] Options
    {
        get
        {
            BaseOption[] arr1 = EquipWeapon.Options.ToArray();
            BaseOption[] arr2 = EquipShield.Options.ToArray();
            BaseOption[] mergedArray = new BaseOption[arr1.Length + arr2.Length];
            arr1.CopyTo(mergedArray, 0);
            arr2.CopyTo(mergedArray, arr1.Length);

            return mergedArray;

            //Dictionary<Guid, BaseOption> list = EquipWeapon.Options.Union(EquipShield.Options)
            //    .GroupBy(kvp => kvp.Key)
            //    .Select(grp => grp.First())
            //    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            //return list;
        }
    }

    /// <summary>
    /// ちから最大値
    /// </summary>
    public ushort PowerMax;

    /// <summary>
    /// ちから現在値
    /// </summary>
    public ushort PowerValue;

    /// <summary>
    /// 命中率(0-1)
    /// </summary>
    public float Dexterity;

    /// <summary>
    /// 回避率(0-1)
    /// </summary>
    public float Evasion;

    /// <summary>
    /// 状態異常(2項演算で保持)
    /// </summary>
    public int CharacterAbnormalState;

    /// <summary>
    /// 状態異常後に経過したターン
    /// </summary>
    public Dictionary<StateAbnormal, int> AbnormalStateTurn;

    #endregion ステータス関連値
    #region 制御関連

    public int HaveScore;

    public virtual ushort ThrowRange
    {
        get
        {
            return CommonConst.SystemValue.ThrowDistance;
        }
    }

    /// <summary>
    /// 移動判定終了までの遊び
    /// </summary>
    protected sbyte _endMoveCount;

    /// <summary>
    /// 移動タイプ
    /// </summary>
    public EnemyMove MoveType;

    /// <summary>
    /// 行動状態
    /// </summary>
    public EnemySearchState MoveState;

    /// <summary>
    /// どのエリアに向かうつもりか
    /// </summary>
    public AreaDirection GotoArea;

    /// <summary>
    /// 次のオブジェクトが攻撃を開始していいか
    /// </summary>
    public bool IsNextAttack;

    public bool IsSpecial = false;

    public AttackInformation AttackInfo;

    public GameObject EquipLeft;
    public GameObject EquipRight;

    protected delegate void DelegateSpecificAction(ManageDungeon dun);
    protected DelegateSpecificAction DelegeteAction;

    public Vector3 MoveTargetPoint;

    #endregion 制御関連

    // アニメーター各ステートへの参照
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int locoState = Animator.StringToHash("Base Layer.Locomotion");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    static int attackState = Animator.StringToHash("Base Layer.Attack");
    static int deathBlowState = Animator.StringToHash("Base Layer.DeathBlow");
    static int throwState = Animator.StringToHash("Base Layer.Throw");
    static int singState = Animator.StringToHash("Base Layer.Sing");
    static int deathState = Animator.StringToHash("Base Layer.Death");


    public override CharacterDirection Direction
    {
        get
        {
            return _Direction;
        }
        set
        {
            if (value == CharacterDirection.Match)
            {
                return;
            }
            if (_Direction == value)
            {
                return;
            }

            _Direction = value;
            if (UnityEngine.Object.Equals(null, ThisDisplayObject) == true)
            {
                return;
            }

            switch (_Direction)
            {
                //上
                case CharacterDirection.Top:
                    ThisTransform.Find("AbnormalImage").localEulerAngles = CommonFunction.EulerAngles[CharacterDirection.Bottom];
                    break;
                //左上
                case CharacterDirection.TopLeft:
                    ThisTransform.Find("AbnormalImage").localEulerAngles = CommonFunction.EulerAngles[CharacterDirection.BottomLeft];
                    break;
                //左
                case CharacterDirection.Left:
                    ThisTransform.Find("AbnormalImage").localEulerAngles = CommonFunction.EulerAngles[CharacterDirection.Left];
                    break;
                //左下
                case CharacterDirection.BottomLeft:
                    ThisTransform.Find("AbnormalImage").localEulerAngles = CommonFunction.EulerAngles[CharacterDirection.TopLeft];
                    break;
                //下
                case CharacterDirection.Bottom:
                    ThisTransform.Find("AbnormalImage").localEulerAngles = CommonFunction.EulerAngles[CharacterDirection.Top];
                    break;
                //右下
                case CharacterDirection.BottomRight:
                    ThisTransform.Find("AbnormalImage").localEulerAngles = CommonFunction.EulerAngles[CharacterDirection.TopRight];
                    break;
                //右
                case CharacterDirection.Right:
                    ThisTransform.Find("AbnormalImage").localEulerAngles = CommonFunction.EulerAngles[CharacterDirection.Right];
                    break;
                //右上
                case CharacterDirection.TopRight:
                    ThisTransform.Find("AbnormalImage").localEulerAngles = CommonFunction.EulerAngles[CharacterDirection.BottomRight];
                    break;
                default:
                    //CommonImage.transform.localEulerAngles = new Vector3(CommonConst.Rotation.None, CommonConst.Rotation.None, CommonConst.Rotation.None);
                    break;
            }
        }
    }

    /// <summary>
    /// 現在の行動の状態
    /// </summary>
    private ActionType _ActType;
    public ActionType ActType
    {
        get
        {
            return _ActType;
        }
        set
        {
            _ActType = value;
            if (value == ActionType.Attack ||
                value == ActionType.Blow ||
                value == ActionType.Throw ||
                value == ActionType.DeathBlow ||
                value == ActionType.Sing ||
                value == ActionType.Special)
            {
                _isEndAnima = false;
            }
        }
    }

    /// <summary>
    /// 死んだか
    /// </summary>
    public bool IsDeath;

    /// <summary>
    /// 攻撃モーションの終了許可
    /// </summary>
    protected bool _isEndAnima;


    public override void ErrorInitialize()
    {
        base.ErrorInitialize();
        CharacterAbnormalState = (int)StateAbnormal.Normal;
        MoveSpeedDefault = CommonConst.SystemValue.MoveSpeedDefault;
        AttackInfo = new AttackInformation();
        BlowAfterDamage = 0;
        RestActionTurn = 0;
        HaveDollar = 0;
        IsSpecial = false;
        IsDeath = false;
        MoveTargetPoint = CommonConst.SystemValue.VecterDammy;
        Direction = CharacterDirection.Bottom;
        ChangeDirection(Direction);

    }

    /// <summary>
    /// オブジェクト初期化処理 
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        CharacterAbnormalState = (int)StateAbnormal.Normal;
        MoveSpeedDefault = CommonConst.SystemValue.MoveSpeedDefault;
        AbnormalStateTurn = new Dictionary<StateAbnormal, int>(new StateAbnormalComparer());
        AbnormalStateTurn.Add(StateAbnormal.Charmed, 0);
        AbnormalStateTurn.Add(StateAbnormal.Confusion, 0);
        AbnormalStateTurn.Add(StateAbnormal.Dark, 0);
        AbnormalStateTurn.Add(StateAbnormal.Decoy, 0);
        AbnormalStateTurn.Add(StateAbnormal.DeadlyPoison, 0);
        AbnormalStateTurn.Add(StateAbnormal.Normal, 0);
        AbnormalStateTurn.Add(StateAbnormal.Palalysis, 0);
        AbnormalStateTurn.Add(StateAbnormal.Poison, 0);
        AbnormalStateTurn.Add(StateAbnormal.Sleep, 0);
        AbnormalStateTurn.Add(StateAbnormal.Slow, 0);
        AbnormalStateTurn.Add(StateAbnormal.Reticent, 0);
        AbnormalStateTurn.Add(StateAbnormal.StiffShoulder, 0);
        AttackInfo = new AttackInformation();
        BlowAfterDamage = 0;
        RestActionTurn = 0;
        HaveDollar = 0;
        IsSpecial = false;
        IsDeath = false;
        MoveTargetPoint = CommonConst.SystemValue.VecterDammy;
    }

    /// <summary>
    /// オブジェクトの配置
    /// </summary>
    public override void SetPosition(int x, int y)
    {
        base.SetPosition(x, y);
    }

    /// <summary>
    /// キャラクターのステータス設定
    /// </summary>
    public virtual void SetFirstStatus(SavePlayingInformation save)
    {

    }

    public virtual void CheckNextLevel(AttackInformation atinf)
    {
    }
    /// <summary>
    /// ターン終了の初期化
    /// </summary>
    public virtual void FinishTurn(ManageDungeon dun)
    {
        RestActionTurn += ActionTurn;
        ActType = ActionType.None;
        AttackInfo.Clear();

        foreach (BaseOption o in Options)
        {
            o.FinishTurn(this);
        }

        //状態異常の継続処理
        CheckTurnAbnormal(dun);
    }

    /// <summary>
    /// アニメーションの初期化
    /// </summary>
    public virtual void UpdateIdleStatus()
    {

        SetAnimator();

        ThisAnimator.SetSpeed(CommonConst.SystemValue.AnimationSpeedDefault);

        ThisAnimator.SetBool(AnimationType.IsMove, false);

        MoveSpeed = MoveSpeedDefault;
    }


    /// <summary>
    /// 吹き飛ばし
    /// </summary>
    public virtual bool Blow(ManageDungeon dun)
    {
        if (CommonFunction.IsNull(BlowPoint) == true)
        {
            return false;
        }

        //目標位置の取得
        Vector3 target = new Vector3(BlowPoint.X * PositionUnit,
            PosY,
            BlowPoint.Y * PositionUnit);

        //目標地点に到着したら終了
        if (Vector3.Distance(ThisTransform.localPosition, target) <= MinDistance)
        {
            //水の上だったら近くの陸地に強制移動
            if (dun.Dungeon.DungeonMap.Get(CurrentPoint).State == LoadStatus.Water)
            {
                MapPoint newp = dun.GetNearFloor(CurrentPoint);
                this.SetPosition(newp.X, newp.Y);

                EffectSmoke.CreateObject(this, true).Play();

                SoundInformation.Sound.Play(SoundInformation.SoundType.Throw);
            }

            ResetObjectPosition();
            MoveSpeed = MoveSpeedDefault;
            BlowPoint = null;
            if (BlowAfterDamage > 0)
            {
                AttackState at = AddDamage(BlowAfterDamage);

                //ダメージエフェクト
                EffectDamage d = EffectDamage.CreateObject(this);
                d.SetText(BlowAfterDamage.ToString(), AttackState.Hit);
                d.Play();

                EffetHitShockWave.CreateObject(this).Play();

                SoundInformation.Sound.Play(SoundInformation.SoundType.AttackHit);

                //ヒットメッセージ
                DisplayInformation.Info.AddMessage(
                    this.GetMessageAttackHit(BlowAfterDamage));
                BlowAfterDamage = 0;

                if(at == AttackState.Death)
                {
                    if(Type == ObjectType.Player)
                    {

                        ScoreInformation.Info.CauseDeath =
                            CommonConst.DeathMessage.Blow;

                        ScoreInformation.Info.CauseDeathType = DeathCouseType.Item;
                    }
                    this.Death();
                    this.DeathAction(dun);
                }
            }
            return false;
        }

        //移動単位の取得
        Vector3 velocity = (target - ThisTransform.localPosition).normalized;

        //移動方向と移動単位が一致するか調べる
        if (CommonFunction.CheckDirectionVector(BlowDirection, velocity) == true)
        {
            //一致していれば通常の移動操作

            //キャラクターを目標に移動
            //ThisDisplayObject.transform.localPosition += (velocity * MoveSpeed);
            //ThisDisplayObject.transform.localPosition += CommonFunction.GetVelocity(velocity, MoveSpeed);
            ThisTransform.Translate(CommonFunction.GetVelocity(velocity, MoveSpeed), Space.World);
            //CharacterController ctr = ThisDisplayObject.GetComponent<CharacterController>();
            //ctr.Move(velocity * MoveSpeed);
        }
        else
        {
            //一致していなければ目標地点を追い越したとみなす
            //強制的にキャラポジを移動させて終了
            ThisTransform.localPosition = target;
        }
        return true;
    }


    private sbyte MoveChangeWait = 2;
    //public bool IsMove;
    /// <summary>
    /// フレーム間アクションの実行
    /// </summary>
    //public bool Move()
    public virtual bool Move(ManageDungeon dun, bool isNextAttack, out bool isSpecial, MapPoint plcurrent)
    {
        isSpecial = false;

        if (IsDeath == true)
        {
            //攻撃情報が残っていれば画面に出す
            if (isNextAttack == true)
            {
                AttackInfo.AttackUpdate(this, dun);
            }
            return false;
        }
        SetAnimator();
        IsNextAttack = true;

        //移動中でなければ終了
        if (ActType == ActionType.None)
        {
            //一定時間の遊びのあと移動をオフに
            if (MoveChangeWait <= 0)
            {
                ThisAnimator.SetBool(AnimationType.IsMove, false);
            }
            else
            {
                MoveChangeWait--;
            }
            MoveTargetPoint = CommonConst.SystemValue.VecterDammy;

            return false;
        }
        else if (ActType == ActionType.Attack)
        {
            //攻撃許可が出ていなければキャンセルする
            if (isNextAttack == false)
            {
                return true;
            }
            //攻撃情報が残っていれば画面に出す
            AttackInfo.AttackUpdate(this, dun);

            //アニメーションの実行判定
            if (CheckNotAction(plcurrent) == true)
            {
                SetNotAction(AnimationType.IsAttack);
                return false;
            }

            //最初の一回だけアクション
            if (ThisAnimator.GetBool(AnimationType.IsAttack) == false)
            {
                ThisAnimator.SetBool(AnimationType.IsMove, false);
                ThisAnimator.SetBool(AnimationType.IsAttack, true);
            }
            // 参照用のステート変数にBase Layer (0)の現在のステートを設定する
            AnimatorStateInfo currentBaseState = ThisAnimator.GetCurrentAnimatorStateInfo();

            //終了許可後にステートメントが変わったら終了とする
            if (_isEndAnima == true
                && currentBaseState.fullPathHash != attackState)
            {
                //オブジェクトの位置を補正
                PositionCorrection();
                ThisAnimator.SetBool(AnimationType.IsAttack, false);
                ActType = ActionType.None;
                return false;
            }

            //ステージが一定以下なら攻撃をキャンセル
            if (currentBaseState.normalizedTime < 0.5
                || currentBaseState.fullPathHash == idleState)
            {
                IsNextAttack = false;
            }

            // ステートが後半になったら終了許可を出す
            if (currentBaseState.normalizedTime > 0.8
            && currentBaseState.fullPathHash == attackState)
            {
                _isEndAnima = true;
            }

            return true;
        }
        else if (ActType == ActionType.DeathBlow)
        {
            //攻撃許可が出ていなければキャンセルする
            if (isNextAttack == false)
            {
                return true;
            }
            //攻撃情報が残っていれば画面に出す
            AttackInfo.AttackUpdate(this, dun);

            //アニメーションの実行判定
            if (CheckNotAction(plcurrent) == true)
            {
                SetNotAction(AnimationType.IsDeathBlow);
                return false;
            }

            //攻撃が終わるまで次の行動はさせない
            IsNextAttack = false;

            //最初の一回だけアクション
            if (ThisAnimator.GetBool(AnimationType.IsDeathBlow) == false)
            {
                ThisAnimator.SetBool(AnimationType.IsMove, false);
                ThisAnimator.SetBool(AnimationType.IsDeathBlow, true);
            }
            // 参照用のステート変数にBase Layer (0)の現在のステートを設定する
            AnimatorStateInfo currentBaseState = ThisAnimator.GetCurrentAnimatorStateInfo();

            //終了許可後にステートメントが変わったら終了とする
            if (_isEndAnima == true
                && currentBaseState.fullPathHash != deathBlowState)
            {
                //オブジェクトの位置を補正
                PositionCorrection();
                ThisAnimator.SetBool(AnimationType.IsDeathBlow, false);
                ActType = ActionType.None;
                return false;
            }


            // ステートが後半になったら終了許可を出す
            if (currentBaseState.normalizedTime > 0.8
                && currentBaseState.fullPathHash == deathBlowState)
            {
                _isEndAnima = true;
            }

            return true;
        }
        else if (ActType == ActionType.Blow)
        {
            //攻撃許可が出ていなければキャンセルする
            if (isNextAttack == false)
            {
                return true;
            }
            //攻撃情報が残っていれば画面に出す
            AttackInfo.AttackUpdate(this, dun);

            if (IsSpecial == true)
            {
                isSpecial = true;
                IsSpecial = false;
            }

            //アニメーションの実行判定
            if (CheckNotAction(plcurrent) == true)
            {
                SetNotAction(AnimationType.IsAttack);
                return false;
            }

            //最初の一回だけアクション
            if (ThisAnimator.GetBool(AnimationType.IsAttack) == false)
            {
                ThisAnimator.SetBool(AnimationType.IsMove, false);
                ThisAnimator.SetBool(AnimationType.IsAttack, true);
            }

            // 参照用のステート変数にBase Layer (0)の現在のステートを設定する
            AnimatorStateInfo currentBaseState = ThisAnimator.GetCurrentAnimatorStateInfo();

            //終了許可後にステートメントが変わったら終了とする
            if (_isEndAnima == true
                && currentBaseState.fullPathHash != attackState)
            {
                //オブジェクトの位置を補正
                PositionCorrection();
                ThisAnimator.SetBool(AnimationType.IsAttack, false);
                ActType = ActionType.None;
                return false;
            }

            //ステージが一定以下なら攻撃をキャンセル
            if (currentBaseState.normalizedTime < 0.5
                || currentBaseState.fullPathHash == idleState)
            {
                IsNextAttack = false;
            }

            // ステートが後半になったら終了許可を出す
            if (currentBaseState.normalizedTime > 0.8
            && currentBaseState.fullPathHash == attackState)
            {
                _isEndAnima = true;
            }

            return true;
        }
        else if (ActType == ActionType.Special)
        {
            //攻撃許可が出ていなければキャンセルする
            if (isNextAttack == false)
            {
                return true;
            }
            //攻撃情報が残っていれば画面に出す
            AttackInfo.AttackUpdate(this, dun);

            if (IsSpecial == true)
            {
                isSpecial = true;
                IsSpecial = false;
            }

            //アニメーションの実行判定
            if (CheckNotAction(plcurrent) == true)
            {
                if (CommonFunction.IsNull(DelegeteAction) == false)
                {
                    DelegeteAction(dun);
                    DelegeteAction = null;
                }

                SetNotAction(ThisAnimator.SpecialMove);
                return false;
            }

            //最初の一回だけアクション
            if (ThisAnimator.GetBool(ThisAnimator.SpecialMove) == false)
            {

                if (CommonFunction.IsNull(DelegeteAction) == false)
                {
                    DelegeteAction(dun);
                    DelegeteAction = null;
                }

                ThisAnimator.SetBool(AnimationType.IsMove, false);
                ThisAnimator.SetBool(ThisAnimator.SpecialMove, true);
            }

            // 参照用のステート変数にBase Layer (0)の現在のステートを設定する
            AnimatorStateInfo currentBaseState = ThisAnimator.GetCurrentAnimatorStateInfo();

            int animState = attackState;
            if (ThisAnimator.SpecialMove == AnimationType.IsThrow)
            {
                animState = throwState;
            }

            //終了許可後にステートメントが変わったら終了とする
            if (_isEndAnima == true
                && currentBaseState.fullPathHash != animState)
            {
                //オブジェクトの位置を補正
                PositionCorrection();
                ThisAnimator.SetBool(ThisAnimator.SpecialMove, false);
                ActType = ActionType.None;
                return false;
            }

            //ステージが一定以下なら攻撃をキャンセル
            if (currentBaseState.normalizedTime < 0.5
                || currentBaseState.fullPathHash == idleState)
            {
                IsNextAttack = false;
            }

            // ステートが後半になったら終了許可を出す
            if (currentBaseState.normalizedTime > 0.8
            && currentBaseState.fullPathHash == animState)
            {
                _isEndAnima = true;
            }

            return true;
        }
        else if (ActType == ActionType.Throw)
        {
            //攻撃情報が残っていれば画面に出す
            AttackInfo.AttackUpdate(this, dun);

            //アニメーションの実行判定
            if (CheckNotAction(plcurrent) == true)
            {
                SetNotAction(AnimationType.IsThrow);
                return false;
            }

            //最初の一回だけアクション
            if (ThisAnimator.GetBool(AnimationType.IsThrow) == false)
            {
                if (this.Type == ObjectType.Player)
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.Throw);
                }

                ThisAnimator.SetBool(AnimationType.IsMove, false);
                ThisAnimator.SetBool(AnimationType.IsThrow, true);
            }

            // 参照用のステート変数にBase Layer (0)の現在のステートを設定する
            AnimatorStateInfo currentBaseState = ThisAnimator.GetCurrentAnimatorStateInfo();

            //終了許可後にステートメントが変わったら終了とする
            if (_isEndAnima == true
                && currentBaseState.fullPathHash != throwState)
            {
                //オブジェクトの位置を補正
                PositionCorrection();

                ThisAnimator.SetBool(AnimationType.IsThrow, false);
                ActType = ActionType.None;
                return false;
            }
            // ステートが後半になったら終了許可を出す
            if (currentBaseState.normalizedTime > 0.8
                && currentBaseState.fullPathHash == throwState)
            {
                _isEndAnima = true;
            }

            return true;
        }
        else if (ActType == ActionType.Sing)
        {
            //攻撃情報が残っていれば画面に出す
            AttackInfo.AttackUpdate(this, dun);

            //アニメーションの実行判定
            if (CheckNotAction(plcurrent) == true)
            {
                SetNotAction(AnimationType.IsSing);
                return false;
            }

            //最初の一回だけアクション
            if (ThisAnimator.GetBool(AnimationType.IsSing) == false)
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Sing);
                ThisAnimator.SetBool(AnimationType.IsSing, true);
            }


            // 参照用のステート変数にBase Layer (0)の現在のステートを設定する
            AnimatorStateInfo currentBaseState = ThisAnimator.GetCurrentAnimatorStateInfo();

            //終了許可後にステートメントが変わったら終了とする
            if (_isEndAnima == true
                && currentBaseState.fullPathHash != singState)
            {
                //オブジェクトの位置を補正
                PositionCorrection();

                ThisAnimator.SetBool(AnimationType.IsSing, false);
                ActType = ActionType.None;
                return false;
            }
            // ステートが後半になったら終了許可を出す
            if (currentBaseState.normalizedTime > 0.8
                && currentBaseState.fullPathHash == singState)
            {
                _isEndAnima = true;
            }

            return true;
        }
        else if (ActType == ActionType.Move)
        {
            //攻撃許可が出ていなければキャンセルする
            if (isNextAttack == false)
            {
                return true;
            }

            //目標位置の取得
            if (MoveTargetPoint == CommonConst.SystemValue.VecterDammy)
            {
                MoveTargetPoint = new Vector3(CurrentPoint.X * PositionUnit,
                    PosY,
                    CurrentPoint.Y * PositionUnit);
            }

            //アニメーションの実行判定
            if (CheckNotAction(plcurrent) == true)
            {
                ThisTransform.localPosition = MoveTargetPoint;
                SetNotAction(AnimationType.IsMove);
                return false;
            }


            //移動アクション終了の遊び
            MoveChangeWait = 2;

            //目標地点に到着したら終了
            if (Vector3.Distance(ThisTransform.localPosition, MoveTargetPoint) <= MinDistance)
            {
                //anim.SetBool("IsMove", false);
                PositionCorrection();
                ActType = ActionType.None;
                return false;
            }

            //移動単位の取得
            Vector3 velocity = (MoveTargetPoint - ThisTransform.localPosition).normalized;
            //移動アニメーションオフ判定の遊びを入れる
            _endMoveCount = CommonConst.Wait.EndMove;

            //移動方向と移動単位が一致するか調べる
            if (CommonFunction.CheckDirectionVector(Direction, velocity) == true)
            {
                //一致していれば通常の移動操作

                //キャラクターを目標に移動
                //ThisDisplayObject.transform.localPosition += (velocity * (MoveSpeed * (30 * Time.deltaTime)));
                //ThisDisplayObject.transform.localPosition += CommonFunction.GetVelocity(velocity, MoveSpeed);
                ThisTransform.Translate(CommonFunction.GetVelocity(velocity, MoveSpeed), Space.World);
            }
            else
            {
                //一致していなければ目標地点を追い越したとみなす
                //強制的にキャラポジを移動させて終了
                ThisTransform.localPosition = MoveTargetPoint;
            }

            //オブジェクトの位置を補正
            PositionCorrection();

            ThisAnimator.SetBool(AnimationType.IsMove, true);
            return true;
        }

        return false;
        //IsMove = false;
    }

    #region 特殊攻撃

    private AttackInformation _DeathBlowInformation;
    public AttackInformation DeathBlowInformation
    {
        get
        {
            return CommonFunction.GetSingleton<AttackInformation>(ref _DeathBlowInformation);
        }
    }

    protected void DeathBlowKugeltanzBase(ManageDungeon dun)
    {
        //メッセージの追加
        AttackInfo.AddMessage(string.Format(CommonConst.Message.DeathBlowKugeltanz, this.DisplayNameInMessage));


        //オプションを取得
        WeaponBase wep = this.EquipWeapon;
        BaseOption[] atoptions = this.Options;

        //8方向の対象位置を攻撃
        DeathBlowTargetPoint.Clear();
        DeathBlowTargetCharacter.Clear();
        dun.SetUpCharacterMap();

        DeathBlowInformation.Clear();

        DeathBlowKugeltanzDir(dun, CharacterDirection.Top, wep, this.PowerValue, atoptions, DeathBlowInformation);
        DeathBlowKugeltanzDir(dun, CharacterDirection.TopLeft, wep, this.PowerValue, atoptions, DeathBlowInformation);
        DeathBlowKugeltanzDir(dun, CharacterDirection.Left, wep, this.PowerValue, atoptions, DeathBlowInformation);
        DeathBlowKugeltanzDir(dun, CharacterDirection.BottomLeft, wep, this.PowerValue, atoptions, DeathBlowInformation);
        DeathBlowKugeltanzDir(dun, CharacterDirection.Bottom, wep, this.PowerValue, atoptions, DeathBlowInformation);
        DeathBlowKugeltanzDir(dun, CharacterDirection.BottomRight, wep, this.PowerValue, atoptions, DeathBlowInformation);
        DeathBlowKugeltanzDir(dun, CharacterDirection.Right, wep, this.PowerValue, atoptions, DeathBlowInformation);
        DeathBlowKugeltanzDir(dun, CharacterDirection.TopRight, wep, this.PowerValue, atoptions, DeathBlowInformation);
        
    }

    private Dictionary<CharacterDirection, MapPoint> _DeathBlowTargetPoint;
    public Dictionary<CharacterDirection, MapPoint> DeathBlowTargetPoint
    {
        get
        {
            return CommonFunction.GetDictionarySingleton<CharacterDirection, MapPoint, CharacterDirectionComparer>(ref _DeathBlowTargetPoint);
        }
    }
    private Dictionary<CharacterDirection, BaseCharacter> _DeathBlowTargetCharacter;
    public Dictionary<CharacterDirection, BaseCharacter> DeathBlowTargetCharacter
    {
        get
        {
            return CommonFunction.GetDictionarySingleton<CharacterDirection, BaseCharacter, CharacterDirectionComparer>(ref _DeathBlowTargetCharacter);
        }
    }

    protected void DeathBlowKugeltanzDir(ManageDungeon dun, CharacterDirection dir, WeaponBase wep, int power, BaseOption[] atoptions, AttackInformation atinf)
    {
        //弾の到達範囲を設定
        DeathBlowTargetPoint.Add(dir,
            dun.GetHitRangePoint(this.CurrentPoint, dir, 3));

        //到達地点に敵がいた場合
        if (CommonFunction.IsNull(dun.CharacterMap.Get(DeathBlowTargetPoint[dir])) == false)
        {
            BaseCharacter target = dun.CharacterMap.Get(DeathBlowTargetPoint[dir]);

            if (CommonFunction.IsRandom(wep.WeaponDexterity) == true)
            {
                BaseOption[] tgoptions = target.Options;

                DeathBlowTargetCharacter.Add(dir, target);

                //与ダメージを計算
                int damage = wep.CalcDamage(dun, this, target, power, atoptions, tgoptions, 5);

                //スコア関連値の更新
                if (target.Type == ObjectType.Enemy)
                {
                    ScoreInformation.Info.AddScore(damage);
                }

                //ダメージ追加
                AttackState atState = CommonFunction.AddDamage(atinf, this, target, damage);

                //エフェクトの追加
                wep.AttackEffect(target, this, damage.ToString(), AttackState.Hit, atinf);

                //対象が死亡したら
                if (atState == AttackState.Death)
                {
                    if(target.Type == ObjectType.Player)
                    {
                        ScoreInformation.Info.CauseDeath =
                            string.Format(CommonConst.DeathMessage.Kugeltanz, DisplayNameNormal);

                        ScoreInformation.Info.CauseDeathType = DeathCouseType.Kugeltanz;
                        ScoreInformation.Info.EnemyObjNo = this.ObjNo;
                    }
                    atinf.AddKillList(target);

                    atinf.AddMessage(
                        target.GetMessageDeath(target.HaveExperience));


                    Death(target, atinf);
                }
            }
            else
            {
                //外れた場合
                atinf.AddHit(target, false);

                EffectDamage d = EffectDamage.CreateObject(target);
                d.SetText("Miss", AttackState.Miss);
                atinf.AddEffect(d);

                DeathBlowTargetCharacter.Add(dir, null);

                atinf.AddMessage(
                    target.GetMessageAttackMiss());
            }
        }
        else
        {
            DeathBlowTargetCharacter.Add(dir, null);
        }
    }
    #endregion 特殊攻撃


    protected virtual void ShedFirstAction(ManageDungeon dun)
    {

    }
    protected virtual void ThrowFirstAction(ManageDungeon dun)
    {

    }

    private void SetNotAction(AnimationType t)
    {
        _isEndAnima = true;

        ThisAnimator.SetBool(t, false);

        ActType = ActionType.None;
    }
    
    protected virtual void PositionCorrection()
    {
        Transform tar = ThisTransform.Find("Body");
        if (CommonFunction.IsNull(tar) == false)
        {
            tar.localPosition = Vector3.zero;
        }
    }

    /// <summary>
    /// ダッシュ
    /// </summary>
    public virtual void Dash()
    {
        //移動でなければ終了
        if(ActType != ActionType.Move)
        {
            return;
        }

        //移動の場合は移動速度とアニメーション速度を上げる
        SetAnimator();
        ThisAnimator.SetSpeed(CommonConst.SystemValue.AnimationSpeedDash);
        MoveSpeed = CommonConst.SystemValue.MoveSpeedDash;
    }

    /// <summary>
    /// アイテムに乗った時など
    /// </summary>
    public void DashCancel()
    {
        //キャンセル
        SetAnimator();
        ThisAnimator.SetSpeed(CommonConst.SystemValue.AnimationSpeedDefault);
        MoveSpeed = MoveSpeedDefault;
    }

    #region ターン消費行動

    /// <summary>
    /// 対象の方向に移動可能か調べる
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public bool CheckMove(ManageDungeon dungeon, CharacterDirection dir, bool isChange = true)
    {
        Direction = dir;
        bool check = false;

        //麻痺の場合は移動無し
        if ((CharacterAbnormalState & (int)StateAbnormal.Palalysis) != 0)
        {
            ChangeDirection(Direction);
            RestActionTurn--;
            return check;
        }

        switch (dir)
        {
            //上
            case CharacterDirection.Top:
                check = CheckMoveUDLR(dungeon, CurrentPoint.X, CurrentPoint.Y + 1,
                    dir);
                break;
            //左上
            case CharacterDirection.TopLeft:
                check = CheckMoveSlanting(dungeon, CurrentPoint.X - 1, CurrentPoint.Y + 1, dir,
                    MapPoint.Get(CurrentPoint.X, CurrentPoint.Y + 1), MapPoint.Get(CurrentPoint.X - 1, CurrentPoint.Y));

                break;
            //左
            case CharacterDirection.Left:
                check = CheckMoveUDLR(dungeon, CurrentPoint.X - 1, CurrentPoint.Y,
                    dir);
                break;
            //左下
            case CharacterDirection.BottomLeft:
                check = CheckMoveSlanting(dungeon, CurrentPoint.X - 1, CurrentPoint.Y - 1, dir,
                    MapPoint.Get(CurrentPoint.X, CurrentPoint.Y - 1), MapPoint.Get(CurrentPoint.X - 1, CurrentPoint.Y));

                break;
            //下
            case CharacterDirection.Bottom:
                check = CheckMoveUDLR(dungeon, CurrentPoint.X, CurrentPoint.Y - 1,
                    dir);
                break;
            //右下
            case CharacterDirection.BottomRight:
                check = CheckMoveSlanting(dungeon, CurrentPoint.X + 1, CurrentPoint.Y - 1, dir,
                    MapPoint.Get(CurrentPoint.X, CurrentPoint.Y - 1), MapPoint.Get(CurrentPoint.X + 1, CurrentPoint.Y));

                break;
            //右
            case CharacterDirection.Right:
                check = CheckMoveUDLR(dungeon, CurrentPoint.X + 1, CurrentPoint.Y,
                    dir);
                break;
            //右上
            case CharacterDirection.TopRight:
                check = CheckMoveSlanting(dungeon, CurrentPoint.X + 1, CurrentPoint.Y + 1, dir,
                    MapPoint.Get(CurrentPoint.X, CurrentPoint.Y + 1), MapPoint.Get(CurrentPoint.X + 1, CurrentPoint.Y));
                break;
            default:
                break;
        }

        //行動した場合はターンカウントを更新
        if(check == true)
        {
            //IsMove = true;
            RestActionTurn--;
        }

        return check;
    }
    

    /// <summary>
    /// 攻撃の発動
    /// </summary>
    public BaseCharacter Attack(ManageDungeon dun)
    {
        //混乱していた場合
        if ((CharacterAbnormalState & (int)StateAbnormal.Confusion) != 0)
        {
            //方向を適当に変更
            Direction = CommonFunction.ReverseDirection.Keys.ElementAt(UnityEngine.Random.Range(0, 8));
            ChangeDirection(Direction);
        }

        //現在位置に強制移動（モーションによるフレームずれを補正）
        ResetObjectPosition();

        //Animator anim = ThisDisplayObject.GetComponent<Animator>();
        //anim.SetBool("IsAttack", true);
        ActType = ActionType.Attack;
        //IsMove = true;
        _isEndAnima = false;

        //武器を取得
        WeaponBase weapon = EquipWeapon;

        //攻撃射程対象の一番近くにいるキャラクターを取得
        dun.SetUpCharacterMap();
        BaseCharacter target = dun.GetAttackTargetCharacter(CurrentPoint, weapon.Range, Direction);
        
        //ターンカウントを更新
        RestActionTurn--;

        //キャラクターが取得できたら対象のキャラに攻撃判定を加える
        if (CommonFunction.IsNull(target) == false)
        {
            //攻撃して結果を取得
            AttackState st = weapon.Attack(dun, target, this, PowerValue, AttackInfo);
            
            switch (st)
            {
                case AttackState.Hit:
                    break;
                case AttackState.Miss:
                    break;
                case AttackState.Death:
                    //対象が死んだら死亡フラグ
                    if(target.Type == ObjectType.Player && target.IsDeath == false)
                    {
                        ScoreInformation.Info.CauseDeath =
                            string.Format(CommonConst.DeathMessage.Attack, DisplayNameNormal);
                        ScoreInformation.Info.CauseDeathType = DeathCouseType.Attack;
                        ScoreInformation.Info.EnemyObjNo = this.ObjNo;
                    }
                    Death(target, this.AttackInfo);

                    return target;

                    break;
            }
        }
        //素振りのとき
        else
        {
            if(Type == ObjectType.Player)
            {
                AttackInfo.AddSound(SoundInformation.SoundType.AttackMiss);
            }
        }
        return null;
    }

    /// <summary>
    /// 必殺技の発動
    /// </summary>
    public virtual bool DeathBlow(ManageDungeon dun)
    {
        return true;
    }
    /// <summary>
    /// 足踏み
    /// </summary>
    public void Idle()
    {
        SetAnimator();
        ThisAnimator.SetSpeed(CommonConst.SystemValue.AnimationSpeedIdle);
        //ターンカウントを更新
        RestActionTurn--;
    }

    /// <summary>
    /// アイテム投擲
    /// </summary>
    public void ThrowItem()
    {

        //現在位置に強制移動（モーションによるフレームずれを補正）
        ResetObjectPosition();

        //Animator anim = GetAnimator();
        //anim.SetBool("IsThrow", true);
        ActType = ActionType.Throw;
        //IsMove = true;
        _isEndAnima = false;
        //ターンカウントを更新
        RestActionTurn--;
    }

    /// <summary>
    /// 歌う
    /// </summary>
    public void SingItem()
    {
        //現在位置に強制移動（モーションによるフレームずれを補正）
        ResetObjectPosition();

        //Animator anim = GetAnimator();
        //anim.SetBool("IsSing", true);
        ActType = ActionType.Sing;
        //IsMove = true;
        _isEndAnima = false;
        //ターンカウントを更新
        RestActionTurn--;
    }

    public void Death(BaseCharacter target, AttackInformation atinf)
    {
        target.Death();

        //オプションによる経験値取得
        int corexp = 0;
        foreach(BaseOption o in Options)
        {
            corexp += o.GetExp(target.HaveExperience);
        }
        target.HaveExperience += corexp;

        //レベルアップチェック
        CurrentExperience += target.HaveExperience;
        CheckNextLevel(atinf);

        //スコア値の更新
        if (target.Type == ObjectType.Enemy && this.Type == ObjectType.Player)
        {
            ScoreInformation.Info.AddScore(target.HaveScore);

            DungeonHistoryInformation.Info.iEnemyBastardCount++;
        }
    }

    public void Death()
    {
        VoiceDeath();

        bool revive = false;
        //武器オプションによる復活
        WeaponBase w = EquipWeapon;
        foreach(BaseOption o in w.Options)
        {
            if(o.DeathRevive() == true)
            {
                revive = true;
                break;
            }
        }
        if(revive == true)
        {
            DisplayInformation.Info.AddMessage(
                string.Format(CommonConst.Message.Sacrifice, w.DisplayNameInMessage));

            EffectFlareCore.CreateObject(this);

            this.CurrentHp = this.MaxHp;
            w.ForceRemoveEquip(this);
            PlayerCharacter.RemoveItem(w);
            return;
        }

        //盾オプションによる復活
        ShieldBase s = EquipShield;
        revive = false;
        foreach (BaseOption o in s.Options)
        {
            if (o.DeathRevive() == true)
            {
                revive = true;
                break;
            }
        }
        //オプション効果による復活
        if (revive == true)
        {
            DisplayInformation.Info.AddMessage(
                string.Format(CommonConst.Message.Sacrifice, s.DisplayNameInMessage));

            EffectFlareCore.CreateObject(this);

            this.CurrentHp = this.MaxHp;
            s.ForceRemoveEquip(this);
            PlayerCharacter.RemoveItem(s);
            return;
        }

        SetAnimator();
        ThisAnimator.SetBool(AnimationType.IsAttack, false);
        //this.ActType = ActionType.Death;
        IsDeath = true;
    }
    #endregion ターン消費行動

    #region ステータス更新系

    /// <summary>
    /// HP回復
    /// </summary>
    /// <param name="hp"></param>
    public void RecoverHp(float hp)
    {
        //最大値を超える場合は最大値まで回復
        if (CurrentHp + hp > MaxHp)
        {
            CurrentHp = MaxHp;
        }
        else
        {
            //超えない場合はそこまで回復
            CurrentHp += hp;
        }
    }


    public void RecoverPower(ushort pow)
    {
        //現在最大値のとき
        if(PowerValue == PowerMax)
        {
            //最大値を上昇
            PowerMax++;
            PowerValue++;
        }
        //最大値を超える場合は最大値まで回復
        if (PowerValue + pow > PowerMax)
        {
            PowerValue = PowerMax;
        }
        else
        {
            //超えない場合はそこまで回復
            PowerValue += pow;
        }
    }
    public bool RecoverPowerNotMaxup(ushort pow)
    {
        //現在最大値のとき
        if (PowerValue == PowerMax)
        {
            //終了
            return false;
        }
        //最大値を超える場合は最大値まで回復
        if (PowerValue + pow > PowerMax)
        {
            PowerValue = PowerMax;
        }
        else
        {
            //超えない場合はそこまで回復
            PowerValue += pow;
        }

        return true;
    }
    public void DamagePower(ushort pow)
    {
        //0以下になる場合は0に
        if (PowerMax - pow <= 0)
        {
            PowerMax = 0;
        }
        else
        {
            //超えない場合はそこまで減少
            PowerMax -= pow;
        }

        if(PowerValue > PowerMax)
        {
            PowerValue = PowerMax;
        }
    }

    public void ReducePower(ushort pow)
    {
        //0以下になる場合は0に
        if (PowerValue - pow <= 0)
        {
            PowerValue = 0;
        }
        else
        {
            //超えない場合はそこまで減少
            PowerValue -= pow;
        }
    }

    public int AddStateAbnormal(StateAbnormal state)
    {
        return AddStateAbnormal((int)state);
    }
    /// <summary>
    /// 状態異常付与
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public int AddStateAbnormal(int state)
    {
        //論理和を取って残ったものは更新対象の異常
        int updateState = CharacterAbnormalState & state;

        //更新対象から差し引いた分が新規のもの
        int newState = state - updateState;
        int resultState = 0;

        //抵抗確率を取得する
        //Dictionary<StateAbnormal, float> prevents = new Dictionary<StateAbnormal, float>();
        //foreach (StateAbnormal val in Enum.GetValues(typeof(StateAbnormal)))
        //{
        //    prevents.Add(val, 0);
        //}
        ////指輪の抵抗値設定
        //this.EquipRing.SetAbnormalPrevent(prevents);

        //オプションの抵抗値設定
        //foreach (BaseOption o in Options.Values)
        //{
        //    o.SetAbnormalPrevent(prevents);
        //}

        RingBase r = this.EquipRing;

        //対象異常の反映処理
        foreach (StateAbnormal val in CommonFunction.StateAbnormals)
        {
            //更新でも新規対象でもなかったら終了
            if((state & (int)val) == 0)
            {
                continue;
            }
            //抵抗値の取得
            float prevent = r.GetAbnormalPrevent(val);

            foreach (BaseOption o in Options)
            {
                prevent = o.GetAbnormalPrevent(val, prevent);
            }

            //抵抗できたら終了
            if (CommonFunction.IsRandom(prevent) == true)
            {
                continue;
            }

            //対象の状態異常を含んでいれば状態を更新する
            if ((updateState & (int)val) != (int)StateAbnormal.Normal)
            {
                AbnormalStateTurn[val] = 0;
                if (Type == ObjectType.Enemy)
                {
                    ScoreInformation.Info.AddScore(10);
                }
            }
            //追加の状態異常があれば追加する
            if ((newState & (int)val) != (int)StateAbnormal.Normal)
            {
                resultState += (int)val;
                if (Type == ObjectType.Enemy)
                {
                    ScoreInformation.Info.AddScore(10);
                }
                AbnormalStateTurn[val] = 0;
                GameObject stp = ThisDisplayObject.transform.Find("AbnormalImage").gameObject;
                GameObject st = null;
                switch (val)
                {
                    case StateAbnormal.Confusion:
                        st = CreateAbnormalInstance("CommonConfusion", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;

                        break;
                    case StateAbnormal.Charmed:
                        st = CreateAbnormalInstance("CommonCharmed", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;

                        break;
                    case StateAbnormal.DeadlyPoison:
                        st = CreateAbnormalInstance("CommonDeadlyPoison", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;
                        break;
                    case StateAbnormal.Palalysis:
                        st = CreateAbnormalInstance("CommonPalalysis", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;
                        break;
                    case StateAbnormal.Poison:
                        st = CreateAbnormalInstance("CommonPoison", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;
                        break;
                    case StateAbnormal.Sleep:
                        st = CreateAbnormalInstance("CommonSleep", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;
                        break;
                    case StateAbnormal.Decoy:

                        GameObject newi = ResourceInformation.GetInstance("EnemyDecoy", true);
                        //GameObject newi = GameObject.Instantiate(ResourceInformation.CommonImage.transform.FindChild("EnemyDecoy").gameObject,
                        //    ResourceInformation.DungeonDynamicObject.transform);
                        List<Transform> temp = new List<Transform>();
                        //他の状態異常を新インスタンスに移す
                        foreach (Transform g in stp.transform)
                        {
                            temp.Add(g);
                        }

                        foreach (Transform g in temp)
                        {
                            g.SetParent(newi.transform.Find("AbnormalImage"));
                            g.localPosition = Vector3.zero;
                            g.localRotation = ResourceInformation.QuaternionZero;
                        }
                        GameObject.Destroy(ThisDisplayObject);
                        ThisDisplayObject = newi;
                        SetPosition(CurrentPoint.X, CurrentPoint.Y);
                        ChangeDirection(Direction);
                        DisplayNameAfter = string.Format("({0})", "デコイ");
                        break;
                    case StateAbnormal.Slow:

                        ActionTurn /= 2;
                        RestActionTurn /= 2;
                        st = CreateAbnormalInstance("CommonSlow", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;

                        break;
                    case StateAbnormal.Acceleration:

                        ActionTurn *= 2;
                        RestActionTurn *= 2;
                        st = CreateAbnormalInstance("CommonAcceleration", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;

                        break;
                    case StateAbnormal.Reticent:
                        st = CreateAbnormalInstance("CommonReticent", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;
                        break;

                    case StateAbnormal.StiffShoulder:
                        st = CreateAbnormalInstance("CommonStiffShoulder", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;
                        break;
                }
            }
        }
        //対象の状態異常を含んでいなければ新規の異常を追加する
        CharacterAbnormalState += resultState;

        return resultState;
    }

    /// <summary>
    /// 状態異常付与
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public int AddForceStateAbnormal(int state)
    {
        //論理和を取って残ったものは更新対象の異常
        int updateState = CharacterAbnormalState & state;

        //更新対象から差し引いた分が新規のもの
        int newState = state - updateState;
        int resultState = 0;


        //対象異常の反映処理
        foreach (StateAbnormal val in CommonFunction.StateAbnormals)
        {
            //更新でも新規対象でもなかったら終了
            if ((state & (int)val) == 0)
            {
                continue;
            }


            //対象の状態異常を含んでいれば状態を更新する
            if ((updateState & (int)val) != (int)StateAbnormal.Normal)
            {
                AbnormalStateTurn[val] = 0;

            }
            //追加の状態異常があれば追加する
            if ((newState & (int)val) != (int)StateAbnormal.Normal)
            {
                resultState += (int)val;

                AbnormalStateTurn[val] = 0;
                GameObject stp = ThisTransform.Find("AbnormalImage").gameObject;
                GameObject st = null;
                switch (val)
                {
                    case StateAbnormal.Confusion:
                        st = CreateAbnormalInstance("CommonConfusion", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;

                        break;
                    case StateAbnormal.Charmed:
                        st = CreateAbnormalInstance("CommonCharmed", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;

                        break;
                    case StateAbnormal.DeadlyPoison:
                        st = CreateAbnormalInstance("CommonDeadlyPoison", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;
                        break;
                    case StateAbnormal.Palalysis:
                        st = CreateAbnormalInstance("CommonPalalysis", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;
                        break;
                    case StateAbnormal.Poison:
                        st = CreateAbnormalInstance("CommonPoison", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;
                        break;
                    case StateAbnormal.Sleep:
                        st = CreateAbnormalInstance("CommonSleep", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;
                        break;
                    case StateAbnormal.Decoy:

                        GameObject newi = ResourceInformation.GetInstance("EnemyDecoy", true);
                        //GameObject newi = GameObject.Instantiate(ResourceInformation.CommonImage.transform.FindChild("EnemyDecoy").gameObject,
                        //    ResourceInformation.DungeonDynamicObject.transform);
                        List<Transform> temp = new List<Transform>();
                        //他の状態異常を新インスタンスに移す
                        foreach (Transform g in stp.transform)
                        {
                            temp.Add(g);
                        }

                        foreach (Transform g in temp)
                        {
                            g.SetParent(newi.transform.Find("AbnormalImage"));
                            g.localPosition = Vector3.zero;
                            g.localRotation = ResourceInformation.QuaternionZero;
                        }
                        GameObject.Destroy(ThisDisplayObject);
                        ThisDisplayObject = newi;
                        SetPosition(CurrentPoint.X, CurrentPoint.Y);
                        ChangeDirection(Direction);
                        DisplayNameAfter = string.Format("({0})", "デコイ");
                        break;
                    case StateAbnormal.Slow:

                        ActionTurn /= 2;
                        RestActionTurn /= 2;
                        st = CreateAbnormalInstance("CommonSlow", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;

                        break;
                    case StateAbnormal.Acceleration:

                        ActionTurn *= 2;
                        RestActionTurn *= 2;
                        st = CreateAbnormalInstance("CommonAcceleration", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;

                        break;
                    case StateAbnormal.Reticent:
                        st = CreateAbnormalInstance("CommonReticent", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;
                        break;

                    case StateAbnormal.StiffShoulder:
                        st = CreateAbnormalInstance("CommonStiffShoulder", stp.transform);
                        st.transform.localPosition = Vector3.zero;
                        st.transform.localRotation = ResourceInformation.QuaternionZero;
                        break;
                }
            }
        }
        //対象の状態異常を含んでいなければ新規の異常を追加する
        CharacterAbnormalState += resultState;

        return resultState;
    }

    private GameObject CreateAbnormalInstance(string name,Transform parent)
    {
        GameObject st = ResourceInformation.GetInstance(name, parent);
        st.name = name;
        return st;
    }

    /// <summary>
    /// ターン事の状態異常判定
    /// </summary>
    /// <returns></returns>
    public bool CheckTurnAbnormal(ManageDungeon dun)
    {

        //対象異常のマークを付与する
        foreach (StateAbnormal val in CommonFunction.StateAbnormals)
        {
            //対象の状態異常を含んでいなければ無視
            if ((CharacterAbnormalState & (int)val) == 0)
            {
                continue;
            }
            //対象を治していいか判定
            else if (TableStateAbnormal.IsRecoverState(val, AbnormalStateTurn[val]) == true)
            {
                AbnormalStateTurn[val] = 0;
                RecoverState((int)val);
                //治ったら
                RemoveAbnormalObject(val);
            }
            //ターン事の効果発動
            else
            {
                AttackState st = AttackState.None;
                switch (val)
                {
                    case StateAbnormal.Confusion:
                        break;
                    case StateAbnormal.Charmed:
                        break;
                    case StateAbnormal.DeadlyPoison:
                        st = AddDamage(5);
                        break;
                    case StateAbnormal.Palalysis:
                        break;
                    case StateAbnormal.Poison:
                        st = AddDamage(1);
                        break;
                    case StateAbnormal.Sleep:
                        break;
                    case StateAbnormal.Decoy:
                        break;
                    case StateAbnormal.Slow:
                        break;
                    case StateAbnormal.Acceleration:
                        break;
                }
                if(Type == ObjectType.Player && st == AttackState.Death)
                {
                    DisplayInformation.Info.AddMessage(GetMessageDeath());
                    ScoreInformation.Info.CauseDeath =
                        string.Format(CommonFunction.StateNames[val]);

                    ScoreInformation.Info.CauseDeathType = DeathCouseType.Status;

                    Death(this, this.AttackInfo);
                    DeathAction(dun);
                }
            }
            AbnormalStateTurn[val]++;
        }

        return true;
    }

    /// <summary>
    /// 状態異常の回復
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public int RecoverState(int state)
    {
        //直したステータス
        int recState = CharacterAbnormalState & state;

        //対象の状態異常を含んでいれば回復する
        if (recState != (int)StateAbnormal.Normal)
        {
            CharacterAbnormalState -= recState;
            
        }
        return recState;
    }

    public void RemoveAbnormalObject(StateAbnormal val)
    {

        GameObject stp = ThisDisplayObject.transform.Find("AbnormalImage").gameObject;
        GameObject st = null;
        switch (val)
        {
            case StateAbnormal.Confusion:
                st = GetImageAbnormal("CommonConfusion", stp.transform);
                break;
            case StateAbnormal.Charmed:
                st = GetImageAbnormal("CommonCharmed", stp.transform);
                break;
            case StateAbnormal.DeadlyPoison:
                st = GetImageAbnormal("CommonDeadlyPoison", stp.transform);
                break;
            case StateAbnormal.Palalysis:
                st = GetImageAbnormal("CommonPalalysis", stp.transform);
                break;
            case StateAbnormal.Poison:
                st = GetImageAbnormal("CommonPoison", stp.transform);
                break;
            case StateAbnormal.Sleep:
                st = GetImageAbnormal("CommonSleep", stp.transform);
                break;
            case StateAbnormal.Decoy:
                //GameObject newi = UnityEngine.Object.Instantiate(ResourceInformation.CommonImage.transform.FindChild(InstanceName).gameObject,
                //    ResourceInformation.DungeonDynamicObject.transform);

                GameObject newi = ResourceInformation.GetInstance(InstanceName, true);

                List<Transform> temp = new List<Transform>();
                //他の状態異常を新インスタンスに移す
                foreach (Transform g in stp.transform)
                {
                    temp.Add(g);
                }

                foreach (Transform g in temp)
                {
                    g.SetParent(newi.transform.Find("AbnormalImage"));
                    g.localPosition = Vector3.zero;
                    g.localRotation = ResourceInformation.QuaternionZero;
                }

                GameObject.Destroy(ThisDisplayObject);
                ThisDisplayObject = newi;
                SetPosition(CurrentPoint.X, CurrentPoint.Y);
                DisplayNameAfter = "";
                ChangeDirection(Direction);
                break;
            case StateAbnormal.Slow:

                if ((CharacterAbnormalState & ((int)StateAbnormal.Acceleration)) == 0)
                {
                    ActionTurn = DefaultActionTurn;
                    RestActionTurn = DefaultActionTurn;
                }
                else
                {
                    ActionTurn *= 2;
                    RestActionTurn *= 2;
                }
                st = GetImageAbnormal("CommonSlow", stp.transform);
                break;
            case StateAbnormal.Acceleration:

                if ((CharacterAbnormalState & ((int)StateAbnormal.Slow)) == 0)
                {
                    ActionTurn = DefaultActionTurn;
                    RestActionTurn = DefaultActionTurn;
                }
                else
                {
                    ActionTurn /= 2;
                    RestActionTurn /= 2;
                }
                st = GetImageAbnormal("CommonAcceleration", stp.transform);
                break;
            case StateAbnormal.Reticent:
                st = GetImageAbnormal("CommonReticent", stp.transform);
                break;
            case StateAbnormal.StiffShoulder:
                st = GetImageAbnormal("CommonStiffShoulder", stp.transform);
                break;
        }
        if (CommonFunction.IsNull(st) == false)
        {
            GameObject.Destroy(st);
        }
    }

    private GameObject GetImageAbnormal(string name,Transform parent)
    {
        Transform tf = parent.Find(name);
        if(CommonFunction.IsNull(tf) == false)
        {
            return tf.gameObject;
        }
        return null;
    }

    /// <summary>
    /// ダメージ更新処理
    /// </summary>
    /// <param name="damage"></param>
    public AttackState AddDamage(int damage)
    {
        //Camera.main.WorldToScreenPoint(_player.transform.localPosition);
        if(damage < 0)
        {
            return AttackState.Hit;
        }
        this.CurrentHp -= damage;
        
        if (this.CurrentHp <= 0)
        {
            this.CurrentHp = 0;
            return AttackState.Death;
        }
        return AttackState.Hit;
    }

    #endregion ステータス更新系

    public virtual void DeathAction(ManageDungeon dun)
    {
        SetAnimator();
        ThisAnimator.SetBool(AnimationType.IsDeath, true);
        ThisAnimator.SetBool(AnimationType.IsMove, false);
        ThisAnimator.SetBool(AnimationType.IsAttack, false);
        ThisAnimator.SetBool(AnimationType.IsSing, false);
        ThisAnimator.SetBool(AnimationType.IsThrow, false);

        if (this.Type == ObjectType.Enemy)
        {

            //GameObject.Destroy(this.ThisDisplayObject, 1);
            ManageDungeon.KillObject(this);
            //UniRx.Observable.FromMicroCoroutine<sbyte>(observer => DestroyObjectCorutine());
        }
        else if(this.Type == ObjectType.Kiln)
        {
            IsActive = false;
        }
    }
    

    /// <summary>
    /// 上下左右への移動チェック（斜めは別）
    /// </summary>
    /// <param name="dungeon"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="rote"></param>
    /// <returns></returns>
    protected bool CheckMoveUDLR(ManageDungeon dungeon, int x,int y, CharacterDirection dir)
    {
        MapPoint target = MapPoint.Get(x, y);
        SetDirection(dir);
        bool isChange = dungeon.CheckCharacterMoveUDLR(target);

        //場所移動可能なら場所替えをする
        if (isChange == true)
        {
            dungeon.MoveCharacter(target, this);
            ActType = ActionType.Move;
        }

        return isChange;
    }

    /// <summary>
    /// 斜めへの移動チェック
    /// </summary>
    /// <param name="dungeon"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="rote"></param>
    /// <param name="leftside"></param>
    /// <param name="rightside"></param>
    /// <param name="isChange"></param>
    /// <returns></returns>
    protected bool CheckMoveSlanting(ManageDungeon dungeon, int x, int y, CharacterDirection dir, MapPoint leftside , MapPoint rightside)
    {
        MapPoint target = MapPoint.Get(x, y);

        SetDirection(dir);
        bool isChange = dungeon.CheckCharacterMoveSlanting(target, leftside, rightside);

        //場所移動可能なら場所替えをする
        if (isChange == true)
        {
            dungeon.MoveCharacter(target, this);
            ActType = ActionType.Move;
        }

        return isChange;
    }
    protected virtual void SetAnimator()
    {
        //switch(InstanceName)
        //{
        //    case "Butterfly_1":
        //        anim = ThisDisplayObject.transform.FindChild("Butterfly_Body").GetComponent<Animator>();
        //        break;
        //    case "TeddyBear_1":
        //        anim = ThisDisplayObject.transform.FindChild("TeddyBear_Body").GetComponent<Animator>();
        //        break;
        //    default:
        //        anim = ThisDisplayObject.GetComponent<Animator>();
        //        break;
        //}
    }
    public virtual WeaponBase EquipWeapon
    {
        get
        {
            return WeaponFreeHand.Instance;
        }
    }

    public virtual ShieldBase EquipShield
    {
        get
        {
            return ShieldFreeHand.Instance;
        }
    }

    public virtual RingBase EquipRing
    {
        get
        {
            return RingFreeHand.Instance;
        }
    }

    public virtual string GetMessageDeath(int exp = 0)
    {
        return string.Format(CommonConst.Message.DeathPlayer, DisplayNameInMessage);
    }
    public virtual string GetMessageAttackHit(string enemyname, int damage)
    {
        return string.Format(CommonConst.Message.AttackPlayer, enemyname, DisplayNameInMessage, damage);
    }
    public virtual string GetMessageAttackHit(int damage)
    {
        return string.Format(CommonConst.Message.AttackMessage, DisplayNameInMessage, damage);
    }
    public virtual string GetMessageAttackMiss()
    {
        return string.Format(CommonConst.Message.AttackMissPlayer, DisplayNameInMessage);
    }
    public virtual VoiceInformation.VoiceType VoiceAttack()
    {
        return VoiceInformation.VoiceType.None;
    }
    public virtual VoiceInformation.VoiceType VoiceDefence()
    {
        return VoiceInformation.VoiceType.None;
    }

    public virtual void VoiceDeath()
    {

    }


    /// <summary>
    /// 移動アニメーションオフの切り替え判定
    /// </summary>
    /// <returns></returns>
    //protected virtual bool CheckIsMoveFalse(DungeonCreateModel dungeon)
    //{
    //    return true;
    //}
}
