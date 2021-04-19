using System.Collections.Generic;
using MyConsole.domain;
using MyConsole.util;

namespace MyConsole.dao
{
    public class TexDao
    {
        public List<Tex> GetTexList()
        {
            var db = ClientUtil.GetConnection();
            var texes = db.Queryable<Tex>().ToList();
            db.Close();
            return texes;
        }


        public void SaveTexList(List<Tex> texes)
        {
            var db = ClientUtil.GetConnection();
            db.Insertable(texes).ExecuteCommand();
            db.Close();
        }
    }
}