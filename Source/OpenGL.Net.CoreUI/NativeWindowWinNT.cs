// Copyright (c) 2017 Luca Piccioni
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

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Local
// ReSharper disable InconsistentNaming

using NetCoreEx.Geometry;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;                // Do not delete me! .NET Core include extension methods
using System.Runtime.InteropServices;
using WinApi.User32;

namespace OpenGL.CoreUI
{
	/// <summary>
	/// NativeWindow implementation for WindowsNT platform.
	/// </summary>
	public class NativeWindowWinNT : NativeWindow
	{
		#region Constructors

		/// <summary>
		/// Create a NativeWindowWinNT.
		/// </summary>
		public NativeWindowWinNT()
		{
			windowsWndProc = WindowsWndProc;
			appHandle = WinApi.Kernel32.Kernel32Methods.GetModuleHandle(typeof(Gl).GetTypeInfo().Assembly.Location);
		}

		#endregion

		#region Platform Resources

		private IntPtr WindowsWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
		{
			switch ((WM)msg) {
				case WM.CREATE:
					return WindowsWndProcCreate(hWnd, wParam, lParam);
				case WM.DESTROY:
					return WindowsWndProcDestroy(hWnd, wParam, lParam);
				case WM.PAINT:
					return WindowsWndProcPaint(hWnd, wParam, lParam);
				case WM.SIZE:
					return WindowsWndProcSize(hWnd, wParam, lParam);

				case WM.SYSCOMMAND:
					break;

				case WM.KEYDOWN:
					return WindowsWndProcKeyDown(hWnd, wParam, lParam);
				case WM.KEYUP:
					return WindowsWndProcKeyUp(hWnd, wParam, lParam);

				case WM.MOUSELEAVE:
					return WindowsWndProcMouseLeave(hWnd, wParam, lParam);
				case WM.MOUSEMOVE:
					return WindowsWndProcMouseMove(hWnd, wParam, lParam);
				case WM.LBUTTONDOWN:
				case WM.RBUTTONDOWN:
				case WM.MBUTTONDOWN:
				case WM.XBUTTONDOWN:
					return WindowsWndProcButtonDown(hWnd, wParam, lParam);
				case WM.LBUTTONUP:
				case WM.RBUTTONUP:
				case WM.MBUTTONUP:
				case WM.XBUTTONUP:
					return WindowsWndProcButtonUp(hWnd, wParam, lParam);
				case WM.LBUTTONDBLCLK:
				case WM.RBUTTONDBLCLK:
				case WM.MBUTTONDBLCLK:
				case WM.XBUTTONDBLCLK:
					return WindowsWndProcButtonDoubleClick(hWnd, wParam, lParam);
				case WM.MOUSEWHEEL:
					return WindowsWndProcMouseWheel(hWnd, wParam, lParam);
				case WM.CLOSE:
					return WindowsWndProcClose(hWnd, wParam, lParam);
			}

			// Callback default window procedure.
			return User32Methods.DefWindowProc(hWnd, msg, wParam, lParam);
		}

		// ReSharper disable UnusedParameter.Local
		
