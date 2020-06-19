
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
		/// [GL] Value of GL_TEXTURE_COVERAGE_SAMPLES_NV symbol.
		/// </summary>
		[RequiredByFeature("GL_NV_texture_multisample")]
		public const int TEXTURE_COVERAGE_SAMPLES_NV = 0x9045;

		/// <summary>
		/// [GL] Value of GL_TEXTURE_COLOR_SAMPLES_NV symbol.
		/// </summary>
		[RequiredByFeature("GL_NV_texture_multisample")]
		public const int TEXTURE_COLOR_SAMPLES_NV = 0x9046;

		/// <summary>
		/// [GL] glTexImage2DMultisampleCoverageNV: Binding for glTexImage2DMultisampleCoverageNV.
		/// </summary>
		/// <param name="target">
		/// A <see cref="T:TextureTarget"/>.
		/// </param>
		/// <param name="coverageSamples">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="colorSamples">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="internalFormat">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="width">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="height">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="fixedSampleLocations">
		/// A <see cref="T:bool"/>.
		/// </param>
		[RequiredByFeature("GL_NV_texture_multisample")]
		public static void TexImage2DMultisampleCoverageNV(TextureTarget target, int coverageSamples, int colorSamples, int internalFormat, int width, int height, bool fixedSampleLocations)
		{
			Debug.Assert(Delegates.pglTexImage2DMultisampleCoverageNV != null, "pglTexImage2DMultisampleCoverageNV not implemented");
			Delegates.pglTexImage2DMultisampleCoverageNV((int)target, coverageSamples, colorSamples, internalFormat, width, height, fixedSampleLocations);
			LogCommand("glTexImage2DMultisampleCoverageNV", null, target, coverageSamples, colorSamples, internalFormat, width, height, fixedSampleLocations			);
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glTexImage3DMultisampleCoverageNV: Binding for glTexImage3DMultisampleCoverageNV.
		/// </summary>
		/// <param name="target">
		/// A <see cref="T:TextureTarget"/>.
		/// </param>
		/// <param name="coverageSamples">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="colorSamples">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="internalFormat">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="width">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="height">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="depth">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="fixedSampleLocations">
		/// A <see cref="T:bool"/>.
		/// </param>
		[RequiredByFeature("GL_NV_texture_multisample")]
		public static void TexImage3DMultisampleCoverageNV(TextureTarget target, int coverageSamples, int colorSamples, int internalFormat, int width, int height, int depth, bool fixedSampleLocations)
		{
			Debug.Assert(Delegates.pglTexImage3DMultisampleCoverageNV != null, "pglTexImage3DMultisampleCoverageNV not implemented");
			Delegates.pglTexImage3DMultisampleCoverageNV((int)target, coverageSamples, colorSamples, internalFormat, width, height, depth, fixedSampleLocations);
			LogCommand("glTexImage3DMultisampleCoverageNV", null, target, coverageSamples, colorSamples, internalFormat, width, height, depth, fixedSampleLocations			);
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glTextureImage2DMultisampleNV: Binding for glTextureImage2DMultisampleNV.
		/// </summary>
		/// <param name="texture">
		/// A <see cref="T:uint"/>.
		/// </param>
		/// <param name="target">
		/// A <see cref="T:TextureTarget"/>.
		/// </param>
		/// <param name="samples">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="internalFormat">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="width">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="height">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="fixedSampleLocations">
		/// A <see cref="T:bool"/>.
		/// </param>
		[RequiredByFeature("GL_NV_texture_multisample")]
		public static void TextureImage2DMultisampleNV(uint texture, TextureTarget target, int samples, int internalFormat, int width, int height, bool fixedSampleLocations)
		{
			Debug.Assert(Delegates.pglTextureImage2DMultisampleNV != null, "pglTextureImage2DMultisampleNV not implemented");
			Delegates.pglTextureImage2DMultisampleNV(texture, (int)target, samples, internalFormat, width, height, fixedSampleLocations);
			LogCommand("glTextureImage2DMultisampleNV", null, texture, target, samples, internalFormat, width, height, fixedSampleLocations			);
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glTextureImage3DMultisampleNV: Binding for glTextureImage3DMultisampleNV.
		/// </summary>
		/// <param name="texture">
		/// A <see cref="T:uint"/>.
		/// </param>
		/// <param name="target">
		/// A <see cref="T:TextureTarget"/>.
		/// </param>
		/// <param name="samples">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="internalFormat">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="width">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="height">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="depth">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="fixedSampleLocations">
		/// A <see cref="T:bool"/>.
		/// </param>
		[RequiredByFeature("GL_NV_texture_multisample")]
		public static void TextureImage3DMultisampleNV(uint texture, TextureTarget target, int samples, int internalFormat, int width, int height, int depth, bool fixedSampleLocations)
		{
			Debug.Assert(Delegates.pglTextureImage3DMultisampleNV != null, "pglTextureImage3DMultisampleNV not implemented");
			Delegates.pglTextureImage3DMultisampleNV(texture, (int)target, samples, internalFormat, width, height, depth, fixedSampleLocations);
			LogCommand("glTextureImage3DMultisampleNV", null, texture, target, samples, internalFormat, width, height, depth, fixedSampleLocations			);
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glTextureImage2DMultisampleCoverageNV: Binding for glTextureImage2DMultisampleCoverageNV.
		/// </summary>
		/// <param name="texture">
		/// A <see cref="T:uint"/>.
		/// </param>
		/// <param name="target">
		/// A <see cref="T:TextureTarget"/>.
		/// </param>
		/// <param name="coverageSamples">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="colorSamples">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="internalFormat">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="width">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="height">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="fixedSampleLocations">
		/// A <see cref="T:bool"/>.
		/// </param>
		[RequiredByFeature("GL_NV_texture_multisample")]
		public static void TextureImage2DMultisampleCoverageNV(uint texture, TextureTarget target, int coverageSamples, int colorSamples, int internalFormat, int width, int height, bool fixedSampleLocations)
		{
			Debug.Assert(Delegates.pglTextureImage2DMultisampleCoverageNV != null, "pglTextureImage2DMultisampleCoverageNV not implemented");
			Delegates.pglTextureImage2DMultisampleCoverageNV(texture, (int)target, coverageSamples, colorSamples, internalFormat, width, height, fixedSampleLocations);
			LogCommand("glTextureImage2DMultisampleCoverageNV", null, texture, target, coverageSamples, colorSamples, internalFormat, width, height, fixedSampleLocations			);
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glTextureImage3DMultisampleCoverageNV: Binding for glTextureImage3DMultisampleCoverageNV.
		/// </summary>
		/// <param name="texture">
		/// A <see cref="T:uint"/>.
		/// </param>
		/// <param name="target">
		/// A <see cref="T:TextureTarget"/>.
		/// </param>
		/// <param name="coverageSamples">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="colorSamples">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="internalFormat">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="width">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="height">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="depth">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="fixedSampleLocations">
		/// A <see cref="T:bool"/>.
		/// </param>
		[RequiredByFeature("GL_NV_texture_multisample")]
		public static void TextureImage3DMultisampleCoverageNV(uint texture, TextureTarget target, int coverageSamples, int colorSamples, int internalFormat, int width, int height, int depth, bool fixedSampleLocations)
		{
			Debug.Assert(Delegates.pglTextureImage3DMultisampleCoverageNV != null, "pglTextureImage3DMultisampleCoverageNV not implemented");
			Delegates.pglTextureImage3DMultisampleCoverageNV(texture, (int)target, coverageSamples, colorSamples, internalFormat, width, height, depth, fixedSampleLocations);
			LogCommand("glTextureImage3DMultisampleCoverageNV", null, texture, target, coverageSamples, colorSamples, internalFormat, width, height, depth, fixedSampleLocations			);
			DebugCheckErrors(null);
		}

		internal static unsafe partial class Delegates
		{
			[RequiredByFeature("GL_NV_texture_multisample")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glTexImage2DMultisampleCoverageNV(int target, int coverageSamples, int colorSamples, int internalFormat, int width, int height, [MarshalAs(UnmanagedType.I1)] bool fixedSampleLocations);

			[RequiredByFeature("GL_NV_texture_multisample")]
			[ThreadStatic]
			internal static glTexImage2DMultisampleCoverageNV pglTexImage2DMultisampleCoverageNV;

			[RequiredByFeature("GL_NV_texture_multisample")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glTexImage3DMultisampleCoverageNV(int target, int coverageSamples, int colorSamples, int internalFormat, int width, int height, int depth, [MarshalAs(UnmanagedType.I1)] bool fixedSampleLocations);

			[RequiredByFeature("GL_NV_texture_multisample")]
			[ThreadStatic]
			internal static glTexImage3DMultisampleCoverageNV pglTexImage3DMultisampleCoverageNV;

			[RequiredByFeature("GL_NV_texture_multisample")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glTextureImage2DMultisampleNV(uint texture, int target, int samples, int internalFormat, int width, int height, [MarshalAs(UnmanagedType.I1)] bool fixedSampleLocations);

			[RequiredByFeature("GL_NV_texture_multisample")]
			[ThreadStatic]
			internal static glTextureImage2DMultisampleNV pglTextureImage2DMultisampleNV;

			[RequiredByFeature("GL_NV_texture_multisample")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glTextureImage3DMultisampleNV(uint texture, int target, int samples, int internalFormat, int width, int height, int depth, [MarshalAs(UnmanagedType.I1)] bool fixedSampleLocations);

			[RequiredByFeature("GL_NV_texture_multisample")]
			[ThreadStatic]
			internal static glTextureImage3DMultisampleNV pglTextureImage3DMultisampleNV;

			[RequiredByFeature("GL_NV_texture_multisample")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glTextureImage2DMultisampleCoverageNV(uint texture, int target, int coverageSamples, int colorSamples, int internalFormat, int width, int height, [MarshalAs(UnmanagedType.I1)] bool fixedSampleLocations);

			[RequiredByFeature("GL_NV_texture_multisample")]
			[ThreadStatic]
			internal static glTextureImage2DMultisampleCoverageNV pglTextureImage2DMultisampleCoverageNV;

			[RequiredByFeature("GL_NV_texture_multisample")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glTextureImage3DMultisampleCoverageNV(uint texture, int target, int coverageSamples, int colorSamples, int internalFormat, int width, int height, int depth, [MarshalAs(UnmanagedType.I1)] bool fixedSampleLocations);

			[RequiredByFeature("GL_NV_texture_multisample")]
			[ThreadStatic]
			internal static glTextureImage3DMultisampleCoverageNV pglTextureImage3DMultisampleCoverageNV;

		}
	}

}