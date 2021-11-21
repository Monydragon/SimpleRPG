﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RpgCore
{
        /// <summary>
        /// This class is responsible for handling Levels and Experience for an object. This can be used as a container to be applied to anything that requires a m_Level and experience.
        /// </summary>
        [System.Serializable]
        public class LevelHandler
        {
            #region Global Class Variables

            /// <summary>
            /// This is a Static Default for the MAX m_Level to be generated automatically for the <see cref="m_Experience_Curve"/>
            /// </summary>
            public static int DEFAULT_MAX_LEVEL = 100;

            /// <summary>
            /// This is a Static Default for the EXP Curve variance to be generated automatically for the <see cref="m_Experience_Curve"/>
            /// </summary>0-
            public static int DEFAULT_BASE_EXP_CURVE = 50;

            /// <summary>
            /// This is a Static Default for the EXP Curve multiplier to be generated automatically for the <see cref="m_Experience_Curve"/>
            /// </summary>
            public static float DEFAULT_BASE_EXP_CURVE_MULTIPLIER = 1f;

            #endregion

            #region Protected Variables

            /// <summary>
            /// The current level.
            /// </summary>
            protected int m_Level = 1;

            /// <summary>
            /// The current experience.
            /// </summary>
            protected int m_Exp;

            /// <summary>
            /// This is the local Max m_Level, used to determine what is considered the MAX m_Level for this object.
            /// </summary>
            protected int m_MaxLevel = DEFAULT_MAX_LEVEL;

            /// <summary>
            /// This is the local Base Exp Curve, used to be able to easily overwrite the Experience Curve when called using the  <see cref="ChangeExpCurve"/> Method.
            /// </summary>
            protected int m_BaseExpCurve = DEFAULT_BASE_EXP_CURVE;

            /// <summary>
            /// This is the local Base Exp Curve Multiplier, used to be able to easily overwrite the Experience Curve when called using the  <see cref="ChangeExpCurve"/> Method.
            /// </summary>
            protected float m_BaseExpCurveMultiplier = DEFAULT_BASE_EXP_CURVE_MULTIPLIER;

            /// <summary>
            /// This is a list of LevelContainer types. This is an experience curve each m_Level has an amount of EXP needed for m_Level up.
            /// This is generated by default with the DEFAULT variables and can be overwritten using the <see cref="ChangeExpCurve"/> Method.
            /// </summary>
            protected List<LevelContainer> m_Experience_Curve = DefaultExpCurveList();

            #endregion

            #region Public Properties

            /// <summary>
            /// The current level property.
            /// </summary>
            public int Level
            {
                get { return m_Level; }
                set
                {
                    if (ValidateExp(value))
                    {
                        m_Level = value;
                    }
                    else
                    {
                        Console.WriteLine("INVALID LEVEL: " + value + " (Max Level: " + MaxLevel + ")");
                    }
                }
            }

            /// <summary>
            /// The current experience property.
            /// </summary>
            public int Exp
            {
                get { return m_Exp; }
                set
                {
                    if (ValidateLevel(value))
                    {
                        m_Exp = value;
                    }
                    else
                    {
                        Console.WriteLine("INVALID EXP: " + value);
                    }
                }
            }

            /// <summary>
            /// This is the local Max m_Level, used to determine what is considered the MAX m_Level for this object.
            /// </summary>
            public int MaxLevel
            {
                get { return m_MaxLevel; }
                set { m_MaxLevel = value; }
            }

            /// <summary>
            /// This is the local Base Exp Curve, used to be able to easily overwrite the Experience Curve when called using the  <see cref="ChangeExpCurve"/> Method.
            /// </summary>
            public int BaseExpCurve
            {
                get { return m_BaseExpCurve; }
                set { m_BaseExpCurve = value; }
            }

            /// <summary>
            /// This is the local Base Exp Curve Multiplier, used to be able to easily overwrite the Experience Curve when called using the  <see cref="ChangeExpCurve"/> Method.
            /// </summary>
            public float BaseExpCurveMultiplier
            {
                get { return m_BaseExpCurveMultiplier; }
                set { m_BaseExpCurveMultiplier = value; }
            }

            /// <summary>
            /// This is a list of LevelContainer types. This is an experience curve each m_Level has an amount of EXP needed for m_Level up.
            /// This is generated by default with the DEFAULT variables and can be overwritten using the <see cref="ChangeExpCurve"/> Method.
            /// </summary>
            public List<LevelContainer> ExperienceCurve
            {
                get { return m_Experience_Curve; }
                set { m_Experience_Curve = value; }
            }

            #endregion

            #region Public Methods

            /// <summary>
            /// SetLevel returns true if the level is set correctly.
            /// </summary>
            /// <param name="_level"></param>
            /// <returns></returns>
            public bool SetLevel(int _level)
            {
                if (_level > MaxLevel)
                {
                    m_Level = MaxLevel;
                    Exp = ExperienceCurve[MaxLevel - 1].Exp;
                    Console.WriteLine("Setting to Max Level:" + MaxLevel);
                    return true;
                }
                if (_level >= 1 && _level <= MaxLevel)
                {
                    m_Level = _level;
                    m_Exp = ExperienceCurve[m_Level - 1].Exp;
                    return true;
                }
                Console.WriteLine("Invalid Level Set");
                return false;
            }
            /// <summary>
            /// GainLevel returns true if level is gained.
            /// </summary>
            /// <param name="_level"></param>
            /// <returns></returns>
            public bool GainLevel(int _level = 1)
            {

                if (m_Level + _level >= MaxLevel)
                {
                    m_Level = MaxLevel;
                    m_Exp = ExperienceCurve[m_Level - 1].Exp;
                    return true;
                }
                m_Level += _level;
                m_Exp = ExperienceCurve[m_Level - 1].Exp;
                return true;
            }
            /// <summary>
            /// GainRandomLevel takes a min and max range and returns true if level is gained.
            /// </summary>
            /// <param name="_levelmin"></param>
            /// <param name="_levelmax"></param>
            /// <returns></returns>
            public bool GainRandomLevel(int _levelmin, int _levelmax)
            {
                var rand = new Random();
                var randlevel = rand.Next(_levelmin, _levelmax);
                if (m_Level + randlevel >= MaxLevel)
                {
                    m_Level = MaxLevel;
                    m_Exp = ExperienceCurve[m_Level - 1].Exp;
                    return true;
                }
                m_Level += randlevel;
                m_Exp = ExperienceCurve[m_Level - 1].Exp;
                return true;
            }
            /// <summary>
            /// LoseLevel returns true if a level is lost.
            /// </summary>
            /// <param name="_level"></param>
            /// <returns></returns>
            public bool LoseLevel(int _level = 1)
            {
                if (m_Level - _level <= 1)
                {
                    m_Level = 1;
                    m_Exp = 0;
                    return true;
                }

                m_Level -= _level;
                m_Exp = ExperienceCurve[m_Level - 1].Exp;
                return true;
            }
            /// <summary>
            /// LoseRandomLevel takes a min and max range and returns true if level is lost.
            /// </summary>
            /// <param name="_levelmin"></param>
            /// <param name="_levelmax"></param>
            /// <returns></returns>
            public bool LoseRandomLevel(int _levelmin, int _levelmax)
            {
            var rand = new Random();
            var randlevel = rand.Next(_levelmin, _levelmax);
                if (m_Level - randlevel <= 1)
                {
                    m_Level = 1;
                    m_Exp = 0;
                    return true;
                }

                m_Level -= randlevel;
                m_Exp = ExperienceCurve[m_Level - 1].Exp;
                return true;
            }
            /// <summary>
            /// Set exp returns true if Exp is set correctly.
            /// </summary>
            /// <param name="_exp"></param>
            /// <returns></returns>
            public bool SetExp(int _exp)
            {
                if (_exp >= 0)
                {
                    m_Exp = _exp;
                    ValidateLevel(m_Exp);
                    return true;
                }
                Console.WriteLine("Invalid Exp Set");
                return false;
            }
            /// <summary>
            /// Gain exp returns true if Exp is gained.
            /// </summary>
            /// <param name="_exp"></param>
            /// <returns></returns>
            public bool GainExp(int _exp)
            {
                if (_exp >= 0)
                {
                    m_Exp += _exp;
                    ValidateLevel(m_Exp);
                    return true;
                }
                Console.WriteLine("GainExp invalid amount. cannot be a negative number.");
                return false;
            }

            /// <summary>
            /// Gain random between a min and max value. exp returns true if exp is gained.
            /// </summary>
            /// <param name="_expmin"></param>
            /// <param name="_expmax"></param>
            /// <returns></returns>
            public bool GainRandomExp(int _expmin, int _expmax)
            {
                var rand = new Random();
                var rand_exp = rand.Next(_expmin, _expmax);
                if (rand_exp >= 0)
                {
                    m_Exp += rand_exp;
                    ValidateLevel(m_Exp);
                    return true;
                }
                Console.WriteLine("GainRandomExp invalid amount. cannot be a negative number.");
                return false;
            }

            /// <summary>
            /// Lose random exp between a min and max value. returns true if exp is lost.
            /// </summary>
            /// <param name="_expmin"></param>
            /// <param name="_expmax"></param>
            /// <returns></returns>
            public bool LoseRandomExp(int _expmin, int _expmax)
            {
                var rand = new Random();
                var rand_exp = rand.Next(_expmin, _expmax);

                if (m_Exp - rand_exp >= 0)
                {
                    m_Exp -= rand_exp;
                    ValidateLevel(m_Exp);
                    return true;
                }
                if (m_Exp - rand_exp <= 0 && rand_exp > 0)
                {
                    m_Exp = 0;
                    m_Level = 1;
                    return true;
                }
                Console.WriteLine("LoseRandomExp invalid amount. cannot be a negative number.");
                return false;
            }

            /// <summary>
            /// Lose exp returns true if exp is lost.
            /// </summary>
            /// <param name="_exp"></param>
            /// <returns></returns>
            public bool LoseExp(int _exp)
            {
                if (m_Exp - _exp >= 0)
                {
                    m_Exp -= _exp;
                    ValidateLevel(m_Exp);
                    return true;
                }
                if (m_Exp - _exp <= 0 && _exp > 0)
                {
                    m_Exp = 0;
                    m_Level = 1;
                    return true;
                }
                Console.WriteLine("LoseExp invalid amount. cannot be a negative number.");
                return false;
            }
            /// <summary>
            /// This is used to validate if a m_Level is gained or lost based on the amount of experience passed in. Returns true if a m_Level is gained or lost.
            /// </summary>
            /// <param name="_exp"></param>
            /// <returns></returns>
            public bool ValidateLevel(int _exp)
            {
                if (_exp < 0)
                {
                    return false;
                }

                if (_exp >= ExperienceCurve[MaxLevel - 1].Exp)
                {
                    m_Level = MaxLevel;
                    return true;
                }

                //This formula calculates the Level based on the experience using a quadratic equation to solve for the level.
                // Example, if the Curve = 50, and the multiplier = 1. and the amount of EXP that is being checked is 100.
                // NOTE: level 1 requires 100 exp to level with this curve and multiplier.
                // the formula is as follows:  50 + sqrt( (50*50) + (4 * 50 * 100) ) / (2 * 50)
                // (50*50) + (4 * 50 * 100) = 2500 + 20000 = 22500
                // the Square Root of 22500 is 150
                // Breaks down to (50 + 150) / 100
                // 200 / 100 = 2 (this is the level for the experience, which means the level can equal this formula.)
                float curve_multi = BaseExpCurve * BaseExpCurveMultiplier;
                var calc = (curve_multi + MathF.Sqrt((curve_multi * curve_multi) + (4 * curve_multi * _exp))) / (2 * curve_multi);
                m_Level = (int)calc;
                return true;
            }

            /// <summary>
            /// This is used to validate if a m_Level is gained or lost based on the amount of experience. Returns true if a m_Level is gained or lost.
            /// </summary>
            /// <returns></returns>
            public bool ValidateLevel()
            {
                if (m_Exp < 0)
                {
                    return false;
                }

                if (m_Exp >= ExperienceCurve[MaxLevel - 1].Exp)
                {
                    m_Level = MaxLevel;
                    return true;
                }

                //This formula calculates the Level based on the experience using a quadratic equation to solve for the level.
                // Example, if the Curve = 50, and the multiplier = 1. and the amount of EXP that is being checked is 100.
                // NOTE: level 1 requires 100 exp to level with this curve and multiplier.
                // the formula is as follows:  50 + sqrt( (50*50) + (4 * 50 * 100) ) / (2 * 50)
                // (50*50) + (4 * 50 * 100) = 2500 + 20000 = 22500
                // the Square Root of 22500 is 150
                // Breaks down to (50 + 150) / 100
                // 200 / 100 = 2 (this is the level for the experience, which means the level can equal this formula.)
                float curve_multi = BaseExpCurve * BaseExpCurveMultiplier;
                var calc = (curve_multi + MathF.Sqrt((curve_multi * curve_multi) + (4 * curve_multi * m_Exp))) / (2 * curve_multi);
                m_Level = (int)calc;

                return true;
            }


            /// <summary>
            /// This is used to validate if the experience based on the m_Level provided. Returns true if a experience is gained or lost.
            /// </summary>
            /// <param name="_level"></param>
            /// <returns></returns>
            public bool ValidateExp(int _level)
            {
                if (_level < 1 || _level > MaxLevel)
                {
                    return false;
                }
                if (_level == MaxLevel)
                {
                    m_Exp = ExperienceCurve[MaxLevel - 1].Exp;
                    return true;
                }
                m_Exp = ExperienceCurve[_level - 1].Exp;
                return true;
            }

            /// <summary>
            /// This is used to validate if the experience based on the m_Level. Returns true if a experience is gained or lost.
            /// </summary>
            /// <returns></returns>
            public bool ValidateExp()
            {
                if (m_Level < 1 || m_Level > MaxLevel)
                {
                    return false;
                }
                if (m_Level == MaxLevel)
                {
                    m_Exp = ExperienceCurve[MaxLevel - 1].Exp;
                    return true;
                }
                m_Exp = ExperienceCurve[m_Level - 1].Exp;
                return true;
            }



            /// <summary>
            /// <para>This is used to change the Exp Curve based Max Level, Exp Curve and Exp Curve Multiplier</para>
            /// <para>the formula used is List_Index * _exp_curve * _exp_curve_multiplier * level (Level is List_Index + 1)</para>
            /// <example>
            /// <para>EXAMPLE: ChangeExpCurve(5, 10, 1) Produces-> Level: 1 Exp: 0, Level: 2 Exp: 20, Level: 3 Exp: 60, Level: 4 Exp: 120, Level: 5 Exp: 200</para>
            /// <para>
            /// EXAMPLE: ChangeExpCurve(100, 50, 1) Produces-> Level: 1 Exp: 0, Level: 2 Exp: 100, Level: 3 Exp: 300, Level: 4 Exp: 600, Level: 5 Exp: 1000 ....
            /// Level: 96 Exp: 456000 Level: 97 Exp: 465600, Level: 98 Exp: 475300, Level: 99 Exp: 485100, Level: 100 Exp: 495000
            /// </para>
            /// </example>
            /// </summary>
            /// <param name="_max_level"></param>
            /// <param name="_exp_curve"></param>
            /// <param name="_exp_curve_multiplier"></param>
            public void ChangeExpCurve(int _max_level, int _exp_curve, float _exp_curve_multiplier = 1)
            {
                MaxLevel = _max_level;
                BaseExpCurve = _exp_curve;
                BaseExpCurveMultiplier = _exp_curve_multiplier;
                ExperienceCurve.Clear();
                for (int i = 0; i < _max_level; i++)
                {
                    var level = i + 1;
                    var exp_formula = i * _exp_curve * _exp_curve_multiplier * level;
                    ExperienceCurve.Add(new LevelContainer(level, exp_formula));
                }
            }

            #endregion

            #region Public Static Methods

            /// <summary>
            /// This Returns a Experience Curve List based on the Default Curve Variables.
            /// <para>the formula used is List_Index * _exp_curve * _exp_curve_multiplier * level (Level is List_Index + 1)</para>
            /// <para>EXAMPLE: DEFAULT_MAX_LEVEL = 100, DEFAULT_BASE_EXP_CURVE = 50, DEFAULT_BASE_EXP_CURVE_MULTIPLIER = 1</para> 
            /// <para>
            /// Produces-> Level: 1 Exp: 0, Level: 2 Exp: 100, Level: 3 Exp: 300, Level: 4 Exp: 600, Level: 5 Exp: 1000 ....
            /// Level: 96 Exp: 456000 Level: 97 Exp: 465600, Level: 98 Exp: 475300, Level: 99 Exp: 485100, Level: 100 Exp: 495000
            /// </para> 
            /// </summary>
            /// <returns></returns>
            public static List<LevelContainer> DefaultExpCurveList()
            {
                List<LevelContainer> levelslist = new List<LevelContainer>();
                for (int i = 0; i < DEFAULT_MAX_LEVEL; i++)
                {
                    var level = i + 1;
                    var exp_formula = i * DEFAULT_BASE_EXP_CURVE * DEFAULT_BASE_EXP_CURVE_MULTIPLIER * level;
                    levelslist.Add(new LevelContainer(level, exp_formula));
                }
                return levelslist;
            }

            /// <summary>
            /// This Returns a Experience Curve List based on the  <see cref="_max_level"/>, <see cref="_exp_curve"/>, <see cref="_exp_curve_modifier"/> Parameters.
            /// <para>the formula used is List_Index * _exp_curve * _exp_curve_multiplier * level (Level is List_Index + 1)</para>
            /// <para>EXAMPLE: _MaxLevel = 100, _ExpCurve = 50, _ExpCurveModifier = 1</para> 
            /// <para>
            /// Produces-> Level: 1 Exp: 0, Level: 2 Exp: 100, Level: 3 Exp: 300, Level: 4 Exp: 600, Level: 5 Exp: 1000 ....
            /// Level: 96 Exp: 456000 Level: 97 Exp: 465600, Level: 98 Exp: 475300, Level: 99 Exp: 485100, Level: 100 Exp: 495000
            /// </para> 
            /// </summary>
            /// <param name="_max_level"></param>
            /// <param name="_exp_curve"></param>
            /// <param name="_exp_curve_modifier"></param>
            /// <returns></returns>
            public static List<LevelContainer> CustomExpCurveList(int _max_level, int _exp_curve, float _exp_curve_modifier = 1)
            {
                List<LevelContainer> levelslist = new List<LevelContainer>();
                for (int i = 0; i < _max_level; i++)
                {
                    var level = i + 1;
                    var exp_formula = i * _exp_curve * _exp_curve_modifier * level;
                    levelslist.Add(new LevelContainer(level, exp_formula));
                }
                return levelslist;
            }

            #endregion

        }
    }