using System;

namespace Payslip_Kata{
    internal class Taxer{
        public static int GetTax(int annual){
            if ((annual > 18200)&&(annual < 37001)){
                return (int)Math.Round((((double)(annual - 18200))*0.19/12), MidpointRounding.ToEven);
            } else if ((annual > 37000)&&(annual <87001)){
                return (int)Math.Round(((3572+((double)(annual - 37000))*0.325)/12), MidpointRounding.ToEven);
            } else if ((annual > 87000)&&(annual < 180001)){
                return (int)Math.Round(((19822+((double)(annual - 87000))*0.37)/12), MidpointRounding.ToEven);
            } else if (annual >= 180001){
                return (int)Math.Round(((54232+((double)(annual - 180000))*0.45)/12), MidpointRounding.ToEven);
            } 

            return 0;
        }
    }
}