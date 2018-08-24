using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
public class WeaponFreeHandEnemy : WeaponBase
{
    public WeaponEffectType WEType;
    private static WeaponFreeHandEnemy instance;

    public static WeaponFreeHandEnemy Instance
    {
        get
        {
            if (CommonFunction.IsNull(instance) == false)
            {
                return instance;
            }
            instance = new WeaponFreeHandEnemy();
            instance.Initialize();
            return instance;
        }
    }

    public override void AttackEffect(BaseCharacter target, BaseCharacter attacker, string damage, AttackState state,AttackInformation atinfo)
    {
        switch (WEType)
        {
            case WeaponEffectType.Stone:
                atinfo.AddEffect(EffectStoneThrow.CreateObject(target, attacker, damage, state));
                break;
            case WeaponEffectType.MachineGun:
                atinfo.AddEffect(
                    EffectGunFIre.CreateObject(target, attacker, damage, state));
                break;
            default:
                base.AttackEffect(target, attacker, damage, state, atinfo);
                break;
        }
    }

    protected override SoundInformation.SoundType GetAttackHitSound()
    {
        switch (WEType)
        {
            case WeaponEffectType.Stone:
                return SoundInformation.SoundType.Shelling;
                break;
            case WeaponEffectType.MachineGun:
                return SoundInformation.SoundType.Machinegun;
                break;
            default:
                return SoundInformation.SoundType.AttackHit;
                break;
        }
    }
    public static WeaponFreeHandEnemy CreateWeapon()
    {
        WeaponFreeHandEnemy i = new WeaponFreeHandEnemy();
        i.Initialize();
        return i;
    }

    public override void Initialize()
    {
        base.Initialize();
        DisplayName = CommonConst.Message.BareHands;
        WeaponBaseDexterity = 0.95f;
        WeaponBaseAttack = 5;
    }
}
