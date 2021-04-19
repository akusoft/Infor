using SqlSugar;

namespace MyConsole.domain
{
    public class Tex
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public string Address { get; set; }
    }
}