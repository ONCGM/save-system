namespace ONCGM.Utility.Editor {
    /// <summary>
    /// Language reference class for the save system editor windows.
    /// </summary>
    public static class Languages {
        public static readonly string[] Yes = new[] {"Yes", "Sim"};
        public static readonly string[] No = new[] {"No", "Não"};
        public static readonly string[] Actions = new[] {"Actions", "Ações"};
        public static readonly string[] Exists = new[] {"Exists", "Existe"};
        public static readonly string[] DoesNotExists = new[] {"Doesn't exist", "Não existe"};
        public static readonly string[][] Minutes = new string[][] {
            new [] {"One", "Two", "Three", "Four", "Five", "Seven", "Ten", "Fifteen", "Twenty", "Thirty", "Forty-Five", "Sixty"},
            new [] {"Um", "Dois", "Três", "Quatro", "Cinco", "Sete", "Dez", "Quinze", "Vinte", "Trinta", "Quarenta e cinco", "Sessenta"}};
        public static readonly string[] Binary = new[] {"Binary", "Binário"};
        public static readonly string[] SaveLocation = new[] {"Save location", "Local do arquivo"};
        public static readonly string[] ApplicationPath = new[] {"(Unity) Application path", "(Unity) Local do aplicativo"};
        public static readonly string[] PersistentPath = new[] {"(Unity) Persistent path", "(Unity) Local persistente"};
        public static readonly string[] DocumentsPath = new[] {"Documents", "Documentos"};
        public static readonly string[] SaveFormat = new[] {"Save format", "Formato do arquivo"};
        public static readonly string[] DirectoryName = new[] {"Directory name:", "Nome da Pasta:"};
        public static readonly string[] SaveFileName = new[] {"Save file name:", "Nome do arquivo:"};
        public static readonly string[] SettingsFileName = new[] {"Auto save prefix:", "Prefixo do auto save"};
        public static readonly string[] AutoSaveToggle = new[] {"Auto save?", "Auto save?"};
        public static readonly string[] HideAutoSave = new[] {"Set auto save as hidden file?", "Esconder arquivos de auto save?"};
        public static readonly string[] FileExtension = new[] {"File extension (Binary):", "Extensão do arquivo (Binário):"};
        public static readonly string[] AutoSaveTime = new[] {"Auto save interval in minutes:", "Intervalo entre saves em minutos:"};
        public static readonly string[] ActiveFile = new[] {"Active file: ", "Arquivo carregado: "};
        public static readonly string[] SaveFile = new[] {"Save", "Salvar"};
        public static readonly string[] SaveName = new[] {"Save file name", "Nome do arquivo"};
        public static readonly string[] LoadFile = new[] {"Load", "Carregar"};
        public static readonly string[] DeleteFile = new[] {"Delete", "Deletar"};
        public static readonly string[] UtilityAndTools = new[] {"Utility & Tools", "Utilidade & Ferramentas"};
        public static readonly string[] Dangerous = new[] {"Dangerous things", "Area perigosa"};
        public static readonly string[] DeleteAllFiles = new[] {"Delete all files", "Deletar todos os arquivos"};
        public static readonly string[] DeletedFiles = new[] {"All files were deleted.", "Todos os arquivos foram deletados."};
        public static readonly string[] CreateFile = new[] {"Create a new save", "Criar um novo arquivo"};
        public static readonly string[] CheckForFiles = new[] {"Check for save files", "Pesquisar por arquivos"};
        public static readonly string[] OpenSaveLocation = new[] {"Open save folder", "Abrir pasta dos arquivos de save"};
        public static readonly string[] CreateSaveFolder = new[] {"Create save folder", "Criar pasta dos arquivos de save"};
        public static readonly string[] FileWindow = new[] {"File: ", "Arquivo: "};
        public static readonly string[] CouldNotFindFileOrDirectory = new[] {"Couldn't find file or directory", "Não foi possível encontrar o arquivo ou a pasta."};
        public static readonly string[] FileNullOrNotLoaded = new[] {"The file hasn't been loaded or is a null", "O arquivo não foi carregado ou é nulo"};
        public static readonly string[] FileNotSaved = new[] {"The file hasn't been serialized.", "O arquivo não foi serializado."};
        public static readonly string[] Feedback = new[]
            {"If you find a translation error or a bug, please let me know.", "Se você encontrar um erro de tradução ou um bug, por favor, entre em contato."};
        public static readonly string[] LanguageNames = new[] {"Language", "Idioma"};
        public static readonly string[] LanguageName = new[] {"English", "Português (Brasil)"};
    }
}
