using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class AnimationInformation : IDisposable
    {
        public static Dictionary<AnimationType, string> _Names;
        public static Dictionary<AnimationType,string> Names
        {
            get
            {
                if(CommonFunction.IsNull(_Names) == false)
                {
                    return _Names;
                }

                _Names = new Dictionary<AnimationType, string>(new AnimationTypeComparer());
                foreach (AnimationType val in CommonFunction.AnimationTypes)
                {
                    //メンバ名を取得する
                    string eName = Enum.GetName(typeof(AnimationType), val);
                    _Names.Add(val, eName);
                }
                return _Names;
            }
        }
        public AnimationType SpecialMove;
        public Dictionary<AnimationType, bool> Active;
        public Animator _anim;
        public Animator Anim
        {
            get
            {
                return _anim;
            }
            set
            {
                _anim = value;
                Initialize();
            }
        }

        public float Speed;

        public AnimationInformation()
        {
            Active = new Dictionary<AnimationType, bool>(new AnimationTypeComparer());
            SpecialMove = AnimationType.IsAttack;
        }

        private void Initialize()
        {
            Active.Clear();
            foreach (AnimationType t in CommonFunction.AnimationTypes)
            {
                Active.Add(t, false);
            }
            Speed = float.MinValue;
        }

        public void SetSpeed(float speed)
        {
            if(Speed != speed)
            {
                Speed = speed;
                Anim.speed = speed;
            }
        }
        public void OffAllAction()
        {
            foreach (AnimationType t in CommonFunction.AnimationTypes)
            {
                SetBool(t, false);
            }
        }

        public void SetBool(AnimationType t,bool b)
        {
            if (Active[t] != b)
            {
                Active[t] = b;
                Anim.SetBool(Names[t], b);
            }
        }

        public bool GetBool(AnimationType t)
        {
            return Active[t];
        }

        public AnimatorStateInfo GetCurrentAnimatorStateInfo()
        {
            return Anim.GetCurrentAnimatorStateInfo(0);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _anim = null;
                    Active = null;
                    // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
                }

                // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~AnimationInformation() {
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
}
