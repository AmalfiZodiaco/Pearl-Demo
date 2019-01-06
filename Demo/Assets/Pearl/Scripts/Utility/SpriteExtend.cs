using UnityEngine;

namespace Pearl
{
    public static class SpriteExtend
    {
        public static Sprite Create(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0, 0));
        }
    }
}
