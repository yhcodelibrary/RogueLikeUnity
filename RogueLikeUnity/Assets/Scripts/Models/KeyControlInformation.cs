using Assets.Scripts;
using Assets.Scripts.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyControlInformation
{
    private static KeyControlModel _info;

    public static KeyControlModel Info
    {
        get
        {
            if(_info != null)
            {
                return _info;
            }
            _info = new KeyControlModel();
            return _info;
        }
        set
        {
            _info = value;
        }
    }


}

public class KeyControlModel
{

    private Dictionary<KeyType, bool> PushManage;
    public void SetPushKey(KeyType t,bool bl)
    {
        PushManage[t] = bl;
    }
    private Dictionary<KeyType, bool> PushManageOneTime;
    public void SetPushKeyOneTime(KeyType t, bool bl)
    {
        PushManageOneTime[t] = bl;
    }


    private Dictionary<KeyType, KeyCode> FieldKeys;
    public Dictionary<KeyType, KeyCode> GetFieldKeys()
    {
        return FieldKeys;
    }
    private Dictionary<KeyType, KeyCode> MenuKeys;
    public Dictionary<KeyType, KeyCode> GetMenuKeys()
    {
        return MenuKeys;
    }

    public bool OnMoveUp()
    {
        //if(Input.GetKeyDown(KeyControlInformation.Info.MoveUp))
        //{
        //    ManageWait.Info.WaitCursor = CommonConst.Wait.MenuSelect;
        //}

        if (OnKeyCommon(Input.GetKey(KeyControlInformation.Info.MoveUp), KeyType.MoveUp))
        {
            return true;
        }
        else if (CheckAxis(VerticalKey, VerticalDir) == true)
        {
            return true;
        }
        return false;
    }

    public bool OnMoveDown()
    {
        //if (Input.GetKeyDown(KeyControlInformation.Info.MoveDown))
        //{
        //    ManageWait.Info.WaitCursor = CommonConst.Wait.MenuSelect;
        //}
        //if (Input.GetKey(KeyControlInformation.Info.MoveDown))
        if (OnKeyCommon(Input.GetKey(KeyControlInformation.Info.MoveDown), KeyType.MoveDown))
        {
            return true;
        }
        else if (CheckAxis(VerticalKey, VerticalDir * -1) == true)
        {
            return true;
        }
        return false;
    }

    public bool OnMoveLeft()
    {
        //if (Input.GetKeyDown(KeyControlInformation.Info.MoveLeft))
        //{
        //    ManageWait.Info.WaitCursor = CommonConst.Wait.MenuSelect;
        //}
        //if (Input.GetKey(KeyControlInformation.Info.MoveLeft))
        if (OnKeyCommon(Input.GetKey(KeyControlInformation.Info.MoveLeft), KeyType.MoveLeft))
        {
            return true;
        }
        else if (CheckAxis(HorizontalKey, HorizontalDir * -1) == true)
        {
            return true;
        }
        return false;
    }

