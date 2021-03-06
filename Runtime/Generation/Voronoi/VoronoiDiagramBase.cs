using Packages.GhostNoise.Generation.Displacement;
using UnityEngine;

namespace Packages.GhostNoise.Generation.Voronoi
{
    /// <summary>
    /// Base class for Voronoi diagrams generators. Voronoi diagrams use a set of control points, that are somehow distributed, and for every point calculate distances to the closest control points.
    /// These distances are then combined to obtain final noise value.
    /// This generator distributes control points by randomly displacing points with integer coordinates. Thus, every unit-sized cube will have a single control point in it,
    /// randomly placed.
    /// </summary>
    public abstract class VoronoiDiagramBase : Generator
    {
        private readonly LatticeNoise[] m_ControlPointSource;

        /// <summary>
        /// Noise period. Used for repeating (seamless) noise.
        /// When Period &gt;0 resulting noise pattern repeats exactly every Period, for all coordinates.
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// Frequency of control points. This has the same effect as applying <see cref="Scale"/> transform to the generator, or placing control points closer together (for high frequency) or further apart (for low frequency)
        /// </summary>
        public float Frequency { get; set; }
        /// <summary>
        /// Create new Voronoi diagram using seed. Control points will be obtained using random displacment seeded by supplied value
        /// </summary>
        /// <param name="seed">Seed value</param>
        protected VoronoiDiagramBase(int seed)
        {
            Frequency = 1;
            m_ControlPointSource = new[] { new LatticeNoise(seed), new LatticeNoise(seed + 1), new LatticeNoise(seed + 2), };
        }

        public override float GetValue(float x, float y, float z)
        {
            if (Period > 0)
            {
                // make periodic lattice. Repeat every Period cells
                x = x % Period; if (x < 0) x += Period;
                y = y % Period; if (y < 0) y += Period;
                z = z % Period; if (z < 0) z += Period;
            }

            // stretch values to match desired frequency
            x *= Frequency;
            y *= Frequency;
            z *= Frequency;

            float min1 = float.MaxValue, min2 = float.MaxValue, min3 = float.MaxValue;

            int xc = Mathf.FloorToInt(x);
            int yc = Mathf.FloorToInt(y);
            int zc = Mathf.FloorToInt(z);

            var v = new Vector3(x, y, z);

            for (int ii = xc - 1; ii < xc + 2; ii++)
            {
                for (int jj = yc - 1; jj < yc + 2; jj++)
                {
                    for (int kk = zc - 1; kk < zc + 2; kk++)
                    {
                        Vector3 displacement = new Vector3(
                            m_ControlPointSource[0].GetValue(ii, jj, kk) * 0.5f + 0.5f,
                            m_ControlPointSource[1].GetValue(ii, jj, kk) * 0.5f + 0.5f,
                            m_ControlPointSource[2].GetValue(ii, jj, kk) * 0.5f + 0.5f);

                        Vector3 cp = new Vector3(ii, jj, kk) + displacement;
                        float distance = Vector3.SqrMagnitude(cp - v);

                        if (distance < min1)
                        {
                            min3 = min2;
                            min2 = min1;
                            min1 = distance;
                        }
                        else if (distance < min2)
                        {
                            min3 = min2;
                            min2 = distance;
                        }
                        else if (distance < min3)
                        {
                            min3 = distance;
                        }
                    }
                }
            }

            return GetResult(Mathf.Sqrt(min1), Mathf.Sqrt(min2), Mathf.Sqrt(min3));
        }

        protected abstract float GetResult(float min1, float min2, float min3);

    }
}