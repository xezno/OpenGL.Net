﻿
// Copyright (C) 2015 Luca Piccioni
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
// USA

#pragma warning disable 618

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace OpenGL
{
	/// <summary>
	/// Modern OpenGL bindings: EGL, Native Platform Interface.
	/// </summary>
	public partial class Egl : KhronosApi
	{
		#region Constructors

		/// <summary>
		/// Static constructor.
		/// </summary>
		static Egl()
		{
			LinkOpenGLProcImports(typeof(Gl), out sImportMap, out sDelegates);
		}

		#endregion

		#region Imports/Delegates Management

		/// <summary>
		/// Synchronize OpenGL delegates.
		/// </summary>
		public static void SyncDelegates()
		{
			SynchDelegates(sImportMap, sDelegates);
		}

		/// <summary>
		/// Default import library.
		/// </summary>
		private const string Library = "opengl32.dll";

		/// <summary>
		/// Imported functions delegates.
		/// </summary>
		private static List<FieldInfo> sDelegates;

		/// <summary>
		/// Build a string->MethodInfo map to speed up extension loading.
		/// </summary>
		internal static SortedList<string, MethodInfo> sImportMap = null;

		#endregion

		#region Debugging

		/// <summary>
		/// Get or set the enable flag for the OpenGL call log.
		/// </summary>
		public static bool CallLogEnabled
		{
			get { return (sCallLogEnabled); }
			set { sCallLogEnabled = value; }
		}

		/// <summary>
		/// OpenGL error checking.
		/// </summary>
		[Conditional("DEBUG")]
		private static void DebugCheckErrors()
		{
			int error = GetError();

			if (error != 0)
				throw new InvalidOperationException(String.Format("EGL error {0}", error));
		}

		/// <summary>
		/// OpenGL logging utility.
		/// </summary>
		/// <param name="format">
		/// A <see cref="String"/> that speficies the format string.
		/// </param>
		/// <param name="args">
		/// A variable arrays of objects used for rendering the <paramref name="format"/>.
		/// </param>
		[Conditional("OPENGL_NET_CALL_LOG_ENABLED")]
		private static void CallLog(string format, params object[] args)
		{
			if (sCallLogEnabled == false)
				return;


		}

		/// <summary>
		/// The enable flag for the OpenGL call log.
		/// </summary>
		private static bool sCallLogEnabled;

		#endregion

		#region Required External Declarations

		/// <summary>
		/// Structure corresponding to EGLClientPixmapHI.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct ClientPixmap
		{
			public IntPtr Data;

			public Int32 Width;

			public Int32 Height;

			public Int32 Stride;
		}

		/// <summary>
		/// Delegate corresponding to EGLSetBlobFuncANDROID.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="keySize"></param>
		/// <param name="value"></param>
		/// <param name="valueSize"></param>
		public delegate void SetBlobFuncDelegate(IntPtr key, UInt32 keySize, IntPtr value, UInt32 valueSize);

		/// <summary>
		/// Delegate corresponding to EGLGetBlobFuncANDROID.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="keySize"></param>
		/// <param name="value"></param>
		/// <param name="valueSize"></param>
		public delegate void GetBlobFuncDelegate(IntPtr key, UInt32 keySize, [Out] IntPtr value, UInt32 valueSize);

		#endregion
	}
}
