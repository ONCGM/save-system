using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONCGM.Utility.Saves {
    /// <summary>
    /// This class holds the editor and runtime settings for the save system. Once the application is built, changes are permanent.
    /// </summary>
    [CreateAssetMenu(fileName = "SaveSystemSettings", menuName = "ONCGM/Save System Settings")]
    [Serializable]
    public class SaveSystemSettings : ScriptableObject {
        /// <summary>
        /// Which path to save the files.
        /// </summary>
        public SavePath saveLocation = SavePath.PersistentPath;
        
        /// <summary>
        /// Which format to save in.
        /// </summary>
        public SaveFormat saveFormat = SaveFormat.Binary;
        
        /// <summary>
        /// What to name the saves folder.
        /// </summary>
        public string directoryName = "Save Data";
        
        /// <summary>
        /// Default name used when serializing a save file. The date at the time of serialization will be added after this name.
        /// </summary>
        public string saveFileDefaultName = "save";
        
        /// <summary>
        /// Default name used when serializing a auto save file. The date at the time of serialization will be added after this name. 
        /// </summary>
        public string autoSavePrefix = "auto save - ";
        
        /// <summary>
        /// Default name used when serializing a exit save file. The date at the time of serialization will be added after this name. 
        /// </summary>
        public string exitSaveName = "exit save";
        
        /// <summary>
        /// The file extension to use when creating a binary file.
        /// </summary>
        public string fileExtension = "oncgm";
        
        /// <summary>
        /// Should the system auto save automatically after a certain time interval?
        /// This is one of the few exception of things that can be changed in runtime, just in case you need it.
        /// It can be changed using the 'SetAutoSave' method by the save system API.
        /// You can also manually trigger an auto save using the 'AutoSave' method.
        /// </summary>
        public bool autoSave = true;
        
        /// <summary>
        /// Should the system set the auto save files as hidden in the system?
        /// This is one of the few exception of things that can be changed in runtime, just in case you need it.
        /// It can be changed using the 'SetAutoSave' method by the save system API.
        /// You can also manually trigger an auto save using the 'AutoSave' method.
        /// </summary>
        public bool hideAutoSave = false;
        
        /// <summary>
        /// How frequent to automatically save the player save file?
        /// This is one of the few exception of things that can be changed in runtime, just in case you need it.
        /// It can be changed using the 'SetAutoSave' method by the save system API.
        /// You can also manually trigger an auto save using the 'AutoSave' method.
        /// </summary>
        public AutoSaveInterval autoSaveInterval = AutoSaveInterval.TenMins;
        
        /// <summary>
        /// This dictates which language the save system editor window will use.
        /// </summary>
        public UserInterfaceLanguage language = UserInterfaceLanguage.English;
    }
}