using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianpingTool
{
    public class Option
    {
        public List<List<decimal>> PriceCombination { get; set; }
        //public List<KeyValuePair<decimal,decimal>> Coupons { get; set; }
        public decimal CalcFee(List<KeyValuePair<decimal,decimal>> Coupons,decimal ExpressFee)
        {
            var totalFee = 0M;
            foreach (var item in PriceCombination)
            {
                var groupFee = (from t in item select t).Sum();
                for (var i = Coupons.Count-1; i > 0; i--)
                {
                    if (groupFee >= Coupons[i].Key)
                    {
                        groupFee -= Coupons[i].Value;
                        break;
                    }
                }
                totalFee += groupFee + ExpressFee;

            }
            return totalFee;
        }

    }
}
