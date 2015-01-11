// utils/common/mathlib.h 

using System;
using System.Runtime.InteropServices;

namespace DotMetamod.HLSDK
{
	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct Vec3
	{
		public float x = 0f;
		public float y = 0f;
		public float z = 0f;

		public Vec3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vec3(float[] arr)
		{
			if(arr.Length > 0)
				x = arr[0];

			if(arr.Length > 1)
				y = arr[1];

			if(arr.Length > 2)
				z = arr[2];
		}

		public Vec3(Vec3 vec)
		{
			x = vec.x;
			y = vec.y;
			z = vec.z;
		}

		public Vec3(float* vec)
		{
			x = vec[0];
			y = vec[1];
			z = vec[2];
		}

		public float this[ushort index]
		{
			get
			{
				switch(index)
				{
					case 0: return x;
					case 1: return y;
					case 2: return z;
					default: throw new IndexOutOfRangeException("Invalid Vec3 index");
				}
			}

			set
			{
				switch(index)
				{
					case 0: { x = value; break; }
					case 1: { y = value; break; }
					case 2: { z = value; break; }
					default: throw new IndexOutOfRangeException("Invalid Vec3 index");
				}
			}
		}

		public float[] GetArray()
		{
			return new float[]{ x, y, z };
		}

		public override string ToString ()
		{
			return string.Format("Vec3s({0}, {1}, {2})", x, y, z);
		}

		public static Vec3 operator-(Vec3 one, Vec3 two)
		{
			return new Vec3(one.x - two.x, one.y - two.y, one.z - two.z);
		}

		public static Vec3 operator+(Vec3 one, Vec3 two)
		{
			return new Vec3(one.x + two.x, one.y + two.y, one.z + two.z);
		}

		public static Vec3 operator/(Vec3 vec, float num)
		{
			return new Vec3(vec.x / num, vec.y / num, vec.z / num);
		}

		public static Vec3 operator*(Vec3 vec, float num)
		{
			return new Vec3(vec.x * num, vec.y * num, vec.z * num);
		}
	};
}

