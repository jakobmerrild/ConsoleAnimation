using System;

namespace ConsoleAnimation
{
    public class Frame
    {
        private char[][] pixels;
        private int width, height;
        public const char EmptyPixel = ' ';

        public Frame(): this(10, 10){}
        public Frame(int width, int height)
        {
            this.width = width;
            this.height = height;
            pixels = new char[width][];
            for(int x = 0; x < width; x++){
                pixels[x] = new char[height];
            }
            Reset();
        }

        public Frame Reset()
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    pixels[x][y] = EmptyPixel;
                }    
            }  
            return this; 
        }

        public Frame SetPixel(int x, int y, char value){
            if(!WithinFrame(x, y))
            {
                System.Console.Error.WriteLine("Trying to set pixel outside frame.");
                return this;
            }
            pixels[x][y] = value;
            return this;
        }

        public void Draw()
        {
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Console.Write(pixels[x][y]);
                }
                System.Console.WriteLine();
            }

        }

        public Frame Copy(){
            var newFrame = new Frame(this.width, this.height);
            newFrame.AddOverLay(this);
            return newFrame;
        }
        
        public Frame AddOverLay(Frame overLay){
            for(var x = 0; x < width; x++){
                for(var y = 0; y < height; y++)
                {
                    if(overLay.WithinFrame(x,y) && overLay.pixels[x][y] != EmptyPixel)
                        pixels[x][y] = overLay.pixels[x][y];
                }
            }
            return this;
        }
        private bool WithinFrame(int x, int y){
            return x >= 0 && x < width && y >= 0 && y < height;
        }

    }
}