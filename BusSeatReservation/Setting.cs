using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusSeatReservation
{
    public class Setting
    {
        public int num { get; set; } = 0;
        public string Id { get; set; } = string.Empty;
        //public string Pw { get; set; } = "";
        //public bool Logged { get; set; } = false;

        private static Setting _instance = null;
        public static Setting Instance
        {
            get
            {
                _instance = _instance ?? new Setting(); 
                return _instance;
            }
        }
        private Setting()
        {

        }
    }
}
