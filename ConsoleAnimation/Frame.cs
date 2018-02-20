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
        /// The color of each pixel.
        /// </summary>
        private ConsoleColor[][] pixelColors;

        /// <summary>
        /// The background colors of each pixel
        /// </summary>
        private ConsoleColor[][] backgroundColors;

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
        /// The default pixel color which is used when no color is specified.
        /// </summary>
        public const ConsoleColor DefaultPixelColor = ConsoleColor.Gray;

        /// <summary>
        /// The default background color which is used when no background color is specified.
        /// </summary>
        public const ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;

        /// <summary>
        /// Gets or sets the current pixel color for this frame.
        /// This is the color which will be used for new pixels if no other
        /// color is specified.
        /// </summary>
        public ConsoleColor PixelColor { get; set; }

        /// <summary>
        /// Gets or sets the current background color for this frame.
        /// This is the color which will be used for the background for new
        /// pixels if no other color is specified.
        /// </summary>
        public ConsoleColor BackGroundColor { get; set; }

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
            pixelColors = new ConsoleColor[width][];
            backgroundColors = new ConsoleColor[width][];
            for(int x = 0; x < width; x++){
                pixels[x] = new char[height];
                pixelColors[x] = new ConsoleColor[height];
                backgroundColors[x] = new ConsoleColor[height];
            }
            PixelColor = DefaultPixelColor;
            BackGroundColor = DefaultBackgroundColor;
            // Set all pixels to the empty pixel.
            Reset();
        }

        /// <summary>
        /// Resets the frame by setting all pixels to be the empty pixel.
        /// And setting all pixel and background colors to the currently specified values.
        /// </summary>
        /// <returns>The frame itself for chained method calling.</returns>
        public Frame Reset()
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    SetPixel(x, y, EmptyPixel, PixelColor, BackGroundColor);
                }    
            }  
            return this; 
        }

        /// <summary>
        /// Sets the background color at a specified coordinate to a given value.
        /// </summary>
        /// <param name="x">The x-coordinate</param>
        /// <param name="y">The y-coordinate</param>
        /// <param name="backGroundColor">The value to set it to.</param>
        /// <returns>The frame itself for chained method calling.</returns>
        public Frame SetBackgroundColor(int x, int y, ConsoleColor backGroundColor)
        {
            if(!WithinFrame(x,y))
            {
                System.Console.Error.WriteLine("Trying to set background outside frame.");
                return this;
            }
            backgroundColors[x][y] = backGroundColor;
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
            return SetPixel(x, y, value, PixelColor);
        }

        /// <summary>
        /// Sets a pixel to be a certain value and have a certain color given its coordinates
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixe.</param>
        /// <param name="pixelValue">The value the pixel should have.</param>
        /// <param name="pixelColor">The color the pixel should have.</param>
        /// <returns>The frame itself for chained method calling.</returns>
        public Frame SetPixel(int x, int y, char pixelValue, ConsoleColor pixelColor){
            return SetPixel(x, y, pixelValue, pixelColor, BackGroundColor);
        }
        
        /// <summary>
        /// Sets a pixel to be a certain value, have a certain color, and a certain background color given its coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel</param>
        /// <param name="y">The y-coordinate of the pixel</param>
        /// <param name="pixelValue">The value the pixel should have</param>
        /// <param name="pixelColor">The color the pixel should have</param>
        /// <param name="backgroundColor">The background color the pixel should have</param>
        /// <returns></returns>
        public Frame SetPixel(int x, int y, char pixelValue, ConsoleColor pixelColor, ConsoleColor backgroundColor){
            if(!WithinFrame(x, y))
            {
                System.Console.Error.WriteLine("Trying to set pixel outside frame.");
                return this;
            }
            pixels[x][y] = pixelValue;
            SetPixelColor(x, y, pixelColor);
            SetBackgroundColor(x, y, backgroundColor);
            return this;  
        }

        /// <summary>
        /// Sets the pixel(foreground) color at a given coordinate
        /// </summary>
        /// <param name="x">The x-coordinate</param>
        /// <param name="y">The y-coordinate</param>
        /// <param name="value">The color the pixel should have</param>
        /// <returns>The frame itself for chained method calling</returns>
        public Frame SetPixelColor(int x, int y, ConsoleColor value){
            if(!WithinFrame(x,y))
            {
                System.Console.Error.WriteLine("Trying to set color outside frame.");
                return this;
            }
            pixelColors[x][y] = value;
            return this;
        }

        /// <summary>
        /// Draws the frame in the console.
        /// </summary>
        public void Draw()
        {
            var oldForegroundColor = Console.ForegroundColor;
            var oldBackgroundColor = Console.BackgroundColor;
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Console.ForegroundColor = pixelColors[x][y];
                    Console.BackgroundColor = backgroundColors[x][y];
                    Console.Write(pixels[x][y]);
                }
                System.Console.WriteLine();
            }
            Console.ForegroundColor = oldForegroundColor;
            Console.BackgroundColor = oldBackgroundColor;
        }

        /// <summary>
        /// Creates a copy of this frame.
        /// </summary>
        /// <returns>The new frame.</returns>
        public Frame Copy(){
            var newFrame = new Frame(this.width, this.height);
            newFrame.AddOverLay(this);
            newFrame.PixelColor = this.PixelColor;
            newFrame.BackGroundColor = this.BackGroundColor;
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
                        SetPixel(x, y, overLay.pixels[x][y], overLay.pixelColors[x][y], overLay.backgroundColors[x][y]);
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
