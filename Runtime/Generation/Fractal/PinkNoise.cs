using System;

namespace Packages.GhostNoise.Generation.Fractal
{
    /// <summary>
    /// Pink noise is a fractal noise that adds together weighted signals sampled at different frequencies, with weight inversely proportional to frequency. .
    /// When source noise is <see cref="GradientNoise"/>, this becomes Perlin noise.
    /// </summary>
    public class PinkNoise : FractalNoiseBase
    {
        private float _curPersitance;


        /// <summary>
        /// Persistence value determines how fast signal diminishes with frequency. i-th octave sugnal will be multiplied by presistence to the i-th power.
        /// Note that persistence values >1 are possible, but will not produce interesting noise (lower frequencies will just drown out)
        /// 
        /// Default value is 0.5
        /// </summary>
        public float Persistence { get; set; }

        ///<summary>
        /// Create new pink noise generator using seed. Seed is used to create a <see cref="GradientNoise"/> source. 
        ///</summary>
        ///<param name="seed">seed value</param>
        public PinkNoise(int seed) : base(seed)
        {
            Persistence = 0.5f;
        }

        ///<summary>
        /// Create new pink noise generator with user-supplied source. Usually one would use this with <see cref="ValueNoise"/> or gradient noise with less dimensions, but 
        /// some weird effects may be achieved with other generators.
        ///</summary>
        ///<param name="source">noise source</param>
        public PinkNoise(Generator source) : base(source)
        {
            Persistence = 0.5f;
        }

       
        protected override float CombineOctave(int curOctave, float signal, float value)
        {
            if (curOctave == 0)
                _curPersitance = 1;

            value = value + signal * _curPersitance;
            _curPersitance *= Persistence;

            return value;
        }
    }
}