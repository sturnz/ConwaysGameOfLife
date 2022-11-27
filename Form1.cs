namespace ConwaysGameOfLife
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initializeBlocks(); 
        }

        void initializeBlocks()
        {
            setRules();
            block = new bool[maxSize, maxSize];
            buffer = new bool[maxSize, maxSize];

            for (int i = 0; i < maxSize; i++)
            {
                for (int j = 0; j < maxSize; j++)
                {
                    // random number between 0 and 1
                    block[i, j] = (r.Next(2) == 0);
                }
            }
            for (int i = 0; i < maxSize; i++)
            {
                block[0, i] = false;    
                block[maxSize-1, i] = false;    
                block[i, 0] = false;    
                block[i, maxSize-1] = false;    
            }
            myBitmap        = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            timer1.Enabled  = true;
        }

        private void setRules()
        {
            for (int i = 0; i < 9; i++)
            {
                rule[0, i] = false;
                rule[1, i] = false;
            }
            rule[0, 2] = true;
            rule[0, 3] = true;
            rule[1, 3] = true;
        }


        


        bool[,] block;
        bool[,] buffer;
        int     maxSize     = 100;
        long    tickCount      = 0;
        Random  r            = new Random();
        Bitmap  myBitmap;
        bool[,]  rule       = new bool[2,9]; // self is on / off

        private void cellTick()
        {
            // ohne die Ecken Range Probleme
            for (int i = 1; i < maxSize - 1; i++)
            {
                for (int j = 1; j < maxSize - 1; j++)
                {
                    int sum = 0;
                    // nmumber of neighbours that are on
                    if (block[i - 1, j - 1]) sum++;
                    if (block[i, j - 1]) sum++;
                    if (block[i + 1, j - 1]) sum++;
                    if (block[i - 1, j]) sum++;
                    if (block[i + 1, j]) sum++;
                    if (block[i - 1, j + 1]) sum++;
                    if (block[i, j + 1]) sum++;
                    if (block[i + 1, j + 1]) sum++;

                    if (block[i, j]) buffer[i, j] = rule[0, sum];
                    else buffer[i, j] = rule[1, sum];
                }

            }
            // now everything is in the buffer, copy it back
            for (int i = 1; i < maxSize - 1; i++)
            {
                for (int j = 1; j < maxSize - 1; j++)
                {
                    block[i, j] = buffer[i, j];
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            mainLoop();
        }

        private void boardRepaint(object sender, PaintEventArgs e)
        {
            tickCount++;
            this.Text = tickCount.ToString();
            int w = 4;
            cellTick();
            Graphics graphics = Graphics.FromImage(myBitmap);
            
            for (int i = 0; i < maxSize; i++)
            {
                for (int j = 0; j < maxSize; j++)
                {
                    if (block[i, j]) graphics.FillRectangle(Brushes.Black, i * w, j * w, w, w);
                    else graphics.FillRectangle(Brushes.White, i * w, j * w, w, w);
                }
            }
            graphics.Dispose();
            pictureBox1.Image = myBitmap;
            
            for (long i = 0; i < 100000000; i++)
            {
                int j = 0;
            }
            timer1.Start();
        }
        void mainLoop()
        {
            timer1.Enabled = false;
            pictureBox1.Refresh();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}