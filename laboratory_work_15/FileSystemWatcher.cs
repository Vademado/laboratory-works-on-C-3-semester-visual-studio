namespace laboratory_work_15
{
    interface IObserver
    {
        void Update(FileSystemWatcher directory, FileSystemWatcherEventArgs e);
    }

    class Observer : IObserver
    {
        public void Update(FileSystemWatcher directory, FileSystemWatcherEventArgs e)
        {
            switch (e.Condition)
            {
                case ECondition.Created:
                    Console.WriteLine($"File {e.FilePath} was created in directory {directory.DirectoryPath}");
                    break;
                case ECondition.Deleted:
                    Console.WriteLine($"File {e.FilePath} was deleted in directory {directory.DirectoryPath}");
                    break;
                case ECondition.Changed:
                    Console.WriteLine($"File {e.FilePath} was changed in directory {directory.DirectoryPath}");
                    break;
                case ECondition.None:
                    Console.WriteLine($"No changes in directory {directory.DirectoryPath}");
                    break;
            }
        }
    }
    internal class FileSystemWatcher
    {
        public string DirectoryPath { get; private set; }
        public List<string> PrimaryFiles { get; private set; }
        public List<string> CurrentFiles { get; private set; }
        public List<string> ChangedFiles { get; private set; }
        private delegate void FileSystemWatcherHandler(FileSystemWatcher fileSystemWatcher, FileSystemWatcherEventArgs e);
        event FileSystemWatcherHandler Notify;
        private Timer timer;

        public FileSystemWatcher(string directoryPath)
        {
            DirectoryPath = directoryPath;
            PrimaryFiles = new List<string>();
            CurrentFiles = new List<string>();
            ChangedFiles = new List<string>();
            TimerCallback tm = new TimerCallback(CheckFiles);
            timer = new Timer(tm, null, 0, 2000);
        }

        public void Stop()
        {
            timer.Dispose();
        }

        public void AddObserver(IObserver observer)
        {
            Notify += observer.Update;
        }

        public void AddFile(string filePath)
        {
            CurrentFiles.Add(filePath);
        }

        public void DeleteFile(string filePath)
        {
            if (PrimaryFiles.Contains(filePath))
            {
                CurrentFiles.Remove(filePath);
            }
        }

        public void ChangeFile(string filePath)
        {
            if (!PrimaryFiles.Contains(filePath))
            {
                CurrentFiles.Add(filePath);
            }
            else if (PrimaryFiles.Contains(filePath))
                ChangedFiles.Add(filePath);
        }

        private void CheckFiles(object obj)
        {
            foreach(var file in CurrentFiles.Except(PrimaryFiles))
            {
                Notify.Invoke(this, new FileSystemWatcherEventArgs(file, ECondition.Created));
            }
            foreach (var file in PrimaryFiles.Except(CurrentFiles))
            {
                Notify.Invoke(this, new FileSystemWatcherEventArgs(file, ECondition.Deleted));
            }
            foreach (var file in ChangedFiles)
            {
                Notify.Invoke(this, new FileSystemWatcherEventArgs(file, ECondition.Changed));
            }
            if (CurrentFiles.Equals(PrimaryFiles) && ChangedFiles.Count == 0)
                Notify.Invoke(this, new FileSystemWatcherEventArgs("", ECondition.None));
            PrimaryFiles = CurrentFiles;
            ChangedFiles.Clear();
        }
    }
    class FileSystemWatcherEventArgs
    {
        public string FilePath { get; }
        public ECondition Condition { get; }
        public FileSystemWatcherEventArgs(string filePath, ECondition condition)
        {
            FilePath = filePath;
            Condition = condition;
        }
    }

    enum ECondition
    {
        Created,
        Deleted,
        Changed,
        None
    }

    #region without a timer
    //interface IObserver
    //{
    //    void Update(Directory directory, DirectoryEventArgs e);
    //}
    //class FileSystemWatcher : IObserver
    //{
    //    public void Update(Directory directory, DirectoryEventArgs e)
    //    {
    //        switch (e.Condition)
    //        {
    //            case ECondition.Created:
    //                Console.WriteLine($"File {e.FilePath} was created in directory {directory.DirectoryPath}");
    //                break;
    //            case ECondition.Deleted:
    //                Console.WriteLine($"File {e.FilePath} was deleted in directory {directory.DirectoryPath}");
    //                break;
    //            case ECondition.Changed:
    //                Console.WriteLine($"File {e.FilePath} was changed in directory {directory.DirectoryPath}");
    //                break;
    //        }
    //    }
    //}

    //class Directory
    //{
    //    public string DirectoryPath { get; private set; }
    //    public List<string> Files { get; private set; }
    //    private delegate void DirectoryHandler(Directory directory, DirectoryEventArgs e);
    //    event DirectoryHandler Notify;

    //    public Directory(string directoryPath)
    //    {
    //        DirectoryPath = directoryPath;
    //        Files = new List<string>();
    //    }

    //    public void AddObserver(IObserver fileSystemWatcher)
    //    {
    //        Notify += fileSystemWatcher.Update;
    //    }

    //    public void AddFile(string filePath)
    //    {
    //        Files.Add(filePath);
    //        Notify.Invoke(this, new DirectoryEventArgs(filePath, ECondition.Created));
    //    }

    //    public void DeleteFile(string filePath)
    //    {
    //        if (Files.Contains(filePath))
    //        {
    //            Files.Remove(filePath);
    //            Notify.Invoke(this, new DirectoryEventArgs(filePath, ECondition.Deleted));
    //        }
    //    }

    //    public void ChangeFile(string filePath)
    //    {
    //        if (!Files.Contains(filePath))
    //        {
    //            Files.Add(filePath);
    //            Notify.Invoke(this, new DirectoryEventArgs(filePath, ECondition.Created));
    //        }
    //        else if (Files.Contains(filePath))
    //            Notify.Invoke(this, new DirectoryEventArgs(filePath, ECondition.Changed));
    //    }


    //}
    //class DirectoryEventArgs
    //{
    //    public string FilePath { get; }
    //    public ECondition Condition { get; }
    //    public DirectoryEventArgs(string filePath, ECondition condition)
    //    {
    //        FilePath = filePath;
    //        Condition = condition;
    //    }
    //}

    //enum ECondition
    //{
    //    Created,
    //    Deleted,
    //    Changed
    //}
    #endregion
}

