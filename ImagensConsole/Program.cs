using System.Drawing;
using System.Runtime.InteropServices;

namespace ImagensConsole
{
    public class Program
    {
        public static int ToConsoleColor(Color c)
        {
            //Aqui é possivel configurar a resolução
            int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0;
            index |= (c.R > 64) ? 4 : 0;
            index |= (c.G > 64) ? 2 : 0;
            index |= (c.B > 64) ? 1 : 0;
            return index;
        }

        public static async Task ConsoleWriteImage(Bitmap src)
        {
            //Aqui é possivel configurar a dimenção da imagem
            decimal min = 88;


            decimal pct = Math.Min(decimal.Divide(min, src.Width), decimal.Divide(min, src.Height));
            Size res = new Size((int)(src.Width * pct), (int)(src.Height * pct));
            Bitmap bmpMin = new Bitmap(src, res);
            for (int i = 0; i < res.Height; i++)
            {
                for (int j = 0; j < res.Width; j++)
                {
                    Console.ForegroundColor = (ConsoleColor)ToConsoleColor(bmpMin.GetPixel(j, i));
                    Console.Write("██");
                }
                Console.WriteLine();
            }
        }
        public static async Task run()
        {
            //pasta aonde esta localizado as imagens, é mostrado em ordem alfabetica.
            string[] frames = Directory.GetFiles(@"E:\casca de tomate frame\36");
            foreach (string frame in frames)
            {
                await ConsoleWriteImage(new Bitmap(frame));
                Console.Clear();
                Thread.Sleep(50);
            }
        }
        [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow", SetLastError = true)]
        static extern IntPtr GetConsoleHandle();
        public static void Main(string[] args)
        {
            run();


            //Abaixo o codigo para exibir imagens de outra forma diferente

            /*
            [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow", SetLastError = true)]
            static extern IntPtr GetConsoleHandle();
            var handler = GetConsoleHandle();
            string[] frames = Directory.GetFiles(@"E:\casca de tomate frame\36");
            foreach (string frame in frames)
            {
                using (var graphics = Graphics.FromHwnd(handler))
                using (var image = Image.FromFile(frame))
                    graphics.DrawImage(image, 50, 50, 650, 500);
                //é possivel configurar a resolução nos valores '650' e '500'
            }
            Console.ReadLine();
            */
        }
    }
}
