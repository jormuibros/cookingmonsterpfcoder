using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="HeroData", menuName="Hero Data")]
public class HeroData : ScriptableObject
{
    [SerializeField] private string heroName;
    [SerializeField] private int heroLife;
    [SerializeField] private int heroPrimaryDamage;
    [SerializeField] private int heroSecondaryDamage;
    [SerializeField] private float heroRunSpeed = 10f;
    [SerializeField] private float heroSpeedRotation = 100f;
    [SerializeField] private float heroJumpforce = 8f;
    [SerializeField] private int herodashSpeed = 50;

public string HeroName
{
    get
    {
        return heroName;
    }
}

public int HeroLife
{
    get
    {
        return heroLife;
    }
}

public int HeroPrimaryDamage
{
    get
    {
        return heroPrimaryDamage;
    }
}

public int HeroSecondaryDamage
{
    get
    {
        return heroSecondaryDamage;
    }
}
      public float HeroRunSpeed
    {
        get
        {
            return heroRunSpeed;
        }
    }
      public float HeroSpeedRotation
    {
        get
        {
            return heroSpeedRotation;
        }
    }

      public float HeroJumpforce
    {
        get
        {
            return heroJumpforce;
        }
    }
      public float HerodashSpeed
    {
        get
        {
            return herodashSpeed;
        }
    }

}