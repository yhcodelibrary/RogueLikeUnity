using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class UseItemInformation
{
    /// <summary>
    /// 使うアイテム
    /// </summary>
    public static BaseItem UseItemMain;

    /// <summary>
    /// 使うアイテムの効果対象
    /// </summary>
    public static BaseItem UseItemEffectTarget;
    public static BagBase ParentDrive;
    /// <summary>
    /// どの動作を行うか
    /// </summary>
    public static MenuItemActionType ItemUseType;

    public static BaseItem FootItem;

    public static bool IsStair;
}
