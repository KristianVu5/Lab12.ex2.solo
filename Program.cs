using System;
using System.IO;
using System.IO.IsolatedStorage;
class DirectoryTraversal
{
    static string ReadTextFileFromIsolatedStorage(string aFileName)
    {
        IsolatedStorageFile store = IsolatedStorageFile.GetStore(
            IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
        using (store)
        {
            IsolatedStorageFileStream stream = new IsolatedStorageFileStream(aFileName, FileMode.Open, FileAccess.Read, store);
            using (stream)
            {
                StreamReader reader = new StreamReader(stream);
                using (reader)
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }
    }

    static void WriteTextFileToIsolatedStorage(string aFileName, string aText)
    {
        IsolatedStorageFile store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User |
            IsolatedStorageScope.Assembly, null, null);
        using (store)
        {
            IsolatedStorageFileStream stream = new IsolatedStorageFileStream(aFileName, FileMode.Create,
                FileAccess.Write, store);
            using (stream)
            {
                StreamWriter writer = new StreamWriter(stream);
                using (writer)
                {
                    writer.Write(aText);
                }
            }
        }
    }
    static void Main()
    {
        try
        {
            string text = ReadTextFileFromIsolatedStorage("notes.txt");
            Console.WriteLine("Text read from isolated storage: {0}", text);
        }
        catch (IOException ioex)
        {
            Console.WriteLine("Error reading from isolated storage: {0}", ioex);
        }

        try
        {
            string text = "Just a test!";
            WriteTextFileToIsolatedStorage("note.txt", text);
            Console.WriteLine("Text written to isolated storage: {0}", text);
        }
        catch (IOException ioex)
        {
            Console.WriteLine("Error writing to isolated storage: {0}", ioex);
        }

    }
}