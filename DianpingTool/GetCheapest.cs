using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianpingTool
{
    public partial class GetCheapest : Form
    {
        public GetCheapest()
        {
            InitializeComponent();
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            List<decimal> prices = new List<decimal>() ;
            for (var i = 0; i < txtPrices.Lines.Count(); i++)
            {
                prices.Add(decimal.Parse(txtPrices.Lines[i]));
            }
            //PrintSolution(prices);
            List<KeyValuePair<decimal, decimal>> Coupons = new List<KeyValuePair<decimal, decimal>>();
            for (var i = 0; i < txtCoupons.Lines.Count(); i++)
            {
                var line = txtCoupons.Lines[i];
                var pair = line.Split(new string[] {" " }, StringSplitOptions.RemoveEmptyEntries);
                Coupons.Add(new KeyValuePair<decimal, decimal>(decimal.Parse(pair[0]), decimal.Parse(pair[1])));
            }

            var ExpressFee = decimal.Parse(txtExpressFee.Text);

            //Dianping P = new Dianping();

            var emptySolution = new List<int>();
            var solutions = CombinationTool.DivideInt(prices.Count, prices.Count, emptySolution);
            var arragements = CombinationTool.Assignments(solutions, prices);
            //Console.WriteLine("--all arranges--");
            //PrintSolutions(arragements);

            var minPrice = 100000M;
            var minSolution = new List<List<decimal>>();
            txtResult.Clear();
            foreach (var solution in arragements)
            {
                var opt = new Option();
                opt.PriceCombination = solution;
                var price = opt.CalcFee(Coupons, ExpressFee);

                if (price < minPrice)
                {
                    minPrice = price;
                    minSolution = solution;
                    //Console.Write("{0}= ", price);
                    var line = PrintSolution(minPrice,minSolution);
                    txtResult.AppendText(line);
                    txtResult.Select(txtResult.Text.Length, 0);
                    txtResult.ScrollToCaret();
                }
            }

            
            //Console.WriteLine("--Cheapest--");
            //Console.WriteLine("{0}=", minPrice);
            //PrintSolutions(minSolution);

            //Console.ReadLine();
        }

        private string PrintSolution(decimal minPrice, List<List<decimal>> minSolution)
        {
            StringBuilder line = new StringBuilder( string.Format("{0}=", minPrice));
            foreach (var s in minSolution)
            {
                line.Append("[");
                foreach (var d in s)
                {
                    line.Append(string.Format("{0} ", d));
                }
                line.Append("],");
            }
            line.Remove(line.Length-1, 1);
            line.AppendLine();
            return line.ToString();
        }

        private void GetCheapest_Load(object sender, EventArgs e)
        {

        }

    }
}
