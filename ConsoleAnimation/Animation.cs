using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleAnimation
{
    public class Animation
    {
        private List<Frame> frames;

        public int FramesPerSecond {get; set;}

        public Animation(): this(10){}
        public Animation(int numberOfFrames): this(new List<Frame>(numberOfFrames)){}
        
        public Animation(IEnumerable<Frame> frames){
            this.frames = frames.ToList();
            FramesPerSecond = 24;
        }

        public void AddFrame(Frame frame){
            frames.Add(frame);
        }

        public Animation AddOverLay(Animation overLay){
            this.frames = frames.Zip(overLay.frames, (ourFrame, theirFrame) => ourFrame.AddOverLay(theirFrame)).ToList();
            return this;
        }

        public void Animate(){
            var oldCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;
            foreach(var frame in frames){
                Console.Clear();
                frame.Draw();
                Thread.Sleep(1000/FramesPerSecond);
            }
            Console.CursorVisible = oldCursorVisible;
        }
    }
}