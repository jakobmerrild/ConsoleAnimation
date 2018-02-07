using System;

namespace ConsoleAnimation
{
    /// <summary>
    /// A class which represents a "Frame" in the console.
    /// A Frame is 'width x height' pixels of <see cref="char"> which
    /// can be "drawn" in the console.
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// The underlying char array which forms the frames pixels.
        /// </summary>
        private char[][] pixels;
        /// <summary>
        /// The width of the frame.
        /// </summary>
        private int width;
        /// <summary>
        /// The height of the frame.
        /// </summary>
        private int height;

        /// <summary>
        /// An empty pixel is the ' ' character, as it won't be drawn.
        /// </summary>
        public const char EmptyPixel = ' ';

        /// <summary>
        /// Instantiates a default frame with width and height of 10 pixels.
        /// </summary>
        public Frame(): this(10, 10){}

        /// <summary>
        /// Instantiates a frame with the specified width and height.
        /// </summary>
        /// <param name="width">The width of the frame in pixels.</param>
        /// <param name="height">The height of the frame in pixels.</param>
        public Frame(int width, int height)
        {
            this.width = width;
            this.height = height;
            pixels = new char[width][];
            for(int x = 0; x < width; x++){
                pixels[x] = new char[height];
            }
            // Set all pixels to the empty pixel.
            Reset();
        }

        /// <summary>
        /// Resets the frame by setting all pixels to be the empty pixel
        /// </summary>
        /// <returns>The frame itself for chained method calling.</returns>
        public Frame Reset()
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    SetPixel(x, y, EmptyPixel);
                }    
            }  
            return this; 
        }

        /// <summary>
        /// Sets a pixel to be a certain value given its coordinates.
        /// </summary>
        /// <remarks>
        /// If the specified pixel is not within the frame nothing will happen.
        /// </remark>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixel.</param>
        /// <param name="value">The value it should be set to.</param>
        /// <returns>The frame itself for chained method calling.</returns>
        public Frame SetPixel(int x, int y, char value){
            if(!WithinFrame(x, y))
            {
                System.Console.Error.WriteLine("Trying to set pixel outside frame.");
                return this;
            }
            pixels[x][y] = value;
            return this;
        }

        /// <summary>
        /// Draws the frame in the console.
        /// </summary>
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

        /// <summary>
        /// Creates a copy of this frame.
        /// </summary>
        /// <returns>The new frame.</returns>
        public Frame Copy(){
            var newFrame = new Frame(this.width, this.height);
            newFrame.AddOverLay(this);
            return newFrame;
        }
        
        /// <summary>
        /// Overlay another frame on top of this one.
        /// Every non-empty pixel in the overlay will overwrite the corresponding
        /// pixel in this frame. Any pixels in the overlay which are outside
        /// this frame will be ignored.
        /// </summary>
        /// <param name="overLay">The frame to overlay this.</param>
        /// <returns>This frame for chained method calling.</returns>
        public Frame AddOverLay(Frame overLay){
            for(var x = 0; x < width; x++){
                for(var y = 0; y < height; y++)
                {
                    if(overLay.WithinFrame(x,y) && overLay.pixels[x][y] != EmptyPixel)
                        SetPixel(x, y, overLay.pixels[x][y]);
                }
            }
            return this;
        }

        /// <summary>
        /// Checks that a certain pixel is with this frame.
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixel.</param>
        /// <returns></returns>
        private bool WithinFrame(int x, int y){
            return x >= 0 && x < width && y >= 0 && y < height;
        }

    }
}