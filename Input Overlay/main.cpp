#include <Windows.h>
#include <d2d1_1.h>

LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	if (uMsg == WM_DESTROY) {
		PostQuitMessage(0);
		return 0;
	}
	else {
		DefWindowProc(hwnd, uMsg, wParam, lParam);
	}
}

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE prevInstance, LPWSTR cmd, int nCmdShow)
{
	WNDCLASSEX windowclass;
	ZeroMemory(&windowclass, sizeof(WNDCLASSEX));
	windowclass.cbSize = sizeof(WNDCLASSEX);
	windowclass.hbrBackground = (HBRUSH)COLOR_WINDOW;
	windowclass.hInstance = hInstance;
	windowclass.lpfnWndProc = WindowProc;
	windowclass.lpszClassName = "MainWindow";
	windowclass.style = CS_HREDRAW | CS_VREDRAW;

	RegisterClassEx(&windowclass);

	HWND handle = CreateWindowEx(0, "MainWindow", "DirectX", WS_OVERLAPPEDWINDOW, 0, 0, 820, 640, NULL, NULL, hInstance, 0);

	if (!handle) return -1;

	ShowWindow(handle, nCmdShow);

	return 0;
}