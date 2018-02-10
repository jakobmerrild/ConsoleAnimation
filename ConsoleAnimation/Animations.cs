namespace ConsoleAnimation
{
    /// <summary>
    /// Static class which can create some 'standard' animations.
    /// </summary>
    public static class Animations
    {
        /// <summary>
        /// Creates a 'blinking sun' animation by use of <see cref="Frames.Sun"/>
        /// and an empty frame. The animation is best played at 2 frames per second.
        /// Which is also what it will be set to.
        /// </summary>
        /// <param name="numberOfFrames">How long the animation should be in frames.</param>
        /// <returns>An animation where every other frame is a sun and every other frame is empty.</returns>
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

        /// <summary>
        /// Create a 'cheering man' animation.
        /// The animation is best played at 4 frames per second.
        /// </summary>
        /// <param name="numberOfFrames">How long the animation should be in frames.</param>
        /// <returns>An animation of a man who continously raises and lowers his arms.</returns>
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

        /// <summary>
        /// Creates an anmation of the "meh" ASCII drawing utilizing <see cref="Frames.Meh"/>
        /// ¯\_(ツ)_/¯ --> for reference
        /// </summary>
        /// <param name="numberOfFrames">How long the animation should be in frames.</param>
        /// <returns>An animation where every other frame is a sun and every other frame is empty.</returns>
        public static Animation ShrugingMeh(int numberOfFrames) {
            var animation = new Animation(numberOfFrames);
            animation.FramesPerSecond = 2;

            var meh = Frames.Meh;
            var mehShrug = meh.Copy();

            // potentially we can have a question asking how we can avoid so much lines
            // that do the same with small alterations
            mehShrug.SetPixel(0, 0, '¯');
            mehShrug.SetPixel(1, 0, '\\');
            mehShrug.SetPixel(2, 0, '_');

            mehShrug.SetPixel(8, 0, '¯');
            mehShrug.SetPixel(7, 0, '/');
            mehShrug.SetPixel(6, 0, '_');

            for(int i = 0; i < numberOfFrames; i++) {
                if(i % 2 == 0){
                    animation.AddFrame(meh);
                } else {
                    animation.AddFrame(mehShrug);
                }
                
            }
            return animation;
        }
    }
}