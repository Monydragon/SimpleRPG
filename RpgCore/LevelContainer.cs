using System;
using System.Collections.Generic;
using System.Text;

namespace RpgCore
{
    public class LevelContainer
    {
        #region Protected Variables

        protected int m_Level;
        protected int m_Exp;

        #endregion

        #region Public Properties

        public int Exp
        {
            get { return m_Exp; }
            set { m_Exp = value; }
        }

        public int Level
        {
            get { return m_Level; }
            set { m_Level = value; }
        }

        #endregion

        #region Public Constructors

        public LevelContainer(int _level, int _exp)
        {
            Level = _level;
            Exp = _exp;
        }
        public LevelContainer(int _level, float _exp)
        {
            Level = _level;
            Exp = (int)Math.Ceiling(_exp);
        }

        #endregion
    }
}
