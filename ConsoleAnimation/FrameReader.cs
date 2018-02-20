using System.IO;
using System.Collections.Generic;

namespace ConsoleAnimation
{
    /// <summary>
    /// Class which can read frames from an underlying stream.
    /// </summary>
    public class FrameReader
    {
        /// <summary>
        /// The underlying stream reader.
        /// </summary>
        private StreamReader reader;

        /// <summary>
        /// Initialize a new isntance of the class which will use a specified stream reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        private FrameReader(StreamReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// Initialize a new instance of the class which will read from a specified stream.
        /// </summary>
        /// <param name="stream">The stream which should be read from by the instance.</param>
        public FrameReader(Stream stream):this(new StreamReader(stream)) {}

        /// <summary>
        /// Initialize a new instance of the class which will read from a specified file.
        /// </summary>
        /// <param name="filePath">The path to the file which should be read.</param>
        public FrameReader(string filePath) :this(File.OpenText(filePath)) {}

        /// <summary>
        /// Read a single frame from the underlying stream.
        /// </summary>
        /// <param name="width">The width of the frame to be read.</param>
        /// <param name="height">The height of the frame to be read.</param>
        /// <returns>A new frame, read from the underlying stream.</returns>
        public Frame ReadFrame(int width, int height)
        {
            var frame = new Frame(width, height);
            for(var y=0; y < height; y++)
            {
                var line = reader.ReadLine();
                for(var x=0; x < width; x++)
                {
                    frame.SetPixel(x, y, line[x]);
                }
            }
            return frame;
        }

        /// <summary>
        /// Read a number of frames from the underlying stream.
        /// </summary>
        /// <param name="width">The width of the frames to be read.</param>
        /// <param name="height">The height of the frames to be read.</param>
        /// <param name="numberOfFrames">The number of frames which should be read.</param>
        /// <returns>A List of frames which was read from the underlying stream.</returns>
        public List<Frame> ReadFrames(int width, int height , int numberOfFrames)
        {
            return null;
            for(var b = 0; b < numberOfFrames;b++)
            {

            }
        }
    }
}
