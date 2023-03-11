using System;

namespace Game.Entites.Data
{
    
    [Serializable]
    public class StatsData
    {
        public BaseStatsData BaseStats; 
        public SkillsData SecondaryStats;
        
        public StatsData(BaseStatsData baseStats = null, SkillsData secondaryStats = null)
        {
            BaseStats = baseStats;
            SecondaryStats = secondaryStats;
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
    }

    public class SkillsData
    {

    }
}
