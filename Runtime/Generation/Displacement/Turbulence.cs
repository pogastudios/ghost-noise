using Packages.GhostNoise.Generation.Fractal;
using UnityEngine;

namespace Packages.GhostNoise.Generation.Displacement
{
	///<summary>
	/// Turbulence is a case of Perturb generator, that uses 3 Perlin noise generators as displacement source.
	///</summary>
	public class Turbulence : Generator
	{
		private readonly int _seed;
		private readonly Generator _src;
        private Generator _displacementX;
        private Generator _displacementY;
        private Generator _displacementZ;
        private float _frequency = 1;
		private int _octaves = 1;

		///<summary>
		/// Turbulence power, in other words, amount by which source will be perturbed.
		/// 
		/// Default value is 1.
		///</summary>
		public float Power { get; set; } = 1;

		///<summary>
		/// Frequency of perturbation noise. 
		/// 
		/// Default value is 1.
		///</summary>
		public float Frequency
		{
			get { return _frequency; }
			set
			{
				_frequency = value;
				CreateDisplacementSource();
			}
		}

		/// <summary>
		/// Octave count of perturbation noise
		/// 
		/// Default value is 6
		/// </summary>
		public int OctaveCount
		{
			get { return _octaves; }
			set
			{
				_octaves = value;
				CreateDisplacementSource();
			}
		}

		///<summary>
		/// Create new perturb generator
		///</summary>
		///<param name="source">Source generator</param>
		///<param name="seed">Seed value for perturbation noise</param>
		public Turbulence(Generator source, int seed)
		{
			_src = source;
			_seed = seed;
			Power = 1;
			Frequency = 1;

			OctaveCount = 6;
			CreateDisplacementSource();
		}

		public override float GetValue(float x, float y, float z)
		{
			Vector3 displacement = new Vector3(_displacementX.GetValue(x, y, z),_displacementY.GetValue(x,y,z),_displacementZ.GetValue(x,y,z))*Power;
			return _src.GetValue(x + displacement.x, y + displacement.y, z + displacement.z);
		}

		private void CreateDisplacementSource()
		{
		    _displacementX = new PinkNoise(_seed) {Frequency = Frequency, OctaveCount = OctaveCount};
		    _displacementY = new PinkNoise(_seed + 1) {Frequency = Frequency, OctaveCount = OctaveCount};
		    _displacementZ = new PinkNoise(_seed + 2) {Frequency = Frequency, OctaveCount = OctaveCount};
		}
	}
}