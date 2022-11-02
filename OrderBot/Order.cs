using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _traveldate = String.Empty;
        private string _travelTime = String.Empty;
        private string _travelPlace = String.Empty;
        private string _travelClass = String.Empty;


        private string _phone = String.Empty;

        public string Phone{
            get => _phone;
            set => _phone = value;
        }

        public string TravelDate{
            get => _traveldate;
            set => _traveldate = value;
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
        SET size = $travelDate
        WHERE phone = $phone
    ";
                commandUpdate.Parameters.AddWithValue("$travelDate", TravelDate);
                commandUpdate.Parameters.AddWithValue("$phone", Phone);
                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(size, phone)
            VALUES($travelDate, $phone)
        ";
                    commandInsert.Parameters.AddWithValue("$travelDate", TravelDate);
                    commandInsert.Parameters.AddWithValue("$phone", Phone);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
