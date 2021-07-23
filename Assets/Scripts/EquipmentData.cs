using UnityEngine;
using System.Collections.Generic;
using System;

//?????f?[?^?N???X(?\??)
public class EquipmentData:MonoBehaviour
{
    public enum WeaponName
    {
        Buki1 = 0,
        Buki2 = 1,
        Buki3 = 2,
        Buki4 = 3
    }

    //???????(?????????)???
    //??
    [Serializable] //???????????????
    public struct Weapon
    {
        public Sprite iconImage;
        public int damageValue;
        public float attackSpace;
        public float coolTime;
        public int energyComsumption;
        public int maxEnergy;
    }
    //??
    public struct Armor
    {
        public Sprite iconImage;
        public int damageCutValue;
    }

    //???????
    public Weapon[] weaponsArray;
    //???????
    public Armor[] armorsArray;

}
