using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Interfaces
{
    public interface ISQLiteHelper
    {
        SQLiteAsyncConnection GetAsyncConnection();
    }
}
