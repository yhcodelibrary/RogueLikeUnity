using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.FieldObjects
{
    public class BaseStair : BaseItem
    {

        public override void Initialize()
        {
            base.Initialize();

            PosY = 2.9f;
            this.IsVisible = true;
            //this.ThrowPoint = MapPoint.Get(x, y);
            //this.CurrentPoint = this.ThrowPoint;
            //this.BeforePoint = this.ThrowPoint;
            this.Type = ObjectType.Stair;
        }

        public override void SetThisDisplayObject(int x, int y)
        {
            Direction = Direction;
            SetPositionThrow(x, y);
        }
    }
}
