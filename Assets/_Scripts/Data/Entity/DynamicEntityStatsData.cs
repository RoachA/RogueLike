using System;

namespace Game.Entites.Data
{
    [Serializable]
    public class DynamicEntityStatsData
    {
        public BaseStatsData BaseStats; 
        public SkillsData Skills;
        
        public DynamicEntityStatsData(BaseStatsData baseStats = null, SkillsData skills = null)
        {
            BaseStats = baseStats;
            Skills = skills;
        }
    }

    [Serializable]
    public class BaseStatsData
    {
        public int LVL; // Level
        public int EXP; // Experience
        public int HP; // Hit Points
        public int MHP; // Max Hit Points
        public int ATK; // Physical Attack
        public int AV; // Armor Value
        public int MAT; // Magic Attack
        public int MDF; // Magic Defense
        public int DV; // DodgeValue
        public int RES; // Status Resistance
        public int SPD; // Speed
        public int MOV;

        public BaseStatsData(int lvl, int exp, int hp, int mhp, int atk, int av, int mat, int mdf, int dv, int res,
            int spd, int mov)
        {
            LVL = lvl;
            EXP = exp;
            HP = hp;
            MHP = mhp;
            ATK = atk;
            AV = av;
            MAT = mat;
            MDF = mdf;
            DV = dv;
            RES = res;
            SPD = spd;
            MOV = mov;
        }
        
        //todo >>>> so damn ugly yo.
        public void SetStat(BaseStatTypes stat, int val)
        {
            switch (stat)
            {
                case BaseStatTypes.LVL:
                    LVL = val;
                    break;
                case BaseStatTypes.EXP:
                    EXP = val;
                    break;
                case BaseStatTypes.HP:
                    HP = val;
                    break;
                case BaseStatTypes.MHP:
                    MHP = val;
                    break;
                case BaseStatTypes.ATK:
                    ATK = val;
                    break;
                case BaseStatTypes.AV:
                    AV = val;
                    break;
                case BaseStatTypes.MAT:
                    MAT = val;
                    break;
                case BaseStatTypes.MDF:
                    MDF = val;
                    break;
                case BaseStatTypes.DV:
                    DV = val;
                    break;
                case BaseStatTypes.RES:
                    RES = val;
                    break;
                case BaseStatTypes.SPD:
                    SPD = val;
                    break;
                case BaseStatTypes.MOV:
                    MOV = val;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stat), stat, null);
            }
        }
    }

    public class SkillsData
    {

    }
}
