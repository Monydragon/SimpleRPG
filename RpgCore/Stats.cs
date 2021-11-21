using System;
using System.Collections.Generic;
using System.Text;

namespace RpgCore
{
        /// <summary>
        /// This is an EXAMPLE to demonstrate how to utilize the LevelHandler for keeping track of stats.
        /// </summary>
        [System.Serializable]
        public class Stats
        {
            #region Protected Variables

            protected LevelHandler m_Level = new LevelHandler();
            protected LevelHandler m_Strength = new LevelHandler();
            protected LevelHandler m_Dexterity = new LevelHandler();
            protected LevelHandler m_Constitution = new LevelHandler();
            protected LevelHandler m_Intelligence = new LevelHandler();
            protected LevelHandler m_Wisdom = new LevelHandler();
            protected LevelHandler m_Charisma = new LevelHandler();

        #endregion

        #region Public Properties

        public LevelHandler Level
            {
                get
                {
                    return m_Level;
                }

                set
                {
                    m_Level = value;
                }
            }

            public LevelHandler Strength
            {
                get
                {
                    return m_Strength;
                }

                set
                {
                    m_Strength = value;
                }
            }

            public LevelHandler Dexterity
            {
                get
                {
                    return m_Dexterity;
                }

                set
                {
                    m_Dexterity = value;
                }
            }

            public LevelHandler Constitution
            {
                get
                {
                    return m_Constitution;
                }

                set
                {
                    m_Constitution = value;
                }
            }

            public LevelHandler Intelligence
            {
                get
                {
                    return m_Intelligence;
                }

                set
                {
                    m_Intelligence = value;
                }
            }

            public LevelHandler Wisdom
            {
                get
                {
                    return m_Wisdom;
                }

                set
                {
                    m_Wisdom = value;
                }
            }

            public LevelHandler Charisma
            {
                get
                {
                    return m_Charisma;
                }

                set
                {
                    m_Charisma = value;
                }
            }

            #endregion

            #region Public Methods

            public void SetDefaultExpCurves()
            {
                m_Level.ExperienceCurve = LevelHandler.DefaultExpCurveList();
                m_Strength.ExperienceCurve = LevelHandler.DefaultExpCurveList();
                m_Dexterity.ExperienceCurve = LevelHandler.DefaultExpCurveList();
                m_Constitution.ExperienceCurve = LevelHandler.DefaultExpCurveList();
                m_Intelligence.ExperienceCurve = LevelHandler.DefaultExpCurveList();
                m_Wisdom.ExperienceCurve = LevelHandler.DefaultExpCurveList();
                m_Charisma.ExperienceCurve = LevelHandler.DefaultExpCurveList();
            }

            public void ChangeExpCurves(int _max_level, int _exp_curve, float _curve_multiplier = 1, params LevelHandler[] _stats)
            {
                for (int i = 0; i < _stats.Length; i++)
                {
                    _stats[i].ChangeExpCurve(_max_level, _exp_curve, _curve_multiplier);
                }
            }

        #endregion

        public override string ToString()
        {
            return $"Stats:\n" +
                $"Level: {Level.Level}\n" +
                $"Strength: {Strength.Level}\n" +
                $"Dexterity: {Dexterity.Level}\n" +
                $"Constitution: {Constitution.Level}\n" +
                $"Intelligence: {Intelligence.Level}\n" +
                $"Wisdom: {Wisdom.Level}\n" +
                $"Charisma: {Charisma.Level}";
        }
    }
        
    }
