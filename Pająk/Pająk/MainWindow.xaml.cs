using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;   // ← Canvas.SetLeft/Top
using System.Windows.Interop;   // ← WindowInteropHelper
using System.Windows.Media;     // ← VisualTreeHelper (na przyszłość DPI)

namespace Pająk
{
    public partial class MainWindow : Window
    {
        private AppSettings _cfg = new(); // zapamiętujemy config, używamy po SourceInitialized
                                          // === ANIMACJA: pola ===
        private bool _animRunning;
        private TimeSpan? _lastRenderTime;

        // Pozycja i prędkość w DIP (device-independent pixels)
        private double _x, _y;
        private Vector _vel = new Vector(-1, +1); // startowy kierunek
        private const double BASE_SPEED = 200.0;   // 200 DIP/s, mnożone przez SpiderSpeed

        public MainWindow()
        {
            InitializeComponent();

            // Ustaw okno na pełny ekran (bez zakrywania paska zadań)
            var bounds = SystemParameters.WorkArea;
            Left = bounds.Left;
            Top = bounds.Top;
            Width = bounds.Width;
            Height = bounds.Height;

            TryApplySettings();             // wczytuje _cfg i np. Topmost
            SnapsToDevicePixels = true;
            UseLayoutRounding = true;

            // Win32 flagi dopiero po utworzeniu HWND
            SourceInitialized += OnSourceInitialized;

            // Gdy wizualne drzewo już powstanie – ustaw pozycję pająka
            Loaded += OnLoadedForStartPosition;

            // Zmiana rozmiaru – przelicz (na overlay zwykle stałe, ale bezpiecznie)
            SizeChanged += (_, __) => ApplyStartPositionSafely();

            // Gdy obrazek pozna swój rozmiar – przelicz
            // (Jeśli SpiderImage nie istnieje w XAML, te eventy po prostu nic nie zrobią)
            if (SpiderImage != null)
            {
                SpiderImage.Loaded += (_, __) => ApplyStartPositionSafely();
                SpiderImage.SizeChanged += (_, __) => ApplyStartPositionSafely();
            }
            // === START ANIMACJI ===
            Loaded += (_, __) =>
            {
                ApplyStartPositionSafely(); // ustal pozycję startową
                _x = Canvas.GetLeft(SpiderImage);
                _y = Canvas.GetTop(SpiderImage);

                if (_vel.Length == 0) _vel = new Vector(1, 0);
                _vel.Normalize();

                StartAnimation();
            };

            // zatrzymaj animację przy zamknięciu
            Unloaded += (_, __) => StopAnimation();

        }

        // Mamy pewność, że istnieje uchwyt okna
        private void OnSourceInitialized(object? sender, EventArgs e)
        {
            // Zastosuj click-through wg konfiguracji
            ApplyClickThrough(_cfg.ClickThroughEnabled);
        }

        private void OnLoadedForStartPosition(object? sender, RoutedEventArgs e)
        {
            // Po Loaded WPF jeszcze kończy pomiary – dajmy jeden tick
            Dispatcher.BeginInvoke(ApplyStartPositionSafely);
        }

        /// <summary>
        /// Bezpiecznie ustawia Canvas.Left/Top dla SpiderImage wg _cfg.StartPosition.
        /// Wykonuje się tylko, gdy znamy wymiary Canvas i Image.
        /// </summary>
        private void ApplyStartPositionSafely()
        {
            if (MainCanvas == null || SpiderImage == null) return;

            // Wymiary obrazka: preferuj Actual*, fallback do Width/Height, a ostatecznie 128
            double imgW = SpiderImage.ActualWidth > 0 ? SpiderImage.ActualWidth
                        : (SpiderImage.Width > 0 ? SpiderImage.Width : 128);
            double imgH = SpiderImage.ActualHeight > 0 ? SpiderImage.ActualHeight
                        : (SpiderImage.Height > 0 ? SpiderImage.Height : 128);

            // Wymiary obszaru: preferuj Actual*, fallback do okna
            double canvasW = MainCanvas.ActualWidth > 0 ? MainCanvas.ActualWidth : ActualWidth;
            double canvasH = MainCanvas.ActualHeight > 0 ? MainCanvas.ActualHeight : ActualHeight;

            if (canvasW <= 0 || canvasH <= 0 || imgW <= 0 || imgH <= 0)
                return; // jeszcze za wcześnie

            var pos = ParseStartPosition(_cfg?.StartPosition);
            var (left, top) = ComputeStartXY(pos, canvasW, canvasH, imgW, imgH);

            Canvas.SetLeft(SpiderImage, left);
            Canvas.SetTop(SpiderImage, top);
        }

