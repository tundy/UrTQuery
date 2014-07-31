using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrTQueryWpf
{
    public static class GameModes
    {
        public static readonly int FFA = 0, LMS = 1, TDM = 3, TS = 4, FTL = 5, CnH = 6, CTF = 7, Bomb = 8, Jump = 9;
        public static readonly Dictionary<object, string> LongNames = new Dictionary<object, string>()
        {
        {FFA, "Free For All"},
        {LMS, "Last Man Standing"},
        {TDM, "Team Death Match"},
        {TS, "Team Survivor"},
        {FTL, "Follow The Leader"},
        {CnH, "Capture and Hold"},
        {CTF, "Capture The Flag"},
        {Bomb, "Bomb Mode"},
        {Jump, "Jump Mode"},
        {FFA.ToString(), "Free For All"},
        {LMS.ToString(), "Last Man Standing"},
        {TDM.ToString(), "Team Death Match"},
        {TS.ToString(), "Team Survivor"},
        {FTL.ToString(), "Follow The Leader"},
        {CnH.ToString(), "Capture and Hold"},
        {CTF.ToString(), "Capture The Flag"},
        {Bomb.ToString(), "Bomb Mode"},
        {Jump.ToString(), "Jump Mode"},
        };
        public static readonly Dictionary<object, string> ShortCuts = new Dictionary<object, string>()
        {
        {FFA, "FFA"},
        {LMS, "LMS"},
        {TDM, "TDM"},
        {TS, "TS"},
        {FTL, "FTL"},
        {CnH, "CnH"},
        {CTF, "CTF"},
        {Bomb, "Bomb"},
        {Jump, "Jump"},
        {FFA.ToString(), "FFA"},
        {LMS.ToString(), "LMS"},
        {TDM.ToString(), "TDM"},
        {TS.ToString(), "TS"},
        {FTL.ToString(), "FTL"},
        {CnH.ToString(), "CnH"},
        {CTF.ToString(), "CTF"},
        {Bomb.ToString(), "Bomb"},
        {Jump.ToString(), "Jump"},
        };
    }

}
