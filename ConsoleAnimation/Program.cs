using System;

namespace ConsoleAnimation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            var frameReader = new FrameReader("frames.txt");
            frameReader.ReadFrame(7,2).Draw();
        }
    }
}
