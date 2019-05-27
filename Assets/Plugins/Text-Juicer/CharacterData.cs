namespace BrunoMikoski.TextJuicer
{
    public struct CharacterData
    {
        private float progress;

        public float Progress
        {
            get { return progress; }
        }

        private float startingTime;

        private float totalAnimationTime;
        private int order;
        private int lastCharacter;

        public int Order
        {
            get { return order; }
        }
        public int LastCharacter
        {
            get { return lastCharacter; }
        }

        public CharacterData(float startTime, float targetAnimationTime, int targetOrder, int lastChar)
        {
            progress = 0.0f;
            startingTime = startTime;
            totalAnimationTime = (startingTime + targetAnimationTime) - startTime;
            order = targetOrder;
            lastCharacter = lastChar;
        }

        public void UpdateTime(float time)
        {
            if (time < startingTime)
                return;

            progress = (time - startingTime) / totalAnimationTime;
        }
    }
}
