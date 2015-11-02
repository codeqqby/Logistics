using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Logistics.Models
{
    public class VerificationCode
    {
        private string code;
        public string Code
        {
            get
            {
                return code;
            }
        }

        private byte[] codeBuffer;

        public byte[] CodeBuffer
        {
            get
            {
                return codeBuffer;
            }
        }

        private string CreateRandomCode(int length)
        {
            int rand;
            char code;
            string randomcode = String.Empty;
            System.Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                rand = random.Next();
                if (rand % 3 == 0)
                {
                    code = (char)('A' + (char)(rand % 26));
                }
                else
                {
                    code = (char)('0' + (char)(rand % 10));
                }
                randomcode += code.ToString();
            }
            return randomcode;
        }

        public void GetVerificationCode(int length)
        {
            this.code = CreateRandomCode(length);
            Bitmap image = new Bitmap((int)Math.Ceiling((this.code.Length * 12.5)), 22);
            Graphics graphic = Graphics.FromImage(image);
            try
            {
                Random random = new Random();
                graphic.Clear(Color.White);
                int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
                for (int index = 0; index < 25; index++)
                {
                    x1 = random.Next(image.Width);
                    x2 = random.Next(image.Width);
                    y1 = random.Next(image.Height);
                    y2 = random.Next(image.Height);

                    graphic.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Red, Color.DarkRed, 1.2f, true);
                graphic.DrawString(this.code, font, brush, 2, 2);

                int x = 0;
                int y = 0;

                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    x = random.Next(image.Width);
                    y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                graphic.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                //输出图片流
                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Jpeg);
                    this.codeBuffer = stream.ToArray();
                }
            }
            finally
            {
                graphic.Dispose();
            }
        }
    }

}