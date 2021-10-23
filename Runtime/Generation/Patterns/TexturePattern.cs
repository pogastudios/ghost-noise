using System;
using UnityEngine;

namespace Packages.GhostNoise.Generation.Patterns
{
    ///<summary>
    /// This generator does the opposite of texture generation. It takes a texture and returns its red channel as a noise value.
    /// Use it to incorporate hand-created patterns in your generation.
    ///</summary>
    public class TexturePattern : Generator
    {
        private readonly Color[] _pixels;
        private readonly int _width;
        private readonly int _height;
        private readonly TextureWrapMode _wrapMode;

        ///<summary>
        /// Create new texture generator
        ///</summary>
        ///<param name="texture">Texture to use. It must be readable. The texture is read in constructor, so any later changes to it will not affect this generator</param>
        ///<param name="wrapMode">Wrapping mode</param>
        public TexturePattern(Texture2D texture, TextureWrapMode wrapMode)
        {
            _pixels = texture.GetPixels();
            _width = texture.width;
            _height = texture.height;

            _wrapMode = wrapMode;
        }

       
        public override float GetValue(float x, float y, float z)
        {
            int ix = Mathf.FloorToInt(x * _width);
            int iy = Mathf.FloorToInt(y * _height);
            ix = Wrap(ix, _width);
            iy = Wrap(iy, _height);
            var c = _pixels[iy * _width + ix];
            return c.r * 2 - 1;
        }

        private int Wrap(int i, int size)
        {
            switch (_wrapMode)
            {
                case TextureWrapMode.Repeat:
                    return i >= 0 ? i % size : (i % size + size);
                case TextureWrapMode.Clamp:
                    return i < 0 ? 0 : i > size ? size - 1 : i;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}