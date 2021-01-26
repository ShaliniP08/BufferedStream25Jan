using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BufferedStream25Jan
{
    class Program
    {
        static void BufferedStreamFunc()
        {
            FileInfo file = new FileInfo(@"E:\\BufferedStream.txt");
            try
            {
                if (file.Directory.Exists) // Make sure the directory exists
                {
                    // Create a new file, if it exists it will be overwritten
                    using (FileStream fileStream = file.Create())
                    {
                        using (BufferedStream bs = new BufferedStream(fileStream, 10000))
                        {
                            for (int i = 1; i < 1000; i++)
                            {
                                String s = "This is line " + i + "\n";

                                byte[] bytes = Encoding.UTF8.GetBytes(s);

                                // Write to buffer, when the buffer is full it will 
                                // automatically push down the file.
                                bs.Write(bytes, 0, bytes.Length);
                            }
                            Console.WriteLine("Length : " + bs.Length.ToString());
                            // Flushing the remaining data in the buffer to the file.
                            bs.Flush();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Directory does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void Main(string[] args)
        {
            BufferedStreamFunc();
            Console.WriteLine("Finished!");
            Console.ReadLine();
        }
    }
}
