

namespace NewQRCodeThing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Point> TopLeft;
        List<bool> RectColorIsBlack;
        List<Point> WidthHeight;
        public MainWindow()
        {
            InitializeComponent();
            BitmapImage bi = new BitmapImage(new Uri("qrsimple.png", UriKind.Relative));
            int stride =  bi.PixelWidth  *  4;
            int size =  bi.PixelHeight  *  stride;
            byte[ ] pixels = new byte[size];
            bi.CopyPixels(pixels, stride, 0);
            TopLeft = new List<Point>( );
            RectColorIsBlack = new List<bool>( );
            WidthHeight = new List<Point>( );

            int x, y;
            x = 0;
            y = 0;
            while (y < bi.PixelHeight)
            {
                int tempHeight = 0;
                while (x < bi.PixelWidth)
                {
                    TopLeft.Add(new Point(x, y));
                    int index = y * stride + 4 * x;
                    byte blue = pixels[index];
                    byte green = pixels[index + 1];
                    byte red = pixels[index + 2];
                    byte alpha = pixels[index + 3];
                    if (red < 25  &&  green < 25 &&  blue < 25)
                    {
                        RectColorIsBlack.Add(true);
                    }
                    else
                    {
                        RectColorIsBlack.Add(false);
                    }
                    int tempWidth = 0;
                    for (int w = x; w < bi.PixelWidth; w++)
                    {
                        index = y * stride + 4 * w;
                        blue = pixels[index];
                        green = pixels[index + 1];
                        red = pixels[index + 2];
                        alpha = pixels[index + 3];
                        if ((red < 25 && green < 25 && blue < 25 && !RectColorIsBlack[RectColorIsBlack.Count - 1]) ||
                                (red > 25 && green > 25 && blue > 25 && RectColorIsBlack[RectColorIsBlack.Count - 1]))
                        {
                         //MessageBox.Show("w: " + w.ToString() +"\ny " + y.ToString() );
                            tempWidth = w;
                            break;
                        }
                    }

                   
                    for (int h = y; h < bi.PixelHeight; h++)
                    {
                        index = h * stride + 4 * x;
                        blue = pixels[index];
                        green = pixels[index + 1];
                        red = pixels[index + 2];
                        alpha = pixels[index + 3];
                        if ((red < 25 && green < 25 && blue < 25 && !RectColorIsBlack[RectColorIsBlack.Count - 1]) ||
                                (red > 25 && green > 25 && blue > 25 && RectColorIsBlack[RectColorIsBlack.Count - 1]))
                        {
                       //   MessageBox.Show("x: " + x.ToString() + "\nh: " + h.ToString());
                            tempHeight = h;
                            break;
                        }
                    }

                    WidthHeight.Add(new Point(tempWidth, tempHeight));
                    MessageBox.Show("x, y " + TopLeft[TopLeft.Count - 1].ToString() + "\nWidth, Height" + WidthHeight[WidthHeight.Count - 1].ToString() + "\nColour is black: " + RectColorIsBlack[RectColorIsBlack.Count - 1].ToString());

                    x = x + tempWidth+1;
                }//end of X loop
                y = y + tempHeight + 1;
            }//end of Y loop

            MessageBox.Show(TopLeft.Count.ToString() + " number of boxes");
        }
    }
}
