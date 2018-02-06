namespace ConsoleAnimation
{
    public static class Animations
    {
        public static Animation BlinkingSun(int numberOfFrames)
        {
            var animation = new Animation(numberOfFrames);
            animation.FramesPerSecond = 2;
            for(int i = 0; i < numberOfFrames; i++)
            {
                if(i % 2 == 0){
                    animation.AddFrame(Frames.Sun);
                }
                else
                {
                    animation.AddFrame(new Frame());
                }
            }
            return animation;
        }

        public static Animation CheeringMan(int numberOfFrames)
        {
            var animation = new Animation(numberOfFrames);
            animation.FramesPerSecond = 4;
            var man = Frames.Man;
            var cheeringMan = man.Copy();
            cheeringMan.SetPixel(4, 8, Frame.EmptyPixel).SetPixel(6, 8, Frame.EmptyPixel);
            var middleMan = cheeringMan.Copy();
            cheeringMan.SetPixel(4, 7, '\\').SetPixel(6, 7, '/');
            middleMan.SetPixel(4, 7, '_').SetPixel(6, 7, '_');
            for(int i = 0; i < numberOfFrames; i++)
            {
                var step = i % 4;
                switch(step)
                {
                    case 0:
                        animation.AddFrame(man);
                    break;
                    case 1:
                    case 3:
                     animation.AddFrame(middleMan);
                    break;
                    case 2:
                        animation.AddFrame(cheeringMan);
                    break;
                }
            }
            return animation;
        }
    }
}