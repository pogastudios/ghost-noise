using System;
using Packages.GhostNoise.Generation.Displacement;
using UnityEngine;

namespace Packages.GhostNoise.Generation.Voronoi
{
    /// <summary>
    /// Voronoi cell diagram uses a set of control points to partition space into cells. Each point in space belongs to a cell that corresponds to closest control point.
    /// This generator distributes control points using a vector noise source, that displaces points with integer coordinates. Thus, every unit-sized cube will have a single control point in it,
    /// randomly placed. A user-supplied function is then used to obtain cell value for a given point.
    /// </summary>
    public class VoronoiCells : Generator
    {
        private readonly Func<int, int, int, float> _cellValueSource;
        private readonly LatticeNoise[] _controlPointSource;

        /// <summary>
        /// Noise period. Used for repeating (seamless) noise.
        /// When Period &gt;0 resulting noise pattern repeats exactly every Period, for all coordinates.
        /// </summary>
        public int Period
        {
            get;
            set;
        }

        /// <summary>
        /// Frequency of control points. This has the same effect as applying <see cref="Scale"/> transform to the generator, or placing control points closer together (for high frequency) or further apart (for low frequency)
        /// </summary>
        public float Frequency { get; set; }

        /// <summary>
        /// Create new Voronoi diagram using seed. Control points will be obtained using random displacment seeded by supplied value
        /// </summary>
        /// <param name="seed">Seed value</param>
        /// <param name="cellValueSource">Function that returns cell's value</param>
        public VoronoiCells(int seed, Func<int, int, int, float> cellValueSource)
        {
            Frequency = 1;
            _controlPointSource = new[] { new LatticeNoise(seed), new LatticeNoise(seed + 1), new LatticeNoise(seed + 2), }; _cellValueSource = cellValueSource;
        }

        public override float GetValue(float x, float y, float z)
        {
            if (Period > 0)
            {
                // make periodic lattice. Repeat every Period cells
                x %= Period; if (x < 0) x += Period;
                y %= Period; if (y < 0) y += Period;
                z %= Period; if (z < 0) z += Period;
            }

            x *= Frequency;
            y *= Frequency;
            z *= Frequency;
            float min = float.MaxValue;
            int ix = 0, iy = 0, iz = 0;

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
                            _controlPointSource[0].GetValue(ii, jj, kk) * 0.5f + 0.5f,
                            _controlPointSource[1].GetValue(ii, jj, kk) * 0.5f + 0.5f,
                            _controlPointSource[2].GetValue(ii, jj, kk) * 0.5f + 0.5f);

                        Vector3 cp = new Vector3(ii, jj, kk) + displacement;
                        float distance = Vector3.SqrMagnitude(cp - v);

                        if (distance < min)
                        {
                            min = distance;
                            ix = ii;
                            iy = jj;
                            iz = kk;
                        }
                    }
                }
            }

            return _cellValueSource(ix, iy, iz);
        }

    }
}