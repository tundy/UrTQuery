using System.Collections.Generic;
using System.Globalization;

namespace UrTQuery
{
    public static class GameModes
    {
// ReSharper disable InconsistentNaming
        public static readonly int FFA = 0, LMS = 1, TDM = 3, TS = 4, FTL = 5, CnH = 6, CTF = 7, Bomb = 8, Jump = 9, Freeze = 10, Gun = 11;
// ReSharper restore InconsistentNaming
        public static readonly Dictionary<object, string> LongNames = new Dictionary<object, string>
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
            {Freeze, "Freeze Tag"},
            {Gun, "Gun Game"},
            {FFA.ToString(CultureInfo.InvariantCulture), "Free For All"},
            {LMS.ToString(CultureInfo.InvariantCulture), "Last Man Standing"},
            {TDM.ToString(CultureInfo.InvariantCulture), "Team Death Match"},
            {TS.ToString(CultureInfo.InvariantCulture), "Team Survivor"},
            {FTL.ToString(CultureInfo.InvariantCulture), "Follow The Leader"},
            {CnH.ToString(CultureInfo.InvariantCulture), "Capture and Hold"},
            {CTF.ToString(CultureInfo.InvariantCulture), "Capture The Flag"},
            {Bomb.ToString(CultureInfo.InvariantCulture), "Bomb Mode"},
            {Jump.ToString(CultureInfo.InvariantCulture), "Jump Mode"},
            {Freeze.ToString(CultureInfo.InvariantCulture), "Freeze Tag"},
            {Gun.ToString(CultureInfo.InvariantCulture), "Gun Game"}
        };
        public static readonly Dictionary<object, string> ShortCuts = new Dictionary<object, string>
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
            {Freeze, "Freeze"},
            {Gun, "Gun"},
            {FFA.ToString(CultureInfo.InvariantCulture), "FFA"},
            {LMS.ToString(CultureInfo.InvariantCulture), "LMS"},
            {TDM.ToString(CultureInfo.InvariantCulture), "TDM"},
            {TS.ToString(CultureInfo.InvariantCulture), "TS"},
            {FTL.ToString(CultureInfo.InvariantCulture), "FTL"},
            {CnH.ToString(CultureInfo.InvariantCulture), "CnH"},
            {CTF.ToString(CultureInfo.InvariantCulture), "CTF"},
            {Bomb.ToString(CultureInfo.InvariantCulture), "Bomb"},
            {Jump.ToString(CultureInfo.InvariantCulture), "Jump"},
            {Freeze.ToString(CultureInfo.InvariantCulture), "Freeze"},
            {Gun.ToString(CultureInfo.InvariantCulture), "Gun"}
        };
    }
}