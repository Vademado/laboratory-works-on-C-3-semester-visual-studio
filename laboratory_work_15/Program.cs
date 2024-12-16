using laboratory_work_15;


internal class Program
{
    private static void Main(string[] args)
    {
        #region task1
        laboratory_work_15.FileSystemWatcher fileSystemWatcher = new laboratory_work_15.FileSystemWatcher("C:/Users/1/source/repos/laboratory_work_15/laboratory_work_15");
        Observer observer = new Observer();
        fileSystemWatcher.AddObserver(observer);

        fileSystemWatcher.AddFile("C:/Users/1/source/repos/laboratory_work_15/laboratory_work_15/Program.cs");
        fileSystemWatcher.AddFile("C:/Users/1/source/repos/laboratory_work_15/laboratory_work_15/FileSystemWatcher.cs");

        fileSystemWatcher.AddFile("C:/Users/1/source/repos/laboratory_work_15/laboratory_work_15/MyLogger.cs");
        
        Console.ReadKey();
        fileSystemWatcher.Stop();
        #endregion
    }
}