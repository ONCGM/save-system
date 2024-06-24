using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using ONCGM.Utility.Saves;
using static ONCGM.Utility.Editor.Languages;
using Debug = UnityEngine.Debug;

namespace ONCGM.Utility.Editor {
    public class SaveSystemWindow : EditorWindow {
        private static string resourcesFilePath = "Assets/ONCGM/Resources/";
        private static string saveSystemSettingsFilePath = "Settings/SaveSystemSettings";
        private static string saveSystemSettingsFileExtension = ".asset";
        private static bool dangerousToggle;
        public static SaveSystemSettings Settings { get; private set; }

        private static int saveListLength = 100;
        private static float saveListLengthBaseMultiplier = 0.2f;
        private static float saveListLengthExpandMultiplier = 0.35f;
        private static int saveListMinimumHeightToExpand = 500;
        private static int saveListMaximumHeightToExpand = 1100;
        private static int saveListBoxWidth = 100;
        private static int saveListYStartHeight = 250;
        private static Rect smallAreaInsideSaveListColumns;
        private static Rect largeAreaInsideSaveListColumns;
        
        
        [MenuItem("ONCGM/Save System")]
        public static void ShowWindow() {
            GetWindow<SaveSystemWindow>("Save System").Show();
        }
        
        public static void LoadOrCreateSaveSystemSettingsFile() {
            Settings = Resources.Load<SaveSystemSettings>(saveSystemSettingsFilePath);
            if(Settings != null) return;
            Settings = ScriptableObject.CreateInstance<SaveSystemSettings>();
            AssetDatabase.CreateAsset(Settings, string.Concat(resourcesFilePath, saveSystemSettingsFilePath, saveSystemSettingsFileExtension));
            AssetDatabase.SaveAssets();
        }
        
        private void OnGUI() {
            if(Settings == null) {
                LoadOrCreateSaveSystemSettingsFile();
            }
            
            saveListYStartHeight = 300;
            saveListLength = (int) ((Screen.height * saveListLengthBaseMultiplier) + 
                                    (Mathf.Lerp(0f, Screen.height * saveListLengthExpandMultiplier, 
                                                Mathf.InverseLerp(saveListMinimumHeightToExpand, saveListMaximumHeightToExpand, Screen.height))));
            saveListBoxWidth = Screen.width / 3;
            smallAreaInsideSaveListColumns = new Rect(5f, 25f, saveListBoxWidth - 20f, saveListLength - 20f);
            largeAreaInsideSaveListColumns = new Rect(5f, 25f, saveListBoxWidth * 2 - 20f, saveListLength - 20f);
            
            ModeSelectionToolbars();
            
            SaveList();
            
            UtilityAndTools();

            BottomBar();
        }

