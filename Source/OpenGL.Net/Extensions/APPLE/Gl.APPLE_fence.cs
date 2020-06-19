
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
		/// [GL] Value of GL_DRAW_PIXELS_APPLE symbol.
		/// </summary>
		[RequiredByFeature("GL_APPLE_fence")]
		public const int DRAW_PIXELS_APPLE = 0x8A0A;

		/// <summary>
		/// [GL] Value of GL_FENCE_APPLE symbol.
		/// </summary>
		[RequiredByFeature("GL_APPLE_fence")]
		public const int FENCE_APPLE = 0x8A0B;

		/// <summary>
		/// [GL] glGenFencesAPPLE: Binding for glGenFencesAPPLE.
		/// </summary>
		/// <param name="fences">
		/// A <see cref="T:uint[]"/>.
		/// </param>
		[RequiredByFeature("GL_APPLE_fence")]
		public static void GenFencesAPPLE(uint[] fences)
		{
			unsafe {
				fixed (uint* p_fences = fences)
				{
					Debug.Assert(Delegates.pglGenFencesAPPLE != null, "pglGenFencesAPPLE not implemented");
					Delegates.pglGenFencesAPPLE(fences.Length, p_fences);
					LogCommand("glGenFencesAPPLE", null, fences.Length, fences					);
				}
			}
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glGenFencesAPPLE: Binding for glGenFencesAPPLE.
		/// </summary>
		[RequiredByFeature("GL_APPLE_fence")]
		public static uint GenFenceAPPLE()
		{
			uint retValue;
			unsafe {
				Delegates.pglGenFencesAPPLE(1, &retValue);
				LogCommand("glGenFencesAPPLE", null, 1, "{ " + retValue + " }"				);
			}
			DebugCheckErrors(null);
			return (retValue);
		}

		/// <summary>
		/// [GL] glDeleteFencesAPPLE: Binding for glDeleteFencesAPPLE.
		/// </summary>
		/// <param name="fences">
		/// A <see cref="T:uint[]"/>.
		/// </param>
		[RequiredByFeature("GL_APPLE_fence")]
		public static void DeleteFencesAPPLE(params uint[] fences)
		{
			unsafe {
				fixed (uint* p_fences = fences)
				{
					Debug.Assert(Delegates.pglDeleteFencesAPPLE != null, "pglDeleteFencesAPPLE not implemented");
					Delegates.pglDeleteFencesAPPLE(fences.Length, p_fences);
					LogCommand("glDeleteFencesAPPLE", null, fences.Length, fences					);
				}
			}
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glSetFenceAPPLE: Binding for glSetFenceAPPLE.
		/// </summary>
		/// <param name="fence">
		/// A <see cref="T:uint"/>.
		/// </param>
		[RequiredByFeature("GL_APPLE_fence")]
		public static void SetFenceAPPLE(uint fence)
		{
			Debug.Assert(Delegates.pglSetFenceAPPLE != null, "pglSetFenceAPPLE not implemented");
			Delegates.pglSetFenceAPPLE(fence);
			LogCommand("glSetFenceAPPLE", null, fence			);
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glIsFenceAPPLE: Binding for glIsFenceAPPLE.
		/// </summary>
		/// <param name="fence">
		/// A <see cref="T:uint"/>.
		/// </param>
		[RequiredByFeature("GL_APPLE_fence")]
		public static bool IsFenceAPPLE(uint fence)
		{
			bool retValue;

			Debug.Assert(Delegates.pglIsFenceAPPLE != null, "pglIsFenceAPPLE not implemented");
			retValue = Delegates.pglIsFenceAPPLE(fence);
			LogCommand("glIsFenceAPPLE", retValue, fence			);
			DebugCheckErrors(retValue);

			return (retValue);
		}

		/// <summary>
		/// [GL] glTestFenceAPPLE: Binding for glTestFenceAPPLE.
		/// </summary>
		/// <param name="fence">
		/// A <see cref="T:uint"/>.
		/// </param>
		[RequiredByFeature("GL_APPLE_fence")]
		public static bool TestFenceAPPLE(uint fence)
		{
			bool retValue;

			Debug.Assert(Delegates.pglTestFenceAPPLE != null, "pglTestFenceAPPLE not implemented");
			retValue = Delegates.pglTestFenceAPPLE(fence);
			LogCommand("glTestFenceAPPLE", retValue, fence			);
			DebugCheckErrors(retValue);

			return (retValue);
		}

		/// <summary>
		/// [GL] glFinishFenceAPPLE: Binding for glFinishFenceAPPLE.
		/// </summary>
		/// <param name="fence">
		/// A <see cref="T:uint"/>.
		/// </param>
		[RequiredByFeature("GL_APPLE_fence")]
		public static void FinishFenceAPPLE(uint fence)
		{
			Debug.Assert(Delegates.pglFinishFenceAPPLE != null, "pglFinishFenceAPPLE not implemented");
			Delegates.pglFinishFenceAPPLE(fence);
			LogCommand("glFinishFenceAPPLE", null, fence			);
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glTestObjectAPPLE: Binding for glTestObjectAPPLE.
		/// </summary>
		/// <param name="object">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="name">
		/// A <see cref="T:uint"/>.
		/// </param>
		[RequiredByFeature("GL_APPLE_fence")]
		public static bool TestObjectAPPLE(int @object, uint name)
		{
			bool retValue;

			Debug.Assert(Delegates.pglTestObjectAPPLE != null, "pglTestObjectAPPLE not implemented");
			retValue = Delegates.pglTestObjectAPPLE(@object, name);
			LogCommand("glTestObjectAPPLE", retValue, @object, name			);
			DebugCheckErrors(retValue);

			return (retValue);
		}

		/// <summary>
		/// [GL] glFinishObjectAPPLE: Binding for glFinishObjectAPPLE.
		/// </summary>
		/// <param name="object">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="name">
		/// A <see cref="T:int"/>.
		/// </param>
		[RequiredByFeature("GL_APPLE_fence")]
		public static void FinishObjectAPPLE(int @object, int name)
		{
			Debug.Assert(Delegates.pglFinishObjectAPPLE != null, "pglFinishObjectAPPLE not implemented");
			Delegates.pglFinishObjectAPPLE(@object, name);
			LogCommand("glFinishObjectAPPLE", null, @object, name			);
			DebugCheckErrors(null);
		}

		internal static unsafe partial class Delegates
		{
			[RequiredByFeature("GL_APPLE_fence")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glGenFencesAPPLE(int n, uint* fences);

			[RequiredByFeature("GL_APPLE_fence")]
			[ThreadStatic]
			internal static glGenFencesAPPLE pglGenFencesAPPLE;

			[RequiredByFeature("GL_APPLE_fence")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glDeleteFencesAPPLE(int n, uint* fences);

			[RequiredByFeature("GL_APPLE_fence")]
			[ThreadStatic]
			internal static glDeleteFencesAPPLE pglDeleteFencesAPPLE;

			[RequiredByFeature("GL_APPLE_fence")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glSetFenceAPPLE(uint fence);

			[RequiredByFeature("GL_APPLE_fence")]
			[ThreadStatic]
			internal static glSetFenceAPPLE pglSetFenceAPPLE;

			[RequiredByFeature("GL_APPLE_fence")]
			[SuppressUnmanagedCodeSecurity]
			[return: MarshalAs(UnmanagedType.I1)]
			internal delegate bool glIsFenceAPPLE(uint fence);

			[RequiredByFeature("GL_APPLE_fence")]
			[ThreadStatic]
			internal static glIsFenceAPPLE pglIsFenceAPPLE;

			[RequiredByFeature("GL_APPLE_fence")]
			[SuppressUnmanagedCodeSecurity]
			[return: MarshalAs(UnmanagedType.I1)]
			internal delegate bool glTestFenceAPPLE(uint fence);

			[RequiredByFeature("GL_APPLE_fence")]
			[ThreadStatic]
			internal static glTestFenceAPPLE pglTestFenceAPPLE;

			[RequiredByFeature("GL_APPLE_fence")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glFinishFenceAPPLE(uint fence);

			[RequiredByFeature("GL_APPLE_fence")]
			[ThreadStatic]
			internal static glFinishFenceAPPLE pglFinishFenceAPPLE;

			[RequiredByFeature("GL_APPLE_fence")]
			[SuppressUnmanagedCodeSecurity]
			[return: MarshalAs(UnmanagedType.I1)]
			internal delegate bool glTestObjectAPPLE(int @object, uint name);

			[RequiredByFeature("GL_APPLE_fence")]
			[ThreadStatic]
			internal static glTestObjectAPPLE pglTestObjectAPPLE;

			[RequiredByFeature("GL_APPLE_fence")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glFinishObjectAPPLE(int @object, int name);

			[RequiredByFeature("GL_APPLE_fence")]
			[ThreadStatic]
			internal static glFinishObjectAPPLE pglFinishObjectAPPLE;

		}
	}

}