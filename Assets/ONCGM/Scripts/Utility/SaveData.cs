using System;

namespace ONCGM.Utility {
    /// <summary>
    /// This class is meant to hold information relevant to the game. A.K.A a save file.
    /// Fill in all the variables you need, I've only placed a few general variables,
    /// because I don't know what you will need in your game. 
    /// </summary>
    [Serializable]
    public class SaveData {
        /// <summary>
        /// The player name. Can be useful for various things.
        /// </summary>
        public string playerName = "ONCGM";

        /// <summary>
        /// How many times has the player died.
        /// </summary>
        public int playerDeathCount = 0;

        /// <summary>
        /// Where was the player last standing when the game was saved.
        /// </summary>
        public float[] lastPlayerPosition = new float[3] {0f, 0f, 0f};

        /// <summary>
        /// List of all checkpoints. True if the player has cleared a checkpoint.
        /// </summary>
        public bool[] checkpoints = new[] {false, false, false};

        /// <summary>
        /// List of all important dialogs. True if the player has seen them.
        /// </summary>
        public bool[] dialogsCleared = new[] {false, false, false};

        /// <summary>
        /// The game difficulty set by the player.
        /// </summary>
        public int difficulty = 2;

        /// <summary>
        /// The graphics quality level. See Unity Quality Settings for more information on how to use this. 
        /// </summary>
        public int graphicsLevel = 1;

        /// <summary>
        /// The LOD (level of detail) setting. See Unity LOD for more information on how to use this.
        /// </summary>
        public int lodLevel = 1;

        /// <summary>
        /// The audio mixer master channel volume. 
        /// </summary>
        public float audioMasterVolume = 0f;

        /// <summary>
        /// The audio mixer sfx channel volume.
        /// </summary>
        public float audioSfxVolume = 0f;

        /// <summary>
        /// The audio mixer music channel volume.
        /// </summary>
        public float audioMusicVolume = 0f;

        /// <summary>
        /// The audio mixer UI and interface channel volume.
        /// </summary>
        public float audioUiVolume = 0f;

        /// <summary>
        /// The screen resolution set by the user. I'm using a 'Vector2Int' because its easier to serialize.
        /// </summary>
        public int[] screenResolution = new int[2] {1280, 720};

        /// <summary>
        /// The total amount of time the player spent playing the game.
        /// </summary>
        public float totalTimeInGame = 0f;
    }
}