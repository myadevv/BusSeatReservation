using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusSeatReservation
{
    public partial class BusReservationControl : UserControl
    {
        private List<RadioButton> _seats; 
        public List<BusSeat> Seats { get; set; }
        private int _row ;
        private int _column;
        private int _entrance;

        public int Row { get; set; } = 9;

        public int Column { get; set; } = 5;

        public int Enterance { get; set; } = 2;
        public int SelectIndex { get; set; } = 0;
        private RadioButton CreateBusSeatControl()
        {
            var checkBox = new RadioButton();

            checkBox.Appearance = System.Windows.Forms.Appearance.Button;
            checkBox.Location = new System.Drawing.Point(118, 63); 
            checkBox.Size = new System.Drawing.Size(50, 22);
            checkBox.TabIndex = 0;
            checkBox.Location = new Point(-100, -100);
            checkBox.Click += CheckBox_Click;
            checkBox.UseVisualStyleBackColor = true;
            checkBox.BackColor = Color.White;  
            return checkBox;
        }

        private void CheckBox_Click(object sender, EventArgs e)
        { 
            var button = sender as RadioButton;

            int index = -1;
            if (int.TryParse( button.Text,out index))
            {
                Console.WriteLine(index.ToString());
                SelectIndex = index;
            }
        }

        private void InitializeBusSeat()
        {

            _seats = new List<RadioButton>();
            int seatIndex = 1;
            for (int i = 0; i < (Row * Column) - (Row) + 1; i++)
            {
                var control = CreateBusSeatControl();
                control.Text = seatIndex.ToString();
                Controls.Add(control);
                _seats.Add(control);
                seatIndex++;
            }
        }
        public void UpdateBusSeat()
        {

            /* padding */
            int left = Padding.Left;
            int right = Padding.Right;
            int top = Padding.Top;
            int bottom = Padding.Bottom;
            int space = 5;
            /* padding */ 
            int controlWidth = (Width - left - right - (space*Column))/Column;
            int controlHeight = (Height - bottom - top - (space * Row)) /Row;


            int index = 0;
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    if (j != Enterance || i == Row - 1)
                    {
                        _seats[index].Size = new Size(controlWidth, controlHeight);
                        _seats[index].Location = new Point(Padding.Left+ (j * controlWidth) + (j * space), Padding.Top+ (i * controlHeight) + (i * space));  
                        index++;
                    }
                }
            }
            if(Seats != null )
            {
                foreach (BusSeat seat in Seats)
                {
                    if (seat.Available == false)
                    {
                        //_seats[seat.SeatNumber - 1].Enabled = false;
                        _seats[seat.SeatNumber - 1].BackColor = Color.Gray; 
                    }
                    else
                    { 
                        _seats[seat.SeatNumber - 1].BackColor = Color.White;
                    }
                }
            }
        }
        public BusReservationControl()
        {
            InitializeComponent();
            InitializeBusSeat();
        }
        protected override void OnPaint(PaintEventArgs e)
        { 
            base.OnPaint(e);  
        }
    }
}
