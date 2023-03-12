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
    public class PlayerStatsData
    {
        public int HU; //humanity
        public int BT; //bloodthirst
    }

    [Serializable]
    public class BaseStatsData
    {
        public int LVL; // Level
        public int EXP; // Experience
        public int HP; // Hit Points
        
        public int MHP; // Max Hit Points
        public int STR; // Strength
        public int STA; // Stamina
        public int DEX; // Dexterity
        public int DEF; // Physc Def
        public int INT; // Intelligence
        public int CHR; // Charisma

        public BaseStatsData(int lvl, int exp, int hp, int mhp, int str, int def, int sta, int @int, int dex, int chr)
        {
            LVL = lvl;
            EXP = exp;
            HP = hp;
            MHP = mhp;
            STR = str;
            DEF = def;
            STA = sta;
            INT = @int;
            DEX = dex;
            CHR = chr;
        }
    }

    public class SkillsData
    {

    }
}
