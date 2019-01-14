using Cairo;
using Gdk;
using ZXing.Common;
using ZXing.Rendering;

namespace ZXing.Net.Mobile.GTK
{
    public class BitmapRenderer : IBarcodeRenderer<Pixbuf>
    {
        public Pixbuf Render(BitMatrix matrix, BarcodeFormat format, string content)
        {
            var black = new Cairo.Color(0, 0, 0);
            var white = new Cairo.Color(1, 1, 1);
            var surface = new ImageSurface(Format.RGB24, matrix.Width, matrix.Height);
            var cr = new Context(surface) { LineWidth = 1.0 };
            for (var x = 0; x < matrix.Width; x++)
            {
                for (var y = 0; y < matrix.Height; y++)
                {
                    cr.SetSourceColor(matrix[x, y] ? black : white);
                    cr.MoveTo(x, y);
                    cr.LineTo(x + surface.Stride, y);
                    cr.Stroke();
                }
            }
            return new Pixbuf(surface.Data,
                Colorspace.Rgb,
                true,
                8,
                matrix.Width,
                matrix.Height,
                surface.Stride);
        }

        public Pixbuf Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
        {
            return Render(matrix, format, content);
        }
    }
}