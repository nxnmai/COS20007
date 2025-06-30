namespace T1_Semester_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Student ID: 105549980
            // A = [2, 3, 5, 7, 11, 13, 17, 19, 23, 29]
            // => last 4 digits: 9980 -> B = [A[9], A[9], A[8], A[0]] = [29, 29, 23, 2]

            int[] B = { 29, 29, 23, 2 };
            string studentID = "105549980";

            // Create the file system
            FileSystem fileSystem = new FileSystem();

            // Add B[0] files (29 files)
            for (int i = 0; i < B[0]; i++)
            {
                string fileName = $"{studentID}-{i:D2}";
                fileSystem.Add(new File(fileName, 1000 + i * 100, "txt"));
            }

            // Add a folder with B[1] files (29 files)
            Folder folder1 = new Folder("Folder1");
            for (int i = 0; i < B[1]; i++)
            {
                string fileName = $"{studentID}-F1-{i:D2}";
                folder1.Add(new File(fileName, 2000 + i * 100, "txt"));
            }
            fileSystem.Add(folder1);

            // Add a folder containing a folder with B[2] files (23 files)
            Folder folder2 = new Folder("Folder2");
            Folder subFolder = new Folder("SubFolder");
            for (int i = 0; i < B[2]; i++)
            {
                string fileName = $"{studentID}-SF-{i:D2}";
                subFolder.Add(new File(fileName, 3000 + i * 100, "txt"));
            }
            folder2.Add(subFolder);
            fileSystem.Add(folder2);

            // Add B[3] empty folders (2 empty folders)
            for (int i = 0; i < B[3]; i++)
            {
                fileSystem.Add(new Folder($"EmptyFolder{i + 1}"));
            }

            // Print the contents 
            fileSystem.PrintContents();
        }
    }
}
