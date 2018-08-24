using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseObject: IEquatable<BaseObject>, IDisposable
{
    /// <summary>
    /// キャラクタ管理番号
    /// </summary>
    public long ObjNo;

    /// <summary>
    /// 現在の位置
    /// </summary>
    public MapPoint CurrentPoint;

    /// <summary>
    /// 移動前の位置
    /// </summary>
    public MapPoint BeforePoint;

    /// <summary>
    /// 投げ先の位置
    /// </summary>
    public MapPoint ThrowPoint;

    /// <summary>
    /// 吹き飛ばしの位置
    /// </summary>
    public MapPoint BlowPoint;

    public CharacterDirection BlowDirection;

    /// <summary>
    /// 可視化するか
    /// </summary>
    public bool IsVisible;

    protected bool _IsActive = true;
    /// <summary>
    /// 有効化フラグ
    /// </summary>
    public virtual bool IsActive
    {
        get
        {
            return _IsActive;
        }
        set
        {
            if(_IsActive == value)
            {
                return;
            }
            _IsActive = value;

            SetActivePosition();
        }
    }

    protected float _PosYNoneActive = 250f;
    protected float _PosY = 2.5f;
    /// <summary>
    /// 高さのデフォルト値
    /// </summary>
    public virtual float PosY
    {
        get
        {
            if(IsActive == true)
            {
                return _PosY;
            }
            else
            {
                return _PosYNoneActive;
                
            }
        }
        set { _PosY = value; }
    }

    public float OriginalPosY
    {
        get
        {
            return _PosY;
        }
    }

    /// <summary>
    /// マップの最小単位の大きさ
    /// </summary>
    public float PositionUnit = CommonConst.SystemValue.FieldUnit;

    /// <summary>
    /// 移動速度
    /// </summary>
    public float MoveSpeed = CommonConst.SystemValue.MoveSpeedDefault;

    /// <summary>
    /// 移動速度
    /// </summary>
    public float MoveSpeedDefault = CommonConst.SystemValue.MoveSpeedDefault;
    
    /// <summary>
    /// ポイントに移動したと判断する範囲
    /// </summary>
    public float MinDistance = 0.1f;

    /// <summary>
    /// キャラクタの種別
    /// </summary>
    public ObjectType Type;

    /// <summary>
    /// キャラクターに対する一意の名前
    /// </summary>
    public Guid Name;

    /// <summary>
    /// キャラクターの名前一般名称
    /// </summary>
    public string DisplayName;

    /// <summary>
    /// キャラクターの名前一般名称【前】
    /// </summary>
    public string DisplayNameBefore;

    /// <summary>
    /// 修飾子のObjNo
    /// </summary>
    public ushort DisplayNameBeforeObjNo;

    /// <summary>
    /// キャラクターの名前一般名称(後ろ)
    /// </summary>
    public string DisplayNameAfter;

    public virtual string DisplayNameNormal
    { get { return string.Format(CommonConst.Format.DefaultName, DisplayNameBefore, DisplayName, DisplayNameAfter); } }
    public virtual string DisplayNameInMessage {
        get { return string.Format(CommonConst.Format.DefaultName, DisplayNameBefore, DisplayName, DisplayNameAfter); } }

    public virtual string DisplayNameInItem { get { return string.Format(CommonConst.Format.DefaultName, DisplayNameBefore, DisplayName, DisplayNameAfter); } }

    /// <summary>
    /// ベースオブジェクトの名称
    /// </summary>
    public string InstanceName;

    /// <summary>
    /// キャラクタをマップに描画するか
    /// </summary>
    protected bool isMapDisplay = false;
    public virtual bool IsMapDisplay
    {
        get
        {
            return isMapDisplay;
        }
        set
        {
            isMapDisplay = value;
        }
    }

    /// <summary>
    /// 投擲によって壊れたか
    /// </summary>
    public bool IsThrowBreak = false;

    /// <summary>
    /// 投擲後に消えたか
    /// </summary>
    public bool IsThrowVanish = false;

    /// <summary>
    /// 説明文
    /// </summary>
    public string Description;

    /// <summary>
    /// マップ表示のオブジェクト
    /// </summary>
    public MaptipInformation MapUnit;

    private AnimationInformation _ThisAnimator;
    protected AnimationInformation ThisAnimator
    {
        get
        {
            if(CommonFunction.IsNull(_ThisAnimator) == false)
            {
                return _ThisAnimator;
            }
            _ThisAnimator = new AnimationInformation();
            return _ThisAnimator;
        }
    }
    
    private Transform _ThisTransform;
    public Transform ThisTransform
    {
        get
        {
            return _ThisTransform;
        }
        set
        {
            _ThisTransform = value;
        }
    }
    /// <summary>
    /// 画面表示オブジェクト
    /// </summary>
    private GameObject _ThisDisplayObject;
    public GameObject ThisDisplayObject
    {
        get
        {
            return _ThisDisplayObject;
        }
        set
        {
            ThisAnimator.Anim = null;
            _ThisDisplayObject = value;
            if (CommonFunction.IsNull(value) == false)
            {
                ThisTransform = value.transform;
            }
        }
    }

    protected CharacterDirection _Direction;
    /// <summary>
    /// 方向
    /// </summary>
    public virtual CharacterDirection Direction
    {
        get
        {
            return _Direction;
        }
        set
        {
            _Direction = value;
        }
    }
    
    public override bool Equals(object obj)
    {
        //objがnullか、型が違うときは、等価でない
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        return this.Name == ((BaseObject)obj).Name;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public virtual void ErrorInitialize()
    {
        
    }

    /// <summary>
    /// オブジェクトの配置
    /// </summary>
    public virtual void SetPosition(int x, int y)
    {
        CurrentPoint = MapPoint.Get(x, y);
        BeforePoint = CurrentPoint;
        //対象を初期位置に配置
        ResetObjectPosition();
        ThrowPoint = CurrentPoint;
    }

    /// <summary>
    /// オブジェクトの配置
    /// </summary>
    public virtual void ResetPositionThrowStart()
    {
        ThisTransform.localPosition = new Vector3(ThisTransform.localPosition.x,
            PosY,
            ThisTransform.localPosition.z);
    }

    public void SetPositionThrow(int x, int y)
    {
        SetPosition(x, y);
    }

    public virtual void SetThisDisplayObject(int x, int y)
    {
        ThisDisplayObject = ResourceInformation.GetInstance(InstanceName, true);
        //ThisDisplayObject = UnityEngine.Object.Instantiate(ResourceInformation.CommonImage.transform.FindChild(InstanceName).gameObject,
        //    ResourceInformation.DungeonDynamicObject.transform);
        Direction = Direction;
        SetPositionThrow(x, y);
    }

    public virtual void SetCharacterDisplayObject(int x, int y)
    {
        ThisDisplayObject = ResourceInformation.GetInstance(InstanceName, true);
        //ThisDisplayObject = UnityEngine.Object.Instantiate(ResourceInformation.CommonImage.transform.FindChild(InstanceName).gameObject,
        //    ResourceInformation.DungeonDynamicObject.transform);
        ChangeDirection(Direction);
        SetPosition(x, y);
    }

    public void RemoveDisplayObject()
    {
        GameObject.Destroy(ThisDisplayObject);
        ThisDisplayObject = null;
    }

    protected bool CheckNotAction(MapPoint plcurrent)
    {
        bool result = false;
        //距離が一定以上あったらアクションさせない
        if (CurrentPoint.DistanceAbs(plcurrent) > 6)
        {
            result = true;
        }
        //消えているときはアクションさせない
        else if (ThisDisplayObject.activeInHierarchy == false)
        {
            result = true;
        }
        //消えているときはアクションさせない
        else if (IsActive == false)
        {
            result = true;
        }
        return result;
    }

    public virtual void Initialize()
    {
        Name = Guid.NewGuid();
        ThisDisplayObject = null;
        Direction = CharacterDirection.Bottom;
        IsVisible = true;
    }

    /// <summary>
    /// 方向の変更
    /// </summary>
    /// <param name="dir"></param>
    public void ChangeDirection(CharacterDirection dir)
    {
        Direction = dir;
        switch (dir)
        {
            //上
            case CharacterDirection.Top:
                SetDirection(CharacterDirection.Top);
                break;
            //左上
            case CharacterDirection.TopLeft:
                SetDirection(CharacterDirection.TopLeft);
                break;
            //左
            case CharacterDirection.Left:
                SetDirection(CharacterDirection.Left);
                break;
            //左下
            case CharacterDirection.BottomLeft:
                SetDirection(CharacterDirection.BottomLeft);
                break;
            //下
            case CharacterDirection.Bottom:
                SetDirection(CharacterDirection.Bottom);
                break;
            //右下
            case CharacterDirection.BottomRight:
                SetDirection(CharacterDirection.BottomRight);
                break;
            //右
            case CharacterDirection.Right:
                SetDirection(CharacterDirection.Right);
                break;
            //右上
            case CharacterDirection.TopRight:
                SetDirection(CharacterDirection.TopRight);
                break;
            default:
                //SetDirection(CharacterDirection.None);
                break;
        }
    }
    
    /// <summary>
    /// 対象オブジェクトの現在位置を再設定する
    /// </summary>
    public virtual void ResetObjectPosition()
    {
        ThisTransform.localPosition = new Vector3(CurrentPoint.X * PositionUnit,
            PosY,
            CurrentPoint.Y * PositionUnit);

        Transform tar = ThisTransform.Find("Body");
        if (CommonFunction.IsNull(tar) == false)
        {
            tar.localPosition = Vector3.zero;
        }
    }

    public virtual void SetActivePosition()
    {
        ThisTransform.localPosition = new Vector3(ThisTransform.localPosition.x,
            PosY,
            ThisTransform.localPosition.z);
    }
    protected void SetDirection(float rote)
    {
        ThisTransform.localEulerAngles = new Vector3(CommonConst.Rotation.None, rote, CommonConst.Rotation.None);

    }
    protected void SetDirection(CharacterDirection dir)
    {
        ThisTransform.localEulerAngles = CommonFunction.EulerAngles[dir];

    }

    public virtual string GetName()
    {
        return DisplayNameInItem;
    }

    public virtual bool Move(PlayerCharacter player)
    {
        if(Type == ObjectType.Stair)
        {
            return false;
        }
        if(CommonFunction.IsNull(ThrowPoint)== true)
        {
            return false;
        }
        //目標位置の取得
        Vector3 target = new Vector3(ThrowPoint.X * PositionUnit,
            PosY,
            ThrowPoint.Y * PositionUnit);

        //距離が一定以上あったらアクションさせない
        if (Vector3.Distance(player.ThisTransform.position,this.ThisTransform.position) > 6)
        {
            //強制的にキャラポジを移動させて終了
            ThisTransform.localPosition = target;
        }

        //目標地点に到着したら終了
        if (Vector3.Distance(ThisTransform.localPosition, target) <= MinDistance)
        {
            //投擲位置に達したらキャラクタコンポーネントを削除し現在位置を強制移動
            //if(ThisDisplayObject.GetComponent<CharacterController>())
            //{
            //    Destroy(ThisDisplayObject.GetComponent<CharacterController>());
            //}
            ResetObjectPosition();

            ThrowPoint = null;
            return false;
        }

        //移動単位の取得
        Vector3 velocity = (target - ThisTransform.localPosition).normalized;

        //移動方向と移動単位が一致するか調べる
        if (CommonFunction.CheckDirectionVector(Direction, velocity) == true)
        {
            //一致していれば通常の移動操作

            //キャラクターを目標に移動
            //ThisDisplayObject.transform.localPosition += (velocity * MoveSpeed);
            //ThisDisplayObject.transform.localPosition += CommonFunction.GetVelocity(velocity, MoveSpeed);
            ThisTransform.Translate(CommonFunction.GetVelocity(velocity, MoveSpeed), Space.World);
        }
        else
        {
            //一致していなければ目標地点を追い越したとみなす
            //強制的にキャラポジを移動させて終了
            ThisTransform.localPosition = target;
        }
        return true;
    }

    public void SetObjectActive(RoomInformation visible,bool usurabi,bool useBefore)
    {
        bool infield = false;
        
        //薄ら日が使われていたら常に可視化
        if (usurabi == true || DungeonInformation.Info.IsBadVisible == false)
        {
            infield = true;
        }
        else// if(c.Type == ObjectType.Enemy)
        {
            if (visible.Left <= this.CurrentPoint.X
                && this.CurrentPoint.X <= visible.Right
                && visible.Top <= this.CurrentPoint.Y
                && this.CurrentPoint.Y <= visible.Bottom)
            {
                infield = true;
            }
            else if(useBefore == true
                && visible.Left <= this.BeforePoint.X
                && this.BeforePoint.X <= visible.Right
                && visible.Top <= this.BeforePoint.Y
                && this.BeforePoint.Y <= visible.Bottom)
            {
                infield = true;
            }
        }
        //視界内で可視性もOKだったらアクティブ
        if (this.IsVisible == true && infield == true)
        {
            this.IsActive = true;
        }
        else
        {
            this.IsActive = false;
        }
    }

    public bool Equals(BaseObject other)
    {
        return this.Name == other.Name;
    }

    #region IDisposable Support
    protected bool disposedValue = false; // 重複する呼び出しを検出するには

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _ThisDisplayObject = null;

                _ThisTransform = null;

                if (CommonFunction.IsNull(_ThisAnimator) == false)
                {
                    _ThisAnimator.Dispose();
                    _ThisAnimator = null;
                }

                if (CommonFunction.IsNull(MapUnit) == false)
                {
                    MapUnit.OffActive();
                    MapUnit = null;
                }
                CurrentPoint = null;
                BeforePoint = null;
                ThrowPoint = null;
                BlowPoint = null;

    // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
}

            // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
            // TODO: 大きなフィールドを null に設定します。

            disposedValue = true;
        }
    }

    // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
    // ~BaseObject() {
    //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
    //   Dispose(false);
    // }

    // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
    public void Dispose()
    {
        // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        Dispose(true);
        // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
        // GC.SuppressFinalize(this);
    }
    #endregion

}
