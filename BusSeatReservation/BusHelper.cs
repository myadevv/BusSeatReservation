using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BusSeatReservation
{
    public static class BusHelper
    {
        private static  string MyConnection = "datasource=155.230.235.245;port=3306;username=cduser1;password=cduser@2020";
        public static int? GetIdFromUserName(string username)
        {
            int? result = -1;
            using (MySqlConnection myConn = new MySqlConnection(MyConnection))
            {

                myConn.Open();
                MySqlCommand selectCommand = new MySqlCommand($"select `userid` from lhjtest.userdata where username='{username}'", myConn);
                using (MySqlDataReader myReader = selectCommand.ExecuteReader())
                {
                    if (myReader.HasRows)
                    {
                        myReader.Read();
                        result = myReader["userid"] as int?;
                    }
                }
            }
            return result;
        }

        public static List<(int reservationId, int busid, int userid, int seatnum, DateTime reservationDateTime)> GetReservationFromUserId(int id)
        {
            var list = new List<(int reservationId, int busid, int userid, int seatnum, DateTime reservationDateTime)>(); 

            using (MySqlConnection myConn = new MySqlConnection(MyConnection))
            {
                myConn.Open();
                MySqlCommand selectCommand = new MySqlCommand($"select * from lhjtest.reserve WHERE userid='{id}'", myConn);
                using (MySqlDataReader myReader = selectCommand.ExecuteReader())
                {
                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            var reserveId = (int)myReader["reserveid"];
                            var busId = (int)myReader["busid"];
                            var userId = (int)myReader["userid"];
                            var seatnum = (int)myReader["seatnum"];
                            var reserveTime = DateTime.Parse(myReader["reserve_time"].ToString());
                            list.Add((reserveId, busId, userId, seatnum, reserveTime));
                        }
                    }
                }
            }
            return list;
        }
        public static (string name, int startId, int destId, DateTime start, DateTime end) GetBusInfoFromBusId(int busid)
        {
            string name = "";
            int destId=0, startId = 0;
            DateTime start = DateTime.Now, end = DateTime.Now;

            using (MySqlConnection myConn = new MySqlConnection(MyConnection))
            {
                myConn.Open();
                MySqlCommand selectCommand = new MySqlCommand($"select * from lhjtest.bus WHERE id='{busid}'", myConn);
                using (MySqlDataReader myReader = selectCommand.ExecuteReader())
                {
                    if (myReader.HasRows)
                    {
                        myReader.Read(); 
                        name = myReader["name"].ToString();
                        destId = (int)myReader["destinationid"];
                        startId = (int)myReader["startid"];
                        start = DateTime.Parse(myReader["departure"].ToString());
                        end = DateTime.Parse(myReader["arrival"].ToString());
                    }
                }
            }
            return (name,startId,destId,start,end);
        }
        public static string GetDestinationNameFromId(int destinationid)
        {
            string result = "";
            using (MySqlConnection myConn = new MySqlConnection(MyConnection))
            {

                myConn.Open();
                MySqlCommand selectCommand = new MySqlCommand($"select `name` from lhjtest.destination WHERE id='{destinationid}'", myConn);
                using (MySqlDataReader myReader = selectCommand.ExecuteReader())
                {
                    if (myReader.HasRows)
                    {
                        myReader.Read();
                        result = myReader["name"].ToString();
                    }
                }
            }
            return result;
        }
        //DELETE FROM `lhjtest`.`reserve` WHERE  `reserveid`=2;


        public static bool DeleteReservation(int reserveid)
        {
            bool result = false;
            using (MySqlConnection myConn = new MySqlConnection(MyConnection))
            {
                myConn.Open();
                MySqlCommand selectCommand = new MySqlCommand($"DELETE FROM `lhjtest`.`reserve` WHERE  `reserveid`='{reserveid}'", myConn);
                result = selectCommand.ExecuteNonQuery() != -1;
            }
            return result;
        }
        public static bool InsertReservation(int busid, int userid, int seatnum)
        {
            bool result = false;
            using (MySqlConnection myConn = new MySqlConnection(MyConnection))
            {

                myConn.Open();
                MySqlCommand selectCommand = new MySqlCommand($"INSERT INTO `lhjtest`.`reserve` ( `busid`, `userid`, `seatnum`, `reserve_time`) VALUES( '{busid}', '{userid}', '{seatnum}', now()); ", myConn);
                result = selectCommand.ExecuteNonQuery() != -1;
            }
            return result;
        }
    }
}