        private static void ModeSelectionToolbars() {
            // Save location toolbar. 
            // Centered label.
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(SaveLocation[(int) Settings.language], EditorStyles.largeLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            var lastLocation = Settings.saveLocation;
            // Toolbar.
            GUILayout.Space(1f);
            Settings.saveLocation = (SavePath) GUILayout.Toolbar((int) Settings.saveLocation, new[] {PersistentPath[(int) Settings.language], ApplicationPath[(int) Settings.language], DocumentsPath[(int) Settings.language]});
            if(lastLocation != Settings.saveLocation) SaveSystem.UpdateDirectory();
            
            // Save mode toolbar.
            // Centered label.
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(Languages.SaveFormat[(int) Settings.language], EditorStyles.largeLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            // Toolbar.
            GUILayout.Space(1f);
            Settings.saveFormat = (SaveFormat) GUILayout.Toolbar((int) Settings.saveFormat, new []{Binary[(int) Settings.language], "JSON", "XML"});
            
            // Save directory name.
            // Centered label & Text field.
            GUILayout.BeginHorizontal();
            GUILayout.Space(7f);
            GUILayout.Label(DirectoryName[(int) Settings.language], EditorStyles.largeLabel);
            Settings.directoryName = GUILayout.TextField(Settings.directoryName, 35);
            if(Settings.saveFormat == Saves.SaveFormat.Binary) {
                GUILayout.Label(Languages.FileExtension[(int) Settings.language], EditorStyles.largeLabel);
                Settings.fileExtension = GUILayout.TextField(Settings.fileExtension, 6);
            }

            GUILayout.Space(7f);
            GUILayout.EndHorizontal();
            
            // Save file name.
            // Centered label & Text field.
            GUILayout.BeginHorizontal();
            GUILayout.Space(7f);
            GUILayout.Label(SaveFileName[(int) Settings.language], EditorStyles.largeLabel);
            Settings.saveFileDefaultName = GUILayout.TextField(Settings.saveFileDefaultName, 35);
            GUILayout.Space(7f);
            GUILayout.EndHorizontal();
            
            // Settings save file name.
            // Centered label & Text field.
            GUILayout.BeginHorizontal();
            GUILayout.Space(7f);
            GUILayout.Label(SettingsFileName[(int) Settings.language], EditorStyles.largeLabel);
            Settings.autoSavePrefix = GUILayout.TextField(Settings.autoSavePrefix, 35);
            GUILayout.Space(7f);
            GUILayout.EndHorizontal();
            
            // Auto save file.
            // Centered label & Text field.
            GUILayout.BeginHorizontal();
            GUILayout.Space(7f);
            GUILayout.Label(AutoSaveToggle[(int) Settings.language], EditorStyles.largeLabel);
            Settings.autoSave = GUILayout.Toggle(Settings.autoSave, Settings.autoSave ? Yes[(int) Settings.language] : No[(int) Settings.language], EditorStyles.toggleGroup);
            
            GUILayout.Label(HideAutoSave[(int) Settings.language], EditorStyles.largeLabel);
            Settings.hideAutoSave = GUILayout.Toggle(Settings.hideAutoSave, Settings.hideAutoSave ? Yes[(int) Settings.language] : No[(int) Settings.language], EditorStyles.toggleGroup);

            GUILayout.EndHorizontal();
            
            // Auto save interval.
            // Centered label & Text field.
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(AutoSaveTime[(int) Settings.language], EditorStyles.largeLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            Settings.autoSaveInterval = (AutoSaveInterval) GUILayout.SelectionGrid(
                (int) Settings.autoSaveInterval, Minutes[(int) Settings.language], 6, EditorStyles.miniButtonMid);
        }
        
        private static void SaveList() {
            // Displays active file
            GUI.Box(new Rect(8f, saveListYStartHeight - 32f, Screen.width - 20f, 30f), "");
            GUILayout.BeginArea(new Rect(4f, saveListYStartHeight - 27.5f, Screen.width - 16f, 25f));
            GUILayout.BeginHorizontal();
            GUILayout.Space(saveListBoxWidth * 0.28f);
            GUILayout.Label(ActiveFile[(int) Settings.language], EditorStyles.label);

            if(SaveSystem.LoadedDataInfo != null) {
                GUILayout.Label(SaveSystem.LoadedDataInfo.Name, EditorStyles.label);

                if(GUILayout.Button(SaveFile[(int) Settings.language])) {
                    SaveSystem.SerializeToFile();
                }

                if(GUILayout.Button(DeleteFile[(int) Settings.language])) {
                    SaveSystem.DeleteLoadedFile();
                    SaveSystem.SearchForSaveFiles();
                }
            } else {
                GUILayout.Label(FileNullOrNotLoaded[(int) Settings.language], EditorStyles.label);
            } 
            
            GUILayout.FlexibleSpace();

            GUILayout.Space(14f);
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            
            // Create horizontal self centering area.
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            // First column.
            GUILayout.BeginArea(new Rect(8f, saveListYStartHeight, saveListBoxWidth * 2 - 14, saveListLength));
            GUILayout.Box(SaveName[(int) Settings.language], new GUILayoutOption[]{GUILayout.Width(saveListBoxWidth * 2 - 14f), GUILayout.Height(saveListLength)});
            GUILayout.BeginArea(largeAreaInsideSaveListColumns);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            foreach(var save in SaveSystem.GameSavesInfo) {
                GUILayout.Space(2);
                GUILayout.Label(
                    $"{save.Name} - {(save.Exists ? Exists[(int) Settings.language] : DoesNotExists[(int) Settings.language])}");
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            GUILayout.EndArea();
            
            // Second column.
            GUILayout.BeginArea(new Rect((saveListBoxWidth - 2f) * 2f, saveListYStartHeight, saveListBoxWidth - 7, saveListLength));
            GUILayout.Box(Actions[(int) Settings.language], new GUILayoutOption[]{GUILayout.Width(saveListBoxWidth - 7f), GUILayout.Height(saveListLength)});
            GUILayout.BeginArea(smallAreaInsideSaveListColumns);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            
            foreach(var save in (new List<FileInfo>(SaveSystem.GameSavesInfo.Concat(SaveSystem.AutoSavesInfo)))) {
                GUILayout.Space(2);
                GUILayout.BeginHorizontal();
                if(GUILayout.Button(LoadFile[(int) Settings.language])) {
                    SaveSystem.LoadGameFile(save.FullName);
                }
                
                if(GUILayout.Button(DeleteFile[(int) Settings.language])) {
                    if(ReferenceEquals(SaveSystem.LoadedDataInfo, save)) {
                        SaveSystem.DeleteLoadedFile();
                    } else {
                        SaveSystem.DeleteFile(save.FullName);
                    }
                    
                    SaveSystem.SearchForSaveFiles();
                }
                
                GUILayout.EndHorizontal();
            }
            
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            GUILayout.EndArea();
            
            // Close horizontal area.
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private static void UtilityAndTools() {
            // Draws the Label in the center of the screen.
            GUILayout.Space(saveListLength + 50f);
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(Languages.UtilityAndTools[(int) Settings.language], EditorStyles.largeLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            // Draws some utility buttons.
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            
            if(GUILayout.Button(CreateFile[(int) Settings.language])) {
                SaveSystem.SerializeToFile(new SaveData(), Settings.saveFormat, Settings.saveLocation);
                SaveSystem.SearchForSaveFiles();
            }

            if(GUILayout.Button(CheckForFiles[(int) Settings.language])) {
                SaveSystem.SearchForSaveFiles();
            }
            
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            
            if(GUILayout.Button(SaveSystem.CheckForDirectory() ? OpenSaveLocation[(int) Settings.language] : CreateSaveFolder[(int) Settings.language])) {
                if(!SaveSystem.CheckForDirectory()) SaveSystem.CreateDirectory();
                
                switch(Settings.saveLocation) {
                    case SavePath.PersistentPath:
                        if(Directory.Exists(Path.Combine(Application.persistentDataPath, Settings.directoryName)))
                            Process.Start(Path.Combine(Application.persistentDataPath, Settings.directoryName));
                        else Debug.Log(CouldNotFindFileOrDirectory[(int) Settings.language]);
                        break;
                    case SavePath.ApplicationPath:
                        if(Directory.Exists(Path.Combine(Application.dataPath, Settings.directoryName)))
                            Process.Start(Path.Combine(Application.dataPath, Settings.directoryName));
                        else Debug.Log(CouldNotFindFileOrDirectory[(int) Settings.language]);
                        break;
                    case SavePath.Documents:
                        if(Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Settings.directoryName)))
                            Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Settings.directoryName));
                        else Debug.Log(CouldNotFindFileOrDirectory[(int) Settings.language]);
                        break;
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            
            // Draws the toggle to hide some dangerous buttons (E.G. A button that deletes everything).
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            dangerousToggle = GUILayout.Toggle(dangerousToggle, Dangerous[(int) Settings.language]);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            // Hides or displays the buttons depending on the toggle value.
            if(!dangerousToggle) return;
            GUILayout.BeginHorizontal();
            if(GUILayout.Button(DeleteAllFiles[(int) Settings.language])) {
                foreach(var file in SaveSystem.GameSavesInfo) {
                    SaveSystem.DeleteFile(file.FullName);
                }
                
                foreach(var file in SaveSystem.AutoSavesInfo) {
                    SaveSystem.DeleteFile(file.FullName);
                }
                
                Debug.Log(DeletedFiles[(int) Settings.language]);
            }
            GUILayout.EndHorizontal();
        }

        private static void BottomBar() {
            // Language name label.
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(string.Concat(LanguageNames[0], " | ", LanguageNames[1]), EditorStyles.largeLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            // Toolbar for selecting the active language.
            Settings.language = (UserInterfaceLanguage) GUILayout.Toolbar((int) Settings.language, new []{LanguageName[0], LanguageName[1]});
            
            // Feedback and branding label.
            GUILayout.Label(Feedback[(int) Settings.language], EditorStyles.centeredGreyMiniLabel);
            GUILayout.Label("Save System by ONCGM", EditorStyles.centeredGreyMiniLabel);
        }
    }
}