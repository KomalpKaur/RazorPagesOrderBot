using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _travelDate = String.Empty;
        private string _travelTime = String.Empty;
        private string _travelPlace = String.Empty;
        private string _travelClass = String.Empty;


        private string _phone = String.Empty;

        public string Phone{
            get => _phone;
            set => _phone = value;
        }

        public string TravelDate{
            get => _travelDate;
            set => _travelDate = value;
        }
        public string TravelTime{
            get => _travelTime;
            set => _travelTime = value;
        }
        public string TravelPlace{
            get => _travelPlace;
            set => _travelPlace = value;
        }
        public string TravelClass{
            get => _travelClass;
            set => _travelClass = value;
        }

        public void Save(){
           using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        UPDATE orders
        SET travelDate = $travelDate , travelTime = $travelTime, travelPlace = $travelPlace, travelClass = $travelClass
        WHERE phone = $phone
    ";
                commandUpdate.Parameters.AddWithValue("$travelDate", TravelDate);
                commandUpdate.Parameters.AddWithValue("$travelTime", TravelTime);
                commandUpdate.Parameters.AddWithValue("$travelPlace", TravelPlace);
                commandUpdate.Parameters.AddWithValue("$travelClass", TravelClass);
                commandUpdate.Parameters.AddWithValue("$phone", Phone);
                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(travelDate, travelTime, travelPlace, travelClass, phone)
            VALUES($travelDate, $travelTime, $travelPlace, $travelClass, $phone)
        ";
                    commandInsert.Parameters.AddWithValue("$travelDate", TravelDate);
                    commandInsert.Parameters.AddWithValue("$travelTime", TravelTime);
                    commandInsert.Parameters.AddWithValue("$travelPlace", TravelPlace);
                    commandInsert.Parameters.AddWithValue("$travelClass", TravelClass);
                    commandInsert.Parameters.AddWithValue("$phone", Phone);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
