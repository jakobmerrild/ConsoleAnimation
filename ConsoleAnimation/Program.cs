using System;

namespace ConsoleAnimation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var animation = Animations.CheeringMan(20).AddOverLay(Animations.BlinkingSun(20));
            animation.FramesPerSecond = 2;
            animation.Animate();

            var animation2 = Animations.ShrugingMeh(4);
            animation2.FramesPerSecond = 2;
            animation2.Animate();
        }
    }
}
