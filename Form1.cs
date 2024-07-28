using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp9.Properties;
using static System.Net.Mime.MediaTypeNames.Image;

namespace WindowsFormsApp9
{
    public partial class TicTacToe : Form
    {
       

        private PictureBox _Dpic;
        private PictureBox _Spic;
        private PictureBox _tpic;
        private Image _dimage;

        stGameStatus GameStatus;
        enPlayer PlayerTurn = enPlayer.Player1;
        enum enPlayer
        {
            Player1,
            Player2
        }

        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        }

        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
          

        }

     
        public bool CheckValues(PictureBox PB1, PictureBox PB2, PictureBox PB3)
        {

            
            if (PB1.Tag.ToString()!="?"&& PB1.Tag.ToString() == PB2.Tag.ToString()&& PB1.Tag.ToString() == PB3.Tag.ToString() )
            {

                if (PB1.Tag.ToString() == "Black")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }

            }

            GameStatus.GameOver = false;
            return false;


        }

        void EndGame()
        {

            lblTurn.Text = "Game Over";
            switch (GameStatus.Winner)
            {

                case enWinner.Player1:

                    lblWinner.Text = "Player1";
                    break;

                case enWinner.Player2:

                    lblWinner.Text = "Player2";
                    break;

                default:

                    lblWinner.Text = "Draw";
                    break;

            }

            MessageBox.Show("GameOver", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public void CheckWinner()
        {


            //checked rows
            //check Row1
            if (CheckValues(pictureBox1, pictureBox2, pictureBox3))
                return;

            //check Row2
            if (CheckValues(pictureBox4, pictureBox5, pictureBox6))
                return;

            //check Row3
            if (CheckValues(pictureBox7, pictureBox8, pictureBox9))
                return;

            //checked cols
            //check col1
            if (CheckValues(pictureBox1, pictureBox4, pictureBox7))
                return;

            //check col2
            if (CheckValues(pictureBox2, pictureBox5, pictureBox8))
                return;

            //check col3
            if (CheckValues(pictureBox3, pictureBox6, pictureBox9))
                return;

            //check Diagonal

            //check Diagonal1
            if (CheckValues(pictureBox1, pictureBox5, pictureBox9))
                return;

            //check Diagonal2
            if (CheckValues(pictureBox3, pictureBox5, pictureBox7))
                return;


        }
        public TicTacToe()
        {
            InitializeComponent();

           
            PlayerTurn = enPlayer.Player1;
            lblTurn.Text = "Player 1";

            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.GameInProgress;
            lblWinner.Text = "In Progress";

        }


        private void TicTacToe_Paint(object sender, PaintEventArgs e)
        {
            //drow horizantal and vertical line

            Graphics graph = e.Graphics;
            Pen pen = new Pen(Color.Black, 2);

            graph.DrawLine(pen, new Point(800, 260), new Point(550, 260));
            graph.DrawLine(pen, new Point(800, 355), new Point(550, 355));

            graph.DrawLine(pen, new Point(630, 200), new Point(630, 420));
            graph.DrawLine(pen, new Point(720, 200), new Point(720, 420));

        }
       

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            //source picturebox
            _Spic = sender as PictureBox;

            //allow picturebox to drag and move
            if (_Spic!=null&&e.Button==MouseButtons.Left)
            {
                
                _Dpic = _Spic;// to make source picturebox invisible after drop it

                _Spic.DoDragDrop(_Spic.Image, DragDropEffects.Copy);
            }

          

        }
       
        private void TicTacToe_Load(object sender, EventArgs e)
        {
            pictureBox1.AllowDrop= true;
            pictureBox2.AllowDrop = true;
            pictureBox3.AllowDrop = true;
            pictureBox4.AllowDrop = true;
            pictureBox5.AllowDrop = true;
            pictureBox6.AllowDrop = true;
            pictureBox7.AllowDrop = true;
            pictureBox8.AllowDrop = true;
            pictureBox9.AllowDrop = true;
        }



        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
          


           if( e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Copy;
            }

            else
            {
                e.Effect = DragDropEffects.None;
            }
           
        }
       
        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            _tpic = sender as PictureBox;

            if (_tpic.Tag.ToString() != null)
            {
                switch (PlayerTurn)
                {
                    case enPlayer.Player1:

                        if (_Dpic.Tag.ToString() == "Red")
                        {
                            _tpic.Image = _tpic.Image;
                            _Spic.Image = _Dpic.Image;
                            _tpic.Tag = _tpic.Tag;
                            MessageBox.Show("cannot play in a row");
                            break;



                        }
                        else
                        {

                            
                                PlayerTurn = enPlayer.Player2;
                                lblTurn.Text = "Player 2";

                                _tpic.Tag = "Black";
                                _dimage = (Image)e.Data.GetData(DataFormats.Bitmap);

                                _tpic.Image = _dimage;


                                CheckWinner();
                                _tpic.Refresh();


                                //make source picturebox invisible after dropped

                                _Dpic.Image = null;
                                break;
                            }

                        

                

                    case enPlayer.Player2:

                        if (_Dpic.Tag.ToString() == "Black")
                        {
                            _tpic.Image = _tpic.Image;
                            _Spic.Image = _Dpic.Image;
                            _tpic.Tag = _tpic.Tag;
                            MessageBox.Show("cannot play in a row");
                            break;
                        }
                        else
                        {
                           
                            
                                PlayerTurn = enPlayer.Player1;
                                lblTurn.Text = "Player 1";

                                _tpic.Tag = "Red";
                                _dimage = (Image)e.Data.GetData(DataFormats.Bitmap);

                                _tpic.Image = _dimage;


                                CheckWinner();
                                _tpic.Refresh();


                                //make source picturebox invisible after dropped

                                _Dpic.Image = null;
                                break;
                            }
                        }


                }
            }

        

       

        private void RestPictureBox(PictureBox PBOX)
        {
            PBOX.Image =null;
            PBOX.Tag = "?";
            PBOX.BackColor = Color.Transparent;

        }
        private void RestartGame()
        {

            RestPictureBox(pictureBox1);
            RestPictureBox(pictureBox2);
            RestPictureBox(pictureBox3);
            RestPictureBox(pictureBox4);
            RestPictureBox(pictureBox5);
            RestPictureBox(pictureBox6);
            RestPictureBox(pictureBox7);
            RestPictureBox(pictureBox8);
            RestPictureBox(pictureBox9);


            if (BlackLargePB1.Image == null || BlackLargePB2.Image == null || BlackLargePB3.Image == null
               || BlackMediumPB1.Image==null || BlackMediumPB2.Image == null || BlackMediumPB3.Image == null
              || BlackSmallPB1.Image==null || BlackSmallPB2.Image == null || BlackSmallPB3.Image == null
              || RedLargePB1.Image==null || RedLargePB2.Image == null || RedLargePB3.Image == null
              || RedMediumPB1.Image== null || RedMediumPB2.Image == null || RedMediumPB3.Image == null
              || RedSmallPB1.Image== null || RedSmallPB2.Image == null || RedSmallPB3.Image == null)
            {
                BlackLargePB1.Image = Properties.Resources.black_l;
                BlackLargePB2.Image = Properties.Resources.black_l;
                BlackLargePB3.Image = Properties.Resources.black_l;
                
                BlackMediumPB1.Image=Properties.Resources.black_m;
                BlackMediumPB2.Image = Properties.Resources.black_m;
                BlackMediumPB3.Image = Properties.Resources.black_m;

                BlackSmallPB1.Image = Properties.Resources.black_s;
                BlackSmallPB2.Image = Properties.Resources.black_s;
                BlackSmallPB3.Image = Properties.Resources.black_s;

                RedLargePB1.Image=Properties.Resources._1553047_l_letter_red_alphabet_letters_icon;
                RedLargePB2.Image = Properties.Resources._1553047_l_letter_red_alphabet_letters_icon;
                RedLargePB3.Image = Properties.Resources._1553047_l_letter_red_alphabet_letters_icon;

                RedMediumPB1.Image = Properties.Resources.red_m;
                RedMediumPB2.Image = Properties.Resources.red_m;
                RedMediumPB3.Image = Properties.Resources.red_m;

                RedSmallPB1.Image = Properties.Resources.red_s;
                RedSmallPB2.Image = Properties.Resources.red_s;
                RedSmallPB3.Image = Properties.Resources.red_s;





            }


            PlayerTurn = enPlayer.Player1;
            lblTurn.Text = "Player 1";
           
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.GameInProgress;
            lblWinner.Text = "In Progress";



        }

        private void RestartButn_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

     
    }
}
