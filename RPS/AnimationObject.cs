using System;
using System.IO;

namespace RPS
{
    class AnimationObject : asd.TextureObject2D
    {
        private asd.Texture2D[] Textures;
        public int Fps;
        private int Count;
        private bool IsFinished;
        private event Action OnFinished;
        public bool IsLoop;

        public AnimationObject(string name, int fps, Action onFinished, bool isLoop = false)
        {
            var list = Directory.GetFiles(name);
            Array.Sort(list);
            Textures = new asd.Texture2D[list.Length];

            for (int i = 0; i < list.Length; i++)
            {
                Textures[i] = asd.Engine.Graphics.CreateTexture2D(list[i]);
            }

            Fps = fps;
            IsFinished = false;
            OnFinished = onFinished;
            Count = 0;
            IsLoop = isLoop;
            Texture = Textures[0];
        }

        protected override void OnUpdate()
        {
            int flame = Count * Fps / 60;
            if (flame < Textures.Length)
            {
                Texture = Textures[flame];
            }
            else if (IsLoop) Count = 0;
            else if (!IsFinished)
            {
                IsFinished = false;
                OnFinished?.Invoke();
            }
            Count++;
        }
    }
}
