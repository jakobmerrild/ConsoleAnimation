using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleAnimation
{
    /// <summary>
    /// A class which represents an Animation of frames which can be drawn in the console. 
    /// </summary>
    public class Animation
    {
        /// <summary>
        /// Backing field for holding the frames of the animation.
        /// </summary>
        private List<Frame> frames;

        /// <summary>
        /// Gets or sets the frames per second of this animation.
        /// Default: 24
        /// </summary>
        public int FramesPerSecond {get; set;}

        /// <summary>
        /// Instantiates a new Animation ready to take 10 frames.
        /// </summary>
        public Animation(): this(10){}

        /// <summary>
        /// Instantiates a new Animation which is ready to take a
        /// specified number of frames.
        /// </summary>
        /// <param name="numberOfFrames">The expected number of frames the animation will be.</param>
        /// <returns>A new animation with no frames in it.</returns>
        public Animation(int numberOfFrames): this(new List<Frame>(numberOfFrames)){}
        
        /// <summary>
        /// Instantiates a new Animation which holds a number of frames.
        /// </summary>
        /// <param name="frames">The frames, in order, of the animation.</param>
        public Animation(IEnumerable<Frame> frames){
            this.frames = frames.ToList();
            FramesPerSecond = 24;
        }

        /// <summary>
        /// Add a frame at the end of the animation.
        /// </summary>
        /// <param name="frame">The frame to add.</param>
        /// <returns>This animation for chained method calling.</returns>
        public Animation AddFrame(Frame frame){
            frames.Add(frame);
            return this;
        }

        /// <summary>
        /// Overlay an animation on top of this animation by overlaying each frame of
        /// the specified animation on top of this animation's frames.
        /// </summary>
        /// <remarks>
        /// If either animation is longer than the other, any extra frames will be
        /// added at the end of the animation.
        /// </remarks>
        /// <param name="overLay">The animation to overlay.</param>
        /// <returns>This animation for chained method calling.</returns>
        public Animation AddOverLay(Animation overLay){
            var thisNumberOfFrames = frames.Count;
            var overLayNumberOfFrames = overLay.frames.Count;
            var suffix = new List<Frame>(Math.Abs(thisNumberOfFrames - overLayNumberOfFrames));
            if (thisNumberOfFrames > overLayNumberOfFrames)
            {
                suffix.AddRange(frames.Skip(overLayNumberOfFrames));
            }  
            else
            {
                suffix.AddRange(overLay.frames.Skip(thisNumberOfFrames));
            }
            this.frames = frames.Zip(overLay.frames, (ourFrame, theirFrame) => ourFrame.AddOverLay(theirFrame)).ToList();
            this.frames.AddRange(suffix);
            return this;
        }

        /// <summary>
        /// Animates this animation by drawing each frame in it.
        /// Drawing <see cref="FramesPerSecond"/> each second.
        /// </summary>
        public void Animate()
        {
            var oldCursorVisible = Console.CursorVisible;
            var oldBackGroundColor = Console.BackgroundColor;
            var oldForeGroundColor = Console.ForegroundColor;
            Console.CursorVisible = false;
            foreach(var frame in frames){
                Console.Clear();
                frame.Draw();
                Thread.Sleep(1000/FramesPerSecond);
            }
            Console.ForegroundColor = oldForeGroundColor;
            Console.BackgroundColor = oldBackGroundColor;
            Console.CursorVisible = oldCursorVisible;
        }
    }
}