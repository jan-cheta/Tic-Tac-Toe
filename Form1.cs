using Microsoft.VisualBasic;

namespace Ancheta_Finals
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PictureBox[] tiles = new PictureBox[9];
        Label playerOrAi = new Label();
        Boolean isPlayerTurn = true;
        List<int> openTile = new List<int>();
        int[,] win = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },
                { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, {0,4,8},{2,4,6} };
        String gameConditon = "Running";

        private void Form1_Load(object sender, EventArgs e)
        {
            initTiles();
            initLabel();
        }

        void tileEvent(object sender, EventArgs e)
        {
            if (isPlayerTurn)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (sender == tiles[i])
                    {
                        playerTurn(tiles[i]);
                        isPlayerTurn = false;
                    }
                }
            }
        }


        void playerTurn(PictureBox pic)
        {
            openTile.Clear();
            pic.BackColor = Color.Blue;
            playerOrAi.Text = "AI Turn";
            playerOrAi.BackColor = Color.Red;
        }

        void initLabel()
        {
            playerOrAi.Text = "Player";
            playerOrAi.SetBounds(50, 20, 100, 30);
            playerOrAi.BackColor = Color.Blue;
            playerOrAi.ForeColor = Color.Black;
            playerOrAi.Font = new Font("Arial", 13);
            Controls.Add(playerOrAi);
        }

        void initTiles()
        {
            int x = 0, y = 0;
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new PictureBox();
                tiles[i].SetBounds(((Width / 2) - 160) + (x * 105), 70 + (y * 105), 100, 100);
                tiles[i].BackColor = Color.Black;
                tiles[i].Click += new System.EventHandler(tileEvent);
                Controls.Add(tiles[i]);
                if (x == 2)
                {
                    y++;
                    x = 0;
                }
                else
                {
                    x++;
                }
            }
        }

        void resetGame()
        {
            gameConditon = "Running";
            playerOrAi.BackColor = Color.Blue;
            playerOrAi.Text = "Player";
            openTile.Clear();
            isPlayerTurn = true;
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].BackColor = Color.Black;
            }
        }

        void aiTurn()
        {
            for (int i = 0; i < tiles.Length; ++i)
            {
                if (tiles[i].BackColor == Color.Black)
                {
                    openTile.Add(i);
                }
            }
            Random ran = new Random();
            int aiRan = ran.Next(openTile.Count);
            int aiNum = openTile[aiRan];
            tiles[aiNum].BackColor = Color.Red;
            playerOrAi.Text = "Player Turn";
            playerOrAi.BackColor = Color.Blue;
        }

        Boolean checkWinner(Color col)
        {
            Boolean winner = false;
            for (int i = 0; i < 8; i++)
            {
                if (tiles[win[i, 0]].BackColor == col & tiles[win[i, 1]].BackColor == col & tiles[win[i, 2]].BackColor == col)
                {
                    winner = true;
                }
            }
            return winner;
        }

        Boolean checkDraw()
        {
            Boolean draw = true;
            for (int i = 0; i < tiles.Length; i++)
            {
                if (tiles[i].BackColor == Color.Black)
                {
                    draw = false;
                }
            }
            return draw;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkWinner(Color.Blue))
            {
                resetGame();
                MessageBox.Show("Player Winner!");
            }
            else if (checkWinner(Color.Red))
            {
                resetGame();
                MessageBox.Show("AI Winner!");
            }
            else if (checkDraw())
            {
                resetGame();
                MessageBox.Show("Draw!");
            }
            else if (!isPlayerTurn)
            {
                aiTurn();
                isPlayerTurn = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            resetGame();
        }
    }
}