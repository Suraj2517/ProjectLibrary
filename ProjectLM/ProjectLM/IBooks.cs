using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectLM;
using System.Threading.Tasks;

namespace ProjectLM
{
    public interface IBooks
    {
        int AddBookDetails();

        int EditBookDetails();

        int DeleteBookDetails();

    }
}