        // ======== Twoje istniejące rzeczy (JSON, click-through) pozostają bez zmian ========

        private void TryApplySettings()
        {
            string cfgPath = Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json");
            Debug.WriteLine($"[Pająk] Szukam konfiguracji: {cfgPath}");

            if (!File.Exists(cfgPath))
            {
                MessageBox.Show(
                    $"Nie znaleziono pliku konfiguracji:\n{cfgPath}\n\n" +
                    "Ustaw w projekcie: Build Action=Content, Copy to Output=Copy if newer.",
                    "Pająk — brak configu",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            try
            {
                string raw = File.ReadAllText(cfgPath, Encoding.UTF8);
                var preview = raw.Length > 60 ? raw.Substring(0, 60) : raw;
                MessageBox.Show($"Pierwsze znaki pliku:\n{preview}", "Podgląd appsettings.json");

                string trimmed = raw.TrimStart();
                if (trimmed.Length == 0 || (trimmed[0] != '{' && trimmed[0] != '['))
                {
                    MessageBox.Show(
                        "Plik nie zaczyna się od '{' ani '[' — to nie jest poprawny JSON.\n" +
                        "Usuń wszystko przed pierwszą klamrą.",
                        "Błędny format JSON",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                    return;
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true
                };

                var cfg = JsonSerializer.Deserialize<AppSettings>(trimmed, options);
                if (cfg == null)
                {
                    MessageBox.Show("Deserializacja zwróciła null – sprawdź składnię JSON.");
                    return;
                }

                _cfg = cfg; // zapamiętujemy

                // Zastosuj to, co można od razu
                Topmost = _cfg.AlwaysOnTop;

                Debug.WriteLine($"[Pająk] Wczytano config: " +
                                $"AlwaysOnTop={_cfg.AlwaysOnTop}, " +
                                $"ClickThroughEnabled={_cfg.ClickThroughEnabled}, " +
                                $"SpiderSpeed={_cfg.SpiderSpeed}, " +
                                $"StartPosition={_cfg.StartPosition}");

                MessageBox.Show($"Wczytano konfigurację:\n" +
                                $"AlwaysOnTop={_cfg.AlwaysOnTop}\n" +
                                $"ClickThroughEnabled={_cfg.ClickThroughEnabled}\n" +
                                $"SpiderSpeed={_cfg.SpiderSpeed}\n" +
                                $"StartPosition={_cfg.StartPosition}",
                                "Pająk — konfiguracja OK",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                // Po wczytaniu configu możemy już zaplanować pozycję startową
                // (wywołanie „na wszelki wypadek”, jeśli Loaded już był)
                ApplyStartPositionSafely();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Błąd wczytywania konfiguracji:\n\n{ex}",
                    "Pająk — wyjątek",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        private void ApplyClickThrough(bool enabled)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd == IntPtr.Zero) return;

            var exStyle = NativeMethods.GetWindowLongPtr(hwnd, NativeMethods.GWL_EXSTYLE);
            long styles = exStyle.ToInt64();

            if (enabled)
                styles |= NativeMethods.WS_EX_TRANSPARENT;
            else
                styles &= ~NativeMethods.WS_EX_TRANSPARENT;

            var prev = NativeMethods.SetWindowLongPtr(hwnd, NativeMethods.GWL_EXSTYLE, new IntPtr(styles));

            if (prev == IntPtr.Zero)
            {
                int err = Marshal.GetLastWin32Error();
                Debug.WriteLine($"SetWindowLongPtr failed. GetLastError={err}");
            }

            NativeMethods.SetWindowPos(
                hwnd, IntPtr.Zero, 0, 0, 0, 0,
                NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE |
                NativeMethods.SWP_NOZORDER | NativeMethods.SWP_FRAMECHANGED
            );
        }

        // ======== NOWE: helpery pozycji startowej ========

        private enum StartPos
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Center
        }

        private static StartPos ParseStartPosition(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return StartPos.TopRight;

            if (Enum.TryParse<StartPos>(value.Trim(), ignoreCase: true, out var pos))
                return pos;

            // Toleruj "Top-Right" / "Top Right" itp.
            string normalized = value.Replace("-", "", StringComparison.Ordinal)
                                     .Replace(" ", "", StringComparison.Ordinal);

            foreach (var name in Enum.GetNames(typeof(StartPos)))
            {
                if (string.Equals(name, normalized, StringComparison.OrdinalIgnoreCase))
                    return (StartPos)Enum.Parse(typeof(StartPos), name);
            }

            return StartPos.TopRight;
        }

        private static (double left, double top) ComputeStartXY(StartPos pos, double canvasW, double canvasH, double imgW, double imgH)
        {
            double left = 0, top = 0;

            switch (pos)
            {
                case StartPos.TopLeft:
                    left = 0; top = 0; break;

                case StartPos.TopRight:
                    left = canvasW - imgW; top = 0; break;

                case StartPos.BottomLeft:
                    left = 0; top = canvasH - imgH; break;

                case StartPos.BottomRight:
                    left = canvasW - imgW; top = canvasH - imgH; break;

                case StartPos.Center:
                    left = (canvasW - imgW) / 2.0;
                    top = (canvasH - imgH) / 2.0;
                    break;
            }

            // Nie wychodź poza ekran
            left = Math.Max(0, Math.Min(left, Math.Max(0, canvasW - imgW)));
            top = Math.Max(0, Math.Min(top, Math.Max(0, canvasH - imgH)));

            return (left, top);
        }
        // === ANIMACJA: logika klatkowania ===

        private void StartAnimation()
        {
            if (_animRunning) return;
            CompositionTarget.Rendering += OnRendering;
            _lastRenderTime = null;
            _animRunning = true;
        }

        private void StopAnimation()
        {
            if (!_animRunning) return;
            CompositionTarget.Rendering -= OnRendering;
            _animRunning = false;
        }

        private void OnRendering(object? sender, EventArgs e)
        {
            if (SpiderImage == null || MainCanvas == null) return;
            if (e is not RenderingEventArgs rea) return;

            if (_lastRenderTime is null)
            {
                _lastRenderTime = rea.RenderingTime;
                return;
            }

            var dt = (rea.RenderingTime - _lastRenderTime.Value).TotalSeconds;
            _lastRenderTime = rea.RenderingTime;

            if (dt <= 0 || dt > 0.25) return;

            double speed = BASE_SPEED * Math.Max(0.0, _cfg?.SpiderSpeed ?? 1.0);

            _x += _vel.X * speed * dt;
            _y += _vel.Y * speed * dt;

            BounceIfNeeded();

            Canvas.SetLeft(SpiderImage, _x);
            Canvas.SetTop(SpiderImage, _y);
        }

        /// <summary>
        /// Odbijanie od krawędzi okna.
        /// </summary>
        private void BounceIfNeeded()
        {
            double imgW = SpiderImage.ActualWidth > 0 ? SpiderImage.ActualWidth : (SpiderImage.Width > 0 ? SpiderImage.Width : 128);
            double imgH = SpiderImage.ActualHeight > 0 ? SpiderImage.ActualHeight : (SpiderImage.Height > 0 ? SpiderImage.Height : 128);

            double canvasW = MainCanvas.ActualWidth > 0 ? MainCanvas.ActualWidth : ActualWidth;
            double canvasH = MainCanvas.ActualHeight > 0 ? MainCanvas.ActualHeight : ActualHeight;

            if (canvasW <= 0 || canvasH <= 0) return;

            double minX = 0, minY = 0;
            double maxX = Math.Max(0, canvasW - imgW);
            double maxY = Math.Max(0, canvasH - imgH);

            bool bounced = false;

            if (_x < minX)
            {
                _x = minX;
                _vel.X = Math.Abs(_vel.X);
                bounced = true;
            }
            else if (_x > maxX)
            {
                _x = maxX;
                _vel.X = -Math.Abs(_vel.X);
                bounced = true;
            }

            if (_y < minY)
            {
                _y = minY;
                _vel.Y = Math.Abs(_vel.Y);
                bounced = true;
            }
            else if (_y > maxY)
            {
                _y = maxY;
                _vel.Y = -Math.Abs(_vel.Y);
                bounced = true;
            }

            if (bounced)
            {
                if (_vel.Length == 0) _vel = new Vector(1, 0);
                _vel.Normalize();
            }
        }

    }

    public sealed class AppSettings
    {
        public bool AlwaysOnTop { get; set; } = false;
        public bool ClickThroughEnabled { get; set; } = false;
        public double SpiderSpeed { get; set; } = 1.0;
        public string StartPosition { get; set; } = "TopRight";
    }

    internal static class NativeMethods
    {
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TRANSPARENT = 0x00000020;

        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOZORDER = 0x0004;
        public const uint SWP_FRAMECHANGED = 0x0020;

        // 64/32-bit safe Get/SetWindowLongPtr
        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW", SetLastError = true)]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongW", SetLastError = true)]
        private static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongW", SetLastError = true)]
        private static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(
            IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
            => IntPtr.Size == 8 ? GetWindowLongPtr64(hWnd, nIndex) : GetWindowLong32(hWnd, nIndex);

        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
            => IntPtr.Size == 8 ? SetWindowLongPtr64(hWnd, nIndex, dwNewLong) : SetWindowLong32(hWnd, nIndex, dwNewLong);
    }

}
