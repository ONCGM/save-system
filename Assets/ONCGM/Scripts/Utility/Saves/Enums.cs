using System;

namespace ONCGM.Utility.Saves {
    /// <summary>
    /// Defines the language available for use in the save system editor window.
    /// </summary>
    public enum UserInterfaceLanguage {
        English,
        Portuguese
    }
    
    /// <summary>
    /// Defines the paths that the system can serialize files to.
    /// </summary>
    public enum SavePath {
        PersistentPath,
        ApplicationPath,
        Documents
    }

    /// <summary>
    /// Defines the format in which to save files as.
    /// </summary>
    public enum SaveFormat {
        Binary,
        Json,
        Xml
    }
    
    /// <summary>
    /// Defines set time intervals for the auto save feature.
    /// </summary>
    public enum AutoSaveInterval {
        OneMin,
        TwoMins,
        ThreeMins,
        FourMins,
        FiveMins,
        SevenMins,
        TenMins,
        FifteenMins,
        TwentyMins,
        ThirtyMins,
        FortyFiveMins,
        SixtyMins
    }
    
    /// <summary>
    /// Converts the 'AutoSaveInterval' enum values to milliseconds for timer usage.
    /// </summary>
    public static class AutoSaveIntervalExtension {
        /// <summary>
        /// Converts the 'AutoSaveInterval' enum values to milliseconds for timer usage.
        /// </summary>
        /// <returns> Will return a default value of ten minutes if a invalid values is passed as a parameter. </returns>
        public static int ToMilliseconds(this AutoSaveInterval self) {
            switch(self) {
                case AutoSaveInterval.OneMin:
                    return 60000;
                case AutoSaveInterval.TwoMins:
                    return 120000;
                case AutoSaveInterval.ThreeMins:
                    return 180000;
                case AutoSaveInterval.FourMins:
                    return 240000;
                case AutoSaveInterval.FiveMins:
                    return 300000;
                case AutoSaveInterval.SevenMins:
                    return 420000;
                case AutoSaveInterval.TenMins:
                    return 600000;
                case AutoSaveInterval.FifteenMins:
                    return 900000;
                case AutoSaveInterval.TwentyMins:
                    return 1200000;
                case AutoSaveInterval.ThirtyMins:
                    return 1800000;
                case AutoSaveInterval.FortyFiveMins:
                    return 2700000;
                case AutoSaveInterval.SixtyMins:
                    return 3600000;
                default:
                    return 600000; 
            }
        }
    }
}