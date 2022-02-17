using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace BusSeatReservation
{ 
    public class BusSeat
    { 
        public bool Available { get; set; } = false;
        public int SeatNumber { get; set; } = 0;

        public override string ToString()
        {
            return $"{nameof(SeatNumber)}: {SeatNumber} {nameof(Available)}: {Available}";
        }

    } 
}
