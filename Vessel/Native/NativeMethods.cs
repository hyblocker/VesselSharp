﻿using System;
using System.Runtime.InteropServices;

namespace Vessel.Native
{
	internal static class Kernel32
	{
		[DllImport("kernel32")]
		public static extern IntPtr GetModuleHandleW(string moduleName);
	}

	internal static class User32
	{
		[DllImport("user32")]
		public static extern ushort RegisterClassExW(ref WNDCLASSEXW lpwcx);

		[DllImport("user32")]
		public static extern IntPtr LoadIconW(IntPtr hInstance, IntPtr lpIconName);

		[DllImport("user32")]
		public static extern IntPtr LoadCursorW(IntPtr hInstance, IntPtr lpIconName);

		[DllImport("user32", CharSet = CharSet.Unicode)]
		public static extern IntPtr CreateWindowExW(
			uint dwExStyle,
			IntPtr lpClassName,
			string lpWindowName,
			WS dwStyle,
			int x,
			int y,
			int nWidth,
			int nHeight,
			IntPtr hwndParent,
			IntPtr hMenu,
			IntPtr hInstance,
			IntPtr lpParam
		);

		[DllImport("user32")]
		public static extern IntPtr DefWindowProcW(IntPtr hwnd, WM msg, IntPtr wparam, IntPtr lparam);

		[DllImport("user32")]
		public static extern int GetMessage(
			out MSG msg,
			IntPtr hwnd,
			uint wMsgFilterMin,
			uint wMsgFilterMax
		);

		[DllImport("user32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool TranslateMessage(ref MSG lpMsg);

		[DllImport("user32")]
		public static extern IntPtr DispatchMessage(ref MSG lpMsg);

		[DllImport("user32")]
		public static extern void PostQuitMessage(int exitCode);

		[DllImport("user32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DestroyWindow(IntPtr hwnd);

		[DllImport("user32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool PostMessage(IntPtr hwnd, WM msg, IntPtr wparam, IntPtr lparam);

		[DllImport("user32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AdjustWindowRectEx(ref RECT lpRect, WS dwStyle, int bMenu, uint dwExStyle);

		[DllImport("user32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ShowWindow(IntPtr hwnd, SW nCmdShow);

		[DllImport("user32.dll")]
		public static extern bool SetWindowText(IntPtr hWnd, string text);

		[DllImport("user32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool UpdateWindow(IntPtr hwnd);

		[DllImport("user32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ValidateRect(IntPtr hwnd, IntPtr lpRect);
	}

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal delegate IntPtr WNDPROC(IntPtr hwnd, WM msg, IntPtr wparam, IntPtr lparam);

	internal struct WNDCLASSEXW
	{
		public uint cbSize;
		public CS style;
		public IntPtr lpfnWndProc;
		public int cbClsExtra;
		public int cbWndExtra;
		public IntPtr hInstance;
		public IntPtr hIcon;
		public IntPtr hCursor;
		public IntPtr hbrBackground;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszMenuName;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszClassName;
		public IntPtr hIconSm;
	}

	internal struct MSG
	{
		public IntPtr hwnd;
		public WM message;
		public IntPtr wParam;
		public IntPtr lParam;
		public uint time;
		public POINT pt;
	}

	internal struct POINT
	{
		public int x;
		public int y;
	}

	internal struct RECT
	{
		public int left;
		public int top;
		public int right;
		public int bottom;
	}

	internal enum CS : uint
	{
		HREDRAW = 0x2,
		VREDRAW = 0x1,
		OWNDC = 0x20
	}

	internal enum WS : uint
	{
		MAXIMIZEBOX = 0x10000,
		MINIMIZEBOX = 0x20000,
		THICKFRAME = 0x40000,
		SYSMENU = 0x80000,
		CAPTION = 0xC00000,
		VISIBLE = 0x10000000,
		OVERLAPPEDWINDOW = SYSMENU | THICKFRAME | CAPTION | MAXIMIZEBOX | MINIMIZEBOX
	}

	internal enum WM : uint
	{
		DESTROY = 0x2,
		SIZE = 0x5,
		PAINT = 0xF,
		CLOSE = 0x10,
		QUIT = 0x12,
		ERASEBKGND = 0x14,

		USER = 0x400,
		GAME_DONE = USER
	}

	internal enum SW
	{
		SHOWDEFAULT = 10
	}

	internal static class CW
	{
		public const int USEDEFAULT = unchecked((int)0x80000000);
	}

	internal static class IDI
	{
		public static readonly IntPtr APPLICATION = new IntPtr(32512);
	}

	internal static class IDC
	{
		public static readonly IntPtr ARROW = new IntPtr(32512);
	}
}
