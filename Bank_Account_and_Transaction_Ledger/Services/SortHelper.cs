using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class SortHelper
    {
        public static List<TransactionRecord> SortTransactionByAmount(List<TransactionRecord> records, bool desc = false)
        {
            List<TransactionRecord> sortedRecords = new List<TransactionRecord>(records);
            int n = sortedRecords.Count;

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n-1-i; j++)
                {
                    double amountJ = double.Parse(sortedRecords[j].Amount);
                    double amountJ1 = double.Parse(sortedRecords[j+1].Amount);

                    bool swap = desc ? amountJ < amountJ1 : amountJ > amountJ1;

                    if (swap)
                    {
                        TransactionRecord temp = sortedRecords[j];
                        sortedRecords[j] = sortedRecords[j + 1];
                        sortedRecords[j + 1] = temp;
                    }
                }
            }
            return sortedRecords;
        }

        public static List<TransactionRecord> SearchTransactionAbove(List<TransactionRecord> records, double threshold)
        {
            List<TransactionRecord> results = new List<TransactionRecord>();
            foreach (TransactionRecord r in records)
            {
                if (double.Parse(r.Amount) > threshold)
                {
                    results.Add(r);
                }
            }
            return results;
        }
    }
}
