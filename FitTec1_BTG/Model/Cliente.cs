using SQLite;

namespace FitTec1_BTG.Model
{
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Lastname { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
    }
}
