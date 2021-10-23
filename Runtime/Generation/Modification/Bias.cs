using System;

namespace Packages.GhostNoise.Generation.Modification
{
    /// <summary>
    /// Bias generator is used to "shift" mean value of source noise. Source is assumed to have values between -1 and 1; after Bias is applied,
    /// the result is still between -1 and 1, but the points that were equal to 0 are shifted by <i>bias value</i>.
    /// </summary>
    public class Bias : Generator
    {
        private readonly float _bias;
        private readonly Generator _src;

        public Bias(Generator source, float bias)
        {
            if (_bias <= -1 || _bias >= 1)
                throw new ArgumentException("Bias must be between -1 and 1");

            _src = source;
            _bias = bias / (1f + bias);
        }

        public override float GetValue(float x, float y, float z)
        {
            var f = _src.GetValue(x, y, z);
            // clamp f to [-1,1] so that we don't ever get a division by 0 error
            if (f < -1)
                f = -1;
            if (f > 1)
                f = 1;
            return (f + 1.0f) / (1.0f - _bias * (1.0f - f) * 0.5f) - 1.0f;
        }
    }
}