    public bool OnMoveRight()
    {
        //if (Input.GetKeyDown(KeyControlInformation.Info.MoveRight))
        //{
        //    ManageWait.Info.WaitCursor = CommonConst.Wait.MenuSelect;
        //}
        //if (Input.GetKey(KeyControlInformation.Info.MoveRight))
        if (OnKeyCommon(Input.GetKey(KeyControlInformation.Info.MoveRight), KeyType.MoveRight))
        {
            return true;
        }
        else if (CheckAxis(HorizontalKey, HorizontalDir ) == true)
        {
            return true;
        }
        return false;
    }
    private bool CheckAxis(string key, int dir)
    {
        if(string.IsNullOrEmpty(key) == true)
        {
            return false;
        }

        if(dir == 1)
        {
            if (Input.GetAxis(key) >= 0.7)
            {
                return true;
            }
        }
        else if(dir == -1)
        {
            if (Input.GetAxis(key) <= -0.7)
            {
                return true;
            }
        }
        return false;
    }
    public bool OnKeyField(KeyType t)
    {
        return OnKeyCommon(Input.GetKey(FieldKeys[t]), t);
    }
    public bool OnKeyDownField(KeyType t)
    {
        //bool isPush = (PushUpManage[t] == false && Input.GetKeyDown(FieldKeys[t]));
        //if (isPush == true)
        //{
        //    PushUpManage[t] = true;
        //}
        bool isPush = Input.GetKeyDown(FieldKeys[t]);
        return OnKeyCommon(isPush, t);
    }
    public bool OnKeyMenu(KeyType t)
    {
        return OnKeyCommon(Input.GetKey(MenuKeys[t]), t);
    }
    public bool OnKeyDownMenu(KeyType t)
    {
        //bool isPush =(PushUpManage[t] == false && Input.GetKeyDown(MenuKeys[t]));
        //if(isPush == true)
        //{
        //    PushUpManage[t] = true;
        //}
        bool isPush = Input.GetKeyDown(MenuKeys[t]);
        return OnKeyCommon(isPush, t);
    }
    private bool OnKeyCommon(bool keybool, KeyType t)
    {
        if (keybool == true || PushManage[t] == true || PushManageOneTime[t] == true)
        {
            PushManageOneTime[t] = false;
            //PushManage[t] = false;
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool OnLeftClick()
    {
        if(OpMode ==  OperationMode.UseMouse
            && Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }
    public bool OnLeftClick(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;

        if (OpMode == OperationMode.UseMouse
            && CommonFunction.IsNull(pointerEventData) == false
            && pointerEventData.button == PointerEventData.InputButton.Left)
        {
            return true;
        }
        return false;
    }
    public bool OnRightClick(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;

        if (OpMode == OperationMode.UseMouse
            && CommonFunction.IsNull(pointerEventData) == false
            && pointerEventData.button == PointerEventData.InputButton.Right)
        {
            return true;
        }
        return false;
    }

    public enum VerticalKeyList
    {
        Vertical0 = 1,
        Vertical1,
        Vertical2,
        Vertical3,
        Vertical4,
        Vertical5,
        Vertical6,
        Vertical7,
        Vertical8,
        Vertical9,
        Vertical10,
        Vertical11,
        Vertical12,
        Vertical13,
        Vertical14,
        Vertical15,
        Vertical16,
        Vertical17,
        Vertical18,
        Vertical19,
        Vertical20,
        Vertical21,
        Vertical22,
        Vertical23,
        Vertical24,
        Vertical25,
        //    , Enum.GetName(typeof(Season), enmVal);
    }
    private static Array _VerticalKeyLists;
    public static Array VerticalKeyLists()
    {
        if (CommonFunction.IsNull(_VerticalKeyLists) == false)
        {
            return _VerticalKeyLists;
        }
        _VerticalKeyLists = Enum.GetValues(typeof(VerticalKeyList));
        return _VerticalKeyLists;
    }

    public enum HorizontalKeyList
    {
        Horizon0 = 1,
        Horizon1,
        Horizon2,
        Horizon3,
        Horizon4,
        Horizon5,
        Horizon6,
        Horizon7,
        Horizon8,
        Horizon9,
        Horizon10,
        Horizon11,
        Horizon12,
        Horizon13,
        Horizon14,
        Horizon15,
        Horizon16,
        Horizon17,
        Horizon18,
        Horizon19,
        Horizon20,
        Horizon21,
        Horizon22,
        Horizon23,
        Horizon24,
        Horizon25,
        //    , Enum.GetName(typeof(Season), enmVal);
    }


    private static Array _HorizontalKeyLists;
    public static Array HorizontalKeyLists()
    {
        if (CommonFunction.IsNull(_HorizontalKeyLists) == false)
        {
            return _HorizontalKeyLists;
        }
        _HorizontalKeyLists = Enum.GetValues(typeof(HorizontalKeyList));
        return _HorizontalKeyLists;
    }
    public OperationMode OpMode;

    public string VerticalKey;
    public string HorizontalKey;

    public int VerticalDir;
    public int HorizontalDir;

    public KeyCode MoveUp { get { return FieldKeys[KeyType.MoveUp]; } set { FieldKeys[KeyType.MoveUp] = value; } }
    public KeyCode MoveDown { get { return FieldKeys[KeyType.MoveDown]; } set { FieldKeys[KeyType.MoveDown] = value; } }
    public KeyCode MoveRight { get { return FieldKeys[KeyType.MoveRight]; } set { FieldKeys[KeyType.MoveRight] = value; } }
    public KeyCode MoveLeft { get { return FieldKeys[KeyType.MoveLeft]; } set { FieldKeys[KeyType.MoveLeft] = value; } }
    /// <summary>
    /// 斜め移動
    /// </summary>
    public KeyCode XMove { get { return FieldKeys[KeyType.XMove]; } set { FieldKeys[KeyType.XMove] = value; } }
    public KeyCode ChangeDirection { get { return FieldKeys[KeyType.ChangeDirection]; } set { FieldKeys[KeyType.ChangeDirection] = value; } }
    public KeyCode Attack { get { return FieldKeys[KeyType.Attack]; } set { FieldKeys[KeyType.Attack] = value; } }
    /// <summary>
    /// 必殺技
    /// </summary>
    public KeyCode DeathBlow { get { return FieldKeys[KeyType.DeathBlow]; } set { FieldKeys[KeyType.DeathBlow] = value; } }
    /// <summary>
    /// 足踏み
    /// </summary>
    public KeyCode Idle { get { return FieldKeys[KeyType.Idle]; } set { FieldKeys[KeyType.Idle] = value; } }
    public KeyCode Dash { get { return FieldKeys[KeyType.Dash]; } set { FieldKeys[KeyType.Dash] = value; } }

    /// <summary>
    /// 画面オープン系
    /// </summary>
    public KeyCode MenuOpen { get { return FieldKeys[KeyType.MenuOpen]; } set { FieldKeys[KeyType.MenuOpen] = value; } }
    public KeyCode MessageLog { get { return FieldKeys[KeyType.MessageLog]; } set { FieldKeys[KeyType.MessageLog] = value; } }
    public KeyCode KeyDisplay { get { return FieldKeys[KeyType.KeyDisplay]; } set { FieldKeys[KeyType.KeyDisplay] = value; } }

    /// <summary>
    /// メニュー系
    /// </summary>
    public KeyCode MenuCancel { get { return MenuKeys[KeyType.MenuCancel]; } set { MenuKeys[KeyType.MenuCancel] = value; } }
    public KeyCode MenuOk { get { return MenuKeys[KeyType.MenuOk]; } set { MenuKeys[KeyType.MenuOk] = value; } }
    public KeyCode LookOption { get { return MenuKeys[KeyType.LookOption]; } set { MenuKeys[KeyType.LookOption] = value; } }
    public KeyCode ItemSort { get { return MenuKeys[KeyType.ItemSort]; } set { MenuKeys[KeyType.ItemSort] = value; } }
    public KeyCode MenuMultiSelectOk { get { return MenuKeys[KeyType.MenuMultiSelectOk]; } set { MenuKeys[KeyType.MenuMultiSelectOk] = value; } }

    public KeyControlModel()
    {
        FieldKeys = new Dictionary<KeyType, KeyCode>(new KeyTypeComparer());
        MenuKeys = new Dictionary<KeyType, KeyCode>(new KeyTypeComparer());
        //KeyTypeMapString = new Dictionary<KeyType, string>(new KeyTypeComparer());
        FieldKeys.Add(KeyType.MoveUp, KeyCode.UpArrow);
        FieldKeys.Add(KeyType.MoveDown, KeyCode.DownArrow);
        FieldKeys.Add(KeyType.MoveRight, KeyCode.RightArrow);
        FieldKeys.Add(KeyType.MoveLeft, KeyCode.LeftArrow);
        FieldKeys.Add(KeyType.XMove, KeyCode.X);
        FieldKeys.Add(KeyType.ChangeDirection, KeyCode.C);
        FieldKeys.Add(KeyType.Attack, KeyCode.A);
        FieldKeys.Add(KeyType.DeathBlow, KeyCode.H);
        FieldKeys.Add(KeyType.Dash, KeyCode.D);
        FieldKeys.Add(KeyType.Idle, KeyCode.S);
        FieldKeys.Add(KeyType.MenuOpen, KeyCode.M);
        FieldKeys.Add(KeyType.KeyDisplay, KeyCode.K);
        FieldKeys.Add(KeyType.MessageLog, KeyCode.L);
        MenuKeys.Add(KeyType.MenuOk, KeyCode.A);
        MenuKeys.Add(KeyType.MenuCancel, KeyCode.C);
        MenuKeys.Add(KeyType.LookOption, KeyCode.R);
        MenuKeys.Add(KeyType.ItemSort, KeyCode.S);
        MenuKeys.Add(KeyType.MenuMultiSelectOk, KeyCode.W);
        VerticalKey = "";
        HorizontalKey = "";
        OpMode = OperationMode.KeyOnly;

        PushManage = new Dictionary<KeyType, bool>(new KeyTypeComparer());
        PushManageOneTime = new Dictionary<KeyType, bool>(new KeyTypeComparer());
        //PushUpManage = new Dictionary<KeyType, bool>(new KeyTypeComparer());

        foreach (KeyType t in Enum.GetValues(typeof(KeyType)))
        {
            PushManage.Add(t, false);
            PushManageOneTime.Add(t, false);
            //PushUpManage.Add(t, false);
            string name = Enum.GetName(typeof(KeyType), t);
            //KeyTypeMap.Add(name, t);
            //KeyTypeMapString.Add(t, name);
        }

        VerticalDir = 1;
        HorizontalDir = 1;
}
    
    public string GetJsonData()
    {
        string json = JsonMapper.ToJson(this);

        return json;
    }

    public KeyControlModel Clone()
    {
        KeyControlModel c = new KeyControlModel();
        foreach (KeyType t in FieldKeys.Keys)
        {
            c.FieldKeys[t] = FieldKeys[t];
        }
        foreach (KeyType t in MenuKeys.Keys)
        {
            c.MenuKeys[t] = MenuKeys[t];
        }

        c.HorizontalDir = HorizontalDir;
        c.HorizontalKey = HorizontalKey;
        c.VerticalDir = VerticalDir;
        c.VerticalKey = VerticalKey;
        c.OpMode = OpMode;
        return c;
    }

    public bool CheckContainsKey(Dictionary<KeyType, KeyCode> list, KeyCode code, KeyType type)
    {
        foreach (KeyType t in list.Keys)
        {
            if (t != type)
            {
                if (list[t] == code)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public string GetKeyValueText()
    {
        const string format = "{0} : {1}";
        StringBuilder sb = new StringBuilder();

        sb.Append(string.Format(format, "移動（上）          ", GetName(MoveUp)));
        sb.AppendLine(string.Format(format, "移動（下）          ", GetName(MoveDown)));
        sb.Append(string.Format(format, "移動（右）          ", GetName(MoveRight)));
        sb.AppendLine(string.Format(format, "移動（左）          ", GetName(MoveLeft)));
        sb.Append(string.Format(format, "斜め移動固定        ", GetName(XMove)));
        sb.AppendLine(string.Format(format, "方向変換固定        ", GetName(ChangeDirection)));
        sb.Append(string.Format(format, "攻撃                ", GetName(Attack)));
        sb.AppendLine(string.Format(format, "必殺技              ", GetName(DeathBlow)));
        sb.Append(string.Format(format, "ダッシュ            ", GetName(Dash)));
        sb.AppendLine(string.Format(format, "メニュー表示        ", GetName(MenuOpen)));
        sb.Append(string.Format(format, "メッセージログ      ", GetName(MessageLog)));
        sb.AppendLine(string.Format(format, "足踏み              ", GetName(Idle)));
        sb.Append(string.Format(format, "キー表示            ", GetName(KeyDisplay)));
        sb.AppendLine(string.Format(format, "メニュー決定        ", GetName(MenuOk)));
        sb.Append(string.Format(format, "メニューキャンセル  ", GetName(MenuCancel)));
        sb.AppendLine(string.Format(format, "オプション参照      ", GetName(LookOption)));
        sb.Append(string.Format(format, "アイテムソート      ", GetName(ItemSort)));
        sb.AppendLine(string.Format(format, "アイテム複数選択    ", GetName(MenuMultiSelectOk)));

        

        return sb.ToString();
    }
    private static int LenB(string stTarget)
    {
        return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(stTarget);
    }
    public static string GetName(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.None: return "None                ";
            case KeyCode.Backspace: return "Backspace           ";
            case KeyCode.Tab: return "Tab                 ";
            case KeyCode.Clear: return "Clear               ";
            case KeyCode.Return: return "Return              ";
            case KeyCode.Pause: return "Pause               ";
            case KeyCode.Escape: return "Escape              ";
            case KeyCode.Space: return "Space               ";
            case KeyCode.Exclaim: return "Exclaim             ";
            case KeyCode.DoubleQuote: return "DoubleQuote         ";
            case KeyCode.Hash: return "Hash                ";
            case KeyCode.Dollar: return "Dollar              ";
            case KeyCode.Ampersand: return "Ampersand           ";
            case KeyCode.Quote: return "Quote               ";
            case KeyCode.LeftParen: return "LeftParen           ";
            case KeyCode.RightParen: return "RightParen          ";
            case KeyCode.Asterisk: return "Asterisk            ";
            case KeyCode.Plus: return "Plus                ";
            case KeyCode.Comma: return "Comma               ";
            case KeyCode.Minus: return "Minus               ";
            case KeyCode.Period: return "Period              ";
            case KeyCode.Slash: return "Slash               ";
            case KeyCode.Alpha0: return "Alpha0              ";
            case KeyCode.Alpha1: return "Alpha1              ";
            case KeyCode.Alpha2: return "Alpha2              ";
            case KeyCode.Alpha3: return "Alpha3              ";
            case KeyCode.Alpha4: return "Alpha4              ";
            case KeyCode.Alpha5: return "Alpha5              ";
            case KeyCode.Alpha6: return "Alpha6              ";
            case KeyCode.Alpha7: return "Alpha7              ";
            case KeyCode.Alpha8: return "Alpha8              ";
            case KeyCode.Alpha9: return "Alpha9              ";
            case KeyCode.Colon: return "Colon               ";
            case KeyCode.Semicolon: return "Semicolon           ";
            case KeyCode.Less: return "Less                ";
            case KeyCode.Equals: return "Equals              ";
            case KeyCode.Greater: return "Greater             ";
            case KeyCode.Question: return "Question            ";
            case KeyCode.At: return "At                  ";
            case KeyCode.LeftBracket: return "LeftBracket         ";
            case KeyCode.Backslash: return "Backslash           ";
            case KeyCode.RightBracket: return "RightBracket        ";
            case KeyCode.Caret: return "Caret               ";
            case KeyCode.Underscore: return "Underscore          ";
            case KeyCode.BackQuote: return "BackQuote           ";
            case KeyCode.A: return "A                   ";
            case KeyCode.B: return "B                   ";
            case KeyCode.C: return "C                   ";
            case KeyCode.D: return "D                   ";
            case KeyCode.E: return "E                   ";
            case KeyCode.F: return "F                   ";
            case KeyCode.G: return "G                   ";
            case KeyCode.H: return "H                   ";
            case KeyCode.I: return "I                   ";
            case KeyCode.J: return "J                   ";
            case KeyCode.K: return "K                   ";
            case KeyCode.L: return "L                   ";
            case KeyCode.M: return "M                   ";
            case KeyCode.N: return "N                   ";
            case KeyCode.O: return "O                   ";
            case KeyCode.P: return "P                   ";
            case KeyCode.Q: return "Q                   ";
            case KeyCode.R: return "R                   ";
            case KeyCode.S: return "S                   ";
            case KeyCode.T: return "T                   ";
            case KeyCode.U: return "U                   ";
            case KeyCode.V: return "V                   ";
            case KeyCode.W: return "W                   ";
            case KeyCode.X: return "X                   ";
            case KeyCode.Y: return "Y                   ";
            case KeyCode.Z: return "Z                   ";
            case KeyCode.Delete: return "Delete              ";
            case KeyCode.Keypad0: return "Keypad0             ";
            case KeyCode.Keypad1: return "Keypad1             ";
            case KeyCode.Keypad2: return "Keypad2             ";
            case KeyCode.Keypad3: return "Keypad3             ";
            case KeyCode.Keypad4: return "Keypad4             ";
            case KeyCode.Keypad5: return "Keypad5             ";
            case KeyCode.Keypad6: return "Keypad6             ";
            case KeyCode.Keypad7: return "Keypad7             ";
            case KeyCode.Keypad8: return "Keypad8             ";
            case KeyCode.Keypad9: return "Keypad9             ";
            case KeyCode.KeypadPeriod: return "KeypadPeriod        ";
            case KeyCode.KeypadDivide: return "KeypadDivide        ";
            case KeyCode.KeypadMultiply: return "KeypadMultiply      ";
            case KeyCode.KeypadMinus: return "KeypadMinus         ";
            case KeyCode.KeypadPlus: return "KeypadPlus          ";
            case KeyCode.KeypadEnter: return "KeypadEnter         ";
            case KeyCode.KeypadEquals: return "KeypadEquals        ";
            case KeyCode.UpArrow: return "UpArrow             ";
            case KeyCode.DownArrow: return "DownArrow           ";
            case KeyCode.RightArrow: return "RightArrow          ";
            case KeyCode.LeftArrow: return "LeftArrow           ";
            case KeyCode.Insert: return "Insert              ";
            case KeyCode.Home: return "Home                ";
            case KeyCode.End: return "End                 ";
            case KeyCode.PageUp: return "PageUp              ";
            case KeyCode.PageDown: return "PageDown            ";
            case KeyCode.F1: return "F1                  ";
            case KeyCode.F2: return "F2                  ";
            case KeyCode.F3: return "F3                  ";
            case KeyCode.F4: return "F4                  ";
            case KeyCode.F5: return "F5                  ";
            case KeyCode.F6: return "F6                  ";
            case KeyCode.F7: return "F7                  ";
            case KeyCode.F8: return "F8                  ";
            case KeyCode.F9: return "F9                  ";
            case KeyCode.F10: return "F10                 ";
            case KeyCode.F11: return "F11                 ";
            case KeyCode.F12: return "F12                 ";
            case KeyCode.F13: return "F13                 ";
            case KeyCode.F14: return "F14                 ";
            case KeyCode.F15: return "F15                 ";
            case KeyCode.Numlock: return "Numlock             ";
            case KeyCode.CapsLock: return "CapsLock            ";
            case KeyCode.ScrollLock: return "ScrollLock          ";
            case KeyCode.RightShift: return "RightShift          ";
            case KeyCode.LeftShift: return "LeftShift           ";
            case KeyCode.RightControl: return "RightControl        ";
            case KeyCode.LeftControl: return "LeftControl         ";
            case KeyCode.RightAlt: return "RightAlt            ";
            case KeyCode.LeftAlt: return "LeftAlt             ";
            case KeyCode.RightCommand: return "RightCommand        ";
            //case KeyCode.RightApple: return "RightApple          ";
            case KeyCode.LeftCommand: return "LeftCommand         ";
            //case KeyCode.LeftApple: return "LeftApple           ";
            case KeyCode.LeftWindows: return "LeftWindows         ";
            case KeyCode.RightWindows: return "RightWindows        ";
            case KeyCode.AltGr: return "AltGr               ";
            case KeyCode.Help: return "Help                ";
            case KeyCode.Print: return "Print               ";
            case KeyCode.SysReq: return "SysReq              ";
            case KeyCode.Break: return "Break               ";
            case KeyCode.Menu: return "Menu                ";
            case KeyCode.Mouse0: return "Mouse0              ";
            case KeyCode.Mouse1: return "Mouse1              ";
            case KeyCode.Mouse2: return "Mouse2              ";
            case KeyCode.Mouse3: return "Mouse3              ";
            case KeyCode.Mouse4: return "Mouse4              ";
            case KeyCode.Mouse5: return "Mouse5              ";
            case KeyCode.Mouse6: return "Mouse6              ";
            case KeyCode.JoystickButton0: return "JoystickButton0     ";
            case KeyCode.JoystickButton1: return "JoystickButton1     ";
            case KeyCode.JoystickButton2: return "JoystickButton2     ";
            case KeyCode.JoystickButton3: return "JoystickButton3     ";
            case KeyCode.JoystickButton4: return "JoystickButton4     ";
            case KeyCode.JoystickButton5: return "JoystickButton5     ";
            case KeyCode.JoystickButton6: return "JoystickButton6     ";
            case KeyCode.JoystickButton7: return "JoystickButton7     ";
            case KeyCode.JoystickButton8: return "JoystickButton8     ";
            case KeyCode.JoystickButton9: return "JoystickButton9     ";
            case KeyCode.JoystickButton10: return "JoystickButton10    ";
            case KeyCode.JoystickButton11: return "JoystickButton11    ";
            case KeyCode.JoystickButton12: return "JoystickButton12    ";
            case KeyCode.JoystickButton13: return "JoystickButton13    ";
            case KeyCode.JoystickButton14: return "JoystickButton14    ";
            case KeyCode.JoystickButton15: return "JoystickButton15    ";
            case KeyCode.JoystickButton16: return "JoystickButton16    ";
            case KeyCode.JoystickButton17: return "JoystickButton17    ";
            case KeyCode.JoystickButton18: return "JoystickButton18    ";
            case KeyCode.JoystickButton19: return "JoystickButton19    ";
            case KeyCode.Joystick1Button0: return "Joystick1Button0    ";
            case KeyCode.Joystick1Button1: return "Joystick1Button1    ";
            case KeyCode.Joystick1Button2: return "Joystick1Button2    ";
            case KeyCode.Joystick1Button3: return "Joystick1Button3    ";
            case KeyCode.Joystick1Button4: return "Joystick1Button4    ";
            case KeyCode.Joystick1Button5: return "Joystick1Button5    ";
            case KeyCode.Joystick1Button6: return "Joystick1Button6    ";
            case KeyCode.Joystick1Button7: return "Joystick1Button7    ";
            case KeyCode.Joystick1Button8: return "Joystick1Button8    ";
            case KeyCode.Joystick1Button9: return "Joystick1Button9    ";
            case KeyCode.Joystick1Button10: return "Joystick1Button10   ";
            case KeyCode.Joystick1Button11: return "Joystick1Button11   ";
            case KeyCode.Joystick1Button12: return "Joystick1Button12   ";
            case KeyCode.Joystick1Button13: return "Joystick1Button13   ";
            case KeyCode.Joystick1Button14: return "Joystick1Button14   ";
            case KeyCode.Joystick1Button15: return "Joystick1Button15   ";
            case KeyCode.Joystick1Button16: return "Joystick1Button16   ";
            case KeyCode.Joystick1Button17: return "Joystick1Button17   ";
            case KeyCode.Joystick1Button18: return "Joystick1Button18   ";
            case KeyCode.Joystick1Button19: return "Joystick1Button19   ";
            case KeyCode.Joystick2Button0: return "Joystick2Button0    ";
            case KeyCode.Joystick2Button1: return "Joystick2Button1    ";
            case KeyCode.Joystick2Button2: return "Joystick2Button2    ";
            case KeyCode.Joystick2Button3: return "Joystick2Button3    ";
            case KeyCode.Joystick2Button4: return "Joystick2Button4    ";
            case KeyCode.Joystick2Button5: return "Joystick2Button5    ";
            case KeyCode.Joystick2Button6: return "Joystick2Button6    ";
            case KeyCode.Joystick2Button7: return "Joystick2Button7    ";
            case KeyCode.Joystick2Button8: return "Joystick2Button8    ";
            case KeyCode.Joystick2Button9: return "Joystick2Button9    ";
            case KeyCode.Joystick2Button10: return "Joystick2Button10   ";
            case KeyCode.Joystick2Button11: return "Joystick2Button11   ";
            case KeyCode.Joystick2Button12: return "Joystick2Button12   ";
            case KeyCode.Joystick2Button13: return "Joystick2Button13   ";
            case KeyCode.Joystick2Button14: return "Joystick2Button14   ";
            case KeyCode.Joystick2Button15: return "Joystick2Button15   ";
            case KeyCode.Joystick2Button16: return "Joystick2Button16   ";
            case KeyCode.Joystick2Button17: return "Joystick2Button17   ";
            case KeyCode.Joystick2Button18: return "Joystick2Button18   ";
            case KeyCode.Joystick2Button19: return "Joystick2Button19   ";
            case KeyCode.Joystick3Button0: return "Joystick3Button0    ";
            case KeyCode.Joystick3Button1: return "Joystick3Button1    ";
            case KeyCode.Joystick3Button2: return "Joystick3Button2    ";
            case KeyCode.Joystick3Button3: return "Joystick3Button3    ";
            case KeyCode.Joystick3Button4: return "Joystick3Button4    ";
            case KeyCode.Joystick3Button5: return "Joystick3Button5    ";
            case KeyCode.Joystick3Button6: return "Joystick3Button6    ";
            case KeyCode.Joystick3Button7: return "Joystick3Button7    ";
            case KeyCode.Joystick3Button8: return "Joystick3Button8    ";
            case KeyCode.Joystick3Button9: return "Joystick3Button9    ";
            case KeyCode.Joystick3Button10: return "Joystick3Button10   ";
            case KeyCode.Joystick3Button11: return "Joystick3Button11   ";
            case KeyCode.Joystick3Button12: return "Joystick3Button12   ";
            case KeyCode.Joystick3Button13: return "Joystick3Button13   ";
            case KeyCode.Joystick3Button14: return "Joystick3Button14   ";
            case KeyCode.Joystick3Button15: return "Joystick3Button15   ";
            case KeyCode.Joystick3Button16: return "Joystick3Button16   ";
            case KeyCode.Joystick3Button17: return "Joystick3Button17   ";
            case KeyCode.Joystick3Button18: return "Joystick3Button18   ";
            case KeyCode.Joystick3Button19: return "Joystick3Button19   ";
            case KeyCode.Joystick4Button0: return "Joystick4Button0    ";
            case KeyCode.Joystick4Button1: return "Joystick4Button1    ";
            case KeyCode.Joystick4Button2: return "Joystick4Button2    ";
            case KeyCode.Joystick4Button3: return "Joystick4Button3    ";
            case KeyCode.Joystick4Button4: return "Joystick4Button4    ";
            case KeyCode.Joystick4Button5: return "Joystick4Button5    ";
            case KeyCode.Joystick4Button6: return "Joystick4Button6    ";
            case KeyCode.Joystick4Button7: return "Joystick4Button7    ";
            case KeyCode.Joystick4Button8: return "Joystick4Button8    ";
            case KeyCode.Joystick4Button9: return "Joystick4Button9    ";
            case KeyCode.Joystick4Button10: return "Joystick4Button10   ";
            case KeyCode.Joystick4Button11: return "Joystick4Button11   ";
            case KeyCode.Joystick4Button12: return "Joystick4Button12   ";
            case KeyCode.Joystick4Button13: return "Joystick4Button13   ";
            case KeyCode.Joystick4Button14: return "Joystick4Button14   ";
            case KeyCode.Joystick4Button15: return "Joystick4Button15   ";
            case KeyCode.Joystick4Button16: return "Joystick4Button16   ";
            case KeyCode.Joystick4Button17: return "Joystick4Button17   ";
            case KeyCode.Joystick4Button18: return "Joystick4Button18   ";
            case KeyCode.Joystick4Button19: return "Joystick4Button19   ";
            case KeyCode.Joystick5Button0: return "Joystick5Button0    ";
            case KeyCode.Joystick5Button1: return "Joystick5Button1    ";
            case KeyCode.Joystick5Button2: return "Joystick5Button2    ";
            case KeyCode.Joystick5Button3: return "Joystick5Button3    ";
            case KeyCode.Joystick5Button4: return "Joystick5Button4    ";
            case KeyCode.Joystick5Button5: return "Joystick5Button5    ";
            case KeyCode.Joystick5Button6: return "Joystick5Button6    ";
            case KeyCode.Joystick5Button7: return "Joystick5Button7    ";
            case KeyCode.Joystick5Button8: return "Joystick5Button8    ";
            case KeyCode.Joystick5Button9: return "Joystick5Button9    ";
            case KeyCode.Joystick5Button10: return "Joystick5Button10   ";
            case KeyCode.Joystick5Button11: return "Joystick5Button11   ";
            case KeyCode.Joystick5Button12: return "Joystick5Button12   ";
            case KeyCode.Joystick5Button13: return "Joystick5Button13   ";
            case KeyCode.Joystick5Button14: return "Joystick5Button14   ";
            case KeyCode.Joystick5Button15: return "Joystick5Button15   ";
            case KeyCode.Joystick5Button16: return "Joystick5Button16   ";
            case KeyCode.Joystick5Button17: return "Joystick5Button17   ";
            case KeyCode.Joystick5Button18: return "Joystick5Button18   ";
            case KeyCode.Joystick5Button19: return "Joystick5Button19   ";
            case KeyCode.Joystick6Button0: return "Joystick6Button0    ";
            case KeyCode.Joystick6Button1: return "Joystick6Button1    ";
            case KeyCode.Joystick6Button2: return "Joystick6Button2    ";
            case KeyCode.Joystick6Button3: return "Joystick6Button3    ";
            case KeyCode.Joystick6Button4: return "Joystick6Button4    ";
            case KeyCode.Joystick6Button5: return "Joystick6Button5    ";
            case KeyCode.Joystick6Button6: return "Joystick6Button6    ";
            case KeyCode.Joystick6Button7: return "Joystick6Button7    ";
            case KeyCode.Joystick6Button8: return "Joystick6Button8    ";
            case KeyCode.Joystick6Button9: return "Joystick6Button9    ";
            case KeyCode.Joystick6Button10: return "Joystick6Button10   ";
            case KeyCode.Joystick6Button11: return "Joystick6Button11   ";
            case KeyCode.Joystick6Button12: return "Joystick6Button12   ";
            case KeyCode.Joystick6Button13: return "Joystick6Button13   ";
            case KeyCode.Joystick6Button14: return "Joystick6Button14   ";
            case KeyCode.Joystick6Button15: return "Joystick6Button15   ";
            case KeyCode.Joystick6Button16: return "Joystick6Button16   ";
            case KeyCode.Joystick6Button17: return "Joystick6Button17   ";
            case KeyCode.Joystick6Button18: return "Joystick6Button18   ";
            case KeyCode.Joystick6Button19: return "Joystick6Button19   ";
            case KeyCode.Joystick7Button0: return "Joystick7Button0    ";
            case KeyCode.Joystick7Button1: return "Joystick7Button1    ";
            case KeyCode.Joystick7Button2: return "Joystick7Button2    ";
            case KeyCode.Joystick7Button3: return "Joystick7Button3    ";
            case KeyCode.Joystick7Button4: return "Joystick7Button4    ";
            case KeyCode.Joystick7Button5: return "Joystick7Button5    ";
            case KeyCode.Joystick7Button6: return "Joystick7Button6    ";
            case KeyCode.Joystick7Button7: return "Joystick7Button7    ";
            case KeyCode.Joystick7Button8: return "Joystick7Button8    ";
            case KeyCode.Joystick7Button9: return "Joystick7Button9    ";
            case KeyCode.Joystick7Button10: return "Joystick7Button10   ";
            case KeyCode.Joystick7Button11: return "Joystick7Button11   ";
            case KeyCode.Joystick7Button12: return "Joystick7Button12   ";
            case KeyCode.Joystick7Button13: return "Joystick7Button13   ";
            case KeyCode.Joystick7Button14: return "Joystick7Button14   ";
            case KeyCode.Joystick7Button15: return "Joystick7Button15   ";
            case KeyCode.Joystick7Button16: return "Joystick7Button16   ";
            case KeyCode.Joystick7Button17: return "Joystick7Button17   ";
            case KeyCode.Joystick7Button18: return "Joystick7Button18   ";
            case KeyCode.Joystick7Button19: return "Joystick7Button19   ";
            case KeyCode.Joystick8Button0: return "Joystick8Button0    ";
            case KeyCode.Joystick8Button1: return "Joystick8Button1    ";
            case KeyCode.Joystick8Button2: return "Joystick8Button2    ";
            case KeyCode.Joystick8Button3: return "Joystick8Button3    ";
            case KeyCode.Joystick8Button4: return "Joystick8Button4    ";
            case KeyCode.Joystick8Button5: return "Joystick8Button5    ";
            case KeyCode.Joystick8Button6: return "Joystick8Button6    ";
            case KeyCode.Joystick8Button7: return "Joystick8Button7    ";
            case KeyCode.Joystick8Button8: return "Joystick8Button8    ";
            case KeyCode.Joystick8Button9: return "Joystick8Button9    ";
            case KeyCode.Joystick8Button10: return "Joystick8Button10   ";
            case KeyCode.Joystick8Button11: return "Joystick8Button11   ";
            case KeyCode.Joystick8Button12: return "Joystick8Button12   ";
            case KeyCode.Joystick8Button13: return "Joystick8Button13   ";
            case KeyCode.Joystick8Button14: return "Joystick8Button14   ";
            case KeyCode.Joystick8Button15: return "Joystick8Button15   ";
            case KeyCode.Joystick8Button16: return "Joystick8Button16   ";
            case KeyCode.Joystick8Button17: return "Joystick8Button17   ";
            case KeyCode.Joystick8Button18: return "Joystick8Button18   ";
            case KeyCode.Joystick8Button19: return "Joystick8Button19   ";

        }
        return "";
    }

    public static KeyCode GetInputKey()
    {
        if (Input.GetKey(KeyCode.None)) { return KeyCode.None; }
        else if (Input.GetKey(KeyCode.Backspace)) { return KeyCode.Backspace; }
        else if (Input.GetKey(KeyCode.Tab)) { return KeyCode.Tab; }
        else if (Input.GetKey(KeyCode.Clear)) { return KeyCode.Clear; }
        else if (Input.GetKey(KeyCode.Return)) { return KeyCode.Return; }
        else if (Input.GetKey(KeyCode.Pause)) { return KeyCode.Pause; }
        else if (Input.GetKey(KeyCode.Escape)) { return KeyCode.Escape; }
        else if (Input.GetKey(KeyCode.Space)) { return KeyCode.Space; }
        else if (Input.GetKey(KeyCode.Exclaim)) { return KeyCode.Exclaim; }
        else if (Input.GetKey(KeyCode.DoubleQuote)) { return KeyCode.DoubleQuote; }
        else if (Input.GetKey(KeyCode.Hash)) { return KeyCode.Hash; }
        else if (Input.GetKey(KeyCode.Dollar)) { return KeyCode.Dollar; }
        else if (Input.GetKey(KeyCode.Ampersand)) { return KeyCode.Ampersand; }
        else if (Input.GetKey(KeyCode.Quote)) { return KeyCode.Quote; }
        else if (Input.GetKey(KeyCode.LeftParen)) { return KeyCode.LeftParen; }
        else if (Input.GetKey(KeyCode.RightParen)) { return KeyCode.RightParen; }
        else if (Input.GetKey(KeyCode.Asterisk)) { return KeyCode.Asterisk; }
        else if (Input.GetKey(KeyCode.Plus)) { return KeyCode.Plus; }
        else if (Input.GetKey(KeyCode.Comma)) { return KeyCode.Comma; }
        else if (Input.GetKey(KeyCode.Minus)) { return KeyCode.Minus; }
        else if (Input.GetKey(KeyCode.Period)) { return KeyCode.Period; }
        else if (Input.GetKey(KeyCode.Slash)) { return KeyCode.Slash; }
        else if (Input.GetKey(KeyCode.Alpha0)) { return KeyCode.Alpha0; }
        else if (Input.GetKey(KeyCode.Alpha1)) { return KeyCode.Alpha1; }
        else if (Input.GetKey(KeyCode.Alpha2)) { return KeyCode.Alpha2; }
        else if (Input.GetKey(KeyCode.Alpha3)) { return KeyCode.Alpha3; }
        else if (Input.GetKey(KeyCode.Alpha4)) { return KeyCode.Alpha4; }
        else if (Input.GetKey(KeyCode.Alpha5)) { return KeyCode.Alpha5; }
        else if (Input.GetKey(KeyCode.Alpha6)) { return KeyCode.Alpha6; }
        else if (Input.GetKey(KeyCode.Alpha7)) { return KeyCode.Alpha7; }
        else if (Input.GetKey(KeyCode.Alpha8)) { return KeyCode.Alpha8; }
        else if (Input.GetKey(KeyCode.Alpha9)) { return KeyCode.Alpha9; }
        else if (Input.GetKey(KeyCode.Colon)) { return KeyCode.Colon; }
        else if (Input.GetKey(KeyCode.Semicolon)) { return KeyCode.Semicolon; }
        else if (Input.GetKey(KeyCode.Less)) { return KeyCode.Less; }
        else if (Input.GetKey(KeyCode.Equals)) { return KeyCode.Equals; }
        else if (Input.GetKey(KeyCode.Greater)) { return KeyCode.Greater; }
        else if (Input.GetKey(KeyCode.Question)) { return KeyCode.Question; }
        else if (Input.GetKey(KeyCode.At)) { return KeyCode.At; }
        else if (Input.GetKey(KeyCode.LeftBracket)) { return KeyCode.LeftBracket; }
        else if (Input.GetKey(KeyCode.Backslash)) { return KeyCode.Backslash; }
        else if (Input.GetKey(KeyCode.RightBracket)) { return KeyCode.RightBracket; }
        else if (Input.GetKey(KeyCode.Caret)) { return KeyCode.Caret; }
        else if (Input.GetKey(KeyCode.Underscore)) { return KeyCode.Underscore; }
        else if (Input.GetKey(KeyCode.BackQuote)) { return KeyCode.BackQuote; }
        else if (Input.GetKey(KeyCode.A)) { return KeyCode.A; }
        else if (Input.GetKey(KeyCode.B)) { return KeyCode.B; }
        else if (Input.GetKey(KeyCode.C)) { return KeyCode.C; }
        else if (Input.GetKey(KeyCode.D)) { return KeyCode.D; }
        else if (Input.GetKey(KeyCode.E)) { return KeyCode.E; }
        else if (Input.GetKey(KeyCode.F)) { return KeyCode.F; }
        else if (Input.GetKey(KeyCode.G)) { return KeyCode.G; }
        else if (Input.GetKey(KeyCode.H)) { return KeyCode.H; }
        else if (Input.GetKey(KeyCode.I)) { return KeyCode.I; }
        else if (Input.GetKey(KeyCode.J)) { return KeyCode.J; }
        else if (Input.GetKey(KeyCode.K)) { return KeyCode.K; }
        else if (Input.GetKey(KeyCode.L)) { return KeyCode.L; }
        else if (Input.GetKey(KeyCode.M)) { return KeyCode.M; }
        else if (Input.GetKey(KeyCode.N)) { return KeyCode.N; }
        else if (Input.GetKey(KeyCode.O)) { return KeyCode.O; }
        else if (Input.GetKey(KeyCode.P)) { return KeyCode.P; }
        else if (Input.GetKey(KeyCode.Q)) { return KeyCode.Q; }
        else if (Input.GetKey(KeyCode.R)) { return KeyCode.R; }
        else if (Input.GetKey(KeyCode.S)) { return KeyCode.S; }
        else if (Input.GetKey(KeyCode.T)) { return KeyCode.T; }
        else if (Input.GetKey(KeyCode.U)) { return KeyCode.U; }
        else if (Input.GetKey(KeyCode.V)) { return KeyCode.V; }
        else if (Input.GetKey(KeyCode.W)) { return KeyCode.W; }
        else if (Input.GetKey(KeyCode.X)) { return KeyCode.X; }
        else if (Input.GetKey(KeyCode.Y)) { return KeyCode.Y; }
        else if (Input.GetKey(KeyCode.Z)) { return KeyCode.Z; }
        else if (Input.GetKey(KeyCode.Delete)) { return KeyCode.Delete; }
        else if (Input.GetKey(KeyCode.Keypad0)) { return KeyCode.Keypad0; }
        else if (Input.GetKey(KeyCode.Keypad1)) { return KeyCode.Keypad1; }
        else if (Input.GetKey(KeyCode.Keypad2)) { return KeyCode.Keypad2; }
        else if (Input.GetKey(KeyCode.Keypad3)) { return KeyCode.Keypad3; }
        else if (Input.GetKey(KeyCode.Keypad4)) { return KeyCode.Keypad4; }
        else if (Input.GetKey(KeyCode.Keypad5)) { return KeyCode.Keypad5; }
        else if (Input.GetKey(KeyCode.Keypad6)) { return KeyCode.Keypad6; }
        else if (Input.GetKey(KeyCode.Keypad7)) { return KeyCode.Keypad7; }
        else if (Input.GetKey(KeyCode.Keypad8)) { return KeyCode.Keypad8; }
        else if (Input.GetKey(KeyCode.Keypad9)) { return KeyCode.Keypad9; }
        else if (Input.GetKey(KeyCode.KeypadPeriod)) { return KeyCode.KeypadPeriod; }
        else if (Input.GetKey(KeyCode.KeypadDivide)) { return KeyCode.KeypadDivide; }
        else if (Input.GetKey(KeyCode.KeypadMultiply)) { return KeyCode.KeypadMultiply; }
        else if (Input.GetKey(KeyCode.KeypadMinus)) { return KeyCode.KeypadMinus; }
        else if (Input.GetKey(KeyCode.KeypadPlus)) { return KeyCode.KeypadPlus; }
        else if (Input.GetKey(KeyCode.KeypadEnter)) { return KeyCode.KeypadEnter; }
        else if (Input.GetKey(KeyCode.KeypadEquals)) { return KeyCode.KeypadEquals; }
        else if (Input.GetKey(KeyCode.UpArrow)) { return KeyCode.UpArrow; }
        else if (Input.GetKey(KeyCode.DownArrow)) { return KeyCode.DownArrow; }
        else if (Input.GetKey(KeyCode.RightArrow)) { return KeyCode.RightArrow; }
        else if (Input.GetKey(KeyCode.LeftArrow)) { return KeyCode.LeftArrow; }
        else if (Input.GetKey(KeyCode.Insert)) { return KeyCode.Insert; }
        else if (Input.GetKey(KeyCode.Home)) { return KeyCode.Home; }
        else if (Input.GetKey(KeyCode.End)) { return KeyCode.End; }
        else if (Input.GetKey(KeyCode.PageUp)) { return KeyCode.PageUp; }
        else if (Input.GetKey(KeyCode.PageDown)) { return KeyCode.PageDown; }
        else if (Input.GetKey(KeyCode.F1)) { return KeyCode.F1; }
        else if (Input.GetKey(KeyCode.F2)) { return KeyCode.F2; }
        else if (Input.GetKey(KeyCode.F3)) { return KeyCode.F3; }
        else if (Input.GetKey(KeyCode.F4)) { return KeyCode.F4; }
        else if (Input.GetKey(KeyCode.F5)) { return KeyCode.F5; }
        else if (Input.GetKey(KeyCode.F6)) { return KeyCode.F6; }
        else if (Input.GetKey(KeyCode.F7)) { return KeyCode.F7; }
        else if (Input.GetKey(KeyCode.F8)) { return KeyCode.F8; }
        else if (Input.GetKey(KeyCode.F9)) { return KeyCode.F9; }
        else if (Input.GetKey(KeyCode.F10)) { return KeyCode.F10; }
        else if (Input.GetKey(KeyCode.F11)) { return KeyCode.F11; }
        else if (Input.GetKey(KeyCode.F12)) { return KeyCode.F12; }
        else if (Input.GetKey(KeyCode.F13)) { return KeyCode.F13; }
        else if (Input.GetKey(KeyCode.F14)) { return KeyCode.F14; }
        else if (Input.GetKey(KeyCode.F15)) { return KeyCode.F15; }
        else if (Input.GetKey(KeyCode.Numlock)) { return KeyCode.Numlock; }
        else if (Input.GetKey(KeyCode.CapsLock)) { return KeyCode.CapsLock; }
        else if (Input.GetKey(KeyCode.ScrollLock)) { return KeyCode.ScrollLock; }
        else if (Input.GetKey(KeyCode.RightShift)) { return KeyCode.RightShift; }
        else if (Input.GetKey(KeyCode.LeftShift)) { return KeyCode.LeftShift; }
        else if (Input.GetKey(KeyCode.RightControl)) { return KeyCode.RightControl; }
        else if (Input.GetKey(KeyCode.LeftControl)) { return KeyCode.LeftControl; }
        else if (Input.GetKey(KeyCode.RightAlt)) { return KeyCode.RightAlt; }
        else if (Input.GetKey(KeyCode.LeftAlt)) { return KeyCode.LeftAlt; }
        else if (Input.GetKey(KeyCode.RightCommand)) { return KeyCode.RightCommand; }
        else if (Input.GetKey(KeyCode.RightApple)) { return KeyCode.RightApple; }
        else if (Input.GetKey(KeyCode.LeftCommand)) { return KeyCode.LeftCommand; }
        else if (Input.GetKey(KeyCode.LeftApple)) { return KeyCode.LeftApple; }
        else if (Input.GetKey(KeyCode.LeftWindows)) { return KeyCode.LeftWindows; }
        else if (Input.GetKey(KeyCode.RightWindows)) { return KeyCode.RightWindows; }
        else if (Input.GetKey(KeyCode.AltGr)) { return KeyCode.AltGr; }
        else if (Input.GetKey(KeyCode.Help)) { return KeyCode.Help; }
        else if (Input.GetKey(KeyCode.Print)) { return KeyCode.Print; }
        else if (Input.GetKey(KeyCode.SysReq)) { return KeyCode.SysReq; }
        else if (Input.GetKey(KeyCode.Break)) { return KeyCode.Break; }
        else if (Input.GetKey(KeyCode.Menu)) { return KeyCode.Menu; }
        else if (Input.GetKey(KeyCode.Mouse0)) { return KeyCode.Mouse0; }
        else if (Input.GetKey(KeyCode.Mouse1)) { return KeyCode.Mouse1; }
        else if (Input.GetKey(KeyCode.Mouse2)) { return KeyCode.Mouse2; }
        else if (Input.GetKey(KeyCode.Mouse3)) { return KeyCode.Mouse3; }
        else if (Input.GetKey(KeyCode.Mouse4)) { return KeyCode.Mouse4; }
        else if (Input.GetKey(KeyCode.Mouse5)) { return KeyCode.Mouse5; }
        else if (Input.GetKey(KeyCode.Mouse6)) { return KeyCode.Mouse6; }
        else if (Input.GetKey(KeyCode.JoystickButton0)) { return KeyCode.JoystickButton0; }
        else if (Input.GetKey(KeyCode.JoystickButton1)) { return KeyCode.JoystickButton1; }
        else if (Input.GetKey(KeyCode.JoystickButton2)) { return KeyCode.JoystickButton2; }
        else if (Input.GetKey(KeyCode.JoystickButton3)) { return KeyCode.JoystickButton3; }
        else if (Input.GetKey(KeyCode.JoystickButton4)) { return KeyCode.JoystickButton4; }
        else if (Input.GetKey(KeyCode.JoystickButton5)) { return KeyCode.JoystickButton5; }
        else if (Input.GetKey(KeyCode.JoystickButton6)) { return KeyCode.JoystickButton6; }
        else if (Input.GetKey(KeyCode.JoystickButton7)) { return KeyCode.JoystickButton7; }
        else if (Input.GetKey(KeyCode.JoystickButton8)) { return KeyCode.JoystickButton8; }
        else if (Input.GetKey(KeyCode.JoystickButton9)) { return KeyCode.JoystickButton9; }
        else if (Input.GetKey(KeyCode.JoystickButton10)) { return KeyCode.JoystickButton10; }
        else if (Input.GetKey(KeyCode.JoystickButton11)) { return KeyCode.JoystickButton11; }
        else if (Input.GetKey(KeyCode.JoystickButton12)) { return KeyCode.JoystickButton12; }
        else if (Input.GetKey(KeyCode.JoystickButton13)) { return KeyCode.JoystickButton13; }
        else if (Input.GetKey(KeyCode.JoystickButton14)) { return KeyCode.JoystickButton14; }
        else if (Input.GetKey(KeyCode.JoystickButton15)) { return KeyCode.JoystickButton15; }
        else if (Input.GetKey(KeyCode.JoystickButton16)) { return KeyCode.JoystickButton16; }
        else if (Input.GetKey(KeyCode.JoystickButton17)) { return KeyCode.JoystickButton17; }
        else if (Input.GetKey(KeyCode.JoystickButton18)) { return KeyCode.JoystickButton18; }
        else if (Input.GetKey(KeyCode.JoystickButton19)) { return KeyCode.JoystickButton19; }
        else if (Input.GetKey(KeyCode.Joystick1Button0)) { return KeyCode.Joystick1Button0; }
        else if (Input.GetKey(KeyCode.Joystick1Button1)) { return KeyCode.Joystick1Button1; }
        else if (Input.GetKey(KeyCode.Joystick1Button2)) { return KeyCode.Joystick1Button2; }
        else if (Input.GetKey(KeyCode.Joystick1Button3)) { return KeyCode.Joystick1Button3; }
        else if (Input.GetKey(KeyCode.Joystick1Button4)) { return KeyCode.Joystick1Button4; }
        else if (Input.GetKey(KeyCode.Joystick1Button5)) { return KeyCode.Joystick1Button5; }
        else if (Input.GetKey(KeyCode.Joystick1Button6)) { return KeyCode.Joystick1Button6; }
        else if (Input.GetKey(KeyCode.Joystick1Button7)) { return KeyCode.Joystick1Button7; }
        else if (Input.GetKey(KeyCode.Joystick1Button8)) { return KeyCode.Joystick1Button8; }
        else if (Input.GetKey(KeyCode.Joystick1Button9)) { return KeyCode.Joystick1Button9; }
        else if (Input.GetKey(KeyCode.Joystick1Button10)) { return KeyCode.Joystick1Button10; }
        else if (Input.GetKey(KeyCode.Joystick1Button11)) { return KeyCode.Joystick1Button11; }
        else if (Input.GetKey(KeyCode.Joystick1Button12)) { return KeyCode.Joystick1Button12; }
        else if (Input.GetKey(KeyCode.Joystick1Button13)) { return KeyCode.Joystick1Button13; }
        else if (Input.GetKey(KeyCode.Joystick1Button14)) { return KeyCode.Joystick1Button14; }
        else if (Input.GetKey(KeyCode.Joystick1Button15)) { return KeyCode.Joystick1Button15; }
        else if (Input.GetKey(KeyCode.Joystick1Button16)) { return KeyCode.Joystick1Button16; }
        else if (Input.GetKey(KeyCode.Joystick1Button17)) { return KeyCode.Joystick1Button17; }
        else if (Input.GetKey(KeyCode.Joystick1Button18)) { return KeyCode.Joystick1Button18; }
        else if (Input.GetKey(KeyCode.Joystick1Button19)) { return KeyCode.Joystick1Button19; }
        else if (Input.GetKey(KeyCode.Joystick2Button0)) { return KeyCode.Joystick2Button0; }
        else if (Input.GetKey(KeyCode.Joystick2Button1)) { return KeyCode.Joystick2Button1; }
        else if (Input.GetKey(KeyCode.Joystick2Button2)) { return KeyCode.Joystick2Button2; }
        else if (Input.GetKey(KeyCode.Joystick2Button3)) { return KeyCode.Joystick2Button3; }
        else if (Input.GetKey(KeyCode.Joystick2Button4)) { return KeyCode.Joystick2Button4; }
        else if (Input.GetKey(KeyCode.Joystick2Button5)) { return KeyCode.Joystick2Button5; }
        else if (Input.GetKey(KeyCode.Joystick2Button6)) { return KeyCode.Joystick2Button6; }
        else if (Input.GetKey(KeyCode.Joystick2Button7)) { return KeyCode.Joystick2Button7; }
        else if (Input.GetKey(KeyCode.Joystick2Button8)) { return KeyCode.Joystick2Button8; }
        else if (Input.GetKey(KeyCode.Joystick2Button9)) { return KeyCode.Joystick2Button9; }
        else if (Input.GetKey(KeyCode.Joystick2Button10)) { return KeyCode.Joystick2Button10; }
        else if (Input.GetKey(KeyCode.Joystick2Button11)) { return KeyCode.Joystick2Button11; }
        else if (Input.GetKey(KeyCode.Joystick2Button12)) { return KeyCode.Joystick2Button12; }
        else if (Input.GetKey(KeyCode.Joystick2Button13)) { return KeyCode.Joystick2Button13; }
        else if (Input.GetKey(KeyCode.Joystick2Button14)) { return KeyCode.Joystick2Button14; }
        else if (Input.GetKey(KeyCode.Joystick2Button15)) { return KeyCode.Joystick2Button15; }
        else if (Input.GetKey(KeyCode.Joystick2Button16)) { return KeyCode.Joystick2Button16; }
        else if (Input.GetKey(KeyCode.Joystick2Button17)) { return KeyCode.Joystick2Button17; }
        else if (Input.GetKey(KeyCode.Joystick2Button18)) { return KeyCode.Joystick2Button18; }
        else if (Input.GetKey(KeyCode.Joystick2Button19)) { return KeyCode.Joystick2Button19; }
        else if (Input.GetKey(KeyCode.Joystick3Button0)) { return KeyCode.Joystick3Button0; }
        else if (Input.GetKey(KeyCode.Joystick3Button1)) { return KeyCode.Joystick3Button1; }
        else if (Input.GetKey(KeyCode.Joystick3Button2)) { return KeyCode.Joystick3Button2; }
        else if (Input.GetKey(KeyCode.Joystick3Button3)) { return KeyCode.Joystick3Button3; }
        else if (Input.GetKey(KeyCode.Joystick3Button4)) { return KeyCode.Joystick3Button4; }
        else if (Input.GetKey(KeyCode.Joystick3Button5)) { return KeyCode.Joystick3Button5; }
        else if (Input.GetKey(KeyCode.Joystick3Button6)) { return KeyCode.Joystick3Button6; }
        else if (Input.GetKey(KeyCode.Joystick3Button7)) { return KeyCode.Joystick3Button7; }
        else if (Input.GetKey(KeyCode.Joystick3Button8)) { return KeyCode.Joystick3Button8; }
        else if (Input.GetKey(KeyCode.Joystick3Button9)) { return KeyCode.Joystick3Button9; }
        else if (Input.GetKey(KeyCode.Joystick3Button10)) { return KeyCode.Joystick3Button10; }
        else if (Input.GetKey(KeyCode.Joystick3Button11)) { return KeyCode.Joystick3Button11; }
        else if (Input.GetKey(KeyCode.Joystick3Button12)) { return KeyCode.Joystick3Button12; }
        else if (Input.GetKey(KeyCode.Joystick3Button13)) { return KeyCode.Joystick3Button13; }
        else if (Input.GetKey(KeyCode.Joystick3Button14)) { return KeyCode.Joystick3Button14; }
        else if (Input.GetKey(KeyCode.Joystick3Button15)) { return KeyCode.Joystick3Button15; }
        else if (Input.GetKey(KeyCode.Joystick3Button16)) { return KeyCode.Joystick3Button16; }
        else if (Input.GetKey(KeyCode.Joystick3Button17)) { return KeyCode.Joystick3Button17; }
        else if (Input.GetKey(KeyCode.Joystick3Button18)) { return KeyCode.Joystick3Button18; }
        else if (Input.GetKey(KeyCode.Joystick3Button19)) { return KeyCode.Joystick3Button19; }
        else if (Input.GetKey(KeyCode.Joystick4Button0)) { return KeyCode.Joystick4Button0; }
        else if (Input.GetKey(KeyCode.Joystick4Button1)) { return KeyCode.Joystick4Button1; }
        else if (Input.GetKey(KeyCode.Joystick4Button2)) { return KeyCode.Joystick4Button2; }
        else if (Input.GetKey(KeyCode.Joystick4Button3)) { return KeyCode.Joystick4Button3; }
        else if (Input.GetKey(KeyCode.Joystick4Button4)) { return KeyCode.Joystick4Button4; }
        else if (Input.GetKey(KeyCode.Joystick4Button5)) { return KeyCode.Joystick4Button5; }
        else if (Input.GetKey(KeyCode.Joystick4Button6)) { return KeyCode.Joystick4Button6; }
        else if (Input.GetKey(KeyCode.Joystick4Button7)) { return KeyCode.Joystick4Button7; }
        else if (Input.GetKey(KeyCode.Joystick4Button8)) { return KeyCode.Joystick4Button8; }
        else if (Input.GetKey(KeyCode.Joystick4Button9)) { return KeyCode.Joystick4Button9; }
        else if (Input.GetKey(KeyCode.Joystick4Button10)) { return KeyCode.Joystick4Button10; }
        else if (Input.GetKey(KeyCode.Joystick4Button11)) { return KeyCode.Joystick4Button11; }
        else if (Input.GetKey(KeyCode.Joystick4Button12)) { return KeyCode.Joystick4Button12; }
        else if (Input.GetKey(KeyCode.Joystick4Button13)) { return KeyCode.Joystick4Button13; }
        else if (Input.GetKey(KeyCode.Joystick4Button14)) { return KeyCode.Joystick4Button14; }
        else if (Input.GetKey(KeyCode.Joystick4Button15)) { return KeyCode.Joystick4Button15; }
        else if (Input.GetKey(KeyCode.Joystick4Button16)) { return KeyCode.Joystick4Button16; }
        else if (Input.GetKey(KeyCode.Joystick4Button17)) { return KeyCode.Joystick4Button17; }
        else if (Input.GetKey(KeyCode.Joystick4Button18)) { return KeyCode.Joystick4Button18; }
        else if (Input.GetKey(KeyCode.Joystick4Button19)) { return KeyCode.Joystick4Button19; }
        else if (Input.GetKey(KeyCode.Joystick5Button0)) { return KeyCode.Joystick5Button0; }
        else if (Input.GetKey(KeyCode.Joystick5Button1)) { return KeyCode.Joystick5Button1; }
        else if (Input.GetKey(KeyCode.Joystick5Button2)) { return KeyCode.Joystick5Button2; }
        else if (Input.GetKey(KeyCode.Joystick5Button3)) { return KeyCode.Joystick5Button3; }
        else if (Input.GetKey(KeyCode.Joystick5Button4)) { return KeyCode.Joystick5Button4; }
        else if (Input.GetKey(KeyCode.Joystick5Button5)) { return KeyCode.Joystick5Button5; }
        else if (Input.GetKey(KeyCode.Joystick5Button6)) { return KeyCode.Joystick5Button6; }
        else if (Input.GetKey(KeyCode.Joystick5Button7)) { return KeyCode.Joystick5Button7; }
        else if (Input.GetKey(KeyCode.Joystick5Button8)) { return KeyCode.Joystick5Button8; }
        else if (Input.GetKey(KeyCode.Joystick5Button9)) { return KeyCode.Joystick5Button9; }
        else if (Input.GetKey(KeyCode.Joystick5Button10)) { return KeyCode.Joystick5Button10; }
        else if (Input.GetKey(KeyCode.Joystick5Button11)) { return KeyCode.Joystick5Button11; }
        else if (Input.GetKey(KeyCode.Joystick5Button12)) { return KeyCode.Joystick5Button12; }
        else if (Input.GetKey(KeyCode.Joystick5Button13)) { return KeyCode.Joystick5Button13; }
        else if (Input.GetKey(KeyCode.Joystick5Button14)) { return KeyCode.Joystick5Button14; }
        else if (Input.GetKey(KeyCode.Joystick5Button15)) { return KeyCode.Joystick5Button15; }
        else if (Input.GetKey(KeyCode.Joystick5Button16)) { return KeyCode.Joystick5Button16; }
        else if (Input.GetKey(KeyCode.Joystick5Button17)) { return KeyCode.Joystick5Button17; }
        else if (Input.GetKey(KeyCode.Joystick5Button18)) { return KeyCode.Joystick5Button18; }
        else if (Input.GetKey(KeyCode.Joystick5Button19)) { return KeyCode.Joystick5Button19; }
        else if (Input.GetKey(KeyCode.Joystick6Button0)) { return KeyCode.Joystick6Button0; }
        else if (Input.GetKey(KeyCode.Joystick6Button1)) { return KeyCode.Joystick6Button1; }
        else if (Input.GetKey(KeyCode.Joystick6Button2)) { return KeyCode.Joystick6Button2; }
        else if (Input.GetKey(KeyCode.Joystick6Button3)) { return KeyCode.Joystick6Button3; }
        else if (Input.GetKey(KeyCode.Joystick6Button4)) { return KeyCode.Joystick6Button4; }
        else if (Input.GetKey(KeyCode.Joystick6Button5)) { return KeyCode.Joystick6Button5; }
        else if (Input.GetKey(KeyCode.Joystick6Button6)) { return KeyCode.Joystick6Button6; }
        else if (Input.GetKey(KeyCode.Joystick6Button7)) { return KeyCode.Joystick6Button7; }
        else if (Input.GetKey(KeyCode.Joystick6Button8)) { return KeyCode.Joystick6Button8; }
        else if (Input.GetKey(KeyCode.Joystick6Button9)) { return KeyCode.Joystick6Button9; }
        else if (Input.GetKey(KeyCode.Joystick6Button10)) { return KeyCode.Joystick6Button10; }
        else if (Input.GetKey(KeyCode.Joystick6Button11)) { return KeyCode.Joystick6Button11; }
        else if (Input.GetKey(KeyCode.Joystick6Button12)) { return KeyCode.Joystick6Button12; }
        else if (Input.GetKey(KeyCode.Joystick6Button13)) { return KeyCode.Joystick6Button13; }
        else if (Input.GetKey(KeyCode.Joystick6Button14)) { return KeyCode.Joystick6Button14; }
        else if (Input.GetKey(KeyCode.Joystick6Button15)) { return KeyCode.Joystick6Button15; }
        else if (Input.GetKey(KeyCode.Joystick6Button16)) { return KeyCode.Joystick6Button16; }
        else if (Input.GetKey(KeyCode.Joystick6Button17)) { return KeyCode.Joystick6Button17; }
        else if (Input.GetKey(KeyCode.Joystick6Button18)) { return KeyCode.Joystick6Button18; }
        else if (Input.GetKey(KeyCode.Joystick6Button19)) { return KeyCode.Joystick6Button19; }
        else if (Input.GetKey(KeyCode.Joystick7Button0)) { return KeyCode.Joystick7Button0; }
        else if (Input.GetKey(KeyCode.Joystick7Button1)) { return KeyCode.Joystick7Button1; }
        else if (Input.GetKey(KeyCode.Joystick7Button2)) { return KeyCode.Joystick7Button2; }
        else if (Input.GetKey(KeyCode.Joystick7Button3)) { return KeyCode.Joystick7Button3; }
        else if (Input.GetKey(KeyCode.Joystick7Button4)) { return KeyCode.Joystick7Button4; }
        else if (Input.GetKey(KeyCode.Joystick7Button5)) { return KeyCode.Joystick7Button5; }
        else if (Input.GetKey(KeyCode.Joystick7Button6)) { return KeyCode.Joystick7Button6; }
        else if (Input.GetKey(KeyCode.Joystick7Button7)) { return KeyCode.Joystick7Button7; }
        else if (Input.GetKey(KeyCode.Joystick7Button8)) { return KeyCode.Joystick7Button8; }
        else if (Input.GetKey(KeyCode.Joystick7Button9)) { return KeyCode.Joystick7Button9; }
        else if (Input.GetKey(KeyCode.Joystick7Button10)) { return KeyCode.Joystick7Button10; }
        else if (Input.GetKey(KeyCode.Joystick7Button11)) { return KeyCode.Joystick7Button11; }
        else if (Input.GetKey(KeyCode.Joystick7Button12)) { return KeyCode.Joystick7Button12; }
        else if (Input.GetKey(KeyCode.Joystick7Button13)) { return KeyCode.Joystick7Button13; }
        else if (Input.GetKey(KeyCode.Joystick7Button14)) { return KeyCode.Joystick7Button14; }
        else if (Input.GetKey(KeyCode.Joystick7Button15)) { return KeyCode.Joystick7Button15; }
        else if (Input.GetKey(KeyCode.Joystick7Button16)) { return KeyCode.Joystick7Button16; }
        else if (Input.GetKey(KeyCode.Joystick7Button17)) { return KeyCode.Joystick7Button17; }
        else if (Input.GetKey(KeyCode.Joystick7Button18)) { return KeyCode.Joystick7Button18; }
        else if (Input.GetKey(KeyCode.Joystick7Button19)) { return KeyCode.Joystick7Button19; }
        else if (Input.GetKey(KeyCode.Joystick8Button0)) { return KeyCode.Joystick8Button0; }
        else if (Input.GetKey(KeyCode.Joystick8Button1)) { return KeyCode.Joystick8Button1; }
        else if (Input.GetKey(KeyCode.Joystick8Button2)) { return KeyCode.Joystick8Button2; }
        else if (Input.GetKey(KeyCode.Joystick8Button3)) { return KeyCode.Joystick8Button3; }
        else if (Input.GetKey(KeyCode.Joystick8Button4)) { return KeyCode.Joystick8Button4; }
        else if (Input.GetKey(KeyCode.Joystick8Button5)) { return KeyCode.Joystick8Button5; }
        else if (Input.GetKey(KeyCode.Joystick8Button6)) { return KeyCode.Joystick8Button6; }
        else if (Input.GetKey(KeyCode.Joystick8Button7)) { return KeyCode.Joystick8Button7; }
        else if (Input.GetKey(KeyCode.Joystick8Button8)) { return KeyCode.Joystick8Button8; }
        else if (Input.GetKey(KeyCode.Joystick8Button9)) { return KeyCode.Joystick8Button9; }
        else if (Input.GetKey(KeyCode.Joystick8Button10)) { return KeyCode.Joystick8Button10; }
        else if (Input.GetKey(KeyCode.Joystick8Button11)) { return KeyCode.Joystick8Button11; }
        else if (Input.GetKey(KeyCode.Joystick8Button12)) { return KeyCode.Joystick8Button12; }
        else if (Input.GetKey(KeyCode.Joystick8Button13)) { return KeyCode.Joystick8Button13; }
        else if (Input.GetKey(KeyCode.Joystick8Button14)) { return KeyCode.Joystick8Button14; }
        else if (Input.GetKey(KeyCode.Joystick8Button15)) { return KeyCode.Joystick8Button15; }
        else if (Input.GetKey(KeyCode.Joystick8Button16)) { return KeyCode.Joystick8Button16; }
        else if (Input.GetKey(KeyCode.Joystick8Button17)) { return KeyCode.Joystick8Button17; }
        else if (Input.GetKey(KeyCode.Joystick8Button18)) { return KeyCode.Joystick8Button18; }
        else if (Input.GetKey(KeyCode.Joystick8Button19)) { return KeyCode.Joystick8Button19; }
        return KeyCode.None;
    }
}

