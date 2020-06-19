
// MIT License
// 
// Copyright (c) 2009-2017 Luca Piccioni
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// This file is automatically generated

#pragma warning disable 649, 1572, 1573

// ReSharper disable RedundantUsingDirective
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

using Khronos;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable JoinDeclarationAndInitializer

namespace OpenGL
{
	public partial class Gl
	{
		/// <summary>
		/// [GL] Value of GL_DETAIL_TEXTURE_2D_SGIS symbol.
		/// </summary>
		[RequiredByFeature("GL_SGIS_detail_texture")]
		public const int DETAIL_TEXTURE_2D_SGIS = 0x8095;

		/// <summary>
		/// [GL] Value of GL_DETAIL_TEXTURE_2D_BINDING_SGIS symbol.
		/// </summary>
		[RequiredByFeature("GL_SGIS_detail_texture")]
		public const int DETAIL_TEXTURE_2D_BINDING_SGIS = 0x8096;

		/// <summary>
		/// [GL] Value of GL_LINEAR_DETAIL_SGIS symbol.
		/// </summary>
		[RequiredByFeature("GL_SGIS_detail_texture")]
		public const int LINEAR_DETAIL_SGIS = 0x8097;

		/// <summary>
		/// [GL] Value of GL_LINEAR_DETAIL_ALPHA_SGIS symbol.
		/// </summary>
		[RequiredByFeature("GL_SGIS_detail_texture")]
		public const int LINEAR_DETAIL_ALPHA_SGIS = 0x8098;

		/// <summary>
		/// [GL] Value of GL_LINEAR_DETAIL_COLOR_SGIS symbol.
		/// </summary>
		[RequiredByFeature("GL_SGIS_detail_texture")]
		public const int LINEAR_DETAIL_COLOR_SGIS = 0x8099;

		/// <summary>
		/// [GL] Value of GL_DETAIL_TEXTURE_LEVEL_SGIS symbol.
		/// </summary>
		[RequiredByFeature("GL_SGIS_detail_texture")]
		public const int DETAIL_TEXTURE_LEVEL_SGIS = 0x809A;

		/// <summary>
		/// [GL] Value of GL_DETAIL_TEXTURE_MODE_SGIS symbol.
		/// </summary>
		[RequiredByFeature("GL_SGIS_detail_texture")]
		public const int DETAIL_TEXTURE_MODE_SGIS = 0x809B;

		/// <summary>
		/// [GL] Value of GL_DETAIL_TEXTURE_FUNC_POINTS_SGIS symbol.
		/// </summary>
		[RequiredByFeature("GL_SGIS_detail_texture")]
		public const int DETAIL_TEXTURE_FUNC_POINTS_SGIS = 0x809C;

		/// <summary>
		/// [GL] glDetailTexFuncSGIS: Binding for glDetailTexFuncSGIS.
		/// </summary>
		/// <param name="target">
		/// A <see cref="T:TextureTarget"/>.
		/// </param>
		/// <param name="n">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="points">
		/// A <see cref="T:float[]"/>.
		/// </param>
		[RequiredByFeature("GL_SGIS_detail_texture")]
		public static void DetailTexFuncSGIS(TextureTarget target, int n, float[] points)
		{
			Debug.Assert(points.Length > 0 && (points.Length % 2) == 0, "empty or not multiple of 2");
			unsafe {
				fixed (float* p_points = points)
				{
					Debug.Assert(Delegates.pglDetailTexFuncSGIS != null, "pglDetailTexFuncSGIS not implemented");
					Delegates.pglDetailTexFuncSGIS((int)target, n, p_points);
					LogCommand("glDetailTexFuncSGIS", null, target, n, points					);
				}
			}
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glDetailTexFuncSGIS: Binding for glDetailTexFuncSGIS.
		/// </summary>
		/// <param name="target">
		/// A <see cref="T:TextureTarget"/>.
		/// </param>
		/// <param name="points">
		/// A <see cref="T:float[]"/>.
		/// </param>
		[RequiredByFeature("GL_SGIS_detail_texture")]
		public static void DetailTexFuncSGIS(TextureTarget target, float[] points)
		{
			Debug.Assert(points.Length > 0 && (points.Length % 2) == 0, "empty or not multiple of 2");
			unsafe {
				fixed (float* p_points = points)
				{
					Debug.Assert(Delegates.pglDetailTexFuncSGIS != null, "pglDetailTexFuncSGIS not implemented");
					Delegates.pglDetailTexFuncSGIS((int)target, points.Length / 2, p_points);
					LogCommand("glDetailTexFuncSGIS", null, target, points.Length / 2, points					);
				}
			}
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glGetDetailTexFuncSGIS: Binding for glGetDetailTexFuncSGIS.
		/// </summary>
		/// <param name="target">
		/// A <see cref="T:TextureTarget"/>.
		/// </param>
		/// <param name="points">
		/// A <see cref="T:float[]"/>.
		/// </param>
		[RequiredByFeature("GL_SGIS_detail_texture")]
		public static void GetDetailTexFuncSGIS(TextureTarget target, [Out] float[] points)
		{
			unsafe {
				fixed (float* p_points = points)
				{
					Debug.Assert(Delegates.pglGetDetailTexFuncSGIS != null, "pglGetDetailTexFuncSGIS not implemented");
					Delegates.pglGetDetailTexFuncSGIS((int)target, p_points);
					LogCommand("glGetDetailTexFuncSGIS", null, target, points					);
				}
			}
			DebugCheckErrors(null);
		}

		internal static unsafe partial class Delegates
		{
			[RequiredByFeature("GL_SGIS_detail_texture")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glDetailTexFuncSGIS(int target, int n, float* points);

			[RequiredByFeature("GL_SGIS_detail_texture")]
			[ThreadStatic]
			internal static glDetailTexFuncSGIS pglDetailTexFuncSGIS;

			[RequiredByFeature("GL_SGIS_detail_texture")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glGetDetailTexFuncSGIS(int target, float* points);

			[RequiredByFeature("GL_SGIS_detail_texture")]
			[ThreadStatic]
			internal static glGetDetailTexFuncSGIS pglGetDetailTexFuncSGIS;

		}
	}

}