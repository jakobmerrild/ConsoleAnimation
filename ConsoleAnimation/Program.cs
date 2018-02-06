using System;

namespace ConsoleAnimation
{
    class Program
    {
        static void Main(string[] args)
        {
            var animation = Animations.CheeringMan(20).AddOverLay(Animations.BlinkingSun(20));
            animation.FramesPerSecond = 2;
            animation.Animate();
        }
    }
}
