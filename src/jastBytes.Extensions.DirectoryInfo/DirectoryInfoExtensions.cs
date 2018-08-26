namespace System.IO
{
    /// <summary>
    /// Extensions for the DirectoryInfo class
    /// </summary>
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// Deletes all files from the given target directory but leaves the directory structure untouched.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="includeSubdirectories"></param>
        public static void CleanFiles(this DirectoryInfo target, bool includeSubdirectories = false)
        {
            foreach (var info in target.GetFiles())
            {
                File.Delete(info.FullName);
            }

            if (!includeSubdirectories) return;

            // Clean each subdirectory using recursion.
            foreach (var diSourceSubDir in target.GetDirectories())
            {
                diSourceSubDir.CleanFiles(true);
            }
        }

        /// <summary>
        /// Deletes everything from the given target directory including files and subdirectories.
        /// </summary>
        /// <param name="target"></param>
        public static void Clean(this DirectoryInfo target)
        {
            foreach (var info in target.GetFiles())
            {
                File.Delete(info.FullName);
            }
            foreach (var info in target.GetDirectories())
            {
                Directory.Delete(info.FullName, true);
            }
        }

        /// <summary>
        /// Copies all files in the source folder to the destination folder and creates the destination folder if needed. 
        /// Copies all subdirectories if requested.
        /// </summary>
        /// <param name="source">Source directory</param>
        /// <param name="target">Target directory</param>
        /// <param name="options"></param>
        public static void Copy(this DirectoryInfo source, DirectoryInfo target, CopyOptions options = null)
        {
            options = options ?? new CopyOptions();
            Directory.CreateDirectory(target.FullName);

            if (options.CleanTargetDirectory) target.Clean();

            // Copy each file into the new directory.
            foreach (var fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), options.OverwriteExisting);
            }

            if (!options.IncludeSubdirectories) return;

            // Copy each subdirectory using recursion.
            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                Copy(diSourceSubDir, nextTargetSubDir, options);
            }
        }

        /// <summary>
        /// Moves all files within source matched by the searchPattern to the target and creates the destination folder if needed.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="searchPattern"></param>
        public static void MoveFiles(this DirectoryInfo source, DirectoryInfo target, string searchPattern = "*.*")
        {
            Directory.CreateDirectory(target.FullName);

            foreach (var file in source.GetFiles(searchPattern))
            {
                file.MoveTo(Path.Combine(target.FullName, Path.GetFileName(file.Name)));
            }
        }
    }

    public class CopyOptions
    {
        /// <summary>
        /// Switch to indicate that all subdirectories should be copied as well
        /// </summary>
        public bool IncludeSubdirectories { get; set; }

        /// <summary>
        /// Switch to indicate that existing files should be overwritten
        /// </summary>
        public bool OverwriteExisting { get; set; }

        /// <summary>
        /// Switch to indicate that all files and directories should be deleted from target directory prior copying
        /// </summary>
        public bool CleanTargetDirectory { get; set; }
    }
}