		private IntPtr WindowsWndProcCreate(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
		{
			// Create device context
			CreateDeviceContext(IntPtr.Zero, hWnd);
			// Create OpenGL context
			CreateDesktopContext();
			// The context is made current unconditionally: event handlers allocate resources
			MakeCurrentContext();
			// Event handling
			OnContextCreated();

			return IntPtr.Zero;
		}
		
		private IntPtr WindowsWndProcDestroy(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
		{
			DeleteContext();
			DestroyDeviceContext();

			return IntPtr.Zero;
		}
		
		private IntPtr WindowsWndProcPaint(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
		{
			MakeCurrentContext();
			OnRender();
			OnContextUpdate();
			SwapBuffers();

			// Animation
			if (Animation) {
				if (AnimationTime > 0) {
					throw new NotImplementedException();
				} else {
					// Invalidate continuosly
					Invalidate();
				}
			}

			return IntPtr.Zero;
		}
		
		private IntPtr WindowsWndProcSize(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
		{
			// Notify about client area size changed
			// Note: 
			OnResize();

			return IntPtr.Zero;
		}
		
		private IntPtr WindowsWndProcKeyDown(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
		{
			VirtualKey virtualKeyDown = (VirtualKey)wParam.ToInt32();
			// bool extendedKeyDown = ((lParam.ToInt64() >> 24) & 1) != 0);
			KeyCode key = ToKeyCode(virtualKeyDown);

			if (key == KeyCode.None)
				return IntPtr.Zero;

			OnKeyDown(key);

			return IntPtr.Zero;
		}
		
		private IntPtr WindowsWndProcKeyUp(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
		{
			VirtualKey virtualKeyUp = (VirtualKey)wParam.ToInt32();
			// bool extendedKeyUp = ((lParam.ToInt64() >> 24) & 1) != 0);
			KeyCode key = ToKeyCode(virtualKeyUp);

			if (key == KeyCode.None)
				return IntPtr.Zero;

			OnKeyUp(key);

			return IntPtr.Zero;
		}
		
		private IntPtr WindowsWndProcMouseLeave(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
		{
			// Next mouse event can track again mouse leave
			trackingMouseLeave = false;

			OnMouseLeave();

			return IntPtr.Zero;
		}

		/// <summary>
		/// Flag indicating whether the system is tracking the mouse leave.
		/// </summary>
		private bool trackingMouseLeave;

		private IntPtr WindowsWndProcMouseMove(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
		{
			Point mouseLocation = WindowsWndProc_GetMouseLocation(lParam);
			MouseButton mouseButton = WindowsWndProc_GetMouseButtons(wParam);

			if (!trackingMouseLeave) {
				// Emulates 'WM_MOUSEENTER'
				OnMouseEnter(mouseLocation, mouseButton);
                // Keep tracking WM_MOUSELEAVE
                TrackMouseEventOptions tme = new TrackMouseEventOptions
                {
                    Flags = TrackMouseEventFlags.TME_LEAVE,
                    TrackedHwnd = hWnd,
                    Size = (uint)Marshal.SizeOf(typeof(TrackMouseEventOptions)),
                    HoverTime = TrackMouseEventOptions.DefaultHoverTime
                };
                trackingMouseLeave = User32Methods.TrackMouseEvent(ref tme);
				Debug.Assert(trackingMouseLeave, new Win32Exception(Marshal.GetLastWin32Error()).Message);
			}

			// Note: WM_MOUSEMOVE is execute just after 'WM_MOUSEENTER'
			OnMouseMove(mouseLocation, mouseButton);

			return IntPtr.Zero;
		}

		private IntPtr WindowsWndProcButtonDown(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
		{
			OnMouseDown(WindowsWndProc_GetMouseLocation(lParam), WindowsWndProc_GetMouseButtons(wParam));

			return IntPtr.Zero;
		}

		private IntPtr WindowsWndProcButtonUp(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
		{
			OnMouseUp(WindowsWndProc_GetMouseLocation(lParam), WindowsWndProc_GetMouseButtons(wParam));

			return IntPtr.Zero;
		}

		private IntPtr WindowsWndProcButtonDoubleClick(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
		{
			OnMouseDoubleClick(WindowsWndProc_GetMouseLocation(lParam), WindowsWndProc_GetMouseButtons(wParam));

			return IntPtr.Zero;
		}
		
		private IntPtr WindowsWndProcMouseWheel(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
		{
            short wheelTicks = (short)(WindowsWndProc_GetWheelDelta(wParam) / /* WHEEL_DELTA */ 120);

            OnMouseWheel(WindowsWndProc_GetMouseLocation(lParam), WindowsWndProc_GetMouseButtons(wParam), wheelTicks);

			return IntPtr.Zero;
		}

		private short WindowsWndProc_GetWheelDelta(IntPtr wParam)
        {
			return ((short)((int)wParam>>16));
        }

		private IntPtr WindowsWndProcClose(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
		{
			// Notify about client closing
			OnClose();

			return IntPtr.Zero;
		}

		// ReSharper restore UnusedParameter.Local

		private Point WindowsWndProc_GetMouseLocation(IntPtr lParam)
		{
			int x = lParam.ToInt32() & 0xFFFF;
			int y = (lParam.ToInt32() >> 16) & 0xFFFF;

			return new Point(x, y);
		}

		private static MouseButton WindowsWndProc_GetMouseButtons(IntPtr wParam)
		{
			MouseButton buttons = MouseButton.None;
			int wParamValue = wParam.ToInt32() & 0xFFFF;

			if ((wParamValue & 0x0001) != 0)
				buttons |= MouseButton.Left;
			if ((wParamValue & 0x0002) != 0)
				buttons |= MouseButton.Right;
			if ((wParamValue & 0x0010) != 0)
				buttons |= MouseButton.Middle;
			if ((wParamValue & 0x0020) != 0)
				buttons |= MouseButton.X1;
			if ((wParamValue & 0x0040) != 0)
				buttons |= MouseButton.X2;

			return buttons;
		}

		/// <summary>
		/// Application instance handle.
		/// </summary>
		private readonly IntPtr appHandle;

		/// <summary>
		/// The native window handle.
		/// </summary>
		private IntPtr windowHandle;

		/// <summary>
		/// Windows procedure.
		/// </summary>
		private readonly WindowProc windowsWndProc;

		#endregion

		#region P/Invoke

		// ReSharper disable FieldCanBeMadeReadOnly.Local
		// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

		private static NativeWindowStyles ToNativeWindowStyle(WindowStyles styles)
		{
			NativeWindowStyles windowStyleses = NativeWindowStyles.None;

			if ((styles & WindowStyles.WS_BORDER) != 0)
				windowStyleses |= NativeWindowStyles.Border;
			if ((styles & WindowStyles.WS_CAPTION) != 0)
				windowStyleses |= NativeWindowStyles.Caption;
			if ((styles & WindowStyles.WS_THICKFRAME) != 0)
				windowStyleses |= NativeWindowStyles.Resizeable;

			return windowStyleses;
		}

		private static WindowStyles FromNativeWindowStyle(NativeWindowStyles styleses)
		{
			WindowStyles windowStyles = 0;

			if ((styleses & NativeWindowStyles.Border) != 0)
				windowStyles |= WindowStyles.WS_BORDER;
			if ((styleses & NativeWindowStyles.Caption) == NativeWindowStyles.Caption)
				windowStyles |= WindowStyles.WS_CAPTION | WindowStyles.WS_BORDER;
			if ((styleses & NativeWindowStyles.Resizeable) == NativeWindowStyles.Resizeable)
				windowStyles |= WindowStyles.WS_THICKFRAME;

			return windowStyles;
		}

		/// <summary>
		/// Convert a <see cref="VirtualKey"/> to the corresponding <see cref="KeyCode"/>.
		/// </summary>
		/// <param name="key">
		/// The <see cref="VirtualKey"/> to be converted.
		/// </param>
		/// <returns>
		/// The <see cref="KeyCode"/> corresponding to <paramref name="key"/>. In case the
		/// conversion is not possible, returns <see cref="KeyCode.None"/>.
		/// </returns>
		private static KeyCode ToKeyCode(VirtualKey key)
		{
			switch (key)
            {
                case VirtualKey.BACK:
                    return KeyCode.Back;
                case VirtualKey.TAB:
					return KeyCode.Tab;

				case VirtualKey.RETURN:
					return KeyCode.Return;
				case VirtualKey.SHIFT:
					return KeyCode.Shift;
				case VirtualKey.CONTROL:
					return KeyCode.Control;
				case VirtualKey.MENU:
					return KeyCode.Menu;
				case VirtualKey.CAPITAL:
					return KeyCode.CapsLock;
				case VirtualKey.ESCAPE:
					return KeyCode.Escape;

				case VirtualKey.SPACE:
					return KeyCode.Space;

				case VirtualKey.END:
					return KeyCode.End;
				case VirtualKey.HOME:
					return KeyCode.Home;

				case VirtualKey.LEFT:
					return KeyCode.Left;
				case VirtualKey.UP:
					return KeyCode.Up;
				case VirtualKey.RIGHT:
					return KeyCode.Right;
				case VirtualKey.DOWN:
					return KeyCode.Down;

                case VirtualKey.INSERT:
					return KeyCode.Insert;
				case VirtualKey.DELETE:
					return KeyCode.Delete;

				case VirtualKey.D0:
					return KeyCode.N0;
				case VirtualKey.D1:
					return KeyCode.N1;
				case VirtualKey.D2:
					return KeyCode.N2;
				case VirtualKey.D3:
					return KeyCode.N3;
				case VirtualKey.D4:
					return KeyCode.N4;
				case VirtualKey.D5:
					return KeyCode.N5;
				case VirtualKey.D6:
					return KeyCode.N6;
				case VirtualKey.D7:
					return KeyCode.N7;
				case VirtualKey.D8:
					return KeyCode.N8;
				case VirtualKey.D9:
					return KeyCode.N9;

				case VirtualKey.A:
					return KeyCode.A;
				case VirtualKey.B:
					return KeyCode.B;
				case VirtualKey.C:
					return KeyCode.C;
				case VirtualKey.D:
					return KeyCode.D;
				case VirtualKey.E:
					return KeyCode.E;
				case VirtualKey.F:
					return KeyCode.F;
				case VirtualKey.G:
					return KeyCode.G;
				case VirtualKey.H:
					return KeyCode.H;
				case VirtualKey.I:
					return KeyCode.I;
				case VirtualKey.J:
					return KeyCode.J;
				case VirtualKey.K:
					return KeyCode.K;
				case VirtualKey.L:
					return KeyCode.L;
				case VirtualKey.M:
					return KeyCode.M;
				case VirtualKey.N:
					return KeyCode.N;
				case VirtualKey.O:
					return KeyCode.O;
				case VirtualKey.P:
					return KeyCode.P;
				case VirtualKey.Q:
					return KeyCode.Q;
				case VirtualKey.R:
					return KeyCode.R;
				case VirtualKey.S:
					return KeyCode.S;
				case VirtualKey.T:
					return KeyCode.T;
				case VirtualKey.U:
					return KeyCode.U;
				case VirtualKey.V:
					return KeyCode.V;
				case VirtualKey.W:
					return KeyCode.W;
				case VirtualKey.X:
					return KeyCode.X;
				case VirtualKey.Y:
					return KeyCode.Y;
				case VirtualKey.Z:
					return KeyCode.Z;

				case VirtualKey.LWIN:
					return KeyCode.LeftWindows;
				case VirtualKey.RWIN:
					return KeyCode.RightWindows;
				case VirtualKey.APPS:
					return KeyCode.Application;

				case VirtualKey.NUMPAD0:
					return KeyCode.Numpad0;
				case VirtualKey.NUMPAD1:
					return KeyCode.Numpad1;
				case VirtualKey.NUMPAD2:
					return KeyCode.Numpad2;
				case VirtualKey.NUMPAD3:
					return KeyCode.Numpad3;
				case VirtualKey.NUMPAD4:
					return KeyCode.Numpad4;
				case VirtualKey.NUMPAD5:
					return KeyCode.Numpad5;
				case VirtualKey.NUMPAD6:
					return KeyCode.Numpad6;
				case VirtualKey.NUMPAD7:
					return KeyCode.Numpad7;
				case VirtualKey.NUMPAD8:
					return KeyCode.Numpad8;
				case VirtualKey.NUMPAD9:
					return KeyCode.Numpad9;
				case VirtualKey.MULTIPLY:
					return KeyCode.Multiply;
				case VirtualKey.ADD:
					return KeyCode.Add;
				case VirtualKey.SEPARATOR:
					return KeyCode.Separator;
				case VirtualKey.SUBTRACT:
					return KeyCode.Subtract;
				case VirtualKey.DECIMAL:
					return KeyCode.Decimal;
				case VirtualKey.DIVIDE:
					return KeyCode.Divide;
				case VirtualKey.F1:
					return KeyCode.F1;
				case VirtualKey.F2:
					return KeyCode.F2;
				case VirtualKey.F3:
					return KeyCode.F3;
				case VirtualKey.F4:
					return KeyCode.F4;
				case VirtualKey.F5:
					return KeyCode.F5;
				case VirtualKey.F6:
					return KeyCode.F6;
				case VirtualKey.F7:
					return KeyCode.F7;
				case VirtualKey.F8:
					return KeyCode.F8;
				case VirtualKey.F9:
					return KeyCode.F9;
				case VirtualKey.F10:
					return KeyCode.F10;
				case VirtualKey.F11:
					return KeyCode.F11;
				case VirtualKey.F12:
					return KeyCode.F12;
				case VirtualKey.F13:
					return KeyCode.F13;
				case VirtualKey.F14:
					return KeyCode.F14;
				case VirtualKey.F15:
					return KeyCode.F15;
				case VirtualKey.F16:
					return KeyCode.F16;
				case VirtualKey.F17:
					return KeyCode.F17;
				case VirtualKey.F18:
					return KeyCode.F18;
				case VirtualKey.F19:
					return KeyCode.F19;
				case VirtualKey.F20:
					return KeyCode.F20;
				case VirtualKey.F21:
					return KeyCode.F21;
				case VirtualKey.F22:
					return KeyCode.F22;
				case VirtualKey.F23:
					return KeyCode.F23;
				case VirtualKey.F24:
					return KeyCode.F24;
				case VirtualKey.NUMLOCK:
					return KeyCode.NumLock;
				case VirtualKey.SCROLL:
					return KeyCode.ScrollLock;
                case VirtualKey.OEM_PLUS:
                    return KeyCode.Plus;
                case VirtualKey.OEM_COMMA:
                    return KeyCode.Comma;
                case VirtualKey.OEM_MINUS:
                    return KeyCode.Minus;
                case VirtualKey.OEM_PERIOD:
                    return KeyCode.Period;

                case VirtualKey.OEM_1:
                    return KeyCode.OEM1;
                case VirtualKey.OEM_2:
					return KeyCode.OEM2;
				case VirtualKey.OEM_3:
					return KeyCode.OEM3;

                default:
					break;
			}
            return KeyCode.None;
		}

		private static VirtualKey ToVirtualKey(KeyCode key)
		{
			switch (key) {
				case KeyCode.Tab:
					return VirtualKey.TAB;

				case KeyCode.Return:
					return VirtualKey.RETURN;
				case KeyCode.Shift:
					return VirtualKey.SHIFT;
				case KeyCode.Control:
					return VirtualKey.CONTROL;
				case KeyCode.Menu:
					return VirtualKey.MENU;
				case KeyCode.CapsLock:
					return VirtualKey.CAPITAL;
				case KeyCode.Escape:
					return VirtualKey.ESCAPE;

				case KeyCode.Space:
					return VirtualKey.SPACE;

				case KeyCode.End:
					return VirtualKey.END;
				case KeyCode.Home:
					return VirtualKey.HOME;

				case KeyCode.Left:
					return VirtualKey.LEFT;
				case KeyCode.Up:
					return VirtualKey.UP;
				case KeyCode.Right:
					return VirtualKey.RIGHT;
				case KeyCode.Down:
					return VirtualKey.DOWN;

				case KeyCode.Insert:
					return VirtualKey.INSERT;
				case KeyCode.Delete:
					return VirtualKey.DELETE;

				case KeyCode.N0:
					return VirtualKey.D0;
				case KeyCode.N1:
					return VirtualKey.D1;
				case KeyCode.N2:
					return VirtualKey.D2;
				case KeyCode.N3:
					return VirtualKey.D3;
				case KeyCode.N4:
					return VirtualKey.D4;
				case KeyCode.N5:
					return VirtualKey.D5;
				case KeyCode.N6:
					return VirtualKey.D6;
				case KeyCode.N7:
					return VirtualKey.D7;
				case KeyCode.N8:
					return VirtualKey.D8;
				case KeyCode.N9:
					return VirtualKey.D9;

				case KeyCode.A:
					return VirtualKey.A;
				case KeyCode.B:
					return VirtualKey.B;
				case KeyCode.C:
					return VirtualKey.C;
				case KeyCode.D:
					return VirtualKey.D;
				case KeyCode.E:
					return VirtualKey.E;
				case KeyCode.F:
					return VirtualKey.F;
				case KeyCode.G:
					return VirtualKey.G;
				case KeyCode.H:
					return VirtualKey.H;
				case KeyCode.I:
					return VirtualKey.I;
				case KeyCode.J:
					return VirtualKey.J;
				case KeyCode.K:
					return VirtualKey.K;
				case KeyCode.L:
					return VirtualKey.L;
				case KeyCode.M:
					return VirtualKey.M;
				case KeyCode.N:
					return VirtualKey.N;
				case KeyCode.O:
					return VirtualKey.O;
				case KeyCode.P:
					return VirtualKey.P;
				case KeyCode.Q:
					return VirtualKey.Q;
				case KeyCode.R:
					return VirtualKey.R;
				case KeyCode.S:
					return VirtualKey.S;
				case KeyCode.T:
					return VirtualKey.T;
				case KeyCode.U:
					return VirtualKey.U;
				case KeyCode.V:
					return VirtualKey.V;
				case KeyCode.W:
					return VirtualKey.W;
				case KeyCode.X:
					return VirtualKey.X;
				case KeyCode.Y:
					return VirtualKey.Y;
				case KeyCode.Z:
					return VirtualKey.Z;

				case KeyCode.LeftWindows:
					return VirtualKey.LWIN;
				case KeyCode.RightWindows:
					return VirtualKey.RWIN;
				case KeyCode.Application:
					return VirtualKey.APPS;

				case KeyCode.Numpad0:
					return VirtualKey.NUMPAD0;
				case KeyCode.Numpad1:
					return VirtualKey.NUMPAD1;
				case KeyCode.Numpad2:
					return VirtualKey.NUMPAD2;
				case KeyCode.Numpad3:
					return VirtualKey.NUMPAD3;
				case KeyCode.Numpad4:
					return VirtualKey.NUMPAD4;
				case KeyCode.Numpad5:
					return VirtualKey.NUMPAD5;
				case KeyCode.Numpad6:
					return VirtualKey.NUMPAD6;
				case KeyCode.Numpad7:
					return VirtualKey.NUMPAD7;
				case KeyCode.Numpad8:
					return VirtualKey.NUMPAD8;
				case KeyCode.Numpad9:
					return VirtualKey.NUMPAD9;
				case KeyCode.Multiply:
					return VirtualKey.MULTIPLY;
				case KeyCode.Add:
					return VirtualKey.ADD;
				case KeyCode.Separator:
					return VirtualKey.SEPARATOR;
				case KeyCode.Subtract:
					return VirtualKey.SUBTRACT;
				case KeyCode.Decimal:
					return VirtualKey.DECIMAL;
				case KeyCode.Divide:
					return VirtualKey.DIVIDE;
				case KeyCode.F1:
					return VirtualKey.F1;
				case KeyCode.F2:
					return VirtualKey.F2;
				case KeyCode.F3:
					return VirtualKey.F3;
				case KeyCode.F4:
					return VirtualKey.F4;
				case KeyCode.F5:
					return VirtualKey.F5;
				case KeyCode.F6:
					return VirtualKey.F6;
				case KeyCode.F7:
					return VirtualKey.F7;
				case KeyCode.F8:
					return VirtualKey.F8;
				case KeyCode.F9:
					return VirtualKey.F9;
				case KeyCode.F10:
					return VirtualKey.F10;
				case KeyCode.F11:
					return VirtualKey.F11;
				case KeyCode.F12:
					return VirtualKey.F12;
				case KeyCode.F13:
					return VirtualKey.F13;
				case KeyCode.F14:
					return VirtualKey.F14;
				case KeyCode.F15:
					return VirtualKey.F15;
				case KeyCode.F16:
					return VirtualKey.F16;
				case KeyCode.F17:
					return VirtualKey.F17;
				case KeyCode.F18:
					return VirtualKey.F18;
				case KeyCode.F19:
					return VirtualKey.F19;
				case KeyCode.F20:
					return VirtualKey.F20;
				case KeyCode.F21:
					return VirtualKey.F21;
				case KeyCode.F22:
					return VirtualKey.F22;
				case KeyCode.F23:
					return VirtualKey.F23;
				case KeyCode.F24:
					return VirtualKey.F24;
				case KeyCode.NumLock:
					return VirtualKey.NUMLOCK;
				case KeyCode.ScrollLock:
					return VirtualKey.SCROLL;

				default:
					throw new NotSupportedException("Unsupported key code " + key);
			}
		}

		#endregion

		#region Multithreading

		/// <summary>
		/// The ID of the thread that has created the window.
		/// </summary>
		private uint ownerThread;

		/// <summary>
		/// Get the current thread ID.
		/// </summary>
		/// <returns></returns>
		[DllImport("kernel32.dll")]
		private static extern uint GetCurrentThreadId();

		#endregion

		#region Error Checking

		private void CheckHandle()
		{
			if (windowHandle == IntPtr.Zero)
				throw new InvalidOperationException("no handle");
		}

		private void CheckNotFullscreen()
		{
			if (Fullscreen)
				throw new InvalidOperationException("fullscreen");
		}

		private void CheckThread()
		{
			if (GetCurrentThreadId() != ownerThread)
				throw new InvalidOperationException("cross-thread operation not allowed");
		}

		#endregion

		#region NativeWindow Overrides

		/// <summary>
		/// Get the display handle associated this instance.
		/// </summary>
		public override IntPtr Display => IntPtr.Zero;

        /// <summary>
		/// Get the native window handle.
		/// </summary>
		public override IntPtr WindowHandle => windowHandle;

        /// <summary>
		/// Create the NativeWindow.
		/// </summary>
		/// <param name="x">
		/// A <see cref="Int32"/> that specifies the X coordinate of the window, in pixels.
		/// </param>
		/// <param name="y">
		/// A <see cref="Int32"/> that specifies the Y coordinate of the window, in pixels.
		/// </param>
		/// <param name="width">
		/// A <see cref="UInt32"/> that specifies the window width, in pixels.
		/// </param>
		/// <param name="height">
		/// A <see cref="UInt32"/> that specifies the window height, in pixels.
		/// </param>
		/// <param name="styles">
		/// The initial <see cref="NativeWindowStyles"/> of the window.
		/// </param>
		public override void Create(int x, int y, uint width, uint height, NativeWindowStyles styles)
		{
			if (windowHandle != IntPtr.Zero)
				throw new InvalidOperationException("already created");

			// Register window class
			WindowClassEx windowClass = new WindowClassEx();
			const string defaultWindowClass = "OpenGL.CoreUI2";

			windowClass.Size = (uint)Marshal.SizeOf(typeof(WindowClassEx));
			windowClass.Styles = WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW | WindowClassStyles.CS_OWNDC;
			windowClass.WindowProc = windowsWndProc;
			windowClass.InstanceHandle = appHandle;
			windowClass.ClassName = defaultWindowClass;
            windowClass.CursorHandle = User32Methods.LoadCursor(IntPtr.Zero, (IntPtr)SystemCursor.IDC_ARROW);

			if (User32Methods.RegisterClassEx(ref windowClass) == 0)
				throw new Win32Exception(Marshal.GetLastWin32Error());

			className = defaultWindowClass;

			try {
				WindowStyles windowStyle = FromNativeWindowStyle(styles);

				// Note: window size is meant as client area, but CreateWindowEx width/height specifies the external
				// frame size: compute offset for frame borders
				Rectangle clientSize = GetClientToFrameRect(x, y, width, height, windowStyle);
				// Create window
				windowHandle = User32Methods.CreateWindowEx(
					0, windowClass.ClassName, string.Empty, windowStyle,
					clientSize.Left, clientSize.Top, clientSize.Right - clientSize.Left, clientSize.Bottom - clientSize.Top,
					IntPtr.Zero, IntPtr.Zero, windowClass.InstanceHandle, IntPtr.Zero
				);

				if (windowHandle == IntPtr.Zero)
					throw new Win32Exception(Marshal.GetLastWin32Error());

				ownerThread = GetCurrentThreadId();
			} catch {
				Dispose();
				throw;
			}

			// UnsafeNativeMethods.EnableMenuItem(UnsafeNativeMethods.GetSystemMenu(_Handle, FALSE), SC_CLOSE, MF_BYCOMMAND | MF_ENABLED);
			
		}

		/// <summary>
		/// Closes the NativeWindow.
		/// </summary>
		public override void Destroy()
		{
			CheckHandle();

			User32Methods.PostMessage(windowHandle, (uint)WM.DESTROY, IntPtr.Zero, IntPtr.Zero);

			Stop();
		}

		/// <summary>
		/// Run the event loop for this NativeWindow.
		/// </summary>
		public override void Run()
		{
			if (loopRunning)
				throw new InvalidOperationException("Loop running");

			loopRunning = true;

			try {
				Message msg;
				int ret;
				
				while ((ret = User32Methods.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0) {
					if (ret < 0)
						throw new Win32Exception(Marshal.GetLastWin32Error());

					switch (msg.Value) {
						case (int)(WM.USER + 13):
							// Terminate message loop
							return;
						default:
							User32Methods.TranslateMessage(ref msg);
							User32Methods.DispatchMessage(ref msg);
							break;
					}
				}
			} finally {
				loopRunning = false;
			}
		}

		/// <summary>
		/// Stops the event loop running for this NativeWindow.
		/// </summary>
		public override void Stop()
		{
			if (!loopRunning)
				throw new InvalidOperationException("loop not running");

			User32Methods.PostMessage(windowHandle, (uint)WM.USER + 13, IntPtr.Zero, IntPtr.Zero);
		}

		/// <summary>
		/// The state of the message loop.
		/// </summary>
		private bool loopRunning;

		/// <summary>
		/// Get or set the NativeWindow location.
		/// </summary>
		public override Point Location
		{
			get {
				CheckHandle();

				Rectangle windowRect;

				if (!User32Methods.GetWindowRect(windowHandle, out windowRect))
					throw new Win32Exception(Marshal.GetLastWin32Error());

				return new Point(windowRect.Left, windowRect.Top);
			}
			set {
				CheckHandle();
				CheckThread();
				CheckNotFullscreen();

				const WindowPositionFlags windowPosFlags =
					WindowPositionFlags.SWP_NOSIZE | WindowPositionFlags.SWP_NOZORDER |
					WindowPositionFlags.SWP_NOACTIVATE |
					WindowPositionFlags.SWP_FRAMECHANGED;
				
				if (!User32Methods.SetWindowPos(windowHandle, IntPtr.Zero, value.X, value.Y, 0, 0, windowPosFlags))
					throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		/// <summary>
		/// Get or set the NativeWindow client area size.
		/// </summary>
		public override NetCoreEx.Geometry.Size ClientSize
		{
			get 
			{
				CheckHandle();
				CheckThread();

				Rectangle clientSize;

				if (!User32Methods.GetClientRect(windowHandle, out clientSize))
					throw new Win32Exception(Marshal.GetLastWin32Error());

				return clientSize.Size;
			}
			set 
			{
				CheckHandle();
				CheckThread();
				CheckNotFullscreen();

				const WindowPositionFlags windowPosFlags =
					WindowPositionFlags.SWP_NOMOVE | WindowPositionFlags.SWP_NOZORDER |
					WindowPositionFlags.SWP_NOACTIVATE |
					WindowPositionFlags.SWP_FRAMECHANGED;

				NetCoreEx.Geometry.Size frameSize = GetClientToFrameRect(0, 0, (uint)value.Width, (uint)value.Height).Size;

				if (!User32Methods.SetWindowPos(windowHandle, IntPtr.Zero, 0, 0, frameSize.Width, frameSize.Height, windowPosFlags))
					throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		private static Rectangle GetClientToFrameRect(int x, int y, uint width, uint height, WindowStyles windowStyles)
		{
			Rectangle clientSize = new Rectangle() {
				Left = x,
				Right = x + (int) width,
				Top = y,
				Bottom = y + (int) height
			};

			if ((windowStyles & WindowStyles.WS_THICKFRAME) != 0) {
				int cxSizeFrame = User32Methods.GetSystemMetrics(SystemMetrics.SM_CXSIZEFRAME) * 2;
				int cySizeFrame = User32Methods.GetSystemMetrics(SystemMetrics.SM_CYSIZEFRAME) * 2;

				clientSize.Left -= cxSizeFrame;
				clientSize.Right += cxSizeFrame;
				clientSize.Top -= cySizeFrame;
				clientSize.Bottom += cySizeFrame;
			}

			if ((windowStyles & WindowStyles.WS_CAPTION) != 0) {
				int cySizeCaption = User32Methods.GetSystemMetrics(SystemMetrics.SM_CYCAPTION);

				clientSize.Bottom += cySizeCaption;
			}

			return clientSize;
		}

		private Rectangle GetClientToFrameRect(int x, int y, uint width, uint height)
		{
			WindowStyles windowStyle = (WindowStyles)User32Methods.GetWindowLongPtr(windowHandle, (int)WindowLongFlags.GWL_STYLE);
			WindowStyles windowStyleEx = (WindowStyles)User32Methods.GetWindowLongPtr(windowHandle, (int)WindowLongFlags.GWL_EXSTYLE);

			return GetClientToFrameRect(x, y, width, height, windowStyle | windowStyleEx);
		}

		/// <summary>
		/// Show the native window.
		/// </summary>
		public override void Show()
		{
			CheckHandle();
			CheckThread();

			User32Methods.ShowWindow(windowHandle, ShowWindowCommands.SW_SHOW);
			Invalidate();
		}

		/// <summary>
		/// Hide the native window.
		/// </summary>
		public override void Hide()
		{
			CheckHandle();
			CheckThread();

			User32Methods.ShowWindow(windowHandle, ShowWindowCommands.SW_HIDE);
		}

		/// <summary>
		/// Get the implemented window styles by the underlying implementation.
		/// </summary>
		public override NativeWindowStyles SupportedStyleses => NativeWindowStyles.Border | NativeWindowStyles.Caption | NativeWindowStyles.Resizeable;

        /// <summary>
		/// The styles of this NativeWindow.
		/// </summary>
		public override NativeWindowStyles Styleses
		{
			get
			{
				CheckHandle();
				CheckThread();

				WindowStyles win32Style = (WindowStyles)User32Methods.GetWindowLongPtr(windowHandle, (int)WindowLongFlags.GWL_STYLE);

				return ToNativeWindowStyle(win32Style);
			}
			set
			{
				CheckHandle();
				CheckNotFullscreen();
				CheckThread();

				NativeWindowStyles styleses = value;

				// No border? No caption!
				if ((styleses & NativeWindowStyles.Border) == 0)
					styleses &= (NativeWindowStyles)~(0x0001 | 0x0004);

				WindowStyles supportedStyles = FromNativeWindowStyle(SupportedStyleses);
				WindowStyles win32Style = (WindowStyles)User32Methods.GetWindowLongPtr(windowHandle, (int)WindowLongFlags.GWL_STYLE) & ~supportedStyles;
				WindowStyles win32StyleValue = FromNativeWindowStyle(styleses);

				User32Methods.SetWindowLongPtr(windowHandle, (int)WindowLongFlags.GWL_STYLE, (IntPtr)(win32Style | win32StyleValue));
				User32Methods.SetWindowPos(windowHandle, IntPtr.Zero, 0, 0, 0, 0, WindowPositionFlags.SWP_FRAMECHANGED | WindowPositionFlags.SWP_NOACTIVATE | WindowPositionFlags.SWP_NOZORDER | WindowPositionFlags.SWP_NOMOVE | WindowPositionFlags.SWP_NOSIZE);
			}
		}

		/// <summary>
		/// Get or set the NativeWindow fullscreen state.
		/// </summary>
		public override bool Fullscreen
		{
			get
			{
				// Not required, but keep API behavior
				CheckHandle();
				CheckThread();

				return fullscreen;
			}
			set {
				CheckHandle();
				CheckThread();

				if (fullscreen == value)
					return;

				if (value) {
					// Get monitor size
					MonitorInfo monitorInfo = new MonitorInfo();
					IntPtr monitor = User32Methods.MonitorFromWindow(windowHandle, MonitorFlag.MONITOR_DEFAULTTONEAREST);

					if (!User32Methods.GetMonitorInfo(monitor, ref monitorInfo))
                    {
						// Failed to get the closest monitor to the window - just try getting the default window instead
						monitor = User32Methods.MonitorFromPoint(new NetCoreEx.Geometry.Point(0, 0), MonitorFlag.MONITOR_DEFAULTTOPRIMARY);
						if (User32Methods.GetMonitorInfo(monitor, ref monitorInfo) == false)
                        {
							// Nope, couldn't get any monitors.
							throw new InvalidOperationException("Unable to get monitor info");
                        }
                    }

					// Store current location and size
					fullscreenRestoreLocation = Location;
					fullscreenRestoreSize = ClientSize;
					// Store current styles
					fullscreenRestoreStyle = (WindowStyles)User32Methods.SetWindowLongPtr(windowHandle, (int)WindowLongFlags.GWL_STYLE, IntPtr.Zero);
					fullscreenRestoreStyleEx = (WindowStyles)User32Methods.SetWindowLongPtr(windowHandle, (int)WindowLongFlags.GWL_EXSTYLE, IntPtr.Zero);

					// Set window styles
					WindowStyles fullscreenStyle = fullscreenRestoreStyle & ~(WindowStyles.WS_CAPTION | WindowStyles.WS_THICKFRAME);

					User32Methods.SetWindowLongPtr(windowHandle, (int)WindowLongFlags.GWL_STYLE, (IntPtr)fullscreenStyle);

					// Set position and size
					Point location = new Point(monitorInfo.WorkRect.Left, monitorInfo.WorkRect.Top);
					NetCoreEx.Geometry.Size size = monitorInfo.MonitorRect.Size;
					const WindowPositionFlags windowPosFlags = WindowPositionFlags.SWP_NOZORDER | WindowPositionFlags.SWP_NOACTIVATE | WindowPositionFlags.SWP_FRAMECHANGED;

					if (!User32Methods.SetWindowPos(windowHandle, IntPtr.Zero, location.X, location.Y, size.Width, size.Height, windowPosFlags))
						throw new InvalidOperationException("unable to set client size");
						
				} else {
					// Restore previous styles
					User32Methods.SetWindowLongPtr(windowHandle, (int)WindowLongFlags.GWL_STYLE, (IntPtr)fullscreenRestoreStyle);
					User32Methods.SetWindowLongPtr(windowHandle, (int)WindowLongFlags.GWL_EXSTYLE, (IntPtr)fullscreenRestoreStyleEx);

					// Restore previous position and size
					Point location = fullscreenRestoreLocation;
					NetCoreEx.Geometry.Size size = fullscreenRestoreSize;
					const WindowPositionFlags windowPosFlags = WindowPositionFlags.SWP_NOZORDER | WindowPositionFlags.SWP_NOACTIVATE | WindowPositionFlags.SWP_FRAMECHANGED;

					if (!User32Methods.SetWindowPos(windowHandle, IntPtr.Zero, location.X, location.Y, size.Width, size.Height, windowPosFlags))
						throw new InvalidOperationException("unable to set client size");
				}

				fullscreen = value;
			}
		}

		/// <summary>
		/// The NativeWindow fullscreen state.
		/// </summary>
		private bool fullscreen;

		/// <summary>
		/// 
		/// </summary>
		private Point fullscreenRestoreLocation;

		/// <summary>
		/// 
		/// </summary>
		private NetCoreEx.Geometry.Size fullscreenRestoreSize;

		/// <summary>
		/// 
		/// </summary>
		private WindowStyles fullscreenRestoreStyle;

		/// <summary>
		/// 
		/// </summary>
		private WindowStyles fullscreenRestoreStyleEx;

		/// <summary>
		/// The window's class name.
		/// </summary>
		private string className;

		/// <summary>
		/// Invalidate the window.
		/// </summary>
		public override void Invalidate()
		{
			CheckHandle();
			CheckThread();

			User32Methods.InvalidateRect(windowHandle, IntPtr.Zero, true);
			User32Methods.UpdateWindow(windowHandle);
		}

		/// <summary>
		/// Emulates the key pressed event.
		/// </summary>
		/// <param name="key">
		/// The <see cref="KeyCode"/> indicating what key is emulated.
		/// </param>
		/// <remarks>
		/// This method is mainly used for testing, but it may be useful for some application.
		/// </remarks>
		public override void EmulatesKeyPress(KeyCode key)
		{
			CheckHandle();

			// Determine arguments
			VirtualKey virtualKey = ToVirtualKey(key);

			// Event sequence
			User32Methods.SendMessage(windowHandle, (uint)WM.KEYDOWN, new IntPtr((uint)virtualKey), IntPtr.Zero);
			User32Methods.SendMessage(windowHandle, (uint)WM.KEYUP,   new IntPtr((uint)virtualKey), IntPtr.Zero);
		}

		private bool cursorVisible;

		/// <summary>
		/// Get or set the cursor visibility.
		/// </summary>
		public override bool CursorVisible { get => cursorVisible;
			set
			{
				cursorVisible = value;
				UpdateCursorVisibility();
			}
		}

		public override void UpdateCursorVisibility()
		{
			// BUG: Setting cursor visibility to "true" after being "false" does not work.
			User32Methods.SetCursor(cursorVisible
				? User32Methods.LoadCursor(appHandle, (IntPtr)SystemCursor.IDC_ARROW)
				: IntPtr.Zero);

			User32Methods.ShowCursor(cursorVisible);
		}

		/// <summary>
		/// Set the window caption.
		/// </summary>
		public override string Caption
		{
			set => User32Methods.SetWindowText(WindowHandle, value);
		}

		/// <summary>
		/// Set the mouse cursor's position to a point on-screen.
		/// </summary>
		/// <param name="location">The point at which to put the cursor.</param>
		public override void SetCursorPos(Point location)
		{
			User32Methods.SetCursorPos(location.X, location.Y);
		}

        /// <summary>
        /// Emulates the mouse move event.
        /// </summary>
        /// <param name="location">
        /// The <see cref="Point"/> indicating the location of the mouse at the event.
        /// </param>
        /// <remarks>
        /// This method is mainly used for testing, but it may be useful for some application.
        /// </remarks>
        public override void EmulatesMouseMove(Point location) { }

		/// <summary>
		/// Emulates the mouse buttons pressed event.
		/// </summary>
		/// <param name="location">
		/// The <see cref="Point"/> indicating the location of the mouse at the event.
		/// </param>
		/// <param name="buttons">
		/// The <see cref="MouseButton"/> indicating the buttons pressed at the event.
		/// </param>
		/// <remarks>
		/// This method is mainly used for testing, but it may be useful for some application.
		/// </remarks>
		public override void EmulatesMouseButtonClick(Point location, MouseButton buttons) { }

		/// <summary>
		/// Emulates the mouse buttons double-pressed event.
		/// </summary>
		/// <param name="location">
		/// The <see cref="Point"/> indicating the location of the mouse at the event.
		/// </param>
		/// <param name="buttons">
		/// The <see cref="MouseButton"/> indicating the buttons double-pressed at the event.
		/// </param>
		/// <remarks>
		/// This method is mainly used for testing, but it may be useful for some application.
		/// </remarks>
		public override void EmulatesMouseButtonDoubleClick(Point location, MouseButton buttons) { }

		/// <summary>
		/// Emulates the mouse buttons wheel event.
		/// </summary>
		/// <param name="location">
		/// The <see cref="Point"/> indicating the location of the mouse at the event.
		/// </param>
		/// <param name="ticks">
		/// The <see cref="Int32"/> indicating the the wheel ticks.
		/// </param>
		/// <remarks>
		/// This method is mainly used for testing, but it may be useful for some application.
		/// </remarks>
		public override void EmulatesMouseWheel(Point location, int ticks) {  }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting managed/unmanaged resources.
		/// </summary>
		/// <param name="disposing">
		/// A <see cref="System.Boolean"/> indicating whether the disposition is requested explictly.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			DeleteContext();
			DestroyDeviceContext();

			if (windowHandle != IntPtr.Zero) {
				CheckThread();

				User32Methods.DestroyWindow(windowHandle);
				windowHandle = IntPtr.Zero;
				ownerThread = 0;
			}

			if (className != null) {
				User32Methods.UnregisterClass(className, appHandle);
				className = null;
			}

			// Base implementation
			base.Dispose(disposing);
		}

		#endregion
	}
}
