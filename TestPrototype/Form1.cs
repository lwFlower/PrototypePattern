using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace TestPrototype
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Pet1.Text = null;
            Pet2.Text = null;
            Pet3.Text = null;
        }
        public interface Animal
        {
            Animal clone();
            String GetQuality();
            String GetType();
            Bitmap GetImage();
        }

        public class Cat : Animal
        {
            private String quality;
            private String type = "кот";
            private Bitmap image;
            public Cat(String quality, Bitmap image)
            {
                this.quality = quality;
                this.image = image;
            }
            public Animal clone()
            {
                return new Cat(this.quality, this.image);
            }
            public String GetQuality()
            {
                return quality;
            }
            public String GetType()
            {
                return type;
            }
            public Bitmap GetImage()
            {
                return image;
            }
        }

        public class Pig : Animal
        {
            private String quality;
            private String type = "свин";
            private Bitmap image;
            public Pig(String quality, Bitmap image)
            {
                this.quality = quality;
                this.image = image;
            }
            public Animal clone()
            {
                return new Pig(this.quality, this.image);
            }
            public String GetQuality()
            {
                return quality;
            }
            public String GetType()
            {
                return type;
            }
            public Bitmap GetImage()
            {
                return image;
            }
        }

        //Cat
        static Animal AvgCatPrototype = new Cat("обычный", Properties.Resources.AvgCat);
        static Animal RareCatPrototype = new Cat("редкий", Properties.Resources.RareCat);
        static Animal LegendCatPrototype = new Cat("легендарный", Properties.Resources.LegendCat);

        //пигус
        static Animal AvgPigPrototype = new Pig("обычный", Properties.Resources.AvgPig);
        static Animal RarePigPrototype = new Pig("редкий", Properties.Resources.RarePig);
        static Animal LegendPigPrototype = new Pig("легендарный", Properties.Resources.LegendPig);
 

        static Animal Guest1, Guest2, Guest3;

        List<Animal> Average = new List<Animal>() { AvgCatPrototype, AvgPigPrototype };
        List<Animal> Rare = new List<Animal>() { RareCatPrototype, RarePigPrototype };
        List<Animal> Legend = new List<Animal>() { LegendCatPrototype, LegendPigPrototype };
        List<Animal> Guests = new List<Animal>() { Guest1, Guest2, Guest3 };

        int GuestID;
        Animal Specific;
        public void Clear()
        {
            //pic1
            pic1.Image = null;
            Pet1.Text = null;
            pic1.BorderStyle = BorderStyle.None;
            //pic2
            pic2.Image = null;
            Pet2.Text = null;
            pic2.BorderStyle = BorderStyle.None;
            //pic3
            pic3.Image= null;
            Pet3.Text = null;
            pic3.BorderStyle = BorderStyle.None;
            GuestID = -1;
        }

        public void btSpin_Click(object sender, EventArgs e)
        {
            Clear();

            Random rnd = new Random();
            int number = rnd.Next(1, 4);
            for (int count = 0; count < number; count++)
            {
                double chance = rnd.NextDouble();
                int typeChance = rnd.Next(0, 2);
                if (chance <= 0.7)
                {
                    Guests[count] = Average[typeChance].clone();
                }
                else if (chance > 0.7 && chance <= 0.9)
                {
                    Guests[count] = Rare[typeChance].clone();
                }
                else
                {
                    Guests[count] = Legend[typeChance].clone();
                }

                //картинки
                Bitmap picType;
                if (Guests[count].GetType() == "кот") picType = Properties.Resources.cat;
                else picType = Properties.Resources.pig;
                if (count == 0)
                {
                    Pet1.Text = Guests[count].GetQuality() + " " + Guests[count].GetType();
                    pic1.Image = picType;
                }
                else if (count == 1)
                {
                    Pet2.Text = Guests[count].GetQuality() + " " + Guests[count].GetType();
                    pic2.Image = picType;
                }
                else
                {
                    Pet3.Text = Guests[count].GetQuality() + " " + Guests[count].GetType();
                    pic3.Image = picType;
                }

            }
        }

        //фотография
        private void pic1_Click(object sender, EventArgs e)
        {
            GuestID = 0;
            pic1.BorderStyle = BorderStyle.Fixed3D;
            pic2.BorderStyle = BorderStyle.None;
            pic3.BorderStyle = BorderStyle.None;
        }
        private void pic2_Click(object sender, EventArgs e)
        {
            GuestID = 1;
            pic2.BorderStyle = BorderStyle.Fixed3D;
            pic1.BorderStyle = BorderStyle.None;
            pic3.BorderStyle = BorderStyle.None;
        }

        private void pic3_Click(object sender, EventArgs e)
        {
            GuestID = 2;
            pic3.BorderStyle = BorderStyle.Fixed3D;
            pic2.BorderStyle = BorderStyle.None;
            pic1.BorderStyle = BorderStyle.None;
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;
            pictureBox6.Image = null;
        }

        private void btF2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void btPhoto_Click(object sender, EventArgs e)
        {
            if (GuestID == -1) { Notif.Visible = true; }
            else
            {
                Notif.Visible = false;
                Specific = Guests[GuestID].clone();
                Bitmap Photo = Specific.GetImage();
                Boolean IsPalced = false;
                while (IsPalced == false)
                {
                    if (pictureBox1.Image == null)
                    {
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox1.Image = Specific.GetImage();
                        IsPalced = true;
                    }
                    else
                    if (pictureBox2.Image == null)
                    {
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox2.Image = Specific.GetImage();
                        IsPalced = true;
                    }
                    else
                    if (pictureBox3.Image == null)
                    {
                        pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox3.Image = Specific.GetImage();
                        IsPalced = true;
                    }
                    else
                    if (pictureBox4.Image == null)
                    {
                        pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox4.Image = Specific.GetImage();
                        IsPalced = true;
                    }
                    else
                    if (pictureBox5.Image == null)
                    {
                        pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox5.Image = Specific.GetImage();
                        IsPalced = true;
                    }
                    else
                    if (pictureBox6.Image == null)
                    {
                        pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox6.Image = Specific.GetImage();
                        IsPalced = true;
                    }
                    break;
                }
            }

        }
    }
}
