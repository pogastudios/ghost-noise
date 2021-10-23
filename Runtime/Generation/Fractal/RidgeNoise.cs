using System;
using UnityEngine;

namespace Packages.GhostNoise.Generation.Fractal
{
    /// <summary>
    /// This generator adds samples with weight decreasing with frequency, like Perlin noise; however, each signal is taken as absolute value, and weighted by previous (i.e. lower-frequency) signal,
    /// creating a sort of feedback loop. Resulting noise has sharp ridges, somewhat resembling cliffs. This is useful for terrain generation.
    /// </summary>
    public class RidgeNoise : FractalNoiseBase
    {
        private float _exponent;
        private float[] _spectralWeights;
        private float _weight;

        /// <summary>
        /// Exponent defines how fast weights decrease with frequency. The higher the exponent, the less weight is given to high frequencies. 
        /// Default value is 1
        /// </summary>
        public float Exponent
        {
            get { return _exponent; }
            set
            {
                _exponent = value;
                OnParamsChanged();
            }
        }

        /// <summary>
        /// Offset is applied to signal at every step. Default value is 1
        /// </summary>
        public float Offset { get; set; }

        /// <summary>
        /// Gain is the weight factor for previous-step signal. Higher gain means more feedback and noisier ridges. 
        /// Default value is 2.
        /// </summary>
        public float Gain { get; set; }

        ///<summary>
        /// Create new ridge generator using seed (seed is used to create a <see cref="GradientNoise"/> source)
        ///</summary>
        ///<param name="seed">seed value</param>
        public RidgeNoise(int seed)
            : base(seed)
        {
            Offset = 1;
            Gain = 2;
            Exponent = 1;
        }

        ///<summary>
        /// Create new ridge generator with user-supplied source. Usually one would use this with <see cref="ValueNoise"/> or gradient noise with less dimensions, but 
        /// some weird effects may be achieved with other generators.
        ///</summary>
        ///<param name="source">noise source</param>
        public RidgeNoise(Generator source)
            : base(source)
        {
            Offset = 1;
            Gain = 2;
            Exponent = 1;
        }

        protected override float CombineOctave(int curOctave, float signal, float value)
        {
            if (curOctave == 0)
                _weight = 1;
            // Make the ridges.
            signal = Offset - Mathf.Abs(signal);

            // Square the signal to increase the sharpness of the ridges.
            signal *= signal;

            // The weighting from the previous octave is applied to the signal.
            // Larger values have higher weights, producing sharp points along the
            // ridges.
            signal *= _weight;

            // Weight successive contributions by the previous signal.
            _weight = signal * Gain;
            if (_weight > 1)
                _weight = 1;

            if (_weight < 0)
                _weight = 0;

            // Add the signal to the output value.
            return value + (signal * _spectralWeights[curOctave]);
        }

        protected override void OnParamsChanged()
        {
            PrecalculateWeights();
        }

        private void PrecalculateWeights()
        {
            float frequency = 1;
            _spectralWeights = new float[OctaveCount];
            for (int ii = 0; ii < OctaveCount; ii++)
            {
                // Compute weight for each frequency.
                _spectralWeights[ii] = Mathf.Pow(frequency, -Exponent);
                frequency *= Lacunarity;
            }
        }

    }